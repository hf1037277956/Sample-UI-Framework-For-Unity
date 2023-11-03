using System.IO;

[Invoke]
public class GetOneLubanConfigBytes: AInvokeHandler<ConfigManager.GetOneConfigBytes, byte[]>
{
    public override byte[] Handle(ConfigManager.GetOneConfigBytes args)
    {
        byte[] bytes = null;

        string configFilePath = $"Assets/Resources/Config/cs/{args.ConfigName}.bytes";
                
        bytes = File.ReadAllBytes(configFilePath);
        
        // if (Define.IsEditor)
        // {
        //     
        // }
        // else
        // {
        //     var textAsset = ResourceComponent.Instance.LoadAsset<TextAsset>($"Config/{args.ConfigName}.bytes", null);
        //     bytes = textAsset.bytes;
        //
        //     ResourceComponent.Instance.ReleaseAsset(textAsset);
        // }

        return bytes;
    }
}