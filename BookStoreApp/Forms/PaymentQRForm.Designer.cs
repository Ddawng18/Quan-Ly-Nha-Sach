#nullable disable
using System.ComponentModel;

namespace BookStoreApp.Forms;

partial class PaymentQRForm
{
    private IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            components?.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        picQrCode = new PictureBox();
        lblProvider = new Label();
        lblOrderId = new Label();
        lblAmount = new Label();
        lblCountdown = new Label();
        lblStatus = new Label();
        btnCancel = new Button();
        btnRefresh = new Button();
        pnlInfo = new Panel();
        ((System.ComponentModel.ISupportInitialize)picQrCode).BeginInit();
        pnlInfo.SuspendLayout();
        SuspendLayout();

        // picQrCode
        picQrCode.BackColor = Color.White;
        picQrCode.BorderStyle = BorderStyle.FixedSingle;
        picQrCode.Location = new Point(24, 24);
        picQrCode.MinimumSize = new Size(250, 250);
        picQrCode.Size = new Size(250, 250);
        picQrCode.SizeMode = PictureBoxSizeMode.Zoom;

        // pnlInfo
        pnlInfo.Location = new Point(298, 24);
        pnlInfo.Size = new Size(280, 250);

        // lblProvider
        lblProvider.AutoSize = true;
        lblProvider.Font = new Font("Segoe UI", 11, FontStyle.Bold);
        lblProvider.Location = new Point(4, 4);
        lblProvider.Text = "Provider: Demo";

        // lblOrderId
        lblOrderId.AutoSize = true;
        lblOrderId.Font = new Font("Segoe UI", 10);
        lblOrderId.Location = new Point(4, 36);
        lblOrderId.Text = "Order: #--";

        // lblAmount
        lblAmount.AutoSize = true;
        lblAmount.Font = new Font("Segoe UI", 12, FontStyle.Bold);
        lblAmount.ForeColor = Color.FromArgb(33, 150, 243);
        lblAmount.Location = new Point(4, 68);
        lblAmount.Text = "0 ₫";

        // lblCountdown
        lblCountdown.AutoSize = true;
        lblCountdown.Font = new Font("Segoe UI", 10);
        lblCountdown.Location = new Point(4, 108);
        lblCountdown.Text = "Time remaining: 05:00";

        // lblStatus
        lblStatus.AutoSize = true;
        lblStatus.Font = new Font("Segoe UI", 11, FontStyle.Bold);
        lblStatus.Location = new Point(4, 148);
        lblStatus.Text = "Status: Waiting for payment...";

        // btnRefresh
        btnRefresh.Location = new Point(4, 196);
        btnRefresh.Size = new Size(120, 40);
        btnRefresh.Text = "Refresh QR";
        btnRefresh.UseVisualStyleBackColor = true;
        btnRefresh.Click += btnRefresh_Click;

        // btnCancel
        btnCancel.Location = new Point(140, 196);
        btnCancel.Size = new Size(120, 40);
        btnCancel.Text = "Cancel";
        btnCancel.UseVisualStyleBackColor = true;
        btnCancel.Click += btnCancel_Click;

        pnlInfo.Controls.Add(lblProvider);
        pnlInfo.Controls.Add(lblOrderId);
        pnlInfo.Controls.Add(lblAmount);
        pnlInfo.Controls.Add(lblCountdown);
        pnlInfo.Controls.Add(lblStatus);
        pnlInfo.Controls.Add(btnRefresh);
        pnlInfo.Controls.Add(btnCancel);

        // PaymentQRForm
        ClientSize = new Size(604, 306);
        Controls.Add(picQrCode);
        Controls.Add(pnlInfo);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "QR Payment";

        ((System.ComponentModel.ISupportInitialize)picQrCode).EndInit();
        pnlInfo.ResumeLayout(false);
        pnlInfo.PerformLayout();
        ResumeLayout(false);
    }

    private PictureBox picQrCode;
    private Label lblProvider;
    private Label lblOrderId;
    private Label lblAmount;
    private Label lblCountdown;
    private Label lblStatus;
    private Button btnCancel;
    private Button btnRefresh;
    private Panel pnlInfo;
}
