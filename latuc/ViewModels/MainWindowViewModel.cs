using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace latuc.ViewModels
{
    public class MainWindowViewModel
    {
        private readonly PageService _pageService;
        public Page PageSource { get; set; }
        public MainWindowViewModel(PageService pageService)
        {
            _pageService = pageService;

            _pageService.onPageChanged += (page) => PageSource = page;

            //_pageService.ChangePage(new AuthorizationPage());
        }
    }
}
