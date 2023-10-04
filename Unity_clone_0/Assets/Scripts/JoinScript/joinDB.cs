using ExitGames.Client.Photon.StructWrapping;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class joinDB : MonoBehaviour
{
    public InputField input_id;
    public InputField input_pwd;
    public InputField input_email;
    public InputField input_nickname;
    string input_gender;

    string CreateURL = "http://localhost/join.php";

public void joinBtn()
    {
        if (input_id.text != null && input_pwd.text != null && input_email.text != null && input_nickname.text != null && input_gender != null)
        {
            StartCoroutine(CreateUser(input_id.text, input_pwd.text, input_nickname.text, input_email.text, input_gender));

        }
        else
        {
            Debug.Log("내용을 확인해 주세요 팝업");
        }
    }
    public void clickWoman()
    {
        input_gender = "1";
    }
    public void clickMan()
    {
        input_gender = "0";
    }
    IEnumerator CreateUser(string id, string pw, string nickname, string email, string gender )
    {
        WWWForm form = new WWWForm();
        form.AddField("idPost", id);
        form.AddField("passwordPost", pw);
        form.AddField("usernamePost", nickname);
        form.AddField("emailPost", email);
        form.AddField("genderPost", gender);

        WWW www = new WWW(CreateURL, form);
        
        yield return www;
        Debug.Log(www.text);
        SceneManager.LoadScene("Login");
    }
 
}
