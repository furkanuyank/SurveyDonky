using SurveyApp.Contexts;
using SurveyApp.Entities;
using SurveyApp.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SurveyApp.Repositories
{
    public class TestAnswerRepository : TestGenericRepository<TestAnswer>, ITestAnswerRepository
    {
        public List<TestAnswer> GetirQuestionIdileHepsi(int questionId)
        {
            using var context = new TestContext();
            return context.TestAnswers.Where(I => I.TestQuestion.TestQuestionId == questionId).ToList();
        }
        public TestAnswer GetirAnswer(int questionId, int choiceNumber)
        {
            using var context = new TestContext();
            List<TestAnswer> answers = new List<TestAnswer>();
            answers = context.TestAnswers.Where(I => I.TestQuestionId == questionId).ToList();
            return answers.First(I => I.ChoiceIndex == choiceNumber);
        }
    }
}
