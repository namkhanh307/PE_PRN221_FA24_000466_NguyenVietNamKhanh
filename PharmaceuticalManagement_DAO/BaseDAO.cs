using Microsoft.EntityFrameworkCore;
using PharmaceuticalManagement_BO.Models;

namespace PharmaceuticalManagement_DAO
{
    public class BaseDAO<T> : IBaseDAO<T> where T : class
    {
        protected readonly Fall24PharmaceuticalDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseDAO(Fall24PharmaceuticalDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
		public async Task<IList<T>> Get(Func<IQueryable<T>, IQueryable<T>>? include = null)
		{
			IQueryable<T> query = _dbSet;
			if (include != null)
			{
				query = include(query);
			}
			return await query.ToListAsync();
		}

		public async Task<T?> GetById(string id, string pk, Func<IQueryable<T>, IQueryable<T>>? include = null)
		{
			IQueryable<T> query = _dbSet;
			if (include != null)
			{
				query = include(query);
			} 
			return await query.FirstOrDefaultAsync(e => EF.Property<string>(e, pk) == id);
		}

		public async Task<bool> Add(T entity)               
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<StoreAccount?> Login(string email, string password)
        {
            return await _context.StoreAccounts.SingleOrDefaultAsync(s => s.EmailAddress == email && s.StoreAccountPassword == password);
        }
        public async Task<PagingVM> SearchMedicineInformation(string? activeIngredients, string? expirationDate, string? warningsAndPrecautions, int pageNumber, int pageSize)
        {
            var medicineInformation = await _context.MedicineInformations.Include(c => c.Manufacturer).ToListAsync();

            var query = medicineInformation.AsQueryable();

            if (!string.IsNullOrWhiteSpace(activeIngredients))
            {
                query = query.Where(c => c.ActiveIngredients.ToLower().Contains(activeIngredients.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(expirationDate))
            {
                query = query.Where(c => c.ExpirationDate == expirationDate);
            }
            if (!string.IsNullOrWhiteSpace(warningsAndPrecautions))
            {
                query = query.Where(c => c.WarningsAndPrecautions.ToLower().Contains(warningsAndPrecautions.ToLower()));
            }
            var totalItems = query.Count();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var items = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return new PagingVM()
            {
                List = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages,
            };
        }
    }
}
