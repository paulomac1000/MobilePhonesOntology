using Newtonsoft.Json;

namespace MobilePhonesOntology.Models
{
    public class Phone
    {
        [JsonProperty(PropertyName = "features")]
        public string AdditionalDeatures { get; set; }

        [JsonProperty(PropertyName = "features_c")]
        public string AdditionalFeaturesDescription { get; set; }

        [JsonProperty(PropertyName = "alert_types")]
        public string AlertTypes { get; set; }

        [JsonProperty(PropertyName = "announced")]
        public string AnnouncedDate { get; set; }

        [JsonProperty(PropertyName = "audio_quality")]
        public string AudioQuality { get; set; }

        [JsonProperty(PropertyName = "_2g_bands")]
        public string Bands2G { get; set; }

        [JsonProperty(PropertyName = "_3g_bands")]
        public string Bands3G { get; set; }

        [JsonProperty(PropertyName = "_4g_bands")]
        public string Bands4G { get; set; }

        [JsonProperty(PropertyName = "battery_c")]
        public string Battery { get; set; }

        [JsonProperty(PropertyName = "performance")]
        public string BenchmarkPerformance { get; set; }

        [JsonProperty(PropertyName = "bluetooth")]
        public string Bluetooth { get; set; }

        [JsonProperty(PropertyName = "body_c")]
        public string Bodytype { get; set; }

        [JsonProperty(PropertyName = "Brand")]
        public string Brand { get; set; }

        [JsonProperty(PropertyName = "browser")]
        public string Browser { get; set; }

        [JsonProperty(PropertyName = "build")]
        public string Build { get; set; }

        [JsonProperty(PropertyName = "call_records")]
        public string CallRecords { get; set; }

        [JsonProperty(PropertyName = "primary_")]
        public string CameraPrimary { get; set; }

        [JsonProperty(PropertyName = "secondary")]
        public string CameraSecondary { get; set; }

        [JsonProperty(PropertyName = "body_c")]
        public string CameraSupport { get; set; }

        [JsonProperty(PropertyName = "camera")]
        public string CameraType { get; set; }

        [JsonProperty(PropertyName = "colors")]
        public string Colors { get; set; }

        [JsonProperty(PropertyName = "chipset")]
        public string CpuName { get; set; }

        [JsonProperty(PropertyName = "cpu")]
        public string CpuType { get; set; }

        [JsonProperty(PropertyName = "dimensions")]
        public string Dimensions { get; set; }

        [JsonProperty(PropertyName = "edge")]
        public string EdgeType { get; set; }

        [JsonProperty(PropertyName = "games")]
        public string Games { get; set; }

        [JsonProperty(PropertyName = "gprs")]
        public string GprsType { get; set; }

        [JsonProperty(PropertyName = "gps")]
        public string Gps { get; set; }

        [JsonProperty(PropertyName = "gpu")]
        public string Gpu { get; set; }

        [JsonProperty(PropertyName = "infrared_port")]
        public string InfraredPort { get; set; }

        [JsonProperty(PropertyName = "_internal")]
        public string InternalMemory { get; set; }

        [JsonProperty(PropertyName = "_3_5mm_jack_")]
        public string Jack35 { get; set; }

        [JsonProperty(PropertyName = "java")]
        public string Java { get; set; }

        [JsonProperty(PropertyName = "keyboard")]
        public string Keyboard { get; set; }

        [JsonProperty(PropertyName = "loudspeaker")]
        public string Loudspeaker { get; set; }

        [JsonProperty(PropertyName = "loudspeaker_")]
        public string LoudspeakerDescription { get; set; }

        [JsonProperty(PropertyName = "card_slot")]
        public string MemoryCardSlot { get; set; }

        [JsonProperty(PropertyName = "messaging")]
        public string Messaging { get; set; }

        [JsonProperty(PropertyName = "Model")]
        public string Model { get; set; }

        [JsonProperty(PropertyName = "multitouch")]
        public string Multitouch { get; set; }

        [JsonProperty(PropertyName = "speed")]
        public string Networks { get; set; }

        [JsonProperty(PropertyName = "network_c")]
        public string NetworksDescription { get; set; }

        [JsonProperty(PropertyName = "os")]
        public string OperatingSystem { get; set; }

        [JsonProperty(PropertyName = "phonebook")]
        public string Phonebook { get; set; }

        [JsonProperty(PropertyName = "price")]
        public string Price { get; set; }

        [JsonProperty(PropertyName = "radio")]
        public string Radio { get; set; }

        [JsonProperty(PropertyName = "sar_eu")]
        public string SarEu { get; set; }

        [JsonProperty(PropertyName = "sar_us")]
        public string SarUs { get; set; }

        [JsonProperty(PropertyName = "display")]
        public string ScreenContrast { get; set; }

        [JsonProperty(PropertyName = "protection")]
        public string ScreenProtection { get; set; }

        [JsonProperty(PropertyName = "resolution")]
        public string ScreenResolution { get; set; }

        [JsonProperty(PropertyName = "size")]
        public string ScreenSize { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string ScreenType { get; set; }

        [JsonProperty(PropertyName = "sensors")]
        public string Sensors { get; set; }

        [JsonProperty(PropertyName = "sim")]
        public string SimType { get; set; }

        [JsonProperty(PropertyName = "sound_c")]
        public string SoundQuality { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "display_c")]
        public string SystemInterface { get; set; }

        [JsonProperty(PropertyName = "technology")]
        public string Technology { get; set; }

        [JsonProperty(PropertyName = "music_play")]
        public string TimeMusicPlay { get; set; }

        [JsonProperty(PropertyName = "stand_by")]
        public string TimeStandBy { get; set; }

        [JsonProperty(PropertyName = "talk_time")]
        public string TimeTalk { get; set; }

        [JsonProperty(PropertyName = "usb")]
        public string Usb { get; set; }

        [JsonProperty(PropertyName = "video")]
        public string VideoRecording { get; set; }

        [JsonProperty(PropertyName = "weight")]
        public string Weight { get; set; }

        [JsonProperty(PropertyName = "wlan")]
        public string Wlan { get; set; }
    }
}