using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    #region Configuracion
    [HideInInspector]
    public string Idioma = "Español";
    [HideInInspector]
    public Font TipoLetra;
    [HideInInspector]
    public bool Mayus = true, Ayuda = true, Dumi = true, Articulo = false;
    [HideInInspector]
    public int Repeticiones =3, Packs = 0;
    #endregion

    #region ScenesIndex
    [HideInInspector]
    public int PreparadosIndex, ListosIndex, YaIndex, GusanosIndex, BurbujasIndex, ColorIndex;
    [HideInInspector]
    public int InicioIndex = 0, ParejasIndex, PuzzleIndex, BitIndex =1;
    #endregion  

    #region ButtonUI
    public Color m_BlackColor, m_PurpleColor, m_GrisColor, m_WhiteColor;

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
