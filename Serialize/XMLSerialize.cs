namespace SomeSerializeAlgorithm.Serialize
{
    public class XMLSerialize : ISerialize
    {

        #region Singleton
        private static volatile XMLSerialize? _Instance = null;
        private static readonly object _lock = new();
        private XMLSerialize() { }
        public static XMLSerialize GetInstance
        {
            get
            {
                if (_Instance == null)
                {
                    lock (_lock)
                    {

                        if (_Instance == null)
                            _Instance = new XMLSerialize();
                    }
                }
                return _Instance;
            }
        }
        #endregion

        T ISerialize.Deserialize<T>(string items)
        {
            var serializer = new XmlSerializer(typeof(T));
            using var reader = new StringReader(items);
            return (T)serializer.Deserialize(reader);
        }
        T ISerialize.Deserialize<T>(byte[] items)
        {
            var serializer = new XmlSerializer(typeof(T));

            using var ms = new MemoryStream(items);
            return (T)serializer.Deserialize(ms);
        }


        byte[] ISerialize.SerializeToArray<T>(T items)
        {
            var serializer = new XmlSerializer(typeof(T));
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add("xsi", "https://MehrdadBahrami.com/");
            using (var ms = new MemoryStream())
            {
                serializer.Serialize(ms, items, namespaces);
                return ms.ToArray();
            }
        }

        string ISerialize.SerializeToString<T>(T items)
        {
            var serializer = new XmlSerializer(typeof(T));
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add("xsi", "https://MehrdadBahrami.com/");
            using (var writer = new StringWriter())
            {
                serializer.Serialize(writer, items, namespaces);
                return writer.ToString();
            }
        }
    }

}
