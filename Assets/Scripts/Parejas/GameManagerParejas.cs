using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerParejas : MonoBehaviour
{

    public GameObject m_LastSelected;
    public GameObject m_ActualSelected;

    public GameObject m_Naranja;
    public GameObject m_Rojo;
    public GameObject m_Azul;
    public GameObject m_Verde;

    public GameObject[] m_Positions = new GameObject[8];

    // Start is called before the first frame update
    void Start()
    {
        int l_Counter = 0;

        for (int i = 0; i < m_Positions.Length; i++)
        {
            if (l_Counter < 2)
            {
                Instantiate(m_Naranja, (m_Positions[i].transform.position), Quaternion.identity);
                l_Counter++;
            }
            else if (l_Counter < 4)
            {
                Instantiate(m_Azul, (m_Positions[i].transform.position), Quaternion.identity);
                l_Counter++;
            }
            else if (l_Counter < 6)
            {
                Instantiate(m_Verde, (m_Positions[i].transform.position), Quaternion.identity);
                l_Counter++;
            }
            else
            {
                Instantiate(m_Rojo, (m_Positions[i].transform.position), Quaternion.identity);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            m_LastSelected = m_ActualSelected;

            Vector3 l_Ray = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            RaycastHit2D l_Hit = Physics2D.Raycast(l_Ray, Vector2.zero);

            if (l_Hit.collider != null)
            {
                m_ActualSelected = l_Hit.collider.gameObject;
            }

        }

        if (m_LastSelected != null)
        {
            if (m_LastSelected.name == m_ActualSelected.name)
            {

                //imagen
                //texto
                //audio

                Destroy(m_ActualSelected);
                Destroy(m_LastSelected);
            }
        }
    }
}
