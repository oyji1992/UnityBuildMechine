using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace UniGameTools.BuildMechine
{
    public class BuildMechineWindows : EditorWindow
    {
        private class Info
        {
            public int Index;
            public string ActionName;
            public string State;
        }

        private List<Info> Infos = new List<Info>();
        private Vector2 scrollPosition;

        void OnGUI()
        {
            var style = new GUIStyle()
            {
                richText = true,
                fontSize = 11,
                normal = new GUIStyleState() { textColor = Color.grey }
            };

            if (BuildMechine.Instance != null)
            {
                Infos.Clear();

                for (var index = 0; index < BuildMechine.Instance.Actions.Count; index++)
                {
                    var instanceAction = BuildMechine.Instance.Actions[index];

                    var state = "<color=grey>未运行</color>";

                    if (index == BuildMechine.Instance.CurrentActionIndex)
                    {
                        if (BuildMechine.Instance.ErrorStop == false)
                        {
                            state = "<color=yellow>运行中</color>";
                        }
                        else
                        {
                            state = "<color=red>失败！</color>";
                        }
                    }
                    else if (index < BuildMechine.Instance.CurrentActionIndex)
                    {
                        state = "<color=green>已完成</color>";
                    }

                    Infos.Add(new Info()
                    {
                        Index = index,
                        ActionName = instanceAction.GetType().Name,
                        State = state
                    });
                }

                BuildMechine.ShowProgress();
            }

            EditorGUILayout.Space();

            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

            EditorGUILayout.LabelField("Infos : ");

            foreach (var info in Infos)
            {
                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.LabelField((info.Index + 1).ToString("d2") + ".", style, GUILayout.Width(30));
                    EditorGUILayout.LabelField(string.Format("[{0}", info.ActionName), style, GUILayout.Width(300));
                    EditorGUILayout.LabelField(string.Format("] : "), style, GUILayout.Width(20));
                    EditorGUILayout.LabelField(string.Format("[{0}]", info.State), style, GUILayout.Width(30));
                }
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndScrollView();
        }

        void Update()
        {
            if (BuildMechine.Instance != null)
            {
                BuildMechine.Instance.UpdateMethod();
            }
            else
            {
                //_needCheckMechine = false;
                var jsonInstance = BuildMechine.JsonInstance;
                if (jsonInstance != null)
                {
                    BuildMechine.Instance = jsonInstance;
                    BuildMechine.JsonInstance = null;
                }
            }
            Repaint();
        }
    }
}