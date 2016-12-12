public class Cell
{
    #region Attributes
    public bool _West, _North, _East, _South;
    public bool _visited;

    public int xPos, zPos;
    #endregion

    #region Constructor
    public Cell(bool west, bool north, bool east, bool south, bool visited)
    {
        this._West = west;
        this._North = north;
        this._East = east;
        this._South = south;
        this._visited = visited;
    }
    #endregion

}
