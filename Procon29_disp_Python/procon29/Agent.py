# -*- coding:utf-8 -*-

import sys
sys.setrecursionlimit(100000)

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
            self.next[1] = next
            self.logfile.LogWrite('{} Next set ({},{})\n'.format(self.color, int(next/1000), next%1000))
    
class AgentData:
    def __init__(self, color, logfile, point):
        self.GetPosition = []
        self.Point = 0
        self.Color = color
        self.AllPoint = point
        self.buffer = []
        self.x, self.y = len(point[0]), len(point)
        self.get = [[0 for i in range(self.x+2)]for j in range(self.y+2)]
        self.TerritoryPoint = 0
        if color == 'blue':
            self.GetColor = '#0077FF'
            self.MovableColor = '#00FFFF'
            self.NextColor = '#7700FF'
        elif color == 'red':
            self.GetColor = '#FF7700'
            self.MovableColor = '#FFFF00'
            self.NextColor = '#FF0077'
        self.RemovableColor = '#FF77FF'
        self.LogFile = logfile
    
    def GetPoint(self, get, enemy_data):
        if str(type(get)) == "<class 'list'>":
            for i in range(len(get)):
                if get[i][0] or get[i][1] == 0:
                    self.Remove(get[i], enemy_data)
                else:
                    self.GetPosition.append(get[i][1])
                    self.Point += self.AllPoint[get[i][1]%1000-1][int(get[i][1]/1000)-1]
                    self.LogFile.LogWrite('{} Get:({},{}) Point:{}\n'\
                                        .format(self.Color, int(get[i][1]/1000), get[i][1]%1000, self.AllPoint[get[i][1]%1000-1][int(get[i][1]/1000)-1]))
        else:
            if get[0] or get[1] == 0:
                self.Remove(get[i], enemy_data)
            else:
                self.GetPosition.append(get[1])
                self.Point += self.AllPoint[get[1]%1000-1][int(get[1]/1000)-1]
                self.LogFile.LogWrite('{} Get:({},{}) Point:{}\n'\
                                    .format(self.Color, int(get[1]/1000), get[1]%1000, self.AllPoint[get[1]%1000-1][int(get[1]/1000)-1]))
        self.LogFile.LogWrite('Now {} get point:{}\n'.format(self.Color, self.Point))

    def Remove(self, rm, enemy):
        if rm[1] != 0:
            enemy.GetPosition.remove(rm[1])
            enemy.Point -= self.AllPoint[int(rm[1]%1000)-1][int(rm[1]/1000)-1]
            self.LogFile.LogWrite('{} Remove:({},{}) Point:{}\n'\
                                .format(enemy.Color, int(rm[1]/1000), int(rm[1]%1000), self.AllPoint[int(rm[1]%1000)-1][int(rm[1]/1000)-1]))

    def FieldPointSearch(self):
        getfield = []
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
                    getfield.append(j*1000+i)
        getfield = [item for item in getfield if item not in self.GetPosition]
        self.LogFile.LogWrite('Territory Posirion{}\n'.format(getfield))
        for i in range(len(getfield)):
            self.TerritoryPoint += abs(self.AllPoint[getfield[i]%1000-1][int(getfield[i]/1000)-1])

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