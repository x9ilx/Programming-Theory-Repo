using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    [System.Serializable]
    public struct onClickParameter
    {
        [Header("Color on click")]
        public Color colorBorderOnClick;
        public Color colorPanelOnClick;
        public Color colorTextOnClick;
    }


    public onClickParameter colorsOnClick;


    private Color deffaultColorButtonBorder;
    private Color deffaultColorButtonPanel;
    private Color deffaultColorButtonText;
    private bool click = false;
    private bool changeColor = false;
    private Image panel;
    private Button thisButton;
    private TextMeshProUGUI text;
    public void OnClick()
    {
        click = true;
    }

    private void Start()
    {

        panel = transform.Find("Panel").gameObject.GetComponent<Image>();
        thisButton = gameObject.GetComponent<Button>();
        text = transform.Find("Text (TMP)").gameObject.GetComponent<TextMeshProUGUI>();

        deffaultColorButtonBorder = thisButton.colors.normalColor;
        deffaultColorButtonPanel = panel.color;
        deffaultColorButtonText = text.color;

    }

    void ChangeOnNewColor()
    {
        ColorBlock cb = new ColorBlock();
        cb.normalColor = colorsOnClick.colorBorderOnClick;
        cb.disabledColor = thisButton.colors.disabledColor;
        cb.selectedColor = colorsOnClick.colorBorderOnClick;
        cb.pressedColor = colorsOnClick.colorBorderOnClick;
        thisButton.colors = cb;

        panel.color = colorsOnClick.colorPanelOnClick;

        text.color = colorsOnClick.colorTextOnClick;
    }
    void ChangeOnOldColor()
    {
        ColorBlock cb = new ColorBlock();
        cb.normalColor = deffaultColorButtonBorder;
        cb.disabledColor = thisButton.colors.disabledColor;
        cb.selectedColor = deffaultColorButtonBorder;
        cb.pressedColor = deffaultColorButtonBorder;
        thisButton.colors = cb;

        panel.color = deffaultColorButtonPanel;

        text.color = deffaultColorButtonText;
    }
    private void Update()
    {
        if (click)
        {
            if (Input.GetMouseButtonDown(0) && !changeColor)
            {
                ChangeOnNewColor();
                changeColor = true;
            }
            if (Input.GetMouseButtonUp(0) && changeColor)
            {
                ChangeOnOldColor();
                changeColor = false;
                click = false;
            }
        }
    }



}
