using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Light Preset", menuName = "Light Orbiter/Preset", order = 1)]
public class LightPreset : ScriptableObject {


    public float xAngle;
    public float yAngle;
    public float zAngle;

    [HideInInspector]
    public Light sourceLight;

    public Color32 lightColor;
}

[CustomEditor(typeof(LightPreset))]
public class LightPresetEditor : Editor
{
    public override void OnInspectorGUI()
    {
        LightPreset myTarget = (LightPreset)target;
        EditorGUILayout.LabelField("Preset angle");

        myTarget.xAngle = EditorGUILayout.FloatField("X Angle", myTarget.xAngle);
        myTarget.yAngle = EditorGUILayout.FloatField("Y Angle", myTarget.yAngle);
        myTarget.zAngle = EditorGUILayout.FloatField("Z Angle", myTarget.zAngle);

        myTarget.lightColor = EditorGUILayout.ColorField("Light Color", myTarget.lightColor);

      
        myTarget.sourceLight = (Light)EditorGUILayout.ObjectField("X Rot Slider", myTarget.sourceLight, typeof(Light), true);

        if (GUILayout.Button("Get From Light"))
        {
            myTarget.xAngle = myTarget.sourceLight.gameObject.transform.eulerAngles.x;
            myTarget.yAngle = myTarget.sourceLight.gameObject.transform.eulerAngles.y;
            myTarget.zAngle = myTarget.sourceLight.gameObject.transform.eulerAngles.z;

            myTarget.lightColor = myTarget.sourceLight.color;
        }

    }
}
