# -*- coding:utf-8 -*-

class Agent:
    def __init__(self, now, out, eget, logfile, color):
        self.now = now
        self.next = 0
        self.out = out
        self.logfile = logfile
        self.color = color
        self.movable, self.removable = self.MovableSet(eget)

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

    def TurnEnd(self, eget):
        self.now = self.next
        self.movable, self.removable = self.MovableSet(eget)
    
    def NextSet(self, next):
        self.next = next
        self.logfile.LogWrite('{} Next set ({},{})\n'.format(self.color, int(next/1000), next%1000))
    
class AgentData:
    def __init__(self, color, logfile):
        self.GetPosition = []
        self.Point = 0
        self.Color = color
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
    
    def GetPoint(self, get, point):
        if str(type(get)) == "<class 'list'>":
            for i in range(len(get)):
                self.GetPosition.append(get[i])
                self.Point += point[get[i]%1000-1][int(get[i]/1000)-1]
                self.LogFile.LogWrite('{} Get:({},{}) Point:{}\n'\
                                        .format(self.Color, int(get[i]/1000), get[i]%1000, point[int(get[i]%1000)-1][int(get[i]/1000)-1]))
        else:
            self.GetPosition.append(get)
            self.Point += point[get%1000-1][int(get/1000)-1]
            self.LogFile.LogWrite('{} Get:({},{}) Point:{}\n'\
                                    .format(self.Color, int(get/1000), int(get%1000), point[int(get%1000)-1][int(get/1000)-1]))

    def Remove(self, rm, point):
        if str(type(rm)) == "<class 'list'>":
            for i in range(len(rm)):
                self.GetPosition.append(rm[i])
                self.Point -= point[int(rm[i]%1000)-1][int(rm[i]/1000)-1]
                self.LogFile.LogWrite('{} Remove:({},{}) Point:{}\n'\
                                        .format(self.Color, int(rm[i]/1000), int(rm[i]%1000), point[int(rm[i]%1000)-1][int(rm[i]/1000)-1]))
        else:
            self.GetPosition.append(rm)
            self.Point -= point[int(rm%1000)-1][int(rm/1000)-1]
            self.LogFile.LogWrite('{} Remove:({},{}) Point:{}\n'\
                                    .format(self.Color, int(rm/1000), int(rm%1000), point[int(rm%1000)-1][int(rm/1000)-1]))