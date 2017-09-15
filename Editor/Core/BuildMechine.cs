using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UniGameTools.BuildMechine.BuildActions;
using UnityEditor;
using UnityEngine;

namespace UniGameTools.BuildMechine
{
    public class BuildMechine
    {
        static BuildMechine()
        {
            Application.logMessageReceived += OnLog;
        }

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
        /// 运行失败的Action
        /// </summary>
        public BuildAction FailureAction;

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

        public static bool BatchMode
        {
            get { return EditorPrefs.GetBool("BuildMechine.BatchMode", false); }
            set
            {
                EditorPrefs.SetBool("BuildMechine.BatchMode", value);
            }
        }

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

                foreach (var action in mechine.Actions)
                {
                    action.Mechine = mechine;
                }

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

                    //                    Debug.Log(mechineJson);

                    var warpers = value.Actions.Select(r => new ActionWarper().SetAction(r)).ToList();
                    var collection = new WarperCollection() { Warpers = warpers };

                    var warpersJson = JsonUtility.ToJson(collection, true);

                    EditorPrefs.SetString("BuildMechine.Actions", warpersJson);

                    //                    Debug.Log(warpersJson);

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
                if (Actions.Count > CurrentActionIndex) return Actions[CurrentActionIndex];

                else return null;
            }
        }

        /// <summary>
        /// 更新方法
        /// </summary>
        public void UpdateMethod()
        {
            if (EditorApplication.isCompiling) return;

            if (CurrentBuildAction != null)
            {
                BuildState buildState;
                try
                {
                    buildState = CurrentBuildAction.OnUpdate();
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                    buildState = BuildState.Failure;

                    var tryGet = Context.TryGet("Error");

                    if (string.IsNullOrEmpty(tryGet))
                    {
                        tryGet = e.ToString();
                    }
                    else
                    {
                        tryGet += "\n" + e.ToString();
                    }

                    Context.Set("Error", tryGet);
                }

                switch (buildState)
                {
                    case BuildState.None:
                    case BuildState.Running:
                        break;
                    case BuildState.Success:
                        {
                            OnActionEnd(CurrentActionIndex);

                            Debug.Log("BuildMechine - End - " + CurrentBuildAction.GetType().Name);
                            Debug.Log(JsonUtility.ToJson(CurrentBuildAction));

                            AssetDatabase.Refresh();
                            AssetDatabase.SaveAssets();

                            CurrentBuildAction.OnExit();

                            CurrentActionIndex++;

                            if (CurrentBuildAction != null)
                            {
                                OnActionEnter(CurrentActionIndex);

                                Debug.Log("BuildMechine - Run - " + CurrentBuildAction.GetType().Name);
                                Debug.Log(JsonUtility.ToJson(CurrentBuildAction));
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
                            Debug.LogWarning("BuildMechine - Build Fail!!!");
                            Debug.Log(JsonUtility.ToJson(CurrentBuildAction));

                            OnActionEnd(CurrentActionIndex);

                            if (this.FailureAction != null)
                            {
                                this.FailureAction.Mechine = this;
                                Debug.Log("BuildMechine - ErrorAction - " + this.FailureAction.GetType().Name);
                                Debug.Log(JsonUtility.ToJson(CurrentBuildAction));

                                var onUpdate = this.FailureAction.OnUpdate();
                            }

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
        }

        private void OnActionEnd(int index)
        {
            ActionTimers[index].EndTime = DateTime.Now.Ticks;
        }

        private void BuildFinished(bool anyError)
        {
            this.MechineTimer.EndTime = DateTime.Now.Ticks;
            AnyError = anyError;

            // Log All Errors;
            Debug.Log("Context : \n" + Context);

            IsBuilding = false;

            JsonInstance = null;

            EditorUtility.ClearProgressBar();

            Debug.LogWarning("<color=yellow>BuildMechine</color> : Build Finished !!!");

            if (BatchMode)
            {
                Debug.Log("Exit");

                EditorApplication.Exit(anyError ? -1 : 0);
            }

        }

        private bool IsFinished
        {
            get { return CurrentBuildAction == null; }
        }

        public static BuildMechine NewPipeline()
        {
            Instance = new BuildMechine();
            Instance.Actions = new List<BuildAction>();
            return Instance;
        }

        public BuildMechine AddActions(params BuildAction[] actions)
        {
            this.Actions.AddRange(actions);

            for (int i = 0; i < actions.Length; i++)
            {
                Instance.ActionTimers.Add(new BuildTimer());
            }

            return this;
        }

        public BuildMechine SetOnFailure(BuildAction action)
        {
            this.FailureAction = action;

            return this;
        }

        public void Run(bool batchMood = false)
        {
            LogFile.Clean();

            BuildHelper.AddBuildNum();

            this.Context.Set("buildnum", BuildHelper.GetBuildNum().ToString());

            BatchMode = batchMood;

            this.Actions.Add(new BuildAction_End());

            this.Actions = Actions.Select(r => r.DeepCopy()).ToList();

            if (this.FailureAction != null)
            {
                this.FailureAction = this.FailureAction.DeepCopy();
            }

            foreach (var action in this.Actions)
            {
                action.Mechine = this;
            }

            Instance.ActionTimers.Add(new BuildTimer());

            Instance.MechineTimer = new BuildTimer()
            {
                StartTime = DateTime.Now.Ticks
            };

            IsBuilding = true;

            Instance.CurrentActionIndex = 0;
            Instance.OnActionEnter(0);

            var window = EditorWindow.GetWindow<BuildMechineWindows>();

            window.Focus();

        }

        private static void OnLog(string condition, string stacktrace, LogType type)
        {
            var msg = "";

            if (condition.EndsWith("\n") == false)
            {
                condition += "\n";
            }

            if (stacktrace.EndsWith("\n") == false)
            {
                stacktrace += "\n";
            }

            switch (type)
            {
                case LogType.Error:
                case LogType.Assert:
                case LogType.Exception:
                    msg = string.Format("\n#######{0}#######\n" +
                                        "{1}" +
                                        "{2}" +
                                        "======={0}=======\n\n", type, condition, stacktrace);
                    break;
                case LogType.Warning:
                case LogType.Log:
                    msg = string.Format("{1}\n", type, condition);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("type", type, null);
            }

            LogFile.Append(msg);
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

    public static class LogFile
    {
        public static void Append(string msg)
        {
            File.AppendAllText("Temp/BuildMechineLog.txt", msg);
        }

        public static string ReadAll()
        {
            return File.ReadAllText("Temp/BuildMechineLog.txt");
        }

        public static void Clean()
        {
            File.WriteAllText("Temp/BuildMechineLog.txt", "");
        }
    }

    public static class StringParse
    {
        public static void Parse(this BuildMechine mechine, ref string parse)
        {
            parse = Replace(parse, "projectname", Application.productName);

            if (mechine.Context != null)
            {
                foreach (var kv in mechine.Context.Contexts)
                {
                    parse = Replace(parse, kv.Key.ToLower(), kv.Value);
                }
            }


            parse = Replace(parse, "mechinestate", mechine.BuildState());
            parse = Replace(parse, "log", LogFile.ReadAll());
        }

        public static string BuildState(this BuildMechine mechine)
        {
            var sb = new StringBuilder();

            for (var index = 0; index < mechine.Actions.Count; index++)
            {
                var mechineAction = mechine.Actions[index];

                var state = "Waiting";

                if (index < mechine.CurrentActionIndex)
                {
                    state = "Finished";
                }

                if (index == mechine.CurrentActionIndex)
                {
                    state = "Running";
                }


                sb.AppendLine(string.Format("{0} - {1} - {2}", index + 1, mechineAction.GetType().Name, state));

            }

            return sb.ToString();
        }

        public static string Replace(string origin, string key, string result)
        {
            if (origin == null) return "";

            return origin.Replace("${" + key.ToLower() + "}", result);
        }
    }

}