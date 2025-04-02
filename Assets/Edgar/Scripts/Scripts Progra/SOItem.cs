using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Scriptable Objects/Create New Item")]
public class SOItem : ScriptableObject
{

    public GameObject itemPrefab;
    public Sprite sprite;
    public  string names;
    public string description;
    public string cantidad;

}




