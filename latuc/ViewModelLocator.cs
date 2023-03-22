namespace latuc
{
    public class ViewModelLocator
    {
        private static ServiceProvider? _provider;
        private static IConfiguration? _configuration;
        public static void Init()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
            _configuration = builder.Build();

            var services = new ServiceCollection();

            #region ViewModel

            services.AddTransient<MainWindowViewModel>();
            services.AddTransient<AuthorizationViewModel>();
            services.AddTransient<AchivementsViewModel>();
            services.AddTransient<LearnPracticViewModel>();
            services.AddTransient<LearnTestViewModel>();
            services.AddTransient<LearnTheoryViewModel>();
            services.AddTransient<MenuViewModel>();
            services.AddTransient<ProfileViewModel>();
            services.AddTransient<TestPageViewModel>();
            services.AddTransient<LearnViewModel>();
            services.AddTransient<RegistrationViewModel>();

            #endregion

            #region Connection

            services.AddDbContext<LatucCodeContext>(options =>
            {
                try
                {
                    var conn = _configuration.GetConnectionString("LocalConnection");
                    options.UseMySql(conn, ServerVersion.AutoDetect(conn));
                }
                catch (MySqlConnector.MySqlException) { }
            }, ServiceLifetime.Singleton);

            #endregion

            #region Services

            services.AddSingleton<PageService>();
            services.AddSingleton<UserService>();

            #endregion

            _provider = services.BuildServiceProvider();

        }
        public MainWindowViewModel? MainWindowViewModel => _provider?.GetRequiredService<MainWindowViewModel>();
        public AuthorizationViewModel? AuthorizationViewModel => _provider?.GetRequiredService<AuthorizationViewModel>();
        public AchivementsViewModel? AchivementsViewModel => _provider?.GetRequiredService<AchivementsViewModel>();
        public LearnPracticViewModel? LearnPracticViewModel => _provider?.GetRequiredService<LearnPracticViewModel>();
        public LearnTestViewModel? LearnTestViewModel => _provider?.GetRequiredService<LearnTestViewModel>();
        public LearnTheoryViewModel? LearnTheoryViewModel => _provider?.GetRequiredService<LearnTheoryViewModel>();
        public MenuViewModel? MenuViewModel => _provider?.GetRequiredService<MenuViewModel>();
        public ProfileViewModel? ProfileViewModel => _provider?.GetRequiredService<ProfileViewModel>();
        public TestPageViewModel? TestPageViewModel => _provider?.GetRequiredService<TestPageViewModel>();
        public LearnViewModel? LearnViewModel => _provider?.GetRequiredService<LearnViewModel>();

        public RegistrationViewModel? RegistrationViewModel => _provider?.GetRequiredService<RegistrationViewModel>();
    }
}
