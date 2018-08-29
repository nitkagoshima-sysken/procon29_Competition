# -*- coding: utf-8 -*-

from . import Bot
from . import ProconNetwork
import wx

class SelectDialog(wx.Dialog):
    """
    Dialog fo select class.
    parent : Select parent object. Default=None
    id : Object id set. Default=wx.ID_ANY
    title : Dialog title setting.
    text : Dialog out string.
    size : Dialog size setting.
    """
    def __init__(self, parent=None, id=wx.ID_ANY, title='', text='', size=(400, 100)):
        wx.Dialog.__init__(self, parent, id, title, size=size)
        self.text = wx.StaticText(self, -1, text,style=wx.SIMPLE_BORDER)
        button1 = wx.Button(self, wx.ID_OK, '1')
        button1.SetDefault()
        button2 = wx.Button(self, wx.ID_CANCEL, '2')

        button_sizer = wx.StdDialogButtonSizer()
        button_sizer.AddButton(button1)
        button_sizer.AddButton(button2)
        button_sizer.Realize()

        sizer = wx.BoxSizer(wx.VERTICAL)
        sizer.Add(self.text, 1, wx.EXPAND | wx.ALL, 3)
        sizer.Add(button_sizer, 0, wx.EXPAND | wx.ALL, 5)
        self.SetSizer(sizer)

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
        self.bot = Bot.ProconNNControl(MyagentData, EnemyagentData, log, network)
        self.auto = True
        self.AutoSetFlag = True

    def LearnSet(self, log, field, agent, flags):
        self.Clear()
        self.bot = [Bot.FakeBot(log, field, agent[0].now, flags[0]),\
                    Bot.FakeBot(log, field, agent[1].now, flags[0]),\
                    Bot.FakeBot(log, field, agent[2].now, flags[1]),\
                    Bot.FakeBot(log, field, agent[3].now, flags[1])]
        self.learn = True
        self.LearnSetFlag = True

class FormatError(Exception):
    pass
