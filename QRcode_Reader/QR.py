from pyzbar.pyzbar import decode
from PIL import Image
import os

# QRコード(QRcode.png)の指定
image = input('File:')
# QRコードの読取り
data = decode(Image.open(image))
# コード内容テキストファイル(output.txt)に出力
f = open('output.txt','w')
# utf-8にデコードして出力
text = data[0][0].decode('utf-8', 'ignore')
outt = text.replace(' ',',')
f.write(outt)
print(outt)
f.close()