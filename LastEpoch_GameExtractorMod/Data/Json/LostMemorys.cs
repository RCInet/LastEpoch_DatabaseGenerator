using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastEpoch_GameExtractorMod.Data.Json
{
    public class LostMemorys
    {
        [JsonProperty("Memorys")]
        public List<Memory> List { get; set; }
    }
    public class Memory
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
