// using System;
// using System.Text;
// using System.Collections;
// using UnityEngine;
// using UnityEngine.UI;
// using JimmysUnityUtilities;
// using LogicUI.FancyTextRendering;

// public class Images : MarkdownLineProcessorBase
// {
//     public override void Process(List<MarkdownLine> lines, MarkdownRenderingSettings settings)
//     {
//         foreach (var line in lines)
//         {
//             ProcessLine(line, settings);
//         }
//     }

//     private void ProcessLine(MarkdownLine line, MarkdownRenderingSettings settings)
//     {
//         var builder = line.Builder;
//         var text = builder.ToString();

//         // Regex to match Markdown image syntax ![alt text](url)
//         var regex = new System.Text.RegularExpressions.Regex(@"!\[(.*?)\]\((.*?)\)");
//         var matches = regex.Matches(text);

//         foreach (System.Text.RegularExpressions.Match match in matches)
//         {
//             var altText = match.Groups[1].Value;
//             var url = match.Groups[2].Value;

//             var imgTag = $"<sprite name=\"{url}\">"; // or use TMP's image tag with correct path

//             builder.Replace(match.Value, imgTag);
//         }
//     }
// }
