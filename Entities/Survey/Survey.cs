using System;
using System.Collections.Generic;

namespace SurveyApp.Entities
{
    public class Survey
    {
        public int SurveyId { get; set; }
        public string Name { get; set; }
        public int WillPublished { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Description { get; set; }

        public int SurveyCategoryId { get; set; }
        public SurveyCategory SurveyCategory { get; set; }

        public virtual ICollection<SurveyQuestion> SurveyQuestions { get; set; }
        public virtual ICollection<SurveyPollster> SurveyPollsters { get; set; }

    }
}
