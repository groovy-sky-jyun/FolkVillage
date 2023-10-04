using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BtnColor : MonoBehaviour
{
    public Button worldBtn;
    private ColorBlock colorBlock;

    public void Start()
    {
        worldBtn.Select();
    }
    public void onClick()
    {
         colorBlock.normalColor = Color.gray;
        Debug.Log("onClick success");
    }
}
