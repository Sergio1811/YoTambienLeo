using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerGusanos : MonoBehaviour
{
    public float m_MaxTime = 30;
    private float currentTime = 0;
    public SceneManagement m_Scener;

    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= m_MaxTime)
        {
            GameManager.m_CurrentToMinigame = 0;
            m_Scener.InicioScene();
        }
    }

}
