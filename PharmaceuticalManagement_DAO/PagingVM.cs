using PharmaceuticalManagement_BO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaceuticalManagement_DAO
{
    public class PagingVM
    {
        public List<MedicineInformation> List = new();
        public int PageSize;
        public int PageNumber;
        public int TotalPages;
    }
}
