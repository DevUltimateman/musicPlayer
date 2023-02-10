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


using System.Web; //url varten linkit auki
using System.Diagnostics; //shell execute linkkejä varten


namespace MusiikkiSovellusProto
{
    
    public partial class Page_player : Page
    {
        //prtottyyppi testaa soitin play & tiedostopolku luku suoraan tidoston polusta
        //SoundPlayer tstplayer = new SoundPlayer();
        SoundPlayer testiplayer = new SoundPlayer();
        Page_Library pageLibra = new();
        //MainWindow mainwina = new();
        //ei prototyyppi versioon
        private bool soitinPaalla = false;
        private bool onkoSliderKaytossa = false;

        //bool raahaa;

        //MainWindow rs;
        //hae eka kirjaston biisit
        //Page_Library testaa = new Page_Library();

        LegacyIndex testaaindex = new LegacyIndex();
        public int testiindex = 1;
        public int listaboxSaver = 1; //button forward failsafe muuttuja

        public Page_player()
        {
            InitializeComponent();
            turn_off();
            PagePlayerContentFade(); //teksti ja kuvat fade
            NappulaOpacity(); //painikkeen kirkkaus
                              //TaytaKappaleLista();
            
           


        }


        private void Ajastin_Update( object päivitä, EventArgs e )
        {
            /*
            if ((mePlayer.Source != null) && (!onkoSliderKaytossa) && (mePlayer.NaturalDuration.HasTimeSpan))
            {
                //sliProgress.Minimum = 0;
                //sliProgress.Maximum = mePlayer.NaturalDuration.TimeSpan.TotalSeconds;
                //sliProgress.Value = mePlayer.Position.TotalSeconds;
            }
            */
        }

        private void Voiko( object soitin, CanExecuteRoutedEventArgs valita)
        {
            valita.CanExecute = true;
        }

        
        private void NappulaOpacity()
        {
            DoubleAnimation dd = new DoubleAnimation(8, TimeSpan.FromSeconds(8));
            
            
            
        }
        private void turn_off()
        {
            lblNowPlaying.Opacity = 0;
            img_now_playing.Opacity = 0;
        }
        private void PagePlayerContentFade()
        {
            

            DoubleAnimation fad = new DoubleAnimation(8, TimeSpan.FromMilliseconds(4000) );

            lblNowPlaying.BeginAnimation(Page_player.OpacityProperty, fad);
            img_now_playing.BeginAnimation(Page_player.OpacityProperty, fad);

            
            //lblNowPlaying.Opacity = 1;
            //img_now_playing.Opacity = 1;

        }


        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {

            testiplayer.Stop();
            
            //sndTest1.LoadedBehavior = MediaState.Play;
        }


        /*

        private void btnHaeKappaleet_Click(object sender, RoutedEventArgs e)
        {
            TaytaKappaleLista();
            
        }

        

        private void TaytaKappaleLista()
        {
            //uusi luokkailmentymä openfiledialogia käyttäen, 
            //tarvii jotta käyttäjä voi valita musiikeilleen lähdekansioita.
            OpenFileDialog kappaleLstHaku = new OpenFileDialog();
            kappaleLstHaku.Multiselect = true;
            kappaleLstHaku.InitialDirectory = @"E:\DEMOIDEAT 2021";

            if(  kappaleLstHaku.ShowDialog() == true )
            {
                tdsto = kappaleLstHaku.FileNames;
                polut = kappaleLstHaku.FileNames;

                for ( int s = 0; s < tdsto.Length; s++ )
                {
                    lstKappaleLista.Items.Add(tdsto[ s ]);
                }
            }
        }

        */

        
        private void Lisaa()
        {
            testiindex += 1;
            listaboxSaver += 1;
        }
        /*
        private void sliderProgress_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TimeSpan aika = new TimeSpan(0, 0, 0, ( int )sliderProgress.Value);
            resetoiSlider(aika);
        }


        void resetoiSlider( TimeSpan aika )
        {
            // mainwina.mediaElement.Position = aika;
            //MainWindow rs = new();
            sss.mediaElement.Position = aika;
            
        }

        private void sliderProgress_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            raahaa = true;
        }

        private void sliderProgress_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if ( raahaa )
            {
                TimeSpan ajat = new TimeSpan(0, 0, 0, ( int )sliderProgress.Value);
                resetoiSlider(ajat);
            }
            raahaa = false;
        }

        private void sliderProgress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
        */
        private void btnFrwd_Click(object sender, RoutedEventArgs e)
        {

            mediaSoitinLuokka eteenpain = new();
            
            

            //eteenpain.IsFailIndex += 1;



            //eteenpain.IsSoitinIndex = eteenpain.IsFailIndex;


            //Page_Library testaa = new Page_Library();

            //int index = testaa.lstKappaleet.SelectedIndex;
            //string indexItem = testaa.lstKappaleet.SelectedItem.ToString();

            //testaa.lstKappaleet.SelectedIndex = testaa.lstKappaleet.SelectedIndex += 1;

            MessageBox.Show( eteenpain.IsSoitinIndex.ToString());
            //var kpl_lst_max_size = testaa.lstKappaleet;
            //var biisiSize = testaa.lstKappaleet.Items.Count.ToString();
            //Lisaa();
            //MessageBox.Show(testiindex.ToString());
            //testaa.lstKappaleet.SelectedIndex ++;

            //muunna kpl_lst_max_size stringistä intiksi
            //int max_size_parsed = Int32.Parse(biisiSize);          
            //MessageBox.Show(testaa.lstKappaleet.SelectedIndex.ToString());
            //listaboxSaver += 1;

            /*
            if ( listaboxSaver >= max_size_parsed )
            {
                testaa.lstKappaleet.SelectedIndex = 1;
                listaboxSaver = 1;
            }

            //debuggaa
            while ( testaa.lstKappaleet.SelectedIndex == max_size_parsed )
            {
                testaa.lstKappaleet.SelectedIndex = 1;
                MessageBox.Show("Lista Index palautettu takaisin alkuun");
                //lstbox item 0 = materialdesign txt ( ei biisi )

            }
            */
            /*
            var maxsize = lstKappaleLista.Items.Count.ToString();

            int value_fromstring = Int32.Parse(maxsize);

            int max_x = value_fromstring;


            int alotus_x = 1;

            lstKappaleLista.SelectedIndex += 1;
            listaboxSaver += 1;

            if ( listaboxSaver >= value_fromstring )
            {
                lstKappaleLista.SelectedIndex = 1;
                listaboxSaver = 1;
            }
            
            //debug
            MessageBox.Show(lstKappaleLista.SelectedIndex.ToString());
            while( lstKappaleLista.SelectedIndex == value_fromstring )
            {
                lstKappaleLista.SelectedIndex = 1;
                MessageBox.Show("Listan Index Vaihdettu takaisin lähtöruutuun");
            }
            */


        }

        private void imgSearch_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if ( txtNytSoi != null )
            {
                
                string osoite = txtNytSoi.Text;
                var tiscord = new ProcessStartInfo("https://www.youtube.com/results?search_query=" + osoite);
                tiscord.UseShellExecute = true;
                tiscord.Verb = "open";
                Process.Start(tiscord);
            }
            
        }




        /*
private void btnBck_Click(object sender, RoutedEventArgs e)
{
   var maxsize = lstKappaleLista.Items.Count.ToString();

   int value_fromstring = Int32.Parse(maxsize);

   int max_x = value_fromstring;



   if (listaboxSaver > 1 )
   {
       lstKappaleLista.SelectedIndex -= 1;
       listaboxSaver -= 1;
   }
   else
   {
       lstKappaleLista.SelectedIndex = 1;
       listaboxSaver = 1;
   }


   //debug
   MessageBox.Show(lstKappaleLista.SelectedIndex.ToString());
   while (lstKappaleLista.SelectedIndex == value_fromstring)
   {
       lstKappaleLista.SelectedIndex = 1;
       MessageBox.Show("Listan Index Vaihdettu takaisin lähtöruutuun");
   }
}
/*
public void lstKappaleLista_SelectionChanged(object sender, SelectionChangedEventArgs e)
{

   tstplayer.Stop();
   var newsong = lstKappaleLista.SelectedItem;
   //newsong.ToString();

   tstplayer.SoundLocation = newsong.ToString();
   tstplayer.Play();
}
*/
    }
}
