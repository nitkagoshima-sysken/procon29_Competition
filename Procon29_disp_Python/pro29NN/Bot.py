# -*- coding: utf-8 -*-

import pickle
import wx
import pro29NN
from . import ProconNetwork
import numpy as np
from collections import OrderedDict

class ProconNNControl:
    """
    Neural network control class.
    """
    def __init__(self, MyAgentData, EnemyAgentData, log, network):
        self.MyAgentData = MyAgentData
        self.EnemyAgentData = EnemyAgentData
        self.AllPoint = MyAgentData.AllPoint
        self.x, self.y = len(self.AllPoint[0]), len(self.AllPoint)
        self.params = {}
        self.log = log
        self.NetWork = network

    def NextEvalution(self, NextPosition1, NextPosition2, MyAgent, EnemyAgent):
        AgentDataRaw = self.PositionRawUpdate(self.MyAgentData, self.EnemyAgentData\
                                            , NextPosition1, NextPosition2, MyAgent, EnemyAgent)
        FieldData = self.UpdatePosition(AgentDataRaw)
        return self.NetWork.predict(FieldData)

    def NextOut(self, NextMovable, MyAgent, EnemyAgent):
        """
        NextMovable data struct:
        [[[remove, pos1], [remove, pos2], ..., [remove, pos8]], [[remove, pos1], [remove, pos2], ..., [remove, pos8]]]
        *The length of the list may not be eight.
        """
        Evalutions = []
        Movables = []
        Move1, Move2 = NextMovable
        for NextMove1 in Move1:
            for NextMove2 in Move2:
                Evalutions.append(self.NextEvalution(NextMove1, NextMove2, MyAgent, EnemyAgent))
                Movables.append(NextMove1, NextMove2)
        EvalutionsArray = np.array(Evalutions)
        Next = Movables[EvalutionsArray.argmax()]
        self.log.LogWrite('Next position {}'.format(Next))
        self.log.LogWrite('Evalutions {}'.format(EvalutionsArray.argmax()))
        return Next

    def SavePickle(self, File_name='ProconNN.pkl'):
        with open(File_name, 'wb') as f:
            pickle.dump(self.params, f)

    def LoadPickle(self, File_name='ProconNN.pkl'):
        with open(File_name, 'rb') as f:
            self.params = pickle.load(f)

    def PositionCalc(self, Position):
        x, y = Position//1000-1, Position%1000-1
        point = self.AllPoint[y][x]
        return (x, y, point)

    def PositionRawUpdate(self, Mydata, Enemydata, pos1, pos2, MyAgent, EnemyAgent):
        AgentData = OrderedDict()
        Blank = []
        for x in range(self.x):
            for y in range(self.y):
                Blank.append(x*1000+y)
        My = Mydata
        Enemy = Enemydata
        My.GetPoint([pos1, pos2], Enemy)
        My.FieldPointSearch()
        Enemy.FieldPointSearch()
        MyAgent[0].next, MyAgent[1].next = pos1, pos2
        MyAgent[0].TurnSet()
        MyAgent[1].TurnSet()
        AgentData['MyGet'], AgentData['MyExist'], AgentData['MyFill'] = My.GetPosition, pos1[1], My.GetField
        AgentData['MyMovable'] = MyAgent[0].movable + MyAgent[1].movable
        AgentData['MyRemovable'] = MyAgent[0].removable + MyAgent[1].removable
        AgentData['EnemyGet'], AgentData['EnemyExist'], AgentData['EnemyFill'] = Enemy.GetPosition, pos1[1], Enemy.GetField
        AgentData['EnemyMovable'] = EnemyAgent[0].movable + EnemyAgent[1].movable
        AgentData['EnemyRemovable'] = EnemyAgent[0].removable + EnemyAgent[1].removable
        Blank = [i for i in Blank if i not in AgentData['MyGet'] + AgentData['MyExist'] + AgentData['MyFill']\
                                            + AgentData['MyMovable'] + AgentData['MyRemovable']\
                                            + AgentData['EnemyGet'] + AgentData['EnemyExist'] + AgentData['EnemyFill']\
                                            + AgentData['EnemyMovable'] + AgentData['EnemyRemovable']]
        AgentData['Blank'] = Blank
        return AgentData


    def UpdatePosition(self, AgentData):
        FieldData = []
        for i in range(11):
            temp = [[0 for i in range(12)] for j in range(12)]
            FieldData.append(temp)
        for i, item in AgentData.items():
            if str(AgentData[item]) == "<class 'list'>":
                for j in range(len(AgentData[item])):
                    x, y, point = self.PositionCalc(AgentData[item][j])
                    FieldData[i][x][y] = point
            else:
                x, y, point = self.PositionCalc(AgentData[item])
                FieldData[i][x][y] = point
        return FieldData

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
