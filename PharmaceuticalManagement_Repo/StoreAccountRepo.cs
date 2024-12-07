using PharmaceuticalManagement_BO.Models;
using PharmaceuticalManagement_DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaceuticalManagement_Repo
{
    public class StoreAccountRepo : IStoreAccountRepo
    {
        private readonly IBaseDAO<StoreAccount> _baseDAO;
        public StoreAccountRepo(IBaseDAO<StoreAccount> baseDAO)
        {
            _baseDAO = baseDAO;
        }
        public async Task<StoreAccount?> Login(string email, string password) => await _baseDAO.Login(email, password);
    }
}
