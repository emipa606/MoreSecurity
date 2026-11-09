using Verse;

namespace SquirtingElephant.Helpers;

public class TableColumn : TableEntity
{
    public TableColumn(TableData tableData, float width) : base(tableData)
    {
        Width = width;
    }

    public float Width
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
}