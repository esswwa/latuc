using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace latuc.ViewModels
{
    public class RegistrationViewModel : BindableBase
    {

        private readonly UserService _userService;
        private readonly PageService _pageService;
        public string Username { get; set; }
        public string Password { get; set; }
        public RegistrationViewModel(UserService userService, PageService pageService)
        {
            _userService = userService;
            _pageService = pageService;
        }



    }
}
