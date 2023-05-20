using DevExpress.Mvvm.Native;
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

        public async Task RegistrationAsync(string email, string login, string password, int idStatistic, int idAchievemnts, int Role) {

            await _latucContext.Users.AddAsync(new User
            {
                Email = email,
                Login = login,
                Password = password,
                IdStatistics = idStatistic,
                IdAchievemnts = idAchievemnts,
                Role = Role,
                exitBool = 0
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
            item.exitBool = Settings.Default.exitBool;
            Users.RemoveAt(index);
            Users.Insert(index, item);
            await _latucContext.SaveChangesAsync();
        }

        public void UpdateProductNull() {
            Settings.Default.exitBool = 0;
            UpdateProduct();
        }

        public int GetMaxIdUser (){

            return _latucContext.Users.Max(u => u.Iduser);
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

