using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BadWordFiltering : MonoBehaviour
{
    public GameObject BadWordList;
    public BadWordData worldList;
    public int count;
    public string[] wordlist;
    void Awake()
    {
        BadWordData worldList= BadWordList.GetComponent<BadWordJSON>().getBadWordList();
        count = worldList.badWord.Length;
        wordlist=worldList.badWord;
    }
    //�弳�̸� true ��ȯ
    public bool CheckSentance(string word)
    {
        for(int i=0; i < count; i++)
        {
            if (word.Contains(wordlist[i]))
            {
               return true;
            }
        }
        return false;
     }
    
}
