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
        private void ReadProButton(Object context)
        {
            System.Windows.Controls.Button btnClicked = (System.Windows.Controls.Button)context;
            string processName = btnClicked.Name;
            MessageBox.Show(processName);
        }

        private async void MenuPage_Loaded(object sender, RoutedEventArgs e)
        {
            await Task.Delay(100);
            DrawerHost.IsLeftDrawerOpen = true;
        }
    }
}
