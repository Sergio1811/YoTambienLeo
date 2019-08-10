using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public GameObject ConfCanvas;

    public GameObject AñadirPalabraCanvas;

    public GameObject EditarCanvas;

    public GameObject MainMenuCanvas;

    public GameObject ConfiguracionPestaña;

    public GameObject[] AllCanvas;

    public void InicioScene()
     {
        SceneManager.LoadScene(GameManager.Instance.InicioIndex);
     }
     public void PreparadosScene()
     {
        SceneManager.LoadScene(GameManager.Instance.PreparadosIndex);
     }
     public void ListosScene()
     {
        SceneManager.LoadScene(GameManager.Instance.ListosIndex);
     }
     public void YaScene()
     {
        SceneManager.LoadScene(GameManager.Instance.YaIndex);
     }
     public void ConfScene()
     {
        DisableAllCanvas();
        ConfCanvas.SetActive(true);
     }
     public void ConfiguarcionScene()
     {
        DisableAllCanvas();
        ConfiguracionPestaña.SetActive(true);
     }
     public void MainMenuScene()
     {
        DisableAllCanvas();
        ConfCanvas.SetActive(true);
     }
     public void AddWordScene()
     {
        DisableAllCanvas();
        ConfCanvas.SetActive(true);
     }
     public void MinijuegoGusanosScene()
     {
        SceneManager.LoadScene(GameManager.Instance.GusanosIndex);
     }
     public void MinijuegoBurbujasScene()
     {
        SceneManager.LoadScene(GameManager.Instance.BurbujasIndex);
     }
     public void MinijuegoColorScene()
     {
        SceneManager.LoadScene(GameManager.Instance.ColorIndex);
     }
     public void WebButton()
     {
        Application.OpenURL("www.yotambienleo.com/recomendaciones");
     }
    public void Quit()
    {
        Application.Quit();
    }

    public void DisableAllCanvas()
    {
        for (int i = 0; i < AllCanvas.Length; i++)
        {
            AllCanvas[i].SetActive(false);
        }
    }
}
