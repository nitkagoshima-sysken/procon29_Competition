from pyzbar.pyzbar import decode
from PIL import Image
import os

# QRコード(QRcode.png)の指定
image = input('File:')
# QRコードの読取り
data = decode(Image.open(image))
# コード内容テキストファイル(output.txt)に出力
f = open('output.csv','w')
# utf-8にデコードして出力
outt = data[0][0].decode('utf-8', 'ignore')
#outt = text.replace(' ',',')
outt = outt.split(':')
for i in range(len(outt)-1):
    f.write(outt[i].strip().replace(' ',',')+'\n')
    print(outt[i].strip().replace(' ',','))
f.close()