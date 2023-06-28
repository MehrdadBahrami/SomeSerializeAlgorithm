using SomeSerializeAlgorithm.Serialize;

namespace SomeSerializeAlgorithm
{
    [ProtoContract(SkipConstructor = true, UseProtoMembersOnly = true)]
    public class ProtobuffSampleClass
    {
        public ProtobuffSampleClass()
        {
            ParametersName = "Mehrdad";
            ParameterNumber = 30;
        }

        [ProtoMember(1)]
        public string ParametersName { get; set; }
        [ProtoMember(2)]
        public int ParameterNumber { get; set; }
    }
    public class JsonSampleClass
    {
        public JsonSampleClass()
        {
            ParametersName = "Mehrdad";
            ParameterNumber = 30;
        }
        [JsonProperty]
        public string ParametersName { get; set; }
        [JsonProperty]
        public int ParameterNumber { get; set; }
    }
    public class XMLSampleClass
    {
        public XMLSampleClass()
        {
            ParametersName = "Mehrdad";
            ParameterNumber = 30;
        }
        public string ParametersName { get; set; }
        public int ParameterNumber { get; set; }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args is null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            bool Show = true;

            #region ProtoBuff

            ISerialize serProto =  ProtobufSerialize.GetInstance;

            var StringProtobufSer = serProto.SerializeToString(new ProtobuffSampleClass());
            if (Show) Console.WriteLine(StringProtobufSer);

            var ArrayProtobufSer = serProto.SerializeToArray(new ProtobuffSampleClass());
            if (Show) Console.WriteLine(ArrayProtobufSer);

            var ProtoBuffResult1 = serProto.Deserialize<ProtobuffSampleClass>(StringProtobufSer);
            var ProtoBuffResult2 = serProto.Deserialize<ProtobuffSampleClass>(ArrayProtobufSer);
            #endregion

            #region XML

            ISerialize serXML = XMLSerialize.GetInstance;

            var StringXMLSer = serXML.SerializeToString(new XMLSampleClass());
            if (Show) Console.WriteLine(StringXMLSer);

            var ArrayXMLSer = serXML.SerializeToArray(new XMLSampleClass());
            if (Show) Console.WriteLine(ArrayXMLSer);

            var XMLResult1 = serXML.Deserialize<XMLSampleClass>(StringXMLSer);
            var XMLResult2 = serXML.Deserialize<XMLSampleClass>(ArrayXMLSer);
            #endregion

            #region Json

            ISerialize serJson = JsonSerialize.GetInstance;

            var StringJsonSer = serJson.SerializeToString(new JsonSampleClass());
            if (Show) Console.WriteLine(StringJsonSer);

            var ArrayJsonSer = serJson.SerializeToArray(new JsonSampleClass());
            if (Show) Console.WriteLine(ArrayJsonSer);

            var JsonResult1 = serJson.Deserialize<JsonSampleClass>(StringJsonSer);
            var JsonResult2 = serJson.Deserialize<JsonSampleClass>(ArrayJsonSer);
            #endregion

        }
    }
}