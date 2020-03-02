using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        SceneManager.LoadScene("InputFileScene");
    }
}
