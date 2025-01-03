using System.Collections.Generic;
using UnityEngine;
using PolyAndCode.UI;
using UnityEngine.UI;

public class RecyclableScrollControlInveltoryWall : MonoBehaviour, IRecyclableScrollRectDataSource
{
    public GameObject inventory_obj;

    [SerializeField]
    RecyclableScrollRect _recyclableScrollRect;

    //Dummy data List
    private List<WallItemDictionary> wallList = new List<WallItemDictionary>();
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
        if(wallList != null) { wallList.Clear(); }
        for(int i=0; i<item.wallItemList.Length; i++)
        {
            WallItemDictionary WallItem = item.wallItemList[i];
            if (WallItem.have)
            {
                wallList.Add(WallItem);
            }
        }
    }

    /// <summary>
    /// Data source method. return the list length.
    /// </summary>
    public int GetItemCount()
    {
         return wallList.Count;
      
    }

    /// <summary>
    /// Data source method. Called for a cell every time it is recycled.
    /// Implement this method to do the necessary cell configuration.
    /// </summary>
    public void SetCell(ICell cell, int index)
    {
        //Casting to the implemented Cell
        var item = cell as WallSetting;
        item.ConfigureCell(wallList[index], index, wallList.Count);
    }
}