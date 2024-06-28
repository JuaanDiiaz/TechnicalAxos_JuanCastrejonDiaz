using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalAxos_JuanCastrejonDiaz.Models
{
    public class Country
    {
        [JsonProperty("name")]
        public Name Name { get; set; }
        [JsonProperty("region")]
        public string Region { get; set; }
        [JsonProperty("cca2")]
        public string CountryCode { get; set; }
        [JsonProperty("flags")]
        public Flags Flags { get; set; }
    }
    public class Name
    {
        [JsonProperty("common")]
        public string Common { get; set; }
        [JsonProperty("official")]
        public string Official { get; set; }
    }
    public class  Flags
    {
        [JsonProperty("png")]
        public string Png { get; set; }

        [JsonProperty("svg")]   
        public string Svg { get; set; }
        
    }
}
