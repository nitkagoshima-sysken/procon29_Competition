# -*- coding:utf-8 -*-

from datetime import datetime
import procon29

type_dic = {procon29.GAME_LOG:'[Game Log] ',\
            procon29.SYSTEM_LOG:'[System Log] ',\
            procon29.FILE_LOG:'[File Log] ',\
            procon29.ERROR:'[Error] ',\
            procon29.DEBAG:'[Debag] '}

class LogControl:
    def __init__(self, file_name):
        self.name = file_name

    def LogWrite(self, write_text, logtype=procon29.GAME_LOG, write_type='a'):
        log_file = open(self.name, write_type)
        log_file.write(datetime.now().strftime("[%Y/%m/%d %H:%M:%S]")+type_dic[logtype]+write_text)
        log_file.close()
