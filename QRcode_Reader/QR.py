# -*- coding: utf-8 -*-

from pyzbar.pyzbar import decode
from PIL import Image
import os
import cv2

cap = cv2.VideoCapture(0)

def decodes():
    image = 'photo.png'
    data = decode(Image.open(image))
    try:
        outt = data[0][0].decode('utf-8', 'ignore')
    except:
        os.remove(image)
        return False
    outt = outt.split(':')
    y, _ = outt[0].strip().split(' ')
    outt.remove('')
    print(outt)
    if len(outt) == int(y)+3:
        with open('output.pqr','w') as f:
            for i in range(len(outt)):
                f.write(outt[i].strip()+':'+'\n')
                print(outt[i].strip()+':')
        Flag = True
    else:
        Flag = False
    os.remove(image)
    return Flag

while(True):
    ret, frame = cap.read()
    cv2.imshow('frame', frame)
    key = cv2.waitKey(1) & 0xFF
    if key == ord('q'):
        break
    if key == ord('s') or key == ord(' '):
        path = 'photo.png'
        cv2.imwrite(path, frame)
        if decodes():
            break

