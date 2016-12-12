using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellRelativePos : MonoBehaviour
{

    #region Attributes
    public Cell cell;
    public Direction direction;
    #endregion

    #region Enums
    public enum Direction
    {
        North,
        South,
        East,
        West
    }
    #endregion

    #region Constructor
    public CellRelativePos(Cell cell, Direction direction)
    {
        this.cell = cell;
        this.direction = direction;
    }
    #endregion
}
