# -*- coding: utf-8 -*-

from pro29NN import Agent, WindowControl, SystemControl, Bot, ProconNetwork
import argparse

def pointSwap(point):
    Returnpoint = []
    for i in range(len(point[0])):
        Returnpoint.append([])
        for j in range(len(point)):
            Returnpoint[i].append(point[j][i])
    return Returnpoint
            

def replasefunc(fline, num):
    ReturnList = []
    fline[num] = fline[num].replace('{', '')
    fline[num] = fline[num].replace('},', ':')
    fline[num] = fline[num].replace('}\n', '')
    strings = list(map(str, fline[num].split(':')))
    for string in strings:
        ReturnList.append(list(map(int, string.strip().split(','))))
    return ReturnList

def make_agentdata(file):
    agents = []
    MyGet = []
    EnemyGet = []
    field_out = []
    field_size = []
    with open(file, 'r') as f:
        fline = f.readlines()
    point = replasefunc(fline, 0)
    point = pointSwap(point)
    print(point)
    x, y = len(point[0]), len(point)
    for i in range(y+2):
        for j in range(x+2):
            field_out.append(j*1000+i)
            if i != 0 and j != 0 and i != y+1 and j != x+1:
                field_size.append(j*1000+i)
    field_out = [item for item in field_out if item not in field_size]
    Now = replasefunc(fline, 1)
    agent0 = Agent.TempAgent((Now[0][0]+1)*1000+Now[0][1]+1, field_out)
    agent1 = Agent.TempAgent((Now[1][0]+1)*1000+Now[1][1]+1, field_out)
    EnemyNow = replasefunc(fline, 2)
    agent2 = Agent.TempAgent((EnemyNow[0][0]+1)*1000+EnemyNow[0][1]+1, field_out)
    agent3 = Agent.TempAgent((EnemyNow[1][0]+1)*1000+EnemyNow[1][1]+1, field_out)
    MyGetpositions = replasefunc(fline, 3)
    for data in MyGetpositions:
        MyGet.append((data[0]+1)*1000+data[1]+1)
    MyagentData = Agent.AgentData(None, None, point)
    MyagentData.GetPosition = MyGet
    EnemyGetpositions = replasefunc(fline, 4)
    for data in EnemyGetpositions:
        EnemyGet.append((data[0]+1)*1000+data[1]+1)
    EnemyagentData = Agent.AgentData(None, None, point)
    EnemyagentData.GetPosition = EnemyGet
    agents.append(Agent.TempAgentData(point, MyagentData, [agent0, agent1]))
    agents.append(Agent.TempAgentData(point, EnemyagentData, [agent2, agent3]))
    agents.append([agent0, agent1, agent2, agent3])
    agents.append(point)
    return agents


if __name__=='__main__':
    nextList = []
    parser = argparse.ArgumentParser()
    parser.add_argument('-o', '--outfile', default='object.out', type=str, help='Output file name setting')
    parser.add_argument('-i', '--infile', default='object.in', type=str, help='Input file name setting')
    args = parser.parse_args()
    agent = make_agentdata(args.infile)
    network = ProconNetwork.Network()
    network.load_params('gene/params100.pkl')
    flags = WindowControl.Flags()
    log = SystemControl.LogControl('bot.log')
    bot = Bot.ProconNNControl(agent[3], log, network, flags)
    agent[2][0].movable, agent[2][0].removable = agent[2][0].MovableSet(agent[1].GetPosition)
    agent[2][1].movable, agent[2][1].removable = agent[2][1].MovableSet(agent[1].GetPosition)
    agent[2][2].movable, agent[2][2].removable = agent[2][2].MovableSet(agent[0].GetPosition)
    agent[2][3].movable, agent[2][3].removable = agent[2][3].MovableSet(agent[0].GetPosition)
    bot.NextSet([agent[2][0], agent[2][1]], [agent[2][2], agent[2][3]], agent[0], agent[1])
    for i in flags.next:
        nextList.append([i//1000-1, i%1000-1])
    print(nextList)
    with open(args.outfile, 'w') as f:
        f.write(str(nextList))