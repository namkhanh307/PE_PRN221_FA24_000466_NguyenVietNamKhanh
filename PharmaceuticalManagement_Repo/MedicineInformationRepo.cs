using Microsoft.EntityFrameworkCore;
using PharmaceuticalManagement_BO.Models;
using PharmaceuticalManagement_DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaceuticalManagement_Repo
{
    public class MedicineInformationRepo : IMedicineInformationRepo
    {
        private readonly IBaseDAO<MedicineInformation> _baseDAO;
        public MedicineInformationRepo(IBaseDAO<MedicineInformation> baseDAO)
        {
            _baseDAO = baseDAO;
        }
        public async Task<PagingVM> SearchMedicineInformation(string? activeIngredients, string? expirationDate, string? warningsAndPrecautions, int pageNumber, int pageSize) => await _baseDAO.SearchMedicineInformation(activeIngredients, expirationDate, warningsAndPrecautions, pageNumber, pageSize);

        public async Task<bool> Add(MedicineInformation entity) => await _baseDAO.Add(entity);
        public async Task<bool> Delete(MedicineInformation entity) => await _baseDAO.Delete(entity);
        public async Task<IList<MedicineInformation>> Get() => await _baseDAO.Get(e => e.Include(m => m.Manufacturer));
        public async Task<MedicineInformation?> GetById(string id) => await _baseDAO.GetById(id, "MedicineId", e => e.Include(m => m.Manufacturer));
        public async Task<bool> Update(MedicineInformation entity) => await _baseDAO.Update(entity);

    }
}
