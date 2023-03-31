using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace latuc.ViewModels
{
    public class RegistrationViewModel : BindableBase
    {

        private readonly UserService _userService;
        private readonly PageService _pageService;
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public RegistrationViewModel(UserService userService, PageService pageService)
        {
            _userService = userService;
            _pageService = pageService;
        }

        public AsyncCommand Registration => new(async () =>
        {
            int maxUser = _userService.GetMaxIdUser() + 1;
            await _userService.AchievementsAsync(maxUser, 0, maxUser, 0);
            await _userService.StatisticsAsync(maxUser, 0, 0, 0, 0, 0);
            await _userService.RegistrationAsync(Email, Login, Password, maxUser, maxUser);
            _pageService.ChangePage(new AuthorizationPage());
        });

        public DelegateCommand Authorization => new(async () => {

            await Application.Current.Dispatcher.InvokeAsync(async () => _pageService.ChangePage(new AuthorizationPage()));
        });

    }
}
