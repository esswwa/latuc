using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace latuc.ViewModels
{
    public class LearnTestViewModel : BindableBase
    {
        private readonly UserService _userService;
        private readonly PageService _pageService;
        private readonly LevelsService _levelsService;

        List<Option> test;


        public String TheoryHeader { get; set; }
        public String UpLeftQuestion { get; set; }
        public String DownLeftQuestion { get; set; }
        public String UpRightQuestion { get; set; }
        public String DownRightQuestion { get; set; }

        int result;


        public LearnTestViewModel(UserService userService, PageService pageService, LevelsService levelsService)
        {
            _userService = userService;
            _pageService = pageService;
            _levelsService = levelsService;

            test = LevelsInfo.option;


            TheoryHeader = test[0].Question;
            UpLeftQuestion = test[0].Number1;
            UpRightQuestion = test[0].Number2;
            DownLeftQuestion = test[0].Number3;
            DownRightQuestion = test[0].Number4;
            result = test[0].Answer;
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
