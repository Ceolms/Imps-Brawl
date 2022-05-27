using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_LevelManager : MonoBehaviour
{
    public static Sc_LevelManager sc_instance;
    public static Sc_LevelManager Instance
    {
        get
        {
            if (sc_instance == null)
            {
                sc_instance = FindObjectOfType<Sc_LevelManager>();
            }
            return sc_instance;
        }
    }

    public List<Collider> list_collidersPlatforms = new List<Collider>();
}
