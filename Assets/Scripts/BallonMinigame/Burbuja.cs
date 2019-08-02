using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burbuja : MonoBehaviour
{

    private float m_Speed = 2;
    private float m_GrowSpeed = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.position += new Vector3(-0.2f,1,0) * Time.deltaTime * m_Speed;


        if(transform.localScale.magnitude < Vector3.one.magnitude + Random.Range(-2,2))
        transform.localScale += Vector3.one * Time.deltaTime * m_GrowSpeed;

        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Ray l_Ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit l_Hit;
            if (Physics.Raycast(l_Ray, out l_Hit))
            {
                if (l_Hit.collider.tag == "Burbuja")
                {
                    Debug.Log("TAPPED");
                    Destroy(l_Hit.collider.gameObject);
                }
            }
        }

        if (Input.GetMouseButton(0))
        {
            Ray l_Ray;
            RaycastHit l_Hit;
            l_Ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(l_Ray, out l_Hit))
            {
                if (l_Hit.collider.tag == "Burbuja")
                {
                    Debug.Log("PUM");
                    Destroy(l_Hit.collider.gameObject);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "DestroyBurbuja")
        {
            Destroy(gameObject);
        }
    }
}

