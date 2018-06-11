from pyzbar.pyzbar import decode
from PIL import Image
import os

image = input('File:')
data = decode(Image.open(image))
f = open('output.csv','w')
outt = data[0][0].decode('utf-8', 'ignore')
outt = outt.split(':')
for i in range(len(outt)-1):
    f.write(outt[i].strip().replace(' ',',')+'\n')
    print(outt[i].strip().replace(' ',','))
f.close()
