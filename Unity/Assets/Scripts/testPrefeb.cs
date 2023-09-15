using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testPrefeb : MonoBehaviour
{
   // public InputField input;
    //public Text nick;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
