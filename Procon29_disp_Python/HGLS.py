<TextWidget>:
    BoxLayout:
        id: input
        orientation: 'vertical'
        size: root.size
        l_text:'aa'

        Label:
            id: label1
            font_size: 68
            text: input.l_text

        Button:
            id: button1
            text: "START"
            font_size: 68
            on_press: input.l_text = root.buttonClicked()