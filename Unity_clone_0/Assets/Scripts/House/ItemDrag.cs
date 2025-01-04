using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform insideCanvas;               // UI�� �ҼӵǾ� �ִ� �ֻ���� insideCanvas Transform
    private RectTransform rect;             // UI ��ġ ��� ���� RectTransform
    private GameObject houseItemJson;
    private float x, y;
    private GameObject furniturePanel;

    public int number;
    public int type;
    private HouseInventoryData inventoryItem;
    private int width;
    private int height;
    private Canvas canvas;
    private void Awake()
    {
        insideCanvas = GameObject.Find("InsideCanvas").transform;
        rect = GetComponent<RectTransform>();
        //insideCanvasGroup = GetComponent<insideCanvasGroup>();
        houseItemJson = GameObject.Find("ItemListJSON");
        furniturePanel = GameObject.Find("FurniturePanel");
        inventoryItem = houseItemJson.GetComponent<HouseInventoryJSON>().getHouseItem();
        canvas = GetComponentInParent<Canvas>();
    }
    public void setNum(int num)
    {
        number = num;
    }
    /// <summary>
    /// ���� ������Ʈ�� �巡���ϱ� ������ �� 1ȸ ȣ��
    /// </summary>
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (type == 0)
        {
            // insideCanvas�� �θ�� ����
            transform.SetParent(insideCanvas);       
            // �θ��� �ڽĵ� �� ù��° ������ ����(���� �� �ڷ� ������)
            //transform.SetAsFirstSibling();      
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0, 1);
            //�κ��丮 �����
            furniturePanel.SetActive(false);
        }
        
    }

    /// <summary>
    /// ���� ������Ʈ�� �巡�� ���� �� �� ������ ȣ��
    /// </summary>
    public void OnDrag(PointerEventData eventData)
    {
        if (type == 0)
        {
            rect.GetComponent<RectTransform>().sizeDelta = new Vector2(inventoryItem.furnitureItemList[number].width, inventoryItem.furnitureItemList[number].height);
            width = inventoryItem.furnitureItemList[number].width / 2;
            height = inventoryItem.furnitureItemList[number].height / 2;

            // ���콺 ��ġ�� ���� ��ǥ�� ��ȯ
            Vector2 localPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(insideCanvas.transform as RectTransform, eventData.position, canvas.worldCamera, out localPosition);
            // �������� ���� ��ġ�� ���콺 ��ġ�� ������Ʈ
            rect.localPosition = new Vector3(localPosition.x - width, localPosition.y + height, 0);
        }
        
        
    }

    /// <summary>
    /// ���� ������Ʈ�� �巡�׸� ������ �� 1ȸ ȣ��
    /// </summary>
    public void OnEndDrag(PointerEventData eventData)
    {
        if(type == 0)
        {
            // ���콺 ��ġ�� ���� ��ǥ�� ��ȯ
            Vector2 localPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(insideCanvas.transform as RectTransform, eventData.position, canvas.worldCamera, out localPosition);

            //������ ��ǥ����
            x = localPosition.x  - width;
            y = localPosition.y + height;
            rect.localPosition = new Vector3(x, y, 0);
            //json���Ͽ� use_check=true����
            inventoryItem.furnitureItemList[number].use_check = true;
            //json���Ͽ� x��ǥ, y��ǥ ����
            inventoryItem.furnitureItemList[number].x = x+width;
            inventoryItem.furnitureItemList[number].y = y-height;
            //json���Ͽ� ����� ���� ����
            houseItemJson.GetComponent<HouseInventoryJSON>().SavePlayerDataToJson();
            
            //�κ��丮 ���̱�
            furniturePanel.SetActive(true);
        }
    }
}

