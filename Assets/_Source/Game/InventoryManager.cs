using System.Collections;
using System.Collections.Generic;
using _Source;
using _Source.Core;
using _Source.Game;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject uIPanel;
    public Transform inventoryPanel;
    public List<InventorySlot> slots = new List<InventorySlot>();
    public bool isOpened = false;
    private Camera mainCamera;
    public float reachDistance = 3;
    
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        for (int i = 0; i < inventoryPanel.childCount; i++)
        {
            if (inventoryPanel.GetChild(i).GetComponent<InventorySlot>() != null)
            {
                slots.Add(inventoryPanel.GetChild(i).GetComponent<InventorySlot>());
            }
        }
        uIPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (isOpened)
            {
                uIPanel.SetActive(false);
            }
            else
            {
                uIPanel.SetActive(true);
            }
            isOpened = !isOpened;
        }

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, reachDistance))
        {
            Debug.DrawRay(ray.origin, ray.direction * reachDistance, Color.green);
            var catchedObject = hit.collider.gameObject.GetComponent<WorldItem>();
                
            if (catchedObject != null)
            {
                AddItem(catchedObject.item, catchedObject.amount);
                Destroy(hit.collider.gameObject);
            }
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * reachDistance, Color.red);
        }
    }

    private void AddItem(ItemObject item, int amount)
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot.Item == item)
            {
                slot.Amount += amount;
                return;
            }
        }
        foreach (InventorySlot slot in slots)
        {
            if (slot.isEmpty)
            {
                slot.Item = item;
                slot.Amount += amount;
                slot.isEmpty = false;

                slot.SetIcon(item.itemIcon);
                break;
            }
        }
        
    }
}
