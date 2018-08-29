# -*- coding: utf-8 -*-

import wx
import os
import sys
from wx import adv


def FiledClear():
    global nowturn
    field[0].Destroy()
    del field[0]
    del agent[:]
    del agent_data[:]
    Modes.Clear()
    blue_Flags.Clear()
    red_Flags.Clear()
    nowturn = 1
    bluepoint_text = wx.StaticText(panel, -1, '青\n取得済み得点:0 \n領域得点:0', style=wx.SIMPLE_BORDER, pos=(0,51))
    redpoint_text = wx.StaticText(panel, -1, '赤\n取得済み得点:0 \n領域得点:0', style=wx.SIMPLE_BORDER, pos=(0,99))
    log.LogWrite('Clear All\n')

def col_func(blue_red, turn_exit):
    mynum, enum = (0, 1) if blue_red else (1, 0)
    if col_check[mynum][0]:
        field[0].Coloring(agent_data[mynum], (agent[mynum*2], agent[mynum*2+1]), out_now=col_check[mynum][0])
    elif turn_exit:
        field[0].Coloring(agent_data[mynum], (agent[mynum*2], agent[mynum*2+1]), out_now=col_check[mynum][1], out_get=col_check[mynum][3])
    else:
        pool.submit(field[0].MovableColoring, agent_data[mynum], (agent[mynum*2], agent[mynum*2+1]), out=col_check[mynum][2])
        pool.submit(field[0].FillColoring, agent_data[mynum], out=col_check[mynum][4])
        pool.submit(field[0].FillColoring, agent_data[enum], out=col_check[enum][4])
        field[0].Coloring(agent_data[mynum], (agent[mynum*2], agent[mynum*2+1]), out_now=col_check[mynum][1], out_get=col_check[mynum][3])

def createbutton(text):
    global field_type
    field.append(procon29.FieldControl.Field(text, wx.Panel(panel, -1, pos=(100,150),size=(800,600)), log))
    agent_data.append(procon29.Agent.AgentData('blue', log, field[0].point))
    agent_data.append(procon29.Agent.AgentData('red', log, field[0].point))
    agent1_y, agent1_x = map(int,text[field[0].y+1].strip().split(' '))
    agent2_y, agent2_x = map(int,text[field[0].y+2].strip().split(' '))
    blue_Flags.next[0], blue_Flags.next[1]= (agent1_x)*1000+(agent1_y), (agent2_x)*1000+(agent2_y)
    field_type = field[0].FieldTypeAnalysis()
    if field_type == 0:
        red_Flags.next[0], red_Flags.next[1] = (field[0].x-agent1_x+1)*1000+(field[0].y-agent1_y+1), (field[0].x-agent2_x+1)*1000+(field[0].y-agent2_y+1)
        if blue_Flags.next[0] in red_Flags.next or blue_Flags.next[1] in red_Flags.next:
            red_Flags.next[0], red_Flags.next[1] = (agent1_x)*1000+(field[0].y-agent1_y+1), (agent2_x)*1000+(field[0].y-agent2_y+1)
        if blue_Flags.next[0] in red_Flags.next or blue_Flags.next[1] in red_Flags.next:
            red_Flags.next[0], red_Flags.next[1] = (field[0].x-agent1_x+1)*1000+(agent1_y), (field[0].x-agent2_x+1)*1000+(agent2_y)
        log.LogWrite('Field type: VERTICAL&HORIZONTAL\n', logtype=procon29.GAME_LOG)
    elif field_type == -1:
        red_Flags.next[0], red_Flags.next[1] = (agent1_x)*1000+(field[0].y-agent1_y+1), (agent2_x)*1000+(field[0].y-agent2_y+1)
        log.LogWrite('Field type: HORIZONTAL\n', logtype=procon29.GAME_LOG)
    elif field_type == 1:
        red_Flags.next[0], red_Flags.next[1] = (field[0].x-agent1_x+1)*1000+(agent1_y), (field[0].x-agent2_x+1)*1000+(agent2_y)
        log.LogWrite('Field type: VERTICAL\n', logtype=procon29.GAME_LOG)
    agent.append(procon29.Agent.Agent(blue_Flags.next[0], field[0].field_out, log, 'blue'))
    agent.append(procon29.Agent.Agent(blue_Flags.next[1], field[0].field_out, log, 'blue'))
    agent.append(procon29.Agent.Agent(red_Flags.next[0], field[0].field_out, log, 'red'))
    agent.append(procon29.Agent.Agent(red_Flags.next[1], field[0].field_out, log, 'red'))
    agent_data[0].GetPoint([agent[0].next, agent[1].next], agent_data[1])
    agent_data[1].GetPoint([agent[2].next, agent[3].next], agent_data[0])
    agent[0].TurnSet(agent_data[1].GetPosition)
    agent[1].TurnSet(agent_data[1].GetPosition)
    agent[2].TurnSet(agent_data[0].GetPosition)
    agent[3].TurnSet(agent_data[0].GetPosition)
    if Modes.auto:
        Modes.AutoSet(log, field[0], [agent[2], agent[3]], red_Flags)
    bluepoint_text.SetLabel('青\n取得済み得点:{} \n領域得点:{}'\
                                .format(agent_data[0].Point, agent_data[0].TerritoryPoint))
    redpoint_text.SetLabel('赤\n取得済み得点:{} \n領域得点:{}'\
                                .format(agent_data[1].Point, agent_data[1].TerritoryPoint))
    col_func(True, False)
    col_func(False, True)

def OverlapFunc(i, j):
    if blue_Flags.next[i] == red_Flags.next[j]:
        if Modes.learn == False:
            overlaperror = wx.MessageDialog(None, '行動対象の座標が重なりました。双方移動できません({},{})'\
                                        .format(int(blue_Flags.next[i]/1000), blue_Flags.next[i]%1000))
            overlaperror.ShowModal()
            overlaperror.Destroy()
        log.LogWrite('Overlap ({},{})\n'.format(int(blue_Flags.next[i]/1000), blue_Flags.next[i]%1000))
        agent[i].next[1] = 0
        agent[j+2].next[1] = 0

def turnendfunc():
    global endflag
    global nowturn
    blue_Flags.Clear()
    red_Flags.Clear()
    for i in range(2):
        for j in range(2):
            OverlapFunc(i, j)
    pool.submit(field[0].MovableColoring, agent_data[1], (agent[2], agent[3]), clear=True)
    pool.submit(field[0].MovableColoring, agent_data[0], (agent[0], agent[1]), clear=True)
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
    col_func(False, True)
    endflag = True if turn == nowturn else False
    if endflag != True:
        col_func(True, False)
        nowturn += 1
        gauge.SetValue(nowturn-1)
    turnunm.SetLabel('現在ターン数:{}\n全ターン数:{}\n残りターン数:{}'.format(nowturn, turn, turn-nowturn))
    if endflag:
        gameenddialog = wx.MessageDialog(None, '全てのターンが終了しました')
        gameenddialog.ShowModal()
        gameenddialog.Destroy()
        log.LogWrite('End game')


def move(agent, agent_data, Flags, eagent, Id_num):
    Flags.turn += 1
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
    if Flags.flag[num] == False:
        field[0].NextColoring(agent_main.next[1], agent_data.NextColor, clear=True)
        agent_main.NextSet(Id_num)
        field[0].MovableColoring(agent_data, agent_main, out=True)
    Flags.flag[num] = False
    if Id_num in agent_main.removable:
        dialog = wx.MessageDialog(None, '相手の取得済み地点です。対象地点を取り除きますか？', '取得地点排除確認', style=wx.YES_NO)
        result = dialog.ShowModal()
        if result == wx.ID_YES:
            agent_main.NextSet(Id_num, overlap=True)
            field[0].NextColoring(agent_main.next[1], agent_data.NextColor)
            Flags.next[num] = Id_num
        else:
            Flags.flag[num] = True
            Flags.turn -= 1
        dialog.Destroy()
    else:
        agent_main.NextSet(Id_num)
        field[0].NextColoring(agent_main.next[1], agent_data.NextColor)
        Flags.next[num] = Id_num

def FieldButtonFunc(Id_num):
    if (Id_num in agent[0].movable or Id_num in agent[1].movable) and Id_num not in (agent[2].now, agent[3].now) and not blue_Flags.end:
        move([agent[0], agent[1]], agent_data[0], blue_Flags, (agent[2], agent[3]), Id_num)
    elif (Id_num in agent[2].movable or Id_num in agent[3].movable) and Id_num not in (agent[0].now, agent[1].now) and blue_Flags.end and not Modes.auto:
        move([agent[2], agent[3]], agent_data[1], red_Flags, (agent[0], agent[1]), Id_num)
    else:
        log.LogWrite('Can not move position ({},{})\n'.format(int(Id_num/1000), Id_num%1000), logtype=procon29.ERROR)
        errordialog = wx.MessageDialog(None, '移動できない地点です ({},{})'.format(int(Id_num/1000), Id_num%1000), '移動可能エリア外',style=wx.ICON_EXCLAMATION)
        errordialog.ShowModal()
        errordialog.Destroy()

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

def OpenFile(file):
    if '.png' in file:
        data = decode(Image.open(file))
        text = data[0][0].decode('utf-8', 'ignore')
        return text.split(':')
    elif '.pqr' in file:
        f = open(file, 'r')
        text = f.read()
        return text.replace('\n','').split(':')
    else:
        raise FormatError(file)

def FileOpenAll(filedata):
    global load_file_flag
    global turn
    if len(field) != 0:
        FiledClear()
    try:
        text = OpenFile(filedata)
    except FormatError:
        log.LogWrite("Can't open file", logtype=procon29.ERROR)
    else:
        log.LogWrite('File open {}\n'.format(filedata), logtype=procon29.FILE_LOG)
        createbutton(text)
        turndialog = wx.TextEntryDialog(None, 'ターン数を入力してください', 'ターン数決定')
        turndialog.SetValue('10')
        turndialog.ShowModal()
        turn = int(turndialog.GetValue())
        turnunm.SetLabel('現在ターン数:{}\n全ターン数:{}\n残りターン数:{}'.format(nowturn, turn, turn-nowturn))
        turndialog.Destroy()
        gauge.SetRange(turn-1)
        gauge.SetValue(nowturn-1)
        log.LogWrite('Complete init. Turn {}\n'.format(turn))
        load_file_flag = True

def Menu_handler(event):
    global load_file_flag
    global debag_flag
    Id_num = event.GetId()
    if Id_num == 10:
        if len(field) != 0:
            FiledClear()
        log.LogWrite('Open file select dialog\n', logtype=procon29.SYSTEM_LOG)
        dialog = wx.FileDialog(None,'Select File','./')
        dialog.SetWildcard('*.png;*.pqr')
        dialog.ShowModal()
        filedata = dialog.GetPath()
        FileOpenAll(filedata)
    elif Id_num == 11:
        start_func()
    elif Id_num == 12:
        FiledClear()
    elif Id_num == 13:
        log.LogWrite('Quit Program\n', logtype=procon29.SYSTEM_LOG)
        sys.exit(0)
    elif Id_num == 14:
        debag_flag = True
        log.LogWrite('Start debag mode\n', logtype=procon29.SYSTEM_LOG)
    elif Id_num == 20:
        log.LogWrite('Open about info\n', logtype=procon29.SYSTEM_LOG)
        info = adv.AboutDialogInfo()
        info.SetName('PPAP -Procon29 Python Application Project-')
        info.SetVersion('2.0.0')
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
            Modes.Clear()
            Modes.manual = True
            start.Disable()
        elif mode == 1:
            if Modes.auto == False:
                Modes.AutoSet(log, field[0], [agent[2], agent[3]], red_Flags)
            color_select.Enable()
            start.Enable()
        elif mode == 2:
            color_select.Disable()
            Modes.Clear()
            Modes.battle = True
            start.Enable()
        elif mode == 3:
            if Modes.learn == False:
                Modes.LearnSet(log, field[0], agent, [blue_Flags, red_Flags])
            color_select.Disable()
            start.Enable()
            cancel.Disable()
            turn_end.Disable()
    elif Id_num == 101:
        color = event.GetSelection()
        log.LogWrite('Chenge color {}->{}\n'.format(now_color+1, color+1), logtype=procon29.SYSTEM_LOG)
        now_color = color
        if color == 0:
            pass
        elif color == 1:
            pass

def CancelFunc():
    if red_Flags.flag[0] != True or red_Flags.flag[1] != True:
        red_Flags.Clear()
        col_func(False, False)
        log.LogWrite('Cancel step\n')
    elif blue_Flags.flag[0] != True or blue_Flags.flag[1] != True:
        blue_Flags.Clear()
        field[0].MovableColoring(agent_data[1], (agent[2], agent[3]), clear=True)
        col_func(True, False)
        log.LogWrite('Cancel step\n')

def Button_handler(event):
    global load_file_flag
    global debag_flag
    Id_num = event.GetId()
    if debag_flag:
        move([agent[0], agent[1]], agent_data[0], blue_Flags, (agent[2], agent[3]), Id_num)
        turnendfunc()
    elif Modes.auto:
        if Id_num == 200:
            start_func()
        elif start_flag == False:
            log.LogWrite('Not start Game', logtype=procon29.ERROR)
            errordialog = wx.MessageDialog(None, 'ゲームがスタートされていません','ゲーム未スタート')
            errordialog.ShowModal()
            errordialog.Destroy()
        elif Id_num == 201:
            blue_Flags.Clear()
            col_func(True, False)
            log.LogWrite('Cancel step\n')
        elif Id_num == 202:
            if blue_Flags.flag == False_list and blue_Flags.end == False:
                Modes.bot[0].NextPositionSet(agent[2], agent_data[1], (agent[0].now, agent[1].now), 0)
                Modes.bot[1].NextPositionSet(agent[3], agent_data[1], (agent[0].now, agent[1].now), 1)
                turnendfunc()
            else:
                log.LogWrite('No finish all step\n', logtype=procon29.ERROR)
                errordialog = wx.MessageDialog(None, '全ての工程が終了していません', '移動未終了',style=wx.ICON_EXCLAMATION)
                errordialog.ShowModal()
                errordialog.Destroy()
        else:
            FieldButtonFunc(Id_num)
    elif Modes.learn:
        if Id_num == 200:
            start_func()
            for i in range(turn):
                Modes.bot[0].NextPositionSet(agent[0], agent_data[0], (agent[2].now, agent[3].now), 0)
                Modes.bot[1].NextPositionSet(agent[1], agent_data[0], (agent[2].now, agent[3].now), 1)
                Modes.bot[2].NextPositionSet(agent[2], agent_data[1], (agent[0].now, agent[1].now), 0)
                Modes.bot[3].NextPositionSet(agent[3], agent_data[1], (agent[0].now, agent[1].now), 1)
                turnendfunc()
        elif start_flag == False:
            log.LogWrite('Not start Game', logtype=procon29.ERROR)
            errordialog = wx.MessageDialog(None, 'ゲームがスタートされていません','ゲーム未スタート')
            errordialog.ShowModal()
            errordialog.Destroy()
    else:
        if Id_num == 201:
            CancelFunc()
        elif Id_num == 202:
            if load_file_flag:
                if red_Flags.flag == False_list:
                    turnendfunc()
                elif blue_Flags.flag == False_list and blue_Flags.end == False:
                    log.LogWrite('Blue step finished\n')
                    field[0].MovableColoring(agent_data[0], (agent[0], agent[1]), clear=True)
                    col_func(False, False)
                    blue_Flags.end = True
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
            FieldButtonFunc(Id_num)
    
def Check_handler(event):
    global col_check
    obj = event.GetEventObject()
    if load_file_flag:
        if obj.IsChecked(0):
            obj.SetCheckedItems((0,))
        for i in range(len(col_set)):
            col_check[event.GetId()-300][i] = obj.IsChecked(i)
        blue_red, turn_exit = (True, blue_Flags.end) if event.GetId() == 300 else (False, not blue_Flags.end)
        col_func(blue_red, turn_exit)

def Key_handler(event):
    Id_num = event.GetKeyCode()
    print(Id_num)
    print(wx.WXK_ESCAPE)
    if Id_num == wx.WXK_ESCAPE:
        print('ok')
        CancelFunc()
    if Id_num == 300:
        blue_Flags.Clear()
        col_func(True, False)
        log.LogWrite('Cancel step\n')
