using SurveyApp.Entities;
using System.Collections.Generic;

namespace SurveyApp.Interfaces
{
    public interface ITestAnswerRepository : ITestGenericRepository<TestAnswer>
    {
        public List<TestAnswer> GetirQuestionIdileHepsi(int questionId);
        public TestAnswer GetirAnswer(int questionId, int choiceNumber);
    }
}
