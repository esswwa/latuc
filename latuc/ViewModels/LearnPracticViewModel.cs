using DevExpress.Internal.WinApi.Windows.UI.Notifications;
using latuc.Data.Model;
using latuc.Services;

namespace latuc.ViewModels
{
    public class CommandVM : VM
    {
        public string Text { get; }
        public bool IsSuccessful { get; }
        public string Result { get; }

        public CommandVM(string text, bool isSuccessful, string result)
        {
            Text = text;
            IsSuccessful = isSuccessful;
            Result = result;
        }
    }
    public class LearnPracticViewModel : VM
    {
        public String TheoryMain { get; set; }

        Practic practic;

        private readonly UserService _userService;
        private readonly PageService _pageService;
        private readonly LevelsService _levelsService;

        public string answer;
        public LearnPracticViewModel(UserService userService, PageService pageService, LevelsService levelsService)
        {
            _userService = userService;
            _pageService = pageService;
                IsInteractive = false;
                Evaluate = new RelayCommand(o => OnEvaluate());
                GoUp = new RelayCommand(o => OnGo(-1));
                GoDown = new RelayCommand(o => OnGo(1));
                Initialize();
            _levelsService = levelsService;

            practic = LevelsInfo.pratic;
            TheoryMain = practic.Question;

            answer = practic.Answer;
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


        public ObservableCollection<CommandVM> AllCommands { get; } = new ObservableCollection<CommandVM>();
        public ObservableCollection<int> AllCommandsErrors { get; set; } = new ObservableCollection<int>();

        public ICommand Evaluate { get; }
        public ICommand GoUp { get; }
        public ICommand GoDown { get; }
        public ICommand Enter { get; }

        string currentInput;
        public string CurrentInput
        {
            get => currentInput;
            set => Set(ref currentInput, value);
        }

        CommandVM lastCommand;
        public CommandVM LastCommand
        {
            get => lastCommand;
            private set => Set(ref lastCommand, value);
        }

        public CommandVM LastCommandCheck
        {
            get => lastCommand;
            private set => Set(ref lastCommand, value);
        }


        CommandVM currentCommand;
        public CommandVM CurrentCommand
        {
            get => currentCommand;
            set { if (Set(ref currentCommand, value)) CurrentInput = currentCommand?.Text; }
        }

        bool isInteractive;
        public bool IsInteractive
        {
            get => isInteractive;
            private set => Set(ref isInteractive, value);
        }

        ScriptsForCompiler.Script script;

        

        static Assembly[] references = new[]
            {
                typeof(object).Assembly,
                typeof(Uri).Assembly,
                typeof(Enumerable).Assembly,
                typeof(MessageBox).Assembly
            };

        static string[] usings = new[]
            {
                "System",
                "System.IO",
                "System.Text",
                "System.Windows",
                "System.Threading.Tasks",
                "System.Windows",
                "System.Windows.Controls"
            };

        async void Initialize()
        {
            script = await ScriptsForCompiler.Script.Create(references, usings);
            IsInteractive = true;
        }
        
        public int score = 0;
        async void OnEvaluate()
        {
            
            try
            {
                string output = "";
                IsInteractive = false;
                var cmd = CurrentInput;
                CurrentInput = null;
                StringWriter sw = new StringWriter();
                Console.SetOut(sw);
                try
                {
                    var result = await script.ExecuteNext(cmd);
                    output = sw.ToString();
                    if(output != "")
                        LastCommand = new CommandVM(cmd, true, result?.ToString() ?? output);

                    else
                        LastCommand = new CommandVM(cmd, true, result?.ToString() ?? "<no output>");
                }
                catch (CompilationErrorException ex)
                {
                    LastCommand = new CommandVM(cmd, false, string.Join(" // ", ex.Diagnostics));
                }
                catch (Exception ex)
                {
                    LastCommand = new CommandVM(cmd, false, ex.Message);
                    LastCommandCheck = new CommandVM(cmd, false, ex.Message);
                }
                if (output.ToString().Contains(answer) || output.ToString() == answer)
                {
                    LastCommand = new CommandVM(cmd, true, output + "Ответ верный");

                    foreach (var item in AllCommands)
                    {
                        if (item.Result.Contains("error"))
                        {
                            AllCommandsErrors.Add(1);
                        }
                    }

                    if (AllCommandsErrors.Count == 0 && AllCommands.Count + 1 >= 2 || (practic.Idpractic == 0 && AllCommandsErrors.Count == 0))
                    {
                        score = 5;
                    }
                    else if (AllCommandsErrors.Count == 1 && AllCommands.Count + 1 >= 3 || (practic.Idpractic == 0 && AllCommandsErrors.Count == 1))
                    {
                        score = 4;
                    }
                    else if (AllCommandsErrors.Count == 2 && AllCommands.Count + 1 >= 4 || (practic.Idpractic == 0 && AllCommandsErrors.Count == 2))
                    {
                        score = 3;
                    }
                    else if (AllCommandsErrors.Count == 3 && AllCommands.Count + 1 >= 5 || (practic.Idpractic == 0 && AllCommandsErrors.Count == 3))
                    {
                        score = 2;
                    }
                    else if (AllCommandsErrors.Count == 4 && AllCommands.Count + 1 >= 6 || (practic.Idpractic == 0 && AllCommandsErrors.Count == 4))
                    {
                        score = 1;
                    }
                    else
                    {
                        score = 0;
                    }

                    
                    MessageBox.Show(score.ToString());

                    bool z = _levelsService.checkBool(practic.Idpractic);
                    if (z == true)
                        await _levelsService.saveRedactPractic(practic.Idpractic, score);
                    else
                        await _levelsService.LevelsStatisticPracticAsync(practic.Idpractic, 0, 0, score, 0, 1, 0);

                    await _userService.editStatisticUser();
                    MessageBox.Show("Практика успешно завершена");
                    _pageService.ChangePage(new LearnPage());
                }
                else {
                    AllCommands.Add(LastCommand);
                    CurrentCommand = null;
                }

            }
            finally
            {
                IsInteractive = true;
            }
        }

        void OnGo(int delta)
        {
            int idx;
            if (CurrentCommand == null)
            {
                idx = delta > 0 ? 0 : AllCommands.Count - 1;
            }
            else
            {
                idx = AllCommands.IndexOf(CurrentCommand);
                if (idx == -1)
                    return;
                idx += delta;
            }
            if (idx < 0 || idx >= AllCommands.Count)
                return;
            CurrentCommand = AllCommands[idx];
        }
        void eNT()
        {

        }
    }
}
