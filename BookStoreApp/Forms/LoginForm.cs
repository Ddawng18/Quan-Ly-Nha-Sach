using BookStoreApp.BLL;
using BookStoreApp.Theme;

namespace BookStoreApp.Forms;

public partial class LoginForm : Form
{
    private readonly IAuthService _authService = ServiceLocator.AuthService;

    public LoginForm()
    {
        InitializeComponent();
        ApplyBranding();
        CenterHeader();
        panelHeader.Resize += (_, _) => CenterHeader();
    }

    private void ApplyBranding()
    {
        AppBranding.ApplyFormIcon(this);
        picLogo.Image = AppBranding.Logo;
    }

    private void CenterHeader()
    {
        tableHeader.Location = new Point(
            Math.Max(0, (panelHeader.ClientSize.Width - tableHeader.Width) / 2),
            Math.Max(0, (panelHeader.ClientSize.Height - tableHeader.Height) / 2));
    }

    private void btnLogin_Click(object sender, EventArgs e)
    {
        var account = _authService.Login(txtUsername.Text, txtPassword.Text);
        if (account is null)
        {
            MessageBox.Show("Invalid username or password.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var mainForm = new MainForm(account.Role);
        mainForm.FormClosed += (_, _) => Close();
        Hide();
        mainForm.Show();
    }
}
