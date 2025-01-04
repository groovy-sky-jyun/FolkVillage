using System.Collections.Generic;
using UnityEngine;
using PolyAndCode.UI;
using UnityEngine.UI;
using System.Linq;

public class RecyclableScrollControlFurniture : MonoBehaviour, IRecyclableScrollRectDataSource
{
    public GameObject inventory_obj;

    [SerializeField]
    RecyclableScrollRect _recyclableScrollRect;

    //[SerializeField]
    //private int _dataLength;

    //Dummy data List
    private List<FurnitureItemDictionary> furnitureList = new List<FurnitureItemDictionary>();
    private HouseInventoryData item;
    //Recyclable scroll rect's data source must be assigned in Awake.
    private void OnEnable()
    {
        inventory_obj.GetComponent<HouseInventoryJSON>().LoadPlayerDataFromJson();
        item = inventory_obj.GetComponent<HouseInventoryJSON>().inventoryItem;
        InitData();
        _recyclableScrollRect.DataSource = this;
    }

    //Initialising _contactList with dummy data 
    private void InitData()
    {
        if (furnitureList != null) furnitureList.Clear();
        for (int i = 0; i < item.furnitureItemList.Length; i++)
        {
            FurnitureItemDictionary obj = item.furnitureItemList[i];
            if (!obj.have)
            {
                furnitureList.Add(obj);
            }
        }
    }

    #region DATA-SOURCE

    /// <summary>
    /// Data source method. return the list length.
    /// </summary>
    public int GetItemCount()
    {
        return furnitureList.Count;
    }

    /// <summary>
    /// Data source method. Called for a cell every time it is recycled.
    /// Implement this method to do the necessary cell configuration.
    /// </summary>
    public void SetCell(ICell cell, int index)
    {
        //Casting to the implemented Cell
        var item = cell as FurnitureItemSetting;
        item.ConfigureCell(furnitureList[index], index);
    }

    #endregion
}