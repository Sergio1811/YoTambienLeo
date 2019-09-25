using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonLenguage
{
    private static SingletonLenguage instance;
    public enum Lenguage {CASTELLANO, CATALAN, INGLES, FRANCES}
    private Lenguage currentLenguage;

    public static SingletonLenguage GetInstance()
    {
        if (instance == null)
        {
            instance = new SingletonLenguage();
            instance.SetLenguage(Lenguage.CASTELLANO);//cambiar en un futuro
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


}
