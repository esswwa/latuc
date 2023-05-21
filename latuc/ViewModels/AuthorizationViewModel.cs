using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace latuc.ViewModels
{
    public class AuthorizationViewModel : BindableBase
    {
        //private readonly UserService _userService;
        //private readonly PageService _pageService;
        //private readonly LatucCodeContext _latucContext;

        //public string Username { get; set; }
        //public string Password { get; set; }
        //public string ErrorMessage { get; set; }
        //public string ErrorMessageButton { get; set; }

        //public ObservableCollection<User> Users { get; set; }

        //public AuthorizationViewModel(UserService userService, PageService pageService, LatucCodeContext latucContext)
        //{
        //    _userService = userService;
        //    _pageService = pageService;
        //    _latucContext = latucContext;
        //}


        //public AsyncCommand Authorization => new(async () =>
        //{

        //    await Task.Run(async () =>
        //    {

        //        if (await _userService.AuthorizationAsync(Username, Password))
        //        {
        //            await Application.Current.Dispatcher.InvokeAsync(async () => _pageService.ChangePage(new LearnPage()));
        //            _userService.UpdateProduct();
        //        }
        //        else
        //        {
        //            ErrorMessageButton = "Неверный логин или пароль";
        //        }
        //    });
        //}, bool () =>
        //{

        //    if (string.IsNullOrWhiteSpace(Username)
        //          || string.IsNullOrWhiteSpace(Password))
        //    {
        //        ErrorMessage = "Пустые поля";
        //        ErrorMessageButton = ErrorMessageButton != string.Empty ? string.Empty : ErrorMessageButton;
        //    }
        //    else
        //        ErrorMessage = string.Empty;

        //    return ErrorMessage == string.Empty;
        //});

        //public DelegateCommand Registration => new(async () =>
        //{
        //    await Application.Current.Dispatcher.InvokeAsync(async () => _pageService.ChangePage(new RegistrationPage()));
        //});

      
    }
}