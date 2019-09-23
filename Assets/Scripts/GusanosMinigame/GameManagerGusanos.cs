using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerGusanos : MonoBehaviour
{
    public float m_MaxTime = 30;
    public SceneManagement m_Scener;

    void Update()
    {
        if (Time.time >= m_MaxTime)
        {
            GameManager.m_CurrentToMinigame = 0;
            m_Scener.InicioScene();
        }
    }

}
