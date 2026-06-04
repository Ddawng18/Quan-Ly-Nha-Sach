namespace BookStoreApp.Forms;

partial class LoginForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        panelHeader = new Panel();
        tableHeader = new TableLayoutPanel();
        picLogo = new PictureBox();
        lblBrand = new Label();
        lblUsername = new Label();
        txtUsername = new TextBox();
        lblPassword = new Label();
        txtPassword = new TextBox();
        btnLogin = new Button();
        panelHeader.SuspendLayout();
        tableHeader.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)picLogo).BeginInit();
        SuspendLayout();
        // 
        // panelHeader
        // 
        panelHeader.BackColor = Color.FromArgb(249, 250, 251);
        panelHeader.Controls.Add(tableHeader);
        panelHeader.Dock = DockStyle.Top;
        panelHeader.Location = new Point(0, 0);
        panelHeader.Name = "panelHeader";
        panelHeader.Padding = new Padding(24, 20, 24, 16);
        panelHeader.Size = new Size(464, 120);
        panelHeader.TabIndex = 5;
        // 
        // tableHeader
        // 
        tableHeader.Anchor = AnchorStyles.None;
        tableHeader.AutoSize = true;
        tableHeader.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        tableHeader.ColumnCount = 2;
        tableHeader.ColumnStyles.Add(new ColumnStyle());
        tableHeader.ColumnStyles.Add(new ColumnStyle());
        tableHeader.Controls.Add(picLogo, 0, 0);
        tableHeader.Controls.Add(lblBrand, 1, 0);
        tableHeader.Location = new Point(0, 0);
        tableHeader.Name = "tableHeader";
        tableHeader.RowCount = 1;
        tableHeader.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableHeader.Size = new Size(393, 80);
        tableHeader.TabIndex = 0;
        // 
        // picLogo
        // 
        picLogo.Location = new Point(0, 0);
        picLogo.Margin = new Padding(0, 0, 16, 0);
        picLogo.Name = "picLogo";
        picLogo.Size = new Size(80, 80);
        picLogo.SizeMode = PictureBoxSizeMode.Zoom;
        picLogo.TabIndex = 0;
        picLogo.TabStop = false;
        // 
        // lblBrand
        // 
        lblBrand.Anchor = AnchorStyles.Left;
        lblBrand.AutoSize = true;
        lblBrand.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
        lblBrand.ForeColor = Color.FromArgb(45, 106, 106);
        lblBrand.Location = new Point(96, 25);
        lblBrand.Margin = new Padding(0, 20, 0, 0);
        lblBrand.Name = "lblBrand";
        lblBrand.Size = new Size(297, 50);
        lblBrand.TabIndex = 1;
        lblBrand.Text = "bookstoreMana";
        lblBrand.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblUsername
        // 
        lblUsername.AutoSize = true;
        lblUsername.Font = new Font("Segoe UI", 9F);
        lblUsername.Location = new Point(48, 136);
        lblUsername.Name = "lblUsername";
        lblUsername.Size = new Size(75, 20);
        lblUsername.TabIndex = 4;
        lblUsername.Text = "Username";
        // 
        // txtUsername
        // 
        txtUsername.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
        txtUsername.Location = new Point(48, 160);
        txtUsername.Name = "txtUsername";
        txtUsername.Size = new Size(368, 27);
        txtUsername.TabIndex = 3;
        // 
        // lblPassword
        // 
        lblPassword.AutoSize = true;
        lblPassword.Font = new Font("Segoe UI", 9F);
        lblPassword.Location = new Point(48, 206);
        lblPassword.Name = "lblPassword";
        lblPassword.Size = new Size(70, 20);
        lblPassword.TabIndex = 2;
        lblPassword.Text = "Password";
        // 
        // txtPassword
        // 
        txtPassword.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
        txtPassword.Location = new Point(48, 230);
        txtPassword.Name = "txtPassword";
        txtPassword.Size = new Size(368, 27);
        txtPassword.TabIndex = 1;
        txtPassword.UseSystemPasswordChar = true;
        // 
        // btnLogin
        // 
        btnLogin.BackColor = Color.FromArgb(37, 99, 235);
        btnLogin.FlatAppearance.BorderSize = 0;
        btnLogin.FlatStyle = FlatStyle.Flat;
        btnLogin.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnLogin.ForeColor = Color.White;
        btnLogin.Location = new Point(48, 284);
        btnLogin.Name = "btnLogin";
        btnLogin.Size = new Size(368, 40);
        btnLogin.TabIndex = 0;
        btnLogin.Text = "Login";
        btnLogin.UseVisualStyleBackColor = false;
        btnLogin.Click += btnLogin_Click;
        // 
        // LoginForm
        // 
        AcceptButton = btnLogin;
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.White;
        ClientSize = new Size(464, 352);
        Controls.Add(btnLogin);
        Controls.Add(txtPassword);
        Controls.Add(lblPassword);
        Controls.Add(txtUsername);
        Controls.Add(lblUsername);
        Controls.Add(panelHeader);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "LoginForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "bookstoreMana — Login";
        panelHeader.ResumeLayout(false);
        panelHeader.PerformLayout();
        tableHeader.ResumeLayout(false);
        tableHeader.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    private Panel panelHeader;
    private TableLayoutPanel tableHeader;
    private PictureBox picLogo;
    private Label lblBrand;
    private Label lblUsername;
    private Label lblPassword;
    private TextBox txtUsername;
    private TextBox txtPassword;
    private Button btnLogin;
}
