using Microsoft.AspNetCore.Mvc;

namespace SurveyApp.Models
{
    public class PasswordModel
    {
        [BindProperty]
        public string Password { get; set; }
    }
}
