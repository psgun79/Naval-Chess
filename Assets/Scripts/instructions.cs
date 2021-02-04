using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class instructions : MonoBehaviour
{
    public List<GameObject> pages;
    public Text page_text;
    public Button prev;
    public Button next;
    int page_num = 0;

    public void nextPage()
    {
        prev.interactable = true;
        pages[page_num].SetActive(false);
        page_num++;
        pages[page_num].SetActive(true);
        page_text.text = (page_num + 1).ToString();
        if (page_num == pages.Count - 1) next.interactable = false;
    }

    public void prevPage()
    {
        next.interactable = true;
        pages[page_num].SetActive(false);
        page_num--;
        pages[page_num].SetActive(true);
        page_text.text = (page_num + 1).ToString();
        if (page_num == 0) prev.interactable = false;
    }
}
