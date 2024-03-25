using _Source.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Source.Game
{
    public class InventorySlot : MonoBehaviour
    {
        public ItemObject _item;

        public ItemObject Item
        {
            get => _item;
            set
            {
                _item = value;
                _amount = new ObservableItemAmount(0, _item.maximumAmount);
                _amount.OnValueChanged += UpdateText;
            }
        }

        private ObservableItemAmount _amount;

        public ObservableItemAmount Amount
        {
            get => _amount;
            set => _amount = value;
        }
        
        public bool isEmpty = true;
        private GameObject _objectImage;
        private TextMeshProUGUI _textAmount;

        private void Start()
        {
            _objectImage = transform.GetChild(0).gameObject;
            _textAmount = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        }
        
        public void SetIcon(Sprite icon)
        {
            Image imageComponent = _objectImage.GetComponent<Image>();
            imageComponent.color = new Color(1, 1, 1, 1);
            imageComponent.sprite = icon;
        }

        void UpdateText(int value)
        {
            if (value == 0)
            {
                _textAmount.text = "";
            }
            else
            {
                _textAmount.text = $"{_amount.Value}/{_item.maximumAmount}";
            }
        }
    }
}