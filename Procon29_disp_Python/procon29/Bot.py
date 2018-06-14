# -*- coding: utf-8 -*-

class Bot():
    def __init__(self, logfile, filed_data, position):
        self.log = logfile
        self.position = position
        self.filed_point = filed_data.point
        self.aim = self.AimCenterSwicth()

    def AimCenterSwicth(self):
        pass

    def NextPositionSet(self, agent, flags):
        temp = -10
        temp_pos = 0
        for i in range(len(agent.moveable)):
            x, y = int(agent.moveable[i]/1000), agent.moveable[i]%1000
            if temp < self.filed_point[y][x]:
                temp_pos = agent.moveable[i]
                temp = self.filed_point[y][x]
        self.move(agent, temp_pos, flags)

    def move(self, agent, pos, flags):
        if pos in agent.removable:
            agent.NextSet(pos, overlap=True)
            flags.next[num] = pos
        else:
            agent.NextSet(pos)
            flags.next[num] = pos