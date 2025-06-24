using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IAccountService
    {
        SystemAccount GetAccountByEmail(string email, string password);
        SystemAccount GetAccountByUserId(short userId);
        void UpdateAccount(SystemAccount account);
        List<SystemAccount> GetAllAccount();
        List<SystemAccount> SearchAccount(string search);
    }
}
