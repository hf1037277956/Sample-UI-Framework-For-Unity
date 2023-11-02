using Bright.Serialization;

{{
    name = x.name
    namespace = x.namespace
    tables = x.tables

}}

{{cs_start_name_space_grace x.namespace}} 
public partial class {{name}}
{
    public static {{name}} Instance;

    private System.Func<string, ByteBuf> _loader;
    private System.Func<string, string, string> _translator;

    {{~for table in tables ~}}
{{~if table.comment != '' ~}}
    /// <summary>
    /// {{table.escape_comment}}
    /// </summary>
{{~end~}}
    private {{table.full_name}} _{{table.name}};
    public {{table.full_name}} {{table.name}} {
        get {
            if (_{{table.name}} == null) {
                _{{table.name}} = new {{table.full_name}}(_loader("{{table.output_data_file}}"));
                _{{table.name}}.Resolve(this);
                if (this._translator != null) _{{table.name}}.TranslateText(this._translator);
            }
            return _{{table.name}};
        }
    }
    
    {{~end~}}

    public object this[string fileName]
    {
        get
        {
            switch (fileName)
            {
            {{~for table in tables ~}}
                case "{{table.full_name}}":
                    return {{table.name}};
            {{~end~}}
                default:
                    return null;
            }
        }
    }

    public {{name}}(System.Func<string, ByteBuf> loader)
    {
        Instance = this;
        _loader = loader;

        PostInit();
    }

    public void Clear()
    {
    {{~for table in tables ~}}
        _{{table.name}}?.Clear();
    {{~end~}}
    }

    public void TranslateText(System.Func<string, string, string> translator)
    {
        this._translator = translator;
    }
    
    partial void PostInit();
}

{{cs_end_name_space_grace x.namespace}}