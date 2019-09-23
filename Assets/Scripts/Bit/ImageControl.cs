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
    public Image m_Image;
    public Image m_ImageBehind;
    public Text m_Text;


    void Start()
    {
        int l_Number = Random.Range(0, m_ImagesPool.Count);
        m_GMBit = GameObject.FindGameObjectWithTag("Bit").GetComponent<GameManagerBit>();
        m_Animation = GetComponent<Animation>();
        m_Image.sprite = Sprite.Create(m_ImagesPool[l_Number], new Rect(0,0,512,512), Vector2.zero);
        m_ImageBehind.sprite = Sprite.Create(m_ImagesPool2[l_Number], new Rect(0, 0, 512, 512), Vector2.zero);
        m_Text.text = m_ImagesPool[l_Number].name;

    }

    
    void Update()
    {
        if(GameManager.Instance.InputRecieved() && m_0touch)
        {
            m_Animation.clip = m_Slide;
            m_Animation.Play();
            m_0touch = false;
            m_1touch = true;
            print("0 done");
        }

        else if (GameManager.Instance.InputRecieved() && m_1touch && !m_Animation.isPlaying)
        {
            m_Animation.clip = m_Spin;
            m_Animation.Play();
            m_1touch = false;
            print("1done");

            StartCoroutine(WaitSeconds(3f));
           
        }

      IEnumerator WaitSeconds(float seconds)
        {
            print(Time.time);
            yield return new WaitForSeconds(seconds);
            print(Time.time);
            m_GMBit.ActivateButtons();
        }
    }

}
