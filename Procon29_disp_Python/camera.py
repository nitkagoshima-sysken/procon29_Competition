from kivy.app import App
from kivy.clock import Clock
from kivy.uix.stacklayout import StackLayout
from kivy.uix.image import Image
from jnius import autoclass, cast
from android import activity
from functools import partial
from datetime import datetime
from os.path import exists

class ImageCaptureApp(App):

    RESULT_CODE = 0x5963

    Intent = autoclass('android.content.Intent')
    PythonActivity = autoclass('org.renpy.android.PythonActivity')
    MediaStore = autoclass('android.provider.MediaStore')
    Uri = autoclass('android.net.Uri')
    parcel = partial(cast, 'android.os.Parcelable')

    def build(self):
        self.root = StackLayout()
        self.root.bind(on_touch_up=self.do_capture)
        activity.bind(on_activity_result=self.on_activity_result)
        return self.root

    def get_filename(self):
        i = 0
        while True:
            i += 1
            fn = '/sdcard/{:%Y%m%d_%H%M}_{:03d}.png'.format(datetime.now(), i)
            if not exists(fn):
                return fn

    def do_capture(self, instance, value):
        self.filename = self.get_filename()
        uri = self.parcel(self.Uri.parse('file://' + self.filename))
        intent = self.Intent(self.MediaStore.ACTION_IMAGE_CAPTURE)
        intent.putExtra(self.MediaStore.EXTRA_OUTPUT, uri)
        self.PythonActivity.mActivity.startActivityForResult(intent, self.RESULT_CODE)

    def on_activity_result(self, requestCode, resultCode, intent):
        if requestCode == self.RESULT_CODE:
            Clock.schedule_once(partial(self.add_picture, self.filename), 0)

    def add_picture(self, filename, *args):
        self.root.add_widget(Image(source=filename, size=(320,320), size_hint=(None,None)))

    def on_pause(self):
        return True

if __name__ == '__main__':
    ImageCaptureApp().run()