using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnChange : MonoBehaviour
{
    public GameObject face_panel;
    public GameObject hair_panel;
    public GameObject shirt_panel;
    public GameObject pants_panel;

    private void Start()
    {
        FaceBtnClick();
    }
    public void FaceBtnClick()
    {

        face_panel.SetActive(true);
        hair_panel.SetActive(false);
        shirt_panel.SetActive(false);
        pants_panel.SetActive(false);
    }
    public void HairBtnClick()
    {
        face_panel.SetActive(false);
        hair_panel.SetActive(true);
        shirt_panel.SetActive(false);
        pants_panel.SetActive(false);
    }
    public void ShirtBtnClick()
    {
        face_panel.SetActive(false);
        hair_panel.SetActive(false);
        shirt_panel.SetActive(true);
        pants_panel.SetActive(false);
    }
    public void PantsBtnClick()
    {
        face_panel.SetActive(false);
        hair_panel.SetActive(false);
        shirt_panel.SetActive(false);
        pants_panel.SetActive(true);
    }
}
