using System.Collections.Generic;

namespace SurveyApp.Models
{
    public class AddTestModel
    {
        public string TestName { get; set; }
        public List<string> QuestionsText { get; set; }
        public int Duration { get; set; }
        public int QuestionQuantity { get; set; }
        public List<List<string>> AnswersText { get; set; }
        public List<int> CorrectAnswers { get; set; }
        public string CreatedBy { get; set; }
        public string Mail { get; set; }
        public int WillSend { get; set; }
        public string Description { get; set; }
    }
}
