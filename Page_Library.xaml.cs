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


using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Media;
using Microsoft.Win32; //open file  location varte
using System.IO;
using System.Threading;
using System.Windows.Threading;

namespace MusiikkiSovellusProto
{
    
    public partial class Page_Library : Page
    {
        
        SoundPlayer soitin = new SoundPlayer(); //tee globaali soitin instanssi
        //mediaSoitinLuokka lol = new(); //ei tarvii enää debug 
        bool ekaKerta = true;
        OpenFileDialog kappaleHaku = new OpenFileDialog();
        public Page_Library()
        {
            InitializeComponent();

            //info & txt block kirjastolle
            //frKirjastoHelpFrame.Visibility = Visibility.Hidden;
            //txtBlockLibraInfo.Visibility = Visibility.Hidden;
            //string debug = txtBlockLibraInfo.Text;
            //MessageBox.Show(debug);

            //info text pimeeks aluks
            txtBlockLibraInfo.Visibility = Visibility.Hidden;
            //close info btn pimeeks aluks
            btnKirjastoHelpClose.Visibility = Visibility.Hidden;

            lblx.Visibility = Visibility.Hidden;
            lblq.Visibility = Visibility.Visible;
        }

        private int KappaleListaFailSafe = 1;
        private int listaboxSaver = 1; //button forward failsafe muuttuja
        private string [] tdsto, polut; //biisitiedot listaa ja polut listassa oleville tiedostoille
        public string VirheRatkaisuMSG = "RATKAISU: VALITSE SOITETTAVAT KAPPALEET ENNEN NIIDEN SOITTAMISTA LISÄÄ SOITTOLISTAAN KOHDASTA";
        
        

        private void btnKirjastoHelpOpen_Click(object sender, RoutedEventArgs e)
        {
            
            txtBlockLibraInfo.Visibility = Visibility.Visible;
            txtBlockLibraInfo.Opacity = 1;
            btnKirjastoHelpClose.Visibility = Visibility.Visible;
            lstKappaleet.Visibility = Visibility.Hidden;
            lblq.Visibility = Visibility.Hidden;
            lblx.Visibility = Visibility.Visible;
        }

        private void btnKirjastoHelpClose_Click(object sender, RoutedEventArgs e)
        {
            
            btnKirjastoHelpClose.Visibility = Visibility.Hidden;
            lstKappaleet.Visibility = Visibility.Visible;

            txtBlockLibraInfo.Visibility = Visibility.Hidden;
            txtBlockLibraInfo.Opacity = 0;
            lblx.Visibility = Visibility.Hidden;
            lblq.Visibility = Visibility.Visible;
        }

        private void btnHaeKappaleet_Click(object sender, RoutedEventArgs e)
        {
            
            TeeKappaleLista();
        }

        

        private void TeeKappaleLista()
        {

            //failsafe sulje apu ikkuna jos näkyvillä

            if( btnKirjastoHelpClose.Visibility == Visibility.Visible )
            {
                btnKirjastoHelpClose.Visibility = Visibility.Hidden;
                txtBlockLibraInfo.Visibility = Visibility.Hidden;
                lstKappaleet.Visibility = Visibility.Visible;
            }
            //uusi luokkailmentymä openfiledialogia käyttäen, 
            //tarvii jotta käyttäjä voi valita musiikeilleen lähdekansioita.
            
            kappaleHaku.Multiselect = true;
            kappaleHaku.InitialDirectory = @"E:\KouluBiisit";
            
            if (kappaleHaku.ShowDialog() == true)
            {
                tdsto = kappaleHaku.FileNames;
                polut = kappaleHaku.FileNames;

                for (int s = 0; s < tdsto.Length; s++)
                {
                    lstKappaleet.Items.Add(tdsto [ s ]);
                }

                
                
            }
        }

        private void btnTestListChange_Click(object sender, RoutedEventArgs e)
        {
            
            var indexnumero = lstKappaleet.Items.Count.ToString();

            if( Int32.Parse( indexnumero) > 0)
            {
                int maxnumero = Int32.Parse(indexnumero);

                //nopeempi on vaa lstKappaleet.SelectedIndex += 1;
                //mutta jokin mättää pitää debugata
                lstKappaleet.SelectedIndex = lstKappaleet.SelectedIndex + 1;

                KappaleListaFailSafe += 1;
                //MessageBox.Show("MAX ARVO: " + maxnumero.ToString() + "\n" + "NYK ARVO: " + KappaleListaFailSafe.ToString());
                if (KappaleListaFailSafe == maxnumero)
                {
                    lstKappaleet.SelectedIndex = 1;
                    KappaleListaFailSafe = 1;
                }
            }

            else
            {
                MessageBox.Show("Valittu soittolista on tyhjä!  \nTäytä soittolistaasi ensiksi valitsemalla jotain kappaleita kohdasta: [ Lisää soittolistalle ]");
            }
            

        }

        public void lstKappaleet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Page_player paivitaNytSoiTxt = new Page_player();
            ///paivitaNytSoiTxt.txtNytSoi.Text = lstKappaleet.SelectedItem.ToString();

            /*
            lol.IsSoundPolku = lstKappaleet.SelectedItem.ToString();
            int currentIndex = mediaSoitinLuokka.MikaIndexAseta(lstKappaleet.SelectedIndex);
            lol.IsSoitinIndex = currentIndex;
            lol.SoitinSoittaa();

            MessageBox.Show(lstKappaleet.SelectedIndex.ToString());
            */
            /*
            try
            {
                var stream = lstKappaleet.SelectedItem;
                soitin.Stop();
                soitin.SoundLocation = stream.ToString();
                soitin.Play();
            }
            
            catch( Exception s )
            {
                MessageBox.Show(s.ToString() + "\n" + VirheRatkaisuMSG );
            }
            */



            //soitin.SoundLocation = lstKappaleet.SelectedItem.ToString();
            //var uusibiisi = lstKappaleet.SelectedItem;
            //newsong.ToString();
            //var kpl_new_loc = lstKappaleet.SelectedItem.ToString();
            //soitin.SoundLocation = kpl_new_loc;
            //soitin.Play();
            //soitin.
            //soitin.SoundLocation = lstKappaleet.SelectedItem.ToString();
            //soitin.SoundLocation = uusibiisi.ToString();
            //soitin.Play();
        }
    }
}
