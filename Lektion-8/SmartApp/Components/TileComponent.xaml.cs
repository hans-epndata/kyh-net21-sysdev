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

namespace SmartApp.Components
{
    /// <summary>
    /// Interaction logic for TileComponent.xaml
    /// </summary>
    public partial class TileComponent : UserControl
    {
        public TileComponent()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TitleProperty = 
            DependencyProperty.Register("Title", typeof(string), typeof(TileComponent));
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty IsCheckedProperty =
            DependencyProperty.Register("IsChecked", typeof(bool), typeof(TileComponent));
        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }

        public static readonly DependencyProperty IconActiveProperty =
            DependencyProperty.Register("IconActive", typeof(string), typeof(TileComponent));
        public string IconActive
        {
            get { return (string)GetValue(IconActiveProperty); }
            set { SetValue(IconActiveProperty, value); }
        }

        public static readonly DependencyProperty IconInActiveProperty =
            DependencyProperty.Register("IconInActive", typeof(string), typeof(TileComponent));
        public string IconInActive
        {
            get { return (string)GetValue(IconInActiveProperty); }
            set { SetValue(IconInActiveProperty, value); }
        }

    }
}
