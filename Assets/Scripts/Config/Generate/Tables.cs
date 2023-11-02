//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Bright.Serialization;



namespace Config
{ 
public partial class Tables
{
    public static Tables Instance;

    private System.Func<string, ByteBuf> _loader;
    private System.Func<string, string, string> _translator;

    private charcter.TbCharacterConfig _TbCharacterConfig;
    public charcter.TbCharacterConfig TbCharacterConfig {
        get {
            if (_TbCharacterConfig == null) {
                _TbCharacterConfig = new charcter.TbCharacterConfig(_loader("charcter_tbcharacterconfig"));
                _TbCharacterConfig.Resolve(this);
                if (this._translator != null) _TbCharacterConfig.TranslateText(this._translator);
            }
            return _TbCharacterConfig;
        }
    }
    
    private common.TbCommonConst _TbCommonConst;
    public common.TbCommonConst TbCommonConst {
        get {
            if (_TbCommonConst == null) {
                _TbCommonConst = new common.TbCommonConst(_loader("common_tbcommonconst"));
                _TbCommonConst.Resolve(this);
                if (this._translator != null) _TbCommonConst.TranslateText(this._translator);
            }
            return _TbCommonConst;
        }
    }
    
    private common.TbL10nConfig _TbL10nConfig;
    public common.TbL10nConfig TbL10nConfig {
        get {
            if (_TbL10nConfig == null) {
                _TbL10nConfig = new common.TbL10nConfig(_loader("common_tbl10nconfig"));
                _TbL10nConfig.Resolve(this);
                if (this._translator != null) _TbL10nConfig.TranslateText(this._translator);
            }
            return _TbL10nConfig;
        }
    }
    
    private common.TbErrorCodeConfig _TbErrorCodeConfig;
    public common.TbErrorCodeConfig TbErrorCodeConfig {
        get {
            if (_TbErrorCodeConfig == null) {
                _TbErrorCodeConfig = new common.TbErrorCodeConfig(_loader("common_tberrorcodeconfig"));
                _TbErrorCodeConfig.Resolve(this);
                if (this._translator != null) _TbErrorCodeConfig.TranslateText(this._translator);
            }
            return _TbErrorCodeConfig;
        }
    }
    
    private common.TbSymbolsConfig _TbSymbolsConfig;
    public common.TbSymbolsConfig TbSymbolsConfig {
        get {
            if (_TbSymbolsConfig == null) {
                _TbSymbolsConfig = new common.TbSymbolsConfig(_loader("common_tbsymbolsconfig"));
                _TbSymbolsConfig.Resolve(this);
                if (this._translator != null) _TbSymbolsConfig.TranslateText(this._translator);
            }
            return _TbSymbolsConfig;
        }
    }
    
    private common.TbKeywordsFilterConfig _TbKeywordsFilterConfig;
    public common.TbKeywordsFilterConfig TbKeywordsFilterConfig {
        get {
            if (_TbKeywordsFilterConfig == null) {
                _TbKeywordsFilterConfig = new common.TbKeywordsFilterConfig(_loader("common_tbkeywordsfilterconfig"));
                _TbKeywordsFilterConfig.Resolve(this);
                if (this._translator != null) _TbKeywordsFilterConfig.TranslateText(this._translator);
            }
            return _TbKeywordsFilterConfig;
        }
    }
    

    public object this[string fileName]
    {
        get
        {
            switch (fileName)
            {
                case "charcter.TbCharacterConfig":
                    return TbCharacterConfig;
                case "common.TbCommonConst":
                    return TbCommonConst;
                case "common.TbL10nConfig":
                    return TbL10nConfig;
                case "common.TbErrorCodeConfig":
                    return TbErrorCodeConfig;
                case "common.TbSymbolsConfig":
                    return TbSymbolsConfig;
                case "common.TbKeywordsFilterConfig":
                    return TbKeywordsFilterConfig;
                default:
                    return null;
            }
        }
    }

    public Tables(System.Func<string, ByteBuf> loader)
    {
        Instance = this;
        _loader = loader;

        PostInit();
    }

    public void Clear()
    {
        _TbCharacterConfig?.Clear();
        _TbCommonConst?.Clear();
        _TbL10nConfig?.Clear();
        _TbErrorCodeConfig?.Clear();
        _TbSymbolsConfig?.Clear();
        _TbKeywordsFilterConfig?.Clear();
    }

    public void TranslateText(System.Func<string, string, string> translator)
    {
        this._translator = translator;
    }
    
    partial void PostInit();
}

}