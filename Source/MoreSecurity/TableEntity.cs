using UnityEngine;

namespace SquirtingElephant.Helpers;

public abstract class TableEntity
{
    protected readonly TableData TableData;

    public string Name = string.Empty;

    public TableEntity(TableData tableData)
    {
        TableData = tableData;
    }

    /// <summary>
    ///     Please do not edit this outside of TableData. This value is calculated.
    /// </summary>
    public Rect Rect { get; set; }
}