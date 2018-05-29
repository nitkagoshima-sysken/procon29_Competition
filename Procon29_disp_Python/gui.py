# -*- coding: utf-8 -*-

from pyzbar.pyzbar import decode
from PIL import Image
import os
import wx
import sys

class log_control:
    def __init__(self, file_name, log_box):
        self.name = file_name
        self.log = log_box

    def log_out(self):
        log_file = open(self.name,'r')
        self.log.AppendText(log_file.read())
        log_file.close()

    def log_write(self, write_text):
        log_file = open(self.name,'w')
        log_file.write(write_text)
        log_file.close()
        self.log_out()

def movable_coloring(movable, over, color='white'):
    for i in range(len(movable)):
        if movable[i] not in field_out and movable[i] not in over:
            if color == 'blue':
                button_3[int(movable[i]%1000)-1][int(movable[i]/1000)-1].SetBackgroundColour('#00ffff')
            elif color == 'red':
                button_3[int(movable[i]%1000)-1][int(movable[i]/1000)-1].SetBackgroundColour('#ffff00')
            elif color == 'white':
                button_3[int(movable[i]%1000)-1][int(movable[i]/1000)-1].SetBackgroundColour('')

def movable_set(now_pos, movable, get_pos, enemy_get, col):
    get_pos.append(now_pos[0])
    get_pos.append(now_pos[1])
    movable_coloring(movable, enemy_get)
    movable[0] = now_pos[0]-(1000+1)
    movable[1] = now_pos[0]-1000
    movable[2] = now_pos[0]-(1000-1)
    movable[3] = now_pos[0]-1
    movable[4] = now_pos[0]+1
    movable[5] = now_pos[0]+(1000-1)
    movable[6] = now_pos[0]+1000
    movable[7] = now_pos[0]+(1000+1)
    movable[8] = now_pos[1]-(1000+1)
    movable[9] = now_pos[1]-1000
    movable[10] = now_pos[1]-(1000-1)
    movable[11] = now_pos[1]-1
    movable[12] = now_pos[1]+1
    movable[13] = now_pos[1]+(1000-1)
    movable[14] = now_pos[1]+1000
    movable[15] = now_pos[1]+(1000+1)
    movable_coloring(movable, enemy_get, color=col)
    for i in range(len(get_pos)):
        button_3[int(get_pos[i]%1000)-1][int(get_pos[i]/1000)-1].SetBackgroundColour(col)
    f = open('test.txt','w')
    f.write(str(movable))
    f.close

def createbutton(text):
    y, x = map(int,text[0].split(' '))
    global field_out
    for i in range(y+1):
        for j in range(x+1):
            field_out.append(j*1000+i)
            if i != 0 and j != 0:
                field_size.append(j*1000+i)
    field_out = [item for item in field_out if item not in field_size]
    agent1_y, agent1_x = map(int,text[y+1].strip().split(' '))
    agent2_y, agent2_x = map(int,text[y+2].strip().split(' '))
    for i in range(y):
        point.append(list(map(int,text[i+1].strip().split(' '))))
    for i in range(y):
        button_3.append([wx.Button(field, (j+1)*1000+(i+1), str(point[i][j]), size=(50,50), pos=(50*j,50*i))\
                        for j in range(x)])
    button_3[agent1_y-1][agent1_x-1].SetBackgroundColour('blue')
    button_3[agent2_y-1][agent2_x-1].SetBackgroundColour('blue')
    now_pos_blue[0] = (agent1_x)*1000+(agent1_y)
    now_pos_blue[1] = (agent2_x)*1000+(agent2_y)
    movable_set(now_pos_blue, movable_blue, get_pos_blue, get_pos_red, 'blue')
    button_3[y-agent1_y][x-agent2_x].SetBackgroundColour('red')
    button_3[y-agent2_y][x-agent1_x].SetBackgroundColour('red')
    now_pos_red[0] = (agent1_x)*1000+(agent2_y)
    now_pos_red[1] = (agent2_x)*1000+(agent1_y)
    movable_set(now_pos_red, movable_red, get_pos_red, get_pos_blue, 'red')
    frame.SetStatusText(str(x),0)
    frame.SetStatusText(str(y),1)

def handler(event):
    Id_num = event.GetId()
    if Id_num == 10:
        dialog = wx.FileDialog(None,'Select File','./')
        dialog.ShowModal()
        image = dialog.GetPath()
        data = decode(Image.open(image))
        log.log_write('[File log] open {}\n'.format(image))
        text = data[0][0].decode('utf-8', 'ignore')
        text = list(text.split(':'))
        createbutton(text)
        log.log_write('[Game log] Complete init\n[Game log] Game start!\n')
    elif Id_num == 11:
        sys.exit(0)
    elif Id_num == 100:
        mode = event.GetSelection()
        if mode == 0:
            frame.SetStatusText('mode:1',1)
        elif mode == 1:
            frame.SetStatusText('mode:2',1)
        elif mode == 2:
            frame.SetStatusText('mode:3',1)
    else:
        y, x = int(Id_num%1000), int(Id_num/1000)
        if Id_num in movable_blue and Id_num not in get_pos_red:
            event.GetEventObject().SetBackgroundColour('blue')
            if Id_num in movable_blue[1:8]:
                now_pos_blue[0] = (x)*1000+(y)
            else:
                now_pos_blue[1] = (x)*1000+(y)
            movable_set(now_pos_blue, movable_blue, get_pos_blue, get_pos_red, 'blue')
        elif Id_num in movable_red and Id_num not in get_pos_blue:
            event.GetEventObject().SetBackgroundColour('red')
            if Id_num in movable_red[1:8]:
                now_pos_red[0] = (x)*1000+(y)
            else:
                now_pos_red[1] = (x)*1000+(y)
            movable_set(now_pos_red, movable_red, get_pos_red, get_pos_blue,'red')
        else:
            dialog2 = wx.MessageDialog(None, '移動できない場所です ({0},{1})'.format(x,y), '移動可能エリア外',style=wx.ICON_EXCLAMATION)
            dialog2.ShowModal()

point = []
button_3 = []
field_out = []
field_size = []
get_pos_blue = []
get_pos_red = []
now_pos_blue = [0, 0]
now_pos_red = [0,0]
movable_blue = [0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]
movable_red = [0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]
x, y = 0, 0
app = wx.App()
log_name = 'game.log'

frame = wx.Frame(None, -1, 'Test', size=(1200,800), style=wx.SYSTEM_MENU | wx.CAPTION | wx.CLOSE_BOX | wx.CLIP_CHILDREN | wx.MINIMIZE_BOX)
frame.CreateStatusBar(3)
frame.SetStatusText('Status Bar here.',2)

panel = wx.Panel(frame,-1)
field = wx.Panel(panel,-1,pos=(100,150),size=(800,600))

mode_type = ('手動入力モード','疑似対戦モード','学習モード')
mode_select = wx.RadioBox(panel, 100, 'モードセレクト', choices=mode_type)
mode_select.SetSelection(0)
layout = wx.BoxSizer(wx.HORIZONTAL)
layout.Add(mode_select, border=10)
panel.SetSizer(layout)

log_text = wx.TextCtrl(panel, -1, pos=(950,10), size=(230,700), style=wx.TE_MULTILINE)
log_text.SetBackgroundColour('white')
log = log_control(log_name, log_text)
log.log_write('[System log] Create {}\n'.format(log_name))

menu_bar = wx.MenuBar()
file_menu = wx.Menu()
open_file = wx.MenuItem(file_menu, 10, 'ファイルを開く')
exit_app = wx.MenuItem(file_menu, 11, '終了')
file_menu.Append(open_file)
file_menu.Append(exit_app)
menu_bar.Append(file_menu, 'ファイル')
frame.SetMenuBar(menu_bar)

frame.Bind(wx.EVT_MENU, handler)
frame.Bind(wx.EVT_BUTTON, handler)
frame.Bind(wx.EVT_RADIOBOX, handler)

frame.Show()
app.MainLoop()