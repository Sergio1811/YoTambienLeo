using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    void Start()
    {
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
        StartCoroutine("TakePicture");
        imagen.color = Color.white;
        managementBDUser.InsertPalabra("pene", "pe-ne", "UserPhoto" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png", "audio", 0);
        managementBDUser.SearchSpriteInRuteFolders("UserPhoto" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png", imagen);
        //List<PalabraFraseUsuarioBD> prueba = managementBDUser.ReadSQlitePalabra();
        text.text = prueba.Count.ToString();
        text.enabled = true;
    }

    IEnumerator TakePicture()
    {
        ScreenCapture.CaptureScreenshot("UserPhoto" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png");
        yield return new WaitForEndOfFrame();
    }

    public void SearchSpriteInRuteFolders(string _rute, Image _image)
    {
        imagen = _image;
        string completeRute = managementBDUser.ruteFolderImage + _rute;
        StartCoroutine(ConvertURLToTexture(completeRute));
    }


    //PARA PASAR DE UNA IMAGEN WEB A UN SPRITE, LLAMANDO CON UNA CORUTINE A ESTO

    IEnumerator ConvertURLToTexture(string _rute)
    {
        WWW www = new WWW(_rute); //Cargando la imagen
        yield return www;

        texture = www.texture; //una vez cargada 
        PassTexture2DToSprite();
    }

    private void PassTexture2DToSprite()
    {
        Rect rect = new Rect(new Vector2(0, 0), new Vector2(texture.width, texture.height));
        imagen.sprite = Sprite.Create(texture, rect, Vector2.down);
    }
}
