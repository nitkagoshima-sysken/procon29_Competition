import wx

class Test(wx.Frame):
    def __init__(self, parent, id, title):
        wx.Frame.__init__(self, parent, id, title)

        panel = wx.Panel(self, -1)
        panel.Bind(wx.EVT_KEY_DOWN, self.OnKeyDown)
        panel.SetFocus()
        self.Show(True)


    # keyboard Callback        
    def OnKeyDown(self, event):
        keycode = event.GetKeyCode()
        print(keycode)
        if keycode == wx.WXK_ESCAPE:
            ret = wx.MessageBox('Are you sure to quit?', 'Question',
                                wx.YES_NO | wx.NO_DEFAULT, self)
            if ret == wx.YES:
                self.Close()
        event.Skip()

app = wx.App()
Test(None, -1, "keyevent" )
app.MainLoop()