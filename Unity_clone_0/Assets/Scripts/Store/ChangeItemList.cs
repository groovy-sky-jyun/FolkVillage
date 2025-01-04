using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeItemList : MonoBehaviour
{
    public GameObject furnitureItem_panel;
    public GameObject wallItem_panel;
    public GameObject floorItem_panel;

    private void Start()
    {
        FurnitureBtnClick();
    }
    public void FurnitureBtnClick()
    {
        furnitureItem_panel.SetActive(true);
        wallItem_panel.SetActive(false);
        floorItem_panel.SetActive(false);
    }
    public void WallBtnClick()
    {
        furnitureItem_panel.SetActive(false);
        wallItem_panel.SetActive(true);
        floorItem_panel.SetActive(false);
    }
    public void FloorBtnClick()
    {
        furnitureItem_panel.SetActive(false);
        wallItem_panel.SetActive(false);
        floorItem_panel.SetActive(true);
    }
}
