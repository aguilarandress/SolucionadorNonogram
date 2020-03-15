using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SimpleFileBrowser;

using static SimpleFileBrowser.FileBrowser;
using System.IO;

public class MenuHandler : MonoBehaviour
{
    private Button loadNonogramBtn;

    void Awake()
    {
        // Get btn
        this.loadNonogramBtn = GameObject.Find("NonogramBtn").transform.Find("Button").GetComponent<Button>();
        this.loadNonogramBtn.onClick.AddListener(this.loadInputFileScene);
    }

    private void loadInputFileScene()
    {
        // Cargar la escena
        SceneManager.LoadScene("NonogramScene");
        buscar();
    }
    private void buscar()
    {
        //Pide el archivo a cargar
        FileBrowser.SetFilters(true, new FileBrowser.Filter("Text Files", ".txt", ".pdf"));
        FileBrowser.SetDefaultFilter(".txt");
        FileBrowser.ShowLoadDialog((path) => { organizarInfo(path); },
                                    () => { Debug.Log("Cancelado..."); },
                                    false, null, "Seleccione un archivo de texto", "Seleccionar");
        
    }
    //Se encarga de cargar el archivo seleccionado 
    private void organizarInfo(string path)
    {
        System.IO.StreamReader file = new System.IO.StreamReader(path);
        string line;
        bool size = true;
        var Rows = new List<int[]>();
        var Columns = new List<int[]>();
        //Recorre el archivo buscando el tamaño del nonogram
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
            //Busca las pistas por fila 

            else if (line == "FILAS")
            {

                while ((line = file.ReadLine()) != null)
                {   
                    //En el momento que encuentra "columnas" sale del ciclo para cargar las pistas de columnas
                    if (line == "COLUMNAS")
                    {
                        break;
                    }
                    
                    string[] sizes = line.Split(',');
                    int contador=0;
                    for (int o = 0; o < sizes.Length; o++)
                    {
                        contador++;
                    }
                    int[] Row = new int[contador];
                    for (int o = 0; o < sizes.Length; o++)
                    {
                        Row[o] = int.Parse (sizes[o]);
                    }
                    Rows.Add(Row);
                }
            }
            //Carga las pistas por columna
            else
            {
                
                string[] sizes = line.Split(',');
                int contador = 0;
                for (int o = 0; o < sizes.Length; o++)
                {
                    contador++;
                }
                int[] column = new int[contador];
                for (int o = 0; o < sizes.Length; o++)
                {
                    column[o]=int.Parse(sizes[o]);
                }
                Columns.Add(column);
            }

        }
        //Añade la info a la instancia de singleton para su uso en varias escenas
        DataManager.Instance.infoMono.Add(Rows);
        DataManager.Instance.infoMono.Add(Columns);
        DataManager.Instance.tablero = new int[DataManager.Instance.size[0], DataManager.Instance.size[1]];
        file.Close();
        //Crea el grid en base a la información proporcionada
        GridC grid = new GridC(DataManager.Instance.size[1], DataManager.Instance.size[0], 3500 / (DataManager.Instance.size[0] * DataManager.Instance.size[1]));
    }
}

    
    



