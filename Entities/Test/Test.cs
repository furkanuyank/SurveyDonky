using System;
using System.Collections.Generic;

namespace SurveyApp.Entities
{
    public class Test
    {
        public int TestId { get; set; }
        public string Name { get; set; }
        public int WillPublished { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int Duration { get; set; }
        public string Mail { get; set; }
        public string CreatedBy { get; set; }
        public int WillSend { get; set; }
        public string Description { get; set; }

        public virtual ICollection<TestQuestion> TestQuestions { get; set; }
        public virtual ICollection<TestPollster> TestPollsters { get; set; }
    }
}
