using latuc.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace latuc.ViewModels
{
    public class AchivementsViewModel : BindableBase
    {
        private readonly UserService _userService;
        private readonly PageService _pageService;
        public AchivementsViewModel(UserService userService, PageService pageService)
        {
            _userService = userService;
            _pageService = pageService;
        }

        public DelegateCommand Authorization => new(() =>
        {
            _pageService.ChangePage(new AuthorizationPage());
            _userService.UpdateProductNull();
        });

        public DelegateCommand Practic => new(() =>
        {
            _pageService.ChangePage(new LearnPracticPage());
        });

        public DelegateCommand Theory => new(() =>
        {
            _pageService.ChangePage(new LearnTheoryPage());
        });
        public DelegateCommand Levels => new(() =>
        {
            _pageService.ChangePage(new LearnPage());
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
