using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
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
}
