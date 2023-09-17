using SurveyApp.Contexts;
using System.Collections.Generic;
using System.Linq;

namespace SurveyApp.Repositories
{
    public class TestGenericRepository<Tablo> where Tablo : class, new()
    {
        public void Ekle(Tablo tablo)
        {
            using var context = new TestContext();
            context.Set<Tablo>().Add(tablo);
            context.SaveChanges();
        }

        public void Guncelle(Tablo tablo)
        {
            using var context = new TestContext();
            context.Set<Tablo>().Update(tablo);
            context.SaveChanges();
        }

        public void Sil(Tablo tablo)
        {
            using var context = new TestContext();
            context.Set<Tablo>().Remove(tablo);
            context.SaveChanges();
        }

        public List<Tablo> GetirHepsi()
        {
            using var context = new TestContext();
            return context.Set<Tablo>().ToList();
        }
        public Tablo GetirIdile(int id)
        {
            using var context = new TestContext();
            return context.Set<Tablo>().Find(id);
        }
    }
}
