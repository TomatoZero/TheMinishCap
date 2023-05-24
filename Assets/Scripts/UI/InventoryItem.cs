using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.UI
{
    public class InventoryItem : MonoBehaviour
    {
        [SerializeField] private Image _ico;
        
        private InventoryItemData _data;

        public InventoryItemData Data
        {
            get => _data;
            set => _data = value;
        }

        private void Awake()
        {
            if (_data != null) _ico.sprite = _data.Icon;
            else
            {
                _ico.sprite = null;
                _ico.color = Color.clear;
            }
        }
    }
}