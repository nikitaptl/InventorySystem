using System.Collections.Generic;
using _Source.Core;
using Cinemachine;
using UnityEngine;

namespace _Source.Game.InventoryManagement
{
    public class InventoryController : MonoBehaviour
    {
        public GameObject uIPanel;
        public Transform inventoryPanel;
        private Camera _mainCamera;
        public List<InventorySlot> slots = new List<InventorySlot>();

        // This scale shows at what distance the character can take objects
        public float reachDistance = 3;
        public bool isOpened = false;

        public CinemachineVirtualCamera virtualCamera;

        // Start is called before the first frame update
        void Start()
        {
            _mainCamera = Camera.main;
            for (int i = 0; i < inventoryPanel.childCount; i++)
            {
                InventorySlot slot = inventoryPanel.GetChild(i).GetComponent<InventorySlot>();
                slots.Add(slot);
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
                    // This action fixes the cursor
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;

                    if (virtualCamera != null)
                    {
                        virtualCamera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_InputAxisName =
                            "Mouse X";
                        virtualCamera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_InputAxisName =
                            "Mouse Y";
                    }
                }
                else
                {
                    uIPanel.SetActive(true);
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;

                    // This action captures the camera position when the inventory is open
                    if (virtualCamera != null)
                    {
                        virtualCamera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_InputAxisName = "";
                        virtualCamera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_InputAxisName = "";
                    }
                }
                isOpened = !isOpened;
            }
            
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            // Сhecking whether the ray points to one of the objects in the world
            if (Physics.Raycast(ray, out hit, reachDistance))
            {
                var catchedObject = hit.collider.gameObject.GetComponent<WorldItem>();
                if (catchedObject != null)
                {
                    AddItem(catchedObject.item, catchedObject.amount);
                    Destroy(hit.collider.gameObject);
                }
            }
        }

        private void AddItem(ItemObject item, int amount)
        {
            // Checks if there is already a slot with an item in the inventory and if it is not filled
            foreach (InventorySlot slot in slots)
            {
                if (slot.Item == item)
                {
                    if (slot.Amount.Value + amount > item.maximumAmount)
                    {
                        continue;
                    }
                    slot.Amount += amount;
                    return;
                }
            }

            // If there are no slots with such an item, it takes the first free one
            foreach (InventorySlot slot in slots)
            {
                if (slot.isEmpty)
                {
                    slot.Item = item;
                    slot.Amount += amount;
                    slot.isEmpty = false;

                    slot.SetImage(item.itemIcon);
                    break;
                }
            }
        }
    }
}