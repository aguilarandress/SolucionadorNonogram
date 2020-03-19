using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Solver;

public class NonoSceneHandler : MonoBehaviour
{
    private Button startNonogramBtn;
    // Start is called before the first frame update
    void Awake()
    {
        this.startNonogramBtn = GameObject.Find("ButtonS").GetComponent<Button>();
        this.startNonogramBtn.onClick.AddListener(Resolver);
    }

    private void Resolver()
    {
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
                        GridC.setBlanco(i, j);
                        break;
                    case 1:
                        GridC.setNegro(i, j);
                        break;
                    default:
                        GridC.setBlanco(i, j);
                        break;
                }
            }
            Debug.Log(result);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
