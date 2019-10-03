using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pairs : MonoBehaviour
{
    [HideInInspector]
    bool m_PieceClicked = false;
    public OnlyOneManager managerOnlyOne;
    private Vector3 m_ClickedPiecePosition;
    public string nombre = "";
    public AudioClip audioClip;
    private Image myImage;

    public GameManagerParejas m_GameManagerParejas;
    private AudioSource audioSource;
    private float timer = 0;

    private void Start()
    {
        m_GameManagerParejas = GameObject.FindGameObjectWithTag("GMParejas").GetComponent<GameManagerParejas>();
        audioSource = m_GameManagerParejas.GetComponent<AudioSource>();
        myImage = gameObject.GetComponent<Image>();
    }
    private void Update()
    {
        if (managerOnlyOne != null)
        {
            if (!m_PieceClicked)
            {
                if (Input.touchCount > 0 && managerOnlyOne.go == null && !m_GameManagerParejas.m_Animation.isPlaying)
                {
                    Touch touch = Input.GetTouch(0);
                    Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                    touchPosition.z = 0f;

                    RaycastHit2D l_RaycastHit = Physics2D.Raycast(touchPosition, Camera.main.transform.forward);
                    if (l_RaycastHit)
                    {
                        if (l_RaycastHit.collider.gameObject == this.gameObject)
                        {
                            m_PieceClicked = true;
                            this.gameObject.transform.SetAsLastSibling();
                            m_ClickedPiecePosition = this.gameObject.transform.position;
                            managerOnlyOne.Catch(true, gameObject);
                        }
                    }

                }

                if (Input.GetMouseButtonDown(0) && managerOnlyOne.go == null && !m_GameManagerParejas.m_Animation.isPlaying)
                {
                    Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    touchPosition.z = 0f;

                    RaycastHit2D l_RaycastHit = Physics2D.Raycast(touchPosition, Camera.main.transform.forward);
                    if (l_RaycastHit)
                    {
                        if (l_RaycastHit.collider.gameObject == this.gameObject)
                        {
                            m_PieceClicked = true;
                            this.gameObject.transform.SetAsLastSibling();
                            m_ClickedPiecePosition = this.gameObject.transform.position;
                            managerOnlyOne.Catch(true, gameObject);

                        }
                    }
                }
            }

            if (m_PieceClicked)
            {
                if (Input.GetMouseButton(0))
                {
                    Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    touchPosition.z = 0f;
                    this.transform.position = touchPosition - new Vector3(myImage.rectTransform.rect.width / 200, myImage.rectTransform.rect.height / 200);
                }

                else if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved))
                {
                    Touch touch = Input.GetTouch(0);
                    Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                    touchPosition.z = 0f;
                    this.transform.position = touchPosition;
                }
            }

            if (timer > 0)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    timer = 0;
                    this.transform.position = m_ClickedPiecePosition;
                    m_PieceClicked = false;
                    managerOnlyOne.Catch(false, null);
                }

            }

            if (m_PieceClicked && (Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)) && timer == 0)
            {
                timer = 0.2f;
            }
        }

    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.gameObject.name == this.gameObject.name) && ((Input.touchCount>0 && Input.GetTouch(0).phase==TouchPhase.Ended )|| Input.GetMouseButtonUp(0)))
        {
            this.transform.position = collision.gameObject.transform.position;
            m_GameManagerParejas.m_ImageZoomed.sprite= this.gameObject.GetComponent<Image>().sprite;
            m_GameManagerParejas.m_TextZoomed.text = nombre;
            m_GameManagerParejas.m_TextZoomed.GetComponent<ConvertFont>().Convert();
            if (!audioSource.isPlaying)
            {
                audioSource.clip = audioClip;
                audioSource.Play();
            }
            m_GameManagerParejas.PairDone();
            Destroy(collision.gameObject);
            Destroy(this.gameObject);

        }
    }
}
