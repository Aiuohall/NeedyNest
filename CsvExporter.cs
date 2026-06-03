using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace NeedyNest
{
    /// <summary>
    /// Exports the rows currently shown in a <see cref="DataGridView"/> to a CSV
    /// file (which opens directly in Excel). Dependency-free.
    /// </summary>
    internal static class CsvExporter
    {
        public static void Export(DataGridView grid, string suggestedName)
        {
            if (grid == null || grid.Rows.Count == 0)
            {
                MessageBox.Show("There is nothing to export.", "Export",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var dlg = new SaveFileDialog
            {
                Filter = "CSV file (*.csv)|*.csv",
                FileName = $"{suggestedName}_{DateTime.Now:yyyyMMdd_HHmmss}.csv"
            })
            {
                if (dlg.ShowDialog() != DialogResult.OK) return;

                try
                {
                    var sb = new StringBuilder();

                    // Header row (visible columns only)
                    bool first = true;
                    foreach (DataGridViewColumn col in grid.Columns)
                    {
                        if (!col.Visible) continue;
                        if (!first) sb.Append(',');
                        sb.Append(Escape(col.HeaderText));
                        first = false;
                    }
                    sb.AppendLine();

                    // Data rows
                    foreach (DataGridViewRow row in grid.Rows)
                    {
                        if (row.IsNewRow) continue;
                        first = true;
                        foreach (DataGridViewColumn col in grid.Columns)
                        {
                            if (!col.Visible) continue;
                            if (!first) sb.Append(',');
                            sb.Append(Escape(row.Cells[col.Index].Value?.ToString() ?? ""));
                            first = false;
                        }
                        sb.AppendLine();
                    }

                    File.WriteAllText(dlg.FileName, sb.ToString(), Encoding.UTF8);
                    MessageBox.Show("Exported successfully to:\n" + dlg.FileName, "Export",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    Logger.Log(ex, "CsvExporter");
                    MessageBox.Show("Export failed: " + ex.Message, "Export",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private static string Escape(string value)
        {
            if (value.Contains(",") || value.Contains("\"") || value.Contains("\n") || value.Contains("\r"))
                return "\"" + value.Replace("\"", "\"\"") + "\"";
            return value;
        }
    }
}
