# -*- coding: utf-8 -*-

from pyzbar.pyzbar import decode
from PIL import Image
import procon29
import os
import wx
from wx import adv
import sys

def clear_flag():
    global blue_turn
    global red_turn
    global blue_flag
    global red_flag
    global change_color
    blue_flag = [True, True]
    red_flag = [True, True]
    blue_turn = 0
    red_turn = 0
    change_color = True

def createbutton(text):
    field.append(procon29.FieldControl.Field(text, wx.Panel(panel,-1,pos=(100,150),size=(800,600))))
    agent1_y, agent1_x = map(int,text[field[0].y+1].strip().split(' '))
    agent2_y, agent2_x = map(int,text[field[0].y+2].strip().split(' '))
    next_blue[0] = (agent1_x)*1000+(agent1_y)
    next_blue[1] = (agent2_x)*1000+(agent2_y)
    blue_agent_data.GetPoint([next_blue[0], next_blue[1]], field[0].point)
    next_red[0] = (agent1_x)*1000+(agent2_y)
    next_red[1] = (agent2_x)*1000+(agent1_y)
    red_agent_data.GetPoint([next_red[0], next_red[1]], field[0].point)
    agent.append(procon29.Agent.Agent(next_blue[0], field[0].field_out, red_agent_data.GetPosition, log, 'blue'))
    agent.append(procon29.Agent.Agent(next_blue[1], field[0].field_out, red_agent_data.GetPosition, log, 'blue'))
    agent.append(procon29.Agent.Agent(next_red[0], field[0].field_out, blue_agent_data.GetPosition, log, 'red'))
    agent.append(procon29.Agent.Agent(next_red[1], field[0].field_out, blue_agent_data.GetPosition, log, 'red'))
    field[0].Coloring(blue_agent_data, (agent[0], agent[1]))
    field[0].MovableColoring(blue_agent_data, (agent[0], agent[1]))
    field[0].Coloring(red_agent_data, (agent[2], agent[3]))

def handler(event):
    global load_file_flag
    global blue_turn
    global red_turn
    global blue_flag
    global red_flag
    global now_mode
    global now_color
    global change_color
    Id_num = event.GetId()
    if Id_num == 10:
        log.LogWrite('Open file select dialog\n', logtype=procon29.SYSTEM_LOG)
        dialog = wx.FileDialog(None,'Select File','./')
        dialog.ShowModal()
        image = dialog.GetPath()
        data = decode(Image.open(image))
        log.LogWrite('File open {}\n'.format(image), logtype=procon29.FILE_LOG)
        text = data[0][0].decode('utf-8', 'ignore')
        text = list(text.split(':'))
        createbutton(text)
        log.LogWrite('Complete init\n')
        load_file_flag = True
    elif Id_num == 11:
        if load_file_flag:
            log.LogWrite('Game start!\n')
        else:
            log.LogWrite('No open file\n', logtype=procon29.ERROR)
            errordialog = wx.MessageDialog(None, 'ファイルが開かれていません', 'ゲームスタートエラー',style=wx.ICON_EXCLAMATION)
            errordialog.ShowModal()
    elif Id_num == 12:
        log.LogWrite('Quit Program\n', logtype=procon29.SYSTEM_LOG)
        sys.exit(0)
    elif Id_num == 20:
        log.LogWrite('Open about info\n', logtype=procon29.SYSTEM_LOG)
        info = adv.AboutDialogInfo()
        info.SetName('高専プロコンGUI')
        info.SetVersion('0.3.0')
        info.SetCopyright('Copyright (c) 2018 Glaz egy.')
        adv.AboutBox(info)
    elif Id_num == 100:
        mode = event.GetSelection()
        log.LogWrite('Chenge mode {}->{}\n'.format(now_mode+1, mode+1), logtype=procon29.SYSTEM_LOG)
        now_mode = mode
        if mode == 0:
            frame.SetStatusText('mode:1',1)
            color_select.Disable()
        elif mode == 1:
            frame.SetStatusText('mode:2',1)
            color_select.Enable()
        elif mode == 2:
            frame.SetStatusText('mode:3',1)
            color_select.Disable()
    elif Id_num == 101:
        color = event.GetSelection()
        log.LogWrite('Chenge color {}->{}\n'.format(now_color+1, color+1), logtype=procon29.SYSTEM_LOG)
        now_color = color
        if color == 0:
            pass
        elif color == 1:
            pass
    elif Id_num == 200:
        if load_file_flag:
            if blue_turn == 2 and red_turn == 2:
                clear_flag()
                field[0].MovableColoring(red_agent_data, (agent[2], agent[3]), clear=True)
                blue_agent_data.GetPoint([agent[0].next, agent[1].next], field[0].point)
                red_agent_data.GetPoint([agent[2].next, agent[3].next], field[0].point)
                agent[0].TurnEnd(red_agent_data.GetPosition)
                agent[1].TurnEnd(red_agent_data.GetPosition)
                agent[2].TurnEnd(blue_agent_data.GetPosition)
                agent[3].TurnEnd(blue_agent_data.GetPosition)
                field[0].Coloring(blue_agent_data, (agent[0], agent[1]))
                field[0].Coloring(red_agent_data, (agent[2], agent[3]))
                field[0].MovableColoring(blue_agent_data, (agent[0], agent[1]))
            else:
                log.LogWrite('No finish all step\n', logtype=procon29.ERROR)
                errordialog = wx.MessageDialog(None, '全ての工程が終了していません', '移動未終了',style=wx.ICON_EXCLAMATION)
                errordialog.ShowModal()
        else:
            log.LogWrite('No open file\n', logtype=procon29.ERROR)
            errordialog = wx.MessageDialog(None, 'ファイルが開かれていません', 'ゲームスタートエラー',style=wx.ICON_EXCLAMATION)
            errordialog.ShowModal()
    else:
        y, x = int(Id_num%1000), int(Id_num/1000)
        overlapdialog = wx.MessageDialog(None, '相手の取得済み領域です。領域を削除しますか？', '領域重複', style=wx.YES_NO)
        if Id_num in agent[0].movable and blue_flag[0]:
            blue_flag[0] = False
            blue_turn += 1
            if Id_num in agent[0].removable:
                result = overlapdialog.ShowModal()
                if result == wx.ID_YES:
                    pass
            else:
                agent[0].NextSet(Id_num)
                field[0].NextColoring(agent[0].next, blue_agent_data.NextColor)
        elif Id_num in agent[1].movable and blue_flag[1]:
            blue_flag[1] = False
            blue_turn += 1
            if Id_num in agent[1].removable:
                overlapdialog.ShowModal()
            else:
                agent[1].NextSet(Id_num)
                field[0].NextColoring(agent[1].next, blue_agent_data.NextColor)
        elif Id_num in agent[2].movable and red_flag[0]:
            red_flag[0] = False
            red_turn += 1
            if Id_num in agent[2].removable:
                overlapdialog.ShowModal()
            else:
                agent[2].NextSet(Id_num)
                field[0].NextColoring(agent[2].next, red_agent_data.NextColor)
        elif Id_num in agent[3].movable and red_flag[1]:
            red_flag[1] = False
            red_turn += 1
            if Id_num in agent[3].removable:
                overlapdialog.ShowModal()
            else:
                agent[3].NextSet(Id_num)
                field[0].NextColoring(agent[3].next, red_agent_data.NextColor)
        elif blue_turn == 2 and red_turn == 2:
            log.LogWrite('Finished all step\n', logtype=procon29.ERROR)
            errordialog = wx.MessageDialog(None, '全ての工程は終了しています', '移動終了済み',style=wx.ICON_EXCLAMATION)
            errordialog.ShowModal()
        else:
            log.LogWrite('Can not move position ({},{})\n'.format(int(Id_num/1000), Id_num%1000), logtype=procon29.ERROR)
            errordialog = wx.MessageDialog(None, '移動できない場所です ({0},{1})'.format(int(Id_num/1000), Id_num%1000), '移動可能エリア外',style=wx.ICON_EXCLAMATION)
            errordialog.ShowModal()
        if blue_turn == 2 and change_color:
            change_color = False
            log.LogWrite('Blue step finished\n')
            field[0].MovableColoring(blue_agent_data, (agent[0], agent[1]), clear=True)
            field[0].Coloring(red_agent_data, (agent[2], agent[3]))
            field[0].MovableColoring(red_agent_data, (agent[2], agent[3]))

text = ''
next_blue = [0, 0]
next_red = [0, 0]
next_all = [0, 0, 0, 0]
app = wx.App()
log_name = 'game.log'
play_color = 'blue'
load_file_flag = False
blue_turn = 0
red_turn = 0
change_color = True
blue_flag = [True, True]
red_flag = [True, True]

frame = wx.Frame(None, -1, 'Test', size=(1200,800), style=wx.SYSTEM_MENU | wx.CAPTION | wx.CLOSE_BOX | wx.CLIP_CHILDREN | wx.MINIMIZE_BOX)
frame.CreateStatusBar(3)
frame.SetStatusText('Status Bar here.',2)

panel = wx.Panel(frame,-1)
field = []
agent = []

mode_type = ('手動入力モード','疑似対戦モード','学習モード')
mode_select = wx.RadioBox(panel, 100, 'モードセレクト', choices=mode_type)
mode_select.SetSelection(0)
now_mode = mode_select.GetSelection()
team_color = ('青チーム','赤チーム')
color_select = wx.RadioBox(panel, 101, '操作色選択', choices=team_color)
color_select.SetSelection(0)
now_color = color_select.GetSelection()
color_select.Disable()
turn_end = wx.Button(panel, 200, 'ターンエンド',size=(100,50))
layout = wx.BoxSizer(wx.HORIZONTAL)
layout.Add(mode_select, border=10)
layout.Add(color_select, border=10)
layout.Add(turn_end, border=10)
panel.SetSizer(layout)

log_text = wx.TextCtrl(panel, -1, pos=(950,10), size=(230,700), style=wx.TE_MULTILINE)
log_text.SetBackgroundColour('white')
log = procon29.SystemControl.LogControl(log_name, log_text)

blue_agent_data = procon29.Agent.AgentData('blue', log)
red_agent_data = procon29.Agent.AgentData('red', log)

menu_bar = wx.MenuBar()
file_menu = wx.Menu()
open_file = wx.MenuItem(file_menu, 10, 'ファイルを開く')
start_game = wx.MenuItem(file_menu, 11, 'ゲームスタート')
exit_app = wx.MenuItem(file_menu, 12, '終了')
file_menu.Append(open_file)
file_menu.Append(start_game)
file_menu.Append(exit_app)
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