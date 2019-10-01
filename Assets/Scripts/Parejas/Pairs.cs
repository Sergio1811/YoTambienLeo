using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pairs : MonoBehaviour
{
    [HideInInspector]
    bool m_PieceClicked = false;

    private Vector3 m_ClickedPiecePosition;
    public string nombre = "";

    public GameManagerParejas m_GameManagerParejas;

    private void Start()
    {
        m_GameManagerParejas = GameObject.FindGameObjectWithTag("GMParejas").GetComponent<GameManagerParejas>();
    }
    private void Update()
    {
        if (!m_PieceClicked)
        {
            if (Input.touchCount > 0)
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
                        m_ClickedPiecePosition = this.gameObject.transform.position;
                    }
                }

            }

            if (Input.GetMouseButtonDown(0))
            {
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                touchPosition.z = 0f;

                RaycastHit2D l_RaycastHit = Physics2D.Raycast(touchPosition, Camera.main.transform.forward);
                if (l_RaycastHit)
                {
                    if (l_RaycastHit.collider.gameObject == this.gameObject)
                    {
                        m_PieceClicked = true;
                        m_ClickedPiecePosition = this.gameObject.transform.position;
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
                this.transform.position = touchPosition;
            }

            else if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved))
            {
                Touch touch = Input.GetTouch(0);
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                touchPosition.z = 0f;
                this.transform.position = touchPosition;
            }
        }

        if (m_PieceClicked && (Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)))
        {
            this.transform.position = m_ClickedPiecePosition;
            m_PieceClicked = false;
        }

    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.gameObject.name == this.gameObject.name) && ((Input.touchCount>0 && Input.GetTouch(0).phase==TouchPhase.Ended )|| Input.GetMouseButtonUp(0)))
        {
            this.transform.position = collision.gameObject.transform.position;
            m_GameManagerParejas.m_ImageZoomed.sprite= this.gameObject.GetComponent<Image>().sprite;
            m_GameManagerParejas.m_TextZoomed.text = nombre;
            m_GameManagerParejas.PairDone();
            Destroy(collision.gameObject);
            Destroy(this.gameObject);

        }
    }
}
