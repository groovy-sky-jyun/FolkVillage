using UnityEngine;

public class InventorySetActive : MonoBehaviour
{
    public GameObject furniturePanel;
    public GameObject wallPanel;
    public GameObject floorPanel;
    private bool open;

    //�ʱ�ȭ
    private void Start()
    {
        FurniturePanelOff();
        open = false;
    }
    //���� ������(�� �����Ҷ�, ��ư �ѹ� �� ������ Ŀ���� ��� �����Ҷ�)
    private void FurniturePanelOff()
    {
        furniturePanel.SetActive(false);
        wallPanel.SetActive(false);
        floorPanel.SetActive(false);
        open = false;
    }
    //ȭ�������� ������ư ������ ��
    public void CustomInventoryBtn()
    {
        if (open)//���� ���¿��� ��ư �ѹ� �� ������ ���� �� ����
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
    //�κ��丮 ���� �ٹ̱� ��ư ������ ��
    public void ClickFurnitureCustomBtn()
    {
        furniturePanel.SetActive(true);
        wallPanel.SetActive(false);
        floorPanel.SetActive(false);
    }
    //�κ��丮 ���� �ٹ̱� ��ư ������ ��
    public void ClickWallCustomBtn()
    {
        furniturePanel.SetActive(false);
        wallPanel.SetActive(true);
        floorPanel.SetActive(false);
    }
    //�κ��丮 �ٴ� �ٹ̱� ��ư ������ ��
    public void ClickTileCustomBtn()
    {
        furniturePanel.SetActive(false);
        wallPanel.SetActive(false);
        floorPanel.SetActive(true);
    }
}