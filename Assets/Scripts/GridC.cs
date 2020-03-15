using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Solver;
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
        //Decide si el nonogram es más grande en un eje o en el otro para ajustar la cámara
        if (DataManager.Instance.size[0] > DataManager.Instance.size[1])
        {
            cameraSize = DataManager.Instance.size[0];
        }
        else
        {
            cameraSize = DataManager.Instance.size[1];
        }
        //Crea una cámara nueva ubicada en 0,0 y le asigna un rango de visión acorde al tamaño del nonogram
        GameObject camaraNew = new GameObject();
        camaraNew.AddComponent<Camera>();
        camaraNew.transform.position = new Vector3(0, 0, -1);
        camaraNew.GetComponent<Camera>().orthographicSize = cameraSize/1.55f ;
        camaraNew.GetComponent<Camera>().orthographic = true;
        //Crea un sprite por cada elemento de la matriz del tamaño del nonogram 
        for (int i = 0; i < gridArray.GetLength(1); i++)
        {
            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                {
                    GameObject go = new GameObject("slot" + i +"_"+ x);
                    SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
                    Sprite cuadro = Resources.Load<Sprite>("square");
                    go.GetComponent<SpriteRenderer>().sprite = cuadro;
                    go.transform.position = new Vector2((-DataManager.Instance.size[0]/2) + i, -(-DataManager.Instance.size[0]/2)-x+-0.6f); 
                    go.transform.localScale = new Vector2(0.7f,0.7f);

                }
            }
        }
        //Se asigna como 0 a todos los valores de la matriz , dado que se requiere para el algoritmo de solución
        for (int i = 0; i < DataManager.Instance.tablero.GetLength(0); i++)
        {
           for (int j = 0; j < DataManager.Instance.tablero.GetLength(1); j++)
            {
                DataManager.Instance.tablero[i, j] = 0;
            }
        }

        Debug.Log("Empezo...");

        var stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();
        NonogramSolver.ResolverNonogram(DataManager.Instance.tablero, DataManager.Instance.infoMono, 0);
        stopwatch.Stop();
        Debug.Log(stopwatch.ElapsedMilliseconds.ToString());
        //Luego de solucionado el nonogram, se pintan los cuadros acorde al resultado
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



    //Funciones que se encargan de cambiar el color de los sprites por otro 
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
