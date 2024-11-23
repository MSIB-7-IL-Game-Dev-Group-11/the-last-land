using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace TheLastLand._Project.Scripts.Utils.Editor
{
    [CustomPropertyDrawer(typeof(ReadOnlyInPlayModeAttribute))]
    public class ReadOnlyInPlayModeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = !Application.isPlaying;

            // Check if the property has a Range attribute
            var rangeAttribute = (RangeAttribute)fieldInfo.GetCustomAttribute(typeof(RangeAttribute), false);
            if (rangeAttribute != null)
            {
                // Draw the slider for the range attribute
                EditorGUI.Slider(position, property, rangeAttribute.min, rangeAttribute.max, label);
            }
            else
            {
                // Draw the property field as usual
                EditorGUI.PropertyField(position, property, label);
            }

            GUI.enabled = true;
        }
    }
}