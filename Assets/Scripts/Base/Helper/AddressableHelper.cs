using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.U2D;

public static class AddressableHelper
{
        public static GameObject LoadUIPrefab(string name, List<AsyncOperationHandle> handles)
        {
            var obj = LoadAsset<GameObject>($"{Define.UIPrefabPath}{name}{Define.DotPrefab}", handles);
            if (obj == null)
            {
                return null;
            }
            
            var go = UnityEngine.Object.Instantiate(obj, GuiSystem.Instance?.Layer_Create);
            go.name = obj.name;
            return go;
        }
        
        public static async UniTask<GameObject> LoadUIPrefabAsync(string name, List<AsyncOperationHandle> handles)
        {
            var obj = await LoadAssetAsync<GameObject>($"{Define.UIPrefabPath}{name}{Define.DotPrefab}", handles);
            if (obj == null)
            {
                return null;
            }
            
            var go = UnityEngine.Object.Instantiate(obj, GuiSystem.Instance?.Layer_Create);
            go.name = obj.name;
            return go;
        }
        
        public static T LoadAsset<T>(object key, List<AsyncOperationHandle> handles)
        {
            var op = Addressables.LoadAssetAsync<T>(key);
            handles?.Add(op);

            op.WaitForCompletion();

            if (op.Status == AsyncOperationStatus.Failed)
            {
                Debug.LogError($"LoadAsset failed! key:{key}");
            }

            return op.Result;
        }
        
        public static async UniTask<T> LoadAssetAsync<T>( object key, List<AsyncOperationHandle> handles)
        {
            var op = Addressables.LoadAssetAsync<T>(key);
            handles?.Add(op);

            await op.Task;

            if (op.Status == AsyncOperationStatus.Failed)
            {
                Debug.LogError($"LoadAssetAsync failed! key:{key}");
            }

            return op.Result;
        }
        
        public static async UniTask<SpriteAtlas> LoadSpriteAtlasAsync(string name, List<AsyncOperationHandle> handles)
        {
            return await LoadAssetAsync<SpriteAtlas>($"{Define.AtlasPath}{name}{Define.DotSpriteAtlas}", handles);
        }
        
        public static void ReleaseAssets(List<AsyncOperationHandle> handles)
        {
            foreach (var handle in handles)
            {
                Addressables.Release(handle);
            }
            handles.Clear();
        }
}