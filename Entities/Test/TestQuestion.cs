using System.Collections.Generic;

namespace SurveyApp.Entities
{
    public class TestQuestion
    {
        public int TestQuestionId { get; set; }
        public string QuestionText { get; set; }
        public int CorrectAnswer { get; set; }

        public int TestId { get; set; }
        public Test Test { get; set; }

        public virtual ICollection<TestAnswer> TestAnswers { get; set; }
    }
}
