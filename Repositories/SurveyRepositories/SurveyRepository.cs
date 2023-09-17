using SurveyApp.Contexts;
using SurveyApp.Entities;
using SurveyApp.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SurveyApp.Repositories
{
    public class SurveyRepository : GenericRepository<Survey>, ISurveyRepository
    {
        public List<Survey> GetirCategoryIdile(int categoryId)
        {
            using var context = new SurveyContext();
            return context.Surveys.Where(I => I.SurveyCategoryId == categoryId).ToList();
        }
    }
}
