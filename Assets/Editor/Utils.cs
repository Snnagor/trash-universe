using UnityEngine;
using UnityEditor;
using Zenject;

public class Utils
{   
    [MenuItem("Utils/Clear prefs")]
    public static void ClearPrefs()
    {
        PlayerPrefs.DeleteAll();        
        Debug.Log("PlayerPrefs cleared!");
    }
}
