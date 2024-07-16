using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using LogicUI.FancyTextRendering;
using TMPro;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;
using Vuplex.WebView;
using LogicUI.FancyTextRendering.MarkdownLogic;

/*
This class handles the display of the information of inspected objects
It interprets a txt file and translates into a vertical layout
This class is expected to attached to a Info panel prefab, which has a vertical layout and templates for the elements that are spawned in it
The info panel this class is attached to should also be a child of the player object
*/
public class FormattedDocumentDisplay : MonoBehaviour
{
    public WebBrowserButtons webBrowserButtons;
    private GameObject imageBlock, textBlock, linkBlock, audioPlayerBlock;
    private Transform verticalLayout;
    void Start()
    {
        imageBlock = transform.GetChild(0).gameObject;
        textBlock = transform.GetChild(1).gameObject;
        linkBlock = transform.GetChild(2).gameObject;
        audioPlayerBlock = transform.GetChild(3).gameObject;
        verticalLayout = transform.GetChild(4);
    }

    // This function is called by the ObjectInspector class
    public void DisplayDocument(TextAsset document, string fileName)
    {
        foreach (Transform child in verticalLayout) Destroy(child.gameObject);

        string fileContents = document.text;
        fileContents = System.Text.RegularExpressions.Regex.Replace(fileContents, @"<\/?div(.*?)>\s*\n\s*", "");
        fileContents = System.Text.RegularExpressions.Regex.Replace(fileContents, @"  ", "");
        var regex = new System.Text.RegularExpressions.Regex(@"!\[(.*?)\]\((.*?)\)");
        var matches = regex.Matches(fileContents);
        var match = matches[0];
        var imgPath = match.Groups[2].Value;        //extracts the actual image path from the regex match: the part in ()
        int startIndex = match.Index;
        int endIndex = match.Length + startIndex;

        displayText(fileContents.Substring(0, startIndex));
        DisplayImage(imgPath);
        displayText(fileContents.Substring(endIndex));   
    }

    private void displayText(string text) {
        TextMeshProUGUI block = Instantiate(textBlock, verticalLayout).GetComponent<TextMeshProUGUI>();
        block.gameObject.SetActive(true);
        var markdownRenderer = block.GetComponent<MarkdownRenderer>();
        markdownRenderer.Source = text;
    }

    public void DisplayImage(String imgPath)
    {
        imgPath = imgPath.Substring(3, imgPath.Length-7);

        int childCount = gameObject.transform.parent.childCount;
        GameObject block = Instantiate(imageBlock, verticalLayout);

        block.SetActive(true);

        Sprite image = Resources.Load<Sprite>(imgPath);
        if (image != null) {
            Debug.Log("Loaded image successfully from path: " + imgPath);
        } else {
            Debug.LogError("Failed to load image from path: " + imgPath);
        }
        
        StartCoroutine(PlaceImageBlock(block, image));
    }


    // In order to get the width of the image inside the vertical layout, we need to wait one frame for it to update
    private IEnumerator PlaceImageBlock(GameObject block, Sprite image)
    {
        yield return null;
        Image imgComponent = block.GetComponent<Image>();
        if (imgComponent != null) imgComponent.sprite = image;
        block.GetComponent<LayoutElement>().preferredHeight = (image.texture.height / (float)image.texture.width) * block.GetComponent<RectTransform>().rect.width;
    }

    // private void DisplayAudioPlayer(String param)
    // {
    //     GameObject block = Instantiate(audioPlayerBlock, verticalLayout);
    //     block.gameObject.SetActive(true);
    //     string[] parameters = param.Split('(', ')')[1].Split(',');
    //     for (int i = 0; i < parameters.Length; i++) parameters[i] = parameters[i].Trim();

    //     TextMeshProUGUI tmp = block.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
    //     tmp.text = parameters[0];
    //     SetTMPParameters(tmp, int.Parse(parameters[1]), parameters[2], parameters[3]);
    //     GetComponent<AudioSource>().clip = Resources.Load<AudioClip>(parameters[4]);
    // }
}
