using Newtonsoft.Json;

namespace FingerprintDownloader
{
    public class Fingerprint
    {
        public bool? valid { get; set; }
        public string payload { get; set; }
        public string ua { get; set; }
        public string[] tags { get; set; }
        public bool? dnt { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string canvas { get; set; }
        public string webgl { get; set; }
        public string rectangles { get; set; }
        public string audio { get; set; }
        public string battery { get; set; }
        public bool? has_battery_api { get; set; }
        public bool? has_battery_device { get; set; }
        public Webgl_Properties webgl_properties { get; set; }
        public Audio_Properties audio_properties { get; set; }
        public string[] fonts { get; set; }
        public string[] headers { get; set; }
        public string lang { get; set; }
        public string native_code { get; set; }
        public Css css { get; set; }
        public Media media { get; set; }
        public Speech[] speech { get; set; }
        public string heap { get; set; }
        public object[] keyboard { get; set; }
        public Connection connection { get; set; }
        public Attr attr { get; set; }
        public Orientation orientation { get; set; }
        public object doNotTrack { get; set; }
    }

    public class Webgl_Properties
    {
        public string unmaskedVendor { get; set; }
        public string unmaskedRenderer { get; set; }
        public string vendor { get; set; }
        public string renderer { get; set; }
        public string alphaBits { get; set; }
        public string blueBits { get; set; }
        public string depthBits { get; set; }
        public string greenBits { get; set; }
        public string maxCombinedTextureImageUnits { get; set; }
        public string maxCubeMapTextureSize { get; set; }
        public string maxFragmentUniformVectors { get; set; }
        public string maxRenderBufferSize { get; set; }
        public string maxTextureImageUnits { get; set; }
        public string maxTextureSize { get; set; }
        public string maxVaryingVectors { get; set; }
        public string maxVertexAttribs { get; set; }
        public string maxVertexTextureImageUnits { get; set; }
        public string maxVertexUniformVectors { get; set; }
        public string redBits { get; set; }
        public string stencilBits { get; set; }
        public string extensions { get; set; }
        public string shadingLanguage { get; set; }
        public string version { get; set; }
        public string maxAnisotropy { get; set; }
    }

    public class Audio_Properties
    {
        public int BaseAudioContextSampleRate { get; set; }
        public float AudioContextBaseLatency { get; set; }
        public int AudioDestinationNodeMaxChannelCount { get; set; }
    }

    public class Css
    {
        public string anyhover { get; set; }
        public string anypointer { get; set; }
        public int aspectratio { get; set; }
        public int color { get; set; }
        public string colorgamut { get; set; }
        public int colorindex { get; set; }
        public int deviceaspectratio { get; set; }
        public int deviceheight { get; set; }
        public int devicewidth { get; set; }
        public string grid { get; set; }
        public int height { get; set; }
        public int width { get; set; }
        public string hover { get; set; }
        public int monochrome { get; set; }
        public string orientation { get; set; }
        public string pointer { get; set; }
        public string preferscolorscheme { get; set; }
        public string prefersreducedmotion { get; set; }
        public int resolution { get; set; }
    }

    public class Media
    {
        public object[] devices { get; set; }
        public Constraints constraints { get; set; }
    }

    public class Constraints
    {
        public bool aspectRatio { get; set; }
        public bool autoGainControl { get; set; }
        public bool brightness { get; set; }
        public bool channelCount { get; set; }
        public bool colorTemperature { get; set; }
        public bool contrast { get; set; }
        public bool deviceId { get; set; }
        public bool echoCancellation { get; set; }
        public bool exposureCompensation { get; set; }
        public bool exposureMode { get; set; }
        public bool exposureTime { get; set; }
        public bool facingMode { get; set; }
        public bool focusDistance { get; set; }
        public bool focusMode { get; set; }
        public bool frameRate { get; set; }
        public bool groupId { get; set; }
        public bool height { get; set; }
        public bool iso { get; set; }
        public bool latency { get; set; }
        public bool noiseSuppression { get; set; }
        public bool pointsOfInterest { get; set; }
        public bool resizeMode { get; set; }
        public bool sampleRate { get; set; }
        public bool sampleSize { get; set; }
        public bool saturation { get; set; }
        public bool sharpness { get; set; }
        public bool torch { get; set; }
        public bool whiteBalanceMode { get; set; }
        public bool width { get; set; }
        public bool zoom { get; set; }
    }

    public class Speech
    {
        public string name { get; set; }
        public string lang { get; set; }
        public bool localService { get; set; }
        public string voiceURI { get; set; }
        public bool _default { get; set; }
    }

    public class Connection
    {
        public string effectiveType { get; set; }
        public int rtt { get; set; }
        public float downlink { get; set; }
        public bool saveData { get; set; }
    }

    public class Attr
    {
        [JsonProperty("navigator.vendorSub")]
        public string navigatorvendorSub { get; set; }
        public string navigatorproductSub { get; set; }
        public string navigatorvendor { get; set; }
        public string navigatorappCodeName { get; set; }
        public string navigatorappName { get; set; }
        public string navigatorappVersion { get; set; }
        public string navigatorplatform { get; set; }
        public string navigatorproduct { get; set; }
        public string navigatoruserAgent { get; set; }
        public int screenavailHeight { get; set; }
        public int screenavailWidth { get; set; }
        public int screenwidth { get; set; }
        public int screenheight { get; set; }
        public int screencolorDepth { get; set; }
        public int screenpixelDepth { get; set; }
        public int screenavailLeft { get; set; }
        public int screenavailTop { get; set; }
        public int outerHeight { get; set; }
        public int outerWidth { get; set; }
        public int hardwareConcurrency { get; set; }
        public int maxTouchPoints { get; set; }
        public int deviceMemory { get; set; }
        public int windowdevicePixelRatio { get; set; }
    }

    public class Orientation
    {
        public int angle { get; set; }
        public string type { get; set; }
    }

}
