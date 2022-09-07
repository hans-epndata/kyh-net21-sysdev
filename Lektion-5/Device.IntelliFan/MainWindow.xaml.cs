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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Device.IntelliFan
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private bool isRunning = false;

        private void btnAction_Click(object sender, RoutedEventArgs e)
        {
            var iconRotateBladeStoryBoard = (BeginStoryboard)TryFindResource("iconRotateBladeStoryBoard");

            if(!isRunning)
            {
                iconRotateBladeStoryBoard.Storyboard.Begin();
                isRunning = true;
                btnAction.Content = "Stop";
            }
            else
            {
                iconRotateBladeStoryBoard.Storyboard.Stop();
                isRunning = false;
                btnAction.Content = "Start";
            }
        }
    }
}
