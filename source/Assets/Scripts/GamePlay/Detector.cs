using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Detector : MonoBehaviour
{
    /// <summary>
    /// VARIABLES
    /// </summary>
    public Collider detected;
    public List<String> InteractableTag;
    private PlayerManager playerManager;
    private QuestManager questManager;
    private NpcManager npcManager;
    private TalkManager talkManager;

    void Start()
    {
        playerManager = transform.parent.GetComponent<PlayerManager>();
        questManager = FindObjectOfType<QuestManager>();
        npcManager = FindObjectOfType<NpcManager>();
        talkManager = FindObjectOfType<TalkManager>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!InteractableTag.Contains(other.tag)) return;
        detected = other;
        detected.GetComponent<PrintInteractable>().GenerateIcon();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!InteractableTag.Contains(other.tag)) return;
        detected.GetComponent<PrintInteractable>().DestroyIcon();
        detected = null;
    }

    public void DetectAction()
    {
        if (detected == null) return;
        
        if (detected.tag == "Door")
        {
            detected.GetComponent<Door>().ManageDoor();
        }

        if (detected.tag == "Npc")
        {
            detected.GetComponent<NpcManager>().TalkAction();
            questManager.AddQuest();
            talkManager.GetTalk();
        }

        if (detected.tag == "Car")
        {
            if(playerManager.isDriving) detected.GetComponent<Car>().GetOff(transform.parent);
            else detected.GetComponent<Car>().GetOn(transform.parent);
        }

        if (detected.tag == "Item")
        {
            Debug.Log("SIU");
        }
    }

}