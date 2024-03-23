using UnityEngine;

namespace SquirtingElephant.Helpers;

public abstract class TableEntity(TableData tableData)
{
    protected readonly TableData TableData = tableData;

    public string Name = string.Empty;

    /// <summary>
    ///     Please do not edit this outside of TableData. This value is calculated.
    /// </summary>
    public Rect Rect { get; set; }
}