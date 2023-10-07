using System;
using System.IO;
using UnityEditor;
using UnityEngine;

public static class UICodeGenerater
{
    [MenuItem("Assets/UI_GeneratePanelCpt", true, -52)]
    public static bool UI_GeneratePanelCptValidate()
    {
        GameObject go = Selection.activeObject as GameObject;
        if (go == null)
        {
            return false;
        }
            
        ReferenceCollector rc = go.GetComponent<ReferenceCollector>();
        if (rc == null)
        {
            return false;
        }
            
        return go.GetComponent<LayerDesc>() != null;
    }
    
    [MenuItem("Assets/UI_GeneratePanelCpt", false, -52)]
    public static void UI_GeneratePanelCpt()
    {
        GameObject go = Selection.activeObject as GameObject;

        //string path = AssetDatabase.GetAssetPath(go);
            
        // int index2 = path.LastIndexOf('/');
        // int index1 = path.LastIndexOf('/', index2 - 1);
        // string panelName = path.Substring(index1 + 1, index2 - index1 - 1);
        string cptName = go.name;

        GeneratePanelCpt(go, cptName, cptName);
        AddPanelName(cptName);
        
        AssetDatabase.Refresh();
    }
        
    // [MenuItem("Assets/HUI_SpawnUICpt", true, -51)]
        // public static bool HUI_SpawnUICptValidate()
        // {
        //     GameObject go = Selection.activeObject as GameObject;
        //     if (go == null)
        //     {
        //         return false;
        //     }
        //     
        //     ReferenceCollector rc = go.GetComponent<ReferenceCollector>();
        //     if (rc == null)
        //     {
        //         return false;
        //     }
        //     
        //     return go.GetComponent<LayerDesc>() == null;
        // }
    
        // [MenuItem("Assets/HUI_SpawnUICpt", false, -51)]
        // public static void HUI_SpawnUICpt()
        // {
        //     GameObject go = Selection.activeObject as GameObject;
        //
        //     string path = AssetDatabase.GetAssetPath(go);
        //     bool isMT = path.StartsWith("Assets/ResMT/");
        //     string MTStr = isMT? "MT" : "";
        //     
        //     int index2 = path.LastIndexOf('/');
        //     int index1 = path.LastIndexOf('/', index2 - 1);
        //     string panelId = path.Substring(index1 + 1, index2 - index1 - 1);
        //     string cptName = go.name;
        //
        //     SpawnUICpt_G(go, MTStr, panelId, cptName);
        //     SpawnUICpt(go, MTStr, panelId, cptName);
        //     SpawnUICptSystem(MTStr, panelId, cptName);
        //
        //     AssetDatabase.Refresh();
        // }

    private static void GeneratePanelCpt(GameObject go, string panelName, string cptName)
    {
        string filePath = $"{Application.dataPath}/Scripts/UI/{panelName}/";
        if (!Directory.Exists(filePath)) 
        { 
            Directory.CreateDirectory(filePath);
        }
        filePath = $"{filePath}{cptName}Cpt.cs";

        string content = File.ReadAllText($"{Application.dataPath}/Scripts/Editor/UI/CodeGenerate/PanelCpt.txt");
        content = content.Replace("[[PanelName]]", panelName);
        content = content.Replace("[[CptName]]", cptName);

        string regionBindingStr = "#region Binding\r\n";
        string endRegionStr = "#endregion\r\n";
        int index1 = content.IndexOf(regionBindingStr, StringComparison.Ordinal) + regionBindingStr.Length;
        int index2 = content.IndexOf(endRegionStr, index1, StringComparison.Ordinal);
        string bindingSourceStr = content.Substring(index1, index2 - index1);

        string bindingTargetStr = "";
        foreach (var data in go.GetComponent<ReferenceCollector>().data)
        {
            bindingTargetStr += bindingSourceStr.Replace("[[CtrlType]]", data.type).Replace("[[CtrlName]]", data.key);
        }
        content = content.Replace(bindingSourceStr, bindingTargetStr);
            
        File.WriteAllText(filePath, content);
    }
        
    private static void AddPanelName(string panelName)
    {
        string filePath = $"{Application.dataPath}/Scripts/Base/UI/UIName.cs";
        if(!File.Exists(filePath))
        {
            Debug.LogError(" UIName.cs不存在!");
            return;
        }
    
        string content = File.ReadAllText(filePath);
        string panelIdStr1 = $"public const string {panelName} = \"{panelName}\";";
        string panelIdStr2 = $"public const string {panelName} = \"{panelName}\";\n";
        if (content.Contains(panelIdStr1))
        {
            return;
        }
    
        int index = content.IndexOf("// 在这里添加新的UI名字", StringComparison.Ordinal);
        content = content.Insert(index, panelIdStr2);
    
        File.WriteAllText(filePath, content);
    }
}