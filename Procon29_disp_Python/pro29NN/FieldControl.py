# -*- coding: utf-8 -*-
import pickle
import wx
import pro29NN

class Field:
    """
    Field control class.
    qrdata : Input qrdata.
    field : Panel object of field.
    logfile : logfile select.
    """
    def __init__(self, qrdata, field, logfile):
        self.y, self.x = map(int,qrdata[0].split(' '))
        self.field_out = []
        self.field_size =[]
        self.panel = []
        self.point =[]
        self.quarter = []
        self.field = field
        self.log = logfile
        for i in range(self.y+2):
            for j in range(self.x+2):
                self.field_out.append(j*1000+i)
                if i != 0 and j != 0 and i != self.y+1 and j != self.x+1:
                    self.field_size.append(j*1000+i)
        self.field_out = [item for item in self.field_out if item not in self.field_size]
        for i in range(self.y):
            self.point.append(list(map(int, qrdata[i+1].strip().split(' '))))
        for i in range(self.y):
            self.panel.append([wx.Button(field, (j+1)*1000+(i+1), str(self.point[i][j]), size=(50,50), pos=(50*j,50*i))\
                            for j in range(self.x)])

    def Coloring(self, agent_data, agent, out_get=False, out_now=False):
        """
        Now position and Get position coloring method.
        """
        if out_get == False:
            self.GetHide(agent_data, agent)
        else:
            for i in range(len(agent_data.GetPosition)):
                self.panel[agent_data.GetPosition[i]%1000-1][int(agent_data.GetPosition[i]/1000)-1]\
                .SetBackgroundColour(agent_data.GetColor)
                self.log.LogWrite('Coloring of get ({},{}):{}\n'\
                                .format(int(agent_data.GetPosition[i]/1000), agent_data.GetPosition[i]%1000, agent_data.GetColor), logtype=pro29NN.SYSTEM_LOG)
        if out_now == False:
            self.NowHide(agent_data, agent)
        else:
            for i in range(2):
                self.panel[int(agent[i].now%1000)-1][int(agent[i].now/1000)-1]\
                .SetBackgroundColour(agent_data.Color)
                self.log.LogWrite('Coloring of now ({},{}):{}\n'\
                                .format(int(agent[i].now/1000), agent[i].now%1000, agent_data.Color), logtype=pro29NN.SYSTEM_LOG)

    def FillColoring(self, agent_data, out=False):
        """
        Fill coloring method.
        """
        if out == False:
            self.FillHide(agent_data)
        else:
            for i in range(len(agent_data.GetField)):
                self.panel[int(agent_data.GetField[i]%1000)-1][int(agent_data.GetField[i]/1000)-1]\
                .SetBackgroundColour(agent_data.FillColor)
                self.log.LogWrite('Coloring of Fill ({},{}):{}\n'\
                                .format(int(agent_data.GetField[i]/1000), agent_data.GetField[i]%1000, '#FFFFFF'), logtype=pro29NN.SYSTEM_LOG)

    def FillHide(self, agent_data):
        """
        Fill coloring clear method.
        """
        for i in range(len(agent_data.GetField)):
            self.panel[int(agent_data.GetField[i]%1000)-1][int(agent_data.GetField[i]/1000)-1]\
            .SetBackgroundColour('')

    def GetHide(self, agent_data, agent):
        for i in range(len(agent_data.GetPosition)):
            self.panel[agent_data.GetPosition[i]%1000-1][int(agent_data.GetPosition[i]/1000)-1]\
            .SetBackgroundColour('')

    def NowHide(self, agent_data, agent):
        for i in range(2):
            self.panel[int(agent[i].now%1000)-1][int(agent[i].now/1000)-1]\
            .SetBackgroundColour('')
    
    def MovableColoring(self, agent_data, agent, clear=False, out=False):
        """
        Movable and Removable coloring method.
        """
        if clear == True:
            self.MoveColoring(agent_data, agent)
        elif out == False:
            self.MoveColoring(agent_data, agent)
        elif str(type(agent)) == "<class 'tuple'>":
            for i in range(2):
                for j in range(len(agent[i].movable)):
                    if agent[i].movable[j] not in agent_data.GetPosition and agent[i].movable[j] != agent[i].next:
                        self.log.LogWrite('Coloring of movable ({},{}):{}\n'\
                                        .format(int(agent[i].movable[j]/1000), agent[i].movable[j]%1000, agent_data.MovableColor), logtype=pro29NN.SYSTEM_LOG)
                        self.panel[agent[i].movable[j]%1000-1][int(agent[i].movable[j]/1000)-1]\
                        .SetBackgroundColour(agent_data.MovableColor)
                for j in range(len(agent[i].removable)):
                    if agent[i].removable[j] not in agent_data.GetPosition:
                        self.panel[int(agent[i].removable[j]%1000)-1][int(agent[i].removable[j]/1000)-1]\
                        .SetBackgroundColour(agent_data.RemovableColor)
                        self.log.LogWrite('Coloring of removable ({},{}):{}\n'\
                                        .format(int(agent[i].removable[j]/1000), agent[i].removable[j]%1000, agent_data.RemovableColor), logtype=pro29NN.SYSTEM_LOG)
        else:
            for j in range(len(agent.movable)):
                if agent.movable[j] not in agent_data.GetPosition and agent.movable[j] != agent.next:
                    self.log.LogWrite('Coloring of movable ({},{}):{}\n'\
                                    .format(int(agent.movable[j]/1000), agent.movable[j]%1000, agent_data.MovableColor), logtype=pro29NN.SYSTEM_LOG)
                    self.panel[agent.movable[j]%1000-1][int(agent.movable[j]/1000)-1]\
                    .SetBackgroundColour(agent_data.MovableColor)
            for j in range(len(agent.removable)):
                if agent.removable[j] not in agent_data.GetPosition:
                    self.panel[int(agent.removable[j]%1000)-1][int(agent.removable[j]/1000)-1]\
                    .SetBackgroundColour(agent_data.RemovableColor)
                    self.log.LogWrite('Coloring of removable ({},{}):{}\n'\
                                    .format(int(agent.removable[j]/1000), agent.removable[j]%1000, agent_data.RemovableColor), logtype=pro29NN.SYSTEM_LOG)
    
    def MoveColoring(self, agent_data, agent):
        """
        Movable and Removable coloring clear method.
        """
        for i in range(2):
            for j in range(len(agent[i].movable)):
                if agent[i].movable[j] not in agent_data.GetPosition:
                    self.panel[int(agent[i].movable[j]%1000)-1][int(agent[i].movable[j]/1000)-1]\
                    .SetBackgroundColour('')
            for j in range(len(agent[i].removable)):
                if agent[i].movable[j] not in agent_data.GetPosition:
                    self.panel[int(agent[i].removable[j]%1000)-1][int(agent[i].removable[j]/1000)-1]\
                    .SetBackgroundColour('')
    
    def NextColoring(self, next, col, clear=False):
        """
        Next position coloring medhod.
        """
        if clear:
            self.panel[int(next%1000)-1][int(next/1000)-1]\
            .SetBackgroundColour('')
        else:
            self.panel[int(next%1000)-1][int(next/1000)-1]\
            .SetBackgroundColour(col)
            self.log.LogWrite('Coloring of next ({},{}):{}\n'\
                            .format(int(next/1000), next%1000, col), logtype=pro29NN.SYSTEM_LOG)

    def FieldTypeAnalysis(self):
        """
        Analysising fiedl type method.
        """
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

    def Destroy(self):
        """
        Destroy field object method.
        """
        self.field.Destroy()
        self.log.LogWrite('Clear ALL', logtype=pro29NN.SYSTEM_LOG)