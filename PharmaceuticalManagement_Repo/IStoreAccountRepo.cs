using PharmaceuticalManagement_BO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaceuticalManagement_Repo
{
    public interface IStoreAccountRepo
    {
        Task<StoreAccount?> Login(string email, string password);
    }
}
