using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeTrial : MonoBehaviour
{
    public TrailRenderer m_Trail;

    private void Start()
    {
        m_Trail.material = GameManager.Instance.m_CurrentMaterial;
    }
    void Update()
    {
        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)|| Input.GetMouseButton(0))
        {
            Plane l_objPlane = new Plane(Camera.main.transform.forward * -1, this.transform.position);

            Ray l_Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float l_rayDistance;
            if (l_objPlane.Raycast(l_Ray, out l_rayDistance))
                this.transform.position = l_Ray.GetPoint(l_rayDistance);
        }
    }
}
