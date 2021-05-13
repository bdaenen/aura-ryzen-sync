using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuraCpuSync
{
    class Settings
    {
        [JsonProperty("coldTemp")]
        public int ColdTemp { get; set; }
        [JsonProperty("hotTemp")]
        public int HotTemp { get; set; }
        [JsonProperty("coldColorRed")]
        public int ColdColorRed { get; set; }
        [JsonProperty("coldColorGreen")]
        public int ColdColorGreen { get; set; }
        [JsonProperty("coldColorBlue")]
        public int ColdColorBlue { get; set; }

        [JsonProperty("hotColorRed")]
        public int HotColorRed { get; set; }
        [JsonProperty("hotColorGreen")]
        public int HotColorGreen { get; set; }
        [JsonProperty("hotColorBlue")]
        public int HotColorBlue { get; set; }
        [JsonProperty("version")]
        public int Version { get; set; }
        [JsonProperty("sensorName")]
        public string SensorName { get; set; }
        [JsonProperty("pollingInterval")]
        public int PollingInterval { get; set;}
        [JsonProperty("enableRazerChroma")]
        public bool EnableRazerChroma { get; set; }
    }
}
