using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace latuc.ViewModels
{
    public class LearnViewModel : BindableBase
    {
        private readonly UserService _userService;
        private readonly PageService _pageService;
        public LearnViewModel(UserService userService, PageService pageService)
        {
            _userService = userService;
            _pageService = pageService;
        }

        public DelegateCommand Authorization => new(() =>
        {
            _pageService.ChangePage(new AuthorizationPage());
        });

        public DelegateCommand Practic => new(() =>
        {
            _pageService.ChangePage(new LearnPracticPage());
        });

        public DelegateCommand Theory => new(() =>
        {
            _pageService.ChangePage(new LearnTheoryPage());
        });

        public DelegateCommand Test => new(() =>
        {
             _pageService.ChangePage(new LearnTestPage());
        });

        public DelegateCommand Profile => new(() => 
        {
            _pageService.ChangePage(new ProfilePage());
        });


    }
}
