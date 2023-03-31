using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace latuc.ViewModels
{
    public class RegistrationViewModel : BindableBase
    {

        private readonly UserService _userService;
        private readonly PageService _pageService;
        public string Email { get; set; }
        public string Login { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorMessageButton { get; set; }
        public string Password { get; set; }
        private List<string> _userLogin { get; set; } = new();
        public RegistrationViewModel(UserService userService, PageService pageService)
        {
            _userService = userService;
            _pageService = pageService;

            Task.Run(async () => _userLogin = await _userService.GetAllUser());
        }

        public AsyncCommand Registration => new(async () =>
        {
            int maxUser = _userService.GetMaxIdUser() + 1;
            await _userService.AchievementsAsync(maxUser, 0, maxUser, 0);
            await _userService.StatisticsAsync(maxUser, 0, 0, 0, 0, 0);
            await _userService.RegistrationAsync(Email, Login, Password, maxUser, maxUser);
            _pageService.ChangePage(new AuthorizationPage());
        }, bool () => {
            if (string.IsNullOrWhiteSpace(Email)
                || string.IsNullOrWhiteSpace(Login)
                || string.IsNullOrWhiteSpace(Password))
                ErrorMessage = "Обязательно";
            else if (Login.Length != 4)
                ErrorMessage = "Слишком короткий логин";
            else if (_userLogin.Contains(Login))
                ErrorMessage = "Логин занят";
            else
                ErrorMessage = string.Empty;

            return ErrorMessage == string.Empty;
        });

        public DelegateCommand Authorization => new(async () => {

            await Application.Current.Dispatcher.InvokeAsync(async () => _pageService.ChangePage(new AuthorizationPage()));
        });

    }
}
