using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadScene : MonoBehaviour
{
    public TextMeshProUGUI textContent;
    public string[] contents;
    string content;

    void Start()
    {   
        content = contents[Random.Range(0, contents.Length)];
        textContent.text = content;
    }
}
