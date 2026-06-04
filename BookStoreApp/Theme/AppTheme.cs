namespace BookStoreApp.Theme;

public static class AppTheme
{
    public static Color Sidebar { get; } = Color.FromArgb(0x1F, 0x29, 0x37);
    public static Color ActiveMenu { get; } = Color.FromArgb(0x25, 0x63, 0xEB);
    public static Color MainBackground { get; } = Color.FromArgb(0xF3, 0xF4, 0xF6);
    public static Color GridHeader { get; } = Color.FromArgb(0x25, 0x63, 0xEB);
    public static Color GridSelected { get; } = Color.FromArgb(0xDB, 0xEA, 0xFE);
    public static Color Add { get; } = Color.FromArgb(0x25, 0x63, 0xEB);
    public static Color Edit { get; } = Color.FromArgb(0xF5, 0x9E, 0x0B);
    public static Color Delete { get; } = Color.FromArgb(0xDC, 0x26, 0x26);

    public static void StyleActionButton(Button button, Color backColor, Color? foreColor = null)
    {
        button.FlatStyle = FlatStyle.Flat;
        button.FlatAppearance.BorderSize = 0;
        button.BackColor = backColor;
        button.ForeColor = foreColor ?? Color.White;
        button.UseVisualStyleBackColor = false;
    }

    public static void StyleRefreshButton(Button button)
    {
        button.FlatStyle = FlatStyle.Flat;
        button.FlatAppearance.BorderSize = 1;
        button.FlatAppearance.BorderColor = Color.FromArgb(0xD1, 0xD5, 0xDB);
        button.BackColor = Color.White;
        button.ForeColor = Color.FromArgb(0x11, 0x18, 0x27);
        button.UseVisualStyleBackColor = false;
    }

    public static void StyleSidebarButton(Button button, bool isActive)
    {
        button.FlatStyle = FlatStyle.Flat;
        button.FlatAppearance.BorderSize = 0;
        button.BackColor = isActive ? ActiveMenu : Sidebar;
        button.ForeColor = Color.White;
        button.UseVisualStyleBackColor = false;
    }

    public static void ApplyGridStyle(DataGridView grid)
    {
        grid.BackgroundColor = Color.White;
        grid.BorderStyle = BorderStyle.FixedSingle;
        grid.EnableHeadersVisualStyles = false;
        grid.ColumnHeadersDefaultCellStyle.BackColor = GridHeader;
        grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        grid.ColumnHeadersDefaultCellStyle.Font = new Font(grid.Font, FontStyle.Bold);
        grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = GridHeader;
        grid.DefaultCellStyle.SelectionBackColor = GridSelected;
        grid.DefaultCellStyle.SelectionForeColor = Color.Black;
        grid.RowHeadersDefaultCellStyle.SelectionBackColor = GridSelected;
    }
}
