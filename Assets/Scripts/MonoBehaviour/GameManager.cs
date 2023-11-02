using System;
using System.Reflection;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        UIManager.Instance.ShowPanel(UIName.MainPanel);
        
        // 这是一条测试事件
        EventSystem.Instance.Publish(new EventTypes.GameStart(){GameName = "This event is published by GameManager"});
    }

    private void Init()
    {
        GameObject gui = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("UI/GUI"));
        UnityEngine.Object.DontDestroyOnLoad(gui);
        
        Assembly assembly = Assembly.GetExecutingAssembly();
        UIManager.Instance.Init(assembly);
        EventSystem.Instance.Init(assembly);
        ConfigManager.Instance.Init();
    }

    private void OnDestroy()
    {
        UIManager.Instance = null;
        EventSystem.Instance = null;
        ConfigManager.Instance = null;
        InputLockSystem.Instance = null;
    }
}