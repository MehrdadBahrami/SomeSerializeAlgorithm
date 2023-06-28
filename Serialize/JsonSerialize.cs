namespace SomeSerializeAlgorithm.Serialize
{
    public class JsonSerialize : ISerialize
    {
        #region Singleton
        private static volatile JsonSerialize? _Instance = null;
        private static readonly object _lock = new();
        private JsonSerialize() { }
        public static JsonSerialize GetInstance
        {
            get
            {
                if (_Instance == null)
                {
                    lock (_lock)
                    {

                        if (_Instance == null)
                            _Instance = new JsonSerialize();
                    }
                }
                return _Instance;
            }
        }
        #endregion
        T ISerialize.Deserialize<T>(string items) => JsonConvert.DeserializeObject<T>(items);
        T ISerialize.Deserialize<T>(byte[] items) => JsonConvert.DeserializeObject<T>(Encoding.UTF32.GetString(items));
        byte[] ISerialize.SerializeToArray<T>(T items) => Encoding.UTF32.GetBytes(JsonConvert.SerializeObject(items));
        string ISerialize.SerializeToString<T>(T items) => JsonConvert.SerializeObject(items);
    }
}
