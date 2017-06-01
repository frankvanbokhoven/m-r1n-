
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace TestPJSUA2Mark.Classes
{
    /// <summary>
    /// This class shows whatever is written to the console into the listbox given as a parameter
    /// </summary>
    public class ConsoleCatcher
    {
        Timer T;
        MemoryStream mem;
        StreamWriter writer;
        TextBox ParentTextBox;

        public ConsoleCatcher(TextBox _textbox)
        {
            mem = new MemoryStream(500);
            writer = new StreamWriter(mem);
            Console.SetOut(writer); //actually connect the console to the stringwriter

            //Create timer
            T = new Timer();
            T.Interval = 250;
            T.Tick += new EventHandler(T_Tick);
            T.Enabled = true;

            ParentTextBox = _textbox;

            Console.WriteLine("output");//just to test                      
        }

        /// <summary>
        /// timers tick event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void T_Tick(object sender, EventArgs e)
        {
            string s = Encoding.Default.GetString(mem.ToArray());
            string[] Lines = s.Split(Environment.NewLine.ToCharArray());
            foreach (string str in Lines)
            {
                if (str.Length != 0)
                {
                    ParentTextBox.AppendText(Environment.NewLine);
                    ParentTextBox.AppendText(DateTime.Now.ToShortDateString() + " : " + str);
                }
            }
        }

        /// <summary>
        /// Do some cleanup before destroy
        /// </summary>
        public void Destroy()
        {
            T.Enabled = false;
            mem.Dispose();
            writer.Dispose();
        }
    }
}