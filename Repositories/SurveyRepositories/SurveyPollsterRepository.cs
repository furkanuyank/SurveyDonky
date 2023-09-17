using SurveyApp.Contexts;
using SurveyApp.Entities;
using SurveyApp.Interfaces.SurveyInterfaces;
using System.Collections.Generic;
using System.Linq;

namespace SurveyApp.Repositories.SurveyRepositories
{
    public class SurveyPollsterRepository : GenericRepository<SurveyPollster>, ISurveyPollsterRepository
    {
        public bool ControlIp(int surveyId, string ip)
        {
            using var context = new SurveyContext();
            List<SurveyPollster> pollsters = new List<SurveyPollster>();
            List<SurveyPollster> samePollsters = new List<SurveyPollster>();
            pollsters = GetirHepsi();

            samePollsters= pollsters.Where(I => I.SurveyId == surveyId).ToList();

            foreach (var item in samePollsters)
            {
                if (item.Ip == ip)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
