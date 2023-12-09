using PolyAndCode.UI;
using UnityEngine;
using UnityEngine.UI;

//Cell class for demo. A cell in Recyclable Scroll Rect must have a cell class inheriting from ICell.
//The class is required to configure the cell(updating UI elements etc) according to the data during recycling of cells.
//The configuration of a cell is done through the DataSource SetCellData method.
//Check RecyclableScrollerDemo class
public class FaceItemSetting : MonoBehaviour, ICell
{
    //UI
    public Image itemImage;

    //Model
    public FaceItemDictionary _contactInfo;
    private int _cellIndex;

    public int face_count=4;
 

    private void Start()
    {
        //Can also be done in the inspector
        GetComponent<Button>().onClick.AddListener(ButtonListener);
    }

    //This is called from the SetCell method in DataSource
    public void ConfigureCell(FaceItemDictionary contactInfo, int cellIndex)
    {
        _cellIndex = cellIndex;
        _contactInfo = contactInfo;

        string spriteName = "CharacterCustom/Face/" + contactInfo.sprite_name;
        itemImage.sprite = Resources.Load<Sprite>(spriteName);
    }


    private void ButtonListener()
    {
        for(int i=0;i< face_count; i++)
        {
            if (_contactInfo.num == i)
            {
                GameObject.FindGameObjectWithTag("FacePanel").transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                GameObject.FindGameObjectWithTag("FacePanel").transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }


}
