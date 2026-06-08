using System.Text;
using BookStoreApp.DTO;

namespace BookStoreApp.Utilities;

public static class ReportExporter
{
    public static void ExportToCsv(ReportSectionDto section, string filePath)
    {
        var sb = new StringBuilder();
        sb.AppendLine(section.SectionName);
        sb.AppendLine(string.Join(",", section.Headers));
        foreach (var row in section.Rows)
        {
            sb.AppendLine(string.Join(",", row.Select(EscapeCsv)));
        }

        File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
    }

    public static void ExportToExcel(ReportSectionDto section, string filePath)
    {
        // Xuất HTML table để Excel mở trực tiếp (không cần thư viện bên thứ ba)
        var sb = new StringBuilder();
        sb.AppendLine("<html xmlns:o=\"urn:schemas-microsoft-com:office:office\" xmlns:x=\"urn:schemas-microsoft-com:office:excel\">");
        sb.AppendLine("<head><meta charset=\"UTF-8\"><style>");
        sb.AppendLine("table{border-collapse:collapse;width:100%;font-family:Segoe UI,sans-serif}");
        sb.AppendLine("th,td{border:1px solid #ccc;padding:8px;text-align:left}");
        sb.AppendLine("th{background:#4472c4;color:#fff}");
        sb.AppendLine("tr:nth-child(even){background:#f2f2f2}");
        sb.AppendLine("h2{color:#333}");
        sb.AppendLine("</style></head><body>");
        sb.AppendLine($"<h2>{EscapeHtml(section.SectionName)}</h2>");
        sb.AppendLine("<table>");

        sb.AppendLine("<tr>");
        foreach (var h in section.Headers)
        {
            sb.AppendLine($"<th>{EscapeHtml(h)}</th>");
        }
        sb.AppendLine("</tr>");

        foreach (var row in section.Rows)
        {
            sb.AppendLine("<tr>");
            foreach (var cell in row)
            {
                sb.AppendLine($"<td>{EscapeHtml(cell)}</td>");
            }
            sb.AppendLine("</tr>");
        }

        sb.AppendLine("</table></body></html>");
        File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
    }

    public static void ExportToPdf(ReportSectionDto section, string filePath)
    {
        var lines = new List<string>();
        var colCount = section.Headers.Count;
        var colWidths = new int[colCount];

        // Tính độ rộng cột dựa trên header và data
        for (var i = 0; i < colCount; i++)
        {
            colWidths[i] = section.Headers[i].Length;
            foreach (var row in section.Rows)
            {
                if (i < row.Count)
                    colWidths[i] = Math.Max(colWidths[i], row[i].Length);
            }
            colWidths[i] = Math.Min(colWidths[i] + 2, 40);
        }

        string Cell(string text, int width) => text.PadRight(width).Substring(0, Math.Min(text.Length, width)).PadRight(width);
        string Sep(char left, char mid, char right) => left + string.Join(mid, colWidths.Select(w => new string('─', w))) + right;

        lines.Add(Sep('┌', '┬', '┐'));
        lines.Add('│' + string.Join('│', section.Headers.Select((h, i) => Cell(h, colWidths[i]))) + '│');
        lines.Add(Sep('├', '┼', '┤'));

        foreach (var row in section.Rows)
        {
            var cells = new List<string>();
            for (var i = 0; i < colCount; i++)
            {
                var val = i < row.Count ? row[i] : "";
                cells.Add(Cell(val, colWidths[i]));
            }
            lines.Add('│' + string.Join('│', cells) + '│');
        }

        lines.Add(Sep('└', '┴', '┘'));

        var sb = new StringBuilder();
        sb.AppendLine($"BOOKSTORE REPORT ─ {section.SectionName}");
        sb.AppendLine($"Generated: {DateTime.Now:dd/MM/yyyy HH:mm}");
        sb.AppendLine();
        lines.ForEach(l => sb.AppendLine(l));

        File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
    }

    public static void ExportMultipleToCsv(IReadOnlyList<ReportSectionDto> sections, string filePath)
    {
        var sb = new StringBuilder();
        foreach (var section in sections)
        {
            sb.AppendLine(section.SectionName);
            sb.AppendLine(string.Join(",", section.Headers));
            foreach (var row in section.Rows)
            {
                sb.AppendLine(string.Join(",", row.Select(EscapeCsv)));
            }
            sb.AppendLine();
        }

        File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
    }

    private static string EscapeCsv(string value)
    {
        if (value.Contains(',') || value.Contains('"'))
        {
            return $"\"{value.Replace("\"", "\"\"")}\"";
        }

        return value;
    }

    private static string EscapeHtml(string value)
    {
        return value
            .Replace("&", "&amp;")
            .Replace("<", "&lt;")
            .Replace(">", "&gt;")
            .Replace("\"", "&quot;");
    }
}
