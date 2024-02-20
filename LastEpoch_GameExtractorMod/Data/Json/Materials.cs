using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastEpoch_GameExtractorMod.Data.Json
{
    public class Materials
    {
        public class Runes
        {
            [JsonProperty("rune")]
            public List<Material> List { get; set; }
        }
        public class Glyphs
        {
            [JsonProperty("glyph")]
            public List<Material> List { get; set; }
        }
        public class Material
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("description")]
            public string Description { get; set; }
        }
    }    
}
