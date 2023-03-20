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

    }
}
