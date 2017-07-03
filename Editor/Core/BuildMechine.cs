using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace UniGameTools.BuildMechine
{
    public class BuildMechine
    {
        /// <summary>
        /// Instance
        /// </summary>
        public static BuildMechine Instance;

        /// <summary>
        /// Context
        /// 用于Action之间数据传递
        /// </summary>
        public BuildContext Context = new BuildContext();

        /// <summary>
        /// Actions
        /// </summary>
        [NonSerialized]
        public List<BuildAction> Actions = new List<BuildAction>();

        /// <summary>
        /// 记录各个Actions的时间
        /// </summary>
        public List<BuildTimer> ActionTimers = new List<BuildTimer>();

        /// <summary>
        /// 当前Action的下标
        /// </summary>
        public int CurrentActionIndex;

        /// <summary>
        /// 是否发生了任意错误
        /// 如果是，管线将会暂停
        /// </summary>
        public bool AnyError;

        /// <summary>
        /// 是否正在打包
        /// </summary>
        public static bool IsBuilding
        {
            get { return EditorPrefs.GetBool("BuildMechine.IsBuilding", false); }
            set { EditorPrefs.SetBool("BuildMechine.IsBuilding", value); }
        }

        /// <summary>
        /// 记录管线的总运行时间
        /// </summary>
        public BuildTimer MechineTimer = new BuildTimer();

        public static BuildMechine JsonInstance
        {
            get
            {
                // 解析BuildMechine中的数据
                var mechineJson = EditorPrefs.GetString("BuildMechine.JsonInstance", "");
                if (string.IsNullOrEmpty(mechineJson)) return null;

                var mechine = JsonUtility.FromJson<BuildMechine>(mechineJson);

                // 解析Action数据
                var actionJson = EditorPrefs.GetString("BuildMechine.Actions", "");
                if (string.IsNullOrEmpty(actionJson)) return null;

                var collection = JsonUtility.FromJson<WarperCollection>(actionJson);
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
                    var mechineJson = JsonUtility.ToJson(value, true);
                    EditorPrefs.SetString("BuildMechine.JsonInstance", mechineJson);

                    var warpers = value.Actions.Select(r => new ActionWarper().SetAction(r)).ToList();
                    var collection = new WarperCollection() { Warpers = warpers };

                    var warpersJson = JsonUtility.ToJson(collection);

                    EditorPrefs.SetString("BuildMechine.Actions", warpersJson);

                    // Debug.Log(json);

                }
            }
        }

        /// <summary>
        /// 当前Action
        /// </summary>
        public BuildAction CurrentBuildAction
        {
            get
            {
                return Actions.Count > CurrentActionIndex ? Actions[CurrentActionIndex] : null;
            }
        }
        //    public void Update()
        //    {
        //        EditorApplication.delayCall += () =>
        //        {
        //            UpdateMethod();
        //        };
        //    }

        /// <summary>
        /// 更新方法
        /// </summary>
        public void UpdateMethod()
        {
            if (CurrentBuildAction != null)
            {
                var buildState = CurrentBuildAction.OnUpdate();
                switch (buildState)
                {
                    case BuildState.None:
                    case BuildState.Running:
                        break;
                    case BuildState.Success:
                        {
                            OnActionEnd(CurrentActionIndex);

                            CurrentActionIndex++;

                            if (CurrentBuildAction != null)
                            {
                                OnActionEnter(CurrentActionIndex);

                                Debug.Log("<color=yellow>BuildMechine</color> : Move To Next : <color=orange>" + CurrentBuildAction.GetType().Name + "</color>");
                                JsonInstance = this;
                            }
                            else
                            {
                                BuildFinished(false);
                            }
                        }
                        break;
                    case BuildState.Failure:
                        {
                            Debug.LogError("<color=yellow>BuildMechine</color> : Build Fail!!!");

                            OnActionEnd(CurrentActionIndex);
                            BuildFinished(true);
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void OnActionEnter(int index)
        {
            ActionTimers[index].StartTime = DateTime.Now.Ticks;

            CurrentBuildAction.Context.Merge(this.Context);
        }

        private void OnActionEnd(int index)
        {
            ActionTimers[index].EndTime = DateTime.Now.Ticks;

            this.Context.Merge(CurrentBuildAction.Context);
        }

        private void BuildFinished(bool anyError)
        {
            this.MechineTimer.EndTime = DateTime.Now.Ticks;
            AnyError = anyError;


            // Log All Errors;
            Debug.Log(Context);

            IsBuilding = false;

            JsonInstance = null;

            EditorUtility.ClearProgressBar();

            Debug.LogWarning("<color=yellow>BuildMechine</color> : Build Finished !!!");

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

            IsBuilding = true;

            for (int i = 0; i < actions.Length; i++)
            {
                Instance.ActionTimers.Add(new BuildTimer());
            }

            Instance.OnActionEnter(0);

            Instance.MechineTimer = new BuildTimer()
            {
                StartTime = DateTime.Now.Ticks
            };


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

        /// <summary>
        /// 获得当前进度
        /// </summary>
        /// <returns></returns>
        public BuildProgress GetProgress()
        {
            if (CurrentBuildAction != null) return CurrentBuildAction.GetProgress();

            return null;
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