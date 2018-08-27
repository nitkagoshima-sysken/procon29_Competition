# -*- coding: utf-8 -*-

class FakeBot():
    def __init__(self, logfile, field_data, position, flags):
        self.log = logfile
        self.position = position
        self.filed_point = field_data.point
        self.flags = flags


    def NextPositionSet(self, agent, agent_data, enow, num):
        temp = -10
        temp_pos = 0
        for i in range(len(agent.movable)):
            x, y = int(agent.movable[i]/1000)-1, agent.movable[i]%1000-1
            if temp < self.filed_point[y][x] and agent.movable[i] not in agent_data.GetPosition and agent.movable[i] not in enow:
                temp_pos = agent.movable[i]
                temp = self.filed_point[y][x]
        if temp == -10:
            for i in range(len(agent.movable)):
                x, y = int(agent.movable[i]/1000)-1, agent.movable[i]%1000-1
                if temp < self.filed_point[y][x] and agent.movable[i] not in enow:
                    temp_pos = agent.movable[i]
                    temp = self.filed_point[y][x]
        self.move(agent, temp_pos, num)

    def move(self, agent, pos, num):
        if pos in agent.removable:
            agent.NextSet(pos, overlap=True)
            self.flags.next[num] = pos
        else:
            agent.NextSet(pos)
            self.flags.next[num] = pos

class MainBot(FakeBot):
    def __init__(self, logfile, field_data, position, flags):
        FakeBot.__init__(self, logfile, field_data, position, flags)
        self.aim = self.AimCenterSwicth()
        self.field = {'outpos' : field_data.field_out,
                    'inpos' : field_data.field_size}
    
    def AimCenterSwicth(self):
        pass