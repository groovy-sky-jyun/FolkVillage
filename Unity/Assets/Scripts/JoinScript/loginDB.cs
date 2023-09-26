using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class loginDB : MonoBehaviour
{
    public InputField input_id;
    public InputField input_pwd;
    string LoginURL = "http://localhost/login.php";

   public void LoginBtn()
    {
        if(input_id.text!=null&& input_pwd.text!=null)
            LoginDB(input_id.text, input_pwd.text);
    }

    public void LoginDB(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("idPost", username);
        form.AddField("passwordPost", password);

        WWW www = new WWW(LoginURL, form);

        Debug.Log(www);
        //SceneManager.LoadScene("MainMap");

    }
}
