using DevExpress.Mvvm.Native;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using static System.Formats.Asn1.AsnWriter;

namespace latuc.Services
{
    public class UserService
    {
        private readonly LatucCodeContext _latucContext;

        public ObservableCollection<User> Users { get; set; }

        public UserService(LatucCodeContext latucContext)
        {
            _latucContext = latucContext;
        }
        public async Task<bool> AuthorizationAsync(string username, string password)
        {
            var user = await _latucContext.Users.SingleOrDefaultAsync(u => u.Login == username);
            if (user == null)
                return false;
            if (user.Password.Equals(password))
            {
                Settings.Default.idUser = user.Iduser;
                Settings.Default.userLogin = user.Login;
                Settings.Default.userEmail = user.Email;
                Settings.Default.userPassword = user.Password;
                Settings.Default.idStatistic = user.IdStatistics;
                Settings.Default.idAchievments = user.IdAchievemnts;
                Settings.Default.Role = user.Role;
                Settings.Default.exitBool = 1;
                return true;
            }
            return false;
        }

        public async Task<List<User>> getUsers()
        {
            return _latucContext.Users.ToList();
        }

        public int checkScore() {

            return _latucContext.Statistics.First(i => i.Idstatistic == Settings.Default.idUser).Score;
        }

        public async Task RegistrationAsync(string email, string login, string password, int idStatistic, int idAchievemnts, int Role)
        {

            await _latucContext.Users.AddAsync(new User
            {
                Email = email,
                Login = login,
                Password = password,
                IdStatistics = idStatistic,
                IdAchievemnts = idAchievemnts,
                Role = Role,
                ExitBool = 0
            });
            await _latucContext.SaveChangesAsync();
        }

        public async Task AchievementsAsync(int IduserAchievements, int Taken, int IdUser, int IdAchievements)
        {

            await _latucContext.UserAchievements.AddAsync(new UserAchievement
            {
                IduserAchievements = IduserAchievements,
                Taken = Taken,
                IdUser = IdUser,
                IdAchievements = IdAchievements
            });
            await _latucContext.SaveChangesAsync();
        }

        public async Task StatisticsAsync(int Idstatistic, int CountOfPassedLevel, int CountTry, int ResultTest, int LanguageLvl, int Score)
        {

            await _latucContext.Statistics.AddAsync(new Statistic
            {
                Idstatistic = Idstatistic,
                CountOfPassedLevel = CountOfPassedLevel,
                CountTry = CountTry,
                ResultTest = ResultTest,
                LanguageLvl = LanguageLvl,
                Score = Score
            });
            await _latucContext.SaveChangesAsync();
        }

        public async void UpdateProduct()
        {
            var currentOrders = await getUsers();
            Users = new ObservableCollection<User>(currentOrders);
            var item = Users.First(i => i.Iduser == Settings.Default.idUser);
            var index = Users.IndexOf(item);
            item.ExitBool = Settings.Default.exitBool;
            Users.RemoveAt(index);
            Users.Insert(index, item);
            await _latucContext.SaveChangesAsync();
        }


        public async Task editStatisticUser()
        {
            var statistic = _latucContext.Statistics.ToList();
            var scoreUser = _latucContext.LevelsStatistics.Where(i => i.Iduser == Settings.Default.idUser).ToList();
            var item = statistic.First(i => i.Idstatistic == Settings.Default.idUser);
            var index = statistic.IndexOf(item);
            int z = 0;
            int z2 = 0;
            foreach (var item1 in scoreUser) {
                z = z + item1.ScoreTest + item1.ScoreTheory + item1.ScorePractic;

            }

            foreach (var item2 in scoreUser)
            {
                if (item2.ScorePractic > 0 && item2.ScoreTest > 0 && item2.ScoreTheory > 0)
                {
                    z2 += 1;
                }
            }

            item.Score = z;
            item.CountOfPassedLevel = z2;

            if (item.Score >= 20)
            {
                item.LanguageLvl = 1;
            }
            if (item.Score >= 40)
            {
                item.LanguageLvl = 2;
            }
            statistic.RemoveAt(index);
            statistic.Insert(index, item);
            await _latucContext.SaveChangesAsync();
            
           
           


        }


        public void UpdateProductNull()
        {
            Settings.Default.exitBool = 0;
            UpdateProduct();
        }

        public int GetMaxIdUser()
        {

            return _latucContext.Users.Max(u => u.Iduser);
        }
        public int getLastScore(int idStat)
        {

            var a = _latucContext.LevelsStatistics.Where(u => u.Iduser == Settings.Default.idUser && u.Id_level == idStat).First();
            return a.ScoreTest;
        }

        public async Task<List<string>> GetAllUser()
        {

            return await _latucContext.Users.Select(u => u.Login).AsNoTracking().ToListAsync();
        }

        public Statistic userStatistic()
        {
            return _latucContext.Statistics.Where(u => u.Idstatistic == Settings.Default.idStatistic).First();
        }

    }
}

