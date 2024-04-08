//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{

    public class Cell
    {
        public bool visited = false;
        public bool[] status = new bool[4]; //0 - top; 1 - down; 2 - left; 3 - right
    }

    [SerializeField] private Vector2Int size;
    [SerializeField] private int initPosition = 0;
    [SerializeField] public GameObject room;
    [SerializeField] private Vector2 roomSize; // 30 X 30 porque el prefab de la sala tiene ese tamaño
    [SerializeField] private int nRooms; // Pasos generados

    List<Cell> board;

    // Start is called before the first frame update
    void Start()
    {
        MazeGenerator();
    }

    /// 

    private void MazeGenerator()
    {
        board = new List<Cell>();
        for (int i = 0; i < size.x; i++) {
            for (int j = 0; j < size.y; j++)
            {
                board.Add(new Cell());
            }
        }
        int currentCell = initPosition;
        Stack <int> path = new Stack<int>(); //Pila
        int num = 0;
        while (num<nRooms)
        {
            num++;
            board[num].visited = true;

            //Comprobar si no hay celdas vecinas
            List<int> neighbours = CheckNeigbours(currentCell);

            //Comprobar si no hay vecinos
            if (neighbours.Count == 0)
            {
                //Si no hay mas camino que probar
                if(path.Count == 0)
                {
                    break; //Se sale
                }
                else //Retrocedemos
                {
                    currentCell = path.Pop(); //Saca 1 elemento de la pila

                }
            }
            else //Si hay vecinos
            {
                path.Push(currentCell);
                int newCell = neighbours[Random.Range(0, neighbours.Count)]; //Asignamos la nueva celda aleatoriamente

                //Vecino derecha o abajo
                if (newCell > currentCell)
                {
                    if(newCell-1 == currentCell) //Derecha
                    {
                        board[currentCell].status[3] = true;
                        board[newCell].status[2] = true;

                    }
                    else //Abajo
                    {
                        board[currentCell].status[0] = true;
                        board[newCell].status[1] = true;
                    }

                }
                else //Vecino izquierda o arriba
                {
                    if (newCell + 1 == currentCell) //Izquierda
                    {
                        board[currentCell].status[2] = true;
                        board[newCell].status[3] = true;
                    }
                    else //Derecha
                    {
                        board[currentCell].status[1] = true;
                        board[newCell].status[0] = true;
                    }


                }
                currentCell = newCell; //Actualizamos a la celda actual
            }

        }
        DungeonGenerator();

    }

    private void DungeonGenerator()
    {
        for (int i = 0; i < size.x; i++)
        {
            for(int j = 0; j < size.y; j++) {
                var newRoom = Instantiate(room, new Vector3(i * roomSize.x, 0, j * roomSize.y), Quaternion.identity, transform).GetComponent<RoomBehaviour>();
                newRoom.UpdateSala(board[i+j*size.x].status);
            }
        }
    }

    /// 

    private List<int> CheckNeigbours(int cell)
    {
        List<int> neigbours = new List<int>();

        if(cell - size.x > 0 && !board[cell-size.x].visited)//Comprobamos TOP
        {
            neigbours.Add(cell - size.x);
        }

        if (cell + size.x < board.Count && !board[cell + size.x].visited)//Comprobamos DOWN
        {
            neigbours.Add(cell + size.x);
        }

        if (cell % size.x != 0 && !board[cell - 1].visited)//Comprobamos LEFT
        {
            neigbours.Add(cell - 1);
        }

        if ((cell+1) % size.x != 0 && !board[cell + 1].visited)//Comprobamos RIGHT
        {
            neigbours.Add(cell + 1);
        }


        return neigbours;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
