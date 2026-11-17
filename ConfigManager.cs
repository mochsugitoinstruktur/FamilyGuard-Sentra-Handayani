using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace FamilyGuard
{
    public class AppConfig
    {
        public bool IsProtectionActive { get; set; }
        public int DnsProfileIndex { get; set; }
        public bool EnableSafeSearch { get; set; }
        public bool EnableDomainBlock { get; set; }
        public List<string> CustomDomains { get; set; }
        public string PinHash { get; set; }
        public string PinSalt { get; set; }

        public AppConfig()
        {
            IsProtectionActive = false;
            DnsProfileIndex = 0; // CleanBrowsing Family
            EnableSafeSearch = true;
            EnableDomainBlock = true;
            CustomDomains = new List<string>();
            PinHash = "";
            PinSalt = "";
        }
    }

    public static class ConfigManager
    {
        private static string ConfigPath
        {
            get
            {
                string appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                string dir = Path.Combine(appData, "FamilyGuard");
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                return Path.Combine(dir, "settings.dat");
            }
        }

        public static AppConfig Load()
        {
            AppConfig config = new AppConfig();
            try
            {
                if (File.Exists(ConfigPath))
                {
                    string[] lines = File.ReadAllLines(ConfigPath, Encoding.UTF8);
                    bool inCustomDomains = false;

                    foreach (string rawLine in lines)
                    {
                        string line = rawLine.Trim();
                        if (string.IsNullOrEmpty(line) || line.StartsWith("#")) continue;

                        if (line == "[CUSTOM_DOMAINS]")
                        {
                            inCustomDomains = true;
                            continue;
                        }

                        if (inCustomDomains)
                        {
                            if (!string.IsNullOrEmpty(line) && !config.CustomDomains.Contains(line))
                            {
                                config.CustomDomains.Add(line);
                            }
                            continue;
                        }

                        int eq = line.IndexOf('=');
                        if (eq > 0)
                        {
                            string key = line.Substring(0, eq).Trim();
                            string val = line.Substring(eq + 1).Trim();

                            if (key.Equals("IsProtectionActive", StringComparison.OrdinalIgnoreCase))
                                config.IsProtectionActive = (val.ToLower() == "true");
                            else if (key.Equals("DnsProfileIndex", StringComparison.OrdinalIgnoreCase))
                            {
                                int idx;
                                if (int.TryParse(val, out idx)) config.DnsProfileIndex = idx;
                            }
                            else if (key.Equals("EnableSafeSearch", StringComparison.OrdinalIgnoreCase))
                                config.EnableSafeSearch = (val.ToLower() == "true");
                            else if (key.Equals("EnableDomainBlock", StringComparison.OrdinalIgnoreCase))
                                config.EnableDomainBlock = (val.ToLower() == "true");
                            else if (key.Equals("PinHash", StringComparison.OrdinalIgnoreCase))
                                config.PinHash = val;
                            else if (key.Equals("PinSalt", StringComparison.OrdinalIgnoreCase))
                                config.PinSalt = val;
                        }
                    }
                }
            }
            catch { }
            return config;
        }

        public static void Save(AppConfig config)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("# FamilyGuard Configuration");
                sb.AppendLine("IsProtectionActive=" + (config.IsProtectionActive ? "true" : "false"));
                sb.AppendLine("DnsProfileIndex=" + config.DnsProfileIndex);
                sb.AppendLine("EnableSafeSearch=" + (config.EnableSafeSearch ? "true" : "false"));
                sb.AppendLine("EnableDomainBlock=" + (config.EnableDomainBlock ? "true" : "false"));
                sb.AppendLine("PinHash=" + (config.PinHash ?? ""));
                sb.AppendLine("PinSalt=" + (config.PinSalt ?? ""));
                sb.AppendLine("[CUSTOM_DOMAINS]");
                if (config.CustomDomains != null)
                {
                    foreach (string d in config.CustomDomains)
                    {
                        if (!string.IsNullOrEmpty(d))
                            sb.AppendLine(d);
                    }
                }
                File.WriteAllText(ConfigPath, sb.ToString(), Encoding.UTF8);
            }
            catch { }
        }

        public static string ComputeHash(string pin, string salt)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(pin + salt);
                byte[] hash = sha.ComputeHash(bytes);
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hash)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }

        public static string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        public static bool VerifyPin(AppConfig config, string enteredPin)
        {
            if (string.IsNullOrEmpty(config.PinHash))
                return true; // No PIN set

            string hash = ComputeHash(enteredPin, config.PinSalt);
            return string.Equals(hash, config.PinHash, StringComparison.OrdinalIgnoreCase);
        }
    }
}
