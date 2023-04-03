using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace latuc.ViewModels
{
    public class LearnTheoryViewModel : BindableBase
    {
        private readonly UserService _userService;
        private readonly PageService _pageService;
        public LearnTheoryViewModel(UserService userService, PageService pageService)
        {
            _userService = userService;
            _pageService = pageService;
        }

        public DelegateCommand Authorization => new(() =>
        {
            _pageService.ChangePage(new AuthorizationPage());
        });

        public DelegateCommand Levels => new(() =>
        {
            _pageService.ChangePage(new LearnPage());
        });

        public DelegateCommand Profile => new(() =>
        {
            _pageService.ChangePage(new ProfilePage());
        });


    }
}
