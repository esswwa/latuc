﻿namespace latuc.ViewModels
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

        async void OnEvaluate()
        {
            try
            {
                IsInteractive = false;
                var cmd = CurrentInput;
                CurrentInput = null;
                try
                {
                    var result = await script.ExecuteNext(cmd);
                    LastCommand = new CommandVM(cmd, true, result?.ToString() ?? "<no output>");
                }
                catch (CompilationErrorException ex)
                {
                    LastCommand = new CommandVM(cmd, false, string.Join(" // ", ex.Diagnostics));
                }
                catch (Exception ex)
                {
                    LastCommand = new CommandVM(cmd, false, ex.Message);
                }
                AllCommands.Add(LastCommand);
                CurrentCommand = null;
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
