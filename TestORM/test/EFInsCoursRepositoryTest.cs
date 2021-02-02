using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestORMCodeFirst.DAL;
using TestORMCodeFirst.Entities;
using TestORMCodeFirst.Persistence;
using Xunit;

namespace TestORMCodeFirst.DAL
{
    public class EFInsCoursRepositoryTest
    {
        private EFInscCoursRepository repoInscriptions;
        private EFEtudiantRepository repoEtudiants;

        private void SetUp()
        {
            // Initialiser les objets nécessaires aux tests
            var builder = new DbContextOptionsBuilder<CegepContext>();
            builder.UseInMemoryDatabase(databaseName: "testInscription_db");   // Database en mémoire
            var context = new CegepContext(builder.Options);
            repoInscriptions = new EFInscCoursRepository(context);
            repoEtudiants = new EFEtudiantRepository(context);
        }

        [Fact]
        public void AjouterInscription()
        {
            // Arrange
            SetUp();
            Etudiant etud = new Etudiant { Nom = "Simard", Prenom = "Serge", DateNaissance = Convert.ToDateTime("1997-10-10"), NoProgramme = 420 };
            repoEtudiants.AjouterEtudiant(etud);
            string session = "H21";


            // Act
            repoInscriptions.AjouterInscription(etud.EtudiantID, "W49", session);

            // Assert
            var result = repoInscriptions.ObtenirInscriptions();
            Assert.Single(result);
            Assert.Same(etud, result.First().Etudiant);
            Assert.Same("W49", result.First().CodeCours);
            Assert.Equal(session, result.First().CodeSession);
        }

        private void DataSeed()
        {
            Etudiant etud1 = new Etudiant { Nom = "Deshaies", Prenom = "Yvan", DateNaissance = Convert.ToDateTime("1977-10-10"), NoProgramme = 420 };
            Etudiant etud2 = new Etudiant { Nom = "Simard", Prenom = "Serge", DateNaissance = Convert.ToDateTime("1980-10-10"), NoProgramme = 420 };
            Etudiant etud3 = new Etudiant { Nom = "Gingras", Prenom = "Karine", DateNaissance = Convert.ToDateTime("1977-10-10"), NoProgramme = 420 };
            Etudiant etud4 = new Etudiant { Nom = "Doré", Prenom = "Hélène", DateNaissance = Convert.ToDateTime("1992-10-10"), NoProgramme = 420 };
            Etudiant etud5 = new Etudiant { Nom = "Gingras", Prenom = "Karine", DateNaissance = Convert.ToDateTime("1977-10-10"), NoProgramme = 420 };
            Etudiant etud6 = new Etudiant { Nom = "Huot", Prenom = "Alain", DateNaissance = Convert.ToDateTime("1977-10-10"), NoProgramme = 420 };
            Etudiant etud7 = new Etudiant { Nom = "Talbot", Prenom = "Jo", DateNaissance = Convert.ToDateTime("1977-10-10"), NoProgramme = 420 };
            repoEtudiants.AjouterEtudiant(etud1);
            repoEtudiants.AjouterEtudiant(etud2);
            repoEtudiants.AjouterEtudiant(etud3);
            repoEtudiants.AjouterEtudiant(etud4);
            repoEtudiants.AjouterEtudiant(etud5);
            repoEtudiants.AjouterEtudiant(etud6);
            repoEtudiants.AjouterEtudiant(etud7);

            string sessionH20 = "H20";
            repoInscriptions.AjouterInscription(etud1.EtudiantID, "W49", sessionH20);
            repoInscriptions.AjouterInscription(etud2.EtudiantID, "W49", sessionH20);
            repoInscriptions.AjouterInscription(etud3.EtudiantID, "W49", sessionH20);
            repoInscriptions.AjouterInscription(etud4.EtudiantID, "W49", sessionH20);
            repoInscriptions.AjouterInscription(etud1.EtudiantID, "W40", sessionH20);
            repoInscriptions.AjouterInscription(etud5.EtudiantID, "W40", sessionH20);

            string sessionH21 = "H21";
            repoInscriptions.AjouterInscription(etud1.EtudiantID, "W49", sessionH21);
            repoInscriptions.AjouterInscription(etud2.EtudiantID, "W49", sessionH21);
            repoInscriptions.AjouterInscription(etud6.EtudiantID, "W49", sessionH21);
        }

        [Fact]
        public void SupprimerToutesLesInscriptions()
        {
            // Arrange
            SetUp();
            DataSeed();

            // Act
            repoInscriptions.SupprimerToutesLesInscriptions();

            // Assert
            var result = repoInscriptions.ObtenirInscriptions();
            Assert.Empty(result);
        }


        [Fact]
        public void NombreEtudiantsInscritsPourUneSession_QuandAucunCours()
        {
            // Arrange
            SetUp();
            DataSeed();

            //Act
            int NbInscriptions = repoInscriptions.NombreEtudiantsInscritsAuCegep("H22");

            // Assert
            Assert.Equal(0, NbInscriptions);
        }

        [Fact]
        public void NombreEtudiantsInscritsPourUneSession_QuandUnCours()
        {
            // Arrange
            SetUp();
            DataSeed();

            //Act
            int NbInscriptions = repoInscriptions.NombreEtudiantsInscritsAuCegep("H21");

            // Assert
            Assert.Equal(3, NbInscriptions);
        }

        [Fact]
        public void NombreEtudiantsInscritsPourUneSession_QuandPlusieursCours()
        {
            // Arrange
            SetUp();
            DataSeed();

            //Act
            int NbInscriptions = repoInscriptions.NombreEtudiantsInscritsAuCegep("H20");

            // Assert
            Assert.Equal(5, NbInscriptions);
        }

        [Fact]
        public void MettreAJourNoteFinale_ShouldModifiFinalGrade()
        {
            // Arrange
            SetUp();
            DataSeed();

            //Act
            repoInscriptions.MettreAJourNoteFinale(1, "w49", "H20", 60);

            // Assert
            Assert.Equal(60, repoInscriptions.ObtenirInscription(1, "w49", "H20").NoteFinale);
        }
        [Fact]
        public void ObtenirPourUneClasseLaMoyenne_ShouldReturnClassAverage()
        {
            // Arrange
            SetUp();
            DataSeed();

            //Act
            double result=repoInscriptions.ObtenirPourUneClasseLaMoyenne("w49", "H20");

            // Assert
            Assert.Equal(0, result);
        }
    }
}
