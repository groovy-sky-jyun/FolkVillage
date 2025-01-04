using PolyAndCode.UI;
using UnityEngine;
using UnityEngine.UI;

//Cell class for demo. A cell in Recyclable Scroll Rect must have a cell class inheriting from ICell.
//The class is required to configure the cell(updating UI elements etc) according to the data during recycling of cells.
//The configuration of a cell is done through the DataSource SetCellData method.
//Check RecyclableScrollerDemo class
public class FloorItemSetting : MonoBehaviour, ICell
{
    //UI
    public Text nameLabel;
    public Text priceLabel;
    public Image itemImage;

    //Model
    public FloorItemDictionary _contactInfo;
    private int _cellIndex;

    private void Start()
    {
        //Can also be done in the inspector
        GetComponent<Button>().onClick.AddListener(ButtonListener);
    }

    //This is called from the SetCell method in DataSource
    public void ConfigureCell(FloorItemDictionary contactInfo, int cellIndex)
    {
        _cellIndex = cellIndex;
        _contactInfo = contactInfo;

        nameLabel.text = contactInfo.name;
        priceLabel.text = contactInfo.price.ToString();

        string spriteName = "HouseItemSprites/" + contactInfo.sprite_name;
        itemImage.sprite = Resources.Load<Sprite>(spriteName);

      //  btnCS.GetComponent<PurchaseItemBtn>().type = contactInfo.type;
       // btnCS.GetComponent<PurchaseItemBtn>().num = contactInfo.num;
    }


    private void ButtonListener()
    {
        Debug.Log("Index : " + _cellIndex +  ", Name : " + _contactInfo.name);
    }


}
