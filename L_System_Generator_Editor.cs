using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;

[CustomEditor(typeof(L_System_Generator))]
public class L_System_Generator_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        L_System_Generator tree = (L_System_Generator)target;

        // If inspector is changed
        if (DrawDefaultInspector())
        {
            //tree.Generate(tree.treeTypes[0]);
        }

        #region Testing Buttons
        /*
        if (GUILayout.Button("Generate0"))
        {
            //tree.treeTypes[0] = new Dictionary<char, string>
            //{
            //    {'X', "F" },
            //    {'F', "F[+F]F[-F]F" }
            //};
            tree.treeTypes[0] = new Dictionary<char, string>();
            tree.treeTypes[0].Add('X', "F");
            tree.treeTypes[0].Add('F', "F[+F]F[-F]F");

            tree.treeTypesDetails[0] = new Dictionary<string, float>
        {
            {"iterations", 5f },
            {"length", 7f },
            {"width", 1.5f },
            {"angle", 25.7f }
        };


            tree.Generate(tree.treeTypes[0]);
        }

        if (GUILayout.Button("Generate2"))
        {

            tree.treeTypes[1] = new Dictionary<char, string>
            {
               {'X', "F" },
               {'F', "F[+F]F[-F][F]" }
            };
            tree.treeTypesDetails[1] = new Dictionary<string, float>
        {
            {"iterations", 5f },
            {"length", 18f },
            {"width", 1.5f },
            {"angle", 20f }
        };

            tree.Generate(tree.treeTypes[1]);
        }

        if (GUILayout.Button("Generate5"))
        {
            tree.treeTypes[5] = new Dictionary<char, string>
            {
                {'X', "F-[[X]+X]+F[+FX]-X" },
                {'F', "FF" }
            };
            tree.treeTypesDetails[5] = new Dictionary<string, float>
        {
            {"iterations", 5f },
            {"length", 7.15f },
            {"width", 1.5f },
            {"angle", 22.5f }
        };

            tree.Generate(tree.treeTypes[5]);
        }
        //*/
        #endregion

        if (GUILayout.Button("Generate Preset Rules"))
        {
            tree.SwitchPresetRules();
            tree.Generate(tree.inputRules);
        }

        if (GUILayout.Button("Custom Rules"))
        {
            tree.AddRule(tree.rule_L1, tree.rule_R1,tree.rule_L2,tree.rule_R2);
            tree.Generate(tree.inputRules);
        }

        if (GUILayout.Button("Change Color"))
        {
            tree.ChangeColor(true);
        }

        if (GUILayout.Button("6 preset trees"))
        {
            tree.preset_Rules = Preset_Rules.Rule1;
            tree.SwitchPresetRules();
            tree.Generate(tree.inputRules);
            tree.SplitScreen();
            tree.preset_Rules = Preset_Rules.Rule2;
            tree.SwitchPresetRules();
            tree.Generate(tree.inputRules);
            tree.SplitScreen();
            tree.preset_Rules = Preset_Rules.Rule3;
            tree.SwitchPresetRules();
            tree.Generate(tree.inputRules);
            tree.SplitScreen();
            tree.preset_Rules = Preset_Rules.Rule4;
            tree.SwitchPresetRules();
            tree.Generate(tree.inputRules);
            tree.SplitScreen();
            tree.preset_Rules = Preset_Rules.Rule5;
            tree.SwitchPresetRules();
            tree.Generate(tree.inputRules);
            tree.SplitScreen();
            tree.preset_Rules = Preset_Rules.Rule6;
            tree.SwitchPresetRules();
            tree.Generate(tree.inputRules);
            tree.SplitScreen();
        }
    }
}
