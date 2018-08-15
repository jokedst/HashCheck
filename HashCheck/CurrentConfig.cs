﻿using Microsoft.Win32;

namespace HashCheck
{
    /// <summary>
    /// Handle config settings, currently using the windows registry
    /// </summary>
    public class CurrentConfig
    {
        private const string RegistryKeyRoot = "SOFTWARE\\jokedst\\HashCheck";

        private const bool DefaultMD5 = true;
        private const bool DefaultSHA1 = true;
        private const bool DefaultSHA256 = true;
        private const bool DefaultSHA384 = true;
        private const bool DefaultSHA512 = true;

        private static bool GetBoolKey(string keyName, bool defaultValue)
        {
            var key = Registry.CurrentUser.OpenSubKey(RegistryKeyRoot);
            if (key == null) return defaultValue;
            var val = key.GetValue(keyName, defaultValue);
            if (val is bool b)
                return b;
            return bool.Parse((string) val);
        }

        private static void SetBoolKey(string keyName, bool value)
        {
            Registry.CurrentUser.CreateSubKey(RegistryKeyRoot)?.SetValue(keyName, value);
        }

        /// <summary>
        /// Where we look for customer directories with newly scanned images
        /// </summary>
        public static bool UseMD5
        {
            get => GetBoolKey("UseMD5", DefaultMD5);
            set => SetBoolKey("UseMD5", value);
        }

        /// <summary>
        /// Where we look for customer directories with newly scanned images
        /// </summary>
        public static bool UseSHA1
        {
            get => GetBoolKey("UseSHA1", DefaultSHA1);
            set => SetBoolKey("UseSHA1", value);
        }

        /// <summary>
        /// Where we look for customer directories with newly scanned images
        /// </summary>
        public static bool UseSHA256
        {
            get => GetBoolKey("UseSHA256", DefaultSHA256);
            set => SetBoolKey("UseSHA256", value);
        }

        /// <summary>
        /// Where we look for customer directories with newly scanned images
        /// </summary>
        public static bool UseSHA384
        {
            get => GetBoolKey("UseSHA384", DefaultSHA384);
            set => SetBoolKey("UseSHA384", value);
        }

        /// <summary>
        /// Where we look for customer directories with newly scanned images
        /// </summary>
        public static bool UseSHA512
        {
            get => GetBoolKey("UseSHA512", DefaultSHA512);
            set => SetBoolKey("UseSHA512", value);
        }
    }
}