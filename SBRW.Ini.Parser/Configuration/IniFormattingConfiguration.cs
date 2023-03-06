using SBRW.Ini.Parser.Model;
using System;
namespace SBRW.Ini.Parser.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public class IniFormattingConfiguration : IDeepCloneable<IniFormattingConfiguration>
    {
        /// <summary>
        /// 
        /// </summary>
        public enum ENewLine
        {
            /// <summary>
            /// 
            /// </summary>
            Windows,
            /// <summary>
            /// 
            /// </summary>
            Unix_Mac
        }
        /// <summary>
        /// 
        /// </summary>
        public IniFormattingConfiguration()
        {
            NewLineType = Environment.NewLine == "\r\n" ? ENewLine.Windows : ENewLine.Unix_Mac;
            NumSpacesBetweenAssigmentAndValue = 1;
            NumSpacesBetweenKeyAndAssigment = 1;
        }

        /// <summary>
        ///     Gets or sets the string to use as new line string when formating an IniData structure using a
        ///     IIniDataFormatter. Parsing an ini-file accepts any new line character (Unix/windows)
        /// </summary>
        /// <remarks>
        ///     This allows to write a file with unix new line characters on windows (and vice versa)
        /// </remarks>
        /// <value>Defaults to value Environment.NewLine</value>
        public string NewLineString
        {
            get
            {
                switch (NewLineType)
                {
                    case ENewLine.Unix_Mac: return "\n";
                    case ENewLine.Windows: return "\r\n";
                    default: return "\n";
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public ENewLine NewLineType { get; set; }

        /// <summary>
        ///     In a property sets the number of spaces between the end of the key  
        ///     and the beginning of the assignment string.
        ///     0 is a valid value.
        /// </summary>
        /// <remarks>
        ///     Defaults to 1 space
        /// </remarks>
        public uint NumSpacesBetweenKeyAndAssigment
        {
            set
            {
                _numSpacesBetweenKeyAndAssigment = value;
                SpacesBetweenKeyAndAssigment = new string(' ', (int)value);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SpacesBetweenKeyAndAssigment { get; private set; }
        /// <summary>
        ///     In a property sets the number of spaces between the end of 
        ///     the assignment string and the beginning of the value.
        ///     0 is a valid value.
        /// </summary>
        /// <remarks>
        ///     Defaults to 1 space
        /// </remarks>
        public uint NumSpacesBetweenAssigmentAndValue
        {
            set
            {
                _numSpacesBetweenAssigmentAndValue = value;
                SpacesBetweenAssigmentAndValue = new string(' ', (int)value);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SpacesBetweenAssigmentAndValue { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public bool NewLineBeforeSection { get; set; } = false;
        /// <summary>
        /// 
        /// </summary>
        public bool NewLineAfterSection { get; set; } = false;
        /// <summary>
        /// 
        /// </summary>
        public bool NewLineAfterProperty { get; set; } = false;
        /// <summary>
        /// 
        /// </summary>
        public bool NewLineBeforeProperty { get; set; } = false;

        
        #region IDeepCloneable<T> Members
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IniFormattingConfiguration DeepClone()
        {
            return MemberwiseClone() as IniFormattingConfiguration;
        }

        #endregion

        #region Fields
        private uint _numSpacesBetweenKeyAndAssigment;
        private uint _numSpacesBetweenAssigmentAndValue;
        #endregion
    }

}
