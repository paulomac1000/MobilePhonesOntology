using Newtonsoft.Json;

namespace MobilePhonesOntology.Models
{
    public class Phone
    {
        [JsonProperty("DeviceName")]
        public string DeviceName { get; set; }

        [JsonProperty("Brand")]
        public string Brand { get; set; }

        [JsonProperty("_2g_bands")]
        public string _2GBands { get; set; }

        [JsonProperty("_3_5mm_jack_")]
        public string _35MmJack { get; set; }

        [JsonProperty("_3g_bands")]
        public string _3GBands { get; set; }

        [JsonProperty("_internal")]
        public string Internal { get; set; }

        [JsonProperty("alarm")]
        public string Alarm { get; set; }

        [JsonProperty("alert_types")]
        public string AlertTypes { get; set; }

        [JsonProperty("announced")]
        public string Announced { get; set; }

        [JsonProperty("battery_c")]
        public string BatteryC { get; set; }

        [JsonProperty("bluetooth")]
        public string Bluetooth { get; set; }

        [JsonProperty("browser")]
        public string Browser { get; set; }

        [JsonProperty("call_records")]
        public string CallRecords { get; set; }

        [JsonProperty("camera_c")]
        public string CameraC { get; set; }

        [JsonProperty("card_slot")]
        public string CardSlot { get; set; }

        [JsonProperty("chipset")]
        public string Chipset { get; set; }

        [JsonProperty("clock")]
        public string Clock { get; set; }

        [JsonProperty("colors")]
        public string Colors { get; set; }

        [JsonProperty("cpu")]
        public string Cpu { get; set; }

        [JsonProperty("dimensions")]
        public string Dimensions { get; set; }

        [JsonProperty("display_c")]
        public string DisplayC { get; set; }

        [JsonProperty("edge")]
        public string Edge { get; set; }

        [JsonProperty("features")]
        public string Features { get; set; }

        [JsonProperty("features_c")]
        public string FeaturesC { get; set; }

        [JsonProperty("games")]
        public string Games { get; set; }

        [JsonProperty("gprs")]
        public string Gprs { get; set; }

        [JsonProperty("gps")]
        public string Gps { get; set; }

        [JsonProperty("infrared_port")]
        public string InfraredPort { get; set; }

        [JsonProperty("java")]
        public string Java { get; set; }

        [JsonProperty("languages")]
        public string Languages { get; set; }

        [JsonProperty("loudspeaker_")]
        public string Loudspeaker { get; set; }

        [JsonProperty("memory_c")]
        public string MemoryC { get; set; }

        [JsonProperty("messaging")]
        public string Messaging { get; set; }

        [JsonProperty("network_c")]
        public string NetworkC { get; set; }

        [JsonProperty("os")]
        public string Os { get; set; }

        [JsonProperty("phonebook")]
        public string Phonebook { get; set; }

        [JsonProperty("primary_")]
        public string Primary { get; set; }

        [JsonProperty("radio")]
        public string Radio { get; set; }

        [JsonProperty("resolution")]
        public string Resolution { get; set; }

        [JsonProperty("sar_eu")]
        public string SarEu { get; set; }

        [JsonProperty("sar_us")]
        public string SarUs { get; set; }

        [JsonProperty("secondary")]
        public string Secondary { get; set; }

        [JsonProperty("sim")]
        public string Sim { get; set; }

        [JsonProperty("size")]
        public string Size { get; set; }

        [JsonProperty("sound_c")]
        public string SoundC { get; set; }

        [JsonProperty("speed")]
        public string Speed { get; set; }

        [JsonProperty("stand_by")]
        public string StandBy { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("talk_time")]
        public string TalkTime { get; set; }

        [JsonProperty("technology")]
        public string Technology { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("usb")]
        public string Usb { get; set; }

        [JsonProperty("video")]
        public string Video { get; set; }

        [JsonProperty("weight")]
        public string Weight { get; set; }

        [JsonProperty("wlan")]
        public string Wlan { get; set; }
    }
}