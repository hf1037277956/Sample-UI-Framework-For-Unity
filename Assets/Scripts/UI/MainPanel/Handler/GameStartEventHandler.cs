using UnityEngine;

public class GameStartEventHandler : AEvent<EventTypes.GameStart>
{
    protected override void Run(EventTypes.GameStart args)
    {
        Debug.Log(args.GameName);

        // 在这里可以执行想要在此事件处理器中执行的Cpt的逻辑
        // 通过UIManager获取MainPanel的实例，然后调用MainPanel的OnGameStart方法
        (UIManager.Instance.GetUICpt(UIName.MainPanel) as MainPanelCpt)?.OnGameStart();
    }
}