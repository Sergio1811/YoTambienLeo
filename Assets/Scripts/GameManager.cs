using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    #region Configuracion
    public string Idioma = "Español";
    public Font TipoLetra;
    public bool Mayus = true;
    public bool Ayuda = true;
    public bool Dumi = true;
    public bool Articulo = false;
    public int Repeticiones = 2;
    public int Packs = 0;
    #endregion

    #region ScenesIndex
    public int PreparadosIndex;
    public int ListosIndex;
    public int YaIndex;
    public int GusanosIndex;
    public int BurbujasIndex;
    public int ColorIndex;
    public int InicioIndex;
    #endregion

    #region ButtonUI
    public Color m_BlackColor;
    public Color m_PurpleColor;
    public Color m_GrisColor;
    public Color m_WhiteColor;

    public Sprite ActiveButton;
    public Sprite DesactivateButton;
    #endregion

    #region WordAdding(Cambiar de Sitio)
    public string WordDifficulty;
    public string Word;
    #endregion

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(this);
        }

       
    }

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameManager();
            }
            return instance;
        }
    }

    public void SaveWord(InputField input)
    {
        Word = input.text;
    }

}
