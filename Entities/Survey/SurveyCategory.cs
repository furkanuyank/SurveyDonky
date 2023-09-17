using System.Collections.Generic;

namespace SurveyApp.Entities
{
    public class SurveyCategory
    {
        public int SurveyCategoryId { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<Survey> Surveys { get; set; }
    }
}
