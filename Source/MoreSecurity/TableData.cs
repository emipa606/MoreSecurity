using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace SquirtingElephant.Helpers
{
    public class TableData
    {
        private const float DEFAULT_ROW_HEIGHT = 32f;

        private Vector2 _Spacing;

        private Vector2 _TableOffset;

        /// <summary>
        ///     Used privately within methods to temporarily disable the updating without changing the UpdateEnabled setting.
        ///     Please set this value back to true at the end of your method or whatever you are doing.
        /// </summary>
        private bool PrivateUpdateEnabled = true;

        public bool UpdateEnabled = true;

        /// <summary>
        /// </summary>
        /// <param name="tableOffset"></param>
        /// <param name="spacing"></param>
        /// <param name="colWidths"></param>
        /// <param name="rowHeights"></param>
        /// <param name="colCount"></param>
        /// <param name="rowCount">Note: A header-row also counts as 1 rowcount.</param>
        public TableData(Vector2 tableOffset, Vector2 spacing, float[] colWidths, float[] rowHeights, int colCount = -1,
            int rowCount = -1)
        {
            Initialize(tableOffset, spacing, colWidths, rowHeights, colCount, rowCount);
        }

        public float Bottom => TableRect.yMax;

        public Vector2 Spacing
        {
            get => _Spacing;
            set
            {
                if (value == _Spacing)
                {
                    return;
                }

                _Spacing = value;
                Update();
            }
        }

        public Vector2 TableOffset
        {
            get => _TableOffset;
            set
            {
                if (value == _TableOffset)
                {
                    return;
                }

                _TableOffset = value;
                Update();
            }
        }

        /// <summary>
        ///     Column Datas.
        /// </summary>
        public List<TableColumn> Columns { get; } = new List<TableColumn>();

        public List<TableRow> Rows { get; } = new List<TableRow>();

        public Rect TableRect { get; private set; } = Rect.zero;

        /// <summary>
        /// </summary>
        /// <param name="tableOffset"></param>
        /// <param name="tableSpacing"></param>
        /// <param name="colWidths"></param>
        /// <param name="rowHeights"></param>
        /// <param name="colCount">
        ///     If this value is greater than <c>colWidths</c> then the last <c>colWidths</c> will be used for
        ///     the extra columns.
        /// </param>
        /// <param name="rowCount">
        ///     If this value is greater than <c>rowHeights</c> then the last <c>rowHeights</c> will be used for
        ///     the extra rows.
        /// </param>
        private void Initialize(Vector2 tableOffset, Vector2 tableSpacing, float[] colWidths, float[] rowHeights,
            int colCount = -1, int rowCount = -1)
        {
            // For performance reasons disable the updating and update after the table initialization instead.
            PrivateUpdateEnabled = false;

            _TableOffset = tableOffset;
            _Spacing = tableSpacing;

            // Add Columns.
            foreach (var colWidth in colWidths)
            {
                AddColumns(colWidth);
            }

            AddColumns(colWidths.Last(), colCount - colWidths.Length);

            // Add Rows.
            foreach (var rowHeight in rowHeights)
            {
                AddRow(rowHeight);
            }

            AddRow(GetLastRowHeight(), rowCount - Rows.Count);

            PrivateUpdateEnabled = true;
            Update();
        }

        public void Update(bool force = false)
        {
            if ((!UpdateEnabled || !PrivateUpdateEnabled) && !force)
            {
                return;
            }

            SetTableRect();
            UpdateColumns();
            UpdateRowsAndFields();
        }

        private float CalcTableWidth()
        {
            if (Columns.Count == 0)
            {
                return 0f;
            }

            var result = Columns[0].Width;
            for (var i = 1; i < Columns.Count; i++)
            {
                result += Columns[i].Width + Spacing.x;
            }

            return result;
        }

        private float CalcTableHeight()
        {
            if (Rows.Count == 0)
            {
                return 0f;
            }

            var result = Rows[0].Height;
            for (var i = 1; i < Rows.Count; i++)
            {
                result += Rows[i].Height + Spacing.y;
            }

            return result;
        }

        private void SetTableRect()
        {
            TableRect = new Rect(TableOffset.x, TableOffset.y, CalcTableWidth(), CalcTableHeight());
        }

        /// <summary>
        ///     Please also call UpdateRowsAndFields() after changing any column through here.
        /// </summary>
        public void UpdateColumns()
        {
            var nextColStart_X = TableRect.x;
            foreach (var col in Columns)
            {
                col.Rect = new Rect(nextColStart_X, TableRect.y, col.Width, TableRect.height);
                nextColStart_X = col.Rect.xMax + Spacing.x;
            }
        }

        public void UpdateRowsAndFields()
        {
            var nextRowStart_Y = TableRect.y;
            foreach (var row in Rows)
            {
                row.Rect = new Rect(TableRect.x, nextRowStart_Y, TableRect.width, row.Height);
                nextRowStart_Y = row.Rect.yMax + Spacing.y;
                row.UpdateFields();
            }
        }

        /// <summary>
        ///     Note: Will do nothing if <c>amount</c> is zero or less.
        /// </summary>
        public void AddRow(float rowHeight, int amount = 1)
        {
            if (amount == 0)
            {
                return;
            }

            PrivateUpdateEnabled = false;
            for (var i = 0; i < amount; i++)
            {
                Rows.Add(new TableRow(this, rowHeight));
            }

            PrivateUpdateEnabled = true;
            Update();
        }

        /// <summary>
        ///     Note: Will do nothing if <c>amount</c> is zero or less.
        /// </summary>
        private void AddColumns(float colWidth, float amount = 1)
        {
            if (amount == 0)
            {
                return;
            }

            PrivateUpdateEnabled = false;
            for (var i = 0; i < amount; i++)
            {
                Columns.Add(new TableColumn(this, colWidth));
            }

            PrivateUpdateEnabled = true;
            Update();
        }

        private void CreateRowsUntil(int rowIdx)
        {
            AddRow(GetLastRowHeight(), rowIdx + 1 - Rows.Count);
        }

        public TableField GetField(int colIdx, int rowIdx)
        {
            if (colIdx >= Columns.Count)
            {
                Log.Error($"Attemped to access a column that's out of bounds. Received: {colIdx}.");
                return TableField.Invalid;
            }

            CreateRowsUntil(rowIdx);
            return Rows[rowIdx].Fields[colIdx];
        }

        private float GetLastRowHeight()
        {
            return Rows.Count > 0 ? Rows.Last().Height : DEFAULT_ROW_HEIGHT;
        }

        public Rect GetRowRect(int rowIdx)
        {
            CreateRowsUntil(rowIdx);
            return Rows[rowIdx].Rect;
        }

        public Rect GetHeaderRect(int colIdx)
        {
            return GetField(colIdx, 0).Rect;
        }

        public Rect GetFieldRect(int colIdx, int rowIdx)
        {
            return GetField(colIdx, rowIdx).Rect;
        }

        /// <summary>
        ///     Will highlight the entire table-row if the mouse is over it. Call this somewhere in DoWindowContents() while a
        ///     Listing_Standard is active.
        /// </summary>
        /// <param name="rowIdx">The current row index to apply this to.</param>
        public void ApplyMouseOverEntireRow(int rowIdx)
        {
            var rowRect = GetRowRect(rowIdx);
            if (Mouse.IsOver(rowRect))
            {
                Widgets.DrawHighlight(rowRect);
            }
        }

#if DEBUG
        private static Texture2D TableRectTexure;
        private static Texture2D ColTexure;
        private static Texture2D RowTexure;
        private static Texture2D FieldTexture;

        /// <summary>
        ///     Notes:
        ///     1. Drawing the Rows bugs (they are not wide enough? Why?)
        ///     2. Sometimes Rimworld complains about more calls to BeginScrollView() than to EndScrollView(). Why? I have no idea
        ///     but it may happen when adding Widgets through a <T> method.
        /// </summary>
        public void DrawTableDebug()
        {
            if (TableRect.width == 0f || TableRect.height == 0f)
            {
                Log.Error("The TableRect its width and/or height are zero.");
                return;
            }

            // Initialize textures.
            var texWidth = (int) TableRect.width;
            var texHeight = (int) TableRect.height;
            Utils.SetupTextureAndColorIt(ref TableRectTexure, texWidth, texHeight, Color.black);
            Utils.SetupTextureAndColorIt(ref ColTexure, texWidth, texHeight, Color.blue);
            Utils.SetupTextureAndColorIt(ref RowTexure, texWidth, texHeight, Color.yellow);
            Utils.SetupTextureAndColorIt(ref FieldTexture, texWidth, texHeight, Color.red);

            // Draw TableRect.
            Widgets.Label(TableRect, new GUIContent(TableRectTexure));
            // Draw all columns.
            Columns.ForEach(c => Widgets.Label(c.Rect, new GUIContent(ColTexure)));
            foreach (var row in Rows)
            {
                // Draw the rows.
                Widgets.Label(row.Rect, new GUIContent(RowTexure));
                // Draw the fields of this row.
                row.Fields.ForEach(f => Widgets.Label(f.Rect, new GUIContent(FieldTexture)));
            }
        }

        public void LogDebugData()
        {
            var rowLocs = string.Empty;
            Rows.ForEach(r => rowLocs += r.Rect + "  ");
            Log.Message(
                $"Table Debug. Table Rect: {TableRect.ToString()}, colCnt: {Columns.Count.ToString()}, rowCnt: {Rows.Count.ToString()}, rowLocs: {rowLocs}");
        }
#endif
    }
}