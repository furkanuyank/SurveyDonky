using SurveyApp.Entities;
using System.Collections.Generic;

namespace SurveyApp.Interfaces
{
    public interface IQuestionRepository: IGenericRepository<SurveyQuestion>
    {
        public SurveyQuestion GetirSurveyIdile(int surveyId);
        public List<SurveyQuestion> GetirSurveyIdileHepsi(int surveyId);
    }
}
