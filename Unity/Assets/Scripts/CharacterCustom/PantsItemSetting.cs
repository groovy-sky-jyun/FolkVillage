using PolyAndCode.UI;
using UnityEngine;
using UnityEngine.UI;

//Cell class for demo. A cell in Recyclable Scroll Rect must have a cell class inheriting from ICell.
//The class is required to configure the cell(updating UI elements etc) according to the data during recycling of cells.
//The configuration of a cell is done through the DataSource SetCellData method.
//Check RecyclableScrollerDemo class
public class PantsItemSetting : MonoBehaviour, ICell
{
    //UI
    public Image itemImage;

    //Model
    public PantsItemDictionary _contactInfo;
    private int _cellIndex;

    public int pants_count = 4;

    private void Start()
    {
        //Can also be done in the inspector
        GetComponent<Button>().onClick.AddListener(ButtonListener);
    }

    //This is called from the SetCell method in DataSource
    public void ConfigureCell(PantsItemDictionary contactInfo, int cellIndex)
    {
        _cellIndex = cellIndex;
        _contactInfo = contactInfo;
        string spriteName = "CharacterCustom/Pants/" + contactInfo.sprite_name;
        itemImage.sprite = Resources.Load<Sprite>(spriteName);
    }


    private void ButtonListener()
    {
        for (int i = 0; i < pants_count; i++)
        {
            if (_contactInfo.num == i)
            {
                GameObject.FindGameObjectWithTag("PantsPanel").transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                GameObject.FindGameObjectWithTag("PantsPanel").transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }


}
