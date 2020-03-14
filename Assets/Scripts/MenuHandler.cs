using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SimpleFileBrowser;
using static SimpleFileBrowser.FileBrowser;
using System.IO;


namespace Solver
{

    public class MenuHandler : MonoBehaviour
    {
        private Button loadNonogramBtn;

        void Awake()
        {
            // Get btn
            this.loadNonogramBtn = GameObject.Find("NonogramBtn").transform.Find("Button").GetComponent<Button>();
            this.loadNonogramBtn.onClick.AddListener(this.loadInputFileScene);
            var clues = new List<List<int[]>>();
            var rows = new List<int[]>();

            rows.Add(new int[] { 3, 2 });
            rows.Add(new int[] { 1, 1, 1, 1 });
            rows.Add(new int[] { 1, 2, 1, 2 });
            rows.Add(new int[] { 1, 2, 1, 1, 3 });
            rows.Add(new int[] { 1, 1, 2, 1 });
            rows.Add(new int[] { 2, 3, 1, 2 });
            rows.Add(new int[] { 9, 3 });
            rows.Add(new int[] { 2, 3 });
            rows.Add(new int[] { 1, 2 });
            rows.Add(new int[] { 1, 1, 1, 1 });
            rows.Add(new int[] { 1, 4, 1 });
            rows.Add(new int[] { 1, 2, 2, 2 });
            rows.Add(new int[] { 1, 1, 1, 1, 1, 1, 2 });
            rows.Add(new int[] { 2, 1, 1, 2, 1, 1 });
            rows.Add(new int[] { 3, 4, 3, 1 });
            clues.Add(rows);

            var columns = new List<int[]>();
            columns.Add(new int[] { 4, 3 });
            columns.Add(new int[] { 1, 6, 2 });
            columns.Add(new int[] { 1, 2, 2, 1, 1 });
            columns.Add(new int[] { 1, 2, 2, 1, 2 });
            columns.Add(new int[] { 3, 2, 3 });
            columns.Add(new int[] { 2, 1, 3 });
            columns.Add(new int[] { 1, 1, 1 });
            columns.Add(new int[] { 2, 1, 4, 1 });
            columns.Add(new int[] { 1, 1, 1, 1, 2 });
            columns.Add(new int[] { 1, 4, 2 });
            columns.Add(new int[] { 1, 1, 2, 1 });
            columns.Add(new int[] { 2, 7, 1 });
            columns.Add(new int[] { 2, 1, 1, 2 });
            columns.Add(new int[] { 1, 2, 1 });
            columns.Add(new int[] { 3, 3 });
            clues.Add(columns);

            // int[,] myMatrix = { { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 } };
            int[,] myMatrix = {
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
      };
            Debug.Log("Nonogram inicial: ");
            PrintMatrix(myMatrix);
            Debug.Log("Nonogram terminado: ");

            //var stopwatch = new Stopwatch();
            //stopwatch.Start();
            NonogramSolver.ResolverNonogram(myMatrix, clues, 0);
            //stopwatch.Stop();
            //Debug.Log("Tardo {0}s", stopwatch.Elapsed.Seconds);
            PrintMatrix(myMatrix);
        }
        public static void PrintMatrix(int[,] matrix)
        {
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    Debug.Log(matrix[i, j]);
                    Debug.Log(" ");
                }
                Debug.Log('\n');

            }
        }
        private void loadInputFileScene()
        {
            // Cargar la escena

            SceneManager.LoadScene("NonogramScene");
            buscar();

        }
        private void buscar()
        {
            FileBrowser.SetFilters(true, new FileBrowser.Filter("Text Files", ".txt", ".pdf"));
            FileBrowser.SetDefaultFilter(".txt");
            FileBrowser.ShowLoadDialog((path) => { organizarInfo(path); },
                                        () => { SceneManager.LoadScene("MenuScene"); },
                                        false, null, "Seleccione un archivo de texto", "Seleccionar");
            
        }
        private void organizarInfo(string path)
        {
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            string line;
            bool size = true;
            var Rows = new List<List<int>>();
            var Columns = new List<List<int>>();
            while ((line = file.ReadLine()) != null)
            {
                if (size)
                {
                    size = false;
                    string[] sizes = line.Split(',');
                    for (int o = 0; o < sizes.Length; o++)
                    {
                        DataManager.Instance.size[o] = int.Parse(sizes[o]);
                    }

                }
                else if (line == "FILAS")
                {

                    while ((line = file.ReadLine()) != null)
                    {
                        if (line == "COLUMNAS")
                        {
                            break;
                        }
                        List<int> Row = new List<int>();
                        string[] sizes = line.Split(',');
                        for (int o = 0; o < sizes.Length; o++)
                        {
                            Row.Add(int.Parse(sizes[o]));
                        }
                        Rows.Add(Row);
                    }
                }
                else
                {
                    List<int> Column = new List<int>();
                    string[] sizes = line.Split(',');
                    for (int o = 0; o < sizes.Length; o++)
                    {
                        Column.Add(int.Parse(sizes[o]));
                    }
                    Columns.Add(Column);
                }

            }
            DataManager.Instance.infoMono.Add(Rows);
            DataManager.Instance.infoMono.Add(Columns);
            DataManager.Instance.tablero = new int[DataManager.Instance.size[0], DataManager.Instance.size[1]];
            file.Close();
            GridC grid = new GridC(DataManager.Instance.size[1], DataManager.Instance.size[0], 3500 / (DataManager.Instance.size[0] * DataManager.Instance.size[1]));
            //NonogramSolver.ResolverNonogram(DataManager.Instance.tablero, DataManager.Instance.infoMono, 0);
            //for (int i = 0; i < DataManager.Instance.size[0]; i++)
            //{
            //    for (int j = 0; j < DataManager.Instance.size[1]; j++)
            //    {
            //        Debug.Log(DataManager.Instance.tablero[i, j]);
            //        Debug.Log(" ");
            //    }
            //    Debug.Log('\n');
            //}
        }
    }

}
    
    



