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
        public async Task<List<Level>> getAllTest()
        {
            return await _latucContext.Levels.Where(u => u.Idlevels == 1).AsNoTracking().ToListAsync();
        }
        public async Task LevelsStatisticAsync(int scoreTest, int countTryTest, int scorePractic, int levelComplete, int countTryPractic, int scoreTheory)
        {
            DateOnly dateOnly = DateOnly.FromDateTime(DateTime.Now);
            await _latucContext.LevelsStatistics.AddAsync(new LevelsStatistic
            {
                Iduser = Settings.Default.idUser,
                ScoreTest = scoreTest,
                Date = dateOnly,
                CountTryTest = countTryTest,
                ScorePractic = scorePractic,
                LevelComplete = levelComplete,
                CountTryPractic = countTryPractic,
                ScoreTheory = scoreTheory
            });
            await _latucContext.SaveChangesAsync();
        }

        public async void UpdateProduct()
        {
            //var currentOrders = await getUsers();
            //Users = new ObservableCollection<User>(currentOrders);
            //var item = Users.First(i => i.Iduser == Settings.Default.idUser);
            //var index = Users.IndexOf(item);
            //item.ExitBool = Settings.Default.exitBool;
            //Users.RemoveAt(index);
            //Users.Insert(index, item);
            //await _latucContext.SaveChangesAsync();
        }

        public void UpdateProductNull()
        {
            //Settings.Default.exitBool = 0;
            //UpdateProduct();
        }

    }
}
