namespace latuc.Views
{
    /// <summary>
    /// Логика взаимодействия для LearnPage.xaml
    /// </summary>
    public partial class LearnPage : Page
    {
        public LearnPage()
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
