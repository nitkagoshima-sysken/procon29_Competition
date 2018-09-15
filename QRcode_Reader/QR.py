# -*- coding: utf-8 -*-

from pyzbar.pyzbar import decode
from PIL import Image
import os
import cv2

cap = cv2.VideoCapture(0)

while(True):
    ret, frame = cap.read()

    cv2.imshow('frame', frame)

    key = cv2.waitKey(1) & 0xFF

    if key == ord('q'):
        break
    if key == ord('s') or key == ord(' '):
        path = 'photo.png'
        cv2.imwrite(path, frame)
        break


# QRコード(QRcode.png)の指定
image = 'photo.png'
# QRコードの読取り
data = decode(Image.open(image))
# コード内容テキストファイル(output.txt)に出力
f = open('output.pqr','w')
# utf-8にデコードして出力
outt = data[0][0].decode('utf-8', 'ignore')
#outt = text.replace(' ',',')
outt = outt.split(':')
for i in range(len(outt)-1):
    f.write(outt[i].strip()+':'+'\n')
    print(outt[i].strip()+':')

f.close()