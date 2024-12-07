using PharmaceuticalManagement_BO.Models;
using PharmaceuticalManagement_DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaceuticalManagement_Repo
{
    public interface IMedicineInformationRepo
    {
        Task<PagingVM> SearchMedicineInformation(string? activeIngredients, string? expirationDate, string? warningsAndPrecautions, int pageNumber, int pageSize);
        Task<IList<MedicineInformation>> Get();
        Task<MedicineInformation?> GetById(string id);
        Task<bool> Add(MedicineInformation entity);
        Task<bool> Update(MedicineInformation entity);
        Task<bool> Delete(MedicineInformation entity);

    }

}
