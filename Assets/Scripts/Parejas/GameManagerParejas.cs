using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerParejas : MonoBehaviour
{
    public GameObject m_Canvas;
    public Texture2D[] m_ImagePairs;
    # region Separador
#endregion
    public GameObject m_Plantilla;
   

    public int m_NumPairs;
    private bool m_IsHorizontal;
    private float m_XPos;
    private float m_YPos;
    private bool m_FirstPair;

    void Start()
    {

        List<Texture2D> m_Pairs = new List<Texture2D>();
        foreach (Texture2D item in m_ImagePairs)
        {
            m_Pairs.Add(item);
        }/*
        m_Pairs.Add(m_Naranja);
        m_Pairs.Add(m_Rojo);
        m_Pairs.Add(m_Verde);
        m_Pairs.Add(m_Azul);
        m_Pairs.Add(m_Amarillo);*/

        List<Texture2D> m_SecondPair = new List<Texture2D>();
        List<Texture2D> m_ThirdPair = new List<Texture2D>();

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
                        Vector3 l_NewPosition = new Vector3(m_XPos * k, m_YPos, 0f);
                        int l_RandomPair = Random.Range(0, m_Pairs.Count);
                        GameObject m_NewPair = Instantiate(m_Plantilla, m_Canvas.transform);
                        m_NewPair.GetComponent<Image>().sprite= Sprite.Create(m_Pairs[l_RandomPair], new Rect(0, 0, 400, 400), Vector2.zero);
                        m_NewPair.GetComponent<RectTransform>().anchoredPosition = l_NewPosition;
                        m_NewPair.name = j.ToString();

                        m_SecondPair.Add(m_Pairs[l_RandomPair]);
                        m_ThirdPair.Add(m_Pairs[l_RandomPair]);
                        m_Pairs.RemoveAt(l_RandomPair);

                        k++;
                        k++;
                    }
                }
                else
                {
                    for (int j = 0; j < m_NumPairs; j++)
                    {
                        Vector3 l_NewPosition = new Vector3(m_XPos * k, m_YPos, 0f);
                        int l_RandomPair = Random.Range(0, m_SecondPair.Count);
                        GameObject m_NewPair = Instantiate(m_Plantilla, m_Canvas.transform);
                        m_NewPair.GetComponent<Image>().sprite = Sprite.Create(m_SecondPair[l_RandomPair], new Rect(0, 0, 400, 400), Vector2.zero);
                        m_NewPair.GetComponent<RectTransform>().anchoredPosition = l_NewPosition;
                        m_NewPair.name = m_ThirdPair.IndexOf(m_SecondPair[l_RandomPair]).ToString();
                        m_SecondPair.RemoveAt(l_RandomPair);

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
                        Vector3 l_NewPosition = new Vector3(m_XPos, m_YPos * k, 0f);
                        int l_RandomPair = Random.Range(0, m_Pairs.Count);
                        GameObject m_NewPair = Instantiate(m_Plantilla, m_Canvas.transform);
                        m_NewPair.GetComponent<Image>().sprite = Sprite.Create(m_Pairs[l_RandomPair], new Rect(0, 0, 400, 400), Vector2.zero);
                        m_NewPair.GetComponent<RectTransform>().anchoredPosition = l_NewPosition;
                        m_NewPair.name = j.ToString();

                        m_SecondPair.Add(m_Pairs[l_RandomPair]);
                        m_ThirdPair.Add(m_Pairs[l_RandomPair]);
                        m_Pairs.RemoveAt(l_RandomPair);

                        k++;
                        k++;

                    }
                }
                else
                {
                    for (int j = 0; j < m_NumPairs; j++)
                    {
                        Vector3 l_NewPosition = new Vector3(m_XPos, m_YPos * k, 0f);
                        int l_RandomPair = Random.Range(0, m_SecondPair.Count);
                        GameObject m_NewPair = Instantiate(m_Plantilla, m_Canvas.transform);
                        m_NewPair.GetComponent<Image>().sprite = Sprite.Create(m_SecondPair[l_RandomPair], new Rect(0, 0, 400, 400), Vector2.zero);
                        m_NewPair.GetComponent<RectTransform>().anchoredPosition = l_NewPosition;
                       
                       m_NewPair.name = m_ThirdPair.IndexOf(m_SecondPair[l_RandomPair]).ToString();
                        m_SecondPair.RemoveAt(l_RandomPair);

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
