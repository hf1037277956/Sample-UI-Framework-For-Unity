using UnityEditor;
using UnityEditor.AddressableAssets.Settings;

public class AddressableProcessor : AssetPostprocessor
{
    public const string prefix = "Assets/Res/";

    private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        bool isDirty = false;
        
        foreach (var str in importedAssets)
        {
            if (IsAddressable(str, out string address, out string groupName, out string labelName))
            {
                var setting = AssetDatabase.LoadAssetAtPath<AddressableAssetSettings>("Assets/AddressableAssetsData/AddressableAssetSettings.asset");
                var group = setting.FindGroup(groupName);
                var label = labelName;

                var guid = AssetDatabase.AssetPathToGUID(str);
                var entry = setting.CreateOrMoveEntry(guid, group);
                entry.SetAddress(address);
                entry.SetLabel(label, true);
                
                isDirty = true;
            }
        }
        
        if (isDirty)
        {
            AssetDatabase.SaveAssets();
        }
    }

    private static bool IsAddressable(string asset, out string address, out string groupName, out string labelName)
    {
        if (asset.StartsWith(prefix + Define.UIPrefabPath) && asset.EndsWith(Define.DotPrefab))
        {
            address = asset.Substring(prefix.Length);
            groupName = "Prefabs";
            labelName = Define.Label_default;
            return true;
        }
        if (asset.StartsWith(prefix + Define.SpinePath) && asset.EndsWith("SkeletonData" + Define.DotAsset))
        {
            address = asset.Substring(prefix.Length);
            groupName = "Spines";
            labelName = Define.Label_default;
            return true;
        }
        if (asset.StartsWith(prefix + Define.TexturePath) && asset.EndsWith(Define.DotPng))
        {
            address = asset.Substring(prefix.Length);
            groupName = "Textures";
            labelName = Define.Label_default;
            return true;
        }
        if (asset.StartsWith(prefix + Define.AtlasPath) && asset.EndsWith(Define.DotSpriteAtlas))
        {
            address = asset.Substring(prefix.Length);
            groupName = "UIAtlas";
            labelName = Define.Label_default;
            return true;
        }
        if (asset.StartsWith(prefix + Define.AudioPath) && asset.EndsWith(Define.DotWav))
        {
            address = asset.Substring(prefix.Length);
            groupName = "Audio";
            labelName = Define.Label_default;
            return true;
        }


        address = null;
        groupName = null;
        labelName = null;
        return false;
    }
}