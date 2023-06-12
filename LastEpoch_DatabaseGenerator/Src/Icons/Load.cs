using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace LastEpoch_DatabaseGenerator.Src.Icons
{
    public class Load
    {
        public static bool Shards_init = false;
        private static string Shards_path = System.IO.Directory.GetCurrentDirectory() + @"\Mods\Src\Icons\Json\";
        private static string Shards_filename = "Shards.json";
        public static void Shards()
        {
            Data.Db_Shards = new List<Get.shard_struct>();
            if (File.Exists(Shards_path + Shards_filename))
            {
                string JsonString = File.ReadAllText(Shards_path + Shards_filename);
                Data.Db_Shards = JsonConvert.DeserializeObject<List<LastEpoch_DatabaseGenerator.Src.Icons.Get.shard_struct>>(JsonString);
            }
            Shards_init = true;
        }
    }
}
