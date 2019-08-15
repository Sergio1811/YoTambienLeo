using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageControl : MonoBehaviour
{
    bool m_0touch = true;
    bool m_1touch = false;
    public GameObject m_WordText;
    public Image m_Image;
    public AudioSource m_AudioSource;
    GameManagerBit m_GMBit;

    void Start()
    {
        m_GMBit = GameObject.FindGameObjectWithTag("Bit").GetComponent<GameManagerBit>();
    }

    
    void Update()
    {
        if(Input.touchCount>0 && Input.GetTouch(0).phase == TouchPhase.Began && m_0touch)
        {
            m_WordText.SetActive(true);
            //m_AudioSource.clip = ;
            m_AudioSource.Play();
            m_0touch = false;
            m_1touch = true;
            print("0 done");
        }

        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && m_1touch)
        {
            //highlight marco 
            //m_Image.sprite = ;
            m_1touch = false;
            print("1done");
        }

        else if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !m_0touch && !m_1touch)
        {
            m_GMBit.NextImage();
            print("pasamoh");
        }
    }
}
