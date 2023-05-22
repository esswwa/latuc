using latuc.Services;
using Microsoft.EntityFrameworkCore.Query.Internal;
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
        private readonly LevelsService _levelsService;
        public int RatingTheory { get; set; }

        public DelegateCommand<string> Theory { get; set; }

        public DelegateCommand<string> Test { get; set; }

        public DelegateCommand<string> Practic { get; set; }

        public LearnViewModel(UserService userService, PageService pageService, LevelsService levelsService)
        {
            _userService = userService;
            _pageService = pageService;
            _levelsService = levelsService;

                RatingTheory = 1;

            Theory = new DelegateCommand<string>(TheoryMethod);
            Test = new DelegateCommand<string>(TestMethod);
            Practic = new DelegateCommand<string>(PracticMethod);
        }

        Theory theory;
        List<Option> option = new();
        Practic practic;

        private void TheoryMethod(string parametr) {
            if (parametr.Contains("Junior"))
            {
                parametr = parametr.Replace("Junior", "");
                theory = _levelsService.getTheoryFirst(parametr);
                MessageBox.Show(theory.Text);

            }
            else if (parametr.Contains("Middle"))
            {
                parametr = parametr.Replace("Middle", "");
                theory = _levelsService.getTheoryFirst(parametr);

            }
            else if (parametr.Contains("Senior"))
            {
                parametr = parametr.Replace("Senior", "");
                theory = _levelsService.getTheoryFirst(parametr);
            }
        }
        
        private void TestMethod(string parametr)
        {
            if (parametr.Contains("Junior"))
            {
                parametr = parametr.Replace("Junior", "");
                option = _levelsService.getAllOptions(parametr);
                MessageBox.Show(option[1].Question);
            }
            else if (parametr.Contains("Middle"))
            {
                parametr = parametr.Replace("Middle", "");
                option = _levelsService.getAllOptions(parametr);

            }
            else if (parametr.Contains("Senior"))
            {
                parametr = parametr.Replace("Senior", "");
                option = _levelsService.getAllOptions(parametr);
            }
        }
        private void PracticMethod(string parametr)
        {
            if (parametr.Contains("Junior"))
            {
                parametr = parametr.Replace("Junior", "");
                practic = _levelsService.getPracticFirst(parametr);
            }
            else if (parametr.Contains("Middle"))
            {
                parametr = parametr.Replace("Middle", "");
                practic = _levelsService.getPracticFirst(parametr);

            }
            else if (parametr.Contains("Senior"))
            {
                parametr = parametr.Replace("Senior", "");
                practic = _levelsService.getPracticFirst(parametr);
            }
        }

        public DelegateCommand Authorization => new(() =>
        {
            _pageService.ChangePage(new AuthorizationPage());
            _userService.UpdateProductNull();
        });

        public DelegateCommand Profile => new(() => 
        {
            _pageService.ChangePage(new ProfilePage());
        });


    }
}
