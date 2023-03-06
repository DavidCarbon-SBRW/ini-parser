using SBRW.Ini.Parser.Example.Deployed.Ini_;
using SBRW.Ini.Parser.Example.Deployed.Reference.Ini_;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBRW.Ini.Parser.Example.Deployed.File_
{
    /// <summary>
    /// Global Settings Save System
    /// </summary>
    /// <remarks>Used to set Values and Save them</remarks>
    public class Save_Settings
    {
        /// <summary>Settings Format Information In Live Memory</summary>
        public static Format_Settings Live_Data { get; set; } = new Format_Settings();
        ///<value>Settings File Information on Disk</value>s
        private static Ini_File SettingFile { get; set; }
        /// <summary>Creates all the NullSafe Values for Settings.ini</summary>
        public static void NullSafe()
        {
            SettingFile = new Ini_File(Ini_Location.Launcher_Settings);

            /* Pervent Removal of Login Info Before Main Screen (Temporary Boolean) */
            Save_Account.SaveLoginInformation = true;
            bool Display_Timer_Migration = false;

            /* Migrate old Key Entries */
            if (SettingFile.Key_Exists("Server"))
            {
                Save_Account.Live_Data.Saved_Server_Address = SettingFile.Key_Read("Server");
                SettingFile.Key_Delete("Server");
                Save_Account.Save();
            }

            if (SettingFile.Key_Exists("AccountEmail"))
            {
                Save_Account.Live_Data.User_Raw_Email = SettingFile.Key_Read("AccountEmail");
                SettingFile.Key_Delete("AccountEmail");
                Save_Account.Save();
            }

            if (SettingFile.Key_Exists("Password"))
            {
                Save_Account.Live_Data.User_Hashed_Password = SettingFile.Key_Read("Password");
                SettingFile.Key_Delete("Password");
                Save_Account.Save();
            }

            /* Reset This Value as its now Safe to Do So */
            Save_Account.SaveLoginInformation = false;

            if (SettingFile.Key_Exists("Firewall"))
            {
                Live_Data.Firewall_Game = Live_Data.Firewall_Launcher = SettingFile.Key_Read("Firewall");
                SettingFile.Key_Delete("Firewall");
            }

            if (SettingFile.Key_Exists("WindowsDefender"))
            {
                Live_Data.Defender_Game = Live_Data.Defender_Launcher = SettingFile.Key_Read("WindowsDefender");
                SettingFile.Key_Delete("WindowsDefender");
            }

            if (SettingFile.Key_Exists("LegacyTimer"))
            {
                Live_Data.Launcher_Display_Timer = SettingFile.Key_Read("LegacyTimer");
                SettingFile.Key_Delete("LegacyTimer");
                Display_Timer_Migration = true;
            }

            /* Check if any Entries are missing */

            if (!SettingFile.Key_Exists("InstallationDirectory"))
            {
                SettingFile.Key_Write("InstallationDirectory", Live_Data.Game_Path);
            }
            else
            {
                Live_Data.Game_Path = SettingFile.Key_Read("InstallationDirectory");
            }

            if (!SettingFile.Key_Exists("OldInstallationDirectory"))
            {
                SettingFile.Key_Write("OldInstallationDirectory", Live_Data.Game_Path_Old);
            }
            else
            {
                Live_Data.Game_Path_Old = SettingFile.Key_Read("OldInstallationDirectory");
            }

            if (!SettingFile.Key_Exists("GameArchivePath"))
            {
                SettingFile.Key_Write("GameArchivePath", Live_Data.Game_Archive_Location);
            }
            else if (File.Exists(SettingFile.Key_Read("GameArchivePath")))
            {
                Live_Data.Game_Archive_Location = SettingFile.Key_Read("GameArchivePath");
            }
            else
            {
                SettingFile.Key_Write("GameArchivePath", Live_Data.Game_Archive_Location);
            }

            if (!SettingFile.Key_Exists("CDN") || string.IsNullOrWhiteSpace(SettingFile.Key_Read("CDN")))
            {
                SettingFile.Key_Write("CDN", Live_Data.Launcher_CDN = "http://localhost");
            }
            else
            {
                if (SettingFile.Key_Read("CDN").EndsWith("/"))
                {
                    SettingFile.Key_Write("CDN", Live_Data.Launcher_CDN = SettingFile.Key_Read("CDN").TrimEnd('/'));
                }
                else
                {
                    Live_Data.Launcher_CDN = SettingFile.Key_Read("CDN");
                }
            }

            if (!SettingFile.Key_Exists("Language") || string.IsNullOrWhiteSpace(SettingFile.Key_Read("Language")))
            {
                SettingFile.Key_Write("Language", Live_Data.Launcher_Language = "EN");
            }
            else
            {
                Live_Data.Launcher_Language = SettingFile.Key_Read("Language");
            }

            if (!SettingFile.Key_Exists("DisableProxy") || string.IsNullOrWhiteSpace(SettingFile.Key_Read("DisableProxy")))
            {
                SettingFile.Key_Write("DisableProxy", Live_Data.Launcher_Proxy = "0");
            }
            else if ((SettingFile.Key_Read("DisableProxy") == "0") || (SettingFile.Key_Read("DisableProxy") == "1"))
            {
                Live_Data.Launcher_Proxy = SettingFile.Key_Read("DisableProxy");
            }
            else
            {
                SettingFile.Key_Write("DisableProxy", Live_Data.Launcher_Proxy = "0");
            }

            if (!SettingFile.Key_Exists("DisableRPC") || string.IsNullOrWhiteSpace(SettingFile.Key_Read("DisableRPC")))
            {
                SettingFile.Key_Write("DisableRPC", Live_Data.Launcher_Discord_Presence = "0");
            }
            else if ((SettingFile.Key_Read("DisableRPC") == "0") || (SettingFile.Key_Read("DisableRPC") == "1"))
            {
                Live_Data.Launcher_Discord_Presence = SettingFile.Key_Read("DisableRPC");
            }
            else
            {
                SettingFile.Key_Write("DisableRPC", Live_Data.Launcher_Discord_Presence = "0");
            }

            if (!SettingFile.Key_Exists("IgnoreUpdateVersion") || string.IsNullOrWhiteSpace(SettingFile.Key_Read("IgnoreUpdateVersion")))
            {
                SettingFile.Key_Write("IgnoreUpdateVersion", Live_Data.Update_Version_Skip);
            }
            else
            {
                Live_Data.Update_Version_Skip = SettingFile.Key_Read("IgnoreUpdateVersion");
            }

            if ((!SettingFile.Key_Exists("FilePermission") || string.IsNullOrWhiteSpace(SettingFile.Key_Read("FilePermission"))))
            {
                SettingFile.Key_Write("FilePermission", Live_Data.Write_Permissions = "Unknown");
            }
            else
            {
                Live_Data.Write_Permissions = SettingFile.Key_Read("FilePermission");
            }

            if (!SettingFile.Key_Exists("GameIntegrity") || string.IsNullOrWhiteSpace(SettingFile.Key_Read("FilePermission")))
            {
                SettingFile.Key_Write("GameIntegrity", Live_Data.Game_Integrity = "Unknown");
            }
            else
            {
                Live_Data.Game_Integrity = SettingFile.Key_Read("GameIntegrity");
            }

            if (!SettingFile.Key_Exists("ProxyPort"))
            {
                SettingFile.Key_Write("ProxyPort", Live_Data.Launcher_Proxy_Port);
            }
            else
            {
                Live_Data.Launcher_Proxy_Port = SettingFile.Key_Read("ProxyPort");
            }

            if (!SettingFile.Key_Exists("WebCallMethod") || string.IsNullOrWhiteSpace(SettingFile.Key_Read("WebCallMethod")))
            {
                SettingFile.Key_Write("WebCallMethod", Live_Data.Launcher_WebClient_Method = "WebClient");
            }
            else if (SettingFile.Key_Read("WebCallMethod") == "WebClient" || SettingFile.Key_Read("WebCallMethod") == "WebClientWithTimeout")
            {
                Live_Data.Launcher_WebClient_Method = SettingFile.Key_Read("WebCallMethod");
            }
            else
            {
                Live_Data.Launcher_WebClient_Method = "WebClient";
            }

            if (!SettingFile.Key_Exists("ThemeSupport") || string.IsNullOrWhiteSpace(SettingFile.Key_Read("ThemeSupport")))
            {
                SettingFile.Key_Write("ThemeSupport", Live_Data.Launcher_Theme_Support = "0");
            }
            else if ((SettingFile.Key_Read("ThemeSupport") == "0") || (SettingFile.Key_Read("ThemeSupport") == "1"))
            {
                Live_Data.Launcher_Theme_Support = SettingFile.Key_Read("ThemeSupport");
            }
            else
            {
                SettingFile.Key_Write("ThemeSupport", Live_Data.Launcher_Theme_Support = "0");
            }

            if (!SettingFile.Key_Exists("Insider") || string.IsNullOrWhiteSpace(SettingFile.Key_Read("Insider")))
            {
                SettingFile.Key_Write("Insider", Live_Data.Launcher_Insider = "0");
            }
            else if ((SettingFile.Key_Read_Int("Insider") >= 0) && (SettingFile.Key_Read_Int("Insider") <= 2))
            {
                Live_Data.Launcher_Insider = SettingFile.Key_Read("Insider");
                if (SettingFile.Key_Read_Int("Insider") == 1)
                {
                    Console.WriteLine("Insider Status: ".ToUpper() + "Opted Into the Beta Preview");
                }
                else if (SettingFile.Key_Read_Int("Insider") == 2)
                {
                    Console.WriteLine("Insider Status: ".ToUpper() + "Opted Into the Development Preview");
                }
            }
            else
            {
                SettingFile.Key_Write("Insider", Live_Data.Launcher_Insider = "0");
            }

            if ((!SettingFile.Key_Exists("DisplayTimer") || string.IsNullOrWhiteSpace(SettingFile.Key_Read("DisplayTimer"))) && !Display_Timer_Migration)
            {
                SettingFile.Key_Write("DisplayTimer", Live_Data.Launcher_Display_Timer = "0");
            }
            else if (Display_Timer_Migration ?
                ((Live_Data.Launcher_Display_Timer == "0") || (Live_Data.Launcher_Display_Timer == "1") || Live_Data.Launcher_Display_Timer == "2") :
                ((SettingFile.Key_Read_Int("DisplayTimer") >= 0) && (SettingFile.Key_Read_Int("DisplayTimer") <= 2)))
            {
                if (!Display_Timer_Migration)
                {
                    Live_Data.Launcher_Display_Timer = SettingFile.Key_Read("DisplayTimer");
                }
            }
            else
            {
                SettingFile.Key_Write("DisplayTimer", Live_Data.Launcher_Display_Timer = "0");
            }

            if (!SettingFile.Key_Exists("LzmaDownloader") || string.IsNullOrWhiteSpace(SettingFile.Key_Read("LzmaDownloader")))
            {
                SettingFile.Key_Write("LzmaDownloader", Live_Data.Launcher_LZMA_Downloader = "0");
            }
            else if ((SettingFile.Key_Read("LzmaDownloader") == "0") || (SettingFile.Key_Read("LzmaDownloader") == "1"))
            {
                Live_Data.Launcher_LZMA_Downloader = SettingFile.Key_Read("LzmaDownloader");
            }
            else
            {
                SettingFile.Key_Write("LzmaDownloader", Live_Data.Launcher_LZMA_Downloader = "0");
            }

            if (!SettingFile.Key_Exists("JSONFrequencyUpdateCache") || string.IsNullOrWhiteSpace(SettingFile.Key_Read("JSONFrequencyUpdateCache")))
            {
                SettingFile.Key_Write("JSONFrequencyUpdateCache", Live_Data.Launcher_JSON_Frequency_Update_Cache = "0");
            }
            else if ((SettingFile.Key_Read("JSONFrequencyUpdateCache") == "0") || (SettingFile.Key_Read("JSONFrequencyUpdateCache") == "1"))
            {
                Live_Data.Launcher_JSON_Frequency_Update_Cache = SettingFile.Key_Read("JSONFrequencyUpdateCache");
            }
            else
            {
                SettingFile.Key_Write("JSONFrequencyUpdateCache", Live_Data.Launcher_JSON_Frequency_Update_Cache = "0");
            }

            if (!SettingFile.Key_Exists("WebCallTimeOut") || string.IsNullOrWhiteSpace(SettingFile.Key_Read("WebCallTimeOut")))
            {
                SettingFile.Key_Write("WebCallTimeOut", Live_Data.Launcher_WebCall_TimeOut_Time = "0");
            }
            else if (int.TryParse(SettingFile.Key_Read("WebCallTimeOut"), out int Converted_String_Value) && Converted_String_Value > 0)
            {

            }
            else
            {
                SettingFile.Key_Write("WebCallTimeOut", Live_Data.Launcher_WebCall_TimeOut_Time = "0");
            }

            if (true)
            {
                if (!SettingFile.Key_Exists("FirewallLauncher") || string.IsNullOrWhiteSpace(SettingFile.Key_Read("FirewallLauncher")))
                {
                    SettingFile.Key_Write("FirewallLauncher", Live_Data.Firewall_Launcher = "Unknown");
                }
                else
                {
                    Live_Data.Firewall_Launcher = SettingFile.Key_Read("FirewallLauncher");
                }

                if (!SettingFile.Key_Exists("FirewallGame") || string.IsNullOrWhiteSpace(SettingFile.Key_Read("FirewallGame")))
                {
                    SettingFile.Key_Write("FirewallGame", Live_Data.Firewall_Game = "Unknown");
                }
                else
                {
                    Live_Data.Firewall_Game = SettingFile.Key_Read("FirewallGame");
                }
            }


            /* Key Entries to Convert into Boolens */

            Console.WriteLine("Custom Proxy Port:".ToUpper() + " -> " + (Live_Data.Launcher_Proxy_Port) + " has been Set");

            /* Key Entries to Remove (No Longer Needed) */

            if (SettingFile.Key_Exists("LauncherPosX"))
            {
                SettingFile.Key_Delete("LauncherPosX");
            }

            if (SettingFile.Key_Exists("LauncherPosY"))
            {
                SettingFile.Key_Delete("LauncherPosY");
            }

            if (SettingFile.Key_Exists("DisableVerifyHash"))
            {
                SettingFile.Key_Delete("DisableVerifyHash");
            }

            if (SettingFile.Key_Exists("TracksHigh"))
            {
                SettingFile.Key_Delete("TracksHigh");
            }

            if (SettingFile.Key_Exists("ModNetDisabled"))
            {
                SettingFile.Key_Delete("ModNetDisabled");
            }

            if (SettingFile.Key_Exists("ModNetZip"))
            {
                SettingFile.Key_Delete("ModNetZip");
            }

            if (SettingFile.Key_Exists("StreamingSupport"))
            {
                SettingFile.Key_Delete("StreamingSupport");
            }

            SettingFile = new Ini_File(Ini_Location.Launcher_Settings);
        }
        /// <summary>Saves all Current Values</summary>
        public static void Save()
        {
            SettingFile = new Ini_File(Ini_Location.Launcher_Settings);

            if (SettingFile.Key_Read("CDN") != Live_Data.Launcher_CDN)
            {
                if (Live_Data.Launcher_CDN.EndsWith("/"))
                {
                    SettingFile.Key_Write("CDN", Live_Data.Launcher_CDN.TrimEnd('/'));
                }
                else
                {
                    SettingFile.Key_Write("CDN", Live_Data.Launcher_CDN);
                }
            }

            if (SettingFile.Key_Read("Language") != Live_Data.Launcher_Language)
            {
                SettingFile.Key_Write("Language", Live_Data.Launcher_Language);
            }

            if (SettingFile.Key_Read("DisableProxy") != Live_Data.Launcher_Proxy)
            {
                SettingFile.Key_Write("DisableProxy", Live_Data.Launcher_Proxy);
            }

            if (SettingFile.Key_Read("DisableRPC") != Live_Data.Launcher_Discord_Presence)
            {
                SettingFile.Key_Write("DisableRPC", Live_Data.Launcher_Discord_Presence);
            }

            if (SettingFile.Key_Read("InstallationDirectory") != Live_Data.Game_Path)
            {
                SettingFile.Key_Write("InstallationDirectory", Live_Data.Game_Path);
            }

            if (SettingFile.Key_Read("OldInstallationDirectory") != Live_Data.Game_Path_Old)
            {
                SettingFile.Key_Write("OldInstallationDirectory", Live_Data.Game_Path_Old);
            }

            if (SettingFile.Key_Read("GameArchivePath") != Live_Data.Game_Archive_Location)
            {
                SettingFile.Key_Write("GameArchivePath", Live_Data.Game_Archive_Location);
            }

            if (SettingFile.Key_Read("IgnoreUpdateVersion") != Live_Data.Update_Version_Skip)
            {
                SettingFile.Key_Write("IgnoreUpdateVersion", Live_Data.Update_Version_Skip);
            }

            if (SettingFile.Key_Read("GameIntegrity") != Live_Data.Game_Integrity)
            {
                SettingFile.Key_Write("GameIntegrity", Live_Data.Game_Integrity);
            }

            if (SettingFile.Key_Read("WebCallMethod") != Live_Data.Launcher_WebClient_Method)
            {
                SettingFile.Key_Write("WebCallMethod", Live_Data.Launcher_WebClient_Method);
            }

            if (SettingFile.Key_Read("ThemeSupport") != Live_Data.Launcher_Theme_Support)
            {
                SettingFile.Key_Write("ThemeSupport", Live_Data.Launcher_Theme_Support);
            }

            if (SettingFile.Key_Read("Insider") != Live_Data.Launcher_Insider)
            {
                SettingFile.Key_Write("Insider", Live_Data.Launcher_Insider);

                if ((SettingFile.Key_Read_Int("Insider") >= 0) && (SettingFile.Key_Read_Int("Insider") <= 2))
                {
                    if (SettingFile.Key_Read_Int("Insider") == 1)
                    {
                        Console.WriteLine("Insider Status: ".ToUpper() + "Opted Into the Beta Preview");
                    }
                    else if (SettingFile.Key_Read_Int("Insider") == 2)
                    {
                        Console.WriteLine("Insider Status: ".ToUpper() + "Opted Into the Development Preview");
                    }
                }
            }

            if (SettingFile.Key_Read("DisplayTimer") != Live_Data.Launcher_Display_Timer)
            {
                SettingFile.Key_Write("DisplayTimer", Live_Data.Launcher_Display_Timer);
            }

            if (SettingFile.Key_Read("LzmaDownloader") != Live_Data.Launcher_LZMA_Downloader)
            {
                SettingFile.Key_Write("LzmaDownloader", Live_Data.Launcher_LZMA_Downloader);
            }

            if (SettingFile.Key_Read("JSONFrequencyUpdateCache") != Live_Data.Launcher_JSON_Frequency_Update_Cache)
            {
                SettingFile.Key_Write("JSONFrequencyUpdateCache", Live_Data.Launcher_JSON_Frequency_Update_Cache);
            }

            if (SettingFile.Key_Read("WebCallTimeOut") != Live_Data.Launcher_WebCall_TimeOut_Time)
            {
                SettingFile.Key_Write("WebCallTimeOut", Live_Data.Launcher_WebCall_TimeOut_Time);
            }

            if (true)
            {
                if (SettingFile.Key_Read("FilePermission") != Live_Data.Write_Permissions)
                {
                    SettingFile.Key_Write("FilePermission", Live_Data.Write_Permissions);
                }

                if (SettingFile.Key_Read("FirewallLauncher") != Live_Data.Firewall_Launcher)
                {
                    SettingFile.Key_Write("FirewallLauncher", Live_Data.Firewall_Launcher);
                }

                if (SettingFile.Key_Read("FirewallGame") != Live_Data.Firewall_Game)
                {
                    SettingFile.Key_Write("FirewallGame", Live_Data.Firewall_Game);
                }
            }

            if (SettingFile.Key_Read("LauncherEnvironment") != Live_Data.Launcher_RunTime_Environment)
            {
                SettingFile.Key_Write("LauncherEnvironment", Live_Data.Launcher_RunTime_Environment);
            }

            SettingFile = new Ini_File(Ini_Location.Launcher_Settings);
        }
    }
}