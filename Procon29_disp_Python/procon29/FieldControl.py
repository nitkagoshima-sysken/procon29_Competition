# -*- coding: utf-8 -*-

import wx

class Field:
    def __init__(self, qrdata, field):
        self.y, self.x = map(int,qrdata[0].split(' '))
        self.field_out = []
        self.field_size =[]
        self.panel = []
        self.point =[]
        for i in range(self.y+1):
            for j in range(self.x+1):
                self.field_out.append(j*1000+i)
                if i != 0 and j != 0:
                    self.field_size.append(j*1000+i)
        self.field_out = [item for item in self.field_out if item not in self.field_size]
        for i in range(self.y):
            self.point.append(list(map(int,qrdata[i+1].strip().split(' '))))
        for i in range(self.y):
            self.panel.append([wx.Button(field, (j+1)*1000+(i+1), str(self.point[i][j]), size=(50,50), pos=(50*j,50*i))\
                            for j in range(self.x)])

    
    def Coloring(self, agent_data, agent):
        for i in range(len(agent_data.GetPosition)):
            self.panel[int(agent_data.GetPosition[i]%1000)-1][int(agent_data.GetPosition[i]/1000)-1]\
            .SetBackgroundColour(agent_data.GetColor)
        for i in range(2):
            self.panel[int(agent[i].now%1000)-1][int(agent[i].now/1000)-1]\
            .SetBackgroundColour(agent_data.Color)
    
    def MovableColoring(self, agent_data, agent, clear=False):
        if clear == True:
            self.MoveColoring(agent_data, agent)
        else:
            for i in range(2):
                for j in range(len(agent[i].movable)):
                    if agent[i].movable[j] not in agent_data.GetPosition and agent[i].movable[j] != agent[i].next:
                        self.panel[int(agent[i].movable[j]%1000)-1][int(agent[i].movable[j]/1000)-1]\
                        .SetBackgroundColour(agent_data.MovableColor)
                for j in range(len(agent[i].removable)):
                    if agent[i].movable[j] not in agent_data.GetPosition and agent[i].movable[j] != agent[i].next:
                        self.panel[int(agent[i].removable[j]%1000)-1][int(agent[i].removable[j]/1000)-1]\
                        .SetBackgroundColour(agent_data.RemovableColor)
    
    def MoveColoring(self, agent_data, agent):
        for i in range(2):
            for j in range(len(agent[i].movable)):
                if agent[i].movable[j] not in agent_data.GetPosition and agent[i].movable[j] != agent[i].next:
                    self.panel[int(agent[i].movable[j]%1000)-1][int(agent[i].movable[j]/1000)-1]\
                    .SetBackgroundColour('')
            for j in range(len(agent[i].removable)):
                if agent[i].movable[j] not in agent_data.GetPosition and agent[i].movable[j] != agent[i].next:
                    self.panel[int(agent[i].removable[j]%1000)-1][int(agent[i].removable[j]/1000)-1]\
                    .SetBackgroundColour('')
    
    def NextColoring(self, next, col):
        self.panel[int(next%1000)-1][int(next/1000)-1]\
        .SetBackgroundColour(col)