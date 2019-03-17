using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarvelHeros.Models
{
    public class Result
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("modified")]
        public DateTime Modified { get; set; }

        [JsonProperty("thumbnail")]
        public Thumbnail Thumbnail { get; set; }

        [JsonProperty("resourceURI")]
        public string ResourceURI { get; set; }

        [JsonProperty("comics")]
        public Comics Comics { get; set; }

        [JsonProperty("series")]
        public Series Series { get; set; }

        [JsonProperty("stories")]
        public Stories Stories { get; set; }

        [JsonProperty("events")]
        public Events Events { get; set; }

        [JsonProperty("urls")]
        public IList<CharacterUrl> Urls { get; set; }
    }
}
