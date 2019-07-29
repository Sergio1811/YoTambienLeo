    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTrialColor : MonoBehaviour
{
    
    public Material m_RedColor;
    public Material m_BlueColor;
    public Material m_GreenColor;
    public Material m_YellowColor;

    public void ChangeColorRed()
    {
        GameManager.Instance.m_CurrentMaterial = m_RedColor;
    }

    public void ChangeColorBlue()
    {
        GameManager.Instance.m_CurrentMaterial = m_BlueColor;
    }

    public void ChangeColorGreen()
    {
        GameManager.Instance.m_CurrentMaterial = m_GreenColor;
    }

    public void ChangeColorYellow()
    {
        GameManager.Instance.m_CurrentMaterial = m_YellowColor;
    }
}
