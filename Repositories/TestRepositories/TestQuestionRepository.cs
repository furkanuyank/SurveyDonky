using SurveyApp.Contexts;
using SurveyApp.Entities;
using SurveyApp.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SurveyApp.Repositories
{
    public class TestQuestionRepository : TestGenericRepository<TestQuestion>, ITestQuestionRepository
    {
        public TestQuestion GetirTestIdile(int testId)
        {
            using var context = new TestContext();
            return context.TestQuestions.Where(I => I.TestId == testId).First();
        }
        public List<TestQuestion> GetirTestIdileHepsi(int testId)
        {
            using var context = new TestContext();
            return context.TestQuestions.Where(I => I.TestId == testId).ToList();
        }
    }
}
