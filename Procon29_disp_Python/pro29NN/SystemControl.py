# -*- coding:utf-8 -*-

from datetime import datetime
import pro29NN

type_dic = {pro29NN.GAME_LOG:'[Game Log] ',\
            pro29NN.SYSTEM_LOG:'[System Log] ',\
            pro29NN.FILE_LOG:'[File Log] ',\
            pro29NN.ERROR:'[Error] ',\
            pro29NN.DEBAG:'[Debag] ',\
            pro29NN.LEARN:'[Learn log] '}

class LogControl:
    def __init__(self, file_name):
        self.name = file_name

    def LogWrite(self, write_text, logtype=pro29NN.GAME_LOG, write_type='a'):
        with open(self.name, write_type) as f:
            f.write(datetime.now().strftime("[%Y/%m/%d %H:%M:%S] ")+type_dic[logtype]+write_text)
