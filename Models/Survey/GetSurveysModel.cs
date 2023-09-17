using Microsoft.AspNetCore.Mvc;
using SurveyApp.Entities;
using System.Collections.Generic;

namespace SurveyApp.Models
{
    public class GetSurveysModel
    {
        [BindProperty]
        public List<SurveyCategory> Categories { get; set; }
        [BindProperty]
        public List<bool> SelectedCategories { get; set; }
        [BindProperty]
        public List<Survey> Surveys { get; set; }
        public List<Survey> AllSurveys { get; set; }
    }
}
