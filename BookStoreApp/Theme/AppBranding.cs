namespace BookStoreApp.Theme;

public static class AppBranding
{
    private static Image? _logo;
    private static Icon? _icon;

    public static Image Logo => _logo ??= LoadLogo();

    public static Icon AppIcon => _icon ??= CreateIcon();

    public static string LogoPath =>
        Path.Combine(AppContext.BaseDirectory, "Assets", "app-logo.png");

    private static Image LoadLogo()
    {
        if (!File.Exists(LogoPath))
        {
            throw new FileNotFoundException("App logo not found.", LogoPath);
        }

        return Image.FromFile(LogoPath);
    }

    private static Icon CreateIcon()
    {
        using var source = new Bitmap(LogoPath);
        using var sized = new Bitmap(source, new Size(64, 64));
        return Icon.FromHandle(sized.GetHicon());
    }

    public static void ApplyFormIcon(Form form)
    {
        form.Icon = (Icon)AppIcon.Clone();
    }
}
