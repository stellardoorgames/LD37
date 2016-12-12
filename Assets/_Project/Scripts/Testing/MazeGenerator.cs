using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MazeGenerator : MonoBehaviour
{
    #region Attributes
    public int _width, _height;

    [SerializeField]
    public double chance;

    public VisualCell visualCellPrefab;

    public Cell[,] cells;


    List<string> Sections = new List<string>();

    private Vector2 _randomCellPos;
    private VisualCell visualCellInst;

    private List<CellRelativePos> neighbors;
    #endregion

    #region Methods
    void Start()
    {
        cells = new Cell[_width, _height];
        Init();
        chance = 0.67;
    }

    void Init()
    {
        for (int i = 0; i < _width; i++)
        {

            for (int j = 0; j < _height; j++)
            {

                cells[i, j] = new Cell(false, false, false, false, false);
                cells[i, j].xPos = i;
                cells[i, j].zPos = j;
            }
        }
        RandomCell();

        InitVisualCell();


        // Clear center for tentacle spawn
        Sections.Add("5_5");
        Sections.Add("5_6");
        Sections.Add("6_5");
        Sections.Add("6_6");
        foreach (string section in Sections)
        {
            GameObject var = GameObject.Find(section);
            var.transform.Find("North").gameObject.SetActive(false);
            var.transform.Find("South").gameObject.SetActive(false);
            var.transform.Find("East").gameObject.SetActive(false);
            var.transform.Find("West").gameObject.SetActive(false);
            //visualCellInst._North.gameObject.SetActive(false);
            //visualCellInst._South.gameObject.SetActive(false);
            //visualCellInst._East.gameObject.SetActive(false);
            //visualCellInst._West.gameObject.SetActive(false);
        }

        GameObject var1 = GameObject.Find("6_4");
        //var1.transform.Find("North").gameObject.SetActive(false);
        var1.transform.Find("South").gameObject.SetActive(false);
        var1.transform.Find("East").gameObject.SetActive(false);
        var1.transform.Find("West").gameObject.SetActive(false);
        var1 = GameObject.Find("7_5");
        var1.transform.Find("North").gameObject.SetActive(false);
        var1.transform.Find("South").gameObject.SetActive(false);
        //var1.transform.Find("East").gameObject.SetActive(false);
        var1.transform.Find("West").gameObject.SetActive(false);
        var1 = GameObject.Find("7_6");
        var1.transform.Find("North").gameObject.SetActive(false);
        var1.transform.Find("South").gameObject.SetActive(false);
        //var1.transform.Find("East").gameObject.SetActive(false);
        var1.transform.Find("West").gameObject.SetActive(false);
        var1 = GameObject.Find("6_7");
        var1.transform.Find("North").gameObject.SetActive(false);
        //var1.transform.Find("South").gameObject.SetActive(false);
        //var1.transform.Find("East").gameObject.SetActive(false);
        //var1.transform.Find("West").gameObject.SetActive(false);
        var1 = GameObject.Find("5_7");
        var1.transform.Find("North").gameObject.SetActive(false);
        //var1.transform.Find("South").gameObject.SetActive(false);
        //var1.transform.Find("East").gameObject.SetActive(false);
        //var1.transform.Find("West").gameObject.SetActive(false);
    }

    void RandomCell()
    {
        _randomCellPos = new Vector2((int)UnityEngine.Random.Range(0, _width), (int)UnityEngine.Random.Range(0, _height));

        GenerateMaze((int)_randomCellPos.x, (int)_randomCellPos.y);
    }

    void GenerateMaze(int x, int y)
    {
        //	Debug.Log("Doing " + x + " " + y);
        Cell currentCell = cells[x, y];
        neighbors = new List<CellRelativePos>();
        if (currentCell._visited == true) return;
        currentCell._visited = true;

        if (x + 1 < _width && cells[x + 1, y]._visited == false)
        {
            neighbors.Add(new CellRelativePos(cells[x + 1, y], CellRelativePos.Direction.East));
        }

        if (y + 1 < _height && cells[x, y + 1]._visited == false)
        {
            neighbors.Add(new CellRelativePos(cells[x, y + 1], CellRelativePos.Direction.South));
        }

        if (x - 1 >= 0 && cells[x - 1, y]._visited == false)
        {
            neighbors.Add(new CellRelativePos(cells[x - 1, y], CellRelativePos.Direction.West));
        }

        if (y - 1 >= 0 && cells[x, y - 1]._visited == false)
        {
            neighbors.Add(new CellRelativePos(cells[x, y - 1], CellRelativePos.Direction.North));
        }

        if (neighbors.Count == 0) return;

        neighbors.Shuffle();

        foreach (CellRelativePos selectedcell in neighbors)
        {
            if (selectedcell.direction == CellRelativePos.Direction.East)
            {
                if (selectedcell.cell._visited) continue;
                currentCell._East = true;
                selectedcell.cell._West = true;
                GenerateMaze(x + 1, y);
            }

            else if (selectedcell.direction == CellRelativePos.Direction.South)
            {
                if (selectedcell.cell._visited) continue;
                currentCell._South = true;
                selectedcell.cell._North = true;
                GenerateMaze(x, y + 1);
            }
            else if (selectedcell.direction == CellRelativePos.Direction.West)
            {
                if (selectedcell.cell._visited) continue;
                currentCell._West = true;
                selectedcell.cell._East = true;
                GenerateMaze(x - 1, y);
            }
            else if (selectedcell.direction == CellRelativePos.Direction.North)
            {
                if (selectedcell.cell._visited) continue;
                currentCell._North = true;
                selectedcell.cell._South = true;
                GenerateMaze(x, y - 1);
            }
        }


    }

    void InitVisualCell()
    {

        foreach (Cell cell in cells)
        {

            visualCellInst = Instantiate(visualCellPrefab, new Vector3(cell.xPos * 3, 0, _height * 3f - cell.zPos * 3), Quaternion.identity) as VisualCell;
            visualCellInst.transform.parent = transform;


            visualCellInst._North.gameObject.SetActive(!cell._North);

            visualCellInst._South.gameObject.SetActive(!cell._South);

            visualCellInst._East.gameObject.SetActive(!cell._East);

            visualCellInst._West.gameObject.SetActive(!cell._West);

            visualCellInst.transform.name = cell.xPos.ToString() + "_" + cell.zPos.ToString();

            // Additional chance to remove any potential walls/barricades created during maze geneartion
            if (Random.value > chance)
            {
                visualCellInst._North.gameObject.SetActive(false);
            }
            if (Random.value > chance)
            {
                visualCellInst._South.gameObject.SetActive(false);
            }
            if (Random.value > chance)
            {
                visualCellInst._East.gameObject.SetActive(false);
            }
            if (Random.value > chance)
            {
                visualCellInst._West.gameObject.SetActive(false);
            }

        }
        #endregion
    }
}
