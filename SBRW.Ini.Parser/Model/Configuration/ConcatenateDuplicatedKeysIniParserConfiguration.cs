using System;
using System.Text.RegularExpressions;
using SBRW.Ini.Parser.Parser;

namespace SBRW.Ini.Parser.Model.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public class ConcatenateDuplicatedKeysIniParserConfiguration : IniParserConfiguration
    {
        /// <summary>
        /// 
        /// </summary>
        public new bool AllowDuplicateKeys { get {return true; }}
        /// <summary>
        /// 
        /// </summary>
        public ConcatenateDuplicatedKeysIniParserConfiguration()
            :base()
        {
            this.ConcatenateSeparator = ";";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ori"></param>
        public ConcatenateDuplicatedKeysIniParserConfiguration(ConcatenateDuplicatedKeysIniParserConfiguration ori)
            :base(ori)
        {
            this.ConcatenateSeparator = ori.ConcatenateSeparator;
        }

        /// <summary>
        ///     Gets or sets the string used to concatenate duplicated keys.
        /// </summary>
        /// <value>
        ///     Defaults to ';'.
        /// </value>
        public string ConcatenateSeparator { get; set; }
    }

}