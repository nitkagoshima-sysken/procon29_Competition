# -*- coding: utf-8 -*-

from pyzbar.pyzbar import decode
from PIL import Image
from wx import adv
import wx
import os
import sys


def OpenFile(file):
    if '.png' in file:
        data = decode(Image.open(file))
        text = data[0][0].decode('utf-8', 'ignore')
        return text.split(':')
    elif '.pqr' in file:
        f = open(file, 'r')
        text = f.read()
        return text.replace('\n','').split(':')
    else:
        raise FormatError(file)

class FormatError(Exception):
    pass
