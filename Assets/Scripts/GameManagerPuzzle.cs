﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class GameManagerPuzzle : MonoBehaviour
{
    public List<Texture2D> m_ImagesPool = new List<Texture2D>();

    public SceneManagement m_Scener;
    int m_CurrentNumRep = 0;
    public GameObject m_ImageTemplate;
    public GameObject m_ColliderTemplate;
    public GameObject m_CollidersSpawns;
    public GameObject m_ImagesSpawn;
    public GameObject m_Canvas;

    Texture2D m_ImagePuzzle;
    public Text m_WordText;
    public Text m_UnseenWordText;

    [HideInInspector]
    public int m_Puntuacion=0;
    public
    int m_NumPieces= 4;
    int m_NumPiecesX;
    int m_NumPiecesY;
    bool m_Completed;

    public Sprite m_CompletedPoint;
    public Transform m_SpawnImpar;
    public Transform m_SpawnPar;
    Transform m_CurrentSpawn;
    public GameObject m_Point;
    static int l_NumReps = GameManager.Instance.m_NeededToMinigame;
    GameObject[] m_Points = new GameObject[l_NumReps];

    List<GameObject> m_Images = new List<GameObject>();
    List<GameObject> m_Colliders = new List<GameObject>();

    public GameObject m_Siguiente;
    public GameObject m_Repetir;

    private void Start()
    {
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

        for (int i = 0; i <= GameManager.m_CurrentToMinigame; i++)
        {
            m_Points[i].GetComponent<Image>().sprite = m_CompletedPoint;
        }

        PassPuzzle();
    }

    private void Update()
    {
        if (!m_Completed)
        {
            PuzzleComplete();
            m_Completed = false;
        }

        if (m_Canvas.activeSelf && (Input.touchCount > 0 || Input.GetMouseButtonDown(0)))
            PassPuzzle();

    }

    public void PuzzleComplete()
    { 
        if (m_Puntuacion == m_NumPieces+1)
        {
            AudioSource l_AS = GetComponent<AudioSource>();
            //l_AS.clip =;
            l_AS.Play();
            ActivateButtons();
            m_Completed = true;
        }
    }

    public void ImagesCollsInstantiation()
    {
        RectTransform l_Colliders = m_CollidersSpawns.GetComponent<RectTransform>();
        RectTransform l_Images = m_ImagesSpawn.GetComponent<RectTransform>();
        float sizeX = -l_Colliders.sizeDelta.x /(m_NumPiecesX);
        float sizeY = l_Colliders.sizeDelta.y /m_NumPiecesY;
        float l_Width = l_Colliders.sizeDelta.x / (m_NumPiecesX);
        float l_Height = l_Colliders.sizeDelta.y / m_NumPiecesY; 
        int l_CurrentPiece = 0;

        m_ImagePuzzle = m_ImagesPool[Random.Range(0, m_ImagesPool.Count)];
        m_WordText.text = m_ImagePuzzle.name;
        m_UnseenWordText.text = m_ImagePuzzle.name;

        Sprite l_SpriteImage;
        Rect rectImage = new Rect(new Vector2(0, 0), l_Colliders.sizeDelta);
        l_SpriteImage = Sprite.Create(m_ImagePuzzle, rectImage, l_Colliders.sizeDelta/2);
        m_CollidersSpawns.GetComponent<Image>().sprite = l_SpriteImage;

        for (int i = m_NumPiecesY-1; i >=0; i--)
        {
            sizeX = -l_Colliders.sizeDelta.x / (m_NumPiecesX);
            sizeY -= l_Colliders.sizeDelta.y / m_NumPiecesY;

            for (int j = 0; j < m_NumPiecesX; j++)
            {
                sizeX += l_Colliders.sizeDelta.x / m_NumPiecesX;

                Sprite l_Sprite;
                Rect rect = new Rect(new Vector2(j * l_Width, i * l_Height), new Vector2(l_Width, l_Height));
                l_Sprite = Sprite.Create(m_ImagePuzzle, rect, new Vector2(0, 0));

                #region ImageInstantiation
                GameObject local = Instantiate(m_ImageTemplate, m_ImagesSpawn.transform);
                m_Images.Add(local);
                local.name = (l_CurrentPiece).ToString();        
                local.GetComponent<Image>().sprite = l_Sprite;
                local.GetComponent<Image>().SetNativeSize();
                local.GetComponent<RectTransform>().pivot = new Vector2(0, 1);
                local.GetComponent<RectTransform>().anchoredPosition =
                    new Vector2(Random.Range(-((l_Images.sizeDelta.x - l_Width))/2, (l_Images.sizeDelta.x - l_Width)/2), Random.Range(-((l_Images.sizeDelta.y - l_Height)) / 2, (l_Images.sizeDelta.y - l_Height) / 2));
                local.GetComponent<BoxCollider2D>().offset = new Vector2(l_Width / 2, -l_Height / 2);
                local.GetComponent<BoxCollider2D>().size = new Vector2(l_Width, l_Height);
                #endregion

                #region ColliderInstantiation
                GameObject local2 = Instantiate(m_ColliderTemplate, m_CollidersSpawns.transform);
                m_Colliders.Add(local2);
                local2.name = l_CurrentPiece.ToString();
                local2.GetComponent<RectTransform>().sizeDelta = new Vector2(l_Colliders.sizeDelta.x/m_NumPiecesX, l_Colliders.sizeDelta.y/m_NumPiecesY);
                local2.GetComponent<RectTransform>().anchoredPosition = new Vector2(sizeX, sizeY);
                local2.GetComponent<BoxCollider2D>().offset = new Vector2(l_Width / 2, -l_Height / 2);
                local2.GetComponent<BoxCollider2D>().size = new Vector2(l_Width/8, l_Height/8);
                #endregion

                l_CurrentPiece++;
            }
        }
    }

    public void ImagesCollsInstantiationRepeat()
    {
        RectTransform l_Colliders = m_CollidersSpawns.GetComponent<RectTransform>();
        RectTransform l_Images = m_ImagesSpawn.GetComponent<RectTransform>();
        float sizeX = -l_Colliders.sizeDelta.x /(m_NumPiecesX);
        float sizeY = l_Colliders.sizeDelta.y /m_NumPiecesY;
        float l_Width = l_Colliders.sizeDelta.x / (m_NumPiecesX);
        float l_Height = l_Colliders.sizeDelta.y / m_NumPiecesY; 
        int l_CurrentPiece = 0;

        Sprite l_SpriteImage;
        Rect rectImage = new Rect(new Vector2(0, 0), l_Colliders.sizeDelta);
        l_SpriteImage = Sprite.Create(m_ImagePuzzle, rectImage, l_Colliders.sizeDelta/2);
        m_CollidersSpawns.GetComponent<Image>().sprite = l_SpriteImage;

        for (int i = m_NumPiecesY-1; i >=0; i--)
        {
            sizeX = -l_Colliders.sizeDelta.x / (m_NumPiecesX);
            sizeY -= l_Colliders.sizeDelta.y / m_NumPiecesY;

            for (int j = 0; j < m_NumPiecesX; j++)
            {
                sizeX += l_Colliders.sizeDelta.x / m_NumPiecesX;

                Sprite l_Sprite;
                Rect rect = new Rect(new Vector2(j * l_Width, i * l_Height), new Vector2(l_Width, l_Height));
                l_Sprite = Sprite.Create(m_ImagePuzzle, rect, new Vector2(0, 0));

                #region ImageInstantiation
                GameObject local = Instantiate(m_ImageTemplate, m_ImagesSpawn.transform);
                m_Images.Add(local);
                local.name = (l_CurrentPiece).ToString();        
                local.GetComponent<Image>().sprite = l_Sprite;
                local.GetComponent<Image>().SetNativeSize();
                local.GetComponent<RectTransform>().pivot = new Vector2(0, 1);
                local.GetComponent<RectTransform>().anchoredPosition =
                    new Vector2(Random.Range(-((l_Images.sizeDelta.x - l_Width))/2, (l_Images.sizeDelta.x - l_Width)/2), Random.Range(-((l_Images.sizeDelta.y - l_Height)) / 2, (l_Images.sizeDelta.y - l_Height) / 2));
                local.GetComponent<BoxCollider2D>().offset = new Vector2(l_Width / 2, -l_Height / 2);
                local.GetComponent<BoxCollider2D>().size = new Vector2(l_Width, l_Height);
                #endregion

                #region ColliderInstantiation
                GameObject local2 = Instantiate(m_ColliderTemplate, m_CollidersSpawns.transform);
                m_Colliders.Add(local2);
                local2.name = l_CurrentPiece.ToString();
                local2.GetComponent<RectTransform>().sizeDelta = new Vector2(l_Colliders.sizeDelta.x/m_NumPiecesX, l_Colliders.sizeDelta.y/m_NumPiecesY);
                local2.GetComponent<RectTransform>().anchoredPosition = new Vector2(sizeX, sizeY);
                local2.GetComponent<BoxCollider2D>().offset = new Vector2(l_Width / 2, -l_Height / 2);
                local2.GetComponent<BoxCollider2D>().size = new Vector2(l_Width/8, l_Height/8);
                #endregion

                l_CurrentPiece++;
            }
        }
    }

    /*public void CutIntoPieces(int l_NumPieces, int l_WidthPiece, int l_HeightPiece)
    {

        for (int i = 0; i < l_NumPieces; i++)
        {
            for (int j = 0; j < l_NumPieces; j++)
            {
                Sprite l_Sprite;
                Rect rect = new Rect(new Vector2(i * l_WidthPiece, j * l_HeightPiece), new Vector2(l_WidthPiece, l_HeightPiece));
                l_Sprite = Sprite.Create(m_ImagePuzzle, rect, new Vector2(0, 0));

                GameObject l_NewOne = Instantiate(m_Renderer, m_Canvas.transform);
                l_NewOne.GetComponent<RectTransform>().anchoredPosition = new Vector2(i * l_WidthPiece * 1.5f, j * l_HeightPiece * 1.5f);
                l_NewOne.GetComponent<Image>().sprite = l_Sprite;
            }
        }

    }*/

    public void PassPuzzle()
    {
        GameManager.m_CurrentToMinigame++;

        GameManager.m_CurrentToMinigame++;

        if (GameManager.m_CurrentToMinigame >= GameManager.Instance.m_NeededToMinigame)
            m_Scener.RandomMinigame();

        else 
        {
            foreach (GameObject item in m_Images)
            {
                Destroy(item);
            }

            foreach (GameObject item in m_Colliders)
            {
                Destroy(item);
            }

            m_Images.Clear();
            m_Colliders.Clear();
            m_Puntuacion = 0;
            m_Canvas.SetActive(false);
            ImagesCollsInstantiation();
            m_Points[m_CurrentNumRep].GetComponent<Image>().sprite = m_CompletedPoint;
            m_CurrentNumRep = 0;
          
        }

    }

    public void RepeatPuzzle()
    {
        foreach (GameObject item in m_Images)
        {
            Destroy(item);
        }

        foreach (GameObject item in m_Colliders)
        {
            Destroy(item);
        }
        m_Images.Clear();
        m_Colliders.Clear();
        m_Puntuacion = 0;
        m_Canvas.SetActive(false);
        m_CurrentNumRep++;
        ImagesCollsInstantiationRepeat();
    }

    public void ActivateButtons()
    {
        m_Siguiente.SetActive(true);
        if (m_CurrentNumRep <= GameManager.Instance.Repeticiones)
            m_Repetir.SetActive(true);
    }

}
