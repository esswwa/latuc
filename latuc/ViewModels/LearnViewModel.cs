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
        public int RatingTheory { get; set; }

        public DelegateCommand<string> Theory { get; set; }

        public DelegateCommand<string> Test { get; set; }

        public DelegateCommand<string> Practic { get; set; }

        public LearnViewModel(UserService userService, PageService pageService)
        {
            _userService = userService;
            _pageService = pageService;
            RatingTheory = 1;

            Theory = new DelegateCommand<string>(TheoryMethod);
            Test = new DelegateCommand<string>(TestMethod);
            Practic = new DelegateCommand<string>(PracticMethod);
        }

        private void TheoryMethod(string parametr) {
            if (parametr.Contains("Junior"))
            {
                parametr = parametr.Replace("Junior", "");
                MessageBox.Show(parametr + "Junior");
            }
            else if (parametr.Contains("Middle"))
            {
                parametr = parametr.Replace("Middle", "");
                MessageBox.Show(parametr + "Middle");

            }
            else if (parametr.Contains("Senior"))
            {
                parametr = parametr.Replace("Senior", "");
                MessageBox.Show(parametr + "Senior");
            }
        }
        private void TestMethod(string parametr)
        {
            MessageBox.Show(parametr);
        }
        private void PracticMethod(string parametr)
        {
            MessageBox.Show(parametr);
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
