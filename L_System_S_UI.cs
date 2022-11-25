using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json.Bson;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using System;

public class L_System_S_UI : MonoBehaviour
{
    public L_System_Generator generator;

    [Header("UI Panels")]
    public GameObject startPanel;
    public GameObject presetPanel;
    public GameObject customPanel;

    [Header("UI Buttons")]
    public Button threeDButton;
    public GameObject threeD, twoD;
    public GameObject[] controller;

    [Header("Text")]
    public TextMeshProUGUI Stereoscopic;

    [Header("Camera Control")]
    public GameObject mainCamera;
    public GameObject multiCamera;

    bool pressed;

    private void Update()
    {
        #region Feature - Hotkey for Preset Rules
        if (hotkeyEnable)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                //Debug.Log("press 1");
                generator.preset_Rules = Preset_Rules.Rule1;
                generator.SwitchPresetRules();
                generator.Generate(generator.inputRules);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                //Debug.Log("press 1");
                generator.preset_Rules = Preset_Rules.Rule2;
                generator.SwitchPresetRules();
                generator.Generate(generator.inputRules);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                //Debug.Log("press 1");
                generator.preset_Rules = Preset_Rules.Rule3;
                generator.SwitchPresetRules();
                generator.Generate(generator.inputRules);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                //Debug.Log("press 1");
                generator.preset_Rules = Preset_Rules.Rule4;
                generator.SwitchPresetRules();
                generator.Generate(generator.inputRules);
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                //Debug.Log("press 1");
                generator.preset_Rules = Preset_Rules.Rule5;
                generator.SwitchPresetRules();
                generator.Generate(generator.inputRules);
            }
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                //Debug.Log("press 1");
                generator.preset_Rules = Preset_Rules.Rule6;
                generator.SwitchPresetRules();
                generator.Generate(generator.inputRules);
            }
            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                //Debug.Log("press 1");
                generator.preset_Rules = Preset_Rules.Rule7;
                generator.SwitchPresetRules();
                generator.Generate(generator.inputRules);
            }
            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                //Debug.Log("press 1");
                generator.preset_Rules = Preset_Rules.Rule8;
                generator.SwitchPresetRules();
                generator.Generate(generator.inputRules);
            }
            if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                //Debug.Log("press 1");
                generator.preset_Rules = Preset_Rules.Rule9;
                generator.SwitchPresetRules();
                generator.Generate(generator.inputRules);
            }
        }
        
        #endregion

        if(pressCustomButton)
        animationIterations(5);
    }

    //TODO: Camera UI
    public void _PresetTrees()
    {
        mainCamera.SetActive(false);
        multiCamera.SetActive(true);
        startPanel.SetActive(false);
        presetPanel.SetActive(true);

        /*/
        generator.preset_Rules = Preset_Rules.Rule1;
        generator.SwitchPresetRules();
        generator.Generate(generator.inputRules);
        generator.SplitScreen();
        generator.preset_Rules = Preset_Rules.Rule2;
        generator.SwitchPresetRules();
        generator.Generate(generator.inputRules);
        generator.SplitScreen();
        generator.preset_Rules = Preset_Rules.Rule3;
        generator.SwitchPresetRules();
        generator.Generate(generator.inputRules);
        generator.SplitScreen();
        generator.preset_Rules = Preset_Rules.Rule4;
        generator.SwitchPresetRules();
        generator.Generate(generator.inputRules);
        generator.SplitScreen();
        generator.preset_Rules = Preset_Rules.Rule5;
        generator.SwitchPresetRules();
        generator.Generate(generator.inputRules);
        generator.SplitScreen();
        generator.preset_Rules = Preset_Rules.Rule6;
        generator.SwitchPresetRules();
        generator.Generate(generator.inputRules);
        generator.SplitScreen();
        */
    }

    public void _PresetToStart()
    {
        mainCamera.SetActive(true);
        multiCamera.SetActive(false);
        startPanel.SetActive(true);
        presetPanel.SetActive(false);
    }

    public void _CustomToStart()
    {
        startPanel.SetActive(true);
        customPanel.SetActive(false);
    }

    bool pressCustomButton;
    public void _CustomButton()
    {
        pressCustomButton = true;
        startPanel.SetActive(false);
        customPanel.SetActive(true);
        for (int i = 0; i < 6; i++)
        {
            Destroy(generator.treesHolder.transform.GetChild(i).gameObject);
        }
        generator.preset_Rules = Preset_Rules.Rule1;
        generator.SwitchPresetRules();
        generator.SetParametersValues(7f, 1.5f, 25.7f);       
    }

    public void _ThreeDButton()
    {

        pressed = !pressed;

        if(pressed)
        {
            //Debug.Log("3d");
            threeD.SetActive(true);
            twoD.SetActive(false);
            Stereoscopic.text = "2D";

        }
        else 
        {
            //Debug.Log("2d");
            threeD.SetActive(false);
            twoD.SetActive(true);
            Stereoscopic.text = "3D";
        }
    }

    public void Btn31()
    {
        generator.preset_Rules = Preset_Rules.Rule7;
        generator.SwitchPresetRules();
        generator.SetIterations(1);
        generator.Generate(generator.inputRules);

        foreach (GameObject g in controller)
        {
            g.SetActive(false);
        }

        controller[6].SetActive(true);
    }

    public void Btn32()
    {
        generator.preset_Rules = Preset_Rules.Rule8;
        generator.SwitchPresetRules();
        generator.SetIterations(1);
        generator.Generate(generator.inputRules);

        foreach (GameObject g in controller)
        {
            g.SetActive(false);
        }

        controller[7].SetActive(true);
    }

    public void Btn33()
    {
        generator.preset_Rules = Preset_Rules.Rule9;
        generator.SwitchPresetRules();
        generator.SetIterations(1);
        generator.Generate(generator.inputRules);

        foreach (GameObject g in controller)
        {
            g.SetActive(false);
        }

        controller[8].SetActive(true);
    }

    public void Btn11()
    {
        //ebug.Log("button 1");
        generator.preset_Rules = Preset_Rules.Rule1;
        generator.SwitchPresetRules();
        generator.SetIterations(1);
        generator.Generate(generator.inputRules);

        foreach(GameObject g in controller)
        {
            g.SetActive(false);
        }

        controller[0].SetActive(true);

    }

    public void Btn12()
    {
        generator.preset_Rules = Preset_Rules.Rule2;
        generator.SwitchPresetRules();
        generator.SetIterations(1);
        generator.Generate(generator.inputRules);

        foreach (GameObject g in controller)
        {
            g.SetActive(false);
        }

        controller[1].SetActive(true);

    }

    public void Btn13()
    {
        generator.preset_Rules = Preset_Rules.Rule3;
        generator.SwitchPresetRules();
        generator.SetIterations(1);
        generator.Generate(generator.inputRules);

        foreach (GameObject g in controller)
        {
            g.SetActive(false);
        }

        controller[2].SetActive(true);

    }

    public void Btn21()
    {
        generator.preset_Rules = Preset_Rules.Rule4;
        generator.SwitchPresetRules();
        generator.SetIterations(1);
        generator.Generate(generator.inputRules);

        foreach (GameObject g in controller)
        {
            g.SetActive(false);
        }

        controller[3].SetActive(true);
    }

    public void Btn22()
    {
        generator.preset_Rules = Preset_Rules.Rule5;
        generator.SwitchPresetRules();
        generator.SetIterations(1);
        generator.Generate(generator.inputRules);

        foreach (GameObject g in controller)
        {
            g.SetActive(false);
        }

        controller[4].SetActive(true);
    }

    public void Btn23()
    {
        generator.preset_Rules = Preset_Rules.Rule6;
        generator.SwitchPresetRules();
        generator.SetIterations(1);
        generator.Generate(generator.inputRules);

        foreach (GameObject g in controller)
        {
            g.SetActive(false);
        }

        controller[5].SetActive(true);
    }

    int i;
    public void Increase()
    {
        switch (generator.preset_Rules)
        {
            case Preset_Rules.None:
                Debug.Log("No current rule to input");
                break;
            case Preset_Rules.Rule1:
                if(i < 5)
                {
                    i++;
                    //Debug.Log(i);
                    generator.SetIterations(i);
                    generator.Generate(generator.inputRules);
                }               
                break;
            case Preset_Rules.Rule2:
                if (i < 5)
                {
                    i++;
                    //Debug.Log(i);
                    generator.SetIterations(i);
                    generator.Generate(generator.inputRules);
                }
                break;
            case Preset_Rules.Rule3:
                if (i < 4)
                {
                    i++;
                    //Debug.Log(i);
                    generator.SetIterations(i);
                    generator.Generate(generator.inputRules);
                }
                break;
            case Preset_Rules.Rule4:
                if (i < 7)
                {
                    i++;
                    //Debug.Log(i);
                    generator.SetIterations(i);
                    generator.Generate(generator.inputRules);
                }
                break;
            case Preset_Rules.Rule5:
                if (i < 7)
                {
                    i++;
                    //Debug.Log(i);
                    generator.SetIterations(i);
                    generator.Generate(generator.inputRules);
                }
                break;
            case Preset_Rules.Rule6:
                if (i < 5)
                {
                    i++;
                    //Debug.Log(i);
                    generator.SetIterations(i);
                    generator.Generate(generator.inputRules);
                }
                break;
            case Preset_Rules.Rule7:
                if (i < 5)
                {
                    i++;
                    //Debug.Log(i);
                    generator.SetIterations(i);
                    generator.Generate(generator.inputRules);
                }
                break;
            case Preset_Rules.Rule8:
                if (i < 6)
                {
                    i++;
                    //Debug.Log(i);
                    generator.SetIterations(i);
                    generator.Generate(generator.inputRules);
                }
                break;
            case Preset_Rules.Rule9:
                if (i < 5)
                {
                    i++;
                    //Debug.Log(i);
                    generator.SetIterations(i);
                    generator.Generate(generator.inputRules);
                }
                break;


        }
    }

    public void Decrease()
    {
        switch (generator.preset_Rules)
        {
            case Preset_Rules.None:
                Debug.Log("No current rule to input");
                break;
            case Preset_Rules.Rule1:
                if (i>0 &&i <= 5)
                {
                    i--;
                    //Debug.Log(i);
                    generator.SetIterations(i);
                    generator.Generate(generator.inputRules);
                }
                break;
            case Preset_Rules.Rule2:
                if (i > 0 && i <= 5)
                {
                    i--;
                    //Debug.Log(i);
                    generator.SetIterations(i);
                    generator.Generate(generator.inputRules);
                }
                break;
            case Preset_Rules.Rule3:
                if (i > 0 && i <= 4)
                {
                    i--;
                    //Debug.Log(i);
                    generator.SetIterations(i);
                    generator.Generate(generator.inputRules);
                }
                break;
            case Preset_Rules.Rule4:
                if (i > 0 && i <= 7)
                {
                    i--;
                    //Debug.Log(i);
                    generator.SetIterations(i);
                    generator.Generate(generator.inputRules);
                }
                break;
            case Preset_Rules.Rule5:
                if (i > 0 && i <= 7)
                {
                    i--;
                    //Debug.Log(i);
                    generator.SetIterations(i);
                    generator.Generate(generator.inputRules);
                }
                break;
            case Preset_Rules.Rule6:
                if (i > 0 && i <= 5)
                {
                    i--;
                    //Debug.Log(i);
                    generator.SetIterations(i);
                    generator.Generate(generator.inputRules);
                }
                break;
            case Preset_Rules.Rule7:
                if (i > 0 && i <= 5)
                {
                    i--;
                    //Debug.Log(i);
                    generator.SetIterations(i);
                    generator.Generate(generator.inputRules);
                }
                break;
            case Preset_Rules.Rule8:
                if (i > 0 && i <= 6)
                {
                    i--;
                    //Debug.Log(i);
                    generator.SetIterations(i);
                    generator.Generate(generator.inputRules);
                }
                break;
            case Preset_Rules.Rule9:
                if (i > 0 && i <= 5)
                {
                    i--;
                    //Debug.Log(i);
                    generator.SetIterations(i);
                    generator.Generate(generator.inputRules);
                }
                break;


        }
    }

    private float timeToSpawn;
    public float spawnRate = 2f;
    public int DelayAmount = 1;
    int value = 1;

    protected float Timer;

    private void animationIterations(int i)
    {
        if (Time.time >= timeToSpawn)
        {
            timeToSpawn = Time.time + 1f / spawnRate;

            Timer += Time.deltaTime;
            if (value < i)
            {
  
                value++;
                Debug.Log("animation" + value);
                generator.SetIterations(value);
                generator.Generate(generator.inputRules);
            }

        }
    }

    [Header("Rule Text")]
    public TMP_InputField rule_L1_Text;
    public TMP_InputField rule_R1_Text;
    public TMP_InputField rule_L2_Text;
    public TMP_InputField rule_R2_Text;

    [Header("Parameter Text")]
    public TMP_InputField iteration_Text;
    public TMP_InputField length_Text;
    public TMP_InputField width_Text;
    public TMP_InputField angle_Text;

    [Header("Leaf Setting")]
    public Toggle isLeaf;
    public TMP_Dropdown _leafType;
    public TMP_InputField _edgesType;
    public TMP_InputField openLeaf_Text;
    public TMP_InputField radius_Text;
    public TMP_InputField _curveEdge;
    public TMP_InputField _sideEdge;

    [Header("Colour")]
    public Toggle randomColor;
    public GameObject colorButton;

    [Header("Generate")]
    public Toggle enableHotKeys;
    public Toggle enableGenerateButton;

    //public void _Rule()
    //{
    //   generator.rule_L1 = //char.Parse(rule_L1_Text.text);
    //    Convert.ToChar(rule_L1_Text.text);
    //    generator.rule_R1 = rule_L2_Text.text;
    //    generator.rule_L2 = char.Parse(rule_R1_Text.text);
    //    generator.rule_R2 = rule_R2_Text.text;
    //}

    public void _Rule_L1()
    {
        generator.rule_L1 = //char.Parse(rule_L1_Text.text);
         Convert.ToChar(rule_L1_Text.text);
    }

    public void _Rule_L2()
    {
        generator.rule_L2 = char.Parse(rule_R1_Text.text);
    }

    public void _Rule_R1()
    {
        generator.rule_R1 = rule_L2_Text.text;
    }

    public void _Rule_R2()
    {
        generator.rule_R2 = rule_R2_Text.text;
    }

    public void _Parameters_iteration()
    {
        generator.iterations = int.Parse(iteration_Text.text);
    }

    public void _Parameters_length()
    {
        generator.length = float.Parse(iteration_Text.text);
    }

    public void _Parameters_width()
    {
        generator.width = float.Parse(iteration_Text.text);
    }

    public void _Parameters_angle()
    {
        generator.angle = float.Parse(iteration_Text.text);
    }

    public void _GenerateTree()
    {
        generator.AddRule(generator.rule_L1, generator.rule_R1, generator.rule_L2, generator.rule_R2);
        generator.SetIterations(generator.iterations);
        generator.SetParametersValues(generator.length, generator.width, generator.angle);
        generator.Generate(generator.inputRules);
    }

    public void _UseLeaf()
    {
        generator.isLeaf = isLeaf.isOn;
    }

    public void _ChangeColorToggle()
    {
        colorButton.SetActive(randomColor.isOn);
    }

    public void _ChangeColor()
    {
        generator.ChangeColor(true);
    }

    public GameObject curveMode;
    public GameObject polyMode;

    public void _LeafType()
    {
       if (_leafType.value == 0)
        {
           generator.leafType = LeafType.Curve;
            polyMode.gameObject.SetActive(false);
            curveMode.gameObject.SetActive(true);
        }
        if (_leafType.value == 1)
        {
            generator.leafType = LeafType.Polygons;
            polyMode.gameObject.SetActive(true);
            curveMode.gameObject.SetActive(false);
        }
    }

    public void _Shapes()
    {
        generator.leafShape = int.Parse(_edgesType.text);
    }

    public void OpenDegree()
    {
        generator.leafOpenDegree = int.Parse(openLeaf_Text.text);
    }


    public void CurveEdge()
    {
        generator.leafCurveEdge = int.Parse(_curveEdge.text);
    }


    public void SideEdge()
    {
        generator.leafCurveSide = int.Parse(_sideEdge.text);
    }

    public void _Radius()
    {
        generator.leafRadius = int.Parse(radius_Text.text);
    }

    bool hotkeyEnable;
    public Button generateButton;

    public void _Hotkeys()
    {
        hotkeyEnable = enableHotKeys.isOn;
        if (enableHotKeys.isOn)
        {
            enableGenerateButton.interactable = false;
        }
        else
        {
            enableGenerateButton.interactable = true;
        }

    }

    public void _EnableGenerateButton()
    {
        generateButton.interactable = enableGenerateButton.isOn;
        if (enableGenerateButton.isOn)
        {
            enableHotKeys.interactable = false;
        }
        else
        {
            enableHotKeys.interactable = true;
        }
    }

    bool isArrow;
    public GameObject panelMenu;

    public void Arrow()
    {

        Animator anim = panelMenu.GetComponent<Animator>();
        if (anim != null)
        {
            //Debug.Log(isArrow);
            isArrow = anim.GetBool("appearUI");
            anim.SetBool("appearUI", !isArrow);
        }
    }

    public Slider randomness;

    public void Randomness()
    {
       generator.ChangeRandomness(randomness.value) ;
    }
}
