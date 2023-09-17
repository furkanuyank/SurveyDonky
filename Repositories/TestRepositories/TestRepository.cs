using SurveyApp.Entities;
using SurveyApp.Interfaces;

namespace SurveyApp.Repositories
{
    public class TestRepository : TestGenericRepository<Test>, ITestRepository
    {
    }
}
