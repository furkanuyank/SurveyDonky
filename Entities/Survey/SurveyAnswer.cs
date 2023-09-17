
namespace SurveyApp.Entities
{
    public class SurveyAnswer
    {
        public int SurveyAnswerId { get; set; }
        public int Choice { get; set; }
        public int ChoiceIndex { get; set; }
        public string AnswerText { get; set; }

        public int QuestionId { get; set; }
        public SurveyQuestion SurveyQuestion { get; set; }
    }
}
