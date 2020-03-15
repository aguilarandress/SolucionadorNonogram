using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CodeMonkey.Utils;
using System.Threading;

public class GridC
{
    private int height;
    private int width;
    private float CellSize;
    private int[,] gridArray;
    public GridC(int height, int width, float CellSize)
    {
        Debug.Log("Entro a grid...");
        this.width = width;
        this.height = height;
        this.CellSize = CellSize;

        this.gridArray = new int[width, height];

        int cameraSize;

        if (DataManager.Instance.size[0] > DataManager.Instance.size[1])
        {
            cameraSize = DataManager.Instance.size[0];
        }
        else
        {
            cameraSize = DataManager.Instance.size[1];
        }
        GameObject camaraNew = new GameObject();
        camaraNew.AddComponent<Camera>();
        camaraNew.transform.position = new Vector3(0, 0, -1);
        camaraNew.GetComponent<Camera>().orthographicSize = cameraSize/1.4f ;
        camaraNew.GetComponent<Camera>().orthographic = true;
        for (int i = 0; i < gridArray.GetLength(1); i++)
        {
            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                {
                    GameObject go = new GameObject("slot" + i +"_"+ x);
                    SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
                    Sprite cuadro = Resources.Load<Sprite>("square");
                    go.GetComponent<SpriteRenderer>().sprite = cuadro;
                    //go.transform.SetParent(parent.transform);
                    //go.transform.localPosition = getWorldPosition(i-2, x-2);
                    go.transform.position = new Vector2((-DataManager.Instance.size[0]/2) + i, (-DataManager.Instance.size[0]/2)+x); 
                    go.transform.localScale = new Vector2(1,1);

                }
            }
        }
        
        for (int i = 0; i < DataManager.Instance.tablero.GetLength(0); i++)
        {
           for (int j = 0; j < DataManager.Instance.tablero.GetLength(1); j++)
            {
                DataManager.Instance.tablero[i, j] = 0;
                //setNegro(i, j);
            }
        }

        Debug.Log("Empezo...");

        var stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();
        NonogramSolver.ResolverNonogram(DataManager.Instance.tablero, DataManager.Instance.infoMono, 0);
        stopwatch.Stop();
        Debug.Log(stopwatch.ElapsedMilliseconds.ToString());
        for (int i = 0; i < DataManager.Instance.tablero.GetLength(0); i++)
        {
            string result = "";
            for (int j = 0; j < DataManager.Instance.tablero.GetLength(1); j++)
            {
                result += DataManager.Instance.tablero[i, j].ToString() + " ";
                switch (DataManager.Instance.tablero[i, j])
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
            Debug.Log(result);
        }
    }
    private Vector3 getWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * this.CellSize;
    }
    public static void setNegro(int x, int y)
    {
        GameObject square = GameObject.Find("slot"+y+"_"+x);
        Sprite cuadro = Resources.Load<Sprite>("squareb");
        square.GetComponent<SpriteRenderer>().sprite = cuadro;
    }
    public static void setBlanco(int x, int y)
    {
        GameObject square = GameObject.Find("slot" + y + "_" + x);
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
