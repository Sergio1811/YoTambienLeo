using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateTrial : MonoBehaviour
{
    public GameObject m_SwipeTrial;
    GameObject m_CurrentTrial = null;

    private void Update()
    {
        if ((/*(/*Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) ||*/ Input.GetMouseButton(0)) && m_CurrentTrial == null)
        {
            print("Eentro");
            Ray l_Ray;
            RaycastHit l_Hit; 
            l_Ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(l_Ray, out l_Hit))
            {
                print("coll");
                m_CurrentTrial = Instantiate(m_SwipeTrial, l_Hit.point, Quaternion.identity);
            }
        }

        else if (m_CurrentTrial != null)
        {
           Destroy( m_CurrentTrial.GetComponent<SwipeTrial>());
            m_CurrentTrial = null;
        }


    }
}
