using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
public class PhoneBtnClick : MonoBehaviour
{
    public Canvas phoneView;
    public Animator animator;
    public GameObject phone_change_screen_obj;

    private void Start()
    {
        phoneView.gameObject.SetActive(false);
    }
    public void OnPhone()
    {
        animator.SetBool("Click", true);
        
    }
    public void OpenPhone()
    {
        phoneView.gameObject.SetActive(true);
        animator.SetBool("Click", false);
        phone_change_screen_obj.GetComponent<PhoneChangeScreen>().BackBtnOnClick();
    }
    public void OffPhone()
    {
        phoneView.gameObject.SetActive(false);
    }
}
