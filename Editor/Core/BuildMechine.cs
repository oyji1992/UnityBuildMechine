using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace UniGameTools.BuildMechine
{
    public class BuildMechine
    {
        public static BuildMechine Instance;

        public List<Info> Infos = new List<Info>();

        [NonSerialized]
        public List<BuildAction> Actions = new List<BuildAction>();

        public int CurrentActionIndex;

        public static bool IsBuilding
        {
            get { return EditorPrefs.GetBool("BuildMechine.IsBuilding", false); }
            set { EditorPrefs.SetBool("BuildMechine.IsBuilding", value); }
        }

        public static BuildMechine JsonInstance
        {
            get
            {
                //            JsonSerializerSettings setting = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };
                //            return JsonConvert.DeserializeObject<BuildMechine>(s, setting);

                var s = EditorPrefs.GetString("BuildMechine.JsonInstance", "");
                if (string.IsNullOrEmpty(s)) return null;

                Debug.Log("Load Json Instance");
                var mechine = JsonUtility.FromJson<BuildMechine>(s);

                var s2 = EditorPrefs.GetString("BuildMechine.Actions", "");
                if (string.IsNullOrEmpty(s2))
                {
                    return null;
                }

                var collection = JsonUtility.FromJson<WarperCollection>(s2);
                mechine.Actions = collection.Warpers.Select(r => r.GetAction()).ToList();
                
                return mechine;
            }
            set
            {
                if (value == null)
                {
                    EditorPrefs.DeleteKey("BuildMechine.JsonInstance");
                    EditorPrefs.DeleteKey("BuildMechine.Actions");
                }
                else
                {
                    //                JsonSerializerSettings setting = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };
                    //                var json = JsonConvert.SerializeObject(value, Formatting.Indented, setting);
                    //                EditorPrefs.SetString("BuildMechine.JsonInstance", json);

                    var json = JsonUtility.ToJson(value);
                    EditorPrefs.SetString("BuildMechine.JsonInstance", json);

                    var warpers = value.Actions.Select(r => new Warper().SetAction(r)).ToList();    
                    var collection = new WarperCollection(){Warpers = warpers};

                    var warpersJson = JsonUtility.ToJson(collection);
                    //                Debug.LogError("actions : " + warpersJson);

                    EditorPrefs.SetString("BuildMechine.Actions", warpersJson);
                    //                Debug.Log("Save : ");
                    //                Debug.Log(serializeObject);
                }
            }
        }

        public BuildAction CurrentBuildAction
        {
            get
            {
                return Actions.Count > CurrentActionIndex ? Actions[CurrentActionIndex] : null;
            }
        }

        public BuildProgress GetProgress()
        {
            if (CurrentBuildAction != null) return CurrentBuildAction.GetProgress();

            return null;
        }

        //    public void Update()
        //    {
        //        EditorApplication.delayCall += () =>
        //        {
        //            UpdateMethod();
        //        };
        //    }

        public void UpdateMethod()
        {
            if (CurrentBuildAction != null)
            {
                var buildState = CurrentBuildAction.OnUpdate();
                switch (buildState)
                {
                    case BuildState.None:
                        {
                            // Debug.Log("Start Action: " + CurrentBuildAction.GetType());

                            // CurrentBuildAction.State = BuildState.InProgress;
                            // CurrentBuildAction.OnEnter();
                        }
                        break;
                    case BuildState.Running:
                        {
                            // CurrentBuildAction.OnUpdate();
                        }
                        break;
                    case BuildState.Success:
                        {
                            Infos.AddRange(CurrentBuildAction.Infos);

                            CurrentActionIndex++;

                            if (CurrentBuildAction != null)
                            {
                                Debug.Log("Start Next Step : " + CurrentBuildAction.GetType());
                                JsonInstance = this;
                            }
                            else
                            {
                                BuildFinished();
                            }


                        }
                        break;
                    case BuildState.Failure:
                        {
                            Infos.AddRange(CurrentBuildAction.Infos);
                            Debug.LogError("打包结束。打包失败了");
                            CurrentActionIndex = int.MaxValue;
                            BuildFinished();
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void BuildFinished()
        {
            // Log All Errors;
            foreach (var error in Infos)
            {
                Debug.LogError(error);
            }

            JsonInstance = null;

            EditorWindow.GetWindow<BuildMechineWindows>().Close();

            EditorUtility.ClearProgressBar();

            Debug.Log("--------------------打包结束--------------------");
        }

        public bool IsFinished
        {
            get { return CurrentBuildAction == null; }
        }

        /// <summary>
        /// 准备好之后。调用Update就开始进入建造管道了
        /// </summary>
        public static void SetPipeline(params BuildAction[] actions)
        {
            var window = EditorWindow.GetWindow<BuildMechineWindows>();

            window.Focus();

            Instance = new BuildMechine();

            Instance.Actions = actions.ToList();

            Instance.CurrentActionIndex = 0;

            //        for (var i = 0; i < actions.Length; i++)
            //        {
            //            var a = actions[i];
            //
            //            a.Mechine = Instance;
            //
            //            if (i + 1 < actions.Length)
            //            {
            //                a.NextAction = actions[i + 1];
            //            }
            //        }
            //
            //        Instance.CurrentBuildAction = actions[0];
        }

        public static void ShowProgress()
        {
            if (Instance != null)
            {
                if (Instance.IsFinished) return;

                var progress = Instance.GetProgress();
                if (progress != null)
                {
                    EditorUtility.DisplayProgressBar(progress.Title, progress.Content, progress.Porgress);
                }
            }
        }
    }
}