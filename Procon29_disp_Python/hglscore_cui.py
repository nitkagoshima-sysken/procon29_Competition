# -*- coding: utf-8 -*-

from joblib import Parallel, delayed
from pro29NN.WindowControl import Flags
import argparse
import pro29NN
import random
import copy
import glob
import sys
import os

def LearnProcess(FieldAgent, start, log):
    paramsScore = {}
    TempData = copy.deepcopy(FieldAgent)
    network = pro29NN.ProconNetwork.Network()
    network2 = pro29NN.ProconNetwork.Network()
    network.load_params(file_name='gene/params{}.pkl'.format(100 if os.path.isfile('gene/params100.pkl') else random.randint(0, 99)))
    red_flags = Flags()
    controler = pro29NN.Bot.ProconNNControl(TempData['agentdatared'].AllPoint, log, network, red_flags)
    blue_flags = Flags()
    network2.load_params(file_name='gene/params{}.pkl'.format(start))
    controler2 = pro29NN.Bot.ProconNNControl(TempData['agentdatablue'].AllPoint, log, network2, blue_flags)
    score = Gaming(controler, controler2, red_flags, blue_flags, TempData)
    paramsScore[start] = [network2.params, score]
    return paramsScore

def Gaming(con, con2, flag1, flag2, TempData):
    for i in range(random.randint(40, 80)):
        con.NextSet(TempData['agentred'], TempData['agentblue'], TempData['agentdatared'], TempData['agentdatablue'], logout=False)
        con2.NextSet(TempData['agentblue'], TempData['agentred'], TempData['agentdatablue'], TempData['agentdatared'], logout=False)
        for i in range(2):
            for j in range(2):
                Overlap(i, j, flag1, flag2, TempData)
        TempData['agentdatablue'].GetPoint([TempData['agentblue'][0].next, TempData['agentblue'][1].next], TempData['agentdatared'])
        TempData['agentdatared'].GetPoint([TempData['agentred'][0].next, TempData['agentred'][1].next], TempData['agentdatablue'])
        TempData['agentdatablue'].FieldPointSearch()
        TempData['agentdatared'].FieldPointSearch()
        TempData['agentblue'][0].TurnSet(TempData['agentdatared'].GetPosition)
        TempData['agentblue'][1].TurnSet(TempData['agentdatared'].GetPosition)
        TempData['agentred'][0].TurnSet(TempData['agentdatablue'].GetPosition)
        TempData['agentred'][1].TurnSet(TempData['agentdatablue'].GetPosition)
    red_point = TempData['agentdatared'].Point + TempData['agentdatared'].TerritoryPoint
    blue_point = TempData['agentdatablue'].Point + TempData['agentdatablue'].TerritoryPoint
    return blue_point-red_point

def Overlap(i, j, flag1, flag2, TempData):
    if flag1.next[i] == flag2.next[j]:
        TempData['agentred'][i].next[1] = 0
        TempData['agentblue'][j].next[1] = 0

class LearnClassMain():
    def __init__(self):
        self.paramsnum = 100
        self.argv = sys.argv

    def SetLearn(self):
        parser = argparse.ArgumentParser()
        parser.add_argument('-s', '--no-parallel', action='store_true', help='Parallel option')
        parser.add_argument('-n', '--no-textfile', action='store_true', help='Field data select option')
        parser.add_argument('-t', '--threads', default=-1, type=int, help='Threads setting')
        parser.add_argument('-g', '--generations', default=40, type=int, help='Gene num setting')
        parser.add_argument('-f', '--field-data-directory', default='../../FieldDataGenerator', type=str, help='Field data exist directory select')
        parser.add_argument('-o', '--logfile', default='learn.log', type=str, help='Out logfile select')
        parser.add_argument('-d', '--gene-datas', default='gene', type=str, help='Gene data exist directory select')
        self.args = parser.parse_args()
        self.fieldatanum = 1
        log_file = self.args.logfile
        self.log = pro29NN.SystemControl.LogControl(log_file)
        self.Evo = pro29NN.Evolutionary.GeneManagement()
        if not os.path.isdir(self.args.gene_datas):
            os.mkdir(self.args.gene_datas)
        if not os.path.isfile(self.args.gene_datas+'/params0.pkl'):
            self.Evo.CreateGene()
        self.GeneNum = self.args.generations
        if self.args.no_textfile:
            path = self.args.field_data_directory
            self.FilePath = glob.glob(path+'/*.pqr')
        else:
            path = input('Field Files list: ')
            with open(path, 'r') as f:
                self.FilePath = [line.strip() for line in f.readlines()]
        self.FieldAgent = []
        for file_name in self.FilePath:
            self.fieldatanum += 1
            qrdata = pro29NN.Functions.OpenFile(file_name)
            self.FieldAgent.append(self.Setting(qrdata))
        self.StartLearn()

    def Setting(self, text):
        TempData = {}
        agent1next = [0, 0]
        agent2next = [0, 0]
        TempData['field'] = pro29NN.FieldControl.LearnField(text)
        TempData['agentdatablue'] = pro29NN.Agent.LaernAgentData('blue', TempData['field'].point)
        TempData['agentdatared'] = pro29NN.Agent.LaernAgentData('red', TempData['field'].point)
        pos1_y, pos1_x = map(int, text[TempData['field'].y+1].strip().split(' '))
        pos2_y, pos2_x = map(int, text[TempData['field'].y+2].strip().split(' '))
        fieldType = TempData['field'].FieldTypeAnalysis()
        agent1next[0], agent1next[1] = (pos1_x*1000)+pos1_y, (pos2_x*1000)+pos2_y
        if fieldType == 0:
            agent2next[0], agent2next[1] = (TempData['field'].x-pos1_x+1)*1000+(TempData['field'].y-pos1_y+1), (TempData['field'].x-pos2_x+1)*1000+(TempData['field'].y-pos2_y+1)
            if agent1next[0] in agent2next or agent1next[1] in agent2next:
                agent2next[0], agent2next[1] = pos1_x*1000+(TempData['field'].y-pos1_y+1), pos2_x*1000+(TempData['field'].y-pos2_y+1)
            if agent1next[0] in agent2next or agent1next[1] in agent2next:
                agent2next[0], agent2next[1] = (TempData['field'].x-pos1_x+1)*1000+pos1_y, (TempData['field'].x-pos2_x+1)*1000+pos2_y
        elif fieldType == -1:
            agent2next[0], agent2next[1] = pos1_x*1000+(TempData['field'].y-pos1_y+1), pos2_x*1000+(TempData['field'].y-pos2_y+1)
        elif fieldType == 1:
            agent2next[0], agent2next[1] = (TempData['field'].x-pos1_x+1)*1000+pos1_y, (TempData['field'].x-pos2_x+1)*1000+pos2_y
        TempData['agentblue'] = []
        TempData['agentred'] = []
        TempData['agentblue'].append(pro29NN.Agent.LearnAgent(agent1next[0], TempData['field'].field_out))
        TempData['agentblue'].append(pro29NN.Agent.LearnAgent(agent1next[1], TempData['field'].field_out))
        TempData['agentred'].append(pro29NN.Agent.LearnAgent(agent2next[0], TempData['field'].field_out))
        TempData['agentred'].append(pro29NN.Agent.LearnAgent(agent2next[1], TempData['field'].field_out))
        TempData['agentdatablue'].GetPoint([TempData['agentblue'][0].next, TempData['agentblue'][1].next], TempData['agentdatared'])
        TempData['agentdatared'].GetPoint([TempData['agentred'][0].next, TempData['agentred'][1].next], TempData['agentdatablue'])
        TempData['agentblue'][0].TurnSet(TempData['agentdatared'].GetPosition)
        TempData['agentblue'][1].TurnSet(TempData['agentdatared'].GetPosition)
        TempData['agentred'][0].TurnSet(TempData['agentdatablue'].GetPosition)
        TempData['agentred'][1].TurnSet(TempData['agentdatablue'].GetPosition)
        return TempData

    def StartLearn(self):
        for i in range(self.GeneNum):
            print('{} generation start'.format(i))
            self.log.LogWrite('{} generation start\n'.format(i), logtype=pro29NN.LEARN)
            paramsScores = []
            paramsScore = [0 for ii in range(self.paramsnum)]
            params = []
            jj = 0
            for FieldAgent in self.FieldAgent:
                if self.args.no_parallel:
                    paratemp = []
                    for n in range(self.paramsnum):
                        paratemp += [LearnProcess(FieldAgent, n, self.log)]
                    paramsScores += paratemp
                else:
                    paramsScores += [Parallel(n_jobs=self.args.threads)([delayed(LearnProcess)(FieldAgent, n, self.log) for n in range(self.paramsnum)])]
                self.log.LogWrite('Finished {} FieldFile\n'.format(jj), logtype=pro29NN.LEARN)
                jj += 1
            temp = {}
            for j in range(self.fieldatanum-1):
                for k in range(self.paramsnum):
                    items = list(paramsScores[j][k].items())
                    paramsScore[items[0][0]] += items[0][1][1]
                    temp[items[0][0]] = items[0][1]
            for num in range(len(paramsScore)):
                params.append([temp[num][0], paramsScore[num] / self.fieldatanum])
            self.Evo.SelectGene(params)

if __name__=='__main__':
    Learn = LearnClassMain()
    Learn.SetLearn()