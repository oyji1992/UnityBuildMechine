using UnityEditor;

public class BuildMechineWindows : EditorWindow
{
    void OnGUI()
    {
        if (EditorApplication.isCompiling)
        {
            EditorGUILayout.LabelField("Compiling...");
            return;
        }

        if (BuildMechine.Instance != null)
        {
            EditorGUILayout.LabelField(string.Format("当前第 {0} 步 : ({1})，总共 {2} 步 ",
                BuildMechine.Instance.CurrentActionIndex + 1,
                BuildMechine.Instance.CurrentBuildAction.GetType(),
                BuildMechine.Instance.Actions.Count));

            BuildMechine.ShowProgress();
        }
    }

    private bool _needCheckMechine = true;

    void Update()
    {
        if (BuildMechine.Instance != null)
        {
            BuildMechine.Instance.UpdateMethod();
            return;
        }
        else
        {
            if (_needCheckMechine == false)
            {
                this.Close();
                return;
            }

            //_needCheckMechine = false;

            var jsonInstance = BuildMechine.JsonInstance;
            if (jsonInstance != null)
            {
                BuildMechine.Instance = jsonInstance;
                BuildMechine.JsonInstance = null;
            }
        }

    }
}