using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonLenguage
{
    private static SingletonLenguage instance;
    public enum Lenguage {CASTELLANO, CATALAN, INGLES, FRANCES}
    private Lenguage currentLenguage;
    public enum OurFont {IMPRENTA, MAYUSCULA, MANUSCRITA}
    private OurFont currentFont;

    public static SingletonLenguage GetInstance()
    {
        if (instance == null)
        {
            instance = new SingletonLenguage();
            instance.SetLenguage(Lenguage.CASTELLANO);//cambiar en un futuro
            instance.SetFont(OurFont.MAYUSCULA);//cambiar en un futuro
        }
        return instance;
    }

    public void SetLenguage(Lenguage _leng)
    {
        currentLenguage = _leng;
    }

    public Lenguage GetLenguage()
    {
        return currentLenguage;
    }

    public void SetFont(OurFont _font)
    {
        currentFont = _font;
    }

    public OurFont GetFont()
    {
        return currentFont;
    }


}
