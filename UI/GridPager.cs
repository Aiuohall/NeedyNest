using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace NeedyNest.UI
{
    /// <summary>
    /// Adds client-side search + pagination to a <see cref="DataGridView"/> that is
    /// bound to a <see cref="DataTable"/>. Builds a small toolbar (search box +
    /// Prev/Next + page label) and shows one page of filtered rows at a time.
    ///
    /// Usage:
    ///     _pager = new GridPager(grid, dataTable, 15, hostPanel, "username", "role");
    /// </summary>
    internal sealed class GridPager
    {
        private readonly DataGridView _grid;
        private readonly DataTable    _source;
        private readonly int          _pageSize;
        private readonly string[]     _searchColumns;

        private readonly TextBox _search = new TextBox();
        private readonly Button  _prev   = new Button();
        private readonly Button  _next   = new Button();
        private readonly Label   _info   = new Label();

        private int _page;

        public GridPager(DataGridView grid, DataTable source, int pageSize, Control toolbarHost, params string[] searchColumns)
        {
            _grid          = grid;
            _source        = source;
            _pageSize      = Math.Max(1, pageSize);
            _searchColumns = searchColumns;

            BuildToolbar(toolbarHost);
            Apply();
        }

        private void BuildToolbar(Control host)
        {
            _search.Font = new Font("Segoe UI", 10F);
            _search.SetBounds(0, 4, 240, 28);
            _search.TextChanged += (s, e) => { _page = 0; Apply(); };

            var searchLabel = new Label
            {
                Text = "Search:", AutoSize = true, Location = new Point(0, 8),
                ForeColor = ThemeManager.ForegroundColor, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold)
            };
            _search.Left = searchLabel.Right + 8;

            _prev.Text = "‹ Prev"; _prev.Size = new Size(80, 30);
            _next.Text = "Next ›"; _next.Size = new Size(80, 30);
            ThemeManager.StyleButton(_prev);
            ThemeManager.StyleButton(_next);
            _prev.Click += (s, e) => { if (_page > 0) { _page--; Apply(); } };
            _next.Click += (s, e) => { if ((_page + 1) * _pageSize < FilteredRowCount()) { _page++; Apply(); } };

            _info.AutoSize = true;
            _info.Font = new Font("Segoe UI", 9F);
            _info.ForeColor = ThemeManager.SubtleText;

            // Lay the toolbar out and keep the page controls on the right edge.
            host.Controls.Add(searchLabel);
            host.Controls.Add(_search);
            host.Controls.Add(_prev);
            host.Controls.Add(_next);
            host.Controls.Add(_info);

            void Layout()
            {
                _next.Location = new Point(host.ClientSize.Width - _next.Width - 4, 4);
                _prev.Location = new Point(_next.Left - _prev.Width - 6, 4);
                _info.Location = new Point(_prev.Left - _info.Width - 12, 9);
            }
            host.Resize += (s, e) => Layout();
            Layout();
        }

        private DataRow[] FilteredRows()
        {
            string term = _search.Text.Trim();
            if (string.IsNullOrEmpty(term) || _searchColumns.Length == 0)
                return _source.Select();

            string safe = term.Replace("'", "''").Replace("[", "[[]").Replace("%", "[%]");
            string filter = string.Join(" OR ",
                _searchColumns.Select(c => $"CONVERT([{c}], 'System.String') LIKE '%{safe}%'"));
            try { return _source.Select(filter); }
            catch { return _source.Select(); }
        }

        private int FilteredRowCount() => FilteredRows().Length;

        private void Apply()
        {
            DataRow[] rows = FilteredRows();
            int total = rows.Length;
            int pages = Math.Max(1, (int)Math.Ceiling(total / (double)_pageSize));
            if (_page >= pages) _page = pages - 1;

            DataTable page = _source.Clone();
            foreach (var r in rows.Skip(_page * _pageSize).Take(_pageSize))
                page.ImportRow(r);

            _grid.DataSource = page;
            _info.Text = total == 0
                ? "No records"
                : $"Page {_page + 1} of {pages}  ({total} record{(total == 1 ? "" : "s")})";

            _prev.Enabled = _page > 0;
            _next.Enabled = (_page + 1) * _pageSize < total;
        }
    }
}
