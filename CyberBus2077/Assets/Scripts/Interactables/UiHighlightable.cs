using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiHighlightable : Highlightable
{
    [Header("UI Highlightable")]
    public Button button;
    public Image image;
    public TextMeshProUGUI text;

    public FontStyles mainFontStyle;
    public FontStyles highlightFontStyle;
    public Color mainColor;

    public override void Highlight()
    {
        text.color = highlightColor;
        text.fontStyle = highlightFontStyle;
    }

    public override void UnHighlight()
    {
        text.color = mainColor;
        text.fontStyle = mainFontStyle;
    }

    public override void Interact(Tool t)
    {
        button.onClick.Invoke();
    }
}
