using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Solver;
using CodeMonkey.Utils;

public class GridC
{
    private int height;
    private int width;
    private float CellSize;
    private int[,] gridArray;
    public GridC(int height, int width, float CellSize)
    {
        this.width = width;
        this.height = height;
        this.CellSize = CellSize;

        this.gridArray = new int[width, height];
        GameObject parent = GameObject.Find("Container");
        for (int i = 0; i < gridArray.GetLength(0); i++)
        {
            for (int x = 0; x < gridArray.GetLength(1); x++)
            {
                {
                    GameObject go = new GameObject("slot" + i +"_"+ x);
                    SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
                    Sprite cuadro = Resources.Load<Sprite>("square");
                    go.GetComponent<SpriteRenderer>().sprite = cuadro;
                    go.transform.SetParent(parent.transform);
                    go.transform.localPosition = getWorldPosition(i-2, x-2);
                    go.transform.localScale = new Vector2((3500/(DataManager.Instance.size[0]*DataManager.Instance.size[1]))/1.5f, (3500 / (DataManager.Instance.size[0] * DataManager.Instance.size[1]))/1.5f);
                }
            }
        }

        for (int i = 0; i < DataManager.Instance.tablero.GetLength(0); i++)
        {
           for (int j = 0; j < DataManager.Instance.tablero.GetLength(1); j++)
            {
                DataManager.Instance.tablero[i, j] = 0;
            }
        }
        NonogramSolver.ResolverNonogram(DataManager.Instance.tablero, DataManager.Instance.infoMono, 0);
        for (int i = 0; i < DataManager.Instance.tablero.GetLength(0); i++)
        {
            for (int j = 0; j < DataManager.Instance.tablero.GetLength(1); j++)
            {
                switch(DataManager.Instance.tablero[i, j])
                {
                    case 0:
                        setBlanco(i, j);
                        break;
                    case 1:
                        setNegro(i, j);
                        break;
                    default:
                        setBlanco(i, j);
                        break;
                }
            }
        }

        // setNegro(0, 1);
    }
    private Vector3 getWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * this.CellSize;
    }
    public static void setNegro(int x, int y)
    {
        GameObject square = GameObject.Find("slot"+x+"_"+y);
        Sprite cuadro = Resources.Load<Sprite>("squareb");
        square.GetComponent<SpriteRenderer>().sprite = cuadro;


    }
    public static void setBlanco(int x, int y)
    {
        GameObject square = GameObject.Find("slot" + x + "_" + y);
        Sprite cuadro = Resources.Load<Sprite>("square");
        square.GetComponent<SpriteRenderer>().sprite = cuadro;


    }
    public static void setX(int x, int y)
    {
        GameObject square = GameObject.Find("slot" + x + "_" + y);
        Sprite cuadro = Resources.Load<Sprite>("squareb");
        square.GetComponent<SpriteRenderer>().sprite = cuadro;
    }

}
