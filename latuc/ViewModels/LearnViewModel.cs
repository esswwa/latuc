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

        public int RatingTheoryStructure { get; set; }
        public int RatingTheoryPeremen { get; set; }
        public int RatingTheoryType { get; set; }
        public int RatingTheoryConstruct { get; set; }
        public int RatingTheoryCicle { get; set; }
        public int RatingTheoryMassive { get; set; }
        public int RatingTheoryReturn { get; set; }
        public int RatingTheorySwitch { get; set; }
        public int RatingTheoryRecursive { get; set; }

        public int RatingTestStructure { get; set; }
        public int RatingTestPeremen { get; set; }
        public int RatingTestType { get; set; }
        public int RatingTestConstruct { get; set; }
        public int RatingTestCicle { get; set; }
        public int RatingTestMassive { get; set; }
        public int RatingTestReturn { get; set; }
        public int RatingTestSwitch { get; set; }
        public int RatingTestRecursive { get; set; }

        public int RatingPracticStructure { get; set; }
        public int RatingPracticPeremen { get; set; }
        public int RatingPracticType { get; set; }
        public int RatingPracticConstruct { get; set; }
        public int RatingPracticCicle { get; set; }
        public int RatingPracticMassive { get; set; }
        public int RatingPracticReturn { get; set; }
        public int RatingPracticSwitch { get; set; }
        public int RatingPracticRecursive { get; set; }

        public DelegateCommand<string> Theory { get; set; }

        public DelegateCommand<string> Test { get; set; }

        public DelegateCommand<string> Practic { get; set; }

        public LearnViewModel(UserService userService, PageService pageService, LevelsService levelsService)
        {
            _userService = userService;
            _pageService = pageService;
            _levelsService = levelsService;

            List<LevelsStatistic> levels = _levelsService.getLevelRating();

            if (levels != null)
            {
                foreach (var item in levels)
                {
                    switch (item.Idlevels.ToString())
                    {
                        case ("0"):
                            {
                                RatingTheoryStructure = item.ScoreTheory;
                                RatingTestStructure = item.ScoreTest;
                                RatingPracticStructure = item.ScorePractic;
                            }
                            break;
                        case ("1"):
                            {
                                RatingTheoryPeremen = item.ScoreTheory;
                                RatingTestPeremen = item.ScoreTest;
                                RatingPracticPeremen = item.ScorePractic;
                            }
                            break;
                        case ("2"):
                            {
                                RatingTheoryType = item.ScoreTheory;
                                RatingTestType = item.ScoreTest;
                                RatingPracticType = item.ScorePractic;
                            }
                            break;
                        case ("3"):
                            {
                                RatingTheoryConstruct = item.ScoreTheory;
                                RatingTestConstruct = item.ScoreTest;
                                RatingPracticConstruct = item.ScorePractic;
                            }
                            break;
                        case ("4"):
                            {
                                RatingTheoryCicle = item.ScoreTheory;
                                RatingTestCicle = item.ScoreTest;
                                RatingPracticCicle = item.ScorePractic;
                            }
                            break;
                        case ("5"):
                            {
                                RatingTheoryMassive = item.ScoreTheory;
                                RatingTestMassive = item.ScoreTest;
                                RatingPracticMassive = item.ScorePractic;
                            }
                            break;
                        case ("6"):
                            {
                                RatingTheoryReturn = item.ScoreTheory;
                                RatingTestReturn = item.ScoreTest;
                                RatingPracticReturn = item.ScorePractic;
                            }
                            break;
                        case ("7"):
                            {
                                RatingTheorySwitch = item.ScoreTheory;
                                RatingTestSwitch = item.ScoreTest;
                                RatingPracticSwitch = item.ScorePractic;
                            }
                            break;
                        case ("8"):
                            {
                                RatingTheoryRecursive = item.ScoreTheory;
                                RatingTestRecursive = item.ScoreTest;
                                RatingPracticRecursive = item.ScorePractic;
                            }
                            break;
                       

                    }
                   
                    
                }
            }
            

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
                LevelsInfo.theory = theory;
                _pageService.ChangePage(new LearnTheoryPage());

            }
            else if (parametr.Contains("Middle"))
            {
                parametr = parametr.Replace("Middle", "");
                theory = _levelsService.getTheoryFirst(parametr);
                LevelsInfo.theory = theory;
                _pageService.ChangePage(new LearnTheoryPage());

            }
            else if (parametr.Contains("Senior"))
            {
                parametr = parametr.Replace("Senior", "");
                theory = _levelsService.getTheoryFirst(parametr);
                LevelsInfo.theory = theory;
                _pageService.ChangePage(new LearnTheoryPage());
            }
        }
        
        private void TestMethod(string parametr)
        {
            if (parametr.Contains("Junior"))
            {
                parametr = parametr.Replace("Junior", "");
                option = _levelsService.getAllOptions(parametr);
                LevelsInfo.option = option;
                _pageService.ChangePage(new LearnTestPage());
            }
            else if (parametr.Contains("Middle"))
            {
                parametr = parametr.Replace("Middle", "");
                option = _levelsService.getAllOptions(parametr);
                LevelsInfo.option = option;
                _pageService.ChangePage(new LearnTestPage());


            }
            else if (parametr.Contains("Senior"))
            {
                parametr = parametr.Replace("Senior", "");
                option = _levelsService.getAllOptions(parametr);
                LevelsInfo.option = option;
                _pageService.ChangePage(new LearnTestPage());

            }
        }
        private void PracticMethod(string parametr)
        {
            if (parametr.Contains("Junior"))
            {
                parametr = parametr.Replace("Junior", "");
                practic = _levelsService.getPracticFirst(parametr);
                LevelsInfo.pratic = practic;
                _pageService.ChangePage(new LearnPracticPage());

            }
            else if (parametr.Contains("Middle"))
            {
                parametr = parametr.Replace("Middle", "");
                practic = _levelsService.getPracticFirst(parametr);
                LevelsInfo.pratic = practic;
                _pageService.ChangePage(new LearnPracticPage());

            }
            else if (parametr.Contains("Senior"))
            {
                parametr = parametr.Replace("Senior", "");
                practic = _levelsService.getPracticFirst(parametr);
                LevelsInfo.pratic = practic;
                _pageService.ChangePage(new LearnPracticPage());
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
