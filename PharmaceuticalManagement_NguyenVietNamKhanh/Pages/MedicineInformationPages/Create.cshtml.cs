using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PharmaceuticalManagement_BO.Models;
using PharmaceuticalManagement_Repo;

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
            ViewData["ManufacturerId"] = new SelectList(await _mRepo.Get(), "ManufacturerId", "ManufacturerId");
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
            await _miRepo.Add(MedicineInformation);
            return RedirectToPage("./Index");
        }
        private async Task PopulateMI()
        {
            ViewData["ManufacturerId"] = new SelectList(await _mRepo.Get(), "ManufacturerId", "ManufacturerId");
        }
    }
}
