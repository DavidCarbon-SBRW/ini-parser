using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBRW.Ini.Parser.Example.Deployed.Ini_
{
    /// <summary>
    /// Ini Location and Name Class
    /// </summary>
    public class Ini_Location
    {
        /// <summary>
        /// Name of the Settings File (Ini)
        /// </summary>
        public static string Name_Settings_Ini { get; set; } = "Settings.ini";
        /// <summary>
        /// Name of the Account File (Ini)
        /// </summary>
        public static string Name_Account_Ini { get; set; } = "Account.ini";
        /// <summary>
        /// Launcher Settings (Ini) Full File Path
        /// </summary>
        public static string Launcher_Settings
        {
            get
            {
                try
                {
                    return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Name_Settings_Ini);
                }
                catch
                {
                    return Name_Settings_Ini;
                }
            }
        }
        /// <summary>
        /// Roaming App Data Folder Location
        /// </summary>
        public static string RoamingAppDataFolder
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            }
        }
        /// <summary>
        /// Roaming App Data Launcher Folder Location
        /// </summary>
        public static string RoamingAppDataFolder_Launcher
        {
            get
            {
                return Path.Combine(RoamingAppDataFolder, "Soapbox Race World", "Launcher");
            }
        }
        /// <summary>
        /// Launcher Account (Ini) Full File Path
        /// </summary>
        public static string Launcher_Account
        {
            get
            {
                return Path.Combine(RoamingAppDataFolder_Launcher, Name_Account_Ini);
            }
        }
    }
}
