using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PharmaceuticalManagement_BO.Models;
using PharmaceuticalManagement_Repo;

namespace PharmaceuticalManagement_NguyenVietNamKhanh.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IStoreAccountRepo _repo;
        public LoginModel(IStoreAccountRepo repo)
        {
            _repo = repo;
        }

        public void OnGet()
        {
        }

        public async Task OnPost()
        {
            string? email = Request.Form["txtEmail"];
            string? password = Request.Form["txtPassword"];
            if (email != null && password != null)
            {
                StoreAccount? account = await _repo.Login(email, password);
                if (account != null && (account.Role == 2 || account.Role == 3))
                {
                    string? role= account.Role.ToString() ?? "";
                    HttpContext.Session.SetString("Role", role);
                    Response.Redirect("/MedicineInformationPages");
                }
                else
                    Response.Redirect("/Error");
            }

        }
    }
}
