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
    private string nombre = "Manzana";
    private Texture2D texture;
    public enum NumOfSearch { NONE, ID, NAME };
    private NumOfSearch currentSearch = NumOfSearch.NAME;
    public Image imagen;
    public AudioSource audioSource;
    public ObjectBD currentObjectBD;
    private string ruteFolderImage;
    private string ruteFolderAudio;
    // Start is called before the first frame update
    void Start()
    {
        ruteFolderImage = "file://" + Application.dataPath + "/Resources/Images/BurbujasMinigame/";//cambiar la dirección cuando se tenga la definitiva
        ruteFolderAudio = "file://" + Application.dataPath + "/Resources/Audios/";
        //ObtainFrase("Manzana Pera Melocoton");
        ReadSQlite();
    }

    // Update is called once per frame
    public ObjectBD ReadSQlite()
    {
        string conection = "URI=file:" + Application.dataPath + "/Plugins/SQLite/BaseDeDatosYoTambienLeo.db";
        IDbConnection dbConection = (IDbConnection)new SqliteConnection(conection);
        dbConection.Open();
        IDbCommand dbcommand = dbConection.CreateCommand();
        string sqlQuery = SearchInBDContenido("Contenido");
        //string sqlQuery = "SELECT id, nombre, imagen, imagen2 FROM Contenido";  //en el from tiene que poner el nombre de la tabla y antes lo que se tiene que seleccionar en sql. Al buscar algun string hay que ponerle las comillas estas '  ' osino no lo reconoce
        dbcommand.CommandText = sqlQuery;
        IDataReader reader = dbcommand.ExecuteReader();


        currentObjectBD = new ObjectBD();

        while (reader.Read())
        {
            currentObjectBD.id = reader.GetInt32(0);
            currentObjectBD.color = reader.GetString(1);
            currentObjectBD.image1 = reader.GetString(2);
            currentObjectBD.image2 = reader.GetString(3);
            currentObjectBD.image3 = reader.GetString(4);
            currentObjectBD.audio = reader.GetString(5);
            currentObjectBD.piecesPuzzle = reader.GetInt32(6);
            currentObjectBD.imagePuzzle = reader.GetInt32(7);
            currentObjectBD.dificultSpanish = reader.GetInt32(8);
            currentObjectBD.nameSpanish = reader.GetString(9);
            currentObjectBD.silabasSpanish = reader.GetString(10);
            currentObjectBD.dificultCatalan = reader.GetInt32(11);
            currentObjectBD.nameCatalan = reader.GetString(12);
            currentObjectBD.silabasCatalan = reader.GetString(13);
            currentObjectBD.paquet = reader.GetInt32(14);
            // Debug.Log("Id = " + id + "  Nombre 1 =" + nombre1 + "  imagen 1 =" + imagen1 + " imagen 2 =" + imagen2);


        }
        if (currentObjectBD.nameSpanish != null)
        {
            currentObjectBD.SeparateSilabas(SingletonLenguage.GetInstance().GetLenguage());
            SearchAudioClip(currentObjectBD.audio);
        }
        else Debug.Log("No existe");

        if (currentObjectBD.image1 != null)
        {
            SearchSpriteInRuteFolders(currentObjectBD.image1);
        }




        reader.Close();
        reader = null;

        dbcommand.Dispose();
        dbcommand = null;

        dbConection.Close();
        dbConection = null;

        return currentObjectBD;
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

                    case SingletonLenguage.Lenguage.INGLES:break;
                    case SingletonLenguage.Lenguage.FRANCES:break;
                }
                break;
        }
        ResetValues();
        return m_SQL;
    }

    private void ResetValues()
    {
        currentSearch = NumOfSearch.NONE;
        idNumber = 0;
        nombre = "";
    }

    public void ChangeIDSearch(int _id = 0)
    {
        idNumber = _id;
        currentSearch = NumOfSearch.ID;
    }

    public void ChangeNameSearch(string _name = "")
    {
        nombre = _name;
        currentSearch = NumOfSearch.NAME;
    }

    public void SearchSpriteInRuteFolders(string _image)
    {
        string completeRute = ruteFolderImage + _image;
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
        Rect rect = new Rect(new Vector2(0,0), new Vector2(texture.width,texture.height));
        imagen.sprite = Sprite.Create(texture, rect, Vector2.down);
    }
    
    public void SearchAudioClip(string _audio)
    {
        string completeRute = ruteFolderAudio;
        switch(SingletonLenguage.GetInstance().GetLenguage())
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

    public List<ObjectBD> ObtainFrase(string _frase)
    {
        List<ObjectBD> words = new List<ObjectBD>();

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
                words.Add(ReadSQlite());
                //print(words[words.Count - 1].nameSpanish);
                currentPalabra = "";
            }
        }
        if (currentPalabra != "")
        {
            ChangeNameSearch(currentPalabra);
            words.Add(ReadSQlite());
            //print(words[words.Count - 1].nameSpanish);
            currentPalabra = "";
        }

        return words;
    }
}
