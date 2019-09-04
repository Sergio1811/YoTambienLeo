using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerParejas : MonoBehaviour
{
    public GameObject m_Canvas;

    public GameObject m_Naranja;
    public GameObject m_Rojo;
    public GameObject m_Azul;
    public GameObject m_Verde;
    public GameObject m_Amarillo;

    public int m_NumPairs;
    private bool m_IsHorizontal;
    private float m_XPos;
    private float m_YPos;
    private bool m_FirstPair;

    void Start()
    {

        List<GameObject> m_Pairs = new List<GameObject>();
        m_Pairs.Add(m_Naranja);
        m_Pairs.Add(m_Rojo);
        m_Pairs.Add(m_Verde);
        m_Pairs.Add(m_Azul);
        m_Pairs.Add(m_Amarillo);

        List<GameObject> m_SecondPair = new List<GameObject>();

        if (Random.Range(0, 2) == 1)
            m_IsHorizontal = true;
        else
            m_IsHorizontal = false;

        m_FirstPair = true;

        if (m_IsHorizontal)
        {
            m_XPos = Screen.width / (m_NumPairs * 2.0f);
            m_YPos = Screen.height / 4;

            for (int i = 0; i < 2; i++)
            {
                int k = 1;

                if (m_FirstPair)
                {
                    for (int j = 0; j < m_NumPairs; j++)
                    {
                        Vector3 m_NewPosition = new Vector3(m_XPos * k, m_YPos, 0f);
                        int m_RandomPair = Random.Range(0, m_Pairs.Count);
                        GameObject m_NewPair = Instantiate(m_Pairs[m_RandomPair], m_Canvas.transform);
                        m_NewPair.GetComponent<RectTransform>().anchoredPosition = m_NewPosition;

                        m_SecondPair.Add(m_NewPair);
                        m_Pairs.RemoveAt(m_RandomPair);

                        k++;
                        k++;
                    }
                }
                else
                {
                    for (int j = 0; j < m_NumPairs; j++)
                    {
                        Vector3 m_NewPosition = new Vector3(m_XPos * k, m_YPos, 0f);
                        int m_RandomPair = Random.Range(0, m_SecondPair.Count);
                        GameObject m_NewPair = Instantiate(m_SecondPair[m_RandomPair], m_Canvas.transform);
                        m_NewPair.GetComponent<RectTransform>().anchoredPosition = m_NewPosition;

                        m_SecondPair.RemoveAt(m_RandomPair);

                        k++;
                        k++;
                    }
                }

                m_FirstPair = false;
                m_YPos *= 3f;
            }

        }
        else
        {
            m_XPos = Screen.width / 4;
            m_YPos = Screen.height / (m_NumPairs * 2.0f);

            for (int i = 0; i < 2; i++)
            {
                int k = 1;

                if (m_FirstPair)
                {
                    for (int j = 0; j < m_NumPairs; j++)
                    {
                        Vector3 m_NewPosition = new Vector3(m_XPos, m_YPos * k, 0f);
                        int m_RandomPair = Random.Range(0, m_Pairs.Count);
                        GameObject m_NewPair = Instantiate(m_Pairs[m_RandomPair], m_Canvas.transform);
                        m_NewPair.GetComponent<RectTransform>().anchoredPosition = m_NewPosition;

                        m_SecondPair.Add(m_NewPair);
                        m_Pairs.RemoveAt(m_RandomPair);

                        k++;
                        k++;
                    }
                }
                else
                {
                    for (int j = 0; j < m_NumPairs; j++)
                    {
                        Vector3 m_NewPosition = new Vector3(m_XPos, m_YPos * k, 0f);
                        int m_RandomPair = Random.Range(0, m_SecondPair.Count);
                        GameObject m_NewPair = Instantiate(m_SecondPair[m_RandomPair], m_Canvas.transform);
                        m_NewPair.GetComponent<RectTransform>().anchoredPosition = m_NewPosition;

                        m_SecondPair.RemoveAt(m_RandomPair);

                        k++;
                        k++;
                    }
                }
                m_FirstPair = false;
                m_XPos *= 3f;
            }
        }
    }

    void Update()
    {

    }

}
