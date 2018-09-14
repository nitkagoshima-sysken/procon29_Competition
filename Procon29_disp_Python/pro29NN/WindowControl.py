# -*- coding: utf-8 -*-

from . import Bot
from . import ProconNetwork
from . import Evolutionary


class Flags():
    """
    Color flag class.
    """
    def __init__(self):
        self.flag = [True, True]
        self.turn = 0
        self.end = False
        self.next = [0,0]
    
    def Clear(self):
        self.flag = [True, True]
        self.turn = 0
        self.end = False

class Modes():
    """
    Mode select class.
    """
    def __init__(self):
        self.manual = False
        self.auto = False
        self.battle = False
        self.learn = False
        self.AutoSetFlag = False
        self.LearnSetFlag = False

    def Clear(self):
        self.manual = False
        self.auto = False
        self.battle = False
        self.learn = False
        self.AutoSetFlag = False
        self.LearnSetFlag = False

    def AutoSet(self, log, MyagentData, EnemyagentData, agent, flags):
        self.Clear()
        network = ProconNetwork.Network()
        network.load_params(file_name='gene/params100.pkl')
        self.bot = Bot.ProconNNControl(MyagentData.AllPoint, log, network, flags)
        self.auto = True
        self.AutoSetFlag = True

    def LearnSet(self, log):
        self.Clear()
        self.learn = True
        self.LearnSetFlag = True
