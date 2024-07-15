using System;
using System.Text;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using JimmysUnityUtilities;
using LogicUI.FancyTextRendering;

namespace LogicUI.FancyTextRendering.MarkdownLogic
{
    class Images : SimpleMarkdownLineProcessor
    { 
        protected override void ProcessLine(MarkdownLine line, MarkdownRenderingSettings settings)
        {            
            StringBuilder builder = line.Builder;
            var text = builder.ToString();

            // Regex to match Markdown image syntax ![alt text](url)
            var regex = new System.Text.RegularExpressions.Regex(@"!\[(.*?)\]\((.*?)\)");
            var matches = regex.Matches(text);

            foreach (System.Text.RegularExpressions.Match match in matches)
            {
                GameObject verticalLayout = GameObject.Find("Vertical Layout");
                if (verticalLayout == null) {
                    Debug.LogError("GameObject 'VerticalLayout' not found.");
                } else {
                    ImageHelper imageHelper = verticalLayout.GetComponent<ImageHelper>();
                    if (imageHelper == null)
                    {
                        Debug.LogError("ImageHelper component not found on GameObject 'VerticalLayout'.");
                    }
                    else
                    {
                        imageHelper.DisplayImage(match.Groups[2].Value);
                        
                        var altText = match.Groups[1].Value;
                        var url = match.Groups[2].Value; 
                        var imgTag = $"<sprite name=\"{url}\">"; // or use TMP's image tag with correct path
                        builder.Replace(match.Value, imgTag);
                    }
                }
                
                // var altText = match.Groups[1].Value;
                // var url = match.Groups[2].Value;

                // var imgTag = $"<sprite name=\"{url}\">"; // or use TMP's image tag with correct path

                // builder.Replace(match.Value, imgTag);
            }
        }
    }
}