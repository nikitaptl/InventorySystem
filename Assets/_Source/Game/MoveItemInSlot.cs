using _Source.Core;
using _Source.Game.InventoryManagement;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Source.Game
{
    public class MoveItemInSlot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        private InventorySlot _currSlot;
        private Transform _player;

        private void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            _currSlot = transform.GetComponentInParent<InventorySlot>();
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_currSlot.isEmpty)
            {
                return;
            }

            GetComponent<RectTransform>().position += new Vector3(eventData.delta.x, eventData.delta.y);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_currSlot.isEmpty)
            {
                return;
            }

            // This action makes the picture more transparent
            GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0.6f);
            GetComponentInChildren<Image>().raycastTarget = false;
            transform.SetParent(transform.parent.parent);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_currSlot.isEmpty)
            {
                return;
            }

            // This action makes the picture not transparent
            GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1f);
            GetComponentInChildren<Image>().raycastTarget = true;

            transform.SetParent(_currSlot.transform);
            transform.position = _currSlot.transform.position;

            var currRaycast = eventData.pointerCurrentRaycast.gameObject;
            if (currRaycast.name == "UIPanel")
            {
                // This action throws an object in front of the character
                GameObject itemObject = Instantiate(_currSlot.Item.itemPrefab,
                    _player.position + Vector3.up + _player.forward, Quaternion.identity);
                itemObject.GetComponent<WorldItem>().amount = _currSlot.Amount.Value;
                ClearSlot();
            }
            else if (currRaycast.transform.parent.parent.GetComponent<InventorySlot>() != null)
            {
                SwapSlots(currRaycast.transform.parent.parent.GetComponent<InventorySlot>());
            }
        }

        void SwapSlots(InventorySlot newSlot)
        {
            ItemObject item = newSlot.Item;
            ObservableItemAmount amount = newSlot.Amount;
            bool isEmpty = newSlot.isEmpty;
            GameObject itemImage = newSlot.ItemImage;
            TextMeshProUGUI itemAmountText = newSlot.textAmount;

            newSlot.Item = _currSlot.Item;
            newSlot.Amount = _currSlot.Amount;
            newSlot.SetImage(_currSlot.ItemImage.GetComponent<Image>().sprite);
            newSlot.textAmount.text = _currSlot.textAmount.text;
            newSlot.isEmpty = _currSlot.isEmpty;

            _currSlot.Item = item;
            _currSlot.Amount = amount;
            _currSlot.isEmpty = isEmpty;
            if (isEmpty == false)
            {
                _currSlot.SetImage(item.itemIcon);
                _currSlot.UpdateText(amount.Value);
            }
            else
            {
                // Make image not transparent
                _currSlot.ItemImage.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                _currSlot.ItemImage.GetComponent<Image>().sprite = null;
                _currSlot.textAmount.text = "";
            }
        }

        public void ClearSlot()
        {
            _currSlot.Item = null;
            _currSlot.Amount = null;
            _currSlot.isEmpty = true;

            // itemImage and testAmount should not be null, so we write zero values in them
            _currSlot.ItemImage.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            _currSlot.ItemImage.GetComponent<Image>().sprite = null;
            _currSlot.textAmount.text = "";
        }
    }
}