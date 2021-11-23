using System;
using SBRW.Ini.Parser.Model.Configuration;
using SBRW.Ini.Parser.Model.Formatting;

namespace SBRW.Ini.Parser.Model
{
    /// <summary>
    ///     Represents all data from an INI file exactly as the <see cref="IniData"/>
    ///     class, but searching for sections and keys names is done with
    ///     a case insensitive search.
    /// </summary>
    public class IniDataCaseInsensitive : IniData
    {
        /// <summary>
        ///     Initializes an empty IniData instance.
        /// </summary>
        public IniDataCaseInsensitive()
            : base (new SectionDataCollection(StringComparer.OrdinalIgnoreCase))
        {
            Global = new KeyDataCollection(StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        ///     Initializes a new IniData instance using a previous
        ///     <see cref="SectionDataCollection"/>.
        /// </summary>
        /// <param name="sdc">
        ///     <see cref="SectionDataCollection"/> object containing the
        ///     data with the sections of the file
        /// </param>
        public IniDataCaseInsensitive(SectionDataCollection sdc)
            : base (new SectionDataCollection(sdc, StringComparer.OrdinalIgnoreCase))
        {
            Global = new KeyDataCollection(StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Copies an instance of the <see cref="SBRW.Ini.Parser.Model.IniDataCaseInsensitive"/> class
        /// </summary>
        /// <param name="ori">Original </param>
        public IniDataCaseInsensitive(IniData ori)
            : this(new SectionDataCollection(ori.Sections, StringComparer.OrdinalIgnoreCase))
        {
            Global = (KeyDataCollection) ori.Global.Clone();
            Configuration = ori.Configuration.Clone();
        }
    }
    
} 