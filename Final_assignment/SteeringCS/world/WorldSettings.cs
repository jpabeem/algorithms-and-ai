using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.world
{
    public class WorldSettings
    {
        public Dictionary<string, bool> SettingsDictionary { get; private set; }

        public WorldSettings(Dictionary<string, bool> dictionary)
        {
            SettingsDictionary = dictionary;
        }

        /// <summary>
        /// Return the value of a given setting.
        /// </summary>
        /// <param name="settingName"></param>
        /// <returns></returns>
        public bool Get(string settingName)
        {
            if (SettingsDictionary.ContainsKey(settingName))
                return SettingsDictionary[settingName];
            else
                throw new ArgumentException("The given setting does not exist.");
        }

        /// <summary>
        /// Set the value of a given setting.
        /// </summary>
        /// <param name="settingName"></param>
        /// <param name="val"></param>
        public void Set(string settingName, bool val)
        {
            if (SettingsDictionary.ContainsKey(settingName))
                SettingsDictionary[settingName] = val;
            else
                throw new ArgumentException("The given setting does not exist.");
        }
    }
}
