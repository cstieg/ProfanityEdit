using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace ProfanityEdit.Modules.Srt
{
    public class Srt
    {
        public List<SrtLine> SrtLines = new List<SrtLine>();

        private StringReader _reader;
        public static Regex TimingRegex = new Regex(@"^[0-9]{2}:[0-9]{2}:[0-9]{2},[0-9]{3} --> [0-9]{2}:[0-9]{2}:[0-9]{2},[0-9]{3}$");

        public Srt() { }

        public Srt(string text)
        {
            ReadText(text);
        }

        public void ReadText(string text)
        {
            _reader = new StringReader(text);
            SrtLine nextLine = GetNextLine();
            while (nextLine != null)
            {
                nextLine = GetNextLine();
                SrtLines.Add(nextLine);
            }
        }

        private SrtLine GetNextLine()
        {
            SrtLine srtLine = new SrtLine();
            string line = _reader.ReadLine();

            // Dispose of any extra blank lines, especially at the end of file
            while (line == "")
            {
                line = _reader.ReadLine();
            }

            // End of File
            if (line == null)
            {
                return null;
            }

            // 1st Line - Line number
            try
            {
                if (line.Length > 5 || int.Parse(line) <= 0)
                {
                    throw new Exception();
                }
            }
            catch
            {
                throw new InvalidSrtException("Invalid line number in SRT file -- \r\n" + line);
            }

            srtLine.LineNumber = int.Parse(line);

            // 2nd Line - Timing
            line = _reader.ReadLine();
            try
            {
                if (TimingRegex.Match(line).Success == false)
                {
                    throw new Exception();
                }
            }
            catch
            {
                throw new InvalidSrtException("Invalid time data in SRT file -- \r\n" + line);
            }
            srtLine.StartTime = SrtLine.SrtTimeToSeconds(line.Substring(0, 12));
            srtLine.FinishTime = SrtLine.SrtTimeToSeconds(line.Substring(17, 12));

            // 3rd Line - String
            line = _reader.ReadLine();
            while (line != "")
            {
                srtLine.Text += line + " ";
                line = _reader.ReadLine();
            }

            // 4th Line (blank) will be read and discarded by while loop above

            return srtLine;
        }

    }
}