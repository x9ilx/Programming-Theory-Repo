using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
{

    public float delayShow = 2;
    public Vector2 offsetTooltip = new Vector2(2, 2);
    public string message;
    public GameObject tooltip;

    private Vector2 currSizeToolTip;
    private float maxWidth = 200;
    private float minWidth = 100;
    private float minHeight = 20;
    private bool created = false;
    private bool show = false;
    private GameObject instance;
    // Start is called before the first frame update


    void ShowToolTip()
    {
        instance.SetActive(true);
        show = true;
    }
    void HideToolTip()
    {
        instance.SetActive(false);
        show = false;
    }
    void CreateToolTip()
    {
        if (!created)
        {
            
            instance.GetComponentInChildren<TextMeshProUGUI>().text = message;

            Vector2 size = (instance.GetComponentInChildren<TextMeshProUGUI>().GetRenderedValues(true));

            TMP_TextInfo info = instance.GetComponentInChildren<TextMeshProUGUI>().GetTextInfo(message);

            size.y = Mathf.Max(minHeight, info.lineInfo[0].lineHeight*(info.lineInfo.Length-2));
            size.x = Mathf.Max(minWidth, info.lineInfo[0].width);
            size.x = Mathf.Min(maxWidth, size.x);

            instance.GetComponent<RectTransform>().sizeDelta = size;
            currSizeToolTip = size;
            created = true;
            
        }
    }

    private void OnGUI()
    {
        if (instance == null)
        {
            instance = Instantiate(tooltip, gameObject.transform);

        }
        else
        {
            CreateToolTip();
            
        }
    }

    Rect RectTransformToScreenSpace(RectTransform transform)
    {
        Vector2 size = Vector2.Scale(transform.rect.size, transform.lossyScale);
        float x = transform.position.x-transform.sizeDelta.x/2;
        float y = transform.position.y - transform.sizeDelta.y/2;

        return new Rect(x, y, size.x, size.y);
    }

    private float timerShowDelay = 0;
    // Update is called once per frame
    void Update()
    {
        if (instance != null)
        {
            Debug.Log(gameObject.name);
            if (RectTransformToScreenSpace(gameObject.GetComponent<RectTransform>()).Contains(Input.mousePosition))
            {
                if (timerShowDelay >= delayShow)
                {
                    ShowToolTip();
                }
                else
                {
                    timerShowDelay += Time.deltaTime;
                }
            }
            else
            {
                timerShowDelay = 0;
                HideToolTip();
            }

            if (show)
            {
                Vector2 mousePosition = Input.mousePosition;
                Vector2 newPosition = mousePosition + (currSizeToolTip / 2) + offsetTooltip;
                instance.transform.position = newPosition;
            }
        }
    }
}
