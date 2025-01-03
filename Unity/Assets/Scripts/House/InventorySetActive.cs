using UnityEngine;

public class InventorySetActive : MonoBehaviour
{
    public GameObject furniturePanel;
    public GameObject wallPanel;
    public GameObject floorPanel;
    private bool open;

    //초기화
    private void Start()
    {
        FurniturePanelOff();
        open = false;
    }
    //전부 가리기(씬 시작할때, 버튼 한번 더 눌러서 커스텀 기능 종료할때)
    private void FurniturePanelOff()
    {
        furniturePanel.SetActive(false);
        wallPanel.SetActive(false);
        floorPanel.SetActive(false);
        open = false;
    }
    //화면우측상단 가구버튼 눌렀을 때
    public void CustomInventoryBtn()
    {
        if (open)//열린 상태에서 버튼 한번 더 눌리면 저장 및 종료
        {
            furniturePanel.SetActive(false);
            wallPanel.SetActive(false);
            floorPanel.SetActive(false);
            open = false;
        }
        else
        {
            open = true;
            ClickFurnitureCustomBtn();
        }
    }
    //인벤토리 가구 꾸미기 버튼 눌렀을 때
    public void ClickFurnitureCustomBtn()
    {
        furniturePanel.SetActive(true);
        wallPanel.SetActive(false);
        floorPanel.SetActive(false);
    }
    //인벤토리 벽지 꾸미기 버튼 눌렀을 때
    public void ClickWallCustomBtn()
    {
        furniturePanel.SetActive(false);
        wallPanel.SetActive(true);
        floorPanel.SetActive(false);
    }
    //인벤토리 바닥 꾸미기 버튼 눌렀을 때
    public void ClickTileCustomBtn()
    {
        furniturePanel.SetActive(false);
        wallPanel.SetActive(false);
        floorPanel.SetActive(true);
    }
}