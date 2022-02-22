using GestionConferences.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionConferences.Models.DAL
{
    public interface IDal : IDisposable
    {
        IList<Salle> ListeSalles();

        IList<Conference> ListeConferences();

        Conference ObtenirConference(int id);

        int CreerConference(DateTime debut, DateTime fin, string nom, string description, int idSalle, int idIntervenant);

        bool ExisteConferenceSalle(DateTime debut, DateTime fin, int idSalle);

        bool ExisteConferenceIntervenant(DateTime debut, DateTime fin, int idIntervenant);

        IList<Inscription> ListeInscriptions(int idConference);

        Inscription ObtenirInscription(int idConference, int idPersonne);

        int CreerInscription(int idConference, int idPersonne);

        void SupprimerInscription(int id);

        IList<Personne> ListePersonnes();

        Personne ObtenirPersonne(int id);

        Personne AuthentifierPersonne(string email, string password);

        bool ExistePersonne(string email);

        int CreerPersonne(string email, string password, string nom, string prenom);
    }
}
