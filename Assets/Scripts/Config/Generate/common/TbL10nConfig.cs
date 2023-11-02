//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Bright.Serialization;
using System.Collections.Generic;

namespace Config.common
{
   
public partial class TbL10nConfig
{
    private static TbL10nConfig instance;
    public static TbL10nConfig Instance
    {
        get
        {
            instance ??= Tables.Instance.TbL10nConfig;
            return instance;
        }
    }

    private readonly Dictionary<int, common.L10nConfig> _dataMap;
    private readonly List<common.L10nConfig> _dataList;
    
    public TbL10nConfig(ByteBuf _buf)
    {
        _dataMap = new Dictionary<int, common.L10nConfig>();
        _dataList = new List<common.L10nConfig>();
        
        for(int n = _buf.ReadSize() ; n > 0 ; --n)
        {
            common.L10nConfig _v;
            _v = common.L10nConfig.DeserializeL10nConfig(_buf);
            _dataList.Add(_v);
            _dataMap.Add(_v.Id, _v);
        }
        PostInit();
    }

    public Dictionary<int, common.L10nConfig> DataMap => _dataMap;
    public List<common.L10nConfig> DataList => _dataList;

    public common.L10nConfig GetOrDefault(int key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public common.L10nConfig Get(int key) => _dataMap[key];
    public common.L10nConfig this[int key] => _dataMap[key];

    public void Resolve(Tables _tables)
    {
        foreach(var v in _dataList)
        {
            v.Resolve(_tables);
        }
        PostResolve();
    }

    public void TranslateText(System.Func<string, string, string> translator)
    {
        foreach(var v in _dataList)
        {
            v.TranslateText(translator);
        }
    }
    
    public void Clear()
    {
        instance = null;
    }
    
    partial void PostInit();
    partial void PostResolve();
}

}