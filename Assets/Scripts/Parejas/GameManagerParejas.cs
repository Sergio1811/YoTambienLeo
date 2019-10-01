using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerParejas : MonoBehaviour
{
    public SceneManagement m_Scener;
    public GameObject m_Canvas;
    public GameObject m_RealCanvas;
    Animation m_Animation;
    public GameObject m_ImageZoom;
    public Image m_ImageZoomed;
    public Text m_TextZoomed;
    public Texture2D[] m_ImagePairs;
    public List<string> palabrasCastellano = new List<string>();
    public List<string> palabrasCatalan = new List<string>();
    public List<AudioClip> audiosCastellano = new List<AudioClip>();
    public List<AudioClip> audiosCatalan = new List<AudioClip>();
    List<Texture2D> RepeatList = new List<Texture2D>();
    int m_CurrentNumRep = 0;
    public static int m_CurrentPairs; 

    # region Separador
#endregion

    public GameObject m_Plantilla;
    public GameObject m_PlantillaPareja;
   

    public int m_NumPairs;
    private bool m_IsHorizontal;
    private float m_XPos;
    private float m_YPos;
    private bool m_FirstPair;

    #region Points
    public Sprite m_CompletedPoint;
    public Sprite m_IncompletedPoint;
    public Transform m_SpawnImpar;
    public Transform m_SpawnPar;
    Transform m_CurrentSpawn;
    public GameObject m_Point;
    static int l_NumReps = GameManager.Instance.m_NeededToMinigame;
    GameObject[] m_Points = new GameObject[l_NumReps];
    public GameObject m_Siguiente;
    public GameObject m_Repetir;
    #endregion

    void Start()
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

        for (int i = 0; i <= GameManager.m_CurrentToMinigame; i++)
        {
            m_Points[i].GetComponent<Image>().sprite = m_CompletedPoint;
        }

        InstantiatePairs();
        m_Animation = m_RealCanvas.GetComponent<Animation>();
    }

    private void Update()
    {
        print(m_CurrentPairs);
        print(m_NumPairs);
        if(m_CurrentPairs==m_NumPairs)
        {
            StartCoroutine(WaitSeconds(3));
            m_CurrentPairs = 0;
        }

        if (m_ImageZoom.activeSelf && GameManager.Instance.InputRecieved()&& !m_RealCanvas.GetComponent<Animation>().isPlaying)
            m_ImageZoom.SetActive(false);
    }

    public void InstantiatePairs ()
    {
        m_Points[GameManager.m_CurrentToMinigame].GetComponent<Image>().sprite = m_CompletedPoint;
        m_CurrentNumRep = 0;

        List<Texture2D> l_Pairs = new List<Texture2D>();
        foreach (Texture2D item in m_ImagePairs)
        {
            l_Pairs.Add(item);
        }

        List<Texture2D> l_SecondPair = new List<Texture2D>();
        List<Texture2D> l_ThirdPair = new List<Texture2D>();
        RepeatList = l_ThirdPair;

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
                        int l_RandomPair = Random.Range(0, l_Pairs.Count);
                        GameObject m_NewPair = Instantiate(m_PlantillaPareja, m_Canvas.transform);
                        m_NewPair.GetComponent<Image>().sprite = Sprite.Create(l_Pairs[l_RandomPair], new Rect(0, 0, 400, 400), Vector2.zero);
                        m_NewPair.GetComponent<RectTransform>().anchoredPosition = l_NewPosition;
                        m_NewPair.name = j.ToString();

                        l_SecondPair.Add(l_Pairs[l_RandomPair]);
                        l_ThirdPair.Add(l_Pairs[l_RandomPair]);
                        l_Pairs.RemoveAt(l_RandomPair);

                        k++;
                        k++;
                    }
                }
                else
                {
                    for (int j = 0; j < m_NumPairs; j++)
                    {
                        Vector3 l_NewPosition = new Vector3(m_XPos * k, m_YPos, 0f);
                        int l_RandomPair = Random.Range(0, l_SecondPair.Count);
                        GameObject m_NewPair = Instantiate(m_Plantilla, m_Canvas.transform);
                        m_NewPair.GetComponent<Image>().sprite = Sprite.Create(l_SecondPair[l_RandomPair], new Rect(0, 0, 400, 400), Vector2.zero);
                        m_NewPair.GetComponent<RectTransform>().anchoredPosition = l_NewPosition;
                        m_NewPair.name = l_ThirdPair.IndexOf(l_SecondPair[l_RandomPair]).ToString();
                        l_SecondPair.RemoveAt(l_RandomPair);

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
                        int l_RandomPair = Random.Range(0, l_Pairs.Count);
                        GameObject m_NewPair = Instantiate(m_Plantilla, m_Canvas.transform);
                        m_NewPair.GetComponent<Image>().sprite = Sprite.Create(l_Pairs[l_RandomPair], new Rect(0, 0, 400, 400), Vector2.zero);
                        m_NewPair.GetComponent<RectTransform>().anchoredPosition = l_NewPosition;
                        m_NewPair.name = j.ToString();

                        l_SecondPair.Add(l_Pairs[l_RandomPair]);
                        l_ThirdPair.Add(l_Pairs[l_RandomPair]);
                        l_Pairs.RemoveAt(l_RandomPair);

                        k++;
                        k++;

                    }
                }
                else
                {
                    for (int j = 0; j < m_NumPairs; j++)
                    {
                        Vector3 l_NewPosition = new Vector3(m_XPos, m_YPos * k, 0f);
                        int l_RandomPair = Random.Range(0, l_SecondPair.Count);
                        GameObject m_NewPair = Instantiate(m_PlantillaPareja, m_Canvas.transform);
                        m_NewPair.GetComponent<Image>().sprite = Sprite.Create(l_SecondPair[l_RandomPair], new Rect(0, 0, 400, 400), Vector2.zero);
                        m_NewPair.GetComponent<RectTransform>().anchoredPosition = l_NewPosition;

                        m_NewPair.name = l_ThirdPair.IndexOf(l_SecondPair[l_RandomPair]).ToString();
                        l_SecondPair.RemoveAt(l_RandomPair);

                        k++;
                        k++;
                    }
                }
                m_FirstPair = false;
                m_XPos *= 3f;
            }
        }
    }

    public void NextPairs()
    {
       
        GameManager.m_CurrentToMinigame++;
        m_Points[GameManager.m_CurrentToMinigame].GetComponent<Image>().sprite = m_CompletedPoint;
        m_CurrentNumRep = 0;

        if (GameManager.m_CurrentToMinigame >= GameManager.Instance.m_NeededToMinigame)
            m_Scener.RandomMinigame();
        else
        {
            List<Texture2D> l_Pairs = new List<Texture2D>();
            foreach (Texture2D item in m_ImagePairs)
            {
                l_Pairs.Add(item);
            }

            List<Texture2D> l_SecondPair = new List<Texture2D>();
            List<Texture2D> l_ThirdPair = new List<Texture2D>();
            RepeatList = l_ThirdPair;

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
                            int l_RandomPair = Random.Range(0, l_Pairs.Count);
                            GameObject m_NewPair = Instantiate(m_PlantillaPareja, m_Canvas.transform);
                            m_NewPair.GetComponent<Image>().sprite = Sprite.Create(l_Pairs[l_RandomPair], new Rect(0, 0, 400, 400), Vector2.zero);
                            m_NewPair.GetComponent<RectTransform>().anchoredPosition = l_NewPosition;
                            m_NewPair.name = j.ToString();

                            l_SecondPair.Add(l_Pairs[l_RandomPair]);
                            l_ThirdPair.Add(l_Pairs[l_RandomPair]);
                            l_Pairs.RemoveAt(l_RandomPair);

                            k++;
                            k++;
                        }
                    }
                    else
                    {
                        for (int j = 0; j < m_NumPairs; j++)
                        {
                            Vector3 l_NewPosition = new Vector3(m_XPos * k, m_YPos, 0f);
                            int l_RandomPair = Random.Range(0, l_SecondPair.Count);
                            GameObject m_NewPair = Instantiate(m_Plantilla, m_Canvas.transform);
                            m_NewPair.GetComponent<Image>().sprite = Sprite.Create(l_SecondPair[l_RandomPair], new Rect(0, 0, 400, 400), Vector2.zero);
                            m_NewPair.GetComponent<RectTransform>().anchoredPosition = l_NewPosition;
                            m_NewPair.name = l_ThirdPair.IndexOf(l_SecondPair[l_RandomPair]).ToString();
                            l_SecondPair.RemoveAt(l_RandomPair);

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
                            int l_RandomPair = Random.Range(0, l_Pairs.Count);
                            GameObject m_NewPair = Instantiate(m_Plantilla, m_Canvas.transform);
                            m_NewPair.GetComponent<Image>().sprite = Sprite.Create(l_Pairs[l_RandomPair], new Rect(0, 0, 400, 400), Vector2.zero);
                            m_NewPair.GetComponent<RectTransform>().anchoredPosition = l_NewPosition;
                            m_NewPair.name = j.ToString();

                            l_SecondPair.Add(l_Pairs[l_RandomPair]);
                            l_ThirdPair.Add(l_Pairs[l_RandomPair]);
                            l_Pairs.RemoveAt(l_RandomPair);

                            k++;
                            k++;

                        }
                    }
                    else
                    {
                        for (int j = 0; j < m_NumPairs; j++)
                        {
                            Vector3 l_NewPosition = new Vector3(m_XPos, m_YPos * k, 0f);
                            int l_RandomPair = Random.Range(0, l_SecondPair.Count);
                            GameObject m_NewPair = Instantiate(m_PlantillaPareja, m_Canvas.transform);
                            m_NewPair.GetComponent<Image>().sprite = Sprite.Create(l_SecondPair[l_RandomPair], new Rect(0, 0, 400, 400), Vector2.zero);
                            m_NewPair.GetComponent<RectTransform>().anchoredPosition = l_NewPosition;

                            m_NewPair.name = l_ThirdPair.IndexOf(l_SecondPair[l_RandomPair]).ToString();
                            l_SecondPair.RemoveAt(l_RandomPair);

                            k++;
                            k++;
                        }
                    }
                    m_FirstPair = false;
                    m_XPos *= 3f;
                }
            }
        }
    }

    public void RepeatPairs()
    {
        m_CurrentNumRep++;

        List<Texture2D> l_Pairs = new List<Texture2D>();
        foreach (Texture2D item in RepeatList)
        {
            l_Pairs.Add(item);
        }

        List<Texture2D> l_SecondPair = new List<Texture2D>();
        List<Texture2D> l_ThirdPair = new List<Texture2D>();

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
                        int l_RandomPair = Random.Range(0, l_Pairs.Count);
                        GameObject m_NewPair = Instantiate(m_PlantillaPareja, m_Canvas.transform);
                        m_NewPair.GetComponent<Image>().sprite = Sprite.Create(l_Pairs[l_RandomPair], new Rect(0, 0, 400, 400), Vector2.zero);
                        m_NewPair.GetComponent<RectTransform>().anchoredPosition = l_NewPosition;
                        m_NewPair.name = j.ToString();

                        l_SecondPair.Add(l_Pairs[l_RandomPair]);
                        l_ThirdPair.Add(l_Pairs[l_RandomPair]);
                        l_Pairs.RemoveAt(l_RandomPair);

                        k++;
                        k++;
                    }
                }
                else
                {
                    for (int j = 0; j < m_NumPairs; j++)
                    {
                        Vector3 l_NewPosition = new Vector3(m_XPos * k, m_YPos, 0f);
                        int l_RandomPair = Random.Range(0, l_SecondPair.Count);
                        GameObject m_NewPair = Instantiate(m_Plantilla, m_Canvas.transform);
                        m_NewPair.GetComponent<Image>().sprite = Sprite.Create(l_SecondPair[l_RandomPair], new Rect(0, 0, 400, 400), Vector2.zero);
                        m_NewPair.GetComponent<RectTransform>().anchoredPosition = l_NewPosition;
                        m_NewPair.name = l_ThirdPair.IndexOf(l_SecondPair[l_RandomPair]).ToString();
                        l_SecondPair.RemoveAt(l_RandomPair);

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
                        int l_RandomPair = Random.Range(0, l_Pairs.Count);
                        GameObject m_NewPair = Instantiate(m_Plantilla, m_Canvas.transform);
                        m_NewPair.GetComponent<Image>().sprite = Sprite.Create(l_Pairs[l_RandomPair], new Rect(0, 0, 400, 400), Vector2.zero);
                        m_NewPair.GetComponent<RectTransform>().anchoredPosition = l_NewPosition;
                        m_NewPair.name = j.ToString();

                        l_SecondPair.Add(l_Pairs[l_RandomPair]);
                        l_ThirdPair.Add(l_Pairs[l_RandomPair]);
                        l_Pairs.RemoveAt(l_RandomPair);

                        k++;
                        k++;

                    }
                }
                else
                {
                    for (int j = 0; j < m_NumPairs; j++)
                    {
                        Vector3 l_NewPosition = new Vector3(m_XPos, m_YPos * k, 0f);
                        int l_RandomPair = Random.Range(0, l_SecondPair.Count);
                        GameObject m_NewPair = Instantiate(m_PlantillaPareja, m_Canvas.transform);
                        m_NewPair.GetComponent<Image>().sprite = Sprite.Create(l_SecondPair[l_RandomPair], new Rect(0, 0, 400, 400), Vector2.zero);
                        m_NewPair.GetComponent<RectTransform>().anchoredPosition = l_NewPosition;

                        m_NewPair.name = l_ThirdPair.IndexOf(l_SecondPair[l_RandomPair]).ToString();
                        l_SecondPair.RemoveAt(l_RandomPair);

                        k++;
                        k++;
                    }
                }
                m_FirstPair = false;
                m_XPos *= 3f;
            }
        }
    }

    public void ActivateButtons()
    {
        m_Siguiente.SetActive(true);
        if (m_CurrentNumRep <= GameManager.Instance.Repeticiones)
            m_Repetir.SetActive(true);
    }

    IEnumerator WaitSeconds(float seconds)
    {
        print(Time.time);
        yield return new WaitForSeconds(seconds);
        print(Time.time);
        ActivateButtons();
    }

    public void PairDone()
    {
        m_CurrentPairs++;
        m_Animation.Play();
    }

    private string PutName(int _name)
    {
        switch(SingletonLenguage.GetInstance().GetLenguage())
        {
            case SingletonLenguage.Lenguage.CASTELLANO:
                return palabrasCastellano[_name];
            case SingletonLenguage.Lenguage.CATALAN:
                return palabrasCatalan[_name];
            default:
                return palabrasCastellano[_name];
        }
    }
}
