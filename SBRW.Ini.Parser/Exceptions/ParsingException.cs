using System;

namespace SBRW.Ini.Parser.Exceptions
{
    /// <summary>
    /// Represents an error ococcurred while parsing data 
    /// </summary>
    public class ParsingException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        public Version LibVersion {get; private set;}
        /// <summary>
        /// 
        /// </summary>
        public int LineNumber {get; private set;}
        /// <summary>
        /// 
        /// </summary>
        public string LineValue {get; private set;}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public ParsingException(string msg)
            :this(msg, 0, string.Empty, null) 
        {}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="innerException"></param>
        public ParsingException(string msg, Exception innerException)
            :this(msg, 0, string.Empty, innerException) 
        {}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="lineNumber"></param>
        /// <param name="lineValue"></param>
        public ParsingException(string msg, int lineNumber, string lineValue)
            :this(msg, lineNumber, lineValue, null)
        {}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="lineNumber"></param>
        /// <param name="lineValue"></param>
        /// <param name="innerException"></param>
        public ParsingException(string msg, int lineNumber, string lineValue, Exception innerException)
            : base(
                string.Format(
                    "{0} while parsing line number {1} with value \'{2}\' - IniParser version: {3}", 
                    msg, lineNumber, lineValue, System.Reflection.Assembly.GetExecutingAssembly().GetName().Version),
                innerException) 
        { 
            LibVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            LineNumber = lineNumber;
            LineValue = lineValue;
        }
    }
}
