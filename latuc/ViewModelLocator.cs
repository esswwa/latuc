using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            services.AddTransient<MainWindow>();
            //services.AddTransient<SignInViewModel>();
            //services.AddTransient<SignUpViewModel>();
            //services.AddTransient<BrowseProductViewModel>();
            //services.AddTransient<BasketInfoViewModel>();
            #endregion

            #region Connection

            //services.AddDbContext<TradeContext>(options =>
            //{
            //    try
            //    {
            //        var conn = _configuration.GetConnectionString("LocalConnection");
            //        options.UseMySql(conn, ServerVersion.AutoDetect(conn));
            //    }
            //    catch (MySqlConnector.MySqlException) { }
            //}, ServiceLifetime.Singleton);

            #endregion

            #region Services

            //services.AddSingleton<PageService>();
            //services.AddSingleton<UserService>();
            //services.AddSingleton<ProductService>();
            //services.AddSingleton<DocumentService>();

            #endregion

            _provider = services.BuildServiceProvider();
            //foreach (var service in services)
            //{
            //    _provider.GetRequiredService(service.ServiceType);
            //}
        }
        public MainWindow? mWindowViewModel => _provider?.GetRequiredService<MainWindow>();
        //public SignInViewModel? SignInViewModel => _provider?.GetRequiredService<SignInViewModel>();
        //public SignUpViewModel? SignUpViewModel => _provider?.GetRequiredService<SignUpViewModel>();
        //public BrowseProductViewModel? BrowseProductViewModel => _provider?.GetRequiredService<BrowseProductViewModel>();
        //public BasketInfoViewModel? BasketInfoViewModel => _provider?.GetRequiredService<BasketInfoViewModel>();
    }
}
