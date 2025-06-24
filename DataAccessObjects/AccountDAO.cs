using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class AccountDAO
    {
        private static AccountDAO instance;
        private static readonly object instanceLock = new object();
        private readonly FunewsManagementContext _context;
        public static AccountDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AccountDAO();
                }
                return instance;
            }
        }
        public AccountDAO()
        {
            _context = new FunewsManagementContext();
        }
        public SystemAccount GetAccountByEmail(string email, string password)
        {
            try
            {
                return _context.SystemAccounts.FirstOrDefault(acc => acc.AccountEmail.Equals(email) && acc.AccountPassword.Equals(password));
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public SystemAccount GetAccountByUserId(short userId)
        {
            try
            {
                return _context.SystemAccounts.FirstOrDefault(acc => acc.AccountId == userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateAccount(SystemAccount account)
        {
            try
            {
                _context.Entry<SystemAccount>(account).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<SystemAccount> GetAllAccount()
        {
            List<SystemAccount> list = new List<SystemAccount>();
            try
            {
                list = _context.SystemAccounts.ToList();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }
        public List<SystemAccount> SearchAccount(string search)
        {
            List<SystemAccount> list = new List<SystemAccount>();
            try
            {
                int role = 0;
                if (search.ToLower().Equals("staff"))
                {
                    role = 2;
                }
                if (search.ToLower().Equals("user"))
                {
                    role = 1;
                }
                list = _context.SystemAccounts.Where(acc => acc.AccountEmail.Contains(search) || acc.AccountName.Contains(search) || acc.AccountRole == role).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }
    }
}
