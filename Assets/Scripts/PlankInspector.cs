using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.UI;


/*
This class makes the player able to inspect objects
Inspection happens when the player is hovering an object with either right or left ray interactors and pressing the trigger
This class is meant to be attacthed to the player
*/
public class PlankInspector : MonoBehaviour
{
    public void SendInfoPanel()
    {
        Debug.Log("plank inspector called");
        Transform infoPanel = GameObject.Find("Info Panel").transform;
        Transform goal = gameObject.transform;
        TextAsset document = Resources.Load<TextAsset>(goal.name);
        if (document == null) return;

        infoPanel.GetComponent<LazyFollow>().target = goal;
        infoPanel.transform.position = goal.position + Vector3.up * 0.1f;
        infoPanel.GetComponent<CanvasGroup>().alpha = 1;
        infoPanel.GetComponent<CanvasGroup>().interactable = true;
        infoPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
        infoPanel.GetChild(0).GetChild(0).GetComponent<BoxCollider>().enabled = true;

        infoPanel.GetComponentInChildren<FormattedDocumentDisplay>().DisplayDocument(document);
    }
}
