**开发尚未完成**

**In Progress**

# 项目介绍
通过BuildMechine实现Unity自动化打包流程


# 使用方式
```
BuildMechine.SetPipeline(
    new BuildAction_Print("Start Build Mechine"),
    new BuildAction_Print("Building"),
    new BuildAction_ForceCompilingScript(),
    new BuildAction_WaitScriptCompile(),
    new BuildAction_Print("Build Finished")
);
```

# 自定方式
```
public class BuildAction_CustomAction : BuildAction
{
    // 字段的数值会被保存 
    public string Msg;

    // 属性的数值不会被保存。非常可能丢失
    public string Msg {get; set;}

    public BuildAction_CustomAction(string msg)
    {
        this.Msg = msg;
    }

    public override BuildState OnUpdate()
    {
        Debug.Log(Msg);
        
        // 状态被设置成Success后，一下次tick会进入下一个任务
        return BuildState.Success;

        // 状态被设置成Failure后，一下次tick会结束任务队列
        // return BuildState.Failure;
    }

    public override BuildProgress GetProgress()
    {
        // 返回空不现实进度条
        return null;

        // 返回具体参数现实进度条
    }
}
```

# BatchModeExample
```
// cmd
"F:\Program Files\Unity5.6.0f3\Editor\Unity.exe" -projectpath "F:\BuildMechine" -executeMethod BuildMechineExample.Build -batchmode
```

C#代码中使用`BuildMechine.SetPipeline_BatchMode`代替`BuildMechine.SetPipeline`

# 注意事项
- 内部使用 UnityEngine.JsonUtility。如果自定义BuildAction里边使用Properties和JsonUtility不兼容的List或者Dictionary或者Array。会导致Action的数据丢失。