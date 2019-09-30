using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using UnityEngine.Windows;

public class PhoneCamera : MonoBehaviour
{

    private bool m_CamAvaible;
    private WebCamTexture m_BackCam;
    private Texture m_DefaultBackground;

    public RawImage m_Background;
    public AspectRatioFitter fit;

    public ManagementBDUser managementBDUser;
    public Image imagen;
    public Text text;
    private Texture2D texture;
    private string data;

    void Start()
    {
        print(Application.persistentDataPath);
        m_DefaultBackground = m_Background.texture;
        WebCamDevice[] m_Devices = WebCamTexture.devices;

        if (m_Devices.Length == 0)
        {
            Debug.Log("No camera detected");
            m_CamAvaible = false;
            return;
        }

        for (int i = 0; i < m_Devices.Length; i++)
        {

            if (!m_Devices[i].isFrontFacing)
            {
                m_BackCam = new WebCamTexture(m_Devices[i].name, Screen.width, Screen.height);
            }
        }

        if (m_BackCam == null)
        {
            Debug.Log("Unable to find back camera");
            return;
        }

        m_BackCam.Play();
        m_Background.texture = m_BackCam;

        m_CamAvaible = true;
    }

    void Update()
    {
        if (!m_CamAvaible)
            return;

        float l_Ratio = (float)m_BackCam.width / (float)m_BackCam.height;
        fit.aspectRatio = l_Ratio;

        float l_ScaleY = m_BackCam.videoVerticallyMirrored ? -1f : 1f;
        m_Background.rectTransform.localScale = new Vector3(1f, l_ScaleY, 1f);

        int l_Orientation = -m_BackCam.videoRotationAngle;
        m_Background.rectTransform.localEulerAngles = new Vector3(0, 0, l_Orientation);

        if (Input.GetKeyDown(KeyCode.P) || ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began)))
        {
            TakeAShot();
        }
    }


    public void TakeAShot()
    {
        data = System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        StartCoroutine("TakePicture");
        text.text = File.Exists(managementBDUser.ruteFolderImage + "UserPhoto" + data + ".png").ToString(); ;
        managementBDUser.SearchSpriteInRuteFolders("UserPhoto" + data + ".png", imagen);

    }

    IEnumerator TakePicture()
    {
        ScreenCapture.CaptureScreenshot("UserPhoto" + data + ".png");
        yield return new WaitForEndOfFrame();
    }

}
