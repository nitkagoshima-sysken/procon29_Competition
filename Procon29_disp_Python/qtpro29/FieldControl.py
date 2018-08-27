# -*- coding: utf-8 -*-
import pickle
from . import ProconNetwork
import numpy as np

class ProconNNControl:
    def __init__(self, network):
        self.FieldData = {}
        self.FieldData['Blank'] = None
        self.FieldData['BlueGet'], self.FieldData['BlueExist'], self.FieldData['BlueFill'] = None, None, None
        self.FieldData['RedGet'], self.FieldData['RedExist'], self.FieldData['RedFill'] = None, None, None
        self.params = {}
        self.NetWork = network

    def DataInput(self, PointData):
        self.FieldData['Blank'] = PointData['Blank']
        self.FieldData['BlueGet'], self.FieldData['BlueExist'], self.FieldData['BlueFill'] = PointData['Blue']
        self.FieldData['RedGet'], self.FieldData['RedExist'], self.FieldData['RedFill'] = PointData['Red']

    def NextEvalution(self, NextPosition):
        FieldData = self.FieldData
        FieldData['BlueGet'] = self.AddPosition(NextPosition, FieldData['BlueGet'])
        return self.NetWork.predict(FieldData)

    def NextOut(self, NextMovable):
        Evalutions = []
        Movables = []
        for NextMove in NextMovable:
            Evalutions.append(self.NextEvalution(NextMove))
            Movables.append(NextMove)
        EvalutionsArray = np.array(Evalutions)
        return Movables[EvalutionsArray.argmax()]

    def SavePickle(self, File_name='ProconNN.pkl'):
        with open(File_name, 'wb') as f:
            pickle.dump(self.params, f)

    def LoadPickle(self, File_name='ProconNN.pkl'):
        with open(File_name, 'rb') as f:
            self.params = pickle.load(f)

    def AddPosition(self, Position, NowPositionData):
        x, y, Point = self.PositionCalc(Position)
        NowPositionData[x][y] = Point
        return NowPositionData

    def DelPosition(self, Position, NowPositionData):
        x, y, _ = self.PositionCalc(Position)
        NowPositionData[x][y] = 0
        return NowPositionData

    def PositionCalc(self, Position):
        pass


class Field:
    def __init__(self):
        pass