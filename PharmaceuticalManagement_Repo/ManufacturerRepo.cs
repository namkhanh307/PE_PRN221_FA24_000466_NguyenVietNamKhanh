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
        public async Task<IList<Manufacturer>> Get() => await _baseDAO.Get();
    }
}
