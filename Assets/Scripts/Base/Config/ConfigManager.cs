using Bright.Serialization;
using Config;

public sealed class ConfigManager
{
    private static ConfigManager _instance;
    
    public static ConfigManager Instance
    {
        get => _instance ??= new ConfigManager();
        set => _instance = value;
    }
    
    public struct GetOneConfigBytes
    {
        public string ConfigName;
    }
    
    private Tables Tables;

    public void Init()
    {
        Tables?.Clear();
        Tables = new Tables(fileName => new ByteBuf(
            EventSystem.Instance.Invoke<GetOneConfigBytes, byte[]>(new GetOneConfigBytes { ConfigName = fileName })));
    }
}