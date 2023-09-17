using SurveyApp.Contexts;
using SurveyApp.Entities;
using SurveyApp.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SurveyApp.Repositories
{
    public class AnswerRepository : GenericRepository<SurveyAnswer>, IAnswerRepository
    {
        public List<SurveyAnswer> GetirQuestionIdileHepsi(int questionId)
        {
            using var context = new SurveyContext();
            return context.SurveyAnswers.Where(I => I.QuestionId == questionId).ToList();
        }
        public SurveyAnswer GetirAnswer(int questionId,int choiceNumber)
        {
            using var context = new SurveyContext();
            List<SurveyAnswer> answers = new List<SurveyAnswer>();
            answers = context.SurveyAnswers.Where(I => I.QuestionId == questionId).ToList();
            return answers.First(I => I.ChoiceIndex == choiceNumber);
        }
    }
}
