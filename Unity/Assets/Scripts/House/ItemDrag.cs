using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform canvas;               // UI�� �ҼӵǾ� �ִ� �ֻ���� Canvas Transform
    private RectTransform rect;             // UI ��ġ ��� ���� RectTransform
    private GameObject houseItemJson;
    private float x, y;
    private GameObject inventoryPanel_1;

    public int number;
    public int type;
    private HouseInventoryData inventoryItem;

    private void Awake()
    {
        canvas = GameObject.Find("Canvas").transform;
        rect = GetComponent<RectTransform>();
        //canvasGroup = GetComponent<CanvasGroup>();
        houseItemJson = GameObject.Find("ItemListJSON");
        inventoryPanel_1 = GameObject.Find("InventoryPanel_1");
        inventoryItem = houseItemJson.GetComponent<HouseInventoryJSON>().getHouseItem();
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
            // canvas�� �θ�� ����
            transform.SetParent(canvas);       
            // �θ��� �ڽĵ� �� ù��° ������ ����(���� �� �ڷ� ������)
            //transform.SetAsFirstSibling();      
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0, 1);
            //�κ��丮 �����
            inventoryPanel_1.SetActive(false);
        }
        
    }

    /// <summary>
    /// ���� ������Ʈ�� �巡�� ���� �� �� ������ ȣ��
    /// </summary>
    public void OnDrag(PointerEventData eventData)
    {
        if (type == 0)
        {
            // ���� ��ũ������ ���콺 ��ġ�� UI ��ġ�� ���� (UI�� ���콺�� �Ѿƴٴϴ� ����)
            rect.localPosition = new Vector3(eventData.position.x - 960, eventData.position.y - 540, 0);
            //rect.localScale = new Vector2(2, 2);
            rect.GetComponent<RectTransform>().sizeDelta = new Vector2(inventoryItem.furnitureList[number].width, inventoryItem.furnitureList[number].height);
            Debug.Log(rect.localPosition + "//" + eventData.position);
        }
        
        
    }

    /// <summary>
    /// ���� ������Ʈ�� �巡�׸� ������ �� 1ȸ ȣ��
    /// </summary>
    public void OnEndDrag(PointerEventData eventData)
    {
        if(type == 0)
        {
            //������ ��ǥ����
            x = eventData.position.x - 960;
            y = eventData.position.y - 540;
            rect.localPosition = new Vector3(x, y, 0);

            //json���Ͽ� use_check=true����
            
            inventoryItem.furnitureList[number].use_check = true;
            //json���Ͽ� x��ǥ, y��ǥ ����
            inventoryItem.furnitureList[number].x = x;
            inventoryItem.furnitureList[number].y = y;
            //json���Ͽ� ����� ���� ����
            houseItemJson.GetComponent<HouseInventoryJSON>().SavePlayerDataToJson();
            
            //�κ��丮 ���̱�
            inventoryPanel_1.SetActive(true);
        }
    }
}

