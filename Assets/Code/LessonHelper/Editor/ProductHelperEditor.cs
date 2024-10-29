using UnityEditor;
using UnityEngine;

namespace Code
{
    [CustomEditor(typeof(ProductHelper))]
    public sealed class ProductHelperEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            var lessonHelper = (ProductHelper) target;
            
            if (GUILayout.Button("ProductPopupShow"))
            {
                lessonHelper.ProductPopupShow();
            }
            
            if (GUILayout.Button("ShopPopupShow"))
            {
                lessonHelper.ShopPopupShow();
            }
        }
    }
}
