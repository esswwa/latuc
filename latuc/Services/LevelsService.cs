using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace latuc.Services
{
    public class LevelsService
    {
        private readonly LatucCodeContext _latucContext;

        public ObservableCollection<User> Users { get; set; }

        public LevelsService(LatucCodeContext latucContext)
        {
            _latucContext = latucContext;
        }

        public Theory getTheory()
        {
            return _latucContext.Theories.Where(u => u.IdTheory == 2).First();
        }
        public Option getTest()
        {
            return _latucContext.Options.Where(u => u.Idoption == 2).First();
        }
        public Practic getPractic()
        {
            return _latucContext.Practics.Where(u => u.Idpractic == 2).First();
        }

    }
}
