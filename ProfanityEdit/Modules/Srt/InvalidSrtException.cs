using System;

namespace ProfanityEdit.Modules.Srt
{
    public class InvalidSrtException : Exception
    {
        public InvalidSrtException() : base() { }

        public InvalidSrtException(string message) : base(message) { }
    }
}