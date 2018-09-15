# -*- coding: utf-8 -*-

import pickle
import pro29NN
import copy
from . import ProconNetwork
from . import Agent
import numpy as np
from collections import OrderedDict

class ProconNNControl:
    """
    Neural network control class.
    """
    def __init__(self, AllPoint, log, network, flags):
        self.AllPoint = AllPoint
        self.x, self.y = len(self.AllPoint[0]), len(self.AllPoint)
        self.params = {}
        self.log = log
        self.NetWork = network
        self.flags = flags

    def NextEvalution(self, NextPosition1, NextPosition2, MyAgent, EnemyAgent, MyAgetnData, EnemyAgentData):
        AgentDataRaw = self.PositionRawUpdate(MyAgetnData, EnemyAgentData\
                                            , NextPosition1, NextPosition2, MyAgent, EnemyAgent)
        FieldData = self.UpdatePosition(AgentDataRaw)
        FieldDataArray = np.array(FieldData)
        return self.NetWork.predict(FieldDataArray.reshape(-1, 11, 12, 12))

    def NextSet(self, MyAgent, EnemyAgent, MyAgetnData, EnemyAgentData, logout=True):
        """

        """
        Move1 = [[False, pos] for pos in MyAgent[0].movable] + [[True, pos] for pos in MyAgent[0].removable]
        Move2 = [[False, pos] for pos in MyAgent[1].movable] + [[True, pos] for pos in MyAgent[1].removable]
        Evalutions = []
        Movables = []
        for NextMove1 in Move1:
            for NextMove2 in Move2:
                Movables.append([copy.deepcopy(NextMove1), copy.deepcopy(NextMove2)])
                Evalutions.append(self.NextEvalution(NextMove1, NextMove2, MyAgent, EnemyAgent, MyAgetnData, EnemyAgentData))
                if logout: print('Pos {}: Eva{}'.format(Movables[-1], Evalutions[-1]))
        EvalutionsArray = np.array(Evalutions)
        Next = Movables[EvalutionsArray.argmax()]
        Eva = EvalutionsArray[EvalutionsArray.argmax()]
        if logout:
            self.log.LogWrite('Next position {}\n'.format(Next))
            self.log.LogWrite('Evalutions {}\n'.format(Eva), logtype=pro29NN.SYSTEM_LOG)
        if logout: print('Best Eva: {}'.format(Eva))
        MyAgent[0].NextSet(Next[0][1], overlap=Next[0][0])
        MyAgent[1].NextSet(Next[1][1], overlap=Next[1][0])
        self.flags.next[0] = Next[0][1]
        self.flags.next[1] = Next[1][1]

    def SavePickle(self, File_name='ProconNN.pkl'):
        with open(File_name, 'wb') as f:
            pickle.dump(self.params, f)

    def LoadPickle(self, File_name='ProconNN.pkl'):
        with open(File_name, 'rb') as f:
            self.params = pickle.load(f)

    def PositionCalc(self, Position):
        x, y = Position//1000-1, Position%1000-1
        point = self.AllPoint[y][x] + 17
        return (x, y, point)

    def PositionRawUpdate(self, Mydata, Enemydata, pos1, pos2, MyAgent, EnemyAgent):
        AgentData = OrderedDict()
        Blank = []
        for x in range(self.x):
            for y in range(self.y):
                Blank.append(x*1000+y)
        My = Agent.TempAgentData(Mydata.AllPoint, Mydata, MyAgent)
        Enemy = Agent.TempAgentData(Mydata.AllPoint, Enemydata, EnemyAgent)
        My.GetPoint([pos1, pos2], Enemy)
        My.FieldPointSearch()
        Enemy.FieldPointSearch()
        My.TurnSet(Enemy.GetPosition, [pos1, pos2])
        Enemy.TurnSet(My.GetPosition, [[True, 0], [True, 0]])
        AgentData['MyGet'], AgentData['MyExist'], AgentData['MyFill'] = My.GetPosition, [pos1[1],], My.GetField
        AgentData['MyMovable'] = My.Agent[0].movable + My.Agent[1].movable
        AgentData['MyRemovable'] = My.Agent[0].removable + My.Agent[1].removable
        AgentData['EnemyGet'], AgentData['EnemyExist'], AgentData['EnemyFill'] = Enemy.GetPosition, [pos1[1],], Enemy.GetField
        AgentData['EnemyMovable'] = Enemy.Agent[0].movable + Enemy.Agent[1].movable
        AgentData['EnemyRemovable'] = Enemy.Agent[0].removable + Enemy.Agent[1].removable
        Blank = [i for i in Blank if i not in AgentData['MyGet'] + [AgentData['MyExist'],] + AgentData['MyFill']\
                                            + AgentData['MyMovable'] + AgentData['MyRemovable']\
                                            + AgentData['EnemyGet'] + [AgentData['EnemyExist'],] + AgentData['EnemyFill']\
                                            + AgentData['EnemyMovable'] + AgentData['EnemyRemovable']]
        AgentData['Blank'] = Blank
        return AgentData

    def UpdatePosition(self, AgentData):
        FieldData = []
        for key, val in AgentData.items():
            temp = [[0 for i in range(12)] for j in range(12)]
            FieldData.append(temp)
            for j in range(len(val)):
                x, y, point = self.PositionCalc(val[j])
                FieldData[-1][x][y] = point
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
        else:
            agent.NextSet(pos)
        self.flags.next[num] = pos
