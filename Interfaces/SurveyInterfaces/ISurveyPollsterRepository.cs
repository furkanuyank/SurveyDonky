using SurveyApp.Entities;

namespace SurveyApp.Interfaces.SurveyInterfaces
{
    public interface ISurveyPollsterRepository : IGenericRepository<SurveyPollster>
    {
        public bool ControlIp(int surveyId, string ip);
    }
}
