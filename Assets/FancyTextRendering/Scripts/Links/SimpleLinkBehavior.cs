using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LogicUI.FancyTextRendering
{
    /// <summary>
    /// Allows links in TextMeshPro text objects to be clicked on, and gives them custom colors when they are hovered or clicked.
    /// </summary>
    [RequireComponent(typeof(TextLinkHelper))]
    [DisallowMultipleComponent]
    public class SimpleLinkBehavior : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<TextLinkHelper>().OnLinkClicked += ClickOnLink;
        }

        private void ClickOnLink(string linkID)
        {
            if (CustomLinks.TryGetValue(linkID, out var action))
                action?.Invoke();
            else
                // if (linkID.Contains(".png"))
                //     DisplayImage(linkID);
                // else 
                Application.OpenURL(linkID);

            Debug.Log($"clicked on link: {linkID}");
        }


        // private void DisplayImage(String imgPath)
        // {
        //     // Debug.Log("DisplayImage called with param: " + param);

        //     // String imgPath = param;
        //     imgPath = imgPath.Substring(0, imgPath.Length-4);
        //     Debug.Log("Extracted imgPath: " + imgPath);

        //     int childCount = gameObject.transform.parent.childCount;
        //     // Debug.Log("Parent" + gameObject.transform.parent.name + " has " + childCount + " children.");
        //     GameObject imageBlock = gameObject.transform.parent.parent.GetChild(0).gameObject;
        //     // Transform verticalLayout = gameObject;
        //     GameObject block = Instantiate(imageBlock, gameObject.transform);
        //     Debug.Log("Instantiated image block");

        //     block.SetActive(true);
        //     Debug.Log("Set image block active");

        //     Sprite image = Resources.Load<Sprite>(imgPath);
        //     if (image != null)
        //     {
        //         Debug.Log("Loaded image successfully from path: " + imgPath);
        //     }
        //     else
        //     {
        //         Debug.LogError("Failed to load image from path: " + imgPath);
        //     }

        //     StartCoroutine(PlaceImageBlock(block, image));
        //     Debug.Log("Started coroutine to place image block");
        // }


        // // In order to get the width of the image inside the vertical layout, we need to wait one frame for it to update
        // private IEnumerator PlaceImageBlock(GameObject block, Sprite image)
        // {
        //     yield return null;
        //     block.GetComponent<Image>().sprite = image;
        //     block.GetComponent<LayoutElement>().preferredHeight = (image.texture.height / (float)image.texture.width) * block.GetComponent<RectTransform>().rect.width;
        // }

        private Dictionary<string, Action> CustomLinks = new Dictionary<string, Action>();

        /// <summary>
        /// Sets some code to be run when a link is clicked on. 
        /// If a link doesn't have a custom action set, we will use <see cref="Application.OpenURL(string)"/> on it.
        /// </summary>
        /// <param name="linkID"></param>
        /// <param name="linkAction"></param>
        public void SetCustomLink(string linkID, Action linkAction)
        {
            CustomLinks[linkID] = linkAction;
        }
    }
}