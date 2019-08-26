using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveTouch : MonoBehaviour
{
    bool m_PieceLocked = false;
    bool m_PieceClicked = false;

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
                    }
                }
             
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                touchPosition.z = 0f;

                RaycastHit2D l_RaycastHit = Physics2D.Raycast(touchPosition, Camera.main.transform.forward);
                if (l_RaycastHit)
                {
                    if (l_RaycastHit.collider.gameObject == this.gameObject)
                    {
                        m_PieceClicked = true;
                    }
                }
            }
        }

        if(m_PieceClicked)
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

        if (m_PieceClicked && (Input.GetMouseButtonUp(0) || (Input.touchCount>0 && Input.GetTouch(0).phase == TouchPhase.Ended)))
        {
            m_PieceClicked = false;
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if((collision.gameObject.name == this.gameObject.name) && (Input.touchCount==0 && Input.GetMouseButtonUp(0)) && !m_PieceLocked)
        {
            this.transform.position = collision.gameObject.transform.position;
            m_PieceLocked = true;
            GameObject.FindGameObjectWithTag("GameManagerPuzzle").GetComponent<GameManagerPuzzle>().m_Points++;
        }
    }
}
