using GestionConferences.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;

namespace GestionConferences.Models.DAL
{
    public class Dal : IDal
    {
        private readonly ConfContext context = new ConfContext();

        public void Dispose()
        {
            context.Dispose();
        }

        /// <summary>
        /// Retourne la liste de toutes les salles.
        /// </summary>
        public IList<Salle> ListeSalles()
        {
            return context.Salles.ToList();
        }

        /// <summary>
        /// Retourne la liste de toutes les conférences.
        /// </summary>
        public IList<Conference> ListeConferences()
        {
            return context.Conferences.ToList();
        }

        /// <summary>
        /// Retourne une conférence en fonction de son ID.
        /// </summary>
        public Conference ObtenirConference(int id)
        {
            return context.Conferences.Find(id);
        }

        /// <summary>
        /// Créer une conférence et retourne son ID.
        /// </summary>
        /// <returns>Retourne l'ID de la conférence créée.</returns>
        public int CreerConference(DateTime debut, DateTime fin, string nom, string description, int idSalle, int idIntervenant)
        {
            Conference conf = new Conference()
            {
                DateDebut = debut,
                DateFin = fin,
                Nom = nom,
                Description = description,
                IdSalle = idSalle,
                IdIntervenant = idIntervenant
            };

            this.context.Conferences.Add(conf);
            this.context.SaveChanges();
            return conf.Id;
        }

        /// <summary>
        /// Test s'il existe une conférence dans une salle spécifiée durant la plage horaire.
        /// </summary>
        public bool ExisteConferenceSalle(DateTime debut, DateTime fin, int idSalle)
        {
            return this.context.Conferences.Any(c => c.DateDebut < fin && c.DateFin > debut && c.IdSalle == idSalle);
        }

        /// <summary>
        /// Test s'il existe une conférence d'un intervenant spécifié durant la plage horaire.
        /// </summary>
        public bool ExisteConferenceIntervenant(DateTime debut, DateTime fin, int idIntervenant)
        {
            return context.Conferences.Any(c => c.DateDebut < fin && c.DateFin > debut && c.IdIntervenant == idIntervenant);
        }

        /// <summary>
        /// Liste toutes les inscriptions d'une conférence spécifiée.
        /// </summary>
        public IList<Inscription> ListeInscriptions(int idConference)
        {
            throw new NotImplementedException("À compléter");
        }

        /// <summary>
        /// Retourne l'inscription d'une personne à une conférence.
        /// </summary>
        public Inscription ObtenirInscription(int idConference, int idPersonne)
        {
            throw new NotImplementedException("À compléter");
        }

        /// <summary>
        /// Créer une inscription et retourne son ID.
        /// </summary>
        /// <returns>Retourne l'ID de l'inscription créée.</returns>
        public int CreerInscription(int idConference, int idPersonne)
        {
            throw new NotImplementedException("À compléter");
        }

        /// <summary>
        /// Supprime une inscription.
        /// </summary>
        public void SupprimerInscription(int id)
        {
            Inscription inscription = context.Inscriptions.Find(id);

            if (inscription != null)
            {
                context.Inscriptions.Remove(inscription);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Retourne la liste de toutes les personnes.
        /// </summary>
        public IList<Personne> ListePersonnes()
        {
            return context.Personnes.ToList();
        }

        /// <summary>
        /// Retourne une personne en fonction de son ID.
        /// </summary>
        public Personne ObtenirPersonne(int id)
        {
            return context.Personnes.Find(id);
        }

        /// <summary>
        /// Retourne une personne en fonction de son Email et Password.
        /// </summary>
        public Personne AuthentifierPersonne(string email, string password)
        {
            Personne pe = this.context.Personnes.FirstOrDefault(p => p.Email == email);

            if (pe != null && Crypto.VerifyHashedPassword(pe.Password, password))
            {
                return pe;
            }
            else
            {
                return null;
            }

        }

        /// <summary>
        /// Test si une personne existe avec l'Email spécifié.
        /// </summary>
        public bool ExistePersonne(string email)
        {
            email = email.Trim().ToLower();
            return context.Personnes.Any(p => p.Email == email);
        }

        /// <summary>
        /// Créer une personne et retourne son ID.
        /// </summary>
        /// <returns>Retourne l'ID de la personne créée.</returns>
        public int CreerPersonne(string email, string password, string nom, string prenom)
        {
            Personne p = new Personne()
            {
                Nom = nom,
                Prenom = prenom,
                Email = email,
                Password = password
            };

            this.context.Personnes.Add(p);
            this.context.SaveChanges();
            return p.Id;
        }
    }
}