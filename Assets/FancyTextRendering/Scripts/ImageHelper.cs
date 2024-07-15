using JimmysUnityUtilities;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;


//todo: attach as a component of image? test? vert layout?
//call function from Links.cs line 51
//test
//maybe change this so start calls imagehelper and imagehelper is private?
namespace LogicUI.FancyTextRendering
{
    /// Loads Images
    public class ImageHelper : MonoBehaviour
    {
        public void DisplayImage(String imgPath)
        {
            imgPath = imgPath.Substring(3, imgPath.Length-7);
            Debug.Log("Extracted imgPath: " + imgPath);

            int childCount = gameObject.transform.parent.childCount;
            // Debug.Log("Parent" + gameObject.transform.parent.name + " has " + childCount + " children.");
            //Transform VerticalLayout = gameObject.transform.parent.GetChild(4);
            GameObject imgBlock = gameObject.transform.parent.GetChild(0).gameObject;
            Transform verticalLayout = gameObject.transform;
            GameObject block = Instantiate(imgBlock, verticalLayout);
            Debug.Log("Instantiated image block");

            block.SetActive(true);
            Debug.Log("Set image block active");

            Sprite image = Resources.Load<Sprite>(imgPath);
            if (image != null) {
                Debug.Log("Loaded image successfully from path: " + imgPath);
            } else {
                Debug.LogError("Failed to load image from path: " + imgPath);
            }
            
            StartCoroutine(PlaceImageBlock(block, image));
            Debug.Log("Started coroutine to place image block");
        }


        // In order to get the width of the image inside the vertical layout, we need to wait one frame for it to update
        private IEnumerator PlaceImageBlock(GameObject block, Sprite image)
        {
            yield return null;
            block.GetComponent<Image>().sprite = image;
            block.GetComponent<LayoutElement>().preferredHeight = (image.texture.height / (float)image.texture.width) * block.GetComponent<RectTransform>().rect.width;
        }
    }
}