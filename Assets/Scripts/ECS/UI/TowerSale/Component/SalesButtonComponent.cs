using System;
using UnityEngine.UI;

namespace ECS.UI.TowerSale.Component
{
    [Serializable]
    public struct SalesButtonComponent
    {
        public Button SaleButton;
        public int SalePrice;
    }
}
