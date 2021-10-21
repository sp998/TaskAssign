using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TaskAssign.Areas.Identity;
using TaskAssign.Data;


namespace TaskAssign.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IServiceProvider _provider;
        private readonly UserManager<ApplicationUser> _userManager;
       

        public string Output { get; set; }
        public bool IsAdmin { get; set; }

        public IndexModel(ILogger<IndexModel> logger,IServiceProvider provider,UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _provider = provider;
            _userManager = userManager;
            Output = "Hello there";
         
        }

        public async Task OnGet()
        {
            
          await CreateRoles();
        }

        public async Task CreateRoles()
        {
            var _roleManager = _provider.GetRequiredService<RoleManager<IdentityRole>>();
          
            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                Output = "Role is not exist";
                var role = new IdentityRole();
                role.Name = "Admin";
                await _roleManager.CreateAsync(role);

            }
         else
            {
              

                string adminEmail = "sanjay.admin@gmail.com";
                    var admin = await _userManager.FindByEmailAsync(adminEmail);
                    if (!await _userManager.IsInRoleAsync(admin,"Admin"))
                    {
                        var adminResult = await _userManager.AddToRoleAsync(admin, "Admin");
                        Output = "Admin User Sucessfully created";
                    }
                    else
                {

                   await DoPageStuffs();
                }
                
               
            }
        }

        private async Task DoPageStuffs()
        {
            var email = HttpContext.User.Identity.Name;
            if (email != null)
            {
                var user = await _userManager.FindByEmailAsync(email);
                IsAdmin = await _userManager.IsInRoleAsync(user, "Admin");

                if (IsAdmin)
                {
                    Output = "Admin";
                }
            }
            
            
        }
    }
}
