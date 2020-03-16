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
        public GameObject cubicle;
    }
    public GameObject cube;
    public int columns = 100;
    public int rows = 100;
    ObjectPooler objectPooler;
    private Cell[,] grid;

    private void Awake()
    {
        grid = new Cell[columns, rows];
    }

    void Start()
    {
        objectPooler = ObjectPooler.Instance;

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

                //Spawning the cubes
                cell.cubicle = objectPooler.SpawnFromPool("Cube", pos, Quaternion.identity);
                // Debug.Log("The Cell [" + i + ", " + j + "] has instantiated with pos " + pos.x + " " + pos.y + " " + pos.z);
                
                //randomizing cubes
                if (Random.Range(0, 2) == 1) cell.isAlive = true;


                grid[i, j] = cell;
                if (cell.isAlive)
                {
                    cell.cubicle.SetActive(true);
                }
                else cell.cubicle.SetActive(false);
            }
        }
    }

    void FixedUpdate()
    {
        //apply rules when space is pressed
        if (Input.GetKey("space"))
        {
            Debug.Log(grid[5, 5].isAlive + " " + grid[5, 5].aliveNeighbours);
            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    CheckNeighbours(new Vector3(i, 0, j));

                    //rules 1 and 3
                    if (grid[i, j].isAlive)
                    {
                        if (grid[i, j].aliveNeighbours < 2)
                        {
                            grid[i, j].isAlive = false;
                            grid[i, j].cubicle.SetActive(false);
                        }
                        //rule 2 doesn't require action
                        else if (grid[i, j].aliveNeighbours > 3)
                        {
                            grid[i, j].isAlive = false;
                            grid[i, j].cubicle.SetActive(false);
                        }
                    }
                    // rule 4
                    if (grid[i, j].aliveNeighbours == 3 && !grid[i, j].isAlive)
                    {
                        grid[i, j].isAlive = true;
                        grid[i, j].cubicle.SetActive(true);
                    }
                }
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
