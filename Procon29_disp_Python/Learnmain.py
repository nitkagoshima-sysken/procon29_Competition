# -*- coding: utf-8 -*-

from kivy.uix.screenmanager import ScreenManager, Screen
from kivy.properties import StringProperty
from kivy.uix.widget import Widget
from kivy.lang import Builder
from pro29NN.WindowControl import *
from kivy.app import App
from threading import Thread
import subprocess
import pro29NN
import Learn
import random
import copy
import wx
import os

from kivy.resources import resource_add_path
import sys

if hasattr(sys, "_MEIPASS"):
    resource_add_path(sys._MEIPASS)


class LearnClass(Widget):
    def __init__(self, **kwargs):
        super(LearnClass, self).__init__(**kwargs)
        self.text = ''

    def buttonClicked(self):
        app = wx.App()
        learn = Learn.LearnClassMain()
        t1 = Thread(target=learn.SetLearn())
        t1.start()
        '''
        if os.path.isfile('Learn.exe'):
            proc = subprocess.Popen(['Learn.exe'])
        else:
            proc = subprocess.Popen(['python', 'Learn.py'])
        proc.communicate()
        '''
        self.text = 'Hello World'

        return self.text

    def SetWindow(self):
        from kivy.core.window import Window
        Window.size = (1600, 900)
        HGLSApp().run()

class HGLSApp(App):
    def __init__(self, **kwargs):
        super(HGLSApp, self).__init__(**kwargs)
        self.title = '水素でGo 学習システム'

    def build(self):
        return LearnClass()

if __name__=='__main__':
    app = wx.App()
    LearnWindow = LearnClass()
    LearnWindow.SetWindow()