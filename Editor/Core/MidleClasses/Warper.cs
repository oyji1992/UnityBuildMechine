using System;
using UnityEngine;

namespace UniGameTools.BuildMechine
{
    [Serializable]
    public class Warper
    {
        public string Type;

        public string ActionJson;

        public Warper SetAction(BuildAction action)
        {
            ActionJson = JsonUtility.ToJson(action);
            Type = action.GetType().FullName;

            return this;
        }

        public BuildAction GetAction()
        {
            var type = typeof(BuildMechine).Assembly.GetType(Type);

            return JsonUtility.FromJson(ActionJson, type) as BuildAction;
        }
    }
}