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
    public class IndexModel : PageModel
    {
        private readonly PharmaceuticalManagement_BO.Models.Fall24PharmaceuticalDbContext _context;

        public IndexModel(PharmaceuticalManagement_BO.Models.Fall24PharmaceuticalDbContext context)
        {
            _context = context;
        }

        public IList<StoreAccount> StoreAccount { get;set; } = default!;

        public async Task OnGetAsync()
        {
            StoreAccount = await _context.StoreAccounts.ToListAsync();
        }
    }
}
