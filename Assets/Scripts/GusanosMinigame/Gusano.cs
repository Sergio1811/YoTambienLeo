using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gusano : MonoBehaviour
{

    public GameObject m_Mariposa;

    private float m_Speed = 0;

    public Sprite m_Gusano_01;
    public Sprite m_Gusano_02;

    private bool m_SpriteGusano01;
    private float m_TimePassed;

    // Start is called before the first frame update
    void Start()
    {
        m_SpriteGusano01 = false;
        m_TimePassed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * Time.deltaTime * m_Speed;

        if(m_SpriteGusano01 && m_TimePassed>0.5f)
        {
            GetComponent<SpriteRenderer>().sprite = m_Gusano_02;
            m_TimePassed = 0;
            m_SpriteGusano01 = false;
            transform.position += new Vector3(0, 0.4f, 0);
            m_Speed = 0;
        }

        if(!m_SpriteGusano01 && m_TimePassed>0.5f)
        {
            GetComponent<SpriteRenderer>().sprite = m_Gusano_01;
            m_TimePassed = 0;
            m_SpriteGusano01 = true;
            transform.position -= new Vector3(0, 0.4f, 0);
            m_Speed = 2f;
        }

        m_TimePassed += Time.deltaTime;

        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Vector3 l_Ray = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            RaycastHit2D l_Hit = Physics2D.Raycast(l_Ray, Vector2.zero);
            if (l_Hit.collider != null)
            {
                if (l_Hit.collider.tag == "Gusano")
                {
                    Debug.Log("TAPPED");
                    //mariposa
                    Instantiate(m_Mariposa, l_Hit.collider.gameObject.transform.position, Quaternion.identity);
                    Destroy(l_Hit.collider.gameObject);
                }
            }
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 l_Ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D l_Hit = Physics2D.Raycast(l_Ray, Vector2.zero);
            if (l_Hit.collider != null)
            {
                if (l_Hit.collider.tag == "Gusano")
                {
                    Debug.Log("ECLOSION");
                    //mariposa
                    Instantiate(m_Mariposa, l_Hit.collider.gameObject.transform.position, Quaternion.identity);
                    Destroy(l_Hit.collider.gameObject);
                }
            }
        }
    }
}
