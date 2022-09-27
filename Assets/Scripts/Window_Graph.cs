using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Window_Graph : MonoBehaviour
{

    [SerializeField] private Sprite circleSprite;
    public RectTransform graphContainer;

    public RectTransform labelTemplateX;
    public RectTransform labelTemplateY;
    public RectTransform dashTemplateX;
    public RectTransform dashTemplateY;

    private List<GameObject> gameObjectList; //This stores the graph nodes so they can be deleted

    private void Awake()
    {
        List<int> valueList = new List<int>() { 5, 98, 56, 45, 30, 22, 17, 15, 13, 17, 25, 37, 40, 36, 33 };
        gameObjectList = new List<GameObject>();
        int maxVisibleValueAmount = valueList.Count;

        ShowGraph(valueList, maxVisibleValueAmount);
    }

    private GameObject CreateCircle(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);

        return gameObject;
    }

    private void ShowGraph(List<int> valueList, int maxVisibleValueAmount)
    {
        foreach(GameObject gameObject in gameObjectList) //Prevent duplicate overlapping graphs
        {
            Destroy(gameObject);
        }
        gameObjectList.Clear();

        float graphHeight = graphContainer.sizeDelta.y;
        float graphWidth = graphContainer.sizeDelta.x;

        float yMaximum = Mathf.Max(valueList.ToArray()) + (Mathf.Max(valueList.ToArray()) - Mathf.Min(valueList.ToArray())) *0.2f;
        float yMinimum = Mathf.Min(valueList.ToArray()) - (Mathf.Max(valueList.ToArray()) - Mathf.Min(valueList.ToArray())) * 0.2f;
        if(yMinimum < 0)
        {
            yMinimum = 0;
        }
        float xSize = graphWidth/ (maxVisibleValueAmount+1);

        GameObject lastCircleGameObject = null; //This stores the previously created circle for creating Lines between the points

        int xIndex = 0;

        for(int i = Mathf.Max(valueList.Count - maxVisibleValueAmount, 0); i < valueList.Count; i++)
        {
            float xPosition = xSize + xIndex * xSize;
            float yPosition = ((valueList[i] - yMinimum)/(yMaximum - yMinimum)) * graphHeight;
            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));
            gameObjectList.Add(circleGameObject);

            //--- For creating lines between the circles ---
            if(lastCircleGameObject != null)
            {
                GameObject dotConnectionGameObject = CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition);
                gameObjectList.Add(dotConnectionGameObject);
            }
            lastCircleGameObject = circleGameObject;

            //---- Create X-axis Labels and Dashes ----
            RectTransform labelX = Instantiate(labelTemplateX);
            labelX.SetParent(graphContainer);
            labelX.gameObject.SetActive(true);
            labelX.anchoredPosition = new Vector2(xPosition, -10f);
            labelX.GetComponent<Text>().text = i.ToString();
            gameObjectList.Add(labelX.gameObject);

            RectTransform dashX = Instantiate(dashTemplateX);
            dashX.SetParent(graphContainer);
            dashX.gameObject.SetActive(true);
            dashX.anchoredPosition = new Vector2(xPosition, 0f);
            dashX.sizeDelta = new Vector2(graphContainer.rect.height+90, 3);
            gameObjectList.Add(dashX.gameObject);

            xIndex++;
        }

        //---- Create Y-axis Labels and Dashes ----
        int separatorCount = 10;
        for (int i = 0; i <= separatorCount; i++)
        {
            RectTransform labelY = Instantiate(labelTemplateX);
            labelY.SetParent(graphContainer);
            labelY.gameObject.SetActive(true);
            float normalizedValue = i * 1f / separatorCount;
            labelY.anchoredPosition = new Vector2(-10f, normalizedValue*graphHeight);
            labelY.GetComponent<Text>().text = Mathf.RoundToInt(yMinimum + normalizedValue*(yMaximum-yMinimum)).ToString();
            gameObjectList.Add(labelY.gameObject);

            RectTransform dashY = Instantiate(dashTemplateY);
            dashY.SetParent(graphContainer);
            dashY.gameObject.SetActive(true);
            dashY.anchoredPosition = new Vector2(0f, normalizedValue * graphHeight);
            dashY.sizeDelta = new Vector2(graphContainer.rect.width+180, 3);
            gameObjectList.Add(dashY.gameObject);
        }
    }

    private GameObject CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB)
    {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 1.5f); //Changes thickness and length of connecting lines
        rectTransform.anchoredPosition = dotPositionA + dir*distance*0.5f;
        float angle = Mathf.Atan2(dotPositionB.y - dotPositionA.y, dotPositionB.x - dotPositionA.x) * 180 / Mathf.PI;
        rectTransform.localEulerAngles = new Vector3(0, 0, angle);
        return gameObject;
    }
}
