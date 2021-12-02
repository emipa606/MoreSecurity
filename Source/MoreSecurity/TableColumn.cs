using Verse;

namespace SquirtingElephant.Helpers;

public class TableColumn : TableEntity
{
    private float _Width;

    public TableColumn(TableData tableData, float width) : base(tableData)
    {
        Width = width;
    }

    public float Width
    {
        get => _Width;
        set
        {
            if (value == _Width)
            {
                return;
            }

            if (value > 0f)
            {
                _Width = value;
                TableData.Update();
            }
            else
            {
                Log.Error($"TableRow received a value of {value} for its Height.");
            }
        }
    }
}