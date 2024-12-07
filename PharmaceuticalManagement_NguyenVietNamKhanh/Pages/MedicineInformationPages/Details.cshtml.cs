using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PharmaceuticalManagement_BO.Models;
using PharmaceuticalManagement_Repo;

namespace PharmaceuticalManagement_NguyenVietNamKhanh.Pages.MedicineInformationPages
{
    public class DetailsModel : PageModel
    {
            private readonly IMedicineInformationRepo _miRepo;

            public DetailsModel(IMedicineInformationRepo miRepo)
            {
                _miRepo = miRepo;
            }

            public MedicineInformation MedicineInformation { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicineinformation = await _miRepo.GetById(id);
            if (medicineinformation == null)
            {
                return NotFound();
            }
            else
            {
                MedicineInformation = medicineinformation;
            }
            return Page();
        }
    }
}
