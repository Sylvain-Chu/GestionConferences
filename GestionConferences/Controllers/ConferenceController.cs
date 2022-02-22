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
            ConferenceViewModel conferenceViewModel = new ConferenceViewModel(dal.ListeSalles(), dal.ListePersonnes());
            return View(conferenceViewModel);
        }

        public ActionResult Creer(ConferenceViewModel pConf)
        {
            // Permet de remplir les listes déroulantes et de conserver les données saisies par l'utilisateur.
            ConferenceViewModel conferenceViewModelAvecListes = new ConferenceViewModel(pConf, dal.ListeSalles(), dal.ListePersonnes());

            // TODO: Vérifications et enregistrement.
            throw new NotImplementedException("À compléter");

            return View(conferenceViewModelAvecListes);
        }

        public ActionResult AfficherConference(int id)
        {
            throw new NotImplementedException("À compléter");
        }
    }
}