using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
        private readonly IManufacturerRepo _mRepo;

        public EditModel(IMedicineInformationRepo miRepo, IManufacturerRepo mRepo)
        {
            _miRepo = miRepo;
            _mRepo = mRepo; 
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
            ViewData["ManufacturerName"] = new SelectList(await _mRepo.Get(), "ManufacturerId", "ManufacturerName");
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
                if (string.IsNullOrEmpty(MedicineInformation.ActiveIngredients))
                {
                    ModelState.AddModelError("MedicineInformation.ActiveIngredients", "Active ingredient is required");
                    return Page();
                }

                if (Regex.IsMatch(MedicineInformation.ActiveIngredients, @"[#@&()]"))
                {
                    ModelState.AddModelError("MedicineInformation.ActiveIngredients", "Invalid value for active ingredient");
                    return Page();
                }

                var words = MedicineInformation.ActiveIngredients.Split(' ');
                foreach (var word in words)
                {
                    if (!Regex.IsMatch(word, @"^[A-Z0-9]"))
                    {
                        ModelState.AddModelError("MedicineInformation.ActiveIngredients", "Invalid value for active ingredient");
                        return Page();
                    }
                }
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
