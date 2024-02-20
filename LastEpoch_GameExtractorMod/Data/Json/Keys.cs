using Newtonsoft.Json;
using System.Collections.Generic;

namespace LastEpoch_GameExtractorMod.Data.Json
{
    public class Keys
    {
        [JsonProperty("key")]
        public List<Key> List { get; set; }
    }
    public class Key
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
