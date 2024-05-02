using Microsoft.AspNetCore.Authorization;

namespace KhumaloCraft.Models
{
    [Authorize(Roles = "Admin")]
    public class AdminPageModel
    {

        public void OnGet()
        {

        }
    }
}
