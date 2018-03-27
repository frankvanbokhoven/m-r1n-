using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UNET_Sounds
{
    public class UNETSoundsController
    {
        private const string cAssistSound = "beep.wav";
        private SoundPlayer player;
         public UNETSoundsController()
        {

        }


        /// <summary>
        /// 2.1.10 Receive Assist: play beep
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PlayAssistBeep()
        {
            try
            {
                if (File.Exists(cAssistSound))
                {
                    string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                    UriBuilder uri = new UriBuilder(codeBase);
                    string path = Uri.UnescapeDataString(uri.Path);
                    string dirpath = Path.GetDirectoryName(path);
                    //  System.Reflection.Assembly a = System.Reflection.Assembly.GetExecutingAssembly();

                    // System.IO.Stream s = a.GetManifestResourceStream(cAssistSound);

                    player = new SoundPlayer(Path.Combine(dirpath, cAssistSound));
                    player.PlayLooping();



                }
            }
            catch (Exception ex)
            {
                //nix
            }
        }

        /// <summary>
        /// stop the assist beep
        /// </summary>
        public void StopAssistBeep()
        {
            try
            {
                //    System.Threading.Thread.Sleep(2000);//todo: kijken of dit wel noodzakelijk is..
                if (player != null)
                {
                    player.Stop();
                }
            }
            catch (Exception ex)
            {
                //nix
            }
        }
    }
}

