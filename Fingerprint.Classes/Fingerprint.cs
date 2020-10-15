using Newtonsoft.Json;

namespace Fingerprint.Classes
{
    public class Fingerprint
    {
        public bool? valid { get; set; }
        public string payload { get; set; }
        public Plugin[] plugins { get; set; }
        public Mime[] mimes { get; set; }
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
        public string[] keyboard { get; set; }
        public Connection connection { get; set; }
        public Attr attr { get; set; }
        public Orientation orientation { get; set; }
        public string doNotTrack { get; set; }
        public int Id { get; internal set; }
    }

    public class Webgl_Properties
    {
        public string unmaskedVendor { get; set; }
        public string unmaskedRenderer { get; set; }
        public string vendor { get; set; }
        public string renderer { get; set; }
        public string shadingLanguage { get; set; }
        public string version { get; set; }
        public string maxAnisotropy { get; set; }
        public string shadingLanguage2 { get; set; }
        public string version2 { get; set; }
        //public Aliasedlinewidthrange aliasedLineWidthRange { get; set; }
        //public Aliasedpointsizerange aliasedPointSizeRange { get; set; }
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
        //public Maxviewportdims maxViewportDims { get; set; }
        public string redBits { get; set; }
        public string stencilBits { get; set; }
        public string subpixelBits { get; set; }
        public string sampleBuffers { get; set; }
        public string samples { get; set; }
        public string maxColorAttachmentsWebgl { get; set; }
        public string maxDrawBuffersWebgl { get; set; }
        public Webglcontextattributesdefaults webglContextAttributesDefaults { get; set; }
        public string extensions { get; set; }
        public int precisionVertexShaderHighFloat { get; set; }
        public int rangeMinVertexShaderHighFloat { get; set; }
        public int rangeMaxVertexShaderHighFloat { get; set; }
        public int precisionVertexShaderMediumFloat { get; set; }
        public int rangeMinVertexShaderMediumFloat { get; set; }
        public int rangeMaxVertexShaderMediumFloat { get; set; }
        public int precisionVertexShaderLowFloat { get; set; }
        public int rangeMinVertexShaderLowFloat { get; set; }
        public int rangeMaxVertexShaderLowFloat { get; set; }
        public int precisionFragmentShaderHighFloat { get; set; }
        public int rangeMinFragmentShaderHighFloat { get; set; }
        public int rangeMaxFragmentShaderHighFloat { get; set; }
        public int precisionFragmentShaderMediumFloat { get; set; }
        public int rangeMinFragmentShaderMediumFloat { get; set; }
        public int rangeMaxFragmentShaderMediumFloat { get; set; }
        public int precisionFragmentShaderLowFloat { get; set; }
        public int rangeMinFragmentShaderLowFloat { get; set; }
        public int rangeMaxFragmentShaderLowFloat { get; set; }
        public int precisionVertexShaderHighInt { get; set; }
        public int rangeMinVertexShaderHighInt { get; set; }
        public int rangeMaxVertexShaderHighInt { get; set; }
        public int precisionVertexShaderMediumInt { get; set; }
        public int rangeMinVertexShaderMediumInt { get; set; }
        public int rangeMaxVertexShaderMediumInt { get; set; }
        public int precisionVertexShaderLowInt { get; set; }
        public int rangeMinVertexShaderLowInt { get; set; }
        public int rangeMaxVertexShaderLowInt { get; set; }
        public int precisionFragmentShaderHighInt { get; set; }
        public int rangeMinFragmentShaderHighInt { get; set; }
        public int rangeMaxFragmentShaderHighInt { get; set; }
        public int precisionFragmentShaderMediumInt { get; set; }
        public int rangeMinFragmentShaderMediumInt { get; set; }
        public int rangeMaxFragmentShaderMediumInt { get; set; }
        public int precisionFragmentShaderLowInt { get; set; }
        public int rangeMinFragmentShaderLowInt { get; set; }
        public int rangeMaxFragmentShaderLowInt { get; set; }
        public string maxVertexUniformComponents2 { get; set; }
        public string maxVertexUniformBlocks2 { get; set; }
        public string maxVertexOutputComponents2 { get; set; }
        public string maxVaryingComponents2 { get; set; }
        public string maxTransformFeedbackInterleavedComponents2 { get; set; }
        public string maxTransformFeedbackSeparateAttribs2 { get; set; }
        public string maxTransformFeedbackSeparateComponents2 { get; set; }
        public string maxFragmentUniformComponents2 { get; set; }
        public string maxFragmentUniformBlocks2 { get; set; }
        public string maxFragmentInputComponents2 { get; set; }
        public string minProgramTexelOffset2 { get; set; }
        public string maxProgramTexelOffset2 { get; set; }
        public string maxDrawBuffers2 { get; set; }
        public string maxColorAttachments2 { get; set; }
        public string maxSamples2 { get; set; }
        public string max3DTextureSize2 { get; set; }
        public string maxArrayTextureLayers2 { get; set; }
        public string maxClientWaitTimeoutWebgl2 { get; set; }
        public string maxElementIndex2 { get; set; }
        public string maxServerWaitTimeout2 { get; set; }
        public string maxTextureLodBias2 { get; set; }
        public string maxUniformBufferBindings2 { get; set; }
        public string maxUniformBlockSize2 { get; set; }
        public string uniformBufferOffsetAlignment2 { get; set; }
        public string maxCombinedUniformBlocks2 { get; set; }
        public string maxCombinedVertexUniformComponents2 { get; set; }
        public string maxCombinedFragmentUniformComponents2 { get; set; }
        public string maxElementsVertices2 { get; set; }
        public string maxElementsIndices2 { get; set; }
        //public Aliasedlinewidthrange2 aliasedLineWidthRange2 { get; set; }
        //public Aliasedpointsizerange2 aliasedPointSizeRange2 { get; set; }
        public Webglcontextattributesdefaults2 webglContextAttributesDefaults2 { get; set; }
        public string alphaBits2 { get; set; }
        public string blueBits2 { get; set; }
        public string depthBits2 { get; set; }
        public string greenBits2 { get; set; }
        public string maxCombinedTextureImageUnits2 { get; set; }
        public string maxCubeMapTextureSize2 { get; set; }
        public string maxFragmentUniformVectors2 { get; set; }
        public string maxRenderBufferSize2 { get; set; }
        public string maxTextureImageUnits2 { get; set; }
        public string maxTextureSize2 { get; set; }
        public string maxVaryingVectors2 { get; set; }
        public string maxVertexAttribs2 { get; set; }
        public string maxVertexTextureImageUnits2 { get; set; }
        public string maxVertexUniformVectors2 { get; set; }
        //public Maxviewportdims2 maxViewportDims2 { get; set; }
        public string redBits2 { get; set; }
        public string stencilBits2 { get; set; }
        public string subpixelBits2 { get; set; }
        public string sampleBuffers2 { get; set; }
        public string samples2 { get; set; }
        public string extensions2 { get; set; }
        public int precisionVertexShaderHighFloat2 { get; set; }
        public int rangeMinVertexShaderHighFloat2 { get; set; }
        public int rangeMaxVertexShaderHighFloat2 { get; set; }
        public int precisionVertexShaderMediumFloat2 { get; set; }
        public int rangeMinVertexShaderMediumFloat2 { get; set; }
        public int rangeMaxVertexShaderMediumFloat2 { get; set; }
        public int precisionVertexShaderLowFloat2 { get; set; }
        public int rangeMinVertexShaderLowFloat2 { get; set; }
        public int rangeMaxVertexShaderLowFloat2 { get; set; }
        public int precisionFragmentShaderHighFloat2 { get; set; }
        public int rangeMinFragmentShaderHighFloat2 { get; set; }
        public int rangeMaxFragmentShaderHighFloat2 { get; set; }
        public int precisionFragmentShaderMediumFloat2 { get; set; }
        public int rangeMinFragmentShaderMediumFloat2 { get; set; }
        public int rangeMaxFragmentShaderMediumFloat2 { get; set; }
        public int precisionFragmentShaderLowFloat2 { get; set; }
        public int rangeMinFragmentShaderLowFloat2 { get; set; }
        public int rangeMaxFragmentShaderLowFloat2 { get; set; }
        public int precisionVertexShaderHighInt2 { get; set; }
        public int rangeMinVertexShaderHighInt2 { get; set; }
        public int rangeMaxVertexShaderHighInt2 { get; set; }
        public int precisionVertexShaderMediumInt2 { get; set; }
        public int rangeMinVertexShaderMediumInt2 { get; set; }
        public int rangeMaxVertexShaderMediumInt2 { get; set; }
        public int precisionVertexShaderLowInt2 { get; set; }
        public int rangeMinVertexShaderLowInt2 { get; set; }
        public int rangeMaxVertexShaderLowInt2 { get; set; }
        public int precisionFragmentShaderHighInt2 { get; set; }
        public int rangeMinFragmentShaderHighInt2 { get; set; }
        public int rangeMaxFragmentShaderHighInt2 { get; set; }
        public int precisionFragmentShaderMediumInt2 { get; set; }
        public int rangeMinFragmentShaderMediumInt2 { get; set; }
        public int rangeMaxFragmentShaderMediumInt2 { get; set; }
        public int precisionFragmentShaderLowInt2 { get; set; }
        public int rangeMinFragmentShaderLowInt2 { get; set; }
        public int rangeMaxFragmentShaderLowInt2 { get; set; }
    }

    public class Aliasedlinewidthrange
    {
        public int _0 { get; set; }
        public int _1 { get; set; }
    }

    public class Aliasedpointsizerange
    {
        public int _0 { get; set; }
        public int _1 { get; set; }
    }

    public class Maxviewportdims
    {
        public int _0 { get; set; }
        public int _1 { get; set; }
    }

    public class Webglcontextattributesdefaults
    {
        public bool alpha { get; set; }
        public bool antialias { get; set; }
        public bool depth { get; set; }
        public bool failIfMajorPerformanceCaveat { get; set; }
        public string powerPreference { get; set; }
        public bool premultipliedAlpha { get; set; }
        public bool preserveDrawingBuffer { get; set; }
        public bool stencil { get; set; }
        public bool desynchronized { get; set; }
        public bool xrCompatible { get; set; }
    }

    public class Aliasedlinewidthrange2
    {
        public int _0 { get; set; }
        public int _1 { get; set; }
    }

    public class Aliasedpointsizerange2
    {
        public int _0 { get; set; }
        public int _1 { get; set; }
    }

    public class Webglcontextattributesdefaults2
    {
        public bool alpha { get; set; }
        public bool antialias { get; set; }
        public bool depth { get; set; }
        public bool failIfMajorPerformanceCaveat { get; set; }
        public string powerPreference { get; set; }
        public bool premultipliedAlpha { get; set; }
        public bool preserveDrawingBuffer { get; set; }
        public bool stencil { get; set; }
        public bool desynchronized { get; set; }
        public bool xrCompatible { get; set; }
    }

    public class Maxviewportdims2
    {
        public int _0 { get; set; }
        public int _1 { get; set; }
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
        public Device[] devices { get; set; }
        public Constraint constraints { get; set; }
    }

    public class Device
    {
        public string deviceId { get; set; }
        public string kind { get; set; }
        public string label { get; set; }
        public string groupId { get; set; }
    }

    public class Constraint
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
        public string type { get; set; }
        public float downlinkMax { get; set; }
        public string effectiveType { get; set; }
        public int rtt { get; set; }
        public float downlink { get; set; }
        public bool saveData { get; set; }
    }

    public class Attr
    {
        [JsonProperty("navigator.vendorSub")]
        public string navigatorvendorSub { get; set; }
        [JsonProperty("navigator.productSub")]
        public string navigatorproductSub { get; set; }
        [JsonProperty("navigator.vendor")]
        public string navigatorvendor { get; set; }
        [JsonProperty("navigator.appCodeName")]
        public string navigatorappCodeName { get; set; }
        [JsonProperty("navigator.appName")]
        public string navigatorappName { get; set; }
        [JsonProperty("navigator.appVersion")]
        public string navigatorappVersion { get; set; }
        [JsonProperty("navigator.platform")]
        public string navigatorplatform { get; set; }
        [JsonProperty("navigator.product")]
        public string navigatorproduct { get; set; }
        [JsonProperty("navigator.userAgent")]
        public string navigatoruserAgent { get; set; }

        [JsonProperty("screen.availHeight")]
        public int screenavailHeight { get; set; }
        [JsonProperty("screen.availWidth")]
        public int screenavailWidth { get; set; }
        [JsonProperty("screen.width")]
        public int screenwidth { get; set; }
        [JsonProperty("screen.height")]
        public int screenheight { get; set; }
        [JsonProperty("screen.colorDepth")]
        public int screencolorDepth { get; set; }
        [JsonProperty("screen.pixelDepth")]
        public int screenpixelDepth { get; set; }
        [JsonProperty("screen.availLeft")]
        public int screenavailLeft { get; set; }
        [JsonProperty("screen.availTop")]
        public int screenavailTop { get; set; }
        public int outerHeight { get; set; }
        public int outerWidth { get; set; }
        [JsonProperty("hardwareConcurrency")]
        public int navigatorhardwareConcurrency { get; set; }
        public int maxTouchPoints { get; set; }
        public int deviceMemory { get; set; }
        [JsonProperty("window.devicePixelRatio")]
        public float windowdevicePixelRatio { get; set; }
    }

    public class Orientation
    {
        public int angle { get; set; }
        public string type { get; set; }
    }

    public class Plugin
    {
        public int _ref { get; set; }
        public string description { get; set; }
        public string filename { get; set; }
        public string name { get; set; }
        public int[] mimes { get; set; }
    }

    public class Mime
    {
        public int _ref { get; set; }
        public string description { get; set; }
        public string suffixes { get; set; }
        public string type { get; set; }
        public int plugin { get; set; }
    }
}
