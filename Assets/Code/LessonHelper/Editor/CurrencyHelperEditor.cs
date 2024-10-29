using UnityEditor;
using UnityEngine;

namespace Code
{
    [CustomEditor(typeof(CurrencyHelper))]
    public sealed class CurrencyHelperEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            var lessonHelper = (CurrencyHelper) target;
            
            if (GUILayout.Button("Add Money"))
            {
                lessonHelper.AddMoney();
            }
            
            if (GUILayout.Button("Spend Money"))
            {
                lessonHelper.SpendMoney();
            }
            
            if (GUILayout.Button("Add Gem"))
            {
                lessonHelper.AddGem();
            }
            
            if (GUILayout.Button("Gem Spend"))
            {
                lessonHelper.SpendGem();
            }
        }
    }
}
