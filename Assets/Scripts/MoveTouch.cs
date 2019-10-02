using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MoveTouch : MonoBehaviour
{
    [HideInInspector]
    public bool m_PieceLocked = false;
    bool m_PieceClicked = false;
    private Vector3 m_ClickedPiecePosition;
    private Image myImage;
    public bool Word = false;

    void Start()
    {
        myImage = gameObject.GetComponent<Image>();
    }

    void Update()
    {
        if (!m_PieceLocked && !m_PieceClicked)
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
                if (!Word)
                    this.transform.position = touchPosition - new Vector3(myImage.rectTransform.rect.width / 256, -myImage.rectTransform.rect.height / 256);
                else
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
            if(!m_PieceLocked)
                this.transform.position = m_ClickedPiecePosition;

            m_PieceClicked = false;
        }

    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.gameObject.name == this.gameObject.name) && (Input.touchCount == 0 && Input.GetMouseButtonUp(0)) && !m_PieceLocked)
        {
            this.transform.position = collision.gameObject.transform.position;
            m_PieceLocked = true;
            if(SceneManager.GetActiveScene().name=="Puzzle")
            GameObject.FindGameObjectWithTag("GameManagerPuzzle").GetComponent<GameManagerPuzzle>().m_Puntuacion++;
        }
    }


}
