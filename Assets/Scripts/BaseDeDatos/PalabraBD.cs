﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalabraBD 
{
    public int id;
    public string color;
    public string image1;
    public string image2;
    public string image3;
    public string audio;
    public int piecesPuzzle;
    public int imagePuzzle;
    public int dificultSpanish;
    public string nameSpanish;
    public string silabasSpanish;
    public int dificultCatalan;
    public string nameCatalan;
    public string silabasCatalan;
    public int paquet;
    public List<string> silabasActuales = new List<string>();
    public string palabraActual;

    //eliminar en un futuro y cambiarlo por el original
    //

    public void SeparateSilabas(SingletonLenguage.Lenguage currentLenguage)//separa la linea de string de silabas segun el idioma a un lista en orden de las silabas.
    {
        string actualWord = "";
        switch (currentLenguage)
        {
            case SingletonLenguage.Lenguage.CASTELLANO:
                actualWord = silabasSpanish;
                break;
            case SingletonLenguage.Lenguage.CATALAN:
                actualWord = silabasCatalan;
                break;
            case SingletonLenguage.Lenguage.INGLES:
                break;
            case SingletonLenguage.Lenguage.FRANCES:
                break;
        }
        if (actualWord != "")
        {
            string currentSilaba = "";
            for (int i = 0; i < actualWord.Length; i++)
            {
                if (actualWord[i] != '-')
                {
                    currentSilaba += actualWord[i];
                }
                else
                {
                    silabasActuales.Add(currentSilaba);
                    //Debug.Log(silabasActuales[silabasActuales.Count - 1]);
                    currentSilaba = "";
                }
            }
            if (currentSilaba != "")
            {
                silabasActuales.Add(currentSilaba);
                //Debug.Log(silabasActuales[silabasActuales.Count - 1]);
                currentSilaba = "";
            }
        }
    }

    public void SetPalabraActual(SingletonLenguage.Lenguage currentLenguage)
    {
        switch (currentLenguage)
        {
            case SingletonLenguage.Lenguage.CASTELLANO:
                palabraActual = nameSpanish;
                break;
            case SingletonLenguage.Lenguage.CATALAN:
                palabraActual = nameCatalan;
                break;
            case SingletonLenguage.Lenguage.INGLES:
                break;
            case SingletonLenguage.Lenguage.FRANCES:
                break;
        }
    }

}
