using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//[ExecuteInEditMode]
public class ScrollController : MonoBehaviour
{

    

    private bool isSliding = false;

    public Scrollbar scrollbar;
    

    public float ScrollStep;

    private RectTransform contentRect;
    List<RectTransform> objectsRects = new List<RectTransform>();

    private void ResetScroll() {


        contentRect = GetComponent<RectTransform>();
        contentRect.offsetMax = new Vector2(0f, contentRect.offsetMax.y);
        contentRect.offsetMin = new Vector2(0f, contentRect.offsetMin.y);

        objectsRects.Clear();
        foreach (Transform child in transform)
        {
            objectsRects.Add(child.GetComponent<RectTransform>());
            Debug.Log(child.name);
        }

        if (contentRect != null)
            contentRect.offsetMax = new Vector2(0, contentRect.offsetMax.y);

        foreach (RectTransform go in objectsRects) { 
            if(go != null)
            {
                go.anchoredPosition = new Vector2(400, go.anchoredPosition.y);
            }
        }

        ScrollStep = 1f / (objectsRects.Count - 1);

        

    }

    public void UpdateScroll() {


        ResetScroll();

        Vector2 prevPanelPos = objectsRects[0].anchoredPosition;

        foreach (RectTransform go in objectsRects)
        {
            if (go != null && go != objectsRects[0])
            {
                contentRect.offsetMax = new Vector2(contentRect.offsetMax.x + 800f, contentRect.offsetMax.y);

                go.anchoredPosition = new Vector2(prevPanelPos.x + 800f, go.anchoredPosition.y);

                prevPanelPos = go.anchoredPosition;
            }
        }


        Debug.Log("Updated!");
    }

    IEnumerator Increase() {

        float elapsedTime = 0;
        float waitTime = 0.35f;
        float scrollVal = scrollbar.value;
        float IncreasedScrollVal = scrollVal + ScrollStep;

        while (elapsedTime < waitTime) {
            scrollbar.value = Mathf.Lerp(scrollVal, IncreasedScrollVal, (elapsedTime / waitTime));

            elapsedTime += Time.deltaTime;
            isSliding = true;
            yield return null;
        }

        scrollbar.value = IncreasedScrollVal;
        isSliding = false;
        yield return null;
    }

    IEnumerator Decrease()
    {

        float elapsedTime = 0;
        float waitTime = 0.35f;
        float scrollVal = scrollbar.value;
        float DecreasedScrollVal = scrollVal - ScrollStep;

        while (elapsedTime < waitTime)
        {
            scrollbar.value = Mathf.Lerp(scrollVal, DecreasedScrollVal, (elapsedTime / waitTime));

            elapsedTime += Time.deltaTime;
            isSliding = true;
            yield return null;
        }

        scrollbar.value = DecreasedScrollVal;
        isSliding = false;
        yield return null;
    }

    public void ShowNext() {

       

        if (scrollbar.value >= 0.999f || isSliding)
            return;

        StartCoroutine(Increase());     

    }

    public void ShowPrev() {
        if (scrollbar.value <= 0 || isSliding)
            return;

        StartCoroutine(Decrease());

    }

    
}
