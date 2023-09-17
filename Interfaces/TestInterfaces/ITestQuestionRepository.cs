using SurveyApp.Entities;
using System.Collections.Generic;

namespace SurveyApp.Interfaces
{
    public interface ITestQuestionRepository: ITestGenericRepository<TestQuestion>
    {
        public TestQuestion GetirTestIdile(int testId);
        public List<TestQuestion> GetirTestIdileHepsi(int testId);
    }
}
