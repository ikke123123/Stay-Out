using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayText : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float displayTime = 5;

    private TextMeshProUGUI tmp = null;

    private void Awake()
    {
        tmp = GetComponent<TextMeshProUGUI>();
    }

    public void TextDisplay(string text)
    {
        StartCoroutine(SetText(text));
    }

    private IEnumerator SetText(string text)
    {
        tmp.text = text;
        while (displayTime == -1)
        {
            yield return new WaitForSeconds(1000);
        }
        yield return new WaitForSeconds(displayTime);
        tmp.text = "";
    }
}
