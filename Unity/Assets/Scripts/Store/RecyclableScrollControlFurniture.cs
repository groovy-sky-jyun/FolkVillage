using System.Collections.Generic;
using UnityEngine;
using PolyAndCode.UI;
using UnityEngine.UI;

public class RecyclableScrollControlFurniture : MonoBehaviour, IRecyclableScrollRectDataSource
{
    public GameObject inventory_obj;
    public ChangeItemList ItemPanel;
    [SerializeField]
    RecyclableScrollRect _recyclableScrollRect;

    //Dummy data List
    private List<FurnitureItemDictionary> furnitureList = new List<FurnitureItemDictionary>();
    private List<WallItemDictionary> wallList = new List<WallItemDictionary>();
    private List<FloorItemDictionary> floorList = new List<FloorItemDictionary>();
    private HouseInventoryData item;
    //Recyclable scroll rect's data source must be assigned in Awake.
    private void OnEnable()
    {
        inventory_obj.GetComponent<HouseInventoryJSON>().LoadPlayerDataFromJson();
        item = inventory_obj.GetComponent<HouseInventoryJSON>().getHouseItem();
        InitData();
        _recyclableScrollRect.DataSource = this;
    }

    //Initialising _contactList with dummy data 
    private void InitData()
    {
        

    }

    

    /// <summary>
    /// Data source method. return the list length.
    /// </summary>
    public int GetItemCount()
    {
        if (ItemPanel.furnitureItem_panel.activeSelf)
        {
            return furnitureList.Count;
        }
        else if (ItemPanel.wallItem_panel.activeSelf)
        {
            return wallList.Count;
        }
        else
        {
            return floorList.Count;
        }

    }

    /// <summary>
    /// Data source method. Called for a cell every time it is recycled.
    /// Implement this method to do the necessary cell configuration.
    /// </summary>
    public void SetCell(ICell cell, int index)
    {
        if (ItemPanel.furnitureItem_panel.activeSelf)
        {
            //Casting to the implemented Cell
            var item = cell as FurnitureSetting;
            item.ConfigureCell(furnitureList[index], index);
        }
        else if (ItemPanel.wallItem_panel.activeSelf)
        {
            //Casting to the implemented Cell
            var item = cell as WallSetting;
           // item.ConfigureCell(wallList[index], index);
        }
        else
        {
            //Casting to the implemented Cell
            var item = cell as FloorSetting;
           // item.ConfigureCell(floorList[index], index);
        }
        
    }

}