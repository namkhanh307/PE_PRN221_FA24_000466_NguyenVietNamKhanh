using PharmaceuticalManagement_BO.Models;

namespace PharmaceuticalManagement_Repo
{
    public interface IManufacturerRepo
    {
        Task<IList<Manufacturer>> Get();
    }
}
