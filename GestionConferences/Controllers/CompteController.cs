using GestionConferences.Models.DAL;
using GestionConferences.Models.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace GestionConferences.Controllers
{
    public class CompteController : Controller
    {

        public CompteController()
        {
        }

        public ActionResult Connexion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Connexion(Personne pPersonne)
        {
            Dal dal = new Dal();

            Personne p = dal.AuthentifierPersonne(pPersonne.Email, pPersonne.Password);
            IdentitySignin(p);

            if (p == null)
            {
                ModelState.AddModelError("Password", "Erreur de mot de passe");
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Conference");
            }
        }

        [HttpPost]
        public ActionResult Deconnexion()
        {
            IdentitySignout();
            RedirectToAction("Index", "Conference");
        }

        public ActionResult CreerCompte()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreerCompte(Personne pPersonne)
        {
            if (!ModelState.IsValid)
                return View(pPersonne);

            Dal dal = new Dal();


            if (dal.ExistePersonne(pPersonne.Email))
            {
                ModelState.AddModelError("Non", "Un Compte a déjà le même mail");
                return View(pPersonne);
            }

            dal.CreerPersonne(pPersonne.Email, Crypto.HashPassword(pPersonne.Password), pPersonne.Nom, pPersonne.Prenom);

            IdentitySignin(pPersonne);

            return RedirectToAction("Index", "Conference");
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