using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace latuc.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly PageService _pageService;
        private readonly LatucCodeContext _latucContext;

        public Page PageSource { get; set; }
        public MainWindowViewModel(PageService pageService, LatucCodeContext latucContext)
        {
            _pageService = pageService;
            _pageService.onPageChanged += (page) => PageSource = page;
            _latucContext = latucContext;


            User user = Proverka();
            if (user == null)
                _pageService.ChangePage(new AuthorizationPage());
            else {
                Settings.Default.idUser = user.Iduser;
                Settings.Default.userLogin = user.Login;
                Settings.Default.userEmail = user.Email;
                Settings.Default.userPassword = user.Password;
                Settings.Default.idStatistic = user.IdStatistics;
                Settings.Default.idAchievments = user.IdAchievemnts;
                Settings.Default.Role = user.Role;
                Settings.Default.exitBool = 1;

                _pageService.ChangePage(new ProfilePage());
            }
        }

        public User Proverka() {
           return _latucContext.Users.Where(u => u.exitBool == 1).FirstOrDefault();
        }
    }
}
