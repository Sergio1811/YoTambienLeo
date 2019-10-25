using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageControl : MonoBehaviour
{
    bool m_0touch = true;
    bool m_1touch = false;
    Animation m_Animation;
    GameManagerBit m_GMBit;
    public AnimationClip m_Spin;
    public AnimationClip m_Slide;
    public List<Sprite> m_ImagesPool = new List<Sprite>();
    public List<Sprite> m_ImagesPool2 = new List<Sprite>();
    public List<string> m_palabras = new List<string>();
    public List<AudioClip> m_audios = new List<AudioClip>();

    public static int m_Length;
    public Image m_Image;
    public Image m_ImageBehind;
    public Text m_Text;
    public List<Font> ourFonts = new List<Font>();
    public AudioSource m_AS;
    public int l_Number;

    public ManagementBD managementBD;
    private List<PalabraBD> conjuntoDePalabrasBD = new List<PalabraBD>();

    void Awake()
    {

        m_GMBit = GameObject.FindGameObjectWithTag("Bit").GetComponent<GameManagerBit>();

    }

    void Start()
    {
        GameObject management = GameObject.FindGameObjectWithTag("BD");
        if (management != null)
        {
            managementBD = management.GetComponent<ManagementBD>();
            //aqui modificar depende de lo que quieras
            conjuntoDePalabrasBD = managementBD.ReadSQlitePalabra();
            m_ImagesPool.Clear();
            m_ImagesPool2.Clear();
            m_palabras.Clear();
            m_audios.Clear();
            foreach (PalabraBD p in conjuntoDePalabrasBD)
            {
                m_ImagesPool.Add(p.GetSprite(p.image1));
                m_ImagesPool2.Add(p.GetSprite(p.image2));
                m_palabras.Add(p.palabraActual);
                m_audios.Add(p.GetAudioClip(p.audio));

            }
        }
        m_Length = m_audios.Count;

        if (m_GMBit.repetir)
        {
            l_Number = m_GMBit.numLastImage;
            m_GMBit.repetir = false;
        }
        else
        {
            bool same = true;
            while (same)
            {
                int random = Random.Range(0, m_Length);

                if (random != m_GMBit.numLastImage)
                {
                    GameManagerBit.m_Alea = random;
                    l_Number = GameManagerBit.m_Alea;
                    same = false;
                    m_GMBit.numLastImage = l_Number;    
                }
            }
        }

        m_Animation = GetComponent<Animation>();
        print("number  " + l_Number);
        m_Image.sprite = m_ImagesPool[l_Number];
        m_ImageBehind.sprite = m_ImagesPool2[l_Number];
        m_Text.text = m_palabras[l_Number];
        m_Text.font = SearchFont();
        //m_Text.fontSize = SingletonLenguage.GetInstance().ConvertSizeDependWords(m_Text.text);
        m_AS.clip = m_audios[l_Number];


    }


    void Update()
    {
        if (GameManager.Instance.InputRecieved() && m_0touch)
        {
            Vector3 positionInput;
            if (Input.touchCount > 0)
                positionInput = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            else
                positionInput = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            print((new Vector2(positionInput.x, positionInput.y) - new Vector2(gameObject.transform.position.x, gameObject.transform.position.y)).magnitude);
            if ((new Vector2(positionInput.x, positionInput.y) - new Vector2(gameObject.transform.position.x, gameObject.transform.position.y)).magnitude < 3f)
            {
                m_Animation.clip = m_Slide;
                m_Animation.Play();
                m_0touch = false;
                m_1touch = true;
                //print("0 done");
            }

        }

        else if (GameManager.Instance.InputRecieved() && m_1touch && !m_Animation.isPlaying && !m_AS.isPlaying)
        {
            Vector3 positionInput;
            if (Input.touchCount > 0)
                positionInput = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            else
                positionInput = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            print((new Vector2(positionInput.x, positionInput.y) - new Vector2(gameObject.transform.position.x, gameObject.transform.position.y)).magnitude);
            if ((new Vector2(positionInput.x, positionInput.y) - new Vector2(gameObject.transform.position.x, gameObject.transform.position.y)).magnitude <= 3f)
            {
                m_Animation.clip = m_Spin;
                m_Animation.Play();
                m_1touch = false;
                //print("1done");

                StartCoroutine(WaitSeconds(3f));
            }

        }

        IEnumerator WaitSeconds(float seconds)
        {
            //print(Time.time);
            yield return new WaitForSeconds(seconds);
            //print(Time.time);
            m_GMBit.ActivateButtons();
        }
    }


    private Font SearchFont()
    {
        switch (SingletonLenguage.GetInstance().GetFont())
        {
            case SingletonLenguage.OurFont.IMPRENTA:
                return ourFonts[0];
            case SingletonLenguage.OurFont.MANUSCRITA:
                return ourFonts[1];
            case SingletonLenguage.OurFont.MAYUSCULA:
                m_Text.text = m_Text.text.ToUpper();
                return ourFonts[2];
            default:
                return ourFonts[0];
        }
    }

}
