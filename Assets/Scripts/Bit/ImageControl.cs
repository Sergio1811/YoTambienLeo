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
    public List<Texture2D> m_ImagesPool = new List<Texture2D>();
    public List<Texture2D> m_ImagesPool2 = new List<Texture2D>();
    public List<string> m_PalabrasCastellano = new List<string>();
    public List<string> m_PalabrasCatalan = new List<string>();
    public List<AudioClip> m_AudioPoolCastellano = new List<AudioClip>();
    public List<AudioClip> m_AudioPoolCatalan = new List<AudioClip>();

    public static int m_Length;
    public Image m_Image;
    public Image m_ImageBehind;
    public Text m_Text;
    public List<Font> ourFonts = new List<Font>();
    public AudioSource m_AS;
    public int l_Number;
    void Awake()
    {
        m_Length = m_AudioPoolCastellano.Count;
        m_GMBit = GameObject.FindGameObjectWithTag("Bit").GetComponent<GameManagerBit>();
        GameManagerBit.m_Alea = Random.Range(0, m_Length);
    }

    void Start()
    {

        if (m_GMBit.repetir)
        {
            l_Number = m_GMBit.numLastImage;
            m_GMBit.repetir = false;
        }
        else
            l_Number = GameManagerBit.m_Alea;

        m_Animation = GetComponent<Animation>();
        print("number  " + l_Number);
        m_Image.sprite = Sprite.Create(m_ImagesPool[l_Number], new Rect(0, 0, m_ImagesPool[l_Number].width / 1.02f, m_ImagesPool[l_Number].height / 1.02f), Vector2.zero);
        m_ImageBehind.sprite = Sprite.Create(m_ImagesPool2[l_Number], new Rect(0, 0, m_ImagesPool[l_Number].width / 1.02f, m_ImagesPool[l_Number].height / 1.02f), Vector2.zero);
        m_Text.text = PutName(l_Number);
        m_Text.font = SearchFont();
        m_Text.fontSize = SingletonLenguage.GetInstance().ConvertSizeDependWords(m_Text.text);
        m_AS.clip = PutAudio(l_Number);


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

    private string PutName(int _alea)
    {
        switch (SingletonLenguage.GetInstance().GetLenguage())
        {
            case SingletonLenguage.Lenguage.CASTELLANO:
                return m_PalabrasCastellano[_alea];
            case SingletonLenguage.Lenguage.CATALAN:
                return m_PalabrasCatalan[_alea];
            default:
                return m_PalabrasCastellano[_alea];
        }
    }

    private AudioClip PutAudio(int _alea)
    {
        switch (SingletonLenguage.GetInstance().GetLenguage())
        {
            case SingletonLenguage.Lenguage.CASTELLANO:
                return m_AudioPoolCastellano[_alea];
            case SingletonLenguage.Lenguage.CATALAN:
                return m_AudioPoolCatalan[_alea];
            default:
                return m_AudioPoolCastellano[_alea];

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
