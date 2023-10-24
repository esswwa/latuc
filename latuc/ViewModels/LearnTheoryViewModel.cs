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
            TheoryMain = TheoryMain.Replace("/n", "\n");
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
        public DelegateCommand goBack => new(async () => _pageService.ChangePage(new LearnPage()));

        public DelegateCommand endRead => new(() =>
        {
            bool z = _levelService.checkBool(theory.IdTheory);
            if (z == true)
                _levelService.saveRedact(theory.IdTheory);
            else
                _levelService.LevelsStatisticAsync(theory.IdTheory, 0, 0, 0, 0, 0, 1);
            _pageService.ChangePage(new LearnPage());
        });
        

    }
}
