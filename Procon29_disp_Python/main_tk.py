# -*- coding: utf-8 -*-

from pyzbar.pyzbar import decode
from PIL import Image
import configparser as config
from concurrent.futures import ThreadPoolExecutor
import time
import procon29
import os
from tkinter import *
import hashlib
import sys

class Test(Frame):
    def __init__(self, master = None):
        Frame.__init__(self, master)
        self.pack()
        self.init()
    
    def init(self):
        self.buff = StringVar()
        self.buff.set('')
        label = Label(self, textvariable = self.buff)
        label.pack()
        for x in range(4):
            button = Button(self, text='test button {}'.format(x), command=self.make_cmd(x))
            button.pack()

    def make_cmd(self, n):
        return lambda : self.buff.set('button {} pressed'.format(n))

app = Test()
app.mainloop()