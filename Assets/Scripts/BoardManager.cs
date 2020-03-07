using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    struct Cell
    {
        public float x;
        public float y;
        public float z;
        public bool isAlive;
        public int aliveNeighbours;
    }
    public GameObject cube;
    public int columns = 100;
    public int rows = 100;

    private Cell[,] grid;

    private void Awake()
    {
        grid = new Cell[columns, rows];
    }

    void Start()
    {
        // Setting up the grid. If cell is alive then instantiate a cube at cell's point.
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                Cell cell = new Cell();
                cell.x = i;
                cell.y = 0;
                cell.z = j;
                Vector3 pos = new Vector3(cell.x, cell.y, cell.z);
                cell.isAlive = Random.value <= 0.13 ? true : false;
                grid[i, j] = cell;
                if (cell.isAlive)
                {
                    Instantiate(cube, pos, Quaternion.identity);
                    // Debug.Log("The Cell [" + i + ", " + j + "] has instantiated with pos " + pos.x + " " + pos.y + " " + pos.z);
                }
            }
        }

        // After setting up the cells check the number of alive neighbours for each cell.
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                CheckNeighbours(new Vector3(i, 0 ,j));
                string isAlive = grid[i, j].isAlive ? " alive" : "dead";
                Debug.Log("The Cell [" + i + ", " + j + "] is " + isAlive + " and has " + grid[i, j].aliveNeighbours + " alive neighbours")  ;
            }
        }
    }

    public void CheckNeighbours(Vector3 pos)
    {
        int aliveNeighbours = 0;
        int x = (int)pos.x;
        int z = (int)pos.z;

        for (int i = x - 1; i <= x + 1; i++)
        {
            // If the cell is on the left edge of the grid, then do not check the left of the cell. 
            // If the cell is on the right edge of the grid, then do not check the right of the cell. 
            if (i - 1 < 0 || i + 1 >= columns)
            {
                continue;
            }
            for (int j = z - 1; j <= z + 1; j++)
            {
                // If the cell is on the top edge of the grid, then do not check above of the cell. 
                // If the cell is on the bottom edge of the grid, then do not check below of the cell. 
                if (j - 1 < 0 || j + 1 >= rows || (i == x && j == z))
                {
                    continue;
                }

                if (grid[i, j].isAlive)
                {
                    aliveNeighbours++;
                }
            }
        }
        grid[x, z].aliveNeighbours = aliveNeighbours;
        // Debug.Log("The Cell [" +x + ", " + z + "] has " + aliveNeighbours + " alive neighbours.");
    }
}
