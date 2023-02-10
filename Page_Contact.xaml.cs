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

using System.Web; //url varten linkit auki
using System.Diagnostics; //shell execute linkkejä varten

namespace MusiikkiSovellusProto
{
    
    public partial class Page_Contact : Page
    {
        public Page_Contact()
        {
            InitializeComponent();
            PiilotaSocialImg();
        }


        void PiilotaSocialImg()
        {
            imgGit1.Opacity = 0;
            imgGit2.Opacity = 0;
            imgPluto1.Opacity = 0;
            imgPluto2.Opacity = 0;
            imgPluto2.Opacity = 0;
            imgPluto1.Opacity = 0;

        }

        private void btnGitHub_Click(object sender, RoutedEventArgs e)
        {
            //github linkki
            var github = new ProcessStartInfo("https://github.com/DevUltimateman");
            github.UseShellExecute = true;
            github.Verb = "open";

            Process.Start(github);
        }

        private void btnYouTube_Click(object sender, RoutedEventArgs e)
        {
            //yt linkki
            var yt = new ProcessStartInfo("https://www.youtube.com/channel/UCbASvXNAlVCyMdcNVx-LcAQ");
            yt.UseShellExecute = true;
            yt.Verb = "open";
            Process.Start(yt);
        }

        private void btnPlutonium_Click(object sender, RoutedEventArgs e)
        {
            //pluto linkki
            var pluto = new ProcessStartInfo("https://forum.plutonium.pw/user/ultimateman");
            pluto.UseShellExecute = true;
            pluto.Verb = "open";
            Process.Start(pluto);
        }

        private void btnGitHub_MouseEnter(object sender, MouseEventArgs e)
        {
            imgGit1.Opacity = 0.9;
            imgGit2.Opacity = 0.9;
        }

        private void btnGitHub_MouseLeave(object sender, MouseEventArgs e)
        {
            imgGit1.Opacity = 0;
            imgGit2.Opacity = 0;
        }

        private void btnYouTube_MouseEnter(object sender, MouseEventArgs e)
        {
            imgYt1.Opacity = 0.9;
            imgYt2.Opacity = 0.9;

        }

        private void btnYouTube_MouseLeave(object sender, MouseEventArgs e)
        {
            imgYt1.Opacity = 0;
            imgYt2.Opacity = 0;
        }

        private void btnPlutonium_MouseEnter(object sender, MouseEventArgs e)
        {
            imgPluto1.Opacity = 0.9;
            imgPluto2.Opacity = 0.9;
        }

        private void btnPlutonium_MouseLeave(object sender, MouseEventArgs e)
        {
            imgPluto1.Opacity = 0;
            imgPluto2.Opacity = 0;
        }
    }
}
