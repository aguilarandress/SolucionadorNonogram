using UnityEngine;
using TMPro;

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

        int cameraSize;
        // Decide si el nonogram es más grande en un eje o en el otro para ajustar la cámara
        if (DataManager.Instance.size[0] > DataManager.Instance.size[1])
        {
            cameraSize = DataManager.Instance.size[0];
        }
        else
        {
            cameraSize = DataManager.Instance.size[1];
        }
        // Crea una cámara nueva ubicada en 0,0 y le asigna un rango de visión acorde al tamaño del nonogram
        GameObject camaraNew = GameObject.Find("Main Camera");
        camaraNew.transform.position = new Vector3(0, 0, -1);
        float constanteCamara;
        if((DataManager.Instance.size[0]* DataManager.Instance.size[1])<30)
        {
            constanteCamara = 0.95f;
        }
        else
        {
            constanteCamara = 1.3f;
        }
        camaraNew.GetComponent<Camera>().orthographicSize = cameraSize / constanteCamara;
        camaraNew.GetComponent<Camera>().orthographic = true;
        // Crea un sprite por cada elemento de la matriz del tamaño del nonogram
        for (int i = 0; i < gridArray.GetLength(1); i++)
        {
            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                {
                    GameObject go = new GameObject("slot" + i +"_"+ x);
                    SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
                    Sprite cuadro = Resources.Load<Sprite>("square");
                    go.GetComponent<SpriteRenderer>().sprite = cuadro;
                    go.transform.position = new Vector2((-DataManager.Instance.size[0]/2) + i, -(-DataManager.Instance.size[0]/2)-x+-1.6f); 
                    go.transform.localScale = new Vector2(0.7f,0.7f);
                }
            }
        }
        // Se crean labels que contienen las pistas
        GameObject contenedor = GameObject.Find("CanvasPistas");
        for (int i=0;i< height;i++)
        {
            GameObject square = GameObject.Find("slot0_"+i.ToString());
            int [] pistas=DataManager.Instance.infoMono[0][i];
            GameObject label = new GameObject();
            label.AddComponent<TextMeshPro>();
            label.transform.localPosition = new Vector3(square.transform.position.x-1.7f,square.transform.position.y,1);
            string pista="";
            for (int o=0;o<pistas.Length;o++)
            {
                pista += " " + pistas[o];
            }
            label.GetComponent<TextMeshPro>().text = pista;
            label.GetComponent<TextMeshPro>().alignment = TMPro.TextAlignmentOptions.Center;
            label.GetComponent<TextMeshPro>().fontSize = 6;
        }
        bool primero = true;
        for (int i = 0; i < width; i++)
        {
            GameObject square = GameObject.Find("slot"+ i.ToString()+"_0");
            int[] pistas = DataManager.Instance.infoMono[1][i];
            GameObject label = new GameObject();
            label.AddComponent<TextMeshPro>();
            label.transform.localPosition = new Vector2(square.transform.position.x , square.transform.position.y+2);
            string pista = "";
            for (int o = 0; o < pistas.Length; o++)
            {
                if (primero)
                {
                    pista += pistas[o];
                    primero = false;
                }
                else
                { 
                    pista += "\n" + pistas[o];
                }
            }
            label.GetComponent<TextMeshPro>().text = pista;
            label.GetComponent<TextMeshPro>().alignment = TMPro.TextAlignmentOptions.Midline;
            label.GetComponent<TextMeshPro>().fontSize = 6;
        }

        // Se asigna como 0 a todos los valores de la matriz , dado que se requiere para el algoritmo de solución
        for (int i = 0; i < DataManager.Instance.tablero.GetLength(0); i++)
        {
           for (int j = 0; j < DataManager.Instance.tablero.GetLength(1); j++)
            {
                DataManager.Instance.tablero[i, j] = 0;
            }
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
        Sprite cuadro = Resources.Load<Sprite>("squarex");
        square.GetComponent<SpriteRenderer>().sprite = cuadro;
    }
    
}
