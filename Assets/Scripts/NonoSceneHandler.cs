using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using Solver;

public class NonoSceneHandler : MonoBehaviour
{
    private Button startNonogramBtn;
    private Toggle animacionToggler;
    private bool animar = false;
    public bool empezo = false;

    // Start is called before the first frame update
    void Awake()
    {

        Debug.Log(animar);
        // Obtener game objects
        this.startNonogramBtn = GameObject.Find("ButtonS").GetComponent<Button>();
        this.animacionToggler = GameObject.Find("animacionToggler").GetComponent<Toggle>();
        // Set event listeners
        this.startNonogramBtn.onClick.AddListener(Resolver);
        this.animacionToggler.onValueChanged.AddListener((value) => ToggleAnimacion());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        // Verificar si se inicio el nonogram y si es animado
        if (empezo && this.animar)
        {
            PintarNonogram();
            if (DataManager.Instance.termino)
            {
                empezo = false;
            }
        }
    }

    private void ToggleAnimacion()
    {
        this.animar = !this.animar;
    }

    private void Resolver()
    {
        // Se inicio la solucion
        empezo = true;
        // Verificar si debe ser animado
        if (!this.animar) DataManager.Instance.animado = false;
        // Iniciar solucion
        Thread solveNonogramThread = new Thread(new ThreadStart(ResolverNonogram));
        solveNonogramThread.Start();
        // Si no es animado esperar a que se termine de resolver
        if (!this.animar)
        {
            solveNonogramThread.Join();
            PintarNonogram();
        }
    }

    void ResolverNonogram()
    {
        // Empezar a medir tiempo
        System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();
        // Resolver nonogram
        NonogramSolver.ResolverNonogram(DataManager.Instance.tablero, DataManager.Instance.infoMono, 0);
        stopwatch.Stop();
        Debug.Log(stopwatch.ElapsedMilliseconds.ToString());
    }

    static void PintarNonogram()
    {
        // Luego de solucionado el nonogram, se pintan los cuadros acorde al resultado
        for (int i = 0; i < DataManager.Instance.tablero.GetLength(0); i++)
        {
            for (int j = 0; j < DataManager.Instance.tablero.GetLength(1); j++)
            {
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
        }
    }
}
