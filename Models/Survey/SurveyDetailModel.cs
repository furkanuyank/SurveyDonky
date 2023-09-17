using SurveyApp.Entities;
using System.Collections.Generic;

namespace SurveyApp.Models
{
    public class SurveyDetailModel
    {
        public Survey Survey { get; set; }
        public List<SurveyQuestion> Questions { get; set; }
        public List<List<SurveyAnswer>> Answers { get; set; }
    }
}
