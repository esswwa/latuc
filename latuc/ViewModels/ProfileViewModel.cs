using MaterialDesignColors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace latuc.ViewModels
{
    public class ProfileViewModel : BindableBase
    {
        private readonly UserService _userService;
        private readonly PageService _pageService;


        public string Login { get; set; }

        public string LanguageLevel { get; set; }

        public int CountPassLevel { get; set; }

        public int CountTry { get; set; }

        public string ResultTest { get; set; }
        public string Score { get; set; }


        public ProfileViewModel(UserService userService, PageService pageService)
        {
            _userService = userService;
            _pageService = pageService;
            UpdateProduct();
        }

        public DelegateCommand Authorization => new(() =>
        {
            _pageService.ChangePage(new AuthorizationPage());
            _userService.UpdateProductNull();
        });

        public DelegateCommand Test => new(() =>
        {
            _pageService.ChangePage(new TestPage());
        });

        public DelegateCommand Achievements => new(() =>
        {
            _pageService.ChangePage(new Achivements());
        });

        public DelegateCommand Levels => new(() =>
        {
            _pageService.ChangePage(new LearnPage());
        });

        public DelegateCommand Profile => new(() =>
        {
            _pageService.ChangePage(new ProfilePage());
        });

        private async void UpdateProduct()
        {
            Statistic Statistics = _userService.userStatistic();
            Login = Settings.Default.userLogin;
            Score =  _userService.checkScore().ToString();

            if (Statistics.LanguageLvl == 0)
                LanguageLevel = "Junior";
            else if (Statistics.LanguageLvl == 1)
                LanguageLevel = "Middle";
            else
                LanguageLevel = "Senior";

            CountPassLevel = Statistics.CountOfPassedLevel;
            CountTry = Statistics.CountTry;
            if (Statistics.ResultTest == 0)
                ResultTest = "Junior";
            else if (Statistics.ResultTest == 1)
                ResultTest = "Middle";
            else
                ResultTest = "Senior";
        }








    }
}
