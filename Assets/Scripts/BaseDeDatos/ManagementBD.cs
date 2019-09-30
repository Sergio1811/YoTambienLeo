using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Data;
using Mono.Data.Sqlite;

public class ManagementBD : MonoBehaviour
{
    private int idNumber = 0;
    private string nombre = "Sopa";
    private string m_frase = "";// Estos de frase
    private int m_dificult = 0;//Estos de frase
    private Texture2D texture;
    public enum NumOfSearch { NONE, ID, NAME, DIFICULT };
    private NumOfSearch currentSearch = NumOfSearch.NONE;
    public enum NumofSearchFrase { NONE, NAME, DIFICULT };
    private NumofSearchFrase currentSearchFrase = NumofSearchFrase.NONE;
    public Image imagen;
    public AudioSource audioSource;
    private string ruteFolderImage;
    private string ruteFolderAudio;

    // Start is called before the first frame update
    void Awake()
    {
        ruteFolderImage = "file://" + Application.dataPath + "/Resources/Images/BurbujasMinigame/";//cambiar la dirección cuando se tenga la definitiva
        ruteFolderAudio = "file://" + Application.dataPath + "/Resources/Audios/";
        //ObtainFrase("Manzana Pera Melocoton");
        //ReadSQlitePalabra();
    }

    // Update is called once per frame
    public List<PalabraBD> ReadSQlitePalabra()
    {
        string conection = "URI=file:" + Application.dataPath + "/Plugins/SQLite/BaseDeDatosYoTambienLeo.db";
        IDbConnection dbConection = (IDbConnection)new SqliteConnection(conection);
        dbConection.Open();
        IDbCommand dbcommand = dbConection.CreateCommand();
        string sqlQuery = SearchInBDContenido("Contenido");
        //string sqlQuery = "SELECT id, nombre, imagen, imagen2 FROM Contenido";  //en el from tiene que poner el nombre de la tabla y antes lo que se tiene que seleccionar en sql. Al buscar algun string hay que ponerle las comillas estas '  ' osino no lo reconoce
        dbcommand.CommandText = sqlQuery;
        IDataReader reader = dbcommand.ExecuteReader();


        List<PalabraBD> currentObjectBD = new List<PalabraBD>();

        while (reader.Read())
        {
            currentObjectBD.Add(new PalabraBD());
            currentObjectBD[currentObjectBD.Count - 1].id = reader.GetInt32(0);
            currentObjectBD[currentObjectBD.Count - 1].color = reader.GetString(1);
            currentObjectBD[currentObjectBD.Count - 1].image1 = reader.GetString(2);
            currentObjectBD[currentObjectBD.Count - 1].image2 = reader.GetString(3);
            currentObjectBD[currentObjectBD.Count - 1].image3 = reader.GetString(4);
            currentObjectBD[currentObjectBD.Count - 1].audio = reader.GetString(5);
            currentObjectBD[currentObjectBD.Count - 1].piecesPuzzle = reader.GetInt32(6);
            currentObjectBD[currentObjectBD.Count - 1].imagePuzzle = reader.GetInt32(7);
            currentObjectBD[currentObjectBD.Count - 1].dificultSpanish = reader.GetInt32(8);
            currentObjectBD[currentObjectBD.Count - 1].nameSpanish = reader.GetString(9);
            currentObjectBD[currentObjectBD.Count - 1].silabasSpanish = reader.GetString(10);
            currentObjectBD[currentObjectBD.Count - 1].dificultCatalan = reader.GetInt32(11);
            currentObjectBD[currentObjectBD.Count - 1].nameCatalan = reader.GetString(12);
            currentObjectBD[currentObjectBD.Count - 1].silabasCatalan = reader.GetString(13);
            currentObjectBD[currentObjectBD.Count - 1].paquet = reader.GetInt32(14);
            // Debug.Log("Id = " + id + "  Nombre 1 =" + nombre1 + "  imagen 1 =" + imagen1 + " imagen 2 =" + imagen2);

        }


        if (currentObjectBD.Count > 0)
        {
            foreach (PalabraBD p in currentObjectBD)
            {
                p.SeparateSilabas(SingletonLenguage.GetInstance().GetLenguage());
            }
        }
        else Debug.Log("No existe");



        reader.Close();
        reader = null;

        dbcommand.Dispose();
        dbcommand = null;

        dbConection.Close();
        dbConection = null;

        return currentObjectBD;
    }

    public List<FraseBD> ReadSQliteFrase()
    {

        string conection = "URI=file:" + Application.dataPath + "/Plugins/SQLite/BaseDeDatosYoTambienLeo.db";
        IDbConnection dbConection = (IDbConnection)new SqliteConnection(conection);
        dbConection.Open();
        IDbCommand dbcommand = dbConection.CreateCommand();
        string sqlQuery = SearchInBDContenidoFrase("Frases");
        //string sqlQuery = "SELECT id, nombre, imagen, imagen2 FROM Contenido";  //en el from tiene que poner el nombre de la tabla y antes lo que se tiene que seleccionar en sql. Al buscar algun string hay que ponerle las comillas estas '  ' osino no lo reconoce
        dbcommand.CommandText = sqlQuery;
        IDataReader reader = dbcommand.ExecuteReader();


        List<FraseBD> m_frase = new List<FraseBD>();

        while (reader.Read())
        {
            m_frase.Add(new FraseBD());
            m_frase[m_frase.Count - 1].id = reader.GetInt32(0);
            m_frase[m_frase.Count - 1].frase = reader.GetString(1);
            m_frase[m_frase.Count - 1].image = reader.GetString(2);
            m_frase[m_frase.Count - 1].sound = reader.GetString(3);
            m_frase[m_frase.Count - 1].idioma = reader.GetInt32(4);
            m_frase[m_frase.Count - 1].dificultad = reader.GetInt32(5);
            // Debug.Log("Id = " + id + "  Nombre 1 =" + nombre1 + "  imagen 1 =" + imagen1 + " imagen 2 =" + imagen2);

        }
        if (m_frase.Count > 0)
        {
            foreach (FraseBD f in m_frase)
            {
                f.palabras = ObtainFrase(f.frase);
            }
        }
        else Debug.Log("No existe");


        reader.Close();
        reader = null;

        dbcommand.Dispose();
        dbcommand = null;

        dbConection.Close();
        dbConection = null;

        return m_frase;
    }

    private string SearchInBDContenido(string _table)
    {
        string m_SQL = "";
        switch (currentSearch)
        {
            case NumOfSearch.NONE:
                m_SQL = ("SELECT * FROM " + _table);
                break;
            case NumOfSearch.ID:
                m_SQL = ("SELECT * FROM " + _table + " WHERE id = " + idNumber);
                break;
            case NumOfSearch.NAME:
                switch (SingletonLenguage.GetInstance().GetLenguage())
                {
                    case SingletonLenguage.Lenguage.CASTELLANO:
                        m_SQL = ("SELECT * FROM " + _table + " WHERE nombreCastellano = " + "'" + nombre + "'");
                        break;
                    case SingletonLenguage.Lenguage.CATALAN:
                        m_SQL = ("SELECT * FROM " + _table + " WHERE nombreCatalan = " + "'" + nombre + "'");
                        break;

                    case SingletonLenguage.Lenguage.INGLES: break;
                    case SingletonLenguage.Lenguage.FRANCES: break;
                }
                break;
        }
        ResetValues();
        return m_SQL;
    }

    private string SearchInBDContenidoFrase(string _table)
    {
        string m_SQL = "";
        switch (currentSearchFrase)
        {
            case NumofSearchFrase.NONE:
                switch (SingletonLenguage.GetInstance().GetLenguage())
                {
                    case SingletonLenguage.Lenguage.CASTELLANO:
                        m_SQL = ("SELECT * FROM " + _table + " WHERE idioma = 0");
                        break;
                    case SingletonLenguage.Lenguage.CATALAN:
                        m_SQL = ("SELECT * FROM " + _table + " WHERE idioma = 1");
                        break;

                    case SingletonLenguage.Lenguage.INGLES:
                        m_SQL = ("SELECT * FROM " + _table + " WHERE AND idioma = 2");
                        break;
                    case SingletonLenguage.Lenguage.FRANCES:
                        m_SQL = ("SELECT * FROM " + _table + " WHERE AND idioma = 3");
                        break;
                }
                break;
            case NumofSearchFrase.NAME:
                switch (SingletonLenguage.GetInstance().GetLenguage())
                {
                    case SingletonLenguage.Lenguage.CASTELLANO:
                        m_SQL = ("SELECT * FROM " + _table + " WHERE frase = " + "'" + m_frase + "'" + " AND idioma = 0");
                        break;
                    case SingletonLenguage.Lenguage.CATALAN:
                        m_SQL = ("SELECT * FROM " + _table + " WHERE frase = " + "'" + m_frase + "'" + " AND idioma = 1");
                        break;

                    case SingletonLenguage.Lenguage.INGLES:
                        m_SQL = ("SELECT * FROM " + _table + " WHERE frase = " + "'" + m_frase + "'" + " AND idioma = 2");
                        break;
                    case SingletonLenguage.Lenguage.FRANCES:
                        m_SQL = ("SELECT * FROM " + _table + " WHERE frase = " + "'" + m_frase + "'" + " AND idioma = 3");
                        break;
                }
                break;                                                                                                      ////BUSCAR UNA FORMA DE JUNTAR CONDICIONES
            case NumofSearchFrase.DIFICULT:
                switch (SingletonLenguage.GetInstance().GetLenguage())
                {
                    case SingletonLenguage.Lenguage.CASTELLANO:
                        m_SQL = ("SELECT * FROM " + _table + " WHERE dificultad = " + "'" + m_dificult + "'" + " AND idioma = 0");
                        break;
                    case SingletonLenguage.Lenguage.CATALAN:
                        m_SQL = ("SELECT * FROM " + _table + " WHERE dificultad = " + "'" + m_dificult + "'" + " AND idioma = 1");
                        break;

                    case SingletonLenguage.Lenguage.INGLES:
                        m_SQL = ("SELECT * FROM " + _table + " WHERE dificultad = " + "'" + m_dificult + "'" + " AND idioma = 2");
                        break;
                    case SingletonLenguage.Lenguage.FRANCES:
                        m_SQL = ("SELECT * FROM " + _table + " WHERE dificultad = " + "'" + m_dificult + "'" + " AND idioma = 3");
                        break;
                }
                break;
        }
        ResetValuesFrase();
        return m_SQL;
    }

    private void ResetValues()
    {
        currentSearch = NumOfSearch.NONE;
        idNumber = 0;
        nombre = "";
    }
    private void ResetValuesFrase()
    {
        currentSearchFrase = NumofSearchFrase.NONE;
        m_frase = "";
        m_dificult = 0;
    }

    public void ChangeIDSearch(int _id)
    {
        idNumber = _id;
        currentSearch = NumOfSearch.ID;
    }

    public void ChangeNameSearch(string _name)
    {
        nombre = _name;
        currentSearch = NumOfSearch.NAME;
    }
    public void ChangeNameSearchFrase(string _name)
    {
        nombre = _name;
        currentSearchFrase = NumofSearchFrase.NAME;
    }

    public void ChangeDificultFrase(int _dificult)
    {
        m_dificult = _dificult;
        currentSearchFrase = NumofSearchFrase.DIFICULT;
    }

    public void SearchSpriteInRuteFolders(string _rute, Image _image)
    {
        imagen = _image;
        string completeRute = ruteFolderImage + _rute;
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

    public void SearchAudioClip(string _audio, AudioSource _audioSource)
    {
        audioSource = _audioSource;
        string completeRute = ruteFolderAudio;
        switch (SingletonLenguage.GetInstance().GetLenguage())
        {
            case SingletonLenguage.Lenguage.CASTELLANO:
                completeRute += "Castellano/" + _audio;
                break;
            case SingletonLenguage.Lenguage.CATALAN:
                completeRute += "Catalan/" + _audio;
                break;
            case SingletonLenguage.Lenguage.INGLES:
                break;
            case SingletonLenguage.Lenguage.FRANCES:
                break;
        }

        WWW www = new WWW(completeRute);
        StartCoroutine(LoadAudio(www));
    }

    private IEnumerator LoadAudio(WWW _www)
    {
        WWW request = _www;
        yield return request;

        AudioClip audio = request.GetAudioClip();
        audioSource.clip = audio;
        audioSource.Play();
    }

    public void ChangeAudioAnImage(Image _image, AudioSource _audio)
    {
        imagen = _image;
        audioSource = _audio;
    }

    public List<PalabraBD> ObtainFrase(string _frase)
    {
        List<PalabraBD> words = new List<PalabraBD>();
        List<PalabraBD> provisional = new List<PalabraBD>();
        string currentPalabra = "";
        for (int i = 0; i < _frase.Length; i++)
        {
            if (_frase[i] != ' ')
            {
                currentPalabra += _frase[i];
            }
            else
            {
                ChangeNameSearch(currentPalabra);
                provisional = ReadSQlitePalabra();
                words.Add(provisional[provisional.Count - 1]);
                if (words.Count > 1)
                    ChangeNameCapitalToLower(words[words.Count - 1]);
                //print(words[words.Count - 1].nameSpanish);
                currentPalabra = "";
            }
        }
        if (currentPalabra != "")
        {
            ChangeNameSearch(currentPalabra);
            provisional = ReadSQlitePalabra();
            words.Add(provisional[provisional.Count - 1]);
            if (words.Count > 1)
                words[words.Count - 1] = ChangeNameCapitalToLower(words[words.Count - 1]);
            //print(words[words.Count - 1].nameSpanish);
            currentPalabra = "";
        }

        return words;
    }

    private PalabraBD ChangeNameCapitalToLower(PalabraBD _palabra)//SI ESTA LA OPCION TODO MAYUSCULAS, ESTO NO HACERLO
    {
        PalabraBD name = _palabra;

        switch(SingletonLenguage.GetInstance().GetLenguage())
        {
            case SingletonLenguage.Lenguage.CASTELLANO:
                name.nameSpanish = _palabra.nameSpanish.ToLower();
                break;
            case SingletonLenguage.Lenguage.CATALAN:
                name.nameCatalan = _palabra.nameCatalan.ToLower();
                break;
            case SingletonLenguage.Lenguage.INGLES:
                break;
            case SingletonLenguage.Lenguage.FRANCES:
                break;
        }
        return name;
    }
}
