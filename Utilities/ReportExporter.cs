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

    public static void ExportToPdf(ReportSectionDto section, string filePath)
    {
        var lines = new List<string>
        {
            "Book Store Report",
            section.SectionName,
            string.Empty,
            string.Join(" | ", section.Headers)
        };
        lines.AddRange(section.Rows.Select(r => string.Join(" | ", r)));
        File.WriteAllLines(filePath, lines, Encoding.UTF8);
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
}
