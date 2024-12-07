using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PharmaceuticalManagement_BO.Models;

namespace PharmaceuticalManagement_NguyenVietNamKhanh.Pages.StoreAccountPages
{
    public class EditModel : PageModel
    {
        private readonly PharmaceuticalManagement_BO.Models.Fall24PharmaceuticalDbContext _context;

        public EditModel(PharmaceuticalManagement_BO.Models.Fall24PharmaceuticalDbContext context)
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

            var storeaccount =  await _context.StoreAccounts.FirstOrDefaultAsync(m => m.StoreAccountId == id);
            if (storeaccount == null)
            {
                return NotFound();
            }
            StoreAccount = storeaccount;
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

            _context.Attach(StoreAccount).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoreAccountExists(StoreAccount.StoreAccountId))
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

        private bool StoreAccountExists(int id)
        {
            return _context.StoreAccounts.Any(e => e.StoreAccountId == id);
        }
    }
}
