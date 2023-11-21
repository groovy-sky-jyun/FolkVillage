using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform canvas;               // UI가 소속되어 있는 최상단의 Canvas Transform
    private RectTransform rect;             // UI 위치 제어를 위한 RectTransform
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
    /// 현재 오브젝트를 드래그하기 시작할 때 1회 호출
    /// </summary>
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (type == 0)
        {
            // canvas를 부모로 설정
            transform.SetParent(canvas);       
            // 부모의 자식들 중 첫번째 순서로 설정(가장 맨 뒤로 보내기)
            //transform.SetAsFirstSibling();      
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0, 1);
            //인벤토리 숨기기
            inventoryPanel_1.SetActive(false);
        }
        
    }

    /// <summary>
    /// 현재 오브젝트를 드래그 중일 때 매 프레임 호출
    /// </summary>
    public void OnDrag(PointerEventData eventData)
    {
        if (type == 0)
        {
            // 현재 스크린상의 마우스 위치를 UI 위치로 설정 (UI가 마우스를 쫓아다니는 상태)
            rect.localPosition = new Vector3(eventData.position.x - 960, eventData.position.y - 540, 0);
            //rect.localScale = new Vector2(2, 2);
            rect.GetComponent<RectTransform>().sizeDelta = new Vector2(inventoryItem.furnitureList[number].width, inventoryItem.furnitureList[number].height);
            Debug.Log(rect.localPosition + "//" + eventData.position);
        }
        
        
    }

    /// <summary>
    /// 현재 오브젝트의 드래그를 종료할 때 1회 호출
    /// </summary>
    public void OnEndDrag(PointerEventData eventData)
    {
        if(type == 0)
        {
            //아이템 좌표설정
            x = eventData.position.x - 960;
            y = eventData.position.y - 540;
            rect.localPosition = new Vector3(x, y, 0);

            //json파일에 use_check=true설정
            
            inventoryItem.furnitureList[number].use_check = true;
            //json파일에 x좌표, y좌표 설정
            inventoryItem.furnitureList[number].x = x;
            inventoryItem.furnitureList[number].y = y;
            //json파일에 변경된 정보 저장
            houseItemJson.GetComponent<HouseInventoryJSON>().SavePlayerDataToJson();
            
            //인벤토리 보이기
            inventoryPanel_1.SetActive(true);
        }
    }
}

