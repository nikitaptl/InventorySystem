using _Source.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Source.Game.InventoryManagement
{
    public class InventorySlot : MonoBehaviour
    {
        public ItemObject _item;

        public ItemObject Item
        {
            get => _item;
            set
            {
                if (value != null)
                {
                    _item = value;
                    _amount = new ObservableItemAmount(0, value.maximumAmount);
                    _amount.OnValueChanged += UpdateText;
                    return;
                }

                _item = value;
            }
        }

        private ObservableItemAmount _amount;

        public ObservableItemAmount Amount
        {
            get => _amount;
            set => _amount = value;
        }

        public bool isEmpty = true;
        private GameObject _itemImage;
        public TextMeshProUGUI textAmount;

        public GameObject ItemImage
        {
            get => _itemImage;
            set => _itemImage = value;
        }

        private void Start()
        {
            _itemImage = transform.GetChild(0).GetChild(0).gameObject;
            textAmount = transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
        }

        public void SetImage(Sprite icon)
        {
            Image imageComponent = _itemImage.GetComponent<Image>();
            // Make image entirely transparent
            imageComponent.color = new Color(1, 1, 1, 1);
            imageComponent.sprite = icon;
        }

        public void UpdateText(int value)
        {
            if (value == 0 || _item.maximumAmount == 1)
            {
                textAmount.text = "";
            }
            else
            {
                textAmount.text = $"{_amount.Value}/{_item.maximumAmount}";
            }
        }
    }
}