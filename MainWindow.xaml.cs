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



//omat includet
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using Microsoft.Win32; //open file  location varte
using System.IO;
using System.Threading;
using System.Windows.Threading;

using System.Media;


namespace MusiikkiSovellusProto
{

    public partial class MainWindow : Window
    {
        private double mainwin_maxOpacity = 1;
        private double mainwin_min_Opacity = 0;

        //Luokkailmentymät / sivut
        Page_Library mainsoitin = new Page_Library();
        SoundPlayer vakioSoitin = new SoundPlayer();
        Page_player playerlist = new Page_player();
        Page_Contact contactpage = new Page_Contact();

        MediaElement mediasoitin = new MediaElement();
        bool soikokappale;

        DispatcherTimer ajastin;
        public delegate void timerTick();
        timerTick ajastinTick;


        //private TimeSpan TotalTime;

        
        bool raahaa;
        public MainWindow()
        {
            InitializeComponent();

            ajastin = new DispatcherTimer();
            ajastin.Interval = TimeSpan.FromMilliseconds(100);
            ajastin.Tick += new EventHandler(invokeTick);
            ajastinTick = new timerTick(prog_bar_pos);
            buttonitPois(); //hack

            //tee luokka ilmentymä playerlist sivulle tässä vaiheessa jottei joka kerta tarvitse luoda uutta / musiikki jatkuuu
            //vain piilota sivu ensinäkemältä kunnes sivu halutaan näkyviin

            //var cc = new Page_player();
            Page_kaikki.Content = playerlist;
            Page_kaikki.Visibility = Visibility.Hidden;
            Page_kaikki.Opacity = 0;

            Page_KappaleLST.Content = null;
            Page_KappaleLST.Visibility = Visibility.Hidden;
            Page_KappaleLST.Opacity = 0;
            
            Page_libra.Content = mainsoitin;
            Page_libra.Visibility = Visibility.Hidden;
            Page_libra.Opacity = 0;

            Page_cont.Content = contactpage;
            Page_cont.Opacity = 0;
            Page_cont.Visibility = Visibility.Hidden;
            
    

            //piilota pause button että voi painaa playta aluksi
            //btnPause.Visibility = Visibility.Hidden;

            sliderProgress.Visibility = Visibility.Hidden;

            AsetaBorderBrushTakas();
            //buttonitPois();

            //ennen ekaa slideria laita pimeeks kunnes hiiri click volume slider ja nostaessa piilottaa
            txtVolNumba.Visibility = Visibility.Hidden;
        }

        void invokeTick( object sender, EventArgs e )
        {
           Dispatcher.Invoke(ajastinTick);
        }

       
        void prog_bar_pos() // päivitä kpl kun bool kappale soi = true & ota soittimen tick huomioon
        {
            if ( soikokappale )
            {
                string sekunnit, minuutit, tunnit;

                #region customizeTime
                if (mediaElement.Position.Seconds < 10)
                {
                    sekunnit = "0" + mediaElement.Position.Seconds.ToString();
                }
                else
                {
                    sekunnit = mediaElement.Position.Seconds.ToString();
                }

                if ( mediaElement.Position.Minutes < 10 )
                {
                    minuutit = "0" + mediaElement.Position.Minutes.ToString();
                }
                else
                {
                    minuutit = mediaElement.Position.Minutes.ToString();
                }

                if ( mediaElement.Position.Hours < 10 )
                {
                    tunnit = "0" + mediaElement.Position.Hours.ToString();
                }
                else
                {
                    tunnit = mediaElement.Position.Hours.ToString();
                }

                #endregion customizeTime


                //playerlist.sliderProgress.Value = mediaElement.Position.TotalMilliseconds;

                
                playerlist.progressBarPlayer.Value = mediaElement.Position.TotalMilliseconds;
                sliderProgress.Value = mediaElement.Position.TotalMilliseconds;
                progressMainView.Value = mediaElement.Position.TotalMilliseconds;
                playerlist.txtPlayerTime.Text = "Aikaa kulunut: " + tunnit + ":" + minuutit + ":" + sekunnit;
                
            }
        }
        decimal btn_default_opacity = 0.32M; //luo button hoverille fade efekti
        decimal btn_default_max = 1.0M; //luo button hover pois opacity takas
        //ei tarvii enää, pidä silti varmuuden vuoks


        

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            //vakioSoitin.Stop();
            //vakioSoitin.SoundLocation = mainsoitin.lstKappaleet.SelectedItem.ToString();
            //vakioSoitin.Play();
            


            //VANHAA YLLÄ
         
                mediaElement.Source = new Uri(mainsoitin.lstKappaleet.SelectedItem.ToString());
                mediaElement.Play();
                soikokappale = true;
                //playerlist.txtNytSoi.Text = mainsoitin.lstKappaleet.SelectedItem.ToString();
            playerlist.txtNytSoi.Text = System.IO.Path.GetFileName(mainsoitin.lstKappaleet.SelectedItem.ToString());


            int max = mainsoitin.lstKappaleet.Items.Count;
            int nykyinen = mainsoitin.lstKappaleet.SelectedIndex;

            playerlist.txtkplLuku.Text = nykyinen.ToString() + " / " + max.ToString();



            sliderProgress.IsEnabled = true;

            //timer = new DispatcherTimer();
            //timer.Interval = TimeSpan.FromMilliseconds(10);
            //timer.Tick += new EventHandler(timer_Tick);
            //tick = new timerTick(changeStatus);

            btnPause.Visibility = Visibility.Visible;
            btnPlay.Visibility = Visibility.Visible;
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            //mediaElement.Stop();
            sliderProgress.IsEnabled = false;
            mediaElement.Pause();
            soikokappale = false;
            btnPause.Visibility = Visibility.Visible;
            btnPlay.Visibility = Visibility.Visible;
            ajastin.Stop();
        }

        private void btnPrevSong_Click(object sender, RoutedEventArgs e)
        {
            
            
            if ( mainsoitin.lstKappaleet.SelectedIndex <= 1 )
            {
                mainsoitin.lstKappaleet.SelectedIndex = 1;
            }
            else if ( mainsoitin.lstKappaleet.SelectedIndex > 1)
            {
                mainsoitin.lstKappaleet.SelectedIndex -= 1;
                
            }

            mediaElement.Source = new Uri(mainsoitin.lstKappaleet.SelectedItem.ToString());
            mediaElement.Play();
            soikokappale = true;
            btnPlay.Visibility = Visibility.Visible;
            int max = mainsoitin.lstKappaleet.Items.Count;
            int nykyinen = mainsoitin.lstKappaleet.SelectedIndex;

            playerlist.txtkplLuku.Text = nykyinen.ToString() + " / " + max.ToString();
            //playerlist.txtNytSoi.Text = mainsoitin.lstKappaleet.SelectedItem.ToString();
            playerlist.txtNytSoi.Text = System.IO.Path.GetFileName(mainsoitin.lstKappaleet.SelectedItem.ToString());
            //vakioSoitin.SoundLocation = mainsoitin.lstKappaleet.SelectedItem.ToString();
            //vakioSoitin.Play();
            //playerlist.txtNytSoi.Text = mainsoitin.lstKappaleet.SelectedItem.ToString();
            sliderProgress.IsEnabled = true;
        }

        private void buttonitPois()
        {
            btnPlay.IsEnabled = false;
            btnPause.IsEnabled = false;
            btnNextSong.IsEnabled = false;
            btnPrevSong.IsEnabled = false;
        }
        private void onkoAvattuKansio()
        {
            btnPlay.IsEnabled = false;
            btnPause.IsEnabled = false;
            btnNextSong.IsEnabled = false;
            btnPrevSong.IsEnabled = false;
        }
        private void btnNextSong_Click(object sender, RoutedEventArgs e)
        {
            
            int kappaleMaxSize = mainsoitin.lstKappaleet.Items.Count;

            if ( mainsoitin.lstKappaleet.SelectedIndex == kappaleMaxSize -1 )
            {
                mainsoitin.lstKappaleet.SelectedIndex = 1;            
            }
            
            else
            {
                mainsoitin.lstKappaleet.SelectedIndex += 1;
            }
            mediaElement.Source = new Uri(mainsoitin.lstKappaleet.SelectedItem.ToString());
            mediaElement.Play();
            soikokappale = true;
            btnPlay.Visibility = Visibility.Visible;

            //playerlist.txtNytSoi.Text = mainsoitin.lstKappaleet.SelectedItem.ToString();

            playerlist.txtNytSoi.Text = System.IO.Path.GetFileName(mainsoitin.lstKappaleet.SelectedItem.ToString());
            int max = mainsoitin.lstKappaleet.Items.Count;
            int nykyinen = mainsoitin.lstKappaleet.SelectedIndex;

            playerlist.txtkplLuku.Text = nykyinen.ToString() + " / " + max.ToString();
            sliderProgress.IsEnabled = true;
        }
        private void Ajastin_Update( object sender, EventArgs e )
        {
            

        }
        private void button_ColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            //sulje sovelllus
            Environment.Exit( 1 );
            
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            start_window.WindowState = WindowState.Minimized;
        }

        private void btnMinimize_Loaded(object sender, RoutedEventArgs e)
        {
            btnMinimize.Background = Brushes.DarkCyan;
        }

        private void btnClose_Loaded_1(object sender, RoutedEventArgs e)
        {
            btnClose.Background = Brushes.DarkRed;
        }

        private void btnClose_Loaded(object sender, RoutedEventArgs e)
        {
            btnClose.Background = Brushes.DarkRed;
        }

        private void btnMinimize_Loaded_1(object sender, RoutedEventArgs e)
        {
            btnMinimize.Background = Brushes.DarkCyan;
        }

        
       

        public void AsetaBorderBrushTakas()
        {
            var tallennaOG = btnGoPlayer.BorderBrush;
        }
        /* LAITA TAKASI MYÖHEMMI KU NÄKEE EKA ET MIKÄ TOI RANDOM BUGI MIKÄ AINA TULEE NÄIST
        private void btnGoLibrary_MouseEnter(object sender, MouseEventArgs e)
        {

            btnGoLibrary.Foreground = Brushes.DarkRed;
            btnGoLibrary.BorderBrush = Brushes.White;
            btnGoLibrary.Background = Brushes.White;
        }

        private void btnGoPlayer_MouseLeave(object sender, MouseEventArgs e)
        {
            btnGoLibrary.Foreground = Brushes.White;
            btnGoLibrary.BorderBrush = Brushes.DarkRed;
            btnGoLibrary.Background = Brushes.Transparent;
        }

        private void btnGoHome_MouseEnter(object sender, MouseEventArgs e)
        {
            btnGoHome.Foreground = Brushes.DarkRed;
            btnGoHome.BorderBrush = Brushes.White;
            btnGoHome.Background = Brushes.White;
        }

        private void btnGoHome_MouseLeave(object sender, MouseEventArgs e)
        {
            //btnGoHome.BorderBrush.Opacity = (double) btn_default_max;
            //btnGoHome.Background = Brushes.Transparent;
            btnGoHome.Background = Brushes.Transparent;
            btnGoHome.Foreground = Brushes.White;
            btnGoHome.BorderBrush = Brushes.DarkRed;
            btnGoHome.Background = Brushes.Transparent;
        }
        */
        private void btnGoHome_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation aniHome = new DoubleAnimation(100, TimeSpan.FromSeconds(10));
            aniHome.Duration = TimeSpan.FromSeconds(10);
            start_window.BeginAnimation(MainWindow.OpacityProperty, aniHome);
            
            
            
            
            sliderProgress.Visibility = Visibility.Visible;
            Page_libra.Visibility = Visibility.Hidden;
            Page_kaikki.Visibility = Visibility.Hidden;
            Page_cont.Visibility = Visibility.Hidden;
            PaaIkkunaShowElems(); //näytä pääikkunan ui elementit go home mentäessä
            //sliderProgress.Visibility = Visibility.Hidden;
            btnNextSong.Visibility = Visibility.Hidden;
            btnPrevSong.Visibility = Visibility.Hidden;
            btnPlay.Visibility = Visibility.Hidden;
            btnPause.Visibility = Visibility.Hidden;
        }

        private void btnGoPlayer_Click(object sender, RoutedEventArgs e)
        {


            DoubleAnimation dani = new DoubleAnimation(8, TimeSpan.FromMilliseconds(1425));
            Page_kaikki.BeginAnimation(Page_player.OpacityProperty, dani);

            Page_kaikki.Visibility = Visibility.Visible;
            Page_cont.Visibility = Visibility.Hidden;
            if ( Page_libra.Visibility == Visibility.Visible )
            {
                Page_libra.Visibility = Visibility.Hidden;
                
            }
            sliderProgress.Visibility = Visibility.Visible;
            if ( start_window.Visibility == Visibility.Visible )
            {
                PaaIkkunaHideElems();
            }
            //sliderProgress.Visibility = Visibility.Visible;

            //progressMainView.Visibility = Visibility.Visible;
            btnNextSong.Visibility = Visibility.Visible;
            btnPrevSong.Visibility = Visibility.Visible;
            //sliderProgress.Visibility = Visibility.Hidden;
            progressMainView.Visibility = Visibility.Hidden;
            if (soikokappale)
            {
                btnPlay.Visibility = Visibility.Visible;
                btnPause.Visibility = Visibility.Visible;
            }
            else
            {
                btnPlay.Visibility = Visibility.Visible;
                btnPause.Visibility = Visibility.Visible;
            }
            //btnPlay.Visibility = Visibility.Visible;
            //btnPause.Visibility = Visibility.Visible;
        }
        private void btnGoLibrary_Click(object sender, RoutedEventArgs e)
        {

            DoubleAnimation libraAni = new DoubleAnimation(8, TimeSpan.FromMilliseconds(1450));
            Page_libra.BeginAnimation(Page_Library.OpacityProperty, libraAni);
            Page_libra.Visibility = Visibility.Visible;
            Page_cont.Visibility = Visibility.Hidden;
            if ( Page_kaikki.Visibility == Visibility.Visible )
            {
                Page_kaikki.Visibility = Visibility.Hidden;
            }
            sliderProgress.Visibility = Visibility.Hidden;
            if ( start_window.Visibility == Visibility.Visible )
            {
                PaaIkkunaHideElems();
            }

            if (soikokappale)
            {
                btnPlay.Visibility = Visibility.Visible;
                btnPause.Visibility = Visibility.Visible;
            }
            else
            {
                btnPlay.Visibility = Visibility.Visible;
                btnPause.Visibility = Visibility.Visible;
            }
            sliderProgress.Visibility = Visibility.Hidden;
            progressMainView.Visibility = Visibility.Hidden;
            btnNextSong.Visibility = Visibility.Visible;
            btnPrevSong.Visibility = Visibility.Visible;
            
            

        }

        private void PaaIkkunaHideElems()
        {
            imgPlayIcon.Visibility = Visibility.Hidden;
            lblOtsikko1.Visibility = Visibility.Hidden;
            lblOtsikko2.Visibility = Visibility.Hidden;
            txtBlockMainInfo.Visibility = Visibility.Hidden;
        }

        private void btnGoContact_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation libraAni = new DoubleAnimation(8, TimeSpan.FromMilliseconds(1450));
            Page_cont.BeginAnimation(Page_Contact.OpacityProperty, libraAni);
            Page_cont.Visibility = Visibility.Visible;
            
            Page_libra.Visibility = Visibility.Hidden;
            Page_kaikki.Visibility = Visibility.Hidden;
            start_window.Visibility = Visibility.Visible;
            sliderProgress.Visibility = Visibility.Hidden;

            btnPause.Visibility = Visibility.Hidden;
            btnPlay.Visibility = Visibility.Hidden;
            
            btnNextSong.Visibility = Visibility.Hidden;
            btnPrevSong.Visibility = Visibility.Hidden;
            sliderProgress.Visibility = Visibility.Hidden;
            progressMainView.Visibility = Visibility.Hidden;
            PaaIkkunaHideElems();
            
        }

        private void sliderVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaElement.Volume = sliderVolume.Value;
            vaihdaVoluumia();
            //int newnumba = sliderVolume.Value;


        }

        private void vaihdaVoluumia()
        {
            if( txtVolNumba != null )
            {
                double muuttuja = sliderVolume.Value;
                string uusimuuttuja = muuttuja.ToString();

                if ( muuttuja == 1 )
                {
                    txtVolNumba.Text = uusimuuttuja + ".0";
                }
                else if ( muuttuja == 0 )
                {
                    txtVolNumba.Text = uusimuuttuja + ".0";
                }
                else

                {
                    txtVolNumba.Text = uusimuuttuja;
                }
                
            }
            
        }
        private void PaaIkkunaShowElems()
        {
            imgPlayIcon.Visibility = Visibility.Visible;
            lblOtsikko1.Visibility = Visibility.Visible;
            lblOtsikko2.Visibility = Visibility.Visible;
            txtBlockMainInfo.Visibility = Visibility.Visible;
        }

        private void mediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            ajastin.Start();

            playerlist.progressBarPlayer.Maximum = mediaElement.NaturalDuration.TimeSpan.TotalMilliseconds;
            sliderProgress.Maximum = mediaElement.NaturalDuration.TimeSpan.TotalMilliseconds;
            progressMainView.Maximum = mediaElement.NaturalDuration.TimeSpan.TotalMilliseconds;
            playerlist.txtNytSoi.Text = System.IO.Path.GetFileName(mainsoitin.lstKappaleet.SelectedItem.ToString());
            soikokappale = true;
            
            
        }

        private void mediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            ajastin.Stop();
            mainsoitin.lstKappaleet.SelectedIndex += 1;
            if ( mainsoitin.lstKappaleet.SelectedIndex == mainsoitin.lstKappaleet.Items.Count - 1 )
            {
                mainsoitin.lstKappaleet.SelectedIndex = 1;
            }
            mediaElement.Source = new Uri(mainsoitin.lstKappaleet.SelectedItem.ToString());
            playerlist.txtNytSoi.Text = System.IO.Path.GetFileName(mainsoitin.lstKappaleet.SelectedItem.ToString());
            mediaElement.Play();
            



        }

        //void asetaSlider( TimeSpan ts )
        //{
        //    mediaElement.Position =ts;
        //}

        private void sliderProgress_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TimeSpan ts = new TimeSpan(0, 0, 0, 0, ( int )sliderProgress.Value );
            changePosition(ts);


        }

        private void sliderProgress_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            raahaa = true;
        }

        private void sliderProgress_PreviewMouseLeftButtonUp1(object sender, MouseButtonEventArgs e)
        {
            if (raahaa)
            {
                TimeSpan ts = new TimeSpan(0, 0, 0, 0, ( int )sliderProgress.Value);
                changePosition(ts);
                playerlist.progressBarPlayer.Value = sliderProgress.Value;
            }
            raahaa = false;
        }


        void changePosition( TimeSpan ts )
        {
            mediaElement.Position = ts;
        }

        private void sliderProgress_PreviewMouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {

        }

        private void sliderProgress_PreviewMouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            raahaa = true;
        }

        private void sliderProgress_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (raahaa)
            {
                TimeSpan ts = new TimeSpan(0, 0, 0, 0, ( int )sliderProgress.Value);
                changePosition(ts);
                playerlist.progressBarPlayer.Value = sliderProgress.Value;
            }
            raahaa = false;
        }

        private void sliderProgress_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            TimeSpan ts = new TimeSpan(0, 0, 0, 0, ( int )sliderProgress.Value);
            changePosition(ts);
        }

        private void sliderProgress_PreviewMouseLeftButtonDown_3(object sender, MouseButtonEventArgs e)
        {
            raahaa = true;
        }

        private void sliderProgress_MouseLeftButtonUp_2(object sender, MouseButtonEventArgs e)
        {
            TimeSpan ts = new TimeSpan(0, 0, 0, 0, ( int )sliderProgress.Value);
            changePosition(ts);
        }

        

        private void sliderProgress_PreviewMouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            if (raahaa)
            {
                TimeSpan ts = new TimeSpan(0, 0, 0, 0, ( int )sliderProgress.Value);
                changePosition(ts);
                playerlist.progressBarPlayer.Value = sliderProgress.Value;
            }
            raahaa = false;
        }

        private void sliderProgress_MouseLeftButtonUp_3(object sender, MouseButtonEventArgs e)
        {
            TimeSpan ts = new TimeSpan(0, 0, 0, 0, ( int )sliderProgress.Value);
            changePosition(ts);
        }

        private void sliderProgress_PreviewMouseLeftButtonDown_4(object sender, MouseButtonEventArgs e)
        {
            raahaa = true;
        }

        private void sliderProgress_PreviewMouseLeftButtonUp_2(object sender, MouseButtonEventArgs e)
        {
            if (raahaa)
            {
                TimeSpan ts = new TimeSpan(0, 0, 0, 0, ( int )sliderProgress.Value);
                changePosition(ts);
                playerlist.progressBarPlayer.Value = sliderProgress.Value;
            }
            raahaa = false;
        }

        private void btnEnablePlaybacks_Click(object sender, RoutedEventArgs e)
        {
            btnPause.IsEnabled = true;
            btnPlay.IsEnabled = true;
            btnNextSong.IsEnabled = true;
            btnPrevSong.IsEnabled = true;
            btnEnablePlaybacks.IsEnabled = false;
            btnEnablePlaybacks.Visibility = Visibility.Hidden;
        }

        private void sliderProgress_MouseLeftButtonUp_4(object sender, MouseButtonEventArgs e)
        {
            TimeSpan ts = new TimeSpan(0, 0, 0, 0, ( int )sliderProgress.Value);
            changePosition(ts);
        }

        private void sliderProgress_PreviewMouseLeftButtonUp_3(object sender, MouseButtonEventArgs e)
        {
            if (raahaa)
            {
                TimeSpan ts = new TimeSpan(0, 0, 0, 0, ( int )sliderProgress.Value);
                changePosition(ts);
                playerlist.progressBarPlayer.Value = sliderProgress.Value;
            }
            raahaa = false;
        }

        private void sliderProgress_PreviewMouseLeftButtonDown_5(object sender, MouseButtonEventArgs e)
        {
            raahaa = true;
        }


        private void sliderVolume_MouseEnter(object sender, MouseEventArgs e)
        {
            txtVolNumba.Visibility = Visibility.Visible;
            imgÄäni.Visibility = Visibility.Hidden;
        }

        private void sliderVolume_MouseLeave(object sender, MouseEventArgs e)
        {
            txtVolNumba.Visibility = Visibility.Hidden;
            imgÄäni.Visibility = Visibility.Visible;
        }
    }
}





