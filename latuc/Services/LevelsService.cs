using MaterialDesignThemes.Wpf;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Ink;

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
        public async Task LevelsStatisticAsync(int id_level,int scoreTest, int countTryTest, int scorePractic, int levelComplete, int countTryPractic, int scoreTheory)
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
                ScoreTheory = scoreTheory,
                Id_level = id_level
            });
            await _latucContext.SaveChangesAsync();
        }
        public async Task LevelsStatisticAsyncTest(int id_level, int scoreTest, int countTryTest, int scorePractic, int levelComplete, int countTryPractic, int scoreTheory)
        {
            DateOnly dateOnly = DateOnly.FromDateTime(DateTime.Now);
            await _latucContext.LevelsStatistics.AddAsync(new LevelsStatistic
            {
                Iduser = Settings.Default.idUser,
                ScoreTest = scoreTest,
                Date = dateOnly,
                CountTryTest = 1,
                ScorePractic = scorePractic,
                LevelComplete = levelComplete,
                CountTryPractic = countTryPractic,
                ScoreTheory = scoreTheory,
                Id_level = id_level
            });
            await _latucContext.SaveChangesAsync();
        }

        public async Task saveRedact(int idlevel)
        {
            var Levels = _latucContext.LevelsStatistics.ToList();

            var item = Levels.First(i => i.Id_level == idlevel && i.Iduser == Settings.Default.idUser);
            var index = Levels.IndexOf(item);
            if (item.ScoreTheory != 1)
            {
                item.ScoreTheory = 1;
                Levels.RemoveAt(index);
                Levels.Insert(index, item);
                await _latucContext.SaveChangesAsync();
            }
            
           
        }
        public async Task saveRedactTest(int idlevel, int scoreTest)
        {
            var Levels = _latucContext.LevelsStatistics.ToList();

            var item = Levels.First(i => i.Id_level == idlevel && i.Iduser == Settings.Default.idUser);
            var index = Levels.IndexOf(item);
            
                item.ScoreTest = scoreTest;
                item.CountTryTest = item.CountTryTest + 1;
                Levels.RemoveAt(index);
                Levels.Insert(index, item);
                await _latucContext.SaveChangesAsync();
          


        }


        public bool checkBool(int idTheory) {
            try
            {
                if (_latucContext.LevelsStatistics.Where(i => i.Iduser == Settings.Default.idUser && i.Id_level == idTheory).First() != null)
                    return true;
                else
                    return false; 
            }
            catch(Exception ex)
            {
                return false;
            }
           

        }

        public Theory getTheoryFirst(string peremen)
        {
            Level levls;
            levls = _latucContext.Levels.Where(u => u.Theme.Contains(peremen)).First();
            return _latucContext.Theories.Where(u => u.IdTheory == levls.IdTheory).First();
        }

        public Practic getPracticFirst(string peremen)
        {
            Level levls;
            levls = _latucContext.Levels.Where(u => u.Theme.Contains(peremen)).First(); 
            return _latucContext.Practics.Where(u => u.Idpractic == levls.Practic).First();
        }

        public List<LevelsStatistic> getLevelRating()
        {
            List<LevelsStatistic> levls;
            levls = _latucContext.LevelsStatistics.Where(u => u.Iduser == Settings.Default.idUser).ToList();
            if (levls != null)
                return levls;
            else
                return null;
        }

        public List<Option> getAllOptions(string peremen)
        {
            Level levls;
            List<Option> optis = new();
            Option opt;
            levls = _latucContext.Levels.Where(u => u.Theme.Contains(peremen)).First();
            opt = _latucContext.Options.Where(u => u.Idoption == levls.Options).First();
            optis.Add(opt);
            opt = _latucContext.Options.Where(u => u.Idoption == levls.Options+1).First();
            optis.Add(opt);
            opt = _latucContext.Options.Where(u => u.Idoption == levls.Options+2).First();
            optis.Add(opt);
            return optis;
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
