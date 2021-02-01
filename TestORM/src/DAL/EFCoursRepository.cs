using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestORMCodeFirst.Entities;
using TestORMCodeFirst.Persistence;

namespace TestORMCodeFirst.DAL
{
    class EFCoursRepository
    {
        private CegepContext contexte;

        public EFCoursRepository(CegepContext ctx)
        {
            contexte = ctx;
        }

        public void AjouterCours(Cours cours)
        {
            contexte.Cours.Add(cours);
            contexte.SaveChanges();
        }

        public List<Cours> ObtenirListeCours()
        {
            return contexte.Cours.ToList();
        }
    }
}
