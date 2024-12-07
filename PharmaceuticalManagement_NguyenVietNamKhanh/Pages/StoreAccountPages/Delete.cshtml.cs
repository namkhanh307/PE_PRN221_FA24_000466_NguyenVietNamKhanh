using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PharmaceuticalManagement_BO.Models;

namespace PharmaceuticalManagement_NguyenVietNamKhanh.Pages.StoreAccountPages
{
    public class DeleteModel : PageModel
    {
        private readonly PharmaceuticalManagement_BO.Models.Fall24PharmaceuticalDbContext _context;

        public DeleteModel(PharmaceuticalManagement_BO.Models.Fall24PharmaceuticalDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public StoreAccount StoreAccount { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storeaccount = await _context.StoreAccounts.FirstOrDefaultAsync(m => m.StoreAccountId == id);

            if (storeaccount == null)
            {
                return NotFound();
            }
            else
            {
                StoreAccount = storeaccount;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storeaccount = await _context.StoreAccounts.FindAsync(id);
            if (storeaccount != null)
            {
                StoreAccount = storeaccount;
                _context.StoreAccounts.Remove(StoreAccount);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
