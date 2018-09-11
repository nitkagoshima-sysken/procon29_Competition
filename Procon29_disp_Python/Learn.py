# -*- coding: utf-8 -*-

from pro29NN.WindowControl import *
import concurrent.futures
import pro29NN
import mypool
import random
import copy
import wx
import os


class LearnClassMain():
    def __init__(self):
        pass

    def SetLearn(self):
        self.fieldatanum = 1
        self.log = pro29NN.SystemControl.LogControl('learn.log')
        self.Evo = pro29NN.Evolutionary.GeneManagement()
        if not os.path.isfile('gene/params0.pkl'):
            self.Evo.CreateGene()
        GeneDialog = wx.TextEntryDialog(None, '世代数を入力してください', '世代数設定')
        GeneDialog.SetValue('40')
        GeneDialog.ShowModal()
        self.GeneNum = int(GeneDialog.GetValue())
        GeneDialog.Destroy()
        self.FieldAgent = []
        FileDialog = wx.FileDialog(None, 'Select File', style=wx.FD_MULTIPLE)
        FileDialog.SetWildcard('*.png;*.pqr')
        FileDialog.ShowModal()
        self.FilePath = FileDialog.GetPaths()
        for file in self.FilePath:
            self.fieldatanum += 1
            qrdata = pro29NN.Functions.OpenFile(file)
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
                #red_Flags.next[0], red_Flags.next[1] = (agent1_x)*1000+(field[0].y-agent1_y+1), (agent2_x)*1000+(field[0].y-agent2_y+1)
            if agent1next[0] in agent2next or agent1next[1] in agent2next:
                agent2next[0], agent2next[1] = (TempData['field'].x-pos1_x+1)*1000+pos1_y, (TempData['field'].x-pos2_x+1)*1000+pos2_y
                #red_Flags.next[0], red_Flags.next[1] = (field[0].x-agent1_x+1)*1000+(agent1_y), (field[0].x-agent2_x+1)*1000+(agent2_y)
        elif fieldType == -1:
            agent2next[0], agent2next[1] = pos1_x*1000+(TempData['field'].y-pos1_y+1), pos2_x*1000+(TempData['field'].y-pos2_y+1)
            #red_Flags.next[0], red_Flags.next[1] = (agent1_x)*1000+(field[0].y-agent1_y+1), (agent2_x)*1000+(field[0].y-agent2_y+1)
        elif fieldType == 1:
            agent2next[0], agent2next[1] = (TempData['field'].x-pos1_x+1)*1000+pos1_y, (TempData['field'].x-pos2_x+1)*1000+pos2_y
            #red_Flags.next[0], red_Flags.next[1] = (field[0].x-agent1_x+1)*1000+(agent1_y), (field[0].x-agent2_x+1)*1000+(agent2_y)
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
            self.log.LogWrite('{} generation start\n'.format(i), logtype=pro29NN.LEARN)
            paramsScores = []
            paramsScore = [0 for ii in range(100)]
            for FieldAgent in self.FieldAgent:
                paramsScores.append(self.LearnProsess(FieldAgent))
            for j in range(self.fieldatanum):
                for k in range(100):
                    paramsScore = paramsScores[j][k][1]
            k = 0
            for num in paramsScore:
                paramsScores[i][1] = num / self.fieldatanum
                k += 1
            self.Evo.SelectGene(paramsScores)
    
    def LearnProsess(self, FieldAgent):
        paramsScore = []
        InitData = copy.deepcopy(FieldAgent)
        TempData = copy.deepcopy(FieldAgent)
        network = pro29NN.ProconNetwork.Network()
        network2 = pro29NN.ProconNetwork.Network()
        network.load_params(file_name='gene/params{}.pkl'.format(100 if os.path.isfile('gene/params100.pkl') else random.randint(0, 99)))
        red_flags = Flags()
        
        for i in range(100):
            self.log.LogWrite('openfile pickle file params{}\n'.format(i), logtype=pro29NN.LEARN)
            controler = pro29NN.Bot.ProconNNControl(TempData['agentdatared'].AllPoint, self.log, network, red_flags)
            blue_flags = Flags()
            network2.load_params(file_name='gene/params{}.pkl'.format(i))
            controler2 = pro29NN.Bot.ProconNNControl(TempData['agentdatablue'].AllPoint, self.log, network2, blue_flags)
            score = self.Gaming(controler, controler2, red_flags, blue_flags, TempData)
            paramsScore.append([network2.params, score])
            TempData = copy.deepcopy(InitData)
        return paramsScore

    def Gaming(self, con, con2, flag1, flag2, TempData):
        for i in range(random.randint(100, 120)):
            con.NextSet(TempData['agentred'], TempData['agentblue'], TempData['agentdatared'], TempData['agentdatablue'], logout=False)
            con2.NextSet(TempData['agentblue'], TempData['agentred'], TempData['agentdatablue'], TempData['agentdatared'], logout=False)
            for i in range(2):
                for j in range(2):
                    self.Overlap(i, j, flag1, flag2, TempData)
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
    
    def Overlap(self, i, j, flag1, flag2, TempData):
        if flag1.next[i] == flag2.next[j]:
            TempData['agentred'][i].next[1] = 0
            TempData['agentblue'][j].next[1] = 0

if __name__=='__main__':
    app = wx.App()
    Learn = LearnClassMain()
    Learn.SetLearn()