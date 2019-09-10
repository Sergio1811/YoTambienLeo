using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerGusanos : MonoBehaviour
{
    public float m_MaxTime;
    public SceneManagement m_Scener;

    void Update()
    {
        if (Time.time >= m_MaxTime)
            m_Scener.InicioScene();
    }
}
