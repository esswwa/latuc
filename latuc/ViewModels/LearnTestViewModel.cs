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

        Option test;


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

            test = levelsService.getTest();


            TheoryHeader = test.Question;
            UpLeftQuestion = test.Number1;
            UpRightQuestion = test.Number2;
            DownLeftQuestion = test.Number3;
            DownRightQuestion = test.Number4;
            result = test.Answer;
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



    }
}
