using System;

namespace _Source.Core
{
    public class ObservableItemAmount
    {
        private int _value;
        private int _maxAmount;
        public Action<int> OnValueChanged;

        public ObservableItemAmount()
        {
            _value = 0;
            _maxAmount = 64;
        }

        public ObservableItemAmount(int maxAmount)
        {
            _value = 0;
            // maxAmount can not be more than 64
            _maxAmount = maxAmount < 64 ? maxAmount : 64;
        }
        
        public ObservableItemAmount(int value, int maxAmount)
        {
            // maxAmount can not be more than 64
            _maxAmount = maxAmount < 64 ? maxAmount : 64;
            // value can not me bore than maxAmount
            _value = value <= _maxAmount ? value : _maxAmount;
        }
        
        public int Value
        {
            get => _value;
            set
            {
                _value = value <= _maxAmount ? value : _maxAmount;
                OnValueChanged?.Invoke(_value);
            }
        }
        
        public static ObservableItemAmount operator +(ObservableItemAmount amount, int value)
        {
            amount.Value = amount.Value + value;
            return amount;
        }

        public override string ToString()
        {
            return _value.ToString();
        }
    }
}