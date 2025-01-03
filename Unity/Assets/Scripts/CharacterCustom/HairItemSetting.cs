using PolyAndCode.UI;
using UnityEngine;
using UnityEngine.UI;

//Cell class for demo. A cell in Recyclable Scroll Rect must have a cell class inheriting from ICell.
//The class is required to configure the cell(updating UI elements etc) according to the data during recycling of cells.
//The configuration of a cell is done through the DataSource SetCellData method.
//Check RecyclableScrollerDemo class
public class HairItemSetting : MonoBehaviour, ICell
{
    //UI
    public Image itemImage;

    //Model
    public HairItemDictionary _contactInfo;
    private int _cellIndex;

    public int hair_count=4;
    private string spriteName;

    private void Start()
    {
        //Can also be done in the inspector
        GetComponent<Button>().onClick.AddListener(ButtonListener);
    }

    //This is called from the SetCell method in DataSource
    public void ConfigureCell(HairItemDictionary contactInfo, int cellIndex)
    {
        _cellIndex = cellIndex;
        _contactInfo = contactInfo;

        spriteName = "CharacterCustom/Hair/" + contactInfo.sprite_name;
        itemImage.sprite = Resources.Load<Sprite>(spriteName);
    }


    private void ButtonListener()
    {
        Transform hair = GameObject.Find("PlayerHair").transform.Find("Hair");
        hair.gameObject.SetActive(true);
        Image hairImage = hair.gameObject.GetComponent<Image>();
        if (hairImage != null)
        {
            hairImage.sprite = Resources.Load<Sprite>(spriteName);
        }
    }


}
