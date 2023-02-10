using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


//omaa
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Media;

using Microsoft.Win32; //open file  location varte
using System.IO;
using System.Threading;
using System.Windows.Threading;


namespace MusiikkiSovellusProto
{
    public class mediaSoitinLuokka
    {
        public bool IsSoitinPlaying { get; set; }

        public int IsSoitinIndex { get; set; }

        public int IsSoundLocation;

        public string IsSoundPolku { get; set; }

        public int IsFailIndex { get; set; }

        public static int MikaIndexAseta(int x)
        {
            int MikaIndex = x;
            return MikaIndex;
        }
        public void SoitinSoittaa()
        {
            try
            {
                SoundPlayer soittaja = new SoundPlayer();
                soittaja.Stop();
                IsSoitinPlaying = true;
                soittaja.SoundLocation = IsSoundPolku;
                if (soittaja.SoundLocation != null)
                {
                    soittaja.Play();
                }
            }
            catch ( Exception s )
            {
                MessageBox.Show(s.ToString());
            }
            
        }
    }
}
