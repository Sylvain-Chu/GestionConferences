using GestionConferences.Models.DAL;
using GestionConferences.Models.Entity;
using GestionConferences.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionConferences.Controllers
{
    public class ConferenceController : Controller
    {

        public ConferenceController()
        {
        }

        public ActionResult Index()
        {
            Dal dal = new Dal();
            return View(dal.ListeConferences());
        }

        public ActionResult Creer()
        {
            Dal dal = new Dal();
            ConferenceViewModel conferenceViewModel = new ConferenceViewModel(dal.ListeSalles(), dal.ListePersonnes());
            return View(conferenceViewModel);
        }

        [HttpPost]
        public ActionResult Creer(ConferenceViewModel pConf)
        {
            Dal dal = new Dal();

            // Permet de remplir les listes déroulantes et de conserver les données saisies par l'utilisateur.
            ConferenceViewModel conferenceViewModelAvecListes = new ConferenceViewModel(pConf, dal.ListeSalles(), dal.ListePersonnes());

            // Vérifications et enregistrement.
            if (dal.ExisteConferenceSalle(pConf.DateDebut, pConf.DateFin, pConf.IdSalle))
            {
                ModelState.AddModelError("IdSalle", "Une conférence est déjà prévu dans cette salle durant ce crénaux horraire.");
                return View(conferenceViewModelAvecListes);
            }

            if (dal.ExisteConferenceIntervenant(pConf.DateDebut, pConf.DateFin, pConf.IdIntervenant))
            {
                ModelState.AddModelError("IdIntervenant", "Un intervenant anime déjà une conférence sur ce crénaux horraire.");
                return View(conferenceViewModelAvecListes);
            }

            dal.CreerConference(pConf.DateDebut, pConf.DateFin, pConf.Nom, pConf.Description, pConf.IdSalle, pConf.IdIntervenant);

            return View(conferenceViewModelAvecListes);
        }

        public ActionResult AfficherConference(int id)
        {
            Dal dal = new Dal();

            return View(dal.ObtenirConference(id));
        }
    }
}