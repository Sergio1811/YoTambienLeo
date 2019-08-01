using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minijuego_Globos : MonoBehaviour
{

    private GameObject m_Spawn;
    private GameObject m_Destroy;

    public GameObject m_Globo;
    public GameObject m_EndGame;

    private float m_TimePassed;
    private float m_TimeToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        m_Spawn = GameObject.FindGameObjectWithTag("SpawnGlobo");
        m_Destroy = GameObject.FindGameObjectWithTag("DestroyGlobo");

        m_TimePassed = 0;
        m_TimeToSpawn = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_TimePassed < 30)
        {
            m_TimePassed += Time.deltaTime;

            if (m_TimeToSpawn > 1)
            {
                Instantiate(m_Globo, m_Spawn.transform.position + new Vector3(Random.Range(-7, 7), Random.Range(-0.5f, 0.5f), 0), Quaternion.identity);
                m_TimeToSpawn = 0;
            }
            else
                m_TimeToSpawn += Time.deltaTime;
        }
        else 
        {
            Debug.Log("End Minigame");
            m_EndGame.SetActive(true);
            //endgame
        }
    }
}
