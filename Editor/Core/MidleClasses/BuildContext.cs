using System;
using System.Collections.Generic;
using System.Text;

namespace UniGameTools.BuildMechine
{
    [Serializable]
    public class BuildContext
    {
        public List<KV> Contexts = new List<KV>();

        //        public void Merge(BuildContext other)
        //        {
        //            foreach (var info in other.Contexts)
        //            {
        //                Remove(info.Key);
        //                Contexts.Add(info);
        //            }
        //        }

        public void Set(string key, string value)
        {
            Remove(key);
            Contexts.Add(new KV()
            {
                Key = key,
                Value = value
            });
        }

        public void Remove(string key)
        {
            Contexts.RemoveAll(r => r.Key == key);
        }

        public string TryGet(string key, string defaultValue = "")
        {
            var find = Contexts.Find(r => r.Key == key);

            if (find != null)
            {
                return find.Value;
            }

            return defaultValue;
        }

        public bool Contains(string key)
        {
            var find = Contexts.Find(r => r.Key == key);
            if (find != null)
            {
                return true;
            }
            return false;
        }


        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var context in Contexts)
            {
                sb.AppendLine(string.Format("{0} - {1}", context.Key, context.Value));
            }

            return sb.ToString();
        }
    }
}