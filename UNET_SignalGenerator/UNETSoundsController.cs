using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace UNET_Sounds
{
   public class UNETSoundsController
    {
        private const string cAssistSound = "beep-01a.wav";
        public UNETSoundsController()
        {

        }


        /// <summary>
        /// 2.1.10 Receive Assist: play beep
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PlayAssistBeep(object sender, EventArgs e)
        {
            if (File.Exists(cAssistSound))
            {
                System.Reflection.Assembly a = System.Reflection.Assembly.GetExecutingAssembly();
                System.IO.Stream s = a.GetManifestResourceStream(cAssistSound);
                SoundPlayer player = new SoundPlayer(s);
                player.Play();
                System.Threading.Thread.Sleep(2000);//todo: kijken of dit wel noodzakelijk is..
                player.Stop();
            }
        }
    }
}
