using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerPuzzle : MonoBehaviour
{
    public GameObject m_ImageTemplate;
    public GameObject m_ColliderTemplate;
    public GameObject m_CollidersSpawns;
    public GameObject m_ImagesSpawn;
    public GameObject m_Canvas;

    [HideInInspector]
    public int m_Points=0;
    public
    int m_NumPieces= 4;
    int m_NumPiecesX;
    int m_NumPiecesY;
    bool m_Completed;
   



    private void Start()
    {
        if (Mathf.Sqrt(m_NumPieces) / (int)Mathf.Sqrt(m_NumPieces) == 1)
        {
            m_NumPiecesX = (int)Mathf.Sqrt(m_NumPieces);
            m_NumPiecesY = (int)Mathf.Sqrt(m_NumPieces);
        }
        else
        {
            m_NumPiecesX = (int)Mathf.Sqrt(m_NumPieces);
            m_NumPiecesY = (int)Mathf.Sqrt(m_NumPieces) + 1;
        }

        ImagesCollsInstantiation();
    }

    private void Update()
    {
        if(!m_Completed)
        PuzzleComplete();

        if (m_Canvas.activeSelf && (Input.touchCount > 0 || Input.GetMouseButtonDown(0)))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void PuzzleComplete()
    {
        if(m_Points == m_NumPieces)
        {
            AudioSource l_AS = GetComponent<AudioSource>();
            m_Canvas.SetActive(true);
            //l_AS.clip = ;
            l_AS.Play();
            m_Completed = true;
        }
    }

    public void ImagesCollsInstantiation()
    {
        RectTransform l_Colliders = m_CollidersSpawns.GetComponent<RectTransform>();
        RectTransform l_Images = m_ImagesSpawn.GetComponent<RectTransform>();
        float sizeX = -l_Colliders.sizeDelta.x /(m_NumPiecesX);
        float sizeY = l_Colliders.sizeDelta.y /m_NumPiecesY;
        int l_CurrentPiece = 0;

        for (int i = 0; i < m_NumPiecesY; i++)
        {
            sizeX = -l_Colliders.sizeDelta.x / (m_NumPiecesX);
            sizeY -= l_Colliders.sizeDelta.y / m_NumPiecesY;

            for (int j = 0; j < m_NumPiecesX; j++)
            {
                sizeX += l_Colliders.sizeDelta.x / m_NumPiecesX;

                GameObject local = Instantiate(m_ImageTemplate, m_ImagesSpawn.transform);
                local.name = l_CurrentPiece.ToString();
                local.GetComponent<Image>().SetNativeSize();
                local.GetComponent<RectTransform>().anchoredPosition =
                    new Vector2(Random.Range(0, l_Images.sizeDelta.x - l_Images.sizeDelta.x/m_NumPiecesX/2), Random.Range(0, l_Images.sizeDelta.y - l_Images.sizeDelta.x / m_NumPiecesY / 2));

                GameObject local2 = Instantiate(m_ColliderTemplate, m_CollidersSpawns.transform);
                local2.name = l_CurrentPiece.ToString();
                local2.GetComponent<RectTransform>().sizeDelta = new Vector2(l_Colliders.sizeDelta.x/m_NumPiecesX, l_Colliders.sizeDelta.y/m_NumPiecesY);
                local2.GetComponent<RectTransform>().anchoredPosition = new Vector2(sizeX, sizeY);
                
                l_CurrentPiece++;
            }
        }
    }
}
