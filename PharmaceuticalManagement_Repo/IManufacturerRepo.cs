using PharmaceuticalManagement_BO.Models;

namespace PharmaceuticalManagement_Repo
{
    public interface IManufacturerRepo
    {
        Task<IList<Manufacturer>> Get();
        Task<Manufacturer?> GetById(string id);
        Task<bool> Add(Manufacturer entity);
        Task<bool> Update(Manufacturer entity);
        Task<bool> Delete(Manufacturer entity);
    }
}
