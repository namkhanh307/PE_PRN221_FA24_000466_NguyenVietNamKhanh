using PharmaceuticalManagement_BO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaceuticalManagement_DAO
{
    public interface IBaseDAO<T>
    {
        Task<T?> GetById(string id, string pk, Func<IQueryable<T>, IQueryable<T>>? include = null);
        Task<IList<T>> Get(Func<IQueryable<T>, IQueryable<T>>? include = null);
		Task<bool> Add(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity);
        Task<StoreAccount?> Login(string email, string password);
        Task<PagingVM> SearchMedicineInformation(string? activeIngredients, string? expirationDate, string? warningsAndPrecautions, int pageNumber, int pageSize);
    }
}
