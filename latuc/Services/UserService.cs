namespace latuc.Services
{
    public class UserService
    {
        private readonly LatucCodeContext _latucContext;
        public UserService(LatucCodeContext latucContext)
        {
            _latucContext = latucContext;
        }
        public async Task<bool> AuthorizationAsync(string username, string password)
        {
            var user = await _latucContext.Users.SingleOrDefaultAsync(u => u.Login == username);
            if (user == null)
                return false; MessageBox.Show("fsdfsd");
            if (user.Password.Equals(password))
            {
                Settings.Default.idUser = user.Iduser;
                Settings.Default.userLogin = user.Login;
                Settings.Default.userEmail = user.Email;
                Settings.Default.userPassword = user.Password;
                Settings.Default.idStatistic = user.IdStatistics;
                Settings.Default.idAchievments = user.IdAchievemnts;
                return true;
            }
            return false;
        }

        public async Task RegistrationAsync(string email, string login, string password, int idStatistic, int idAchievemnts) {

            await _latucContext.Users.AddAsync(new User
            {
                Email = email, 
                Login = login,
                Password = password,
                IdStatistics = idStatistic,
                IdAchievemnts = idAchievemnts
            });
            await _latucContext.SaveChangesAsync();
        }

        public async Task GetMaxIdUser (){

           // return await _latucContext.Users.Select(u => _latucContext.Users.Max(u.IdAchievemntsNavigation)).AsNoTracking();
        }

    }
}

