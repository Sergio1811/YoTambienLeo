using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frases : MonoBehaviour
{
    List<string> frasesNivel1 = new List<string>();
    List<string> frasesNivel2 = new List<string>();
    public int dificultad = 1;
    
    private void Start()
    {
        Init();
        print(GetRandomFrase());
    }

    private void Init()
    {
        switch(SingletonLenguage.GetInstance().GetLenguage())
        {
            case SingletonLenguage.Lenguage.CASTELLANO:
                frasesNivel1.Add("El niño come sopa");
                frasesNivel1.Add("La silla es verde");
                frasesNivel1.Add("El limón es amarillo");
                frasesNivel1.Add("Un vaso de leche");

                frasesNivel2.Add("La niña come una piruleta");
                frasesNivel2.Add("La niña juega con la pelota");
                frasesNivel2.Add("El niño lava los platos");
                frasesNivel2.Add("La niña se lava los dientes");
                frasesNivel2.Add("Las fresas son rojas");
                frasesNivel2.Add("La pelota es redonda");
                frasesNivel2.Add("La naranja es de color naranja");
                break;
            case SingletonLenguage.Lenguage.CATALAN:
                frasesNivel1.Add("El nen menja sopa");
                frasesNivel1.Add("La cadira és verda");
                frasesNivel1.Add("La llimona és groga");
                frasesNivel1.Add("Un got de llet");

                frasesNivel2.Add("La nena menja una piruleta");
                frasesNivel2.Add("La nena juga amb la pilota");
                frasesNivel2.Add("El nen renta els plats");
                frasesNivel2.Add("La nena es renta les dents");
                frasesNivel2.Add("Les maduixes son vermelles");
                frasesNivel2.Add("La pilota és rodona");
                frasesNivel2.Add("La taronja és de color taronja");
                break;
            case SingletonLenguage.Lenguage.INGLES:
                break;
            case SingletonLenguage.Lenguage.FRANCES:
                break;
        }

    }

    public string GetRandomFrase()
    {
        string m_frase = "";
        switch (dificultad)
        {
            case 1:
                m_frase = frasesNivel1[Random.Range(0, frasesNivel1.Count - 1)];
                break;
            case 2:
                m_frase = frasesNivel2[Random.Range(0, frasesNivel2.Count - 1)];
                break;
            case 3:
                break;
        }
        return m_frase;
    }
}
