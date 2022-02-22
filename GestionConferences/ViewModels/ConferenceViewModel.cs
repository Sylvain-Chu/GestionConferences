using GestionConferences.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionConferences.ViewModels
{
    public class ConferenceViewModel : IValidatableObject
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Salle")]
        public int IdSalle { get; set; }
        public IEnumerable<SelectListItem> Salles { get; }

        [Display(Name = "Intervenant")]
        public int IdIntervenant { get; set; }
        public IEnumerable<SelectListItem> Intervenants { get; }

        [Required]
        [Display(Name = "Début")]
        public DateTime DateDebut { get; set; }

        [Required]
        [Display(Name = "Fin")]
        public DateTime DateFin { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name = "Nom")]
        public string Nom { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        /// <summary>
        /// Constructeur par défaut.
        /// </summary>
        public ConferenceViewModel()
        {
            Salles = new SelectListItem[0];
            Intervenants = new SelectListItem[0];
        }

        /// <summary>
        /// Constructeur avec données pour les listes déroulantes.
        /// </summary>
        /// <param name="salles">Liste des salles disponibles à la selection.</param>
        /// <param name="personnes">Liste des personnes disponibles à la selection.</param>
        public ConferenceViewModel(IEnumerable<Salle> salles, IEnumerable<Personne> personnes)
        {
            Salles = salles.Select(s => new SelectListItem() { Text = s.Numero, Value = s.Id.ToString() });
            Intervenants = personnes.Select(p => new SelectListItem() { Text = p.Nom + " " + p.Prenom, Value = p.Id.ToString() });
        }

        /// <summary>
        /// Constructeur à partir d'un ViewModel existant et avec données pour les listes déroulantes.
        /// </summary>
        /// <param name="viewModel">Le ConferenceViewModel d'origine.</param>
        /// <param name="salles">Liste des salles disponibles à la selection.</param>
        /// <param name="personnes">Liste des personnes disponibles à la selection.</param>
        public ConferenceViewModel(ConferenceViewModel viewModel, IEnumerable<Salle> salles, IEnumerable<Personne> personnes)
            : this(salles, personnes)
        {
            Id = viewModel.Id;
            IdSalle = viewModel.IdSalle;
            IdIntervenant = viewModel.IdIntervenant;
            DateDebut = viewModel.DateDebut;
            DateFin = viewModel.DateFin;
            Nom = viewModel.Nom;
            Description = viewModel.Description;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var valid = new List<ValidationResult>();

            if (DateDebut > DateFin)
                valid.Add(new ValidationResult("La date de fin ne peut pas être antérieur à la date de début.", new[] { "DateFin" }));

            return valid;
        }
    }
}