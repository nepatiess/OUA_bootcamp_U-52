using UnityEngine;
using UnityEditor;

namespace UnityEditor.PostProcessing
{
<<<<<<< Updated upstream
    [CustomPropertyDrawer(typeof(UnityEngine.MinAttribute))]
=======
    [CustomPropertyDrawer(typeof(UnityEngine.PostProcessing.MinAttribute))]
>>>>>>> Stashed changes
    sealed class MinDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
<<<<<<< Updated upstream
            UnityEngine.MinAttribute attribute = (UnityEngine.MinAttribute)base.attribute;
=======
            
            UnityEngine.PostProcessing.MinAttribute attribute = (UnityEngine.PostProcessing.MinAttribute)base.attribute;
>>>>>>> Stashed changes

            if (property.propertyType == SerializedPropertyType.Integer)
            {
                int v = EditorGUI.IntField(position, label, property.intValue);
                property.intValue = (int)Mathf.Max(v, attribute.min);
            }
            else if (property.propertyType == SerializedPropertyType.Float)
            {
                float v = EditorGUI.FloatField(position, label, property.floatValue);
                property.floatValue = Mathf.Max(v, attribute.min);
            }
            else
            {
                EditorGUI.LabelField(position, label.text, "Use Min with float or int.");
            }
        }
    }
}