using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    //뒤로가기버튼 (Character Custom --> House)
   public void CharacterCustomToHouse()
    {
        SceneManager.LoadScene("House");
    }
   public void HouseToCharacterCustom()
    {
        SceneManager.LoadScene("CharacterCustom");
    }
    public void HouseToMainMap()
    {
        SceneManager.LoadScene("MainMap");
    }
    public void MainToStore()
    {
        SceneManager.LoadScene("Store");
    }
}
