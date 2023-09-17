using Microsoft.AspNetCore.Mvc;
using SurveyApp.Entities;
using System.Collections.Generic;


namespace SurveyApp.Models
{
    public class AttendSurveyModel
    {
        [BindProperty]
        public Survey Survey { get; set; }
        [BindProperty]
        public List<SurveyQuestion> Questions { get; set; }
        public List<List<SurveyAnswer>> Answers { get; set; }
        [BindProperty]
        public List<int> SelectedValues { get; set; }
        public int SurveyPollsterSurveyId { get; set; }
        public string Ip { get; set; }
    }
}
