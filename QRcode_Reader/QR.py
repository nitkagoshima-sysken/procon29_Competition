# -*- coding: utf-8 -*-

from pyzbar.pyzbar import decode
from PIL import Image
import os

data = decode(Image.open(input('File:')))
f = open('output.pqr','w')
print(data)
string = data[0][0].decode('utf-8', 'ignore')
f.write(string.strip())
f.close()