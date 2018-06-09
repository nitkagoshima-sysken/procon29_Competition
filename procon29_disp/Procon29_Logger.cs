using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace procon29_disp
{
    class Procon29_Logger
    {
        private RichTextBox richTextBox;

        public RichTextBox RichTextBox { get => richTextBox; set => richTextBox = value; }

        public Procon29_Logger(RichTextBox richTextBox)
        {
            RichTextBox = richTextBox;
        }

        public void Write(Color color, string text)
        {
            RichTextBox.SelectionColor = color;
            RichTextBox.SelectedText = text;
        }

        public void WriteLine(Color color, string text)
        {
            RichTextBox.SelectionColor = color;
            RichTextBox.SelectedText = text + "\n";
        }
    }
}
