using System;

namespace SBRW.Ini.Parser.Exceptions
{
    /// <summary>
    /// Represents an error occurred while parsing data 
    /// </summary>
    public class ParsingException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        public Version LibVersion { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public uint LineNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string LineContents { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="lineNumber"></param>

        public ParsingException(string msg, uint lineNumber)
            :this(msg, lineNumber, string.Empty, null)
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
        /// <param name="lineContents"></param>
        public ParsingException(string msg, uint lineNumber, string lineContents)
            :this(msg, lineNumber, lineContents, null)
        {}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="lineNumber"></param>
        /// <param name="lineContents"></param>
        /// <param name="innerException"></param>
        public ParsingException(string msg, uint lineNumber, string lineContents, Exception innerException)
            : base(
                $"{msg} while parsing line number {lineNumber} with value \'{lineContents}\'", 
                innerException) 
        { 
            LibVersion = GetAssemblyVersion();
            LineNumber = lineNumber;
            LineContents = lineContents;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Version GetAssemblyVersion()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
        }
    }
}
