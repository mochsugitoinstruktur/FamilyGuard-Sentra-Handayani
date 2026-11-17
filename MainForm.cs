using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Security.Principal;
using System.Windows.Forms;

namespace FamilyGuard
{
    public class MainForm : Form
    {
        private AppConfig config;
        private List<DnsProfile> dnsProfiles;

        // UI Controls - Header & Status
        private Panel panelHeader;
        private Label lblHeaderTitle;
        private Label lblHeaderSub;
        private Label lblHeaderCreator;
        private Panel panelStatusCard;
        private Label lblStatusIcon;
        private Label lblStatusText;
        private Label lblStatusSub;
        private Button btnToggleProtection;
        private Label lblAdminWarning;

        // Footer Bar (Tampil Permanen di Bagian Bawah Jendela)
        private Panel panelFooter;
        private Label lblFooterCreator;

        // Tabs
        private TabControl tabControl;
        private TabPage tabSettings;
        private TabPage tabCustom;
        private TabPage tabPin;
        private TabPage tabLog;
        private TabPage tabAuthor;

        // Tab Settings
        private ComboBox cbDnsProfile;
        private Label lblDnsDescription;
        private CheckBox chkSafeSearch;
        private CheckBox chkDomainBlock;
        private Button btnApplySettings;

        // Tab Custom
        private ListBox listCustomDomains;
        private TextBox txtNewDomain;
        private Button btnAddDomain;
        private Button btnRemoveDomain;

        // Tab PIN
        private Label lblPinStatus;
        private Button btnSetPin;
        private Button btnRemovePin;

        // Tab Log
        private RichTextBox rtbLog;

        public MainForm()
        {
            InitializeComponent();
        }

        private Label CreatePageAuthorBadge(int width, int y)
        {
            Label lbl = new Label();
            lbl.Text = "👨‍🏫 Moch. Sugito S.T (Instruktur Ahli Pertama) | NIP : 199205262025061002";
            lbl.Font = new Font("Segoe UI", 8.2f, FontStyle.Bold);
            lbl.ForeColor = Color.FromArgb(71, 85, 105);
            lbl.BackColor = Color.FromArgb(241, 245, 249);
            lbl.BorderStyle = BorderStyle.FixedSingle;
            lbl.Location = new Point(20, y);
            lbl.Size = new Size(width, 24);
            lbl.TextAlign = ContentAlignment.MiddleCenter;
            return lbl;
        }

        private void InitializeComponent()
        {
            this.Text = "FamilyGuard - Moch. Sugito S.T (Instruktur Ahli Pertama) [NIP : 199205262025061002]";
            this.Size = new Size(710, 700);
            this.MinimumSize = new Size(710, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(248, 250, 252);
            this.Font = new Font("Segoe UI", 9.5f, FontStyle.Regular);

            // 1. Header Panel (Tampilan Atas)
            panelHeader = new Panel();
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Height = 84;
            panelHeader.BackColor = Color.FromArgb(15, 23, 42); // Slate 900

            lblHeaderTitle = new Label();
            lblHeaderTitle.Text = "🛡️ FamilyGuard - Perlindungan Keluarga";
            lblHeaderTitle.Font = new Font("Segoe UI", 12.5f, FontStyle.Bold);
            lblHeaderTitle.ForeColor = Color.White;
            lblHeaderTitle.Location = new Point(20, 10);
            lblHeaderTitle.AutoSize = true;

            lblHeaderSub = new Label();
            lblHeaderSub.Text = "Filter konten dewasa (18+), radikalisme & pencarian aman untuk anak";
            lblHeaderSub.Font = new Font("Segoe UI", 8.8f, FontStyle.Regular);
            lblHeaderSub.ForeColor = Color.FromArgb(148, 163, 184);
            lblHeaderSub.Location = new Point(22, 34);
            lblHeaderSub.AutoSize = true;

            lblHeaderCreator = new Label();
            lblHeaderCreator.Text = "Pembuat: Moch. Sugito S.T (Instruktur Ahli Pertama) | NIP : 199205262025061002";
            lblHeaderCreator.Font = new Font("Segoe UI", 8.3f, FontStyle.Bold);
            lblHeaderCreator.ForeColor = Color.FromArgb(56, 189, 248); // Sky blue
            lblHeaderCreator.Location = new Point(22, 58);
            lblHeaderCreator.AutoSize = true;

            panelHeader.Controls.Add(lblHeaderTitle);
            panelHeader.Controls.Add(lblHeaderSub);
            panelHeader.Controls.Add(lblHeaderCreator);

            // Admin Warning Label (if not elevated)
            lblAdminWarning = new Label();
            lblAdminWarning.Dock = DockStyle.Top;
            lblAdminWarning.Height = 30;
            lblAdminWarning.BackColor = Color.FromArgb(254, 240, 138);
            lblAdminWarning.ForeColor = Color.FromArgb(133, 77, 14);
            lblAdminWarning.Font = new Font("Segoe UI", 8.8f, FontStyle.Bold);
            lblAdminWarning.TextAlign = ContentAlignment.MiddleCenter;
            lblAdminWarning.Text = "⚠️ Aplikasi memerlukan hak Administrator. Jalankan sebagai Administrator agar filter aktif.";
            lblAdminWarning.Visible = !IsAdministrator();

            // Permanent Footer Panel (Tampilan Bawah - Selalu Muncul di Semua Layar)
            panelFooter = new Panel();
            panelFooter.Dock = DockStyle.Bottom;
            panelFooter.Height = 36;
            panelFooter.BackColor = Color.FromArgb(15, 23, 42); // Slate 900

            lblFooterCreator = new Label();
            lblFooterCreator.Text = "👨‍🏫 Moch. Sugito S.T (Instruktur Ahli Pertama)  •  NIP : 199205262025061002";
            lblFooterCreator.Font = new Font("Segoe UI", 8.8f, FontStyle.Bold);
            lblFooterCreator.ForeColor = Color.FromArgb(226, 232, 240);
            lblFooterCreator.Dock = DockStyle.Fill;
            lblFooterCreator.TextAlign = ContentAlignment.MiddleCenter;
            panelFooter.Controls.Add(lblFooterCreator);

            // 2. Status Card Panel
            panelStatusCard = new Panel();
            panelStatusCard.Location = new Point(20, 124);
            panelStatusCard.Size = new Size(654, 90);
            panelStatusCard.BackColor = Color.White;
            panelStatusCard.BorderStyle = BorderStyle.FixedSingle;

            lblStatusIcon = new Label();
            lblStatusIcon.Font = new Font("Segoe UI", 24f, FontStyle.Bold);
            lblStatusIcon.Location = new Point(14, 16);
            lblStatusIcon.Size = new Size(48, 48);
            lblStatusIcon.TextAlign = ContentAlignment.MiddleCenter;

            lblStatusText = new Label();
            lblStatusText.Font = new Font("Segoe UI", 11.5f, FontStyle.Bold);
            lblStatusText.Location = new Point(70, 18);
            lblStatusText.Size = new Size(360, 24);

            lblStatusSub = new Label();
            lblStatusSub.Font = new Font("Segoe UI", 8.3f, FontStyle.Regular);
            lblStatusSub.ForeColor = Color.FromArgb(100, 116, 139);
            lblStatusSub.Location = new Point(72, 44);
            lblStatusSub.Size = new Size(360, 34);

            btnToggleProtection = new Button();
            btnToggleProtection.Location = new Point(445, 22);
            btnToggleProtection.Size = new Size(190, 44);
            btnToggleProtection.Font = new Font("Segoe UI", 10.5f, FontStyle.Bold);
            btnToggleProtection.FlatStyle = FlatStyle.Flat;
            btnToggleProtection.FlatAppearance.BorderSize = 0;
            btnToggleProtection.Click += BtnToggleProtection_Click;

            panelStatusCard.Controls.Add(lblStatusIcon);
            panelStatusCard.Controls.Add(lblStatusText);
            panelStatusCard.Controls.Add(lblStatusSub);
            panelStatusCard.Controls.Add(btnToggleProtection);

            // 3. Tab Control (Halaman Konten)
            tabControl = new TabControl();
            tabControl.Location = new Point(20, 224);
            tabControl.Size = new Size(654, 385);
            tabControl.Font = new Font("Segoe UI", 9.3f, FontStyle.Regular);

            tabSettings = new TabPage("Pengaturan Filter");
            tabCustom = new TabPage("Situs Kustom");
            tabPin = new TabPage("Keamanan PIN");
            tabLog = new TabPage("Status & Log");
            tabAuthor = new TabPage("Halaman Pembuat");

            InitTabSettings();
            InitTabCustom();
            InitTabPin();
            InitTabLog();
            InitTabAuthor();

            tabControl.TabPages.Add(tabSettings);
            tabControl.TabPages.Add(tabCustom);
            tabControl.TabPages.Add(tabPin);
            tabControl.TabPages.Add(tabLog);
            tabControl.TabPages.Add(tabAuthor);

            // Add all to form
            this.Controls.Add(tabControl);
            this.Controls.Add(panelStatusCard);
            this.Controls.Add(panelFooter);
            this.Controls.Add(lblAdminWarning);
            this.Controls.Add(panelHeader);

            this.Load += MainForm_Load;
            this.FormClosing += MainForm_FormClosing;
        }

        private void InitTabSettings()
        {
            tabSettings.BackColor = Color.White;

            Label lblDnsTitle = new Label();
            lblDnsTitle.Text = "1. Penyedia DNS Ramah Keluarga (Network Shield):";
            lblDnsTitle.Font = new Font("Segoe UI", 9.3f, FontStyle.Bold);
            lblDnsTitle.Location = new Point(20, 12);
            lblDnsTitle.Size = new Size(450, 18);

            cbDnsProfile = new ComboBox();
            cbDnsProfile.DropDownStyle = ComboBoxStyle.DropDownList;
            cbDnsProfile.Location = new Point(22, 34);
            cbDnsProfile.Size = new Size(420, 25);
            cbDnsProfile.SelectedIndexChanged += CbDnsProfile_SelectedIndexChanged;

            lblDnsDescription = new Label();
            lblDnsDescription.Font = new Font("Segoe UI", 8.3f, FontStyle.Italic);
            lblDnsDescription.ForeColor = Color.FromArgb(71, 85, 105);
            lblDnsDescription.Location = new Point(22, 62);
            lblDnsDescription.Size = new Size(590, 32);

            Label lblDivider = new Label();
            lblDivider.BorderStyle = BorderStyle.Fixed3D;
            lblDivider.Location = new Point(20, 98);
            lblDivider.Size = new Size(600, 2);

            Label lblHostsTitle = new Label();
            lblHostsTitle.Text = "2. Filter Lokal Tingkat Sistem (Windows):";
            lblHostsTitle.Font = new Font("Segoe UI", 9.3f, FontStyle.Bold);
            lblHostsTitle.Location = new Point(20, 106);
            lblHostsTitle.Size = new Size(450, 18);

            chkSafeSearch = new CheckBox();
            chkSafeSearch.Text = "Wajibkan Pencarian Aman (SafeSearch di Google, YouTube, Bing & DuckDuckGo)";
            chkSafeSearch.Font = new Font("Segoe UI", 9.2f, FontStyle.Regular);
            chkSafeSearch.Location = new Point(22, 128);
            chkSafeSearch.Size = new Size(590, 22);

            Label lblSafeSearchNote = new Label();
            lblSafeSearchNote.Text = "Menghilangkan gambar/video dewasa dan radikal dari mesin pencari (aktif juga di mode Incognito/Private).";
            lblSafeSearchNote.Font = new Font("Segoe UI", 8.2f, FontStyle.Regular);
            lblSafeSearchNote.ForeColor = Color.FromArgb(100, 116, 139);
            lblSafeSearchNote.Location = new Point(44, 150);
            lblSafeSearchNote.Size = new Size(570, 24);

            chkDomainBlock = new CheckBox();
            chkDomainBlock.Text = "Blokir Radikalisme, Ekstremisme Kekerasan, Situs 18+ & Alat Bypass Web Proxy";
            chkDomainBlock.Font = new Font("Segoe UI", 9.2f, FontStyle.Regular);
            chkDomainBlock.Location = new Point(22, 176);
            chkDomainBlock.Size = new Size(590, 22);

            Label lblDomainNote = new Label();
            lblDomainNote.Text = "Mencegah komputer membuka portal terlarang, forum kebencian, situs sadis, dan proxy bypass secara instan.";
            lblDomainNote.Font = new Font("Segoe UI", 8.2f, FontStyle.Regular);
            lblDomainNote.ForeColor = Color.FromArgb(100, 116, 139);
            lblDomainNote.Location = new Point(44, 198);
            lblDomainNote.Size = new Size(570, 24);

            btnApplySettings = new Button();
            btnApplySettings.Text = "Terapkan & Simpan Pengaturan";
            btnApplySettings.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            btnApplySettings.BackColor = Color.FromArgb(30, 64, 175);
            btnApplySettings.ForeColor = Color.White;
            btnApplySettings.FlatStyle = FlatStyle.Flat;
            btnApplySettings.FlatAppearance.BorderSize = 0;
            btnApplySettings.Location = new Point(22, 230);
            btnApplySettings.Size = new Size(230, 36);
            btnApplySettings.Click += BtnApplySettings_Click;

            // Identitas Pembuat di Halaman Pengaturan Filter
            Label lblPageAuthor = CreatePageAuthorBadge(600, 290);

            tabSettings.Controls.Add(lblDnsTitle);
            tabSettings.Controls.Add(cbDnsProfile);
            tabSettings.Controls.Add(lblDnsDescription);
            tabSettings.Controls.Add(lblDivider);
            tabSettings.Controls.Add(lblHostsTitle);
            tabSettings.Controls.Add(chkSafeSearch);
            tabSettings.Controls.Add(lblSafeSearchNote);
            tabSettings.Controls.Add(chkDomainBlock);
            tabSettings.Controls.Add(lblDomainNote);
            tabSettings.Controls.Add(btnApplySettings);
            tabSettings.Controls.Add(lblPageAuthor);
        }

        private void InitTabCustom()
        {
            tabCustom.BackColor = Color.White;

            Label lblTitle = new Label();
            lblTitle.Text = "Daftar Domain Tambahan yang Ingin Diblokir oleh Orang Tua:";
            lblTitle.Font = new Font("Segoe UI", 9.3f, FontStyle.Bold);
            lblTitle.Location = new Point(20, 12);
            lblTitle.Size = new Size(500, 18);

            listCustomDomains = new ListBox();
            listCustomDomains.Location = new Point(22, 34);
            listCustomDomains.Size = new Size(430, 175);
            listCustomDomains.Font = new Font("Consolas", 9.5f);

            btnRemoveDomain = new Button();
            btnRemoveDomain.Text = "Hapus Terpilih";
            btnRemoveDomain.Location = new Point(465, 34);
            btnRemoveDomain.Size = new Size(130, 30);
            btnRemoveDomain.BackColor = Color.FromArgb(241, 245, 249);
            btnRemoveDomain.FlatStyle = FlatStyle.Flat;
            btnRemoveDomain.FlatAppearance.BorderSize = 0;
            btnRemoveDomain.Click += BtnRemoveDomain_Click;

            Label lblAddPrompt = new Label();
            lblAddPrompt.Text = "Tambah domain baru (contoh: reddit.com, omegle.com, domain.xyz):";
            lblAddPrompt.Font = new Font("Segoe UI", 8.8f, FontStyle.Regular);
            lblAddPrompt.ForeColor = Color.FromArgb(71, 85, 105);
            lblAddPrompt.Location = new Point(20, 218);
            lblAddPrompt.Size = new Size(500, 18);

            txtNewDomain = new TextBox();
            txtNewDomain.Location = new Point(22, 238);
            txtNewDomain.Size = new Size(340, 25);
            txtNewDomain.Font = new Font("Segoe UI", 9.5f);

            btnAddDomain = new Button();
            btnAddDomain.Text = "Tambah ke Blokir";
            btnAddDomain.Location = new Point(370, 237);
            btnAddDomain.Size = new Size(130, 27);
            btnAddDomain.BackColor = Color.FromArgb(16, 185, 129);
            btnAddDomain.ForeColor = Color.White;
            btnAddDomain.FlatStyle = FlatStyle.Flat;
            btnAddDomain.FlatAppearance.BorderSize = 0;
            btnAddDomain.Click += BtnAddDomain_Click;

            // Identitas Pembuat di Halaman Situs Kustom
            Label lblPageAuthor = CreatePageAuthorBadge(600, 290);

            tabCustom.Controls.Add(lblTitle);
            tabCustom.Controls.Add(listCustomDomains);
            tabCustom.Controls.Add(btnRemoveDomain);
            tabCustom.Controls.Add(lblAddPrompt);
            tabCustom.Controls.Add(txtNewDomain);
            tabCustom.Controls.Add(btnAddDomain);
            tabCustom.Controls.Add(lblPageAuthor);
        }

        private void InitTabPin()
        {
            tabPin.BackColor = Color.White;

            Label lblTitle = new Label();
            lblTitle.Text = "Kunci Pengaturan dengan PIN Orang Tua:";
            lblTitle.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            lblTitle.Location = new Point(20, 14);
            lblTitle.Size = new Size(500, 20);

            lblPinStatus = new Label();
            lblPinStatus.Font = new Font("Segoe UI", 9.3f, FontStyle.Bold);
            lblPinStatus.Location = new Point(22, 42);
            lblPinStatus.Size = new Size(500, 22);

            Label lblDesc = new Label();
            lblDesc.Text = "Saat PIN diatur, aplikasi akan meminta PIN setiap kali seseorang mencoba mematikan filter, mengubah pengaturan, atau menutup aplikasi. Ini mencegah anak mematikan proteksi.";
            lblDesc.Font = new Font("Segoe UI", 8.8f, FontStyle.Regular);
            lblDesc.ForeColor = Color.FromArgb(71, 85, 105);
            lblDesc.Location = new Point(22, 70);
            lblDesc.Size = new Size(580, 48);

            btnSetPin = new Button();
            btnSetPin.Text = "Atur / Ubah PIN";
            btnSetPin.Font = new Font("Segoe UI", 9.3f, FontStyle.Bold);
            btnSetPin.Location = new Point(22, 126);
            btnSetPin.Size = new Size(140, 36);
            btnSetPin.BackColor = Color.FromArgb(30, 136, 229);
            btnSetPin.ForeColor = Color.White;
            btnSetPin.FlatStyle = FlatStyle.Flat;
            btnSetPin.FlatAppearance.BorderSize = 0;
            btnSetPin.Click += BtnSetPin_Click;

            btnRemovePin = new Button();
            btnRemovePin.Text = "Hapus PIN";
            btnRemovePin.Font = new Font("Segoe UI", 9.3f, FontStyle.Regular);
            btnRemovePin.Location = new Point(172, 126);
            btnRemovePin.Size = new Size(110, 36);
            btnRemovePin.BackColor = Color.FromArgb(241, 245, 249);
            btnRemovePin.FlatStyle = FlatStyle.Flat;
            btnRemovePin.FlatAppearance.BorderSize = 0;
            btnRemovePin.Click += BtnRemovePin_Click;

            // Identitas Pembuat di Halaman Keamanan PIN
            Label lblPageAuthor = CreatePageAuthorBadge(600, 290);

            tabPin.Controls.Add(lblTitle);
            tabPin.Controls.Add(lblPinStatus);
            tabPin.Controls.Add(lblDesc);
            tabPin.Controls.Add(btnSetPin);
            tabPin.Controls.Add(btnRemovePin);
            tabPin.Controls.Add(lblPageAuthor);
        }

        private void InitTabLog()
        {
            tabLog.BackColor = Color.White;

            Label lblPageAuthor = CreatePageAuthorBadge(620, 6);

            rtbLog = new RichTextBox();
            rtbLog.Location = new Point(10, 36);
            rtbLog.Size = new Size(624, 280);
            rtbLog.ReadOnly = true;
            rtbLog.BackColor = Color.FromArgb(248, 250, 252);
            rtbLog.Font = new Font("Consolas", 8.8f);

            tabLog.Controls.Add(lblPageAuthor);
            tabLog.Controls.Add(rtbLog);
        }

        private void InitTabAuthor()
        {
            tabAuthor.BackColor = Color.White;

            Panel pnlCard = new Panel();
            pnlCard.Location = new Point(20, 16);
            pnlCard.Size = new Size(600, 295);
            pnlCard.BackColor = Color.FromArgb(241, 245, 249);
            pnlCard.BorderStyle = BorderStyle.FixedSingle;

            Label lblBadge = new Label();
            lblBadge.Text = "👨‍🏫 INFORMASI PEMBUAT APLIKASI";
            lblBadge.Font = new Font("Segoe UI", 10.5f, FontStyle.Bold);
            lblBadge.ForeColor = Color.FromArgb(30, 64, 175);
            lblBadge.Location = new Point(20, 16);
            lblBadge.AutoSize = true;

            Label lblName = new Label();
            lblName.Text = "Moch. Sugito S.T";
            lblName.Font = new Font("Segoe UI", 14f, FontStyle.Bold);
            lblName.ForeColor = Color.FromArgb(15, 23, 42);
            lblName.Location = new Point(20, 50);
            lblName.AutoSize = true;

            Label lblJabatan = new Label();
            lblJabatan.Text = "Jabatan : Instruktur Ahli Pertama";
            lblJabatan.Font = new Font("Segoe UI", 11f, FontStyle.Bold);
            lblJabatan.ForeColor = Color.FromArgb(51, 65, 85);
            lblJabatan.Location = new Point(22, 86);
            lblJabatan.AutoSize = true;

            Label lblNip = new Label();
            lblNip.Text = "NIP     : 199205262025061002";
            lblNip.Font = new Font("Segoe UI", 11f, FontStyle.Bold);
            lblNip.ForeColor = Color.FromArgb(15, 118, 110);
            lblNip.Location = new Point(22, 116);
            lblNip.AutoSize = true;

            Label lblDivider = new Label();
            lblDivider.BorderStyle = BorderStyle.Fixed3D;
            lblDivider.Location = new Point(20, 150);
            lblDivider.Size = new Size(560, 2);

            Label lblAboutDesc = new Label();
            lblAboutDesc.Text = "Aplikasi FamilyGuard ini didedikasikan untuk membatasi dan melindungi pengguna komputer agar tidak dapat mengakses konten dewasa 17 tahun ke atas, radikalisme, terorisme, ekstremisme, ujaran kebencian, serta konten terlarang lainnya demi keamanan digital keluarga.";
            lblAboutDesc.Font = new Font("Segoe UI", 9.2f, FontStyle.Regular);
            lblAboutDesc.ForeColor = Color.FromArgb(71, 85, 105);
            lblAboutDesc.Location = new Point(22, 162);
            lblAboutDesc.Size = new Size(555, 65);

            Label lblFooterNote = new Label();
            lblFooterNote.Text = "Moch. Sugito S.T (Instruktur Ahli Pertama) - NIP : 199205262025061002";
            lblFooterNote.Font = new Font("Segoe UI", 8.8f, FontStyle.Bold);
            lblFooterNote.ForeColor = Color.FromArgb(30, 64, 175);
            lblFooterNote.Location = new Point(22, 240);
            lblFooterNote.AutoSize = true;

            pnlCard.Controls.Add(lblBadge);
            pnlCard.Controls.Add(lblName);
            pnlCard.Controls.Add(lblJabatan);
            pnlCard.Controls.Add(lblNip);
            pnlCard.Controls.Add(lblDivider);
            pnlCard.Controls.Add(lblAboutDesc);
            pnlCard.Controls.Add(lblFooterNote);

            tabAuthor.Controls.Add(pnlCard);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            config = ConfigManager.Load();
            dnsProfiles = DnsManager.GetProfiles();

            foreach (DnsProfile p in dnsProfiles)
            {
                cbDnsProfile.Items.Add(p);
            }

            if (config.DnsProfileIndex >= 0 && config.DnsProfileIndex < dnsProfiles.Count)
                cbDnsProfile.SelectedIndex = config.DnsProfileIndex;
            else
                cbDnsProfile.SelectedIndex = 0;

            chkSafeSearch.Checked = config.EnableSafeSearch;
            chkDomainBlock.Checked = config.EnableDomainBlock;

            RefreshCustomDomainsList();
            UpdatePinStatusUI();
            UpdateProtectionStatusUI();

            LogMessage("FamilyGuard telah siap.");
            LogMessage("Pembuat: Moch. Sugito S.T (Instruktur Ahli Pertama) | NIP : 199205262025061002");
            if (config.IsProtectionActive)
            {
                LogMessage("Status: Proteksi sebelumnya dalam keadaan AKTIF.");
            }
            else
            {
                LogMessage("Status: Proteksi saat ini NONAKTIF. Klik 'Aktifkan Proteksi' untuk mengamankan komputer.");
            }
        }

        private void CbDnsProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDnsProfile.SelectedItem is DnsProfile)
            {
                DnsProfile p = (DnsProfile)cbDnsProfile.SelectedItem;
                lblDnsDescription.Text = p.Description + " (Primary: " + p.PrimaryIpv4 + ", Secondary: " + p.SecondaryIpv4 + ")";
            }
        }

        private void UpdateProtectionStatusUI()
        {
            if (config.IsProtectionActive)
            {
                panelStatusCard.BackColor = Color.FromArgb(240, 253, 244); // Green 50
                panelStatusCard.ForeColor = Color.FromArgb(22, 101, 52);

                lblStatusIcon.Text = "🛡️";
                lblStatusText.Text = "STATUS: PROTEKSI AKTIF";
                lblStatusText.ForeColor = Color.FromArgb(21, 128, 61); // Green 700

                lblStatusSub.Text = "Komputer terlindungi dari konten dewasa, radikalisme, dan SafeSearch diwajibkan.";
                lblStatusSub.ForeColor = Color.FromArgb(22, 101, 52);

                btnToggleProtection.Text = "Matikan Proteksi";
                btnToggleProtection.BackColor = Color.FromArgb(239, 68, 68); // Red
                btnToggleProtection.ForeColor = Color.White;
            }
            else
            {
                panelStatusCard.BackColor = Color.FromArgb(254, 242, 242); // Red 50
                panelStatusCard.ForeColor = Color.FromArgb(153, 27, 27);

                lblStatusIcon.Text = "⚠️";
                lblStatusIcon.ForeColor = Color.FromArgb(220, 38, 38);
                lblStatusText.Text = "STATUS: PROTEKSI NONAKTIF";
                lblStatusText.ForeColor = Color.FromArgb(185, 28, 28); // Red 700

                lblStatusSub.Text = "Komputer belum terlindungi. Semua situs masih dapat diakses bebas.";
                lblStatusSub.ForeColor = Color.FromArgb(100, 116, 139);

                btnToggleProtection.Text = "Aktifkan Proteksi";
                btnToggleProtection.BackColor = Color.FromArgb(22, 163, 74); // Green
                btnToggleProtection.ForeColor = Color.White;
            }
        }

        private void UpdatePinStatusUI()
        {
            if (string.IsNullOrEmpty(config.PinHash))
            {
                lblPinStatus.Text = "Status: Belum ada PIN terpasang (Tidak Terkunci).";
                lblPinStatus.ForeColor = Color.FromArgb(220, 38, 38);
                btnRemovePin.Enabled = false;
                btnSetPin.Text = "Pasang PIN Orang Tua";
            }
            else
            {
                lblPinStatus.Text = "Status: PIN Aktif Terpasang (Terkunci untuk Anak).";
                lblPinStatus.ForeColor = Color.FromArgb(22, 163, 74);
                btnRemovePin.Enabled = true;
                btnSetPin.Text = "Ubah PIN";
            }
        }

        private void RefreshCustomDomainsList()
        {
            listCustomDomains.Items.Clear();
            if (config.CustomDomains != null)
            {
                foreach (string d in config.CustomDomains)
                {
                    listCustomDomains.Items.Add(d);
                }
            }
        }

        private bool CheckParentPin(string actionDesc)
        {
            if (string.IsNullOrEmpty(config.PinHash))
                return true; // No PIN required

            using (PinDialog dlg = new PinDialog("Verifikasi PIN Orang Tua", "Masukkan PIN Anda untuk " + actionDesc + ":"))
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    if (ConfigManager.VerifyPin(config, dlg.EnteredPin))
                    {
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("PIN yang dimasukkan salah! Tindakan dibatalkan.", "PIN Salah", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            return false;
        }

        private void BtnToggleProtection_Click(object sender, EventArgs e)
        {
            if (config.IsProtectionActive)
            {
                // Disabling protection requires PIN
                if (!CheckParentPin("mematikan proteksi filter"))
                    return;

                DisableProtection();
            }
            else
            {
                EnableProtection();
            }
        }

        private void EnableProtection()
        {
            LogMessage("--- Memulai Pengaktifan Proteksi FamilyGuard ---");

            // 1. DNS Profile
            DnsProfile selectedProfile = (DnsProfile)cbDnsProfile.SelectedItem;
            string dnsLog;
            bool dnsOk = DnsManager.ApplyDns(selectedProfile, out dnsLog);
            LogMessage(dnsLog);

            // 2. Hosts Protection
            string hostsLog;
            bool hostsOk = HostsManager.ApplyHostsProtection(chkSafeSearch.Checked, chkDomainBlock.Checked, config.CustomDomains, out hostsLog);
            LogMessage(hostsLog);

            config.IsProtectionActive = true;
            config.DnsProfileIndex = cbDnsProfile.SelectedIndex;
            config.EnableSafeSearch = chkSafeSearch.Checked;
            config.EnableDomainBlock = chkDomainBlock.Checked;
            ConfigManager.Save(config);

            UpdateProtectionStatusUI();

            MessageBox.Show(
                "Proteksi telah berhasil diaktifkan!\r\n\r\n" +
                "• DNS Filter: " + selectedProfile.Name + "\r\n" +
                "• SafeSearch: " + (chkSafeSearch.Checked ? "Aktif" : "Nonaktif") + "\r\n" +
                "• Blokir Radikalisme & 18+: " + (chkDomainBlock.Checked ? "Aktif" : "Nonaktif") + "\r\n\r\n" +
                "Aplikasi dibuat oleh: Moch. Sugito S.T (Instruktur Ahli Pertama) | NIP : 199205262025061002\r\n\r\n" +
                "Tips: Tutup dan buka kembali browser pengguna untuk menerapkan perubahan.",
                "Proteksi Aktif - FamilyGuard", MessageBoxButtons.OK, MessageBoxIcon.Information
            );
        }

        private void DisableProtection()
        {
            LogMessage("--- Menonaktifkan Proteksi FamilyGuard ---");

            string dnsLog;
            DnsManager.ResetDnsToDefault(out dnsLog);
            LogMessage(dnsLog);

            string hostsLog;
            HostsManager.RemoveHostsProtection(out hostsLog);
            LogMessage(hostsLog);

            config.IsProtectionActive = false;
            ConfigManager.Save(config);

            UpdateProtectionStatusUI();

            MessageBox.Show(
                "Semua proteksi dan batasan telah dinonaktifkan.\r\nPengaturan DNS dan sistem telah dikembalikan seperti semula.\r\n\r\n" +
                "Aplikasi dibuat oleh: Moch. Sugito S.T (Instruktur Ahli Pertama) | NIP : 199205262025061002",
                "Proteksi Dinonaktifkan - FamilyGuard", MessageBoxButtons.OK, MessageBoxIcon.Information
            );
        }

        private void BtnApplySettings_Click(object sender, EventArgs e)
        {
            if (config.IsProtectionActive)
            {
                if (!CheckParentPin("mengubah pengaturan proteksi"))
                    return;

                // Re-apply if already active
                EnableProtection();
            }
            else
            {
                config.DnsProfileIndex = cbDnsProfile.SelectedIndex;
                config.EnableSafeSearch = chkSafeSearch.Checked;
                config.EnableDomainBlock = chkDomainBlock.Checked;
                ConfigManager.Save(config);
                MessageBox.Show("Pengaturan tersimpan. Klik 'Aktifkan Proteksi' untuk menerapkannya ke komputer.", "Pengaturan Disimpan", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnAddDomain_Click(object sender, EventArgs e)
        {
            string domain = HostsManager.CleanDomain(txtNewDomain.Text);
            if (string.IsNullOrEmpty(domain))
            {
                MessageBox.Show("Masukkan nama domain yang valid (misal: situsburuk.com)", "Domain Kosong", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!config.CustomDomains.Contains(domain))
            {
                config.CustomDomains.Add(domain);
                ConfigManager.Save(config);
                RefreshCustomDomainsList();
                txtNewDomain.Clear();

                if (config.IsProtectionActive)
                {
                    string log;
                    HostsManager.ApplyHostsProtection(config.EnableSafeSearch, config.EnableDomainBlock, config.CustomDomains, out log);
                    LogMessage("Domain '" + domain + "' ditambahkan dan langsung diblokir.");
                }
                else
                {
                    LogMessage("Domain '" + domain + "' ditambahkan ke daftar kustom.");
                }
            }
        }

        private void BtnRemoveDomain_Click(object sender, EventArgs e)
        {
            if (listCustomDomains.SelectedIndex >= 0)
            {
                if (!CheckParentPin("menghapus situs dari daftar blokir"))
                    return;

                string selected = (string)listCustomDomains.SelectedItem;
                config.CustomDomains.Remove(selected);
                ConfigManager.Save(config);
                RefreshCustomDomainsList();

                if (config.IsProtectionActive)
                {
                    string log;
                    HostsManager.ApplyHostsProtection(config.EnableSafeSearch, config.EnableDomainBlock, config.CustomDomains, out log);
                    LogMessage("Domain '" + selected + "' dihapus dari daftar blokir.");
                }
            }
        }

        private void BtnSetPin_Click(object sender, EventArgs e)
        {
            // If already set, verify old first
            if (!string.IsNullOrEmpty(config.PinHash))
            {
                if (!CheckParentPin("mengubah PIN orang tua"))
                    return;
            }

            using (PinDialog dlg = new PinDialog("Atur PIN Orang Tua Baru", "Masukkan 4 - 8 angka PIN baru:"))
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    string pin = dlg.EnteredPin.Trim();
                    if (pin.Length < 4)
                    {
                        MessageBox.Show("PIN minimal harus terdiri dari 4 karakter/angka.", "PIN Terlalu Pendek", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string salt = ConfigManager.GenerateSalt();
                    config.PinSalt = salt;
                    config.PinHash = ConfigManager.ComputeHash(pin, salt);
                    ConfigManager.Save(config);

                    UpdatePinStatusUI();
                    MessageBox.Show("PIN Orang Tua berhasil disimpan! Simpan baik-baik PIN Anda.", "PIN Berhasil Disetel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogMessage("PIN Orang Tua telah berhasil diperbarui.");
                }
            }
        }

        private void BtnRemovePin_Click(object sender, EventArgs e)
        {
            if (CheckParentPin("menghapus PIN orang tua"))
            {
                config.PinHash = "";
                config.PinSalt = "";
                ConfigManager.Save(config);
                UpdatePinStatusUI();
                MessageBox.Show("Kunci PIN telah dihapus. Aplikasi kini tidak meminta PIN.", "PIN Dihapus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LogMessage("Kunci PIN orang tua telah dinonaktifkan.");
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (config.IsProtectionActive && !string.IsNullOrEmpty(config.PinHash))
            {
                // Check PIN when user tries to close the app if protection is active
                if (!CheckParentPin("menutup aplikasi saat proteksi sedang aktif"))
                {
                    e.Cancel = true;
                }
            }
        }

        private void LogMessage(string msg)
        {
            if (rtbLog != null && !rtbLog.IsDisposed)
            {
                string time = DateTime.Now.ToString("HH:mm:ss");
                rtbLog.AppendText("[" + time + "] " + msg + "\r\n");
                rtbLog.SelectionStart = rtbLog.Text.Length;
                rtbLog.ScrollToCaret();
            }
        }

        private bool IsAdministrator()
        {
            try
            {
                WindowsIdentity identity = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch
            {
                return false;
            }
        }
    }
}
