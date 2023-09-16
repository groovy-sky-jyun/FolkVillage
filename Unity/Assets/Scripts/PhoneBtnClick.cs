using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
public class PhoneBtnClick : MonoBehaviour
{
    public Canvas phoneView;
    public Animator animator;

    public void OnPhone()
    {
        animator.SetBool("Click", true);
        
    }
    public void OpenPhone()
    {
        phoneView.gameObject.SetActive(true);
        animator.SetBool("Click", false);
    }
    public void OffPhone()
    {
        phoneView.gameObject.SetActive(false);
    }
}
