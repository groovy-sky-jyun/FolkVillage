using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using UnityEngine.UI;
using Photon.Pun;

public class BubbleChat : MonoBehaviourPunCallbacks
{
    private Image bubble;
    private Text bubbleText;
    // Start is called before the first frame update
    void Start()
    {
        // PhotonNetwork.IsMessageQueueRunning = true;
        bubble = GameObject.Find("Player/Canvas/Image").GetComponent<Image>();
        bubbleText = GameObject.Find("Player/Canvas/Image/speechBubble").GetComponent<Text>();
        bubble.enabled=false;
        bubbleText.enabled=false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
