using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarvelHeros.Models
{
    public class Item
    {
        [JsonProperty("resourceURI")]
        public string ResourceURI { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
