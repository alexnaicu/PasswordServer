using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

using PasswordServer.Web.External;

namespace PasswordServer.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> logger;
        private readonly IPasswordServerClient passwordClient;

        public IndexModel(ILogger<IndexModel> logger, IPasswordServerClient passwordClient)
        {
            this.logger = logger;
            this.passwordClient = passwordClient ?? throw new ArgumentNullException(nameof(passwordClient));
        }

        [DisplayName("User Id:")]
        [BindProperty]
        public int UserId { get; set; } = 0;

        [DisplayName("Last password:")]
        public string Password { get; set; }

        [DisplayName("Valid time left:")]
        public int ValidTimeInSeconds { get; set; }

        [DisplayName("Last User Id:")]
        public int LastUserId { get; set; }

        public void OnGet()
        {
        }        
        public async Task<IActionResult> OnPostAsync()
        {
            if (!this.ModelState.IsValid)            
                return this.Page();

            var passwordData = await this.passwordClient.GetPassword(this.UserId);
            if (passwordData != null)
            {
                this.Password = passwordData.Password;
                this.ValidTimeInSeconds = passwordData.ValidTimeInSeconds;
                this.LastUserId = this.UserId;                
            }

            return this.Page();            
        }
    }
}
