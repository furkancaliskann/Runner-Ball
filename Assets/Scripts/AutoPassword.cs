#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;

[InitializeOnLoad]
public class AutoPassword
{
    static AutoPassword()
    {
        PlayerSettings.Android.keystorePass = "159651";
        PlayerSettings.Android.keyaliasName = "furkan";
        PlayerSettings.Android.keyaliasPass = "159651";
    }
}

#endif