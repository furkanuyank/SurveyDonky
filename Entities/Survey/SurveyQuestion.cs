using System.Collections.Generic;

namespace SurveyApp.Entities
{
    public class SurveyQuestion
    {
        public int SurveyQuestionId { get; set; }
        public string QuestionText { get; set; }

        public int SurveyId { get; set; }
        public Survey Survey { get; set; }

        public virtual ICollection<SurveyAnswer> SurveyAnswers { get; set; }

    }
}
