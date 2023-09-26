using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
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
            StartCoroutine(LoginDB(input_id.text, input_pwd.text));
    }

    IEnumerator LoginDB(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("idPost", username);
        form.AddField("passwordPost", password);

        UnityWebRequest www =UnityWebRequest.Post(LoginURL, form);

        yield return www.SendWebRequest();
        if (www.downloadHandler.text == "login success")
        {
            Debug.Log("로그인 성공");
            SceneManager.LoadScene("MainMap");
        }
        else
            Debug.Log(www.downloadHandler.text);



    }

    public void onClickJoinBtn()
    {
        SceneManager.LoadScene("Join");
    }
}
