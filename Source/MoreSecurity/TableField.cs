using UnityEngine;

namespace SquirtingElephant.Helpers;

public class TableField : TableEntity
{
    public TableField(TableData tableData, TableColumn column, TableRow row) : base(tableData)
    {
        Column = column;
        Row = row;
        Update();
    }

    private TableColumn Column { get; }

    private TableRow Row { get; }

    public static TableField Invalid => new(null, null, null);

    public void Update()
    {
        Rect = new Rect(Column.Rect.x, Row.Rect.y, Column.Rect.width, Row.Rect.height);
    }
}