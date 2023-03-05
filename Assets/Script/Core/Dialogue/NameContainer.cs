using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NameContainer
{
    [SerializeField]private GameObject root;
    [SerializeField] private TextMeshPro nameText;

    public void Show(string nameToShow = "")
    {
        root.SetActive(true);
        if(nameToShow != string.Empty)
            nameText.text = nameToShow;
    }

    public void Hide()
    {
        root.SetActive(false);
    }
}
