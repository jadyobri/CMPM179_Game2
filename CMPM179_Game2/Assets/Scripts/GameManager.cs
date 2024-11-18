using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance is null)
            {
                Debug.LogError("Game Manager is NULL");
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }

    //To use this just put down a variable you want below like this
    public bool isPet { get; set; }
    public bool startWater { get; set; }
    public Animator doneMoving { get; set; }
    public bool canPet { get; set; }
    //    public float heat { get; set; }
    //Then you add it by using this ex: GameManager.Instance.heat = heat
    //This can be used in any file.
}
