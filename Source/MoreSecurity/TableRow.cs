using System.Collections.Generic;
using Verse;

namespace SquirtingElephant.Helpers;

public class TableRow : TableEntity
{
    public TableRow(TableData tableData, float height) : base(tableData)
    {
        Height = height;
        SetFields(tableData);
    }

    public List<TableField> Fields { get; } = [];

    public float Height
    {
        get;
        private set
        {
            if (value == field)
            {
                return;
            }

            if (value > 0f)
            {
                field = value;
                TableData.Update();
            }
            else
            {
                Log.Error($"TableRow received a value of {value} for its Height.");
            }
        }
    }

    private void SetFields(TableData tableData)
    {
        Fields.Clear();
        tableData.Columns.ForEach(c => Fields.Add(new TableField(tableData, c, this)));
    }

    public void UpdateFields()
    {
        Fields.ForEach(f => f.Update());
    }
}