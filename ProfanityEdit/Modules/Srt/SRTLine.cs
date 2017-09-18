namespace ProfanityEdit.Modules.Srt
{
    public class SrtLine
    {
        public int LineNumber { get; set; }
        public float StartTime { get; set; }
        public float FinishTime { get; set; }
        public string Text { get; set; }

        public static float SrtTimeToSeconds(string srtTime)
        {
            int hour = int.Parse(srtTime.Substring(0, 2));
            int minute = int.Parse(srtTime.Substring(3, 2));
            int second = int.Parse(srtTime.Substring(6, 2));
            float millisecond = float.Parse("0." + srtTime.Substring(9, 3));
            int seconds = (hour * 3600) + (minute * 60) + second;
            return seconds + millisecond;
        }
    }
}