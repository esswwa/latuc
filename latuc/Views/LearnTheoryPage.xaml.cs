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

namespace latuc.Views
{
    /// <summary>
    /// Логика взаимодействия для LearnTheoryPage.xaml
    /// </summary>
    public partial class LearnTheoryPage : Page
    {
        public LearnTheoryPage()
        {
            InitializeComponent();
            this.Loaded += MenuPage_Loaded;
        }

        private async void MenuPage_Loaded(object sender, RoutedEventArgs e)
        {
            await Task.Delay(100);
            DrawerHost.IsLeftDrawerOpen = true;
        }
    }
}
