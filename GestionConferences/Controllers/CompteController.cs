using GestionConferences.Models.DAL;
using GestionConferences.Models.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace GestionConferences.Controllers
{
    public class CompteController : Controller
    {
        private readonly IDal dal;

        public CompteController(IDal dal)
        {
            this.dal = dal;
        }

        public ActionResult Connexion()
        {
            throw new NotImplementedException("À compléter");
        }

        public ActionResult Connexion(Personne pPersonne)
        {
            throw new NotImplementedException("À compléter");
        }

        [HttpPost]
        public ActionResult Deconnexion()
        {
            throw new NotImplementedException("À compléter");
        }

        public ActionResult CreerCompte()
        {
            throw new NotImplementedException("À compléter");
        }

        public ActionResult CreerCompte(Personne pPersonne)
        {
            throw new NotImplementedException("À compléter");
        }

        private void IdentitySignin(Personne personne)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, personne.Id.ToString()),
                new Claim(ClaimTypes.Email, personne.Email),
                new Claim(ClaimTypes.Name, personne.Prenom + " " + personne.Nom),
            };

            if (personne.Role != null)
                claims.Add(new Claim(ClaimTypes.Role, personne.Role));

            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

            HttpContext.GetOwinContext().Authentication.SignIn(new AuthenticationProperties()
            {
                AllowRefresh = true,
                IsPersistent = true,
                ExpiresUtc = DateTime.UtcNow.AddDays(7)
            }, identity);
        }

        private void IdentitySignout()
        {
            HttpContext.GetOwinContext().Authentication.SignOut(
                DefaultAuthenticationTypes.ApplicationCookie,
                DefaultAuthenticationTypes.ExternalCookie
            );
        }
    }
}