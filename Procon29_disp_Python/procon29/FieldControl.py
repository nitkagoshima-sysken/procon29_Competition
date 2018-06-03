# -*- coding: utf-8 -*-

import wx
import procon29

class Field:
    def __init__(self, qrdata, field, logfile):
        self.y, self.x = map(int,qrdata[0].split(' '))
        self.field_out = []
        self.field_size =[]
        self.panel = []
        self.point =[]
        self.field = field
        self.log = logfile
        for i in range(self.y+2):
            for j in range(self.x+2):
                self.field_out.append(j*1000+i)
                if i != 0 and j != 0 and i != self.y+1 and j != self.x+1:
                    self.field_size.append(j*1000+i)
        self.field_out = [item for item in self.field_out if item not in self.field_size]
        for i in range(self.y):
            self.point.append(list(map(int,qrdata[i+1].strip().split(' '))))
        for i in range(self.y):
            self.panel.append([wx.Button(field, (j+1)*1000+(i+1), str(self.point[i][j]), size=(50,50), pos=(50*j,50*i))\
                            for j in range(self.x)])

    
    def Coloring(self, agent_data, agent):
        for i in range(len(agent_data.GetPosition)):
            self.panel[agent_data.GetPosition[i]%1000-1][int(agent_data.GetPosition[i]/1000)-1]\
            .SetBackgroundColour(agent_data.GetColor)
            self.log.LogWrite('Coloring of get ({},{}):{}\n'\
                            .format(int(agent_data.GetPosition[i]/1000), agent_data.GetPosition[i]%1000, agent_data.GetColor), logtype=procon29.SYSTEM_LOG)
        for i in range(2):
            self.panel[int(agent[i].now%1000)-1][int(agent[i].now/1000)-1]\
            .SetBackgroundColour(agent_data.Color)
            self.log.LogWrite('Coloring of now ({},{}):{}\n'\
                            .format(int(agent[i].now/1000), agent[i].now%1000, agent_data.Color), logtype=procon29.SYSTEM_LOG)
    
    def MovableColoring(self, agent_data, agent, clear=False):
        if clear == True:
            self.MoveColoring(agent_data, agent)
        else:
            for i in range(2):
                for j in range(len(agent[i].movable)):
                    if agent[i].movable[j] not in agent_data.GetPosition and agent[i].movable[j] != agent[i].next:
                        self.log.LogWrite('Coloring of movable ({},{}):{}\n'\
                                        .format(int(agent[i].movable[j]/1000), agent[i].movable[j]%1000, agent_data.MovableColor), logtype=procon29.SYSTEM_LOG)
                        self.panel[agent[i].movable[j]%1000-1][int(agent[i].movable[j]/1000)-1]\
                        .SetBackgroundColour(agent_data.MovableColor)
                for j in range(len(agent[i].removable)):
                    if agent[i].removable[j] not in agent_data.GetPosition:
                        self.panel[int(agent[i].removable[j]%1000)-1][int(agent[i].removable[j]/1000)-1]\
                        .SetBackgroundColour(agent_data.RemovableColor)
                        self.log.LogWrite('Coloring of removable ({},{}):{}\n'\
                                        .format(int(agent[i].removable[j]/1000), agent[i].removable[j]%1000, agent_data.RemovableColor), logtype=procon29.SYSTEM_LOG)
    
    def MoveColoring(self, agent_data, agent):
        for i in range(2):
            for j in range(len(agent[i].movable)):
                if agent[i].movable[j] not in agent_data.GetPosition:
                    self.panel[int(agent[i].movable[j]%1000)-1][int(agent[i].movable[j]/1000)-1]\
                    .SetBackgroundColour('')
            for j in range(len(agent[i].removable)):
                if agent[i].movable[j] not in agent_data.GetPosition:
                    self.panel[int(agent[i].removable[j]%1000)-1][int(agent[i].removable[j]/1000)-1]\
                    .SetBackgroundColour('')
    
    def NextColoring(self, next, col):
        self.panel[int(next%1000)-1][int(next/1000)-1]\
        .SetBackgroundColour(col)
        self.log.LogWrite('Coloring of next ({},{}):{}\n'\
                        .format(int(next/1000), next%1000, col), logtype=procon29.SYSTEM_LOG)

    def Destroy(self):
        self.field.Destroy()
        self.log.LogWrite('Clear ALL', logtype=procon29.SYSTEM_LOG)

    def FieldTypeAnalysis(self):
        typenum = 0
        if self.point[0][0] == self.point[self.y-1][0] and\
            self.point[1][0] == self.point[self.y-2][0] and\
            self.point[2][0] == self.point[self.y-3][0]:
            typenum -= 1
        if self.point[self.y-1][0] == self.point[self.y-1][self.x-1] and\
            self.point[self.y-1][1] == self.point[self.y-1][self.x-2] and\
            self.point[self.y-1][2] == self.point[self.y-1][self.x-3]:
            typenum += 1
        return typenum