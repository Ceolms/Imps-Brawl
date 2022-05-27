using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Sc_UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textFPS;
    [SerializeField]
    private TextMeshProUGUI textSpeed;


    int m_frameCounter = 0;
    float m_timeCounter = 0.0f;
    float m_lastFramerate = 0.0f;
    public float m_refreshTime = 0.5f;

    // Update is called once per frame
    void Update()
    {
        if (m_timeCounter < m_refreshTime)
        {
            m_timeCounter += Time.deltaTime;
            m_frameCounter++;
        }
        else
        {
            //This code will break if you set your m_refreshTime to 0, which makes no sense.
            m_lastFramerate = (float)m_frameCounter / m_timeCounter;
            m_frameCounter = 0;
            m_timeCounter = 0.0f;
        }
        textFPS.text = "FPS : " + m_lastFramerate;
        textSpeed.text = "SPEED : " + Sc_GameManager.Instance.list_players[0].F_MOVESPEED;
    }
}
