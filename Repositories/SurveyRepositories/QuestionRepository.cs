using SurveyApp.Contexts;
using SurveyApp.Entities;
using SurveyApp.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SurveyApp.Repositories
{
    public class QuestionRepository : GenericRepository<SurveyQuestion>, IQuestionRepository
    {
        public SurveyQuestion GetirSurveyIdile(int surveyId)
        {
            using var context = new SurveyContext();
            return context.SurveyQuestions.Where(I => I.SurveyId == surveyId).First();
        }
        public List<SurveyQuestion> GetirSurveyIdileHepsi(int surveyId)
        {
            using var context = new SurveyContext();
            return context.SurveyQuestions.Where(I => I.SurveyId == surveyId).ToList();
        }
    }
}
