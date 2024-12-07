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
    public class ManufacturerRepo : IManufacturerRepo
    {
        private readonly IBaseDAO<Manufacturer> _baseDAO;
        public ManufacturerRepo(IBaseDAO<Manufacturer> baseDAO)
        {             
            _baseDAO = baseDAO; 
        }
        public async Task<bool> Add(Manufacturer entity) => await _baseDAO.Add(entity);
        public async Task<bool> Delete(Manufacturer entity) => await _baseDAO.Delete(entity);
        public async Task<IList<Manufacturer>> Get() => await _baseDAO.Get();
        public async Task<Manufacturer?> GetById(string id) => await _baseDAO.GetById(id, "ManufacturerId");
        public async Task<bool> Update(Manufacturer entity) => await _baseDAO.Update(entity);
    }
}
