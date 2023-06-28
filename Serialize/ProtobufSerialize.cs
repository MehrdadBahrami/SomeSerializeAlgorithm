namespace SomeSerializeAlgorithm.Serialize
{
    public class ProtobufSerialize : ISerialize
    {
        #region Singleton
        private static volatile ProtobufSerialize? _Instance = null;
        private static readonly object _lock = new();
        private ProtobufSerialize() { }
        public static ProtobufSerialize GetInstance
        {
            get
            {
                if (_Instance == null)
                {
                    lock (_lock)
                    {

                        if (_Instance == null)
                            _Instance = new ProtobufSerialize();
                    }
                }
                return _Instance;
            }
        }
        #endregion
        public T Deserialize<T>(string items) where T : class
        {
            if (string.IsNullOrWhiteSpace(items))
                throw new ArgumentException($"'{nameof(items)}' cannot be null or whitespace.", nameof(items));

            byte[] arr = Convert.FromBase64String(items);
          
            using var ms = new MemoryStream(arr);
            return Serializer.Deserialize<T>(ms);
        }
        public T Deserialize<T>(byte[] items) where T : class
        {
            if (items is null)
                throw new ArgumentNullException(nameof(items));


            using var stream = new MemoryStream(items);
            return Serializer.Deserialize<T>(stream);
        }
        byte[] ISerialize.SerializeToArray<T>(T items)
        {
            if (items is null)
                throw new ArgumentNullException(nameof(items));

            using (var stream = new MemoryStream())
            {
                Serializer.Serialize<T>(stream, items);
                return stream.ToArray();
            }
        }
        string ISerialize.SerializeToString<T>(T items)
        {
            if (items is null)
                throw new ArgumentNullException(nameof(items));

            using (var ms = new MemoryStream())
            {
                Serializer.Serialize(ms, items);
                return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
            }

        }
    }
}
