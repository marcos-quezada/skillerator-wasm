using System.Collections.Generic;

namespace skillerator.Models{
    public class GeonamesData{
        public List<GeonameItem> Geonames {get; set;}
    }

    public class GeonameItem{
        public string Continent {get; set;}
        public string Capital {get; set;}
        public string Languages {get; set;}
        public int GeonameId  {get; set;}
        public double South {get; set;}
        public string IsoAlpha3 {get; set;}
        public double North {get; set;}
        public string FipsCode {get; set;}
        public string Population {get; set;}
        public double East {get; set;}
        public string IsoNumeric {get; set;}
        public string AreaInSqKm {get; set;}
        public string CountryCode {get; set;}
        public double West {get; set;}
        public string CountryName {get; set;}
        public string PostalCodeFormat {get; set;}
        public string ContinentName {get; set;}
        public string CurrencyCode {get; set;}

        public string CountryCodeCSSClass => this.CountryCode.ToLower() + " flag";
    }
}