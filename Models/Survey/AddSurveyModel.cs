using SurveyApp.Entities;
using System;
using System.Collections.Generic;

namespace SurveyApp.Models
{
    public class AddSurveyModel
    {
        public string SurveyName { get; set; }
        public List<string> QuestionsText { get; set; }
        public int QuestionQuantity { get; set; }
        public List<SurveyCategory> Categories { get; set; }
        public int CategoryId { get; set; }
        public List<List<string>> AnswerTexts { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Description { get; set; }
    }
}
