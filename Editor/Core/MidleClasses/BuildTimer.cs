using System;

namespace UniGameTools.BuildMechine
{
    [Serializable]
    public class BuildTimer
    {
        public long StartTime;
        public long EndTime;

        public TimeSpan Duration
        {
            get { return BuildHelper.CalDuration(DateTime.Now.Ticks, StartTime, EndTime); }
        }
    }
}