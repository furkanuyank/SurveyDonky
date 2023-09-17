using SurveyApp.Entities;
using System.Collections.Generic;

namespace SurveyApp.Interfaces
{
    public interface ISurveyRepository:IGenericRepository<Survey>
    {
        public List<Survey> GetirCategoryIdile(int categoryId);
    }
}
