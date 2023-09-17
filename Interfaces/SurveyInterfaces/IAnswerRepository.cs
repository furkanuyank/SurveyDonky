using SurveyApp.Entities;
using System.Collections.Generic;

namespace SurveyApp.Interfaces
{
    public interface IAnswerRepository : IGenericRepository<SurveyAnswer>
    {
        public List<SurveyAnswer> GetirQuestionIdileHepsi(int questionId);
        public SurveyAnswer GetirAnswer(int questionId, int answerNumber);
    }
}
