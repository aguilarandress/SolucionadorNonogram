using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using Solver;

public class NonoSceneHandler : MonoBehaviour
{
    private Button startNonogramBtn;
    private bool animar;
    public bool empezo;

    // Start is called before the first frame update
    void Awake()
    {
        this.startNonogramBtn = GameObject.Find("ButtonS").GetComponent<Button>();
        this.startNonogramBtn.onClick.AddListener(Resolver);
    }

    private void Resolver()
    {
        Debug.Log("Empezo...");
        empezo = true;
        Thread solveNonogramThread = new Thread(new ThreadStart(ResolverNonogram));
        solveNonogramThread.Start();
    }

    void FixedUpdate()
    {
        if (empezo)
        {

            if (animar)
            {
                // TODO: 
            }
            else
            {
                // Luego de solucionado el nonogram, se pintan los cuadros acorde al resultado
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
                if (DataManager.Instance.termino)
                {
                    empezo = false;
                }
            }
        }
    }

    static void ResolverNonogram()
    {
        Debug.Log("xd...");
        var stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();
        NonogramSolver.ResolverNonogram(DataManager.Instance.tablero, DataManager.Instance.infoMono, 0);
        stopwatch.Stop();
        Debug.Log(stopwatch.ElapsedMilliseconds.ToString());
    }

    static void PintarNonogram()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
