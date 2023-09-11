using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatBtnManager : MonoBehaviour
{
    public InputField nomalInput;
    public InputField worldInput;
  
   
    public void WorldChatOnClick()
    {
        nomalInput.text = "";

    }
    public void NomaChatOnClick()
    {
        worldInput.text = "";
    }
    
}
