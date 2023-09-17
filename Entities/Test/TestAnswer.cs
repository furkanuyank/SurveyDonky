
namespace SurveyApp.Entities
{
    public class TestAnswer
    {
        public int TestAnswerId { get; set; }
        public int Choice { get; set; }
        public int ChoiceIndex { get; set; }
        public string AnswerText { get; set; }

        public int TestQuestionId { get; set; }
        public TestQuestion TestQuestion { get; set; }
    }
}
