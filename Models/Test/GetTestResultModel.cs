using SurveyApp.Entities;
using System.Collections.Generic;

namespace SurveyApp.Models
{
    public class GetTestResultModel
    {
        public Test Test { get; set; }
        public List<TestQuestion> Questions { get; set; }
        public List<List<TestAnswer>> Answers { get; set; }
    }
}
