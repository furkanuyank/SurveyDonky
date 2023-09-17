
namespace SurveyApp.Entities
{
    public class TestPollster
    {
        public int TestPollsterId { get; set; }
        public string Name { get; set; }
        public string SchoolId { get; set; }
        public int Correct { get; set; }
        public int Wrong { get; set; }

        public int TestId { get; set; }
        public Test Test { get; set; }
    }
}
