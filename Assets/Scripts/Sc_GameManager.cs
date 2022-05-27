using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_GameManager : MonoBehaviour
{
    public static Sc_GameManager sc_instance;

    public static Sc_GameManager Instance
    {
        get
        {
            if (sc_instance == null)
            {
                sc_instance = FindObjectOfType<Sc_GameManager>();
            }
            return sc_instance;
        }
    }

    public List<Sc_CharacterController> list_players = new List<Sc_CharacterController>();

    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }
}
