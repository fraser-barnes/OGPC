using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Settings", menuName = "Scriptable Objects/Settings")]
public class Settings : ScriptableObject
{
    public float volume = 100;
    public bool subtitles = false;
}
