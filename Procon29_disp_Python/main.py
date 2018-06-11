# -*- coding: utf-8 -*-

from pyzbar.pyzbar import decode
from PIL import Image
from wx import adv
import procon29
import os
import wx
import hashlib
import sys

def clear_flag():
    global blue_turn
    global red_turn
    global blue_flag
    global red_flag
    blue_flag = [True, True]
    red_flag = [True, True]
    blue_turn = 0
    red_turn = 0

def GameEndCheck():
    global nowturn
    global turn
    if turn == nowturn:
        return True
    

def createbutton(text):
    global field_type
    field.append(procon29.FieldControl.Field(text, wx.Panel(panel,-1,pos=(100,150),size=(800,600)), log))
    agent_data.append(procon29.Agent.AgentData('blue', log, field[0].point))
    agent_data.append(procon29.Agent.AgentData('red', log, field[0].point))
    agent1_y, agent1_x = map(int,text[field[0].y+1].strip().split(' '))
    agent2_y, agent2_x = map(int,text[field[0].y+2].strip().split(' '))
    next_blue[0], next_blue[1]= (agent1_x)*1000+(agent1_y), (agent2_x)*1000+(agent2_y)
    field_type = field[0].FieldTypeAnalysis()
    if field_type == 0:
        next_red[0], next_red[1]= (field[0].x-agent1_x+1)*1000+(field[0].y-agent2_y+1), (field[0].x-agent2_x+1)*1000+(field[0].y-agent1_y+1)
        log.LogWrite('Field type: VERTICAL&HORIZONTAL\n', logtype=procon29.GAME_LOG)
    elif field_type == -1:
        next_red[0], next_red[1]= (agent1_x)*1000+(field[0].y-agent1_y+1), (agent2_x)*1000+(field[0].y-agent2_y+1)
        log.LogWrite('Field type: HORIZONTAL\n', logtype=procon29.GAME_LOG)
    elif field_type == 1:
        next_red[0], next_red[1]= (field[0].x-agent1_x+1)*1000+(agent1_y), (field[0].x-agent2_x+1)*1000+(agent2_y)
        log.LogWrite('Field type: VERTICAL\n', logtype=procon29.GAME_LOG)
    agent.append(procon29.Agent.Agent(next_blue[0], field[0].field_out, log, 'blue'))
    agent.append(procon29.Agent.Agent(next_blue[1], field[0].field_out, log, 'blue'))
    agent.append(procon29.Agent.Agent(next_red[0], field[0].field_out, log, 'red'))
    agent.append(procon29.Agent.Agent(next_red[1], field[0].field_out, log, 'red'))
    agent_data[0].GetPoint([agent[0].next, agent[1].next], agent_data[1])
    agent_data[1].GetPoint([agent[2].next, agent[3].next], agent_data[0])
    agent[0].TurnSet(agent_data[1].GetPosition)
    agent[1].TurnSet(agent_data[1].GetPosition)
    agent[2].TurnSet(agent_data[0].GetPosition)
    agent[3].TurnSet(agent_data[0].GetPosition)
    bluepoint_text.SetLabel('青\n取得済み得点:{} \n領域得点:{}'\
                                .format(agent_data[0].Point, agent_data[0].TerritoryPoint))
    redpoint_text.SetLabel('赤\n取得済み得点:{} \n領域得点:{}'\
                                .format(agent_data[1].Point, agent_data[1].TerritoryPoint))
    field[0].Coloring(agent_data[0], (agent[0], agent[1]))
    field[0].MovableColoring(agent_data[0], (agent[0], agent[1]))
    field[0].Coloring(agent_data[1], (agent[2], agent[3]))

def trunendfunc():
    global endflag
    global nowturn
    clear_flag()
    for i in range(2):
        for j in range(2):
            if next_blue[i] == next_red[j]:
                overlaperror = wx.MessageDialog(None, '行動対象の座標が重なりました。双方移動できません({},{})'\
                                            .format(int(next_blue[i]/1000),next_blue[i]%1000))
                overlaperror.ShowModal()
                overlaperror.Destroy()
                log.LogWrite('Overlap ({},{})'.format(int(next_blue[i]/1000),next_blue[i]%1000))
                agent[i].next[1] = 0
                agent[j+2].next[1] = 0
    field[0].MovableColoring(agent_data[1], (agent[2], agent[3]), clear=True)
    agent_data[0].GetPoint([agent[0].next, agent[1].next], agent_data[1])
    agent_data[0].FieldPointSearch()
    agent_data[1].GetPoint([agent[2].next, agent[3].next], agent_data[0])
    agent_data[1].FieldPointSearch()
    agent[0].TurnSet(agent_data[1].GetPosition)
    agent[1].TurnSet(agent_data[1].GetPosition)
    agent[2].TurnSet(agent_data[0].GetPosition)
    agent[3].TurnSet(agent_data[0].GetPosition)
    bluepoint_text.SetLabel('青\n取得済み得点:{} \n領域得点:{}'\
                                .format(agent_data[0].Point, agent_data[0].TerritoryPoint))
    redpoint_text.SetLabel('赤\n取得済み得点:{} \n領域得点:{}'\
                                .format(agent_data[1].Point, agent_data[1].TerritoryPoint))
    field[0].Coloring(agent_data[0], (agent[0], agent[1]))
    field[0].Coloring(agent_data[1], (agent[2], agent[3]))
    endflag = GameEndCheck()
    if endflag != True:
        field[0].MovableColoring(agent_data[0], (agent[0], agent[1]))
        nowturn += 1
    turnunm.SetLabel('現在ターン数:{}\n全ターン数:{}\n残りターン数:{}'.format(nowturn, turn, turn-nowturn))
    if endflag:
        gameenddialog = wx.MessageDialog(None, '全てのターンが終了しました')
        gameenddialog.ShowModal()
        gameenddialog.Destroy()
        log.LogWrite('End game')

def handler(event):
    global load_file_flag
    global blue_turn
    global red_turn
    global blue_flag
    global red_flag
    global now_mode
    global now_color
    global step_end
    global turn
    global debag_flag
    Id_num = event.GetId()
    if Id_num == 10:
        log.LogWrite('Open file select dialog\n', logtype=procon29.SYSTEM_LOG)
        dialog = wx.FileDialog(None,'Select File','./')
        dialog.SetWildcard('*.png')
        dialog.ShowModal()
        image = dialog.GetPath()
        data = decode(Image.open(image))
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
        if load_file_flag:
            log.LogWrite('Game start!\n')
        else:
            log.LogWrite('No open file\n', logtype=procon29.ERROR)
            errordialog = wx.MessageDialog(None, 'ファイルが開かれていません', 'ゲームスタートエラー',style=wx.ICON_EXCLAMATION)
            errordialog.ShowModal()
            errordialog.Destroy()
    elif Id_num == 12:
        field[0].Destroy()
        del field[0]
        del agent[:]
        del agent_data[:]
        clear_flag()
        bluepoint_text = wx.StaticText(panel, -1, '青\n取得済み得点:0 \n領域得点:0', style=wx.SIMPLE_BORDER, pos=(0,51))
        redpoint_text = wx.StaticText(panel, -1, '赤\n取得済み得点:0 \n領域得点:0', style=wx.SIMPLE_BORDER, pos=(0,99))
        log.LogWrite('Clear All\n')
    elif Id_num == 13:
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
        info.SetVersion('1.0.1')
        info.SetCopyright('Copyright (c) 2018 Glaz egy.')
        adv.AboutBox(info)
    elif Id_num == 100:
        mode = event.GetSelection()
        log.LogWrite('Chenge mode {}->{}\n'.format(now_mode+1, mode+1), logtype=procon29.SYSTEM_LOG)
        now_mode = mode
        if mode == 0:
            color_select.Disable()
            start.Disable()
        elif mode == 1:
            color_select.Enable()
            start.Enable()
        elif mode == 2:
            color_select.Disable()
            start.Enable()
        elif mode == 3:
            color_select.Disable()
            start.Enable()
    elif Id_num == 101:
        color = event.GetSelection()
        log.LogWrite('Chenge color {}->{}\n'.format(now_color+1, color+1), logtype=procon29.SYSTEM_LOG)
        now_color = color
        if color == 0:
            pass
        elif color == 1:
            pass
    elif Id_num == 200:
        pass
    elif Id_num == 201:
        if red_flag[0] != True or red_flag[1] != True:
            red_turn = 0
            red_flag = [True, True]
            field[0].MovableColoring(agent_data[1], (agent[2], agent[3]))
            log.LogWrite('Cancel step\n')
        elif blue_flag[0] != True or blue_flag[1] != True:
            blue_turn = 0
            step_end = False
            blue_flag = [True, True]
            field[0].MovableColoring(agent_data[1], (agent[2], agent[3]), clear=True)
            field[0].Coloring(agent_data[0], (agent[0], agent[1]))
            field[0].MovableColoring(agent_data[0], (agent[0], agent[1]))
            log.LogWrite('Cancel step\n')
    elif Id_num == 202:
        if load_file_flag:
            if red_turn == 2:
                trunendfunc()
                step_end = False
            elif blue_turn == 2:
                log.LogWrite('Blue step finished\n')
                field[0].MovableColoring(agent_data[0], (agent[0], agent[1]), clear=True)
                field[0].Coloring(agent_data[1], (agent[2], agent[3]))
                field[0].MovableColoring(agent_data[1], (agent[2], agent[3]))
                step_end = True
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
        overlapdialog = wx.MessageDialog(None, '相手の取得済み地点です。対象地点を取り除きますか？', '取得視点排除確認', style=wx.YES_NO)
        if Id_num in agent[0].movable and blue_flag[0] and Id_num not in [agent[2].now, agent[3].now]:
            blue_flag[0] = False
            blue_turn += 1
            if Id_num in agent[0].removable:
                result = overlapdialog.ShowModal()
                if result == wx.ID_YES:
                    agent[0].NextSet(Id_num, overlap=True)
                    field[0].NextColoring(agent[0].next[1], agent_data[0].NextColor)
                    next_blue[0] = Id_num
                else:
                    blue_flag[0] = True
                    blue_turn -= 1
            else:
                agent[0].NextSet(Id_num)
                field[0].NextColoring(agent[0].next[1], agent_data[0].NextColor)
                next_blue[0] = Id_num
        elif Id_num in agent[1].movable and blue_flag[1] and Id_num not in [agent[2].now, agent[3].now]:
            blue_flag[1] = False
            blue_turn += 1
            if Id_num in agent[1].removable:
                result = overlapdialog.ShowModal()
                if result == wx.ID_YES:
                    agent[1].NextSet(Id_num, overlap=True)
                    field[0].NextColoring(agent[1].next[1], agent_data[0].NextColor)
                    next_blue[1] = Id_num
                else:
                    blue_flag[1] = True
                    blue_turn -= 1
            else:
                agent[1].NextSet(Id_num)
                field[0].NextColoring(agent[1].next[1], agent_data[0].NextColor)
                next_blue[1] = Id_num
        elif Id_num in agent[2].movable and red_flag[0] and Id_num not in [agent[0].now, agent[1].now]:
            red_flag[0] = False
            red_turn += 1
            if Id_num in agent[2].removable:
                result = overlapdialog.ShowModal()
                if result == wx.ID_YES:
                    agent[2].NextSet(Id_num, overlap=True)
                    field[0].NextColoring(agent[2].next[1], agent_data[1].NextColor)
                    next_red[0] = Id_num
                else:
                    blue_flag[0] = True
                    blue_turn -= 1
            else:
                agent[2].NextSet(Id_num)
                field[0].NextColoring(agent[2].next[1], agent_data[1].NextColor)
                next_red[0] = Id_num
        elif Id_num in agent[3].movable and red_flag[1] and Id_num not in [agent[0].now, agent[1].now]:
            red_flag[1] = False
            red_turn += 1
            if Id_num in agent[3].removable:
                result = overlapdialog.ShowModal()
                if result == wx.ID_YES:
                    agent[3].NextSet(Id_num, overlap=True)
                    field[0].NextColoring(agent[3].next[1], agent_data[1].NextColor)
                    next_red[1] = Id_num
                else:
                    blue_flag[1] = True
                    blue_turn -= 1
            else:
                agent[3].NextSet(Id_num)
                field[0].NextColoring(agent[3].next[1], agent_data[1].NextColor)
                next_red[1] = Id_num
        elif blue_turn == 2 and step_end != True:
            log.LogWrite('Finished all step\n', logtype=procon29.ERROR)
            errordialog = wx.MessageDialog(None, '全ての工程は終了しています', '移動終了済み',style=wx.ICON_EXCLAMATION)
            errordialog.ShowModal()
            errordialog.Destroy()
        else:
            log.LogWrite('Can not move position ({},{})\n'.format(int(Id_num/1000), Id_num%1000), logtype=procon29.ERROR)
            errordialog = wx.MessageDialog(None, '移動できない地点です ({0},{1})'.format(int(Id_num/1000), Id_num%1000), '移動可能エリア外',style=wx.ICON_EXCLAMATION)
            errordialog.ShowModal()
            errordialog.Destroy()
        overlapdialog.Destroy()

text = ''
next_blue = [0, 0]
next_red = [0, 0]
play_color = 'blue'
load_file_flag = False
blue_turn = 0
red_turn = 0
step_end = False
blue_flag = [True, True]
red_flag = [True, True]
field_type = 0
endflag = False
turn = 10
nowturn = 1
debag_flag = False
passcode = ['e71d2f7f7ae998e3e9c4509c31e577a98371b234f36e466402998c8817860049']

app = wx.App()

frame = wx.Frame(None, -1, 'PPAP -Procon29 Python Application Project-', size=(800,900), style=wx.SYSTEM_MENU | wx.CAPTION | wx.CLOSE_BOX | wx.CLIP_CHILDREN | wx.MINIMIZE_BOX)

panel = wx.Panel(frame,-1)
field = []
agent = []

mode_type = ('手動入力モード','疑似対戦モード','実対戦モード','学習モード')
mode_select = wx.RadioBox(panel, 100, 'モードセレクト', choices=mode_type)
mode_select.SetSelection(0)
now_mode = mode_select.GetSelection()
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

agent_data = []

#Setting menubar
menu_bar = wx.MenuBar()
file_menu = wx.Menu()
open_file = wx.MenuItem(file_menu, 10, 'ファイルを開く')
start_game = wx.MenuItem(file_menu, 11, 'ゲームスタート')
clear_disp = wx.MenuItem(file_menu, 12, '画面クリア')
exit_app = wx.MenuItem(file_menu, 13, '終了')
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

frame.Bind(wx.EVT_MENU, handler)
frame.Bind(wx.EVT_BUTTON, handler)
frame.Bind(wx.EVT_RADIOBOX, handler)

frame.Show()
app.MainLoop()