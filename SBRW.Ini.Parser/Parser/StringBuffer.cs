using System.Collections.Generic;
using System.IO;

namespace SBRW.Ini.Parser.Parser
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class StringBuffer
    {
        /// <summary>
        /// 
        /// </summary>
        public struct Range
        {
            int _start, _size;
            /// <summary>
            /// 
            /// </summary>
            public int start
            {
                get
                {
                    return _start;
                }

                set
                {
                    _start = value < 0 ? 0 : value;
                }
            }
            /// <summary>
            /// 
            /// </summary>
            public int size
            {
                get
                {
                    return _size;
                }

                set
                {
                    _size = value < 0 ? 0 : value;
                }
            }
            /// <summary>
            /// 
            /// </summary>
            public int end
            {
                get
                {
                    return _size <= 0 ? 0 : _start + (_size - 1);
                }
            }
            /// <summary>
            /// 
            /// </summary>
            public bool IsEmpty { get { return _size == 0; } }
            /// <summary>
            /// 
            /// </summary>
            public void Reset()
            {
                start = 0;
                size = 0;
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="start"></param>
            /// <param name="size"></param>
            /// <returns></returns>
            public static Range FromIndexWithSize(int start, int size)
            {
                if (start < 0 || size <= 0) return new Range();

                return new Range { start = start, size = size };
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="start"></param>
            /// <param name="end"></param>
            /// <returns></returns>
            public static Range WithIndexes(int start, int end)
            {
                if (start < 0 || end < 0 || end - start < 0)

                {
                    return new Range();
                }

                return new Range { start = start, size = end - start + 1 };
            }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return string.Format("[start:{0}, end:{1} size: {2}]",
                                     start,
                                     end,
                                     size);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        readonly static int DefaultCapacity = 256;
        /// <summary>
        /// 
        /// </summary>
        public StringBuffer()
            :this(StringBuffer.DefaultCapacity 
                 )
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="capacity"></param>
        public StringBuffer(int capacity)
        {
            _buffer = new List<char>(capacity);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="bufferIndexes"></param>
        internal StringBuffer(List<char> buffer, Range bufferIndexes)
        {
            _buffer = buffer;
            _bufferIndexes = bufferIndexes;
        }
        /// <summary>
        /// 
        /// </summary>
        public int Count { get { return _bufferIndexes.size; } }
        /// <summary>
        /// 
        /// </summary>
        public bool IsEmpty
        {
            get { return _bufferIndexes.IsEmpty; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsWhitespace
        {
            get
            {
                int startIdx = _bufferIndexes.start;
                while (startIdx <= _bufferIndexes.end
                    && char.IsWhiteSpace(_buffer[startIdx]))
                {
                    startIdx++;
                }

                return startIdx > _bufferIndexes.end;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        public char this[int idx]
        {
            get
            {
                return _buffer[idx + _bufferIndexes.start];
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public StringBuffer DiscardChanges()
        {
            _bufferIndexes = Range.FromIndexWithSize(0, _buffer.Count);
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="subString"></param>
        /// <param name="startingIndex"></param>
        /// <returns></returns>
        public Range FindSubstring(string subString, int startingIndex = 0)
        {
            int subStringLength = subString.Length;

            if (subStringLength <= 0 || Count < subStringLength)
            {
                return new Range();
            }

            startingIndex += _bufferIndexes.start;

            // Search the first char of the substring
            for (int firstCharIdx = startingIndex; firstCharIdx <= _bufferIndexes.end; ++firstCharIdx)
            {
                if (_buffer[firstCharIdx] != subString[0])
                {
                    continue;
                }

                // Fail now if the substring can't fit given the size of the
                // buffer and the search start index
                if (firstCharIdx + subStringLength - 1 > _bufferIndexes.end)
                {
                    return new Range();
                }

                bool isSubstringMismatch = false;
                // Check if the substring matches starting at the index
                for (int currentIdx = 0; currentIdx < subStringLength; ++currentIdx)
                {
                    if (_buffer[firstCharIdx + currentIdx] != subString[currentIdx])
                    {
                        isSubstringMismatch = true;
                        break;
                    }
                }

                if (isSubstringMismatch)
                {
                    continue;
                }

                 return Range.FromIndexWithSize(firstCharIdx - _bufferIndexes.start, subStringLength);
            }

            return new Range();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool ReadLine()
        {
            if (_dataSource == null) return false;

            _buffer.Clear();
            int c = _dataSource.Read();

            // Read until new line ('\n') or EOF (-1)
            while (c != '\n' && c != -1)
            {
                if (c != '\r')
                {
                    _buffer.Add((char)c);
                }

                c = _dataSource.Read();
            }

            _bufferIndexes = Range.FromIndexWithSize(0, _buffer.Count);

            return _buffer.Count > 0 || c != -1;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataSource"></param>
        public void Reset(TextReader dataSource)
        {
            _dataSource = dataSource;
            _bufferIndexes.Reset();
            _buffer.Clear();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="range"></param>
        public void Resize(Range range)
        {
            Resize(range.start, range.size);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="newSize"></param>
        public void Resize(int newSize)
        {
            Resize(0, newSize);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="startIdx"></param>
        /// <param name="size"></param>
        public void Resize(int startIdx, int size)
        { 
            if (startIdx < 0 || size < 0) return;

            var internalStartIdx = _bufferIndexes.start + startIdx;
            var internalEndIdx = internalStartIdx + size - 1;

            if (internalEndIdx > _bufferIndexes.end) return;
            
            _bufferIndexes.start = internalStartIdx;
            _bufferIndexes.size = size;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="startIdx"></param>
        /// <param name="endIdx"></param>
        public void ResizeBetweenIndexes(int startIdx, int endIdx)
        {
            Resize(startIdx, endIdx - startIdx + 1);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public StringBuffer Substring(Range range)
        {
            var copy = SwallowCopy();
            copy.Resize(range);
            return copy;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public StringBuffer SwallowCopy()
        {
            return new StringBuffer(_buffer, _bufferIndexes);
        }
        /// <summary>
        /// 
        /// </summary>
        public void TrimStart()
        {
            if (IsEmpty) return;

            int startIdx = _bufferIndexes.start;
            while (startIdx <= _bufferIndexes.end
                && char.IsWhiteSpace(_buffer[startIdx]))
            {
                startIdx++;
            }

            // We need to make a copy of this value because _bufferIndexes.end
            // is a computed property, so it will change if we modify
            // _bufferIndexes.start or _bufferIndexes.size
            int endIdx = _bufferIndexes.end;

            _bufferIndexes.start = startIdx;
            _bufferIndexes.size = endIdx - startIdx + 1;
        }
        /// <summary>
        /// 
        /// </summary>
        public void TrimEnd()
        {
            if (IsEmpty) return;

            int endIdx = _bufferIndexes.end;

            while (endIdx >= _bufferIndexes.start
                && char.IsWhiteSpace(_buffer[endIdx]))
            {
                endIdx--;
            }

            _bufferIndexes.size = endIdx - _bufferIndexes.start + 1;
        }
        /// <summary>
        /// 
        /// </summary>
        public void Trim()
        {
            TrimEnd();
            TrimStart();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool StartsWith(string str)
        {
            if (string.IsNullOrEmpty(str)) return false;
            if (IsEmpty) return false;

            int strIdx = 0;
            int bufferIdx = _bufferIndexes.start;

            for (; strIdx < str.Length; ++strIdx, ++bufferIdx)
            {
                if (str[strIdx] != _buffer[bufferIdx]) return false;
            }

            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return new string(_buffer.ToArray(),
                               _bufferIndexes.start,
                               _bufferIndexes.size);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public string ToString(Range range)
        {
            if (range.IsEmpty
             || range.start < 0
             || range.size > _bufferIndexes.size
             || range.start + _bufferIndexes.start > _bufferIndexes.end)
            {
                return string.Empty;
            }

            return new string(_buffer.ToArray(),
                              _bufferIndexes.start + range.start,
                              range.size);
        }

        TextReader _dataSource;
        List<char> _buffer;
        Range _bufferIndexes;
    }
}
