using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using PharmaceuticalManagement_BO.Models;
using PharmaceuticalManagement_Repo;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace PharmaceuticalManagement_NguyenVietNamKhanh.Pages.MedicineInformationPages
{
    public class CreateModel : PageModel
    {
        private readonly IMedicineInformationRepo _miRepo;
        private readonly IManufacturerRepo _mRepo;

        public CreateModel(IMedicineInformationRepo miRepo, IManufacturerRepo mRepo)
        {
            _miRepo = miRepo;
            _mRepo = mRepo;
        }

        public async Task<IActionResult> OnGet()
        {
            ViewData["ManufacturerName"] = new SelectList(await _mRepo.Get(), "ManufacturerId", "ManufacturerName");
            return Page();
        }

        [BindProperty]
        public MedicineInformation MedicineInformation { get; set; } = default!;       

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                await PopulateMI();
                return Page();
            }
            var existedCandidate = await _miRepo.GetById(MedicineInformation.MedicineId);
            if (existedCandidate != null)
            {
                ModelState.AddModelError("MedicineInformation.MedicineId", "MedicineInformation.MedicineId this ID already exists.");
                await PopulateMI();
                return Page();
            }
            if (string.IsNullOrEmpty(MedicineInformation.ActiveIngredients))
            {
                ModelState.AddModelError("MedicineInformation.ActiveIngredients", "Active ingredient is required");
                await PopulateMI();
                return Page();
            }

            if (Regex.IsMatch(MedicineInformation.ActiveIngredients, @"[#@&()]"))
            {
                ModelState.AddModelError("MedicineInformation.ActiveIngredients", "Invalid value for active ingredient");
                await PopulateMI();
                return Page();
            }

            var words = MedicineInformation.ActiveIngredients.Split(' ');
            foreach (var word in words)
            {
                if (!Regex.IsMatch(word, @"^[A-Z0-9]"))
                {
                    ModelState.AddModelError("MedicineInformation.ActiveIngredients", "Invalid value for active ingredient");
                    await PopulateMI();
                    return Page();
                }
            }
            await _miRepo.Add(MedicineInformation);
            return RedirectToPage("./Index");
        }
        private async Task PopulateMI()
        {
            ViewData["ManufacturerName"] = new SelectList(await _mRepo.Get(), "ManufacturerId", "ManufacturerName");
        }
    }
}
