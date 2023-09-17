using SurveyApp.Contexts;
using SurveyApp.Entities;
using SurveyApp.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SurveyApp.Repositories
{
    public class TestPollsterRepository : TestGenericRepository<TestPollster>, ITestPollsterRepository
    {
        public List<TestPollster> GetirTestIdile(int testId)
        {
            using var context = new TestContext();
            return context.TestPollsters.Where(I => I.TestId == testId).ToList();
        }

        public bool isAnyTestPollster(int testId, string schoolId)
        {
            using var context = new TestContext();
            List<TestPollster> testPollsters = new List<TestPollster>();
            testPollsters = GetirTestIdile(testId);

            for (int i = 0; i < testPollsters.Count; i++)
            {
                if (testPollsters[i].SchoolId==schoolId)
                {
                    return true;
                }
            }
            return false;
        }
        public int GetTestPollsterId(int testId, string schoolId)
        {
            using var context = new TestContext();
            List<TestPollster> testPollsters = new List<TestPollster>();
            testPollsters = GetirTestIdile(testId);

            for (int i = 0; i < testPollsters.Count; i++)
            {
                if (testPollsters[i].SchoolId == schoolId)
                {
                    return testPollsters[i].TestPollsterId;
                }
            }
            return -1;
        }
    }
}
