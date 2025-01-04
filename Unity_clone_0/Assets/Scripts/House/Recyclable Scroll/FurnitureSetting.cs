using PolyAndCode.UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

//Cell class for demo. A cell in Recyclable Scroll Rect must have a cell class inheriting from ICell.
//The class is required to configure the cell(updating UI elements etc) according to the data during recycling of cells.
//The configuration of a cell is done through the DataSource SetCellData method.
//Check RecyclableScrollerDemo class
public class FurnitureSetting : MonoBehaviour, ICell
{
    //UI
    public Image itemImage;
   

    private void Start()
    {
        //Can also be done in the inspector
        GetComponent<Button>().onClick.AddListener(ButtonListener);
    }

    //This is called from the SetCell method in DataSource
    public void ConfigureCell(FurnitureItemDictionary contactInfo, int cellIndex)
    {
        string spriteName = "HouseItemSprites/" + contactInfo.name;
        itemImage.sprite = Resources.Load <Sprite>(spriteName);
        gameObject.AddComponent<ItemDrag>();
    }


    private void ButtonListener()
    {
        Debug.Log("");
    }


}
