using UnityEngine;

namespace SquirtingElephant.Helpers
{
    public class TableField : TableEntity
    {
        public TableField(TableData tableData, TableColumn column, TableRow row) : base(tableData)
        {
            Column = column;
            Row = row;
            Update();
        }

        public TableColumn Column { get; }

        public TableRow Row { get; }

        public static TableField Invalid => new TableField(null, null, null);

        public void Update()
        {
            Rect = new Rect(Column.Rect.x, Row.Rect.y, Column.Rect.width, Row.Rect.height);
        }
    }
}