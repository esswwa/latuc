using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace latuc.ViewModels
{
    public class AuthorizationViewModel : BindableBase
    {
        private readonly UserService _userService;
        private readonly PageService _pageService;
        public string Username { get; set; }
        public string Password { get; set; }
        public AuthorizationViewModel(UserService userService, PageService pageService)
        {
            _userService = userService;
            _pageService = pageService;
        }


        public AsyncCommand Authorization => new(async () =>
        {

            await Task.Run(async () =>
            {

                if (await _userService.AuthorizationAsync(Username, Password))
                {
                    MessageBox.Show("fdsfsdfds");
                    await Application.Current.Dispatcher.InvokeAsync(async () => _pageService.ChangePage(new MenuPage()));
                }
                else
                {

                    MessageBox.Show("Let`s try");
                }
            });
        });
    }
}
