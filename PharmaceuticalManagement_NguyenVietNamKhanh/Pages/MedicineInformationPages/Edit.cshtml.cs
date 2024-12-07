using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PharmaceuticalManagement_BO.Models;
using PharmaceuticalManagement_Repo;

namespace PharmaceuticalManagement_NguyenVietNamKhanh.Pages.MedicineInformationPages
{
    public class EditModel : PageModel
    {
        private readonly IMedicineInformationRepo _miRepo;

        public EditModel(IMedicineInformationRepo miRepo)
        {
            _miRepo = miRepo;
        }

        [BindProperty]
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
            MedicineInformation = medicineinformation;
            ViewData["ManufacturerId"] = new SelectList(await _miRepo.Get(), "ManufacturerId", "ManufacturerId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _miRepo.Update(MedicineInformation);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await MedicineInformationExists(MedicineInformation.MedicineId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private async Task<bool> MedicineInformationExists(string id)
        {
            return await _miRepo.GetById(id) != null;
        }
    }
}
