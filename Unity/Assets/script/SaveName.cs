using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConnectPhoton : MonoBehaviour
{
    public InputField user_name;
    
    // Start is called before the first frame update
    public void OnClick() 
    {
        PlayerPrefs.SetString("name", user_name.text);
        PlayerPrefs.Save();
        SceneManager.LoadScene("MainMap");
    }

   
}
