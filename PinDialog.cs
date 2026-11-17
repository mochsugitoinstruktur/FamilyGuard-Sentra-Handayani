using System;
using System.Drawing;
using System.Windows.Forms;

namespace FamilyGuard
{
    public class PinDialog : Form
    {
        private TextBox txtPin;
        private Button btnOk;
        private Button btnCancel;
        private Label lblTitle;
        private Label lblPrompt;
        private Panel pnlAuthorBox;
        private Label lblAuthorName;
        private Label lblAuthorNip;

        public string EnteredPin
        {
            get { return txtPin.Text; }
        }

        public PinDialog(string title, string prompt)
        {
            InitializeComponent(title, prompt);
        }

        private void InitializeComponent(string title, string prompt)
        {
            this.Text = title + " - Moch. Sugito S.T [NIP: 199205262025061002]";
            this.Size = new Size(400, 260);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 9.5f, FontStyle.Regular);

            lblTitle = new Label();
            lblTitle.Text = title;
            lblTitle.Font = new Font("Segoe UI", 11f, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(33, 43, 54);
            lblTitle.Location = new Point(20, 14);
            lblTitle.AutoSize = true;

            lblPrompt = new Label();
            lblPrompt.Text = prompt;
            lblPrompt.ForeColor = Color.FromArgb(70, 80, 95);
            lblPrompt.Location = new Point(20, 42);
            lblPrompt.Size = new Size(345, 36);

            txtPin = new TextBox();
            txtPin.PasswordChar = '●';
            txtPin.Font = new Font("Segoe UI", 12f, FontStyle.Bold);
            txtPin.TextAlign = HorizontalAlignment.Center;
            txtPin.Location = new Point(24, 80);
            txtPin.Size = new Size(336, 29);
            txtPin.MaxLength = 16;
            txtPin.KeyDown += TxtPin_KeyDown;

            btnOk = new Button();
            btnOk.Text = "Konfirmasi";
            btnOk.DialogResult = DialogResult.OK;
            btnOk.Location = new Point(164, 118);
            btnOk.Size = new Size(95, 30);
            btnOk.BackColor = Color.FromArgb(30, 136, 229);
            btnOk.ForeColor = Color.White;
            btnOk.FlatStyle = FlatStyle.Flat;
            btnOk.FlatAppearance.BorderSize = 0;

            btnCancel = new Button();
            btnCancel.Text = "Batal";
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(265, 118);
            btnCancel.Size = new Size(95, 30);
            btnCancel.BackColor = Color.FromArgb(240, 240, 240);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.FlatAppearance.BorderSize = 0;

            // Box Pembuat di Halaman Dialog PIN
            pnlAuthorBox = new Panel();
            pnlAuthorBox.Location = new Point(16, 160);
            pnlAuthorBox.Size = new Size(352, 46);
            pnlAuthorBox.BackColor = Color.FromArgb(241, 245, 249);
            pnlAuthorBox.BorderStyle = BorderStyle.FixedSingle;

            lblAuthorName = new Label();
            lblAuthorName.Text = "Moch. Sugito S.T (Instruktur Ahli Pertama)";
            lblAuthorName.Font = new Font("Segoe UI", 8.5f, FontStyle.Bold);
            lblAuthorName.ForeColor = Color.FromArgb(15, 23, 42);
            lblAuthorName.Location = new Point(6, 4);
            lblAuthorName.Size = new Size(340, 18);
            lblAuthorName.TextAlign = ContentAlignment.MiddleCenter;

            lblAuthorNip = new Label();
            lblAuthorNip.Text = "NIP : 199205262025061002";
            lblAuthorNip.Font = new Font("Segoe UI", 8.2f, FontStyle.Regular);
            lblAuthorNip.ForeColor = Color.FromArgb(71, 85, 105);
            lblAuthorNip.Location = new Point(6, 22);
            lblAuthorNip.Size = new Size(340, 18);
            lblAuthorNip.TextAlign = ContentAlignment.MiddleCenter;

            pnlAuthorBox.Controls.Add(lblAuthorName);
            pnlAuthorBox.Controls.Add(lblAuthorNip);

            this.Controls.Add(lblTitle);
            this.Controls.Add(lblPrompt);
            this.Controls.Add(txtPin);
            this.Controls.Add(btnOk);
            this.Controls.Add(btnCancel);
            this.Controls.Add(pnlAuthorBox);

            this.AcceptButton = btnOk;
            this.CancelButton = btnCancel;
        }

        private void TxtPin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
