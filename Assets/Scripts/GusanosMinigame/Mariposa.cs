using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mariposa : MonoBehaviour
{

    private float m_Speed = 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0.5f, 0.5f, 0) * Time.deltaTime * m_Speed;
    }
}
