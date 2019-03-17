using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarvelHeros.Models
{
    public class Thumbnail
    {
        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("extension")]
        public string Extension { get; set; }
    }
}
