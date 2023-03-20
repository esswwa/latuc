using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                return false;
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
    }
}
