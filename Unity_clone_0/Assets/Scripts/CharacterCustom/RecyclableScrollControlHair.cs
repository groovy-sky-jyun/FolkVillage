using System.Collections.Generic;
using UnityEngine;
using PolyAndCode.UI;
using UnityEngine.UI;

/// <summary>
/// Demo controller class for Recyclable Scroll Rect. 
/// A controller class is responsible for providing the scroll rect with datasource. Any class can be a controller class. 
/// The only requirement is to inherit from IRecyclableScrollRectDataSource and implement the interface methods
/// </summary>

/*/Dummy Data model for demostraion
public struct ContactInfo
{
    public string Name;
    public string Price;
    public string ItemImg;
}*/


public class RecyclableScrollControlHair : MonoBehaviour, IRecyclableScrollRectDataSource
{
    public GameObject inventory_obj;

    [SerializeField]
    RecyclableScrollRect _recyclableScrollRect;

    [SerializeField]
    private int _dataLength;

    //Dummy data List
    private List<HairItemDictionary> hairItemList = new List<HairItemDictionary>();
    private CustomInventoryData item;
    //Recyclable scroll rect's data source must be assigned in Awake.
    private void Awake()
    {
        Setting();
    }
    private void OnEnable()
    {
        Setting();
    }
    public void Setting()
    {
        inventory_obj.GetComponent<ShirtInventoryJSON>().LoadPlayerDataFromJson();
        item = inventory_obj.GetComponent<ShirtInventoryJSON>().inventoryItem;
        InitData();
        _recyclableScrollRect.DataSource = this;
    }
    //Initialising _contactList with dummy data 
    private void InitData()
    {
        if (hairItemList != null) hairItemList.Clear();
        for (int i = 0; i < _dataLength; i++)
        {
            HairItemDictionary obj = item.hairItemList[i];
            if (obj.have)
            {
                hairItemList.Add(obj);
            }
        }
    }

    #region DATA-SOURCE

    /// <summary>
    /// Data source method. return the list length.
    /// </summary>
    public int GetItemCount()
    {
        return hairItemList.Count;
    }

    /// <summary>
    /// Data source method. Called for a cell every time it is recycled.
    /// Implement this method to do the necessary cell configuration.
    /// </summary>
    public void SetCell(ICell cell, int index)
    {
        //Casting to the implemented Cell
        var item = cell as HairItemSetting;
        item.ConfigureCell(hairItemList[index], index);
    }

    #endregion
}