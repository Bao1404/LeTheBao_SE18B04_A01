using BusinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AccountService : IAccountService
    {
        public SystemAccount GetAccountByEmail(string email, string password) => AccountDAO.Instance.GetAccountByEmail(email, password);
        public SystemAccount GetAccountByUserId(short userId) => AccountDAO.Instance.GetAccountByUserId(userId);
        public void UpdateAccount(SystemAccount account) => AccountDAO.Instance.UpdateAccount(account);
        public List<SystemAccount> GetAllAccount() => AccountDAO.Instance.GetAllAccount();
        public List<SystemAccount> SearchAccount(string search) => AccountDAO.Instance.SearchAccount(search);
    }
}
