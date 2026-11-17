using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FamilyGuard
{
    public static class HostsManager
    {
        private const string SectionHeader = "# === FAMILYGUARD PROTECTION START ===";
        private const string SectionFooter = "# === FAMILYGUARD PROTECTION END ===";

        public static string HostsFilePath
        {
            get
            {
                string sysDir = Environment.GetFolderPath(Environment.SpecialFolder.System);
                return Path.Combine(sysDir, @"drivers\etc\hosts");
            }
        }

        // SafeSearch IP Mapping
        public static readonly string[] SafeSearchRules = new string[]
        {
            // Google SafeSearch
            "216.239.38.120 forcesafesearch.google.com",
            "216.239.38.120 google.com",
            "216.239.38.120 www.google.com",
            "216.239.38.120 google.co.id",
            "216.239.38.120 www.google.co.id",
            "216.239.38.120 google.com.sg",
            "216.239.38.120 www.google.com.sg",

            // Bing Strict SafeSearch
            "204.79.197.220 strict.bing.com",
            "204.79.197.220 bing.com",
            "204.79.197.220 www.bing.com",

            // YouTube Restricted Mode
            "216.239.38.119 restrict.youtube.com",
            "216.239.38.119 youtube.com",
            "216.239.38.119 www.youtube.com",
            "216.239.38.119 m.youtube.com",
            "216.239.38.119 youtubei.googleapis.com",
            "216.239.38.119 youtube.googleapis.com",
            "216.239.38.119 www.youtube-nocookie.com",

            // DuckDuckGo SafeSearch
            "52.142.124.215 safe.duckduckgo.com",
            "52.142.124.215 duckduckgo.com"
        };

        // Radikalisme, Terorisme, Ekstremisme & Hate Groups
        public static readonly string[] ExtremismBlocklist = new string[]
        {
            "stormfront.org",
            "www.stormfront.org",
            "ironmarch.org",
            "www.ironmarch.org",
            "dailystormer.in",
            "www.dailystormer.in",
            "dailystormer.su",
            "www.dailystormer.su",
            "alhayatmedia.org",
            "www.alhayatmedia.org",
            "ansar1.info",
            "www.ansar1.info",
            "dabiqmagazine.com",
            "www.dabiqmagazine.com",
            "jihadology.net",
            "www.jihadology.net",
            "ansaralhaqq.com",
            "www.ansaralhaqq.com",
            "vanguardnewsnetwork.com",
            "www.vanguardnewsnetwork.com",
            "thewhitenetwork.com",
            "www.thewhitenetwork.com",
            "kkk.bz",
            "www.kkk.bz",
            "americannaziparty.com",
            "www.americannaziparty.com",
            "aryan-nations.org",
            "www.aryan-nations.org",
            "bloodandhonour.com",
            "www.bloodandhonour.com",
            "combat18.org",
            "www.combat18.org",
            "atomwaffendivision.org",
            "www.atomwaffendivision.org",
            "thebase.org",
            "www.thebase.org",
            "isis-news.net",
            "www.isis-news.net",
            "khilafah.com",
            "www.khilafah.com",
            "hizb-ut-tahrir.org",
            "www.hizb-ut-tahrir.org",
            "alqaeda.org",
            "www.alqaeda.org",
            "terrorist-bulletin.org",
            "www.terrorist-bulletin.org",
            "radicalislam.org",
            "www.radicalislam.org",
            "whitehonor.com",
            "www.whitehonor.com",
            "hate-speech.org",
            "www.hate-speech.org",
            "deathpenalty.org",
            "extremism.org",
            "www.extremism.org",
            "jihadi.org",
            "www.jihadi.org",
            "gurochan.net",
            "www.gurochan.net",
            "bestgore.fun",
            "www.bestgore.fun",
            "seegore.com",
            "www.seegore.com",
            "gorecenter.com",
            "www.gorecenter.com",
            "theync.com",
            "www.theync.com",
            "kaotic.com",
            "www.kaotic.com",
            "heavy-r.com",
            "www.heavy-r.com"
        };

        // Konten Dewasa & Pornografi Populer
        public static readonly string[] AdultBlocklist = new string[]
        {
            "pornhub.com", "www.pornhub.com",
            "xvideos.com", "www.xvideos.com",
            "xnxx.com", "www.xnxx.com",
            "xhamster.com", "www.xhamster.com",
            "redtube.com", "www.redtube.com",
            "youporn.com", "www.youporn.com",
            "tube8.com", "www.tube8.com",
            "beeg.com", "www.beeg.com",
            "spankbang.com", "www.spankbang.com",
            "eporner.com", "www.eporner.com",
            "chaturbate.com", "www.chaturbate.com",
            "camsoda.com", "www.camsoda.com",
            "stripchat.com", "www.stripchat.com",
            "bongacams.com", "www.bongacams.com",
            "livejasmin.com", "www.livejasmin.com",
            "onlyfans.com", "www.onlyfans.com",
            "fansly.com", "www.fansly.com",
            "brazzers.com", "www.brazzers.com",
            "naughtyamerica.com", "www.naughtyamerica.com",
            "tnaflix.com", "www.tnaflix.com",
            "drtuber.com", "www.drtuber.com",
            "thumbzilla.com", "www.thumbzilla.com",
            "nuvid.com", "www.nuvid.com",
            "txxx.com", "www.txxx.com",
            "javhd.com", "www.javhd.com",
            "hentaihaven.xxx", "www.hentaihaven.xxx",
            "hanime.tv", "www.hanime.tv",
            "nhentai.net", "www.nhentai.net",
            "erome.com", "www.erome.com",
            "porntrex.com", "www.porntrex.com",
            "sex.com", "www.sex.com",
            "playboy.com", "www.playboy.com",
            "penthouse.com", "www.penthouse.com",
            "cam4.com", "www.cam4.com",
            "faphouse.com", "www.faphouse.com",
            "rule34.xxx", "www.rule34.xxx",
            "gelbooru.com", "www.gelbooru.com",
            "danbooru.donmai.us",
            "4chan.org", "www.4chan.org",
            "boards.4channel.org",
            "kiwifarms.net", "www.kiwifarms.net",
            "omegle.com", "www.omegle.com",
            "chathub.cam", "www.chathub.cam",
            "bazoocam.org", "www.bazoocam.org",
            "camzap.com", "www.camzap.com",
            "shagle.com", "www.shagle.com",
            "chatrandom.com", "www.chatrandom.com",
            "coomeet.com", "www.coomeet.com"
        };

        // Web Proxies / Unblockers (Mencegah anak melakukan bypass pemblokiran)
        public static readonly string[] BypassBlocklist = new string[]
        {
            "kproxy.com", "www.kproxy.com",
            "croxyproxy.com", "www.croxyproxy.com",
            "blockaway.net", "www.blockaway.net",
            "proxysite.com", "www.proxysite.com",
            "hide.me", "www.hide.me",
            "hidester.com", "www.hidester.com",
            "whoer.net", "www.whoer.net",
            "megaproxy.com", "www.megaproxy.com",
            "filterbypass.me", "www.filterbypass.me",
            "4everproxy.com", "www.4everproxy.com",
            "plainproxy.com", "www.plainproxy.com",
            "vpnbook.com", "www.vpnbook.com",
            "anonymouse.org", "www.anonymouse.org",
            "zalmos.com", "www.zalmos.com",
            "unblocksite.id", "www.unblocksite.id",
            "bypassweb.com", "www.bypassweb.com"
        };

        public static bool ApplyHostsProtection(bool enableSafeSearch, bool enableDomainBlocks, List<string> customDomains, out string log)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                string path = HostsFilePath;
                if (!File.Exists(path))
                {
                    File.WriteAllText(path, "# Windows Hosts File\r\n127.0.0.1 localhost\r\n::1 localhost\r\n");
                }

                // Remove read-only attribute if present
                FileAttributes attr = File.GetAttributes(path);
                if ((attr & FileAttributes.ReadOnly) != 0)
                {
                    File.SetAttributes(path, attr & ~FileAttributes.ReadOnly);
                }

                string currentContent = File.ReadAllText(path, Encoding.UTF8);
                string cleanContent = RemoveFamilyGuardSection(currentContent);

                StringBuilder newSection = new StringBuilder();
                newSection.AppendLine(SectionHeader);
                newSection.AppendLine("# Waktu Diperbarui: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                int ruleCount = 0;

                // 1. SafeSearch Rules
                if (enableSafeSearch)
                {
                    newSection.AppendLine("\r\n# [1] Mode SafeSearch Wajib (Google, Bing, YouTube, DuckDuckGo)");
                    foreach (string rule in SafeSearchRules)
                    {
                        newSection.AppendLine(rule);
                        ruleCount++;
                    }
                }

                // 2. Domain Blocklist
                if (enableDomainBlocks)
                {
                    newSection.AppendLine("\r\n# [2] Blokir Radikalisme, Terorisme & Ekstremisme Kekerasan");
                    foreach (string domain in ExtremismBlocklist)
                    {
                        newSection.AppendLine("0.0.0.0 " + domain);
                        ruleCount++;
                    }

                    newSection.AppendLine("\r\n# [3] Blokir Konten Dewasa & Pornografi (18+)");
                    foreach (string domain in AdultBlocklist)
                    {
                        newSection.AppendLine("0.0.0.0 " + domain);
                        ruleCount++;
                    }

                    newSection.AppendLine("\r\n# [4] Blokir Web Proxy & Alat Bypass Filter");
                    foreach (string domain in BypassBlocklist)
                    {
                        newSection.AppendLine("0.0.0.0 " + domain);
                        ruleCount++;
                    }
                }

                // 3. Custom Domains
                if (customDomains != null && customDomains.Count > 0)
                {
                    newSection.AppendLine("\r\n# [5] Daftar Blokir Tambahan (Orang Tua)");
                    foreach (string rawDomain in customDomains)
                    {
                        string clean = CleanDomain(rawDomain);
                        if (!string.IsNullOrEmpty(clean))
                        {
                            newSection.AppendLine("0.0.0.0 " + clean);
                            if (!clean.StartsWith("www."))
                            {
                                newSection.AppendLine("0.0.0.0 www." + clean);
                            }
                            ruleCount++;
                        }
                    }
                }

                newSection.AppendLine(SectionFooter);

                // Write back
                string finalContent = cleanContent.TrimEnd() + "\r\n\r\n" + newSection.ToString();
                File.WriteAllText(path, finalContent, Encoding.UTF8);

                DnsManager.FlushDns();

                sb.AppendLine(string.Format("Berhasil menerapkan {0} aturan pemblokiran ke file sistem Windows (hosts).", ruleCount));
                log = sb.ToString();
                return true;
            }
            catch (Exception ex)
            {
                log = "Gagal memperbarui file hosts: " + ex.Message + " (Pastikan dijalankan sebagai Administrator)";
                return false;
            }
        }

        public static bool RemoveHostsProtection(out string log)
        {
            try
            {
                string path = HostsFilePath;
                if (!File.Exists(path))
                {
                    log = "File hosts tidak ditemukan.";
                    return true;
                }

                FileAttributes attr = File.GetAttributes(path);
                if ((attr & FileAttributes.ReadOnly) != 0)
                {
                    File.SetAttributes(path, attr & ~FileAttributes.ReadOnly);
                }

                string currentContent = File.ReadAllText(path, Encoding.UTF8);
                string cleanContent = RemoveFamilyGuardSection(currentContent);

                File.WriteAllText(path, cleanContent.TrimEnd() + "\r\n", Encoding.UTF8);
                DnsManager.FlushDns();

                log = "Semua aturan pemblokiran hosts FamilyGuard berhasil dinonaktifkan.";
                return true;
            }
            catch (Exception ex)
            {
                log = "Gagal membersihkan file hosts: " + ex.Message;
                return false;
            }
        }

        private static string RemoveFamilyGuardSection(string content)
        {
            int startIndex = content.IndexOf(SectionHeader);
            if (startIndex == -1)
                return content;

            int endIndex = content.IndexOf(SectionFooter, startIndex);
            if (endIndex == -1)
            {
                // Section opened but not closed, remove until end
                return content.Substring(0, startIndex);
            }

            endIndex += SectionFooter.Length;
            return content.Substring(0, startIndex) + content.Substring(endIndex);
        }

        public static string CleanDomain(string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;
            string d = input.Trim().ToLower();
            if (d.StartsWith("http://")) d = d.Substring(7);
            if (d.StartsWith("https://")) d = d.Substring(8);
            int slashIdx = d.IndexOf('/');
            if (slashIdx >= 0) d = d.Substring(0, slashIdx);
            int colonIdx = d.IndexOf(':');
            if (colonIdx >= 0) d = d.Substring(0, colonIdx);
            return d.Trim();
        }
    }
}
