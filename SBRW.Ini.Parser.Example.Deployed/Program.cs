using System;
using System.IO;
using System.Text;
using SBRW.Ini.Parser;
using SBRW.Ini.Parser.Example.Deployed.File_;
using SBRW.Ini.Parser.Example.Deployed.Reference.Ini_;

namespace SBRW.Ini.Parser.Example.Deployed
{
    public class MainProgram
    {
        public static string testIniFile { get; set; }
        public static void Main()
        {
            Save_Settings.NullSafe();
            Save_Account.NullSafe();

            Console.WriteLine("Game_Path (From File): " + Save_Settings.Live_Data.Game_Path);

            Save_Settings.Live_Data.Game_Path = "C:\\Some\\FÖlder";
            Save_Settings.Save();

            Console.WriteLine("Settings Saved");

            Save_Settings.Live_Data = new Format_Settings();
            Console.WriteLine("Settings Cached Data Cleared");

            Save_Settings.NullSafe();

            Console.WriteLine("Game_Path: " + Save_Settings.Live_Data.Game_Path);
            Console.WriteLine("Complete");
        }
    }
}
