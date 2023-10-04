using System;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public class ReferenceCollectorData
{
	public string key;
	public string type;
	public UnityEngine.Object data;
}

public class ReferenceCollectorDataComparer : IComparer<ReferenceCollectorData>
{
	public int Compare(ReferenceCollectorData x, ReferenceCollectorData y)
	{
		return string.Compare(x?.key, y?.key, StringComparison.Ordinal);
	}
}

public class ReferenceCollector : MonoBehaviour, ISerializationCallbackReceiver
{
	public List<ReferenceCollectorData> data = new();
	
	private readonly Dictionary<string, ReferenceCollectorData> dict = new();
	
#if UNITY_EDITOR
	public void Add(string key, string type, UnityEngine.Object obj)
	{
		SerializedObject serializedObject = new SerializedObject(this);
		SerializedProperty dataProperty = serializedObject.FindProperty("data");
		int i;
		for (i = 0; i < data.Count; i++)
		{
			if (data[i].key == key) break;
		}

		if (i != data.Count)
		{
			SerializedProperty element = dataProperty.GetArrayElementAtIndex(i);
			element.FindPropertyRelative("type").stringValue = type;
			element.FindPropertyRelative("data").objectReferenceValue = obj;
		}
		else
		{
			dataProperty.InsertArrayElementAtIndex(i);
			SerializedProperty element = dataProperty.GetArrayElementAtIndex(i);
			element.FindPropertyRelative("key").stringValue = key;
			element.FindPropertyRelative("type").stringValue = type;
			element.FindPropertyRelative("data").objectReferenceValue = obj;
		}

		EditorUtility.SetDirty(this);
		serializedObject.ApplyModifiedProperties();
		serializedObject.UpdateIfRequiredOrScript();
	}

	public void Remove(string key)
	{
		SerializedObject serializedObject = new SerializedObject(this);
		SerializedProperty dataProperty = serializedObject.FindProperty("data");
		int i;
		for (i = 0; i < data.Count; i++)
		{
			if (data[i].key == key)
			{
				break;
			}
		}

		if (i != data.Count)
		{
			dataProperty.DeleteArrayElementAtIndex(i);
		}

		EditorUtility.SetDirty(this);
		serializedObject.ApplyModifiedProperties();
		serializedObject.UpdateIfRequiredOrScript();
	}

	public void Clear()
	{
		SerializedObject serializedObject = new SerializedObject(this);
		var dataProperty = serializedObject.FindProperty("data");
		dataProperty.ClearArray();
		EditorUtility.SetDirty(this);
		serializedObject.ApplyModifiedProperties();
		serializedObject.UpdateIfRequiredOrScript();
	}

	public void Sort()
	{
		SerializedObject serializedObject = new SerializedObject(this);
		data.Sort(new ReferenceCollectorDataComparer());
		EditorUtility.SetDirty(this);
		serializedObject.ApplyModifiedProperties();
		serializedObject.UpdateIfRequiredOrScript();
	}
#endif

	public T Get<T>(string key) where T : class
	{
		if (!dict.TryGetValue(key, out var collectorData))
		{
			return null;
		}
		return collectorData.data as T;
	}
		
	public UnityEngine.Object GetObject(string key)
	{
		if (!dict.TryGetValue(key, out var collectorData))
		{
			return null;
		}
		return collectorData.data;
	}


	public void OnBeforeSerialize()
	{
		
	}

	public void OnAfterDeserialize()
	{
		dict.Clear();
		foreach (ReferenceCollectorData referenceCollectorData in data)
		{
			this.dict.TryAdd(referenceCollectorData.key, referenceCollectorData);
		}
	}
}