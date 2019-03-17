using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarvelHeros.Models
{
    public class CharacterUrl
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
