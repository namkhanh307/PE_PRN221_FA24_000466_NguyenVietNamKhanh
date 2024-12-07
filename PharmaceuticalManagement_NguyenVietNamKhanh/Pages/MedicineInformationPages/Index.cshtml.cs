using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PharmaceuticalManagement_BO.Models;
using PharmaceuticalManagement_DAO;
using PharmaceuticalManagement_Repo;

namespace PharmaceuticalManagement_NguyenVietNamKhanh.Pages.MedicineInformationPages
{
    public class IndexModel : PageModel
    {
        private readonly IMedicineInformationRepo _miRepo;

        public IndexModel(IMedicineInformationRepo miRepo)
        {
            _miRepo = miRepo;
        }

        public IList<MedicineInformation> MedicineInformation { get;set; } = default!;
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        //public void OnGet(string? activeIngredients, DateTime? expirationDate, string? warningsAndPrecautions, int pageNumber = 1, int pageSize = 3)
        //{
        //    var (items, totalItems, totalPages) = _miRepo.SearchMedicineInformation(activeIngredients, expirationDate, warningsAndPrecautions, pageNumber, pageSize);

        //    MedicineInformation = items;
        //    CurrentPage = pageNumber;
        //    TotalPages = totalPages;
        //}
        public async Task OnGetAsync(string activeIngredients, string expirationDate, string warningsAndPrecautions, int pageNumber = 1, int pageSize = 3)
        {
            //MedicineInformation = await _miRepo.Get();
			PagingVM result = await _miRepo.SearchMedicineInformation(activeIngredients, expirationDate, warningsAndPrecautions, pageNumber, pageSize);

			MedicineInformation = result.List;
			CurrentPage = result.PageNumber;
			TotalPages = result.TotalPages;

		}
    }
}
