using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestORMCodeFirst.Entities;
using TestORMCodeFirst.Persistence;
using Xunit;

namespace TestORMCodeFirst.DAL
{
    class EFECoursRepositoryTest
    {
        private EFCoursRepository repoCours;
        private EFInscCoursRepository repoInscriptions;

        private void SetUp()
        {
            // Initialiser les objets nécessaires aux tests
            var builder = new DbContextOptionsBuilder<CegepContext>();
            builder.UseInMemoryDatabase(databaseName: "testCours_db");   // Database en mémoire
            var contexte = new CegepContext(builder.Options);
            repoCours = new EFCoursRepository(contexte);
            repoInscriptions = new EFInscCoursRepository(contexte);
        }

        [Fact]
    }
}
