# -*- coding: utf-8 -*-

from . import ProconNetwork
from operator import attrgetter
import pickle
import random
import copy

SelectDic = {'Tournament':0,
            'Roulette':1,
            'Ranking':2,
            'Elite':3}

def ParamsClassCreate(prams):
    return ParamsData(prams[0], prams[1])

def swap(num1, num2):
    temp = num1
    num1 = num2
    num2 = temp
    return (num1, num2)

class ParamsData:
    def __init__(self, params, score):
        self.params = {}
        self.params = copy.deepcopy(params)
        self.score = score

class GeneManagement:
    def __init__(self, GeneNum=100, CXPB=0.5, MUTPB=0.2, MUTINDPB=0.05,
                SelectMethod='Tournament'):
        self.GeneNum = GeneNum
        self.CXPB = CXPB
        self.MUTPB = MUTPB
        self.MUTINDPB = MUTINDPB
        self.SelectMethod = SelectDic[SelectMethod]
    
    def GeneSeedSelect(self, Gene1, Gene2):
        self.GeneSeed = []
        self.GeneSeed.append(Gene1)
        self.GeneSeed.append(Gene2)

    def CreateGene(self, Seed=False):
        if Seed:
            self.GeneSeedSelect('gene/params0.pkl', 'gene/params1.pkl')
        else:
            for i in range(self.GeneNum):
                Network = ProconNetwork.Network()
                Network.save_params(file_name='gene/params{}.pkl'.format(i))

    def SelectGene(self, paramsdata):
        if self.SelectMethod == 0:
            params = list(map(ParamsClassCreate, paramsdata))
            self.SaveParams(max(params, key=attrgetter('score')), FileName='params100.pkl')
            NextGeneration = self.SelectTournament(params)
        elif self.SelectMethod == 1:
            pass
        elif self.SelectMethod == 2:
            pass
        
        CrossOver = []
        for child1, child2 in zip(NextGeneration[::2], NextGeneration[1::2]):
            child1, child2 = self.UniformCross(child1, child2)
            CrossOver.append(child1)
            CrossOver.append(child2)
        NextGeneration = copy.deepcopy(CrossOver)
        
        Mutant = []
        for child in NextGeneration:
            if random.random() < self.MUTPB:
                child = self.MutantGenerate(child)
            Mutant.append()
        NextGeneration = copy.deepcopy(Mutant)

        i = 0
        for child in NextGeneration:
            self.SaveParams(child.params, FileName='params{}.pkl'.format(i))
            i += 1
        
    def SelectTournament(self, params, toursize=3):
        choice = []
        for i in range(self.GeneNum):
            select = [random.choice(params) for j in range(toursize)]
            choice.append(max(select, key=attrgetter('score')))
        return choice

    def UniformCross(self, data1, data2):
        child1 = copy.deepcopy(data1)
        child2 = copy.deepcopy(data2)
        for key, val in child1.params.items():
            shape = val.shape
            if len(shape) == 1:
                for i in range(shape[0]):
                    if random.random() < self.CXPB:
                        child1.params[key][i], child2.params[key][i] = swap(child1.params[key][i], child2.params[key][i])
            elif len(shape) == 2:
                for i in range(shape[0]):
                    for j in range(shape[1]):
                        if random.random() < self.CXPB:
                            child1.params[key][i][j], child2.params[key][i][j] = swap(child1.params[key][i][j], child2.params[key][i][j])
            elif len(shape) == 3:
                for i in range(shape[0]):
                    for j in range(shape[1]):
                        for k in range(shape[2]):
                            if random.random() < self.CXPB:
                                child1.params[key][i][j][k], child2.params[key][i][j][k] = swap(child1.params[key][i][j][k], child2.params[key][i][j][k])
            elif len(shape) == 4:
                for i in range(shape[0]):
                    for j in range(shape[1]):
                        for k in range(shape[2]):
                            for l in range(shape[3]):
                                if random.random() < self.CXPB:
                                    child1.params[key][i][j][k][l], child2.params[key][i][j][k][l] = swap(child1.params[key][i][j][k][l], child2.params[key][i][j][k][l])
        return (child1, child2)
    
    def MutantGenerate(self, data):
        child = copy.deepcopy(data)
        for key, val in child.params.items():
            shape = val.shape
            if len(shape) == 1:
                for i in range(shape[0]):
                    if random.random() < self.CXPB:
                        child.params[key][i] = 0.01 * random.random()
            elif len(shape) == 2:
                for i in range(shape[0]):
                    for j in range(shape[1]):
                        if random.random() < self.CXPB:
                            child.params[key][i][j] = 0.01 * random.random()
            elif len(shape) == 3:
                for i in range(shape[0]):
                    for j in range(shape[1]):
                        for k in range(shape[2]):
                            if random.random() < self.CXPB:
                                child.params[key][i][j][k] = 0.01 * random.random()
            elif len(shape) == 4:
                for i in range(shape[0]):
                    for j in range(shape[1]):
                        for k in range(shape[2]):
                            for l in range(shape[3]):
                                if random.random() < self.CXPB:
                                    child.params[key][i][j][k][l] = 0.01 * random.random()
        return child

    def SaveParams(self, Data, FileName='params.pkl'):
        params = {}
        for key, val in Data.params.items():
            params[key] = val
        with open('gene'+FileName, 'wb') as f:
            pickle.dump(params, f)

if __name__=='__main__':
    gene = GeneManagement()
    gene.CreateGene()