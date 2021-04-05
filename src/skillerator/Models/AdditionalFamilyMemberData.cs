using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace skillerator.Models{
    public class AdditionalFamilyMemberData{
        [Display(Name = "Vorname(n)")] public string FirstName { get; set; }
        [Display(Name = "Familienname")] public string LastName {get; set;}
        [Display(Name = "Geschlecht")] public string Gender {get; set;}
        [Display(Name = "Geburtsdatum")] public DateTime DateOfBirth {get; set;}
        [Display(Name = "Staatsangehörigkeit(en)")] public List<string> Nationalities {get; set;}
        [Display(Name = "Verwandtschaftsverhältnis")] public string FamilyRelationship {get; set;}
        [Display(Name = "Wohnanschrift/Adresse")] public string AddressOfResidence {get; set;}
    }
}