using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform insideCanvas;               // UI가 소속되어 있는 최상단의 insideCanvas Transform
    private RectTransform rect;             // UI 위치 제어를 위한 RectTransform
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
    /// 현재 오브젝트를 드래그하기 시작할 때 1회 호출
    /// </summary>
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (type == 0)
        {
            // insideCanvas를 부모로 설정
            transform.SetParent(insideCanvas);       
            // 부모의 자식들 중 첫번째 순서로 설정(가장 맨 뒤로 보내기)
            //transform.SetAsFirstSibling();      
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0, 1);
            //인벤토리 숨기기
            furniturePanel.SetActive(false);
        }
        
    }

    /// <summary>
    /// 현재 오브젝트를 드래그 중일 때 매 프레임 호출
    /// </summary>
    public void OnDrag(PointerEventData eventData)
    {
        if (type == 0)
        {
            rect.GetComponent<RectTransform>().sizeDelta = new Vector2(inventoryItem.furnitureItemList[number].width, inventoryItem.furnitureItemList[number].height);
            width = inventoryItem.furnitureItemList[number].width / 2;
            height = inventoryItem.furnitureItemList[number].height / 2;

            // 마우스 위치를 로컬 좌표로 변환
            Vector2 localPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(insideCanvas.transform as RectTransform, eventData.position, canvas.worldCamera, out localPosition);
            // 아이템의 로컬 위치를 마우스 위치로 업데이트
            rect.localPosition = new Vector3(localPosition.x - width, localPosition.y + height, 0);
        }
        
        
    }

    /// <summary>
    /// 현재 오브젝트의 드래그를 종료할 때 1회 호출
    /// </summary>
    public void OnEndDrag(PointerEventData eventData)
    {
        if(type == 0)
        {
            // 마우스 위치를 로컬 좌표로 변환
            Vector2 localPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(insideCanvas.transform as RectTransform, eventData.position, canvas.worldCamera, out localPosition);

            //아이템 좌표설정
            x = localPosition.x  - width;
            y = localPosition.y + height;
            rect.localPosition = new Vector3(x, y, 0);
            //json파일에 use_check=true설정
            inventoryItem.furnitureItemList[number].use_check = true;
            //json파일에 x좌표, y좌표 설정
            inventoryItem.furnitureItemList[number].x = x+width;
            inventoryItem.furnitureItemList[number].y = y-height;
            //json파일에 변경된 정보 저장
            houseItemJson.GetComponent<HouseInventoryJSON>().SavePlayerDataToJson();
            
            //인벤토리 보이기
            furniturePanel.SetActive(true);
        }
    }
}

