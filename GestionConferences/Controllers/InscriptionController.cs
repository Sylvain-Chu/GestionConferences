using GestionConferences.Models.DAL;
using GestionConferences.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace GestionConferences.Controllers
{
    public class InscriptionController : Controller
    {
        private readonly IDal dal;

        public InscriptionController(IDal dal)
        {
            this.dal = dal;
        }

        public ActionResult Inscrits(int id)
        {
            return View(dal.ListeInscriptions(id));
        }

        public ActionResult Inscrire(int id)
        {
            int idPersonne = GetIdPersonne();

            // TODO: Tester si la personne actuellement connectée est déjà inscrite à la conférence (id en paramètre).
            // Rediriger vers Désinscription si c'est le cas.
            // Sinon donner un objet Inscription à la vue (avec les données nécessaires pré-remplies).
            throw new NotImplementedException("À compléter");
        }

        public ActionResult Inscrire(Inscription pInscription)
        {
            throw new NotImplementedException("À compléter");
        }

        public ActionResult Desinscrire(int id)
        {
            int idPersonne = GetIdPersonne();

            // TODO: Tester si la personne actuellement connectée est déjà inscrite à la conférence (id en paramètre).
            // Rediriger vers Inscription si c'est n'est pas le cas.
            // Sinon donner un objet Inscription à la vue.
            throw new NotImplementedException("À compléter");
        }

        public ActionResult Desinscrire(Inscription pInscription)
        {
            throw new NotImplementedException("À compléter");
        }

        private int GetIdPersonne()
        {
            return int.Parse(((ClaimsIdentity)User.Identity).Claims.First(s => s.Type == ClaimTypes.NameIdentifier).Value);
        }
    }
}