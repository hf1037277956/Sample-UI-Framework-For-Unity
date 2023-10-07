using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

[CustomEditor(typeof (ReferenceCollector))]
[CanEditMultipleObjects]
public class ReferenceCollectorEditor : Editor
{
    private static readonly string[] classNames = new[]{
	    nameof(Object),
	    nameof(GameObject),
	    nameof(Transform),
	    nameof(CanvasGroup),
	    nameof(Graphic),
	    nameof(Text),
	    nameof(Image),
	    nameof(RawImage),
	    nameof(Slider),
	    nameof(Scrollbar),
	    nameof(Dropdown),
	    nameof(Toggle),
        nameof(ToggleGroup),
        nameof(Button),
        nameof(ScrollRect),
        nameof(InputField),
    };

	private static readonly Dictionary<string, Type> classTypes = new Dictionary<string, Type>()
    {
	    {nameof(Object), typeof(Object)},
	    {nameof(GameObject), typeof(GameObject)},
	    {nameof(Transform), typeof(Transform)},
	    {nameof(CanvasGroup), typeof(CanvasGroup)},
	    {nameof(Graphic), typeof(Graphic)},
	    {nameof(Text), typeof(Text)},
	    {nameof(Image), typeof(Image)},
	    {nameof(RawImage), typeof(RawImage)},
	    {nameof(Slider), typeof(Slider)},
	    {nameof(Scrollbar), typeof(Scrollbar)},
	    {nameof(Dropdown), typeof(Dropdown)},
	    {nameof(Toggle), typeof(Toggle)},
	    {nameof(ToggleGroup), typeof(ToggleGroup)},
	    {nameof(Button), typeof(Button)},
	    {nameof(InputField), typeof(InputField)},
	    {nameof(ScrollRect), typeof(ScrollRect)},
	    {nameof(HorizontalLayoutGroup), typeof(HorizontalLayoutGroup)},
	    {nameof(VerticalLayoutGroup), typeof(VerticalLayoutGroup)},
	    {nameof(GridLayoutGroup), typeof(GridLayoutGroup)},
    };
    
    private string searchKey
	{
		get => _searchKey;
		set
		{
			if (_searchKey != value)
			{
				_searchKey = value;
				heroPrefab = referenceCollector.Get<Object>(searchKey);
			}
		}
	}

	private ReferenceCollector referenceCollector;

	private Object heroPrefab;

	private string _searchKey = "";

    private ReorderableList dataList;
    private int? removeIndex;

	private void DelNullReference()
	{
		var dataProperty = serializedObject.FindProperty("data");
		for (int i = dataProperty.arraySize - 1; i >= 0; i--)
		{
			var subProperty = dataProperty.GetArrayElementAtIndex(i).FindPropertyRelative("data");
			if (subProperty.objectReferenceValue == null)
			{
				dataProperty.DeleteArrayElementAtIndex(i);
			}
		}
	}

	private void OnEnable()
	{
		referenceCollector = (ReferenceCollector) target;

        dataList = new ReorderableList(serializedObject, serializedObject.FindProperty("data"), true, true, false, false);
		//绘制元素
        dataList.drawElementCallback = (rect, i, a ,b ) =>
        {
            var itemData = dataList.serializedProperty.GetArrayElementAtIndex(i);
			
			rect.height = EditorGUIUtility.singleLineHeight;

			var rect1 = new Rect(rect)
			{
				width = rect.width / 3 - 10
			};
			var rect2 = new Rect(rect1)
			{
				x = rect1.x + rect1.width + 5
			};
			var rect3 = new Rect(rect2)
			{
				x = rect2.x + rect2.width + 5
			};
			var rect4 = new Rect(rect3.x + rect3.width + 5, rect3.y, 20, 20);

			string oldKey = itemData.FindPropertyRelative("key").stringValue;
			string newKey = EditorGUI.TextField(rect1, oldKey);
			string type = itemData.FindPropertyRelative("type").stringValue ?? classNames[0];
			int index = Array.IndexOf(classNames, type);
			int newIndex = EditorGUI.Popup(rect2, index, classNames);
			string newType = classNames[newIndex];
			var newT = classTypes[newType];
			var obj = itemData.FindPropertyRelative("data").objectReferenceValue;
			var newObj = obj;
			if (index <= 1)
			{
				var go = obj as GameObject;
				if (go != null)
				{
					if (newIndex >= 2) newObj = go.GetComponent(newT);
				}
			}
			else
			{
				var c = obj as Component;
				if (c != null)
				{
					if (newIndex == 1) newObj = c.gameObject;
					else if (newIndex >= 2) newObj = c.gameObject.GetComponent(newT);
				}
				else
				{
					var go = obj as GameObject;
					if (go != null)
					{
						if (newIndex == 1) newObj = go;
						else if (newIndex >= 2) newObj = go.GetComponent(newT);
					}
				}
			}

			newObj = EditorGUI.ObjectField(rect3, newObj, classTypes[newType], true);

			if (GUI.Button(rect4, "X"))
			{
				removeIndex = i;
			}
			else if (newKey != oldKey || newIndex != index || newObj != obj)
			{
				itemData.FindPropertyRelative("key").stringValue = newKey;
				itemData.FindPropertyRelative("type").stringValue = newType;
				if (newObj != null) itemData.FindPropertyRelative("data").objectReferenceValue = newObj;
			}
        };
        //绘制表头
        dataList.drawHeaderCallback = rect =>
        {
	        GUI.Label(rect, "引用列表");
        };
    }

	public override void OnInspectorGUI()
	{
		Undo.RecordObject(referenceCollector, "Changed Settings");

		var dataProperty = serializedObject.FindProperty("data");
		GUILayout.BeginHorizontal();
		if (GUILayout.Button("添加引用"))
		{
			AddReference(dataProperty, Guid.NewGuid().GetHashCode().ToString(), null);
		}
		if (GUILayout.Button("全部删除"))
		{
			dataProperty.ClearArray();
		}
		if (GUILayout.Button("删除空引用"))
		{
			DelNullReference();
		}
		if (GUILayout.Button("排序"))
		{
			referenceCollector.Sort();
		}
		EditorGUILayout.EndHorizontal();
		
		EditorGUILayout.BeginHorizontal();
		searchKey = EditorGUILayout.TextField(searchKey);
		EditorGUILayout.ObjectField(heroPrefab, typeof (Object), false);
		if (GUILayout.Button("删除"))
		{
			referenceCollector.Remove(searchKey);
			heroPrefab = null;
		}
		GUILayout.EndHorizontal();
		EditorGUILayout.Space();

		//绘制列表
		dataList.DoLayoutList();

		//删除列表项
		if (removeIndex.HasValue)
		{
			dataProperty.DeleteArrayElementAtIndex(removeIndex.Value);
			removeIndex = null;
		}
		
		//拖动添加引用
		var eventType = Event.current.type;
		if (eventType == EventType.DragUpdated || eventType == EventType.DragPerform)
		{
			// Show a copy icon on the drag
			DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
		
			if (eventType == EventType.DragPerform)
			{
				DragAndDrop.AcceptDrag();
				foreach (var o in DragAndDrop.objectReferences)
				{
					AddReference(dataProperty, o.name, o);
				}
			}
		
			Event.current.Use();
		}

		serializedObject.ApplyModifiedProperties();
		serializedObject.UpdateIfRequiredOrScript();
	}
	
	private void AddReference(SerializedProperty dataProperty, string key, Object obj)
	{
		int index = dataProperty.arraySize;
		dataProperty.InsertArrayElementAtIndex(index);
		var element = dataProperty.GetArrayElementAtIndex(index);
		string type = classNames[0];
		var go = obj as GameObject;
		if (go != null)
		{
			int i;
			for (i = classNames.Length - 1; i > 2; i--)
			{
				string tempType = classNames[i];
				var tempT = classTypes[tempType];
				var c = go.GetComponent(tempT);
				if (c != null)
				{
					type = tempType;
					obj = c;
					break;
				}
			}

			if (i == 2) type = classNames[1];
		}

		element.FindPropertyRelative("key").stringValue = key;
		element.FindPropertyRelative("type").stringValue = type;
		element.FindPropertyRelative("data").objectReferenceValue = obj;
	}
}
