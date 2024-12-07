using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PharmaceuticalManagement_BO.Models;

namespace PharmaceuticalManagement_NguyenVietNamKhanh.Pages.StoreAccountPages
{
    public class CreateModel : PageModel
    {
        private readonly PharmaceuticalManagement_BO.Models.Fall24PharmaceuticalDbContext _context;

        public CreateModel(PharmaceuticalManagement_BO.Models.Fall24PharmaceuticalDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public StoreAccount StoreAccount { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.StoreAccounts.Add(StoreAccount);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
