using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;


public class LightOrbiter : MonoBehaviour
{

    public bool allowX = true;    
    public bool allowY = true;
    public bool allowZ = true;

    public Slider xScroll;
    public Slider yScroll;
    public Slider zScroll;
    public LightPreset[] presets;
    public Button[] presetButtons;
    public bool AllowColorPicking;
    public ColorPicker colorPicker;

    private Color lightColor;
    private Vector3 rotVector;
    private bool valueChanged;
    private float nextXRot;
    private float nextYRot;
    private float nextZRot;
    private Light thisLight;

    


    void Awake()
    {

     
        xScroll.maxValue = 360;
        xScroll.minValue = 0;

        yScroll.maxValue = 360;
        yScroll.minValue = 0;

        zScroll.maxValue = 360;
        zScroll.minValue = 0;

        xScroll.value = transform.rotation.eulerAngles.x;
        yScroll.value = transform.rotation.eulerAngles.y;
        zScroll.value = transform.rotation.eulerAngles.z;

        xScroll.onValueChanged.AddListener(delegate {ValueChangeCheckX();});
        yScroll.onValueChanged.AddListener(delegate {ValueChangeCheckY();});
        zScroll.onValueChanged.AddListener(delegate {ValueChangeCheckZ();});

        nextXRot=  transform.eulerAngles.x;
        nextYRot = transform.eulerAngles.y;
        nextZRot = transform.eulerAngles.z;
       
        xScroll.enabled=allowX;
        yScroll.enabled = allowY;
        zScroll.enabled = allowY;
        int delegationIndex = 0;

        foreach(Button b in presetButtons)
        {
            b.onClick.AddListener(delegate { applyPreset(b.name); });
            delegationIndex++;
        }

        if (GetComponent<Light>() != null)
            thisLight= GetComponent<Light>();


        if(AllowColorPicking && colorPicker!=null)
        { 
            colorPicker.onValueChanged.AddListener(color =>
            {
               lightColor = color;
            });            
        }

        lightColor = thisLight.color;
    }

    private void applyPreset(string name)
    {
        int i = 0;
        for (i = 0; i < presetButtons.Length; i++)
        {

            if (i < presets.Length && presetButtons[i].name==name)
            {
                LightPreset preset = presets[i];
                xScroll.value = preset.xAngle;
                yScroll.value = preset.yAngle;
                zScroll.value = preset.zAngle;
                lightColor = preset.lightColor; 
                
            }
        }
    }

    private void ValueChangeCheckX()
    {
        nextXRot = xScroll.value;       
        valueChanged = true;
    }

    private void ValueChangeCheckY()
    {
        nextYRot = yScroll.value;        
        valueChanged = true;
    }

    private void ValueChangeCheckZ()
    {
        nextZRot = zScroll.value;
        valueChanged = true;
    }

    void Update () {

        thisLight.color = lightColor;
        if (valueChanged){
            rotVector = new Vector3(nextXRot, nextYRot, nextZRot);
            transform.eulerAngles = rotVector;
        }       
	}

    void LateUpdate()
    {
       valueChanged = false;
    }

}


[CustomEditor(typeof(LightOrbiter))]
public class LightOrbiterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        LightOrbiter myTarget = (LightOrbiter)target;
        EditorGUILayout.LabelField("Allowed rotations");

        
        myTarget.allowX = EditorGUILayout.Toggle("Allow X rotation", myTarget.allowX);
        myTarget.allowY = EditorGUILayout.Toggle("Allow Y rotation", myTarget.allowY);
        myTarget.allowZ = EditorGUILayout.Toggle("Allow Z rotation", myTarget.allowZ);
        

        //("Label:", target.myClass, typeof(MyClass), true);
        if (myTarget.allowX)
            myTarget.xScroll = (Slider)EditorGUILayout.ObjectField("X Rot Slider", myTarget.xScroll, typeof(Slider),true );
        if (myTarget.allowY)
            myTarget.yScroll = (Slider)EditorGUILayout.ObjectField("Y Rot Slider", myTarget.yScroll, typeof(Slider), true);
        if (myTarget.allowZ)
            myTarget.zScroll = (Slider)EditorGUILayout.ObjectField("Z Rot Slider", myTarget.zScroll, typeof(Slider), true);

        myTarget.AllowColorPicking = EditorGUILayout.Toggle("Allow Color Pick", myTarget.AllowColorPicking);
        if (myTarget.AllowColorPicking)
            myTarget.colorPicker = (ColorPicker)EditorGUILayout.ObjectField("Color Picker", myTarget.colorPicker, typeof(ColorPicker), true);
        EditorGUILayout.Separator();

        EditorGUILayout.LabelField("Presets and presets buttons");
        SerializedObject so = new SerializedObject(target);

        SerializedProperty presetsProperty = so.FindProperty("presets");
        EditorGUILayout.PropertyField(presetsProperty, true); 
        so.ApplyModifiedProperties(); 
        
        SerializedProperty presetsButtonsProperty = so.FindProperty("presetButtons");
        EditorGUILayout.PropertyField(presetsButtonsProperty, true);
        so.ApplyModifiedProperties(); 


    }
}

