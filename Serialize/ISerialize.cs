namespace SomeSerializeAlgorithm.Serialize
{
    public interface ISerialize
    {
        string SerializeToString<T>(T items) where T : class;
        byte[] SerializeToArray<T>(T items) where T : class;
        T Deserialize<T>(string items) where T : class;
        T Deserialize<T>(byte[] items) where T : class;
    }
}
