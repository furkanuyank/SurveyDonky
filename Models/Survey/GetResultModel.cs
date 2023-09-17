using SurveyApp.Entities;
using System.Collections.Generic;

namespace SurveyApp.Models
{
    public class GetResultModel
    {
        public Survey Survey { get; set; }
        public List<SurveyQuestion> Questions { get; set; }
        public List<List<SurveyAnswer>> Answers { get; set; }
    }
}
