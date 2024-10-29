using UnityEngine;

namespace Code
{
    [CreateAssetMenu(fileName = "ProductCatalog", menuName = "Data/New ProductCatalog")]
    public sealed class ProductCatalog : ScriptableObject
    {
        public ProductInfo[] Products;
    }
}
