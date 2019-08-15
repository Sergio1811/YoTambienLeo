using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerBit : MonoBehaviour
{
    int m_CurrentNumRep = 0;
    public GameObject m_NewBit;
    public Transform m_NewBitPosition;
    public Sprite m_CompletedPoint;
    public Sprite m_IncompletedPoint;
    GameObject m_CurrentBit;

    public Transform m_SpawnImpar;
    public Transform m_SpawnPar;
    Transform m_CurrentSpawn;
    public GameObject m_Point;
    static int l_NumReps = GameManager.Instance.Repeticiones;
    GameObject[] m_Points = new GameObject[l_NumReps];

    private void Start()
    {
        print(m_Points.Length);
        if (l_NumReps % 2 == 0)
        {
            m_CurrentSpawn = m_SpawnPar;
            m_CurrentSpawn.GetComponent<RectTransform>().anchoredPosition -= new Vector2((75 * (l_NumReps / 2 - 1)), 0);
        }
        else
        {
            m_CurrentSpawn = m_SpawnImpar;
            m_CurrentSpawn.GetComponent<RectTransform>().anchoredPosition -= new Vector2((75 * (l_NumReps / 2)), 0);
        }

        for (int i = 0; i < l_NumReps; i++)
        {
                m_Points[i] = Instantiate(m_Point, m_CurrentSpawn.transform);
                m_Points[i].GetComponent<RectTransform>().anchoredPosition += new Vector2(m_Points[i].transform.position.x + (i * 75), 0);
        }

        NextImage();
    }
    public void NextImage()
    {
        if (m_CurrentNumRep < l_NumReps)
        {
            Destroy(m_CurrentBit);
            m_CurrentBit = Instantiate(m_NewBit, m_NewBitPosition);
            m_Points[m_CurrentNumRep].GetComponent<Image>().sprite = m_CompletedPoint;
            m_CurrentNumRep++;
            print("nextImage");
        }

        else
        {
            Destroy(m_CurrentBit);
            print("FinishRep");
        }
    }
}
