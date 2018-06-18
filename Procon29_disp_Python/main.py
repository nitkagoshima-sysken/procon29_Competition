# -*- coding: utf-8 -*-

from pyzbar.pyzbar import decode
from PIL import Image
from wx import adv

import configparser as config

import procon29
import os
import wx
import hashlib
import sys

import win32con

wxId = {wx.ID_OK:0,
        wx.ID_CANCEL:1}

class SelectDialog(wx.Dialog):
    """
    Dialog fo select class.
    parent : Select parent object. Default=None
    id : Object id set. Default=wx.ID_ANY
    title : Dialog title setting.
    text : Dialog out string.
    size : Dialog size setting.
    """
    def __init__(self, parent=None, id=wx.ID_ANY, title='', text='', size=(400, 100)):
        wx.Dialog.__init__(self, parent, id, title, size=size)
        self.text = wx.StaticText(self, -1, text,style=wx.SIMPLE_BORDER)
        button1 = wx.Button(self, wx.ID_OK, '1')
        button1.SetDefault()
        button2 = wx.Button(self, wx.ID_CANCEL, '2')

        button_sizer = wx.StdDialogButtonSizer()
        button_sizer.AddButton(button1)
        button_sizer.AddButton(button2)
        button_sizer.Realize()

        sizer = wx.BoxSizer(wx.VERTICAL)
        sizer.Add(self.text, 1, wx.EXPAND | wx.ALL, 3)
        sizer.Add(button_sizer, 0, wx.EXPAND | wx.ALL, 5)
        self.SetSizer(sizer)

class Flags():
    """
    Color flag class.
    """
    def __init__(self):
        self.flag = [True, True]
        self.turn = 0
        self.end = False
        self.next = [0,0]
    
    def Clear(self):
        self.flag = [True, True]
        self.turn = 0
        self.end = False

class Modes():
    def __init__(self):
        self.manual = False
        self.auto = False
        self.battle = False
        self.learn = False
        self.AutoSetFlag = False

    def Clear(self):
        self.manual = False
        self.auto = False
        self.battle = False
        self.learn = False

    def AutoSet(self, log, field, agent, flags):
        self.bot = [procon29.Bot.FakeBot(log, field, agent[0].now, flags), procon29.Bot.FakeBot(log, field, agent[1].now, flags)]
        self.AutoSetFlag = True

def FiledClear():
    field[0].Destroy()
    del field[0]
    del agent[:]
    del agent_data[:]
    blue_flags.Clear()
    red_flags.Clear()
    bluepoint_text = wx.StaticText(panel, -1, '青\n取得済み得点:0 \n領域得点:0', style=wx.SIMPLE_BORDER, pos=(0,51))
    redpoint_text = wx.StaticText(panel, -1, '赤\n取得済み得点:0 \n領域得点:0', style=wx.SIMPLE_BORDER, pos=(0,99))
    log.LogWrite('Clear All\n')


def GameEndCheck():
    global nowturn
    global turn
    if turn == nowturn:
        return True
    

def col_func(blue_red, turn_exit):
    if col_check_blue[0] and blue_red:
        field[0].Coloring(agent_data[0], (agent[0], agent[1]), out_now=col_check_blue[0])
    elif turn_exit and blue_red:
        field[0].Coloring(agent_data[0], (agent[0], agent[1]), out_now=col_check_blue[1], out_get=col_check_blue[3])
    elif blue_red:
        field[0].Coloring(agent_data[0], (agent[0], agent[1]), out_now=col_check_blue[1], out_get=col_check_blue[3])
        field[0].MovableColoring(agent_data[0], (agent[0], agent[1]), out=col_check_blue[2])
        field[0].FillColoring(agent_data[0], out=col_check_blue[4])
        field[0].FillColoring(agent_data[1], out=col_check_red[4])
    if col_check_red[0] and blue_red == False:
        field[0].Coloring(agent_data[1], (agent[2], agent[3]), out_now=col_check_red[0], out_get=turn_end)
    elif turn_exit and blue_red == False:
        field[0].Coloring(agent_data[1], (agent[2], agent[3]), out_now=col_check_red[1], out_get=col_check_red[3])
    elif blue_red == False:
        field[0].Coloring(agent_data[1], (agent[2], agent[3]), out_now=col_check_red[1], out_get=col_check_red[3])
        field[0].MovableColoring(agent_data[1], (agent[2], agent[3]), out=col_check_red[2])
        field[0].FillColoring(agent_data[0], out=col_check_blue[4])
        field[0].FillColoring(agent_data[1], out=col_check_red[4])

def createbutton(text):
    global field_type
    field.append(procon29.FieldControl.Field(text, wx.Panel(panel,-1,pos=(100,150),size=(800,600)), log))
    agent_data.append(procon29.Agent.AgentData('blue', log, field[0].point))
    agent_data.append(procon29.Agent.AgentData('red', log, field[0].point))
    agent1_y, agent1_x = map(int,text[field[0].y+1].strip().split(' '))
    agent2_y, agent2_x = map(int,text[field[0].y+2].strip().split(' '))
    blue_flags.next[0], blue_flags.next[1]= (agent1_x)*100+(agent1_y), (agent2_x)*100+(agent2_y)
    field_type = field[0].FieldTypeAnalysis()
    if field_type == 0:
        red_flags.next[0], red_flags.next[1]= (field[0].x-agent1_x+1)*100+(field[0].y-agent2_y+1), (field[0].x-agent2_x+1)*100+(field[0].y-agent1_y+1)
        log.LogWrite('Field type: VERTICAL&HORIZONTAL\n', logtype=procon29.GAME_LOG)
    elif field_type == -1:
        red_flags.next[0], red_flags.next[1]= (agent1_x)*100+(field[0].y-agent1_y+1), (agent2_x)*100+(field[0].y-agent2_y+1)
        log.LogWrite('Field type: HORIZONTAL\n', logtype=procon29.GAME_LOG)
    elif field_type == 1:
        red_flags.next[0], red_flags.next[1]= (field[0].x-agent1_x+1)*100+(agent1_y), (field[0].x-agent2_x+1)*100+(agent2_y)
        log.LogWrite('Field type: VERTICAL\n', logtype=procon29.GAME_LOG)
    agent.append(procon29.Agent.Agent(blue_flags.next[0], field[0].field_out, log, 'blue'))
    agent.append(procon29.Agent.Agent(blue_flags.next[1], field[0].field_out, log, 'blue'))
    agent.append(procon29.Agent.Agent(red_flags.next[0], field[0].field_out, log, 'red'))
    agent.append(procon29.Agent.Agent(red_flags.next[1], field[0].field_out, log, 'red'))
    agent_data[0].GetPoint([agent[0].next, agent[1].next], agent_data[1])
    agent_data[1].GetPoint([agent[2].next, agent[3].next], agent_data[0])
    agent[0].TurnSet(agent_data[1].GetPosition)
    agent[1].TurnSet(agent_data[1].GetPosition)
    agent[2].TurnSet(agent_data[0].GetPosition)
    agent[3].TurnSet(agent_data[0].GetPosition)

    if modes.auto:
        modes.AutoSet(log, field[0], [agent[2], agent[3]], red_flags)
    bluepoint_text.SetLabel('青\n取得済み得点:{} \n領域得点:{}'\
                                .format(agent_data[0].Point, agent_data[0].TerritoryPoint))
    redpoint_text.SetLabel('赤\n取得済み得点:{} \n領域得点:{}'\
                                .format(agent_data[1].Point, agent_data[1].TerritoryPoint))

    col_func(True, False)
    col_func(False, True)

def turnendfunc():
    global endflag
    global nowturn
    blue_flags.Clear()
    red_flags.Clear()
    for i in range(2):
        for j in range(2):
            if blue_flags.next[i] == red_flags.next[j]:
                overlaperror = wx.MessageDialog(None, '行動対象の座標が重なりました。双方移動できません({},{})'\
                                            .format(int(blue_flags.next[i]/100), blue_flags.next[i]%100))
                overlaperror.ShowModal()
                overlaperror.Destroy()
                log.LogWrite('Overlap ({},{})'.format(int(blue_flags.next[i]/100), blue_flags.next[i]%100))
                agent[i].next[1] = 0
                agent[j+2].next[1] = 0
    field[0].MovableColoring(agent_data[1], (agent[2], agent[3]), clear=True)
    field[0].MovableColoring(agent_data[0], (agent[0], agent[1]), clear=True)
    agent_data[0].GetPoint([agent[0].next, agent[1].next], agent_data[1])
    agent_data[1].GetPoint([agent[2].next, agent[3].next], agent_data[0])
    agent_data[0].FieldPointSearch()
    agent_data[1].FieldPointSearch()
    agent[0].TurnSet(agent_data[1].GetPosition)
    agent[1].TurnSet(agent_data[1].GetPosition)
    agent[2].TurnSet(agent_data[0].GetPosition)
    agent[3].TurnSet(agent_data[0].GetPosition)
    bluepoint_text.SetLabel('青\n取得済み得点:{} \n領域得点:{}'\
                                .format(agent_data[0].Point, agent_data[0].TerritoryPoint))
    redpoint_text.SetLabel('赤\n取得済み得点:{} \n領域得点:{}'\
                                .format(agent_data[1].Point, agent_data[1].TerritoryPoint))
    #col_func(True, False)
    col_func(False, True)
    endflag = GameEndCheck()
    if endflag != True:
        col_func(True, False)
        nowturn += 1
    turnunm.SetLabel('現在ターン数:{}\n全ターン数:{}\n残りターン数:{}'.format(nowturn, turn, turn-nowturn))
    if endflag:
        gameenddialog = wx.MessageDialog(None, '全てのターンが終了しました')
        gameenddialog.ShowModal()
        gameenddialog.Destroy()
        log.LogWrite('End game')

def move(agent, agent_data, flags, eagent, Id_num):
    flags.turn += 1
    if Id_num in agent[0].movable and Id_num in agent[1].movable:
        checkdialog = SelectDialog(None, -1, text='移動可能領域が重なっています。移動する方を選択してください', title='移動可能領域重複')
        num = wxId[checkdialog.ShowModal()]
        agent_main = agent[num]
    elif Id_num in agent[0].movable:
        agent_main = agent[0]
        num = 0
    elif Id_num in agent[1].movable:
        agent_main = agent[1]
        num = 1
    del agent[:]
    if flags.flag[num] == False:
        field[0].NextColoring(agent_main.next[1], agent_data.NextColor, clear=True)
        agent_main.NextSet(Id_num)
        field[0].MovableColoring(agent_data, agent_main, out=True)
    flags.flag[num] = False
    if Id_num in agent_main.removable:
        dialog = wx.MessageDialog(None, '相手の取得済み地点です。対象地点を取り除きますか？', '取得地点排除確認', style=wx.YES_NO)
        result = dialog.ShowModal()
        if result == wx.ID_YES:
            agent_main.NextSet(Id_num, overlap=True)
            field[0].NextColoring(agent_main.next[1], agent_data.NextColor)
            flags.next[num] = Id_num
        else:
            flags.flag[num] = True
            flags.turn -= 1
        dialog.Destroy()
    else:
        agent_main.NextSet(Id_num)
        field[0].NextColoring(agent_main.next[1], agent_data.NextColor)
        flags.next[num] = Id_num

def start_func():
    global start_flag
    global load_file_flag
    if load_file_flag:
        start_flag = True
        log.LogWrite('Game Start\n')
        color_select.Disable()
        start.Disable()
    else:
        log.LogWrite('No open file\n', logtype=procon29.ERROR)
        errordialog = wx.MessageDialog(None, 'ファイルが開かれていません', 'ゲームスタートエラー',style=wx.ICON_EXCLAMATION)
        errordialog.ShowModal()
        errordialog.Destroy()

def Menu_handler(event):
    global load_file_flag
    global turn
    global debag_flag
    Id_num = event.GetId()
    if Id_num == 10:
        if len(field) != 0:
            FiledClear()
        log.LogWrite('Open file select dialog\n', logtype=procon29.SYSTEM_LOG)
        dialog = wx.FileDialog(None,'Select File','./')
        dialog.SetWildcard('*.png')
        dialog.ShowModal()
        image = dialog.GetPath()
        try:
            data = decode(Image.open(image))
        except AttributeError:
            return
        log.LogWrite('File open {}\n'.format(image), logtype=procon29.FILE_LOG)
        text = data[0][0].decode('utf-8', 'ignore')
        text = list(text.split(':'))
        createbutton(text)
        turndialog = wx.TextEntryDialog(None, 'ターン数を入力してください', 'ターン数決定')
        turndialog.SetValue('10')
        turndialog.ShowModal()
        turn = int(turndialog.GetValue())
        turnunm.SetLabel('現在ターン数:{}\n全ターン数:{}\n残りターン数:{}'.format(nowturn, turn, turn-nowturn))
        turndialog.Destroy()
        log.LogWrite('Complete init. Turn {}\n'.format(turn))
        load_file_flag = True
    elif Id_num == 11:
        start_func()
    elif Id_num == 12:
        FiledClear()
    elif Id_num == procon29.EXIT:
        log.LogWrite('Quit Program\n', logtype=procon29.SYSTEM_LOG)
        sys.exit(0)
    elif Id_num == 14:
        passdialog = wx.TextEntryDialog(None, 'パスワードを入力してください', 'デバッグユーザー認証')
        passdialog.SetValue('')
        passdialog.ShowModal()
        hashstr = hashlib.sha256(passdialog.GetValue().encode('utf-8')).hexdigest()
        if hashstr in passcode:
            debag_flag = True
            log.LogWrite('Success Debag user login\n', logtype=procon29.SYSTEM_LOG)
        else:
            errordialog = wx.MessageDialog(None, 'パスワードが違います', '認証失敗')
            errordialog.ShowModal()
            errordialog.Destroy()
            log.LogWrite('Error password input:[{}]\n'.format(passdialog.GetValue()), logtype=procon29.ERROR)
        passdialog.Destroy()
    elif Id_num == 20:
        log.LogWrite('Open about info\n', logtype=procon29.SYSTEM_LOG)
        info = adv.AboutDialogInfo()
        info.SetName('PPAP -Procon29 Python Application Project-')

        info.SetVersion('1.5.1')
        info.SetCopyright('Copyright (c) 2018 Glaz egy.')
        adv.AboutBox(info)
    
def Radio_handler(event):
    global now_mode
    global now_color
    Id_num = event.GetId()
    if Id_num == 100:
        mode = event.GetSelection()
        log.LogWrite('Chenge mode {}->{}\n'.format(now_mode+1, mode+1), logtype=procon29.SYSTEM_LOG)
        now_mode = mode
        if mode == 0:
            color_select.Disable()
            modes.Clear()
            modes.manual = True
            start.Disable()
        elif mode == 1:
            if modes.auto == False:
                modes.AutoSet(log, field[0], [agent[2], agent[3]], red_flags)
            color_select.Enable()
            modes.Clear()
            modes.auto = True
            start.Enable()
        elif mode == 2:
            color_select.Disable()
            modes.Clear()
            modes.battle = True
            start.Enable()
        elif mode == 3:
            color_select.Disable()
            modes.Clear()
            modes.learn = True
            start.Enable()
    elif Id_num == 101:
        color = event.GetSelection()
        log.LogWrite('Chenge color {}->{}\n'.format(now_color+1, color+1), logtype=procon29.SYSTEM_LOG)
        now_color = color
        if color == 0:
            pass
        elif color == 1:
            pass

def Button_handler(event):
    global load_file_flag
    global step_end
    global debag_flag
    Id_num = event.GetId()
    if debag_flag:
        move([agent[0], agent[1]], agent_data[0], blue_flags, (agent[2], agent[3]), Id_num)
        turnendfunc()
    elif modes.auto:
        if Id_num == 200:
            start_func()
        elif start_flag == False:
            log.LogWrite('Not start Game', logtype=procon29.ERROR)
            errordialog = wx.MessageDialog(None, 'ゲームがスタートされていません','ゲーム未スタート')
            errordialog.ShowModal()
            errordialog.Destroy()
        elif Id_num == 201:
            blue_flags.Clear()
            col_func(True, False)
            log.LogWrite('Cancel step\n')
        elif Id_num == 202:
            if blue_flags.flag == False_list and blue_flags.end == False:
                modes.bot[0].NextPositionSet(agent[2], agent_data[1], (agent[0].now, agent[1].now), 0)
                modes.bot[1].NextPositionSet(agent[3], agent_data[1], (agent[0].now, agent[1].now), 1)
                turnendfunc()
            else:
                log.LogWrite('No finish all step\n', logtype=procon29.ERROR)
                errordialog = wx.MessageDialog(None, '全ての工程が終了していません', '移動未終了',style=wx.ICON_EXCLAMATION)
                errordialog.ShowModal()
                errordialog.Destroy()
        else:
            if (Id_num in agent[0].movable or Id_num in agent[1].movable) and Id_num not in (agent[2].now, agent[3].now):
                move([agent[0], agent[1]], agent_data[0], blue_flags, (agent[2], agent[3]), Id_num)
            else:
                log.LogWrite('Can not move position ({},{})\n'.format(int(Id_num/100), Id_num%100), logtype=procon29.ERROR)
                errordialog = wx.MessageDialog(None, '移動できない地点です ({0},{1})'.format(int(Id_num/100), Id_num%100), '移動可能エリア外',style=wx.ICON_EXCLAMATION)
                errordialog.ShowModal()
                errordialog.Destroy()
    else:
        if Id_num == 201:
            if red_flags.flag[0] != True or red_flags.flag[1] != True:
                red_flags.Clear()
                col_func(False, False)
                log.LogWrite('Cancel step\n')
            elif blue_flags.flag[0] != True or blue_flags.flag[1] != True:
                blue_flags.Clear()
                field[0].MovableColoring(agent_data[1], (agent[2], agent[3]), clear=True)
                col_func(True, False)
                log.LogWrite('Cancel step\n')
        elif Id_num == 202:
            if load_file_flag:
                if red_flags.flag == False_list:
                    turnendfunc()
                elif blue_flags.flag == False_list and blue_flags.end == False:
                    log.LogWrite('Blue step finished\n')
                    field[0].MovableColoring(agent_data[0], (agent[0], agent[1]), clear=True)
                    col_func(False, False)
                    blue_flags.end = True
                else:
                    log.LogWrite('No finish all step\n', logtype=procon29.ERROR)
                    errordialog = wx.MessageDialog(None, '全ての工程が終了していません', '移動未終了',style=wx.ICON_EXCLAMATION)
                    errordialog.ShowModal()
                    errordialog.Destroy()
            else:
                log.LogWrite('No open file\n', logtype=procon29.ERROR)
                errordialog = wx.MessageDialog(None, 'ファイルが開かれていません', 'ゲームスタートエラー',style=wx.ICON_EXCLAMATION)
                errordialog.ShowModal()
                errordialog.Destroy()
        else:
            if (Id_num in agent[0].movable or Id_num in agent[1].movable) and Id_num not in (agent[2].now, agent[3].now) and not blue_flags.end:
                move([agent[0], agent[1]], agent_data[0], blue_flags, (agent[2], agent[3]), Id_num)
            elif (Id_num in agent[2].movable or Id_num in agent[3].movable) and Id_num not in (agent[0].now, agent[1].now) and blue_flags.end:
                move([agent[2], agent[3]], agent_data[1], red_flags, (agent[0], agent[1]), Id_num)
            else:
                log.LogWrite('Can not move position ({},{})\n'.format(int(Id_num/100), Id_num%100), logtype=procon29.ERROR)
                errordialog = wx.MessageDialog(None, '移動できない地点です ({0},{1})'.format(int(Id_num/100), Id_num%100), '移動可能エリア外',style=wx.ICON_EXCLAMATION)
                errordialog.ShowModal()
                errordialog.Destroy()
    
def check_handler(event):
    global col_check_blue
    global col_check_red
    if event.GetId() == 300:
        if coloring_select_blue.IsChecked(0):
            coloring_select_blue.SetCheckedItems((0,))
        for i in range(len(col_set)):
            col_check_blue[i] = coloring_select_blue.IsChecked(i)
        col_func(True, False)
    if event.GetId() == 301:
        if coloring_select_red.IsChecked(0):
            coloring_select_red.SetCheckedItems((0,))
        for i in range(len(col_set)):
            col_check_red[i] = coloring_select_red.IsChecked(i)
        col_func(False, (blue_flags.flag[0] and blue_flags.flag[1]))
            

text = ''
play_color = 'blue'
load_file_flag = False
step_end = False
blue_flags = Flags()
red_flags = Flags()
field_type = 0
endflag = False
turn = 10
nowturn = 1
debag_flag = False
passcode = ['e71d2f7f7ae998e3e9c4509c31e577a98371b234f36e466402998c8817860049']
col_check_blue = [True for i in range(5)]
col_check_blue[0] = False
col_check_red = [True for i in range(5)]
col_check_red[0] = False
False_list = [False, False]
start_flag = False

app = wx.App()
frame = wx.Frame(None, -1, 'PPAP -Procon29 Python Application Project-', size=(800,900), style=wx.SYSTEM_MENU | wx.CAPTION | wx.CLOSE_BOX | wx.CLIP_CHILDREN | wx.MINIMIZE_BOX)

panel = wx.Panel(frame,-1)
field = []
agent = []
agent_data = []

mode_type = ('手動入力モード','疑似対戦モード','実対戦モード','学習モード')
mode_select = wx.RadioBox(panel, 100, 'モードセレクト', choices=mode_type)
mode_select.SetSelection(0)
now_mode = mode_select.GetSelection()
modes = Modes()
modes.manual = True
team_color = ('青チーム','赤チーム')
color_select = wx.RadioBox(panel, 101, '操作色選択', choices=team_color)
color_select.SetSelection(0)
now_color = color_select.GetSelection()
color_select.Disable()
start = wx.Button(panel, 200, 'スタート', size=(80,50))
start.Disable()
cancel = wx.Button(panel, 201, 'キャンセル', size=(80,50))
turn_end = wx.Button(panel, 202, 'ターンエンド',size=(80,50))
layout = wx.BoxSizer(wx.HORIZONTAL)
layout.Add(mode_select, border=10)
layout.Add(color_select, border=10)
layout.Add(start)
layout.Add(cancel)
layout.Add(turn_end)
panel.SetSizer(layout)

bluepoint_text = wx.StaticText(panel, -1, '青\n取得済み得点:0 \n領域得点:0', style=wx.SIMPLE_BORDER, pos=(0,51))
redpoint_text = wx.StaticText(panel, -1, '赤\n取得済み得点:0 \n領域得点:0', style=wx.SIMPLE_BORDER, pos=(0,99))
turnunm = wx.StaticText(panel, -1, '現在ターン数:1\n全ターン数:0\n残りターン数:0', style=wx.SIMPLE_BORDER, pos=(680,51), size=(100,50))

log = procon29.SystemControl.LogControl('game.log')

col_set = ('現在地のみ','現在地','移動可能','移動済み','獲得領域')
wx.StaticText(panel, -1, '青表示色選択', pos=(0,150))
coloring_select_blue = wx.CheckListBox(panel, 300, choices=col_set, pos=(0,170), size=(100,100))
coloring_select_blue.SetCheckedItems((1,2,3,4))
wx.StaticText(panel, -1, '赤表示色選択', pos=(0,275))
coloring_select_red = wx.CheckListBox(panel, 301, choices=col_set, pos=(0,295), size=(100,100))
coloring_select_red.SetCheckedItems((1,2,3,4))

#Setting menubar
menu_bar = wx.MenuBar()
file_menu = wx.Menu()
open_file = wx.MenuItem(file_menu, 10, 'ファイルを開く')
start_game = wx.MenuItem(file_menu, 11, 'ゲームスタート')
clear_disp = wx.MenuItem(file_menu, 12, '画面クリア')
exit_app = wx.MenuItem(file_menu, procon29.EXIT, '終了')
debag_mode = wx.MenuItem(file_menu, 14, 'デバッグモード')
file_menu.Append(open_file)
file_menu.Append(start_game)
file_menu.Append(clear_disp)
file_menu.Append(exit_app)
file_menu.Append(debag_mode)
menu_bar.Append(file_menu, 'ファイル')
help_menu = wx.Menu()
about = wx.MenuItem(help_menu, 20, 'バージョン情報')
help_menu.Append(about)
menu_bar.Append(help_menu, 'ヘルプ')
frame.SetMenuBar(menu_bar)

frame.Bind(wx.EVT_MENU, Menu_handler)
frame.Bind(wx.EVT_BUTTON, Button_handler)
frame.Bind(wx.EVT_HOTKEY, Button_handler)
frame.Bind(wx.EVT_RADIOBOX, Radio_handler)
frame.Bind(wx.EVT_CHECKLISTBOX, check_handler)

frame.Show()
app.MainLoop()