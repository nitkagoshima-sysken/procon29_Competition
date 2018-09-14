# -*- coding:utf-8 -*-

import sys

class Agent:
    def __init__(self, now, out, logfile, color):
        self.now = now
        self.next = [False, now]
        self.out = out
        self.logfile = logfile
        self.color = color
        self.movable, self.removable = 0, 0

    def MovableSet(self, eget):
        movable = []
        movable.append(self.now -(1000 + 1))
        movable.append(self.now - 1000)
        movable.append(self.now -(1000 - 1))
        movable.append(self.now +(1000 + 1))
        movable.append(self.now + 1000)
        movable.append(self.now +(1000 - 1))
        movable.append(self.now + 1)
        movable.append(self.now - 1)
        movable = [item for item in movable if item not in self.out]
        removable = [item for item in movable if item in eget]
        self.logfile.LogWrite('{} set movable {}\n'.format(self.color, str(movable)))
        self.logfile.LogWrite('{} set removable {}\n'.format(self.color, str(removable)))
        return (movable, removable)

    def TurnSet(self, eget):
        if self.next[0] or self.next[1] == 0:
            self.now = self.now
        else:
            self.now = self.next[1]
        self.next[0] = False
        self.movable, self.removable = self.MovableSet(eget)
        self.overlap_flag = False
    
    def NextSet(self, next, overlap=False):
        if overlap:
            self.next[0] = True
            self.next[1] = next
            self.logfile.LogWrite('{} Next set ({},{}) & Overlap\n'.format(self.color, int(next/1000), next%1000))
        else:
            self.next[0] = False
            self.next[1] = next
            self.logfile.LogWrite('{} Next set ({},{})\n'.format(self.color, int(next/1000), next%1000))
    
class AgentData:
    def __init__(self, color, logfile, point):
        self.GetPosition = []
        self.Point = 0
        self.Color = color
        self.AllPoint = point
        self.buffer = []
        self.GetField = []
        self.x, self.y = len(point[0]), len(point)
        self.get = [[0 for i in range(self.x+2)]for j in range(self.y+2)]
        self.TerritoryPoint = 0
        if color == 'blue':
            self.GetColor = '#0077FF'
            self.MovableColor = '#00FFFF'
            self.NextColor = '#7700FF'
            self.FillColor = '#007777'
        elif color == 'red':
            self.GetColor = '#FF7700'
            self.MovableColor = '#FFFF00'
            self.NextColor = '#FF0077'
            self.FillColor = '#778800'
        self.RemovableColor = '#FF77FF'
        self.LogFile = logfile
    
    def GetPoint(self, get, enemy_data, logout=True):
        if str(type(get)) == "<class 'list'>":
            for i in range(len(get)):
                if get[i][0] or get[i][1] == 0:
                    self.Remove(get[i], enemy_data, logout)
                elif get[i][1] not in self.GetPosition:
                    self.GetPosition.append(get[i][1])
                    self.Point += self.AllPoint[get[i][1]%1000-1][int(get[i][1]/1000)-1]
                    if logout:
                        self.LogFile.LogWrite('{} Get:({},{}) Point:{}\n'\
                                        .format(self.Color, int(get[i][1]/1000), get[i][1]%1000, self.AllPoint[get[i][1]%1000-1][int(get[i][1]/1000)-1]))
        else:
            if get[0] or get[1] == 0:
                self.Remove(get[i], enemy_data, logout)
            elif get[1] not in self.GetPosition:
                self.GetPosition.append(get[1])
                self.Point += self.AllPoint[get[1]%1000-1][int(get[1]/1000)-1]
                if logout:
                    self.LogFile.LogWrite('{} Get:({},{}) Point:{}\n'\
                                    .format(self.Color, int(get[1]/1000), get[1]%1000, self.AllPoint[get[1]%1000-1][int(get[1]/1000)-1]))
        if logout:
            self.LogFile.LogWrite('Now {} get point:{}\n'.format(self.Color, self.Point))

    def Remove(self, rm, enemy, logout=True):
        print('Remove')
        if rm[1] != 0:
            enemy.GetPosition.remove(rm[1])
            enemy.Point -= self.AllPoint[int(rm[1]%1000)-1][int(rm[1]/1000)-1]
            if logout:
                self.LogFile.LogWrite('{} Remove:({},{}) Point:{}\n'\
                                .format(enemy.Color, int(rm[1]/1000), int(rm[1]%1000), self.AllPoint[int(rm[1]%1000)-1][int(rm[1]/1000)-1]))

    def FieldPointSearch(self, logout=True):
        self.GetField = []
        self.TerritoryPoint = 0
        for i in range(self.y+2):
            for j in range(self.x+2):
                if (j*1000)+i in self.GetPosition:
                    self.get[i][j] = 1
                elif i == 0 or j == 0 or i == self.y+1 or j == self.x+1:
                    self.get[i][j] = -1
                else:
                    self.get[i][j] = 0
        for i in range(self.y+1):
            for j in range(self.x+1):
                if self.get[i][j] == 1:
                    for k in range(self.x-j):
                        if self.get[i][j+k] == 0:
                            del self.buffer[:]
                            self.TerritoryFill(j+k, i)
        for i in range(len(self.get)):
            for j in range(len(self.get[0])):
                if self.get[i][j] == 2:
                    self.GetField.append(j*1000+i)
        self.GetField = [item for item in self.GetField if item not in self.GetPosition]
        if logout:
            self.LogFile.LogWrite('Territory Posirion{}\n'.format(self.GetField))
        for i in range(len(self.GetField)):
            self.TerritoryPoint += abs(self.AllPoint[self.GetField[i]%1000-1][int(self.GetField[i]/1000)-1])

    def TerritoryFill(self, x, y):
        self.buffer.append((x, y))
        if self.get[y-1][x] == -1 or self.get[y][x+1] == -1 or self.get[y+1][x] == -1 or self.get[y][x-1] == -1 or\
            self.get[y-1][x] == -2 or self.get[y][x+1] == -2 or self.get[y+1][x] == -2 or self.get[y][x-1] == -2:
            self.get[y][x] = -2
            return -2
        else:
            self.get[y][x] = 2
        if self.get[y][x+1] == 0 and self.get[y][x] != -2:
            self.get[y][x] = self.TerritoryFill(x+1, y)
        if self.get[y][x-1] == 0 and self.get[y][x] != -2:
            self.get[y][x] = self.TerritoryFill(x-1, y)
        if self.get[y-1][x] == 0 and self.get[y][x] != -2:
            self.get[y][x] = self.TerritoryFill(x, y-1)
        if self.get[y+1][x] == 0 and self.get[y][x] != -2:
            self.get[y][x] = self.TerritoryFill(x, y+1)
        if self.get[y][x] != -2:
            return 2
        else:
            for i in range(len(self.buffer)):
                self.get[self.buffer[i][1]][self.buffer[i][0]] = -2
            return -2

class TempAgentData:
    def __init__(self, point, InputData, Agent):
        self.GetPosition = []
        self.AllPoint = point
        self.buffer = []
        self.GetField = []
        self.x, self.y = len(point[0]), len(point)
        self.get = [[0 for i in range(self.x+2)]for j in range(self.y+2)]
        self.DataCopy(InputData, Agent)

    def GetPoint(self, get, enemy_data):
        if str(type(get)) == "<class 'list'>":
            for i in range(len(get)):
                if get[i][0] or get[i][1] == 0:
                    self.Remove(get[i], enemy_data)
                elif get[i][1] not in self.GetPosition:
                    self.GetPosition.append(get[i][1])
        else:
            if get[0] or get[1] == 0:
                self.Remove(get[i], enemy_data)
            elif get[1] not in self.GetPosition:
                self.GetPosition.append(get[1])

    def Remove(self, rm, enemy):
        if rm[1] != 0:
            enemy.GetPosition.remove(rm[1])

    def FieldPointSearch(self):
        self.GetField = []
        self.TerritoryPoint = 0
        for i in range(self.y+2):
            for j in range(self.x+2):
                if (j*1000)+i in self.GetPosition:
                    self.get[i][j] = 1
                elif i == 0 or j == 0 or i == self.y+1 or j == self.x+1:
                    self.get[i][j] = -1
                else:
                    self.get[i][j] = 0
        for i in range(self.y+1):
            for j in range(self.x+1):
                if self.get[i][j] == 1:
                    for k in range(self.x-j):
                        if self.get[i][j+k] == 0:
                            del self.buffer[:]
                            self.TerritoryFill(j+k, i)
        for i in range(len(self.get)):
            for j in range(len(self.get[0])):
                if self.get[i][j] == 2:
                    self.GetField.append(j*1000+i)
        self.GetField = [item for item in self.GetField if item not in self.GetPosition]

    def TerritoryFill(self, x, y):
        self.buffer.append((x, y))
        if self.get[y-1][x] == -1 or self.get[y][x+1] == -1 or self.get[y+1][x] == -1 or self.get[y][x-1] == -1 or\
            self.get[y-1][x] == -2 or self.get[y][x+1] == -2 or self.get[y+1][x] == -2 or self.get[y][x-1] == -2:
            self.get[y][x] = -2
            return -2
        else:
            self.get[y][x] = 2
        if self.get[y][x+1] == 0 and self.get[y][x] != -2:
            self.get[y][x] = self.TerritoryFill(x+1, y)
        if self.get[y][x-1] == 0 and self.get[y][x] != -2:
            self.get[y][x] = self.TerritoryFill(x-1, y)
        if self.get[y-1][x] == 0 and self.get[y][x] != -2:
            self.get[y][x] = self.TerritoryFill(x, y-1)
        if self.get[y+1][x] == 0 and self.get[y][x] != -2:
            self.get[y][x] = self.TerritoryFill(x, y+1)
        if self.get[y][x] != -2:
            return 2
        else:
            for i in range(len(self.buffer)):
                self.get[self.buffer[i][1]][self.buffer[i][0]] = -2
            return -2

    def MovableSet(self, eget, agent):
        movable = []
        movable.append(agent.now -(1000 + 1))
        movable.append(agent.now - 1000)
        movable.append(agent.now -(1000 - 1))
        movable.append(agent.now +(1000 + 1))
        movable.append(agent.now + 1000)
        movable.append(agent.now +(1000 - 1))
        movable.append(agent.now + 1)
        movable.append(agent.now - 1)
        movable = [item for item in movable if item not in agent.out]
        removable = [item for item in movable if item in eget]
        return (movable, removable)

    def TurnSet(self, eget, pos):
        for i in range(len(self.Agent)):
            self.Agent[i].next = pos[i]
            self.Agent[i].TurnSet(eget)

    def DataCopy(self, InputData, Agent):
        self.Agent = []
        self.GetPosition = InputData.GetPosition[:]
        self.GetField = InputData.GetField[:]
        for agent in Agent:
            self.Agent.append(TempAgent(agent.now, agent.out))

class TempAgent:
    def __init__(self, now, out):
        self.now = now
        self.next = [False, now]
        self.out = out
        self.movable, self.removable = 0, 0

    def MovableSet(self, eget):
        movable = []
        movable.append(self.now -(1000 + 1))
        movable.append(self.now - 1000)
        movable.append(self.now -(1000 - 1))
        movable.append(self.now +(1000 + 1))
        movable.append(self.now + 1000)
        movable.append(self.now +(1000 - 1))
        movable.append(self.now + 1)
        movable.append(self.now - 1)
        movable = [item for item in movable if item not in self.out]
        removable = [item for item in movable if item in eget]
        return (movable, removable)

    def TurnSet(self, eget):
        if self.next[0] or self.next[1] == 0:
            self.now = self.now
        else:
            self.now = self.next[1]
        self.next[0] = False
        self.movable, self.removable = self.MovableSet(eget)
        self.overlap_flag = False

class LaernAgentData:
    def __init__(self, color, point):
        self.GetPosition = []
        self.Point = 0
        self.Color = color
        self.AllPoint = point
        self.buffer = []
        self.GetField = []
        self.x, self.y = len(point[0]), len(point)
        self.get = [[0 for i in range(self.x+2)]for j in range(self.y+2)]
        self.TerritoryPoint = 0
    
    def GetPoint(self, get, enemy_data, logout=True):
        if str(type(get)) == "<class 'list'>":
            for i in range(len(get)):
                if get[i][0] or get[i][1] == 0:
                    self.Remove(get[i], enemy_data, logout)
                elif get[i][1] not in self.GetPosition:
                    self.GetPosition.append(get[i][1])
                    self.Point += self.AllPoint[get[i][1]%1000-1][int(get[i][1]/1000)-1]

        else:
            if get[0] or get[1] == 0:
                self.Remove(get[i], enemy_data, logout)
            elif get[1] not in self.GetPosition:
                self.GetPosition.append(get[1])
                self.Point += self.AllPoint[get[1]%1000-1][int(get[1]/1000)-1]


    def Remove(self, rm, enemy, logout=True):
        if rm[1] != 0:
            enemy.GetPosition.remove(rm[1])
            enemy.Point -= self.AllPoint[int(rm[1]%1000)-1][int(rm[1]/1000)-1]
            
    def FieldPointSearch(self, logout=True):
        self.GetField = []
        self.TerritoryPoint = 0
        for i in range(self.y+2):
            for j in range(self.x+2):
                if (j*1000)+i in self.GetPosition:
                    self.get[i][j] = 1
                elif i == 0 or j == 0 or i == self.y+1 or j == self.x+1:
                    self.get[i][j] = -1
                else:
                    self.get[i][j] = 0
        for i in range(self.y+1):
            for j in range(self.x+1):
                if self.get[i][j] == 1:
                    for k in range(self.x-j):
                        if self.get[i][j+k] == 0:
                            del self.buffer[:]
                            self.TerritoryFill(j+k, i)
        for i in range(len(self.get)):
            for j in range(len(self.get[0])):
                if self.get[i][j] == 2:
                    self.GetField.append(j*1000+i)
        self.GetField = [item for item in self.GetField if item not in self.GetPosition]
        
        for i in range(len(self.GetField)):
            self.TerritoryPoint += abs(self.AllPoint[self.GetField[i]%1000-1][int(self.GetField[i]/1000)-1])

    def TerritoryFill(self, x, y):
        self.buffer.append((x, y))
        if self.get[y-1][x] == -1 or self.get[y][x+1] == -1 or self.get[y+1][x] == -1 or self.get[y][x-1] == -1 or\
            self.get[y-1][x] == -2 or self.get[y][x+1] == -2 or self.get[y+1][x] == -2 or self.get[y][x-1] == -2:
            self.get[y][x] = -2
            return -2
        else:
            self.get[y][x] = 2
        if self.get[y][x+1] == 0 and self.get[y][x] != -2:
            self.get[y][x] = self.TerritoryFill(x+1, y)
        if self.get[y][x-1] == 0 and self.get[y][x] != -2:
            self.get[y][x] = self.TerritoryFill(x-1, y)
        if self.get[y-1][x] == 0 and self.get[y][x] != -2:
            self.get[y][x] = self.TerritoryFill(x, y-1)
        if self.get[y+1][x] == 0 and self.get[y][x] != -2:
            self.get[y][x] = self.TerritoryFill(x, y+1)
        if self.get[y][x] != -2:
            return 2
        else:
            for i in range(len(self.buffer)):
                self.get[self.buffer[i][1]][self.buffer[i][0]] = -2
            return -2

class LearnAgent:
    def __init__(self, now, out):
        self.now = now
        self.next = [False, now]
        self.out = out
        self.movable, self.removable = 0, 0

    def MovableSet(self, eget):
        movable = []
        movable.append(self.now -(1000 + 1))
        movable.append(self.now - 1000)
        movable.append(self.now -(1000 - 1))
        movable.append(self.now +(1000 + 1))
        movable.append(self.now + 1000)
        movable.append(self.now +(1000 - 1))
        movable.append(self.now + 1)
        movable.append(self.now - 1)
        movable = [item for item in movable if item not in self.out]
        removable = [item for item in movable if item in eget]
        return (movable, removable)

    def TurnSet(self, eget):
        if self.next[0] or self.next[1] == 0:
            self.now = self.now
        else:
            self.now = self.next[1]
        self.next[0] = False
        self.movable, self.removable = self.MovableSet(eget)
        self.overlap_flag = False
    
    def NextSet(self, next, overlap=False):
        if overlap:
            self.next[0] = True
            self.next[1] = next
        else:
            self.next[0] = False
            self.next[1] = next
