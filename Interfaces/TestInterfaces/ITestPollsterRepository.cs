using SurveyApp.Entities;
using System.Collections.Generic;

namespace SurveyApp.Interfaces
{
    public interface ITestPollsterRepository : ITestGenericRepository<TestPollster>
    {
        public List<TestPollster> GetirTestIdile(int testId);
        public bool isAnyTestPollster(int testId, string schoolId);
        public int GetTestPollsterId(int testId, string schoolId);
    }
}
