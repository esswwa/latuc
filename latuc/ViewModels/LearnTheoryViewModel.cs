using latuc.Data.Model;
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
        private readonly LevelsService _levelService;
        public String TheoryMain { get; set;}
        Theory theory;

        public LearnTheoryViewModel(UserService userService, PageService pageService, LevelsService levelService)
        {
            _userService = userService;
            _pageService = pageService;
            _levelService = levelService;
            theory = LevelsInfo.theory;
            TheoryMain = theory.Text;
        }

        public DelegateCommand Authorization => new(() =>
        {
            _pageService.ChangePage(new AuthorizationPage());
            _userService.UpdateProductNull();
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
