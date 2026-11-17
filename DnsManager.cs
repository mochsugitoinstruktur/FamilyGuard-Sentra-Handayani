using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;

namespace FamilyGuard
{
    public class DnsProfile
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PrimaryIpv4 { get; set; }
        public string SecondaryIpv4 { get; set; }

        public DnsProfile(string name, string description, string primary, string secondary)
        {
            Name = name;
            Description = description;
            PrimaryIpv4 = primary;
            SecondaryIpv4 = secondary;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public static class DnsManager
    {
        [DllImport("dnsapi.dll", EntryPoint = "DnsFlushResolverCache")]
        private static extern UInt32 DnsFlushResolverCache();

        public static List<DnsProfile> GetProfiles()
        {
            List<DnsProfile> list = new List<DnsProfile>();
            list.Add(new DnsProfile(
                "CleanBrowsing Family (Rekomendasi Utama)",
                "Memblokir konten dewasa, situs radikal/ekstremis, judi, dan proxy bypass. Otomatis SafeSearch.",
                "185.228.168.168",
                "185.228.169.168"
            ));
            list.Add(new DnsProfile(
                "Cloudflare Family 1.1.1.3",
                "Sangat cepat. Memblokir malware, phishing, dan semua situs dewasa (18+).",
                "1.1.1.3",
                "1.0.0.3"
            ));
            list.Add(new DnsProfile(
                "AdGuard Family Protection",
                "Memblokir situs dewasa, iklan tidak senonoh, dan memaksakan pencarian aman.",
                "94.140.14.15",
                "94.140.15.16"
            ));
            return list;
        }

        public static List<string> GetActiveAdapterNames()
        {
            List<string> activeNames = new List<string>();
            try
            {
                NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
                foreach (NetworkInterface nic in interfaces)
                {
                    if (nic.NetworkInterfaceType == NetworkInterfaceType.Loopback)
                        continue;
                    if (nic.NetworkInterfaceType == NetworkInterfaceType.Tunnel)
                        continue;
                    if (nic.Description.ToLower().Contains("virtual") || nic.Description.ToLower().Contains("hyper-v"))
                        continue;

                    // Include Ethernet & Wireless or any active interface
                    if (nic.OperationalStatus == OperationalStatus.Up ||
                        nic.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 ||
                        nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                    {
                        if (!activeNames.Contains(nic.Name))
                        {
                            activeNames.Add(nic.Name);
                        }
                    }
                }
            }
            catch { }
            return activeNames;
        }

        public static bool ApplyDns(DnsProfile profile, out string log)
        {
            StringBuilder sb = new StringBuilder();
            bool overallSuccess = true;

            List<string> adapters = GetActiveAdapterNames();
            if (adapters.Count == 0)
            {
                sb.AppendLine("Tidak ada adapter jaringan yang terdeteksi.");
                log = sb.ToString();
                return false;
            }

            foreach (string adapter in adapters)
            {
                sb.AppendLine(string.Format("Mengatur DNS pada adapter: '{0}'...", adapter));

                // 1. Primary DNS via netsh
                string cmdPrimary = string.Format("interface ipv4 set dns name=\"{0}\" static {1} primary", adapter, profile.PrimaryIpv4);
                string out1 = RunNetsh(cmdPrimary);

                // 2. Secondary DNS via netsh
                string cmdSecondary = string.Format("interface ipv4 add dns name=\"{0}\" {1} index=2", adapter, profile.SecondaryIpv4);
                string out2 = RunNetsh(cmdSecondary);

                // Backup/Alternative via PowerShell in case netsh localized name issues
                string psCmd = string.Format(
                    "Set-DnsClientServerAddress -InterfaceAlias '{0}' -ServerAddresses ('{1}','{2}') -ErrorAction SilentlyContinue",
                    adapter.Replace("'", "''"), profile.PrimaryIpv4, profile.SecondaryIpv4
                );
                RunPowerShell(psCmd);

                sb.AppendLine(string.Format("  -> DNS 1: {0}, DNS 2: {1} diterapkan.", profile.PrimaryIpv4, profile.SecondaryIpv4));
            }

            FlushDns();
            sb.AppendLine("Cache DNS Windows berhasil dibersihkan (DNS Flushed).");
            log = sb.ToString();
            return overallSuccess;
        }

        public static bool ResetDnsToDefault(out string log)
        {
            StringBuilder sb = new StringBuilder();
            List<string> adapters = GetActiveAdapterNames();

            foreach (string adapter in adapters)
            {
                sb.AppendLine(string.Format("Mengembalikan DNS adapter '{0}' ke otomatis (DHCP)...", adapter));
                RunNetsh(string.Format("interface ipv4 set dns name=\"{0}\" dhcp", adapter));

                string psCmd = string.Format(
                    "Set-DnsClientServerAddress -InterfaceAlias '{0}' -ResetServerAddresses -ErrorAction SilentlyContinue",
                    adapter.Replace("'", "''")
                );
                RunPowerShell(psCmd);
            }

            FlushDns();
            sb.AppendLine("DNS telah dikembalikan ke bawaan otomatis.");
            log = sb.ToString();
            return true;
        }

        public static void FlushDns()
        {
            try
            {
                DnsFlushResolverCache();
            }
            catch { }

            try
            {
                ProcessStartInfo psi = new ProcessStartInfo("ipconfig", "/flushdns");
                psi.CreateNoWindow = true;
                psi.UseShellExecute = false;
                Process p = Process.Start(psi);
                if (p != null)
                {
                    p.WaitForExit(3000);
                }
            }
            catch { }
        }

        private static string RunNetsh(string arguments)
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo("netsh", arguments);
                psi.CreateNoWindow = true;
                psi.UseShellExecute = false;
                psi.RedirectStandardOutput = true;
                psi.RedirectStandardError = true;
                using (Process p = Process.Start(psi))
                {
                    string output = p.StandardOutput.ReadToEnd();
                    p.WaitForExit(4000);
                    return output;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private static void RunPowerShell(string script)
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "powershell.exe";
                psi.Arguments = "-NoProfile -ExecutionPolicy Bypass -Command \"" + script + "\"";
                psi.CreateNoWindow = true;
                psi.UseShellExecute = false;
                using (Process p = Process.Start(psi))
                {
                    p.WaitForExit(4000);
                }
            }
            catch { }
        }
    }
}
