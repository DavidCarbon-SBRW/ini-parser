using System;
using System.Collections.Generic;
using SBRW.Ini.Parser.Exceptions;
using SBRW.Ini.Parser.Model;
using SBRW.Ini.Parser.Model.Configuration;

namespace SBRW.Ini.Parser.Parser
{
    /// <summary>
    /// 
    /// </summary>
    public class ConcatenateDuplicatedKeysIniDataParser : IniDataParser
    {
        /// <summary>
        /// 
        /// </summary>
        public new ConcatenateDuplicatedKeysIniParserConfiguration Configuration
        {
            get
            {
                return (ConcatenateDuplicatedKeysIniParserConfiguration)base.Configuration;
            }
            set
            {
                base.Configuration = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public ConcatenateDuplicatedKeysIniDataParser()
            :this(new ConcatenateDuplicatedKeysIniParserConfiguration())
        {}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parserConfiguration"></param>
        public ConcatenateDuplicatedKeysIniDataParser(ConcatenateDuplicatedKeysIniParserConfiguration parserConfiguration)
            :base(parserConfiguration)
        {}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="keyDataCollection"></param>
        /// <param name="sectionName"></param>
        protected override void HandleDuplicatedKeyInCollection(string key, string value, KeyDataCollection keyDataCollection, string sectionName)
        {
            keyDataCollection[key] += Configuration.ConcatenateSeparator + value;
        }
    }

}
