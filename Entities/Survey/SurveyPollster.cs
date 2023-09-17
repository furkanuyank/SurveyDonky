
namespace SurveyApp.Entities
{
    public class SurveyPollster
    {
        public int SurveyPollsterId { get; set; }
        public string Ip { get; set; }

        public int SurveyId { get; set; }
        public Survey Survey { get; set; }
    }
}
