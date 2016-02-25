using UnityEngine;
using System.Collections;

public class Data {

    static Data instance;

    public static Data GetInstance(){
        if (instance == null)
        {
            instance = new Data();
        }
        return instance;
    }

    public void SetSolved(string puzzleName){
        PlayerPrefs.SetInt(puzzleName, 1);
    }

    public bool GetSolved(string puzzleName){
        var intVal = PlayerPrefs.GetInt(puzzleName);
        return (intVal == 1);
    }

}
