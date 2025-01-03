using PolyAndCode.UI;
using UnityEngine;
using UnityEngine.UI;

//Cell class for demo. A cell in Recyclable Scroll Rect must have a cell class inheriting from ICell.
//The class is required to configure the cell(updating UI elements etc) according to the data during recycling of cells.
//The configuration of a cell is done through the DataSource SetCellData method.
//Check RecyclableScrollerDemo class
public class FloorSetting : MonoBehaviour, ICell
{
    //UI
    public Image itemImage;

    //Model
    public FloorItemDictionary _contactInfo;

    private int _cellIndex;
    private int cellLength;
    private GameObject itemListCS;
    private HouseInventoryData inventoryItem;
    private void Start()
    {
        //Can also be done in the inspector
        GetComponent<Button>().onClick.AddListener(ButtonListener);
    }

    //This is called from the SetCell method in DataSource
    public void ConfigureCell(FloorItemDictionary contactInfo, int cellIndex, int length)
    {
        _cellIndex = cellIndex;
        _contactInfo = contactInfo;
        cellLength = length;

        string spriteName = "HouseItemSprites/" + contactInfo.sprite_name;
        itemImage.sprite = Resources.Load < Sprite >(spriteName);

        itemListCS = GameObject.FindGameObjectWithTag("ItemStructures");
        inventoryItem = itemListCS.GetComponent<HouseInventoryJSON>().inventoryItem;
    }


    private void ButtonListener()
    {
        for (int i = 0; i < cellLength; i++)
        {
            if (_contactInfo.num == i)
            {
                GameObject.FindGameObjectWithTag("FloorItem").transform.GetChild(i).gameObject.SetActive(true);
                inventoryItem.floorItemList[i].use_check = true;
                itemListCS.GetComponent<HouseInventoryJSON>().SavePlayerDataToJson();
            }
            else
            {
                GameObject.FindGameObjectWithTag("FloorItem").transform.GetChild(i).gameObject.SetActive(false);
                inventoryItem.floorItemList[i].use_check = false;
                itemListCS.GetComponent<HouseInventoryJSON>().SavePlayerDataToJson();
            }
        }
    }


}
