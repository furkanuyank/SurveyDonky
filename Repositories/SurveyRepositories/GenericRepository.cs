using SurveyApp.Contexts;
using System.Collections.Generic;
using System.Linq;

namespace SurveyApp.Repositories
{
    public class GenericRepository<Tablo> where Tablo : class, new()
    {
        public void Ekle(Tablo tablo)
        {
            using var context = new SurveyContext();
            context.Set<Tablo>().Add(tablo);
            context.SaveChanges();
        }

        public void Guncelle(Tablo tablo)
        {
            using var context = new SurveyContext();
            context.Set<Tablo>().Update(tablo);
            context.SaveChanges();
        }

        public void Sil(Tablo tablo)
        {
            using var context = new SurveyContext();
            context.Set<Tablo>().Remove(tablo);
            context.SaveChanges();
        }

        public List<Tablo> GetirHepsi()
        {
            using var context = new SurveyContext();
            return context.Set<Tablo>().ToList();
        }
        public Tablo GetirIdile(int id)
        {
            using var context = new SurveyContext();
            return context.Set<Tablo>().Find(id);
        }
    }
}
