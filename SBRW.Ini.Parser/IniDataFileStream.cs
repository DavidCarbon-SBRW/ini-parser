using System;
using System.IO;

namespace SBRW.Ini.Parser
{

    /// <summary>
    ///     Represents an INI data parser for streams.
    /// </summary>
    public class IniDataFileStream
    {
        /// <summary>
        ///     This instance will handle ini data parsing and writing
        /// </summary>
        public IniDataParser Parser { get; protected set; }

        /// <summary>
        ///     Ctor
        /// </summary>
        public IniDataFileStream() : this(new IniDataParser()) 
        {

        }

        /// <summary>
        ///     Ctor
        /// </summary>
        /// <param name="parser"></param>
        public IniDataFileStream(IniDataParser parser)
        {
            Parser = parser;
        }
        #region Public Methods

        /// <summary>
        ///     Reads data in INI format from a stream.
        /// </summary>
        /// <param name="reader">Reader stream.</param>
        /// <returns>
        ///     And <see cref="IniData"/> instance with the readed ini data parsed.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if <paramref name="reader"/> is <c>null</c>.
        /// </exception>
        public IniData ReadData(StreamReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }
            else
            {
                return Parser.Parse(reader.ReadToEnd());
            }
        }

        /// <summary>
        ///     Writes the ini data to a stream.
        /// </summary>
        /// <param name="writer">A write stream where the ini data will be stored</param>
        /// <param name="iniData">An <see cref="IniData"/> instance.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if <paramref name="writer"/> is <c>null</c>.
        /// </exception>
        public void WriteData(StreamWriter writer, IniData iniData)
        {
            if (iniData == null)
            {
                throw new ArgumentNullException("iniData");
            }
            else if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }
            else
            {
                writer.Write(iniData);
            }
        }

        #endregion
    }
}
