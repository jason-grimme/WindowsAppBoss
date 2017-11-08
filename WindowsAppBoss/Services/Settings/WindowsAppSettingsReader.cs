using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAppBoss.Services.Settings.Registry;

namespace WindowsAppBoss.Services.Settings
{
    public class WindowsAppSettingsReader
    {

        public WindowsAppSettingsReader(string pathToSettingsFile)
        {
            _pathToSettingsFile = pathToSettingsFile;
        }

        /// <summary>
        /// Extracts the settings and return as formatted text
        /// </summary>
        /// <returns></returns>
        public Task<string> GetSettingAsTextAsync()
        {
            return Task.Factory.StartNew<string>( () =>
            {
                return GetSettingsText(_pathToSettingsFile);
            });
        }

        #region private stuff
        private static string GetSettingsText(string pathToSettingsFile)
        {
            string text = string.Empty;
            try
            {
                if (File.Exists(pathToSettingsFile))
                {
                    var hive = new RegistryHive(pathToSettingsFile);
                    if (hive != null && hive.RootKey != null)
                    {
                        text = WriteNodeKey(hive.RootKey, 1);
                    }
                }
                else
                {
                    text += (pathToSettingsFile + " does not exist");
                }
            }
            catch (Exception ex)
            {
                text += ("Exception: " + ex.Message);
            }
            return text;
        }


   
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodeKey"></param>
        /// <param name="indentionLevel"></param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        private static string WriteNodeKey(NodeKey nodeKey, int indentionLevel)
        {
            var builder = new StringBuilder();
            try
            {
                if (nodeKey != null)
                {
                    builder.AppendLine(Indent(indentionLevel) + "Key: " + nodeKey.Name);
                    indentionLevel++;
                    if (nodeKey.ChildNodes != null && nodeKey.ChildNodes.Any())
                    {
                        foreach (var child in nodeKey.ChildNodes)
                        {
                            string text = WriteNodeKey(child, indentionLevel);
                            builder.AppendLine(text);
                        }
                    }
                    if (nodeKey.ChildValues != null && nodeKey.ChildValues.Any())
                    {
                        foreach (var value in nodeKey.ChildValues)
                        {
                            string dataName = "[Undetermined]";
                            string dataValue = "[Undetermined]";
                            try
                            {
                                dataName = value.Name;
                                dataValue = GetReadableValueOfBytes(value.ValueType, value.Data);
                                builder.AppendFormat(Indent(indentionLevel) + "Item:    {0}  :  {1} \r\n", dataName, Indent(dataValue, indentionLevel));
                            }
                            catch (Exception ex)
                            {
                                builder.AppendLine("<Exception processing key  " + dataName + ", Exception: " + ex.Message + ">");
                            }
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                builder.AppendLine("Exception: " + ex.Message);
            }
            return builder.ToString();

        }


        private static string GetReadableValueOfBytes(int dataType, byte[] dataValueBytes)
        {
           
            string contents = "[Undetermined]";
            byte[] dataMinusTimeStamp = (dataValueBytes.Length >= 8)
                ? dataValueBytes.Take(dataValueBytes.Length - 8).ToArray()
                : dataValueBytes;

            if (dataType == ValueTypes.String) 
            {
                contents = System.Text.Encoding.Unicode.GetString(dataMinusTimeStamp).TrimEnd('\0');
            }
            else if (dataType == ValueTypes.Boolean)
            {
                contents = BitConverter.ToBoolean(dataMinusTimeStamp, 0).ToString();
            }
            else if (dataType == ValueTypes.Int32)
            {
                contents = BitConverter.ToInt32(dataMinusTimeStamp, 0).ToString();
            }
            else if (dataType == ValueTypes.Int64) // Long
            {
                contents = BitConverter.ToInt64(dataMinusTimeStamp, 0).ToString();
            }
            else if (dataType == ValueTypes.Int16)
            {
                contents = BitConverter.ToInt16(dataMinusTimeStamp, 0).ToString();
            }
            else if (dataType == ValueTypes.Char)
            {
                contents = BitConverter.ToChar(dataMinusTimeStamp, 0).ToString();
            }
            else if (dataType == ValueTypes.Double)
            {
                contents = BitConverter.ToDouble(dataMinusTimeStamp, 0).ToString();
            }
            else if (dataType == ValueTypes.Single)
            {
                contents = BitConverter.ToSingle(dataMinusTimeStamp, 0).ToString();
            }
            else if (dataType == ValueTypes.Uint16)
            {
                contents = BitConverter.ToUInt16(dataMinusTimeStamp, 0).ToString();
            }
            else if (dataType == ValueTypes.Uint32)
            {
                contents = BitConverter.ToUInt32(dataMinusTimeStamp, 0).ToString();
            }
            else if (dataType == ValueTypes.Uint64)
            {
                contents = BitConverter.ToUInt64(dataMinusTimeStamp, 0).ToString();
            }
            else if (dataType == ValueTypes.ArrayInts)
            {
                var size = sizeof(Int32);
                int[] collection = new int[dataMinusTimeStamp.Length / size];
                for (int i = 0; i < dataMinusTimeStamp.Length; i += size)
                {
                    collection[i / size] = BitConverter.ToInt32(dataMinusTimeStamp, i);
                }
                contents = "int32 [] { " + String.Join(", ", collection) + " }";
            }
            else if (dataType == ValueTypes.ArrayOfBool)
            {
                var size = sizeof(bool);
                bool[] collection = new bool[dataMinusTimeStamp.Length / size];
                for (int i = 0; i < dataMinusTimeStamp.Length; i += size)
                {
                    collection[i / size] = BitConverter.ToBoolean(dataMinusTimeStamp, i);
                }
                contents = "bool [] { " + String.Join(", ", collection) + " }";
            }
            else if (dataType == ValueTypes.ArrayOfStrings)
            {
                // This was a tough one
                // nn == length, pp == padding, xx == conent
                //                             08         |one                    |08         |two                    |12         |three                             |      
                //                             nn nn nn nn xx xx xx xx xx xx xx xx nn nn nn nn xx xx xx xx xx xx xx xx nn nn nn nn xx xx xx xx xx xx xx xx xx xx xx xx
                // {"one", "two", "three" } == 08 00 00 00 6F 00 6E 00 65 00 00 00 08 00 00 00 74 00 77 00 6F 00 00 00 0C 00 00 00 74 00 68 00 72 00 65 00 65 00 00 00
                //string debugBytes = BitConverter.ToString(dataMinusTimeStamp);

                int index = 0;
                List<string> chunks = new List<string>();
                while (index < dataMinusTimeStamp.Length)
                {
                    var lengthBytes = dataMinusTimeStamp.Skip(index).Take(sizeof(Int32)).ToArray();
                    int chunkLength = BitConverter.ToInt32(lengthBytes, 0);
                    var chunkBytes = dataMinusTimeStamp.Skip(index + sizeof(Int32)).Take(chunkLength);
                    chunks.Add(System.Text.Encoding.Unicode.GetString(chunkBytes.ToArray()).TrimEnd('\0'));
                    index += ( sizeof(Int32) + chunkLength);
                }
                contents = "string [] { \"" + String.Join("\", \"", chunks) + "\" }";
            }
            else
            {
                contents = String.Format("Unknown data type ({0}).  Data: {1}", dataType, BitConverter.ToString(dataMinusTimeStamp));
            }

            return contents;
        }

        private static string BytesToString(byte[] bytes)
        {
            return System.Text.Encoding.Unicode.GetString(bytes);
        }

        private static string Indent(int level)
        {
            return new String('\t', level);
        }

        private static string Indent(string text, int level)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                text = text.Replace(Environment.NewLine, (Environment.NewLine + Indent(level)));
            }
            return text;
        }

#endregion private stuff

        #region public static 
        /// <summary>
        /// Provides a likely path to the settings.dat file for a given app
        /// </summary>
        /// <param name="packageDataDirectory"></param>
        /// <returns></returns>
        public static string GetSettingsPath(string packageDataDirectory)
        {
            return Path.Combine(packageDataDirectory, Constants.DirectoryNames.Settings, Constants.FileNames.Settings);
        }
        #endregion public static

        private readonly string _pathToSettingsFile;

        private static class ValueTypes
        {
            public const int Byte = 100000001;
            public const int Int16 = 100000002;
            public const int Int32 = 100000004;
            public const int Int64 = 100000006; // Long is int64
            public const int Single = 100000008;
            public const int Uint16 = 100000003;
            public const int Uint32 = 100000005;
            public const int Uint64 = 100000007;
            public const int Double = 100000009;
            public const int Char = 100000010;
            public const int Boolean = 100000011;
            public const int String = 100000012;
            // Array is original + 8 (I think)
            public const int ArrayOfBool = 100000019;
            public const int ArrayInts = 100000023;
            public const int ArrayOfStrings = 100000031;

        }
    }
}
