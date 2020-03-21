using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public List<List<int[]>> infoMono = new List<List<int[]>>();  
    public int[] size = new int[2];
    public int[,] tablero;
    public int constante= 2700;
    public int sizeconst;
    public bool termino = false;
    public bool animado = false;
    public int animatedTime;
    public void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
