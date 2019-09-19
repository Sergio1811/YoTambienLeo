using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Data;
using Mono.Data.Sqlite;

public class ScriptPruebaBD : MonoBehaviour
{
    private int idNumber = 0;
    private string nombre = "Manzana";
    public enum NumOfSearch { NONE, ID, NAME };
    private Texture2D texture;
    public Image imagen;
    private NumOfSearch currentSearch = NumOfSearch.NAME;
    // Start is called before the first frame update
    void Start()
    {
        ReadSQlite();
    }

    // Update is called once per frame
    void ReadSQlite()
    {
        string conection = "URI=file:" + Application.dataPath + "/Plugins/SQLite/BaseDeDatosYoTambienLeo.db";
        IDbConnection dbConection = (IDbConnection)new SqliteConnection(conection);
        dbConection.Open();
        IDbCommand dbcommand = dbConection.CreateCommand();
        string sqlQuery = SearchInBDContenido("Contenido");
        //string sqlQuery = "SELECT id, nombre, imagen, imagen2 FROM Contenido";  //en el from tiene que poner el nombre de la tabla y antes lo que se tiene que seleccionar en sql. Al buscar algun string hay que ponerle las comillas estas '  ' osino no lo reconoce
        dbcommand.CommandText = sqlQuery;
        IDataReader reader = dbcommand.ExecuteReader();

        while (reader.Read())
        {
            int id = reader.GetInt32(0);
            string nombre1 = reader.GetString(1);
            string imagen1 = reader.GetString(2);
            string imagen2 = reader.GetString(3);
            StartCoroutine(ConvertURLToTexture(imagen1));

            Debug.Log("Id = " + id + "  Nombre 1 =" + nombre1 + "  imagen 1 =" + imagen1 + " imagen 2 =" + imagen2);


        }



        reader.Close();
        reader = null;

        dbcommand.Dispose();
        dbcommand = null;

        dbConection.Close();
        dbConection = null;

    }

    private string SearchInBDContenido(string _table)
    {
        string m_SQL = "";
        switch (currentSearch)
        {
            case NumOfSearch.NONE:
                m_SQL = ("SELECT id, nombre, imagen, imagen2 FROM " + _table);
                break;
            case NumOfSearch.ID:
                m_SQL = ("SELECT id, nombre, imagen, imagen2 FROM " + _table + " WHERE id = " + idNumber);
                break;
            case NumOfSearch.NAME:
                m_SQL = ("SELECT id, nombre, imagen, imagen2 FROM " + _table + " WHERE nombre = " + "'" + nombre + "'");
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


    IEnumerator ConvertURLToTexture(string _url)
    {
        WWW www = new WWW(_url); //Cargando la imagen
        yield return www;

        texture = www.texture; //una vez cargada 
        ColocarLaImagen();
    }

    private void ColocarLaImagen()
    {
        Rect rect = new Rect(new Vector2(0,0), new Vector2(texture.width,texture.height));
        imagen.sprite = Sprite.Create(texture, rect, Vector2.down);
    }
}
