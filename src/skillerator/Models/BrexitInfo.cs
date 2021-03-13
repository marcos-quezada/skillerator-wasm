using System.ComponentModel.DataAnnotations;
using FluentValidation;
using System.Collections.Generic;
using System;

namespace skillerator.Models{
    public class BrexitInfo{
        
        [Display(Name = "Lebten Sie am 31.12.2020 in Deutschland?")] public bool isElegible{get;set;}

        private bool _isRegistration;
        [Display(Name = "Aufenthaltsanzeige von britischen Staatsangehörigen")] public bool isRegistration{
            get{
                return _isRegistration;
            }
            set{
                _isRegistration = value;
            }
        }
        [Display(Name = "Antrag für eine Fiktionsbescheinigung für britische Staatsangehörige")] public bool isCertificate {
            get{
                return !_isRegistration;
            } set{
                _isRegistration = !value;
            }
        }
        [Display(Name = "Vorname(n)")] public string FirstName { get; set; }
        [Display(Name = "Familienname")] public string LastName { get; set; }
        [Display(Name = "Frühere Namen")] public string FormerNames {get; set;}
        [Display(Name = "Geschlecht")] public string Gender {get; set;}
        [Display(Name = "Geburtsdatum")] public string Birthdate {get; set;}
        [Display(Name = "Britische Staatsangehörigkeiten")] public List<string> BritishNationalities {get; set;}
        [Display(Name = "Weitere Staatsangehörigkeiten")] public string[] OtherNationalities{get; set;} 
        [Display(Name = "Wohnadresse")] public string StreetAddress{get; set;} 
        [Display(Name = "Hausnummer")] public string Number{get; set;} 
        [Display(Name = "PLZ")] public int ZipCode{get; set;} 
        [Display(Name = "Stadt")] public string City{get; set;}
        protected internal string ProjectUUID = Guid.NewGuid().ToString();

    }

    public class BrexitInfoValidator : AbstractValidator<BrexitInfo>{
        public BrexitInfoValidator(){
            RuleSet("personal", () => {
                RuleFor(bInfo => bInfo.FirstName)
                .NotEmpty().WithMessage("You must enter your first name")
                .MaximumLength(50).WithMessage("First name cannot be longer than 50 characters");

                RuleFor(bInfo => bInfo.LastName)
                .NotEmpty().WithMessage("You must enter your last name")
                .MaximumLength(50).WithMessage("Last name cannot be longer than 50 characters");

                RuleFor(binfo => binfo.Gender)
                .NotEmpty().WithMessage("You must select your gender");

                RuleFor(bInfo => bInfo.BritishNationalities)
                .NotEmpty().WithMessage("You must select at least one british nationality");
            });
            RuleSet("contact", () => {
                RuleFor(bInfo => bInfo.StreetAddress)
                .NotEmpty().WithMessage("You must enter the street address");

                RuleFor(bInfo => bInfo.Number)
                .NotEmpty().WithMessage("You must enter the number of your building / house");

                RuleFor(bInfo => bInfo.ZipCode)
                .GreaterThan(0).WithMessage("You must enter a valid ZIP code");

                RuleFor(bInfo => bInfo.City)
                .NotEmpty().WithMessage("You must enter the city");
            });
        }
    }
}