using latuc.Data.Model;
using latuc.Services;
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


        public DelegateCommand<string> ClickTest { get; set; }
        int result;

        string z = "";

        int checkScoreTest = 0;
        int checkScore = 0;
        public LearnTestViewModel(UserService userService, PageService pageService, LevelsService levelsService)
        {
            _userService = userService;
            _pageService = pageService;
            _levelsService = levelsService;

            test = LevelsInfo.option;


            TheoryHeader = test[checkScore].Question;
            UpLeftQuestion = test[checkScore].Number1;
            UpRightQuestion = test[checkScore].Number2;
            DownLeftQuestion = test[checkScore].Number3;
            DownRightQuestion = test[checkScore].Number4;
            result = test[checkScore].Answer;
            if (result == 1) {
                z = "UpLeftQuestion";
            }
            else if (result == 2)
            {
                z = "UpRightQuestion";
            }
            else if (result == 3)
            {
                z = "DownLeftQuestion";
            }
            else if (result == 4)
            {
                z = "DownRightQuestion";
            }
            ClickTest = new DelegateCommand<string>(TheoryMethod);
        }

        public DelegateCommand Authorization => new(() =>
        {
            _pageService.ChangePage(new AuthorizationPage());
            _userService.UpdateProductNull();
        });
        private void TheoryMethod(string parametr)
        {
          
            if (parametr.Contains("UpLeftQuestion"))
            {
                if (z == "UpLeftQuestion")
                {

                    MessageBox.Show("Правильный ответ");
                    checkScoreTest += 1;
                    checkScore += 1;

                }

                else
                {
                    MessageBox.Show("Неправильный ответ");
                    checkScore += 1;

                }
            }
            else if (parametr.Contains("UpRightQuestion"))
            {
                if (z == "UpRightQuestion")
                {
                    checkScoreTest += 1;
                    MessageBox.Show("Правильный ответ");
                    checkScore += 1;

                }

                else
                {
                    MessageBox.Show("Неправильный ответ");
                    checkScore += 1;

                }
               
            }
            else if (parametr.Contains("DownLeftQuestion"))
            {
                if (z == "DownLeftQuestion")
                {
                    checkScoreTest += 1;

                    MessageBox.Show("Правильный ответ");
                    checkScore += 1;

                }

                else
                {
                    MessageBox.Show("Неправильный ответ");
                    checkScore += 1;

                }
            }
            else if (parametr.Contains("DownRightQuestion"))
            {
                if (z == "DownRightQuestion")
                {
                    checkScoreTest += 1;
                    MessageBox.Show("Правильный ответ");
                    checkScore += 1;

                }

                else
                {
                    MessageBox.Show("Неправильный ответ");
                    checkScore += 1;

                }
            }

            if (checkScore <= 2)
            {
                TheoryHeader = test[checkScore].Question;
                UpLeftQuestion = test[checkScore].Number1;
                UpRightQuestion = test[checkScore].Number2;
                DownLeftQuestion = test[checkScore].Number3;
                DownRightQuestion = test[checkScore].Number4;
                result = test[checkScore].Answer;
            }
            else
            {
                MessageBox.Show("Вы прошли тест");
                _pageService.ChangePage(new LearnPage());

               

                bool z = _levelsService.checkBool(test[0].Idoption / 3);
                
                int z1 = _userService.getLastScore(test[0].Idoption / 3);

                if (z == true)
                     _levelsService.saveRedactTest(test[0].Idoption / 3, checkScoreTest);
                else
                     _levelsService.LevelsStatisticAsync(test[0].Idoption / 3, checkScoreTest, 0, 0, 0, 0, 0);

                checkScoreTest = checkScoreTest - z1;
                
                if (checkScoreTest > 0)
                    _userService.editStatisticUser();

                MessageBox.Show(checkScoreTest.ToString());

                _pageService.ChangePage(new LearnPage());
            }

        }

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
