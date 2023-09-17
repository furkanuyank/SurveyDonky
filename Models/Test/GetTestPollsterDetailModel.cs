using System.ComponentModel.DataAnnotations;

namespace SurveyApp.Models
{
    public class GetTestPollsterDetailModel
    {
        public int TestId { get; set; }
        public string Name { get; set; }
        [RegularExpression(@"^\d+$", ErrorMessage = "Lütfen numarayı doğru girin")]
        public string SchoolId { get; set; }
        public int Correct { get; set; }
        public int Wrong { get; set; }
    }
}
