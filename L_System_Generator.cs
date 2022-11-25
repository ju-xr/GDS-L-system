using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System.Net.Http.Headers;
using Newtonsoft.Json.Serialization;
using Unity.VisualScripting;
using System.Runtime.CompilerServices;
using UnityEngine.UIElements;

[Serializable]
public class SavedData
{
    public Vector3 position;
    public Quaternion rotation;
    public Color savedColor;
}

[Serializable]
public struct LSystem_Rules
{
    public char rule_L;
    public string rule_R;
}


public enum Mode
{
    Default = 0, //single mode
    Split = 1, //split screen with all 8 required rules
    Stereoscopic = 2, // 3d mode
    WithLeaf = 3,
    Animation = 4
}

public struct LSystem_Parameters
{
    public int iteration;
    public float length;
    public float width;
    public float angle;
}

public enum Preset_Rules // 9 require rules
{
    None = 0,
    Rule1 = 1,
    Rule2 = 2,
    Rule3 = 3,
    Rule4 = 4,
    Rule5 = 5,
    Rule6 = 6,
    Rule7 = 7,
    Rule8 = 8,
    Rule9 = 9
}

public enum LeafType // 9 require rules
{
    Polygons = 0,
    Curve = 1,
}

public class L_System_Generator: MonoBehaviour
{
    [Header("Tree Root")]
    public GameObject tree = null;
    public GameObject treesHolder = null;

    [Header("Camera Movement")]
    [SerializeField] public Transform cameraRotator;
    [SerializeField] public GameObject cameraItself;

    //[SerializeField] private bool useFoliage;
    //[SerializeField] float leafSize;
    //[SerializeField] Material leafMaterial;

    // First Try - Delete later
    [Header("Temp Tree Types")]
    private int currentTreeType = 0;
    public List<LSystem_Rules>[] LS_rules;
    public Dictionary<char, string>[] treeTypes = new Dictionary<char, string>[9];
    public Dictionary<string, float>[] treeTypesDetails = new Dictionary<string, float>[9];

    //UI
    [Header("Temp UI")]
    //public Slider iterationsSlider;
    public TextMeshProUGUI iterationsNumberText;
    //public Slider lengthSlider;
    //public Slider widthSlider;
    //public Slider angleSlider;
    //public Slider randomnessSlider;
    private float currentRound = 0f;
    private float currentZoom = 300f;
    private float r1=0;
    private float r2=0;

    //private Stack<TransformInfo> transformStack;
    //public static Dictionary<char, string> rules;
    [Header("Custom Modes")]
    public Preset_Rules preset_Rules;
    public Mode mode = Mode.Default;


    //input rules
    [Header("Input L-system Rules")]
    public char rule_L1;
    public string rule_R1;
    public char rule_L2;
    public string rule_R2;

    //L-sysytem Parameters
    [Header("L-sysytem Parameters")]
    private const string axiom = "X";
    private string currentString = string.Empty;
    public int iterations = 5;
    public float length = 5f;
    public float width = 1.5f;
    public float angle = 25.7f;

    //Bonus Stochastic L-system

    [Header("L-sysytem Stochastic")]
    [Range(0f, 100f)]
    [SerializeField] private float randomness = 0f;
    [SerializeField] private float variance;
    //private float[] randomRotations;

    [Header("L-sysytem 3D Trees")]
    [SerializeField] private GameObject treeRoot;
    [SerializeField] private GameObject branch;
    [SerializeField] private GameObject leaf;
    [SerializeField] private GameObject colorTree;

    [Header("L-sysytem Color Trees")]
    [SerializeField] private Gradient sourceGradient;

    [Header("L-sysytem Tree with Leaves")]
    //public float minLeafLength;
    //public float maxLeafLength;
    //public float LeafWidth = 5f;
    //public Texture2D leafTexture;
    public LeafType leafType = LeafType.Polygons;
    public bool isLeaf = false;
    [Range(4,25)]
    public int leafShape = 20; //circle
    public float leafRadius = 3f;
    public float leafOpenDegree = 30f;
    public float leafCurveSide = 2f;
    public float leafCurveEdge = 2f;

    [Header("Split Screen with multi trees")]
    public bool multiTree;


    #region treeTypes/Rules initial testing - Old Method
    /*
    private void Awake()
    {
        
        
        treeTypes[0] = new Dictionary<char, string>
            {
                {'X', "F" },
                {'F', "F[+F]F[-F]F" }
            };

        treeTypesDetails[0] = new Dictionary<string, float>
        {
            {"iterations", 5f },
            {"length", 7f },
            {"width", 1.5f },
            {"angle", 25.7f }
        };

        treeTypes[1] = new Dictionary<char, string>
            {
               {'X', "F" },
               {'F', "F[+F]F[-F][F]" }
            };
        treeTypesDetails[1] = new Dictionary<string, float>
        {
            {"iterations", 5f },
            {"length", 18f },
            {"width", 1.5f },
            {"angle", 20f }
        };

        treeTypes[2] = new Dictionary<char, string>
            {
                {'X', "F" },
                {'F', "FF-[-F+F+F]+[+F-F-F]" }
            };
        treeTypesDetails[2] = new Dictionary<string, float>
        {
            {"iterations", 4f },
            {"length", 20f },
            {"width", 1.5f },
            {"angle", 22.5f }
        };

        treeTypes[3] = new Dictionary<char, string>
            {
                {'X', "F[+X]F[-X]+X" },
                {'F', "FF" }
            };
        treeTypesDetails[3] = new Dictionary<string, float>
        {
            {"iterations", 7f },
            {"length", 2.25f },
            {"width", 1.5f },
            {"angle", 20f }
        };

        treeTypes[4] = new Dictionary<char, string>
            {
                {'X', "F[+X][-X]FX" },
                {'F', "FF" }
            };
        treeTypesDetails[4] = new Dictionary<string, float>
        {
            {"iterations", 7f },
            {"length", 2.3f },
            {"width", 1.5f },
            {"angle", 25.7f }
        };

        treeTypes[5] = new Dictionary<char, string>
            {
                {'X', "F-[[X]+X]+F[+FX]-X" },
                {'F', "FF" }
            };
        treeTypesDetails[5] = new Dictionary<string, float>
        {
            {"iterations", 5f },
            {"length", 7.15f },
            {"width", 1.5f },
            {"angle", 22.5f }
        };

        treeTypes[6] = new Dictionary<char, string>
            {
                {'X', "[F[+FX][*+FX][/+FX]]" },
                {'F', "FF" }
            };
        treeTypesDetails[6] = new Dictionary<string, float>
        {
            {"iterations", 5f },
            {"length", 9.5f },
            {"width", 1.5f },
            {"angle", 27f }
        };

        treeTypes[7] = new Dictionary<char, string>
            {
                {'X', "[*+FX]X[+FX][/+F-FX]" },
                {'F', "FF" }
            };
        treeTypesDetails[7] = new Dictionary<string, float>
        {
            {"iterations", 6f },
            {"length", 4.8f },
            {"width", 1.5f },
            {"angle", 28f }
        };

        treeTypes[8] = new Dictionary<char, string>
            {
                {'X', "[F[-X+F[+FX]][*-X+F[+FX]][/-X+F[+FX]-X]]" },
                {'F', "FF" }
            };
        treeTypesDetails[8] = new Dictionary<string, float>
        {
            {"iterations", 5f },
            {"length", 6.3f },
            {"width", 1.5f },
            {"angle", 25f }
        };
        
}
    //*/

    #endregion

    void Start()
    {
        //TreeTypeDetailsInput(0);
        //Generate(treeTypes[0]);
    }

    #region ========= L System Main Functions =========

    //Funtion01: Add rules from the inputRules OR presetRules
    //Note: public dictionary will not show in the editor inspector in Unity
    public Dictionary<char, string> inputRules = new Dictionary<char, string>();
    public void AddRule(char rl1, string rr1, char rl2, string rr2)
    {
        inputRules.Clear();
        rule_L1 = rl1;
        rule_L2 = rl2;
        rule_R1 = rr1;
        rule_R2 = rr2;
        //Debug.Log("custom rules");
        inputRules.Add(rule_L1, rule_R1);
        inputRules.Add(rule_L2, rule_R2);
    }

    //public void AddPresetRule(char rl1, string rr1, char rl2, string rr2)
    //{
    //    inputRules.Clear();
    //    inputRules.Add(rl1, rr1);
    //    inputRules.Add(rl2, rr2);
    //}

    public void SetParametersValues(float l, float w, float a)
    {
        length = l;
        width = w;
        angle = a;
    }

    public void SetIterations(int i)
    {
        iterations = i;
    }


    //Need to implement before generate the preset rules
    public void SwitchPresetRules()
    {
        switch (preset_Rules)
        {
            case Preset_Rules.None:
                Debug.Log("No current rule to input");
                break;
            case Preset_Rules.Rule1:
                AddRule('X', "F", 'F', "F[+F]F[-F]F");
                SetIterations(5);
                SetParametersValues(7f, 1.5f, 25.7f);              
                break;
            case Preset_Rules.Rule2:
                AddRule('X', "F", 'F', "F[+F]F[-F][F]");
                SetIterations(5);
                SetParametersValues(18f, 1.5f, 20f);
                break;
            case Preset_Rules.Rule3:
                AddRule('X', "F", 'F', "FF-[-F+F+F]+[+F-F-F]");
                SetIterations(4);
                SetParametersValues(20f, 1.5f, 22.5f);
                break;
            case Preset_Rules.Rule4:
                AddRule('X', "F[+X]F[-X]+X", 'F', "FF");
                SetIterations(7);
                SetParametersValues(2.25f, 1.5f, 20f);
                break;
            case Preset_Rules.Rule5:
                AddRule('X', "F[+X][-X]FX", 'F', "FF");
                SetIterations(7);
                SetParametersValues(2.3f, 1.5f, 25.7f);
                break;
            case Preset_Rules.Rule6:
                AddRule('X', "F-[[X]+X]+F[+FX]-X", 'F', "FF");
                SetIterations(5);
                SetParametersValues(7.15f, 1.5f, 22.5f);
                break;
            case Preset_Rules.Rule7:
                AddRule('X', "[F[+FX][*+FX][/+FX]]", 'F', "FF");
                SetIterations(5);
                SetParametersValues(9.5f, 1.5f, 27f);
                break;
            case Preset_Rules.Rule8:
                AddRule('X', "[*+FX]X[+FX][/+F-FX]", 'F', "FF");
                SetIterations(6);
                SetParametersValues(4.8f, 1.5f, 28f);
                break;
            case Preset_Rules.Rule9:
                AddRule('X', "[F[-X+F[+FX]][*-X+F[+FX]][/-X+F[+FX]-X]]", 'F', "FF");
                SetIterations(5);
                SetParametersValues(6.3f, 1.5f, 25f);
                break;


        }
        
    }
    #endregion

    public void SwitchMode(Mode mode)
    {
        switch (mode)
        {
            case Mode.Default:
                //preset_Rules = Preset_Rules.Rule1;
                //SwitchPresetRules();
                break;
            case Mode.Split:
                Debug.Log("Slipt");
                break;
            case Mode.Stereoscopic:
                Debug.Log("Stereoscopic");
                break;
            case Mode.WithLeaf:
                isLeaf= true;
                Debug.Log("WithLeaf: "+ isLeaf);
                break;

        }
    }

    //public void TreeTypeDetailsInput(int treeNo)
    //{
    //    iterations = (int)treeTypesDetails[treeNo]["iterations"];
    //    iterationsSlider.value = treeTypesDetails[treeNo]["iterations"];
    //    length = treeTypesDetails[treeNo]["length"];
    //    lengthSlider.value = length;
    //    width = treeTypesDetails[treeNo]["width"];
    //    widthSlider.value = width;
    //    angle = treeTypesDetails[treeNo]["angle"];
    //    angleSlider.value = angle;
    //}

    public void ChangeColor(bool rainbow)
    {
        
        int renderLineCount = tree.GetComponentsInChildren<LineRenderer>().Length;
        LineRenderer[] branchColor = new LineRenderer[renderLineCount];
        int gradientColorKeyCount = sourceGradient.colorKeys.Length;

        for (int i = 0; i < renderLineCount; i++)
        {
            branchColor[i] = tree.transform.GetChild(i).GetComponent<LineRenderer>();
            if (!rainbow)
            {
                branchColor[i].sharedMaterial.color = sourceGradient.Evaluate(UnityEngine.Random.Range(0f, 1f));
            }
            if (rainbow)
            {
                branchColor[i].material.color = sourceGradient.Evaluate(UnityEngine.Random.Range(0f, 1f));                   
            }

        }
    }




    #region Render Line Generation
    //*
    public void Generate(Dictionary<char, string> rules)
    {

        if (!multiTree)
        {
#if UNITY_STANDALONE_WIN && !UNITY_EDITOR
            DestroyImmediate(tree);
#endif

#if UNITY_EDITOR
            DestroyImmediate(tree);
#endif
        }

        Stack<SavedData> savedDataStack = new Stack<SavedData>();

        //transformStack.Push(new TransformInfo()
        //{
        //    position = Vector3.zero,
        //    rotation = Quaternion.identity,
        //});

        tree = Instantiate(treeRoot);

        currentString = axiom;

        StringBuilder stringbuilding = new StringBuilder();

        for (int i = 0; i < iterations; i++)
        {
            foreach (char c in currentString)
            {
                stringbuilding.Append(rules.ContainsKey(c) ? rules[c] : c.ToString());
                //Debug.Log("current string: " + c + " rules[c]: " + rules[c] + "string building to string: " + stringbuilding.ToString());
            }
            currentString = stringbuilding.ToString();
            stringbuilding = new StringBuilder();
        }

        //Color startColor = UnityEngine.Random.ColorHSV();
        //Color endColor = UnityEngine.Random.ColorHSV();

        for (int c = 0; c < currentString.Length; c++)
        {
            switch (currentString[c])
            {
                case 'F':
                    Vector3 initialPosition = transform.position;
                    transform.Translate(Vector3.up * length);

                    #region leaf test code
                    //startColor = endColor;
                    //endColor = UnityEngine.Random.ColorHSV();                    



                    //isLeaf = false;
                    ////Debug.Log(c);
                    //if (currentString[(c + 1) % currentString.Length] == 'X' || currentString[(c + 3) % currentString.Length] == 'F' && currentString[(c + 4) % currentString.Length] == 'X')
                    //{
                    //    treeSegment = Instantiate(leaf);
                    //    isLeaf = true;
                    //    //currentString[(c + 2) % currentString.Length] == 'F' ||
                    //}
                    ////Leaf Test
                    //if (!rule_R1.Contains("X") && !rule_R2.Contains("X") && currentString[(c + 2) % currentString.Length] == 'F')
                    //{
                    //    Debug.Log("!X");
                    //    treeSegment = Instantiate(leaf);
                    //    isLeaf = true;
                    //}
                    //else
                    //{
                    //    treeSegment = Instantiate(branch);
                    //}
                    #endregion

                    //Generete branches
                    GameObject treeSegment = Instantiate(branch);
                    treeSegment.transform.SetParent(tree.transform);


                    LineRenderer treeRend = treeSegment.GetComponent<LineRenderer>();
                    treeRend.SetPosition(0, initialPosition);
                    treeRend.SetPosition(1, transform.position);
                    treeRend.startWidth = width;
                    treeRend.endWidth = width;

                    #region gradient test code
                    //float alpha = 1.0f;
                    //Gradient gradient = new Gradient();
                    //gradient.SetKeys(
                    //    new GradientColorKey[] { new GradientColorKey(startColor, 0.0f), new GradientColorKey(endColor, 1.0f) },
                    //    new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
                    //);
                    //treeRend.colorGradient = gradient;
                    //treeRend.startColor(0, initialPosition);
                    //treeRend.endColor(1, transform.position);

                    //treeRend.material = new Material(Shader.Find("Sprites/Default"));
                    //treeRend.colorGradient = gradient;
                    #endregion
                    break;

                case 'X':
                    break;

                case '+':
                    transform.Rotate(Vector3.forward * angle);
                    break;

                case '-':
                    transform.Rotate(Vector3.back * angle);
                    break;

                case '*':
                    transform.Rotate(Vector3.up * angle * 5 * (1 + r1));
                    break;

                case '/':
                    transform.Rotate(Vector3.down * angle * 5 * (1 + r2));
                    break;

                case '[':
                    savedDataStack.Push(new SavedData()
                    {
                        position = transform.position,
                        rotation = transform.rotation,
                        //savedColor = endColor
                    }); ;
                    break;

                case ']':

                    #region Feature Leaf Fucntion
                    if (isLeaf)
                    {

                        #region random leaf - Old method
                        /*/
                        GameObject leaves = Instantiate(leaf);
                        leaves.transform.SetParent(tree.transform);
                        //leaves.GetComponent<LineRenderer>().
                        leaves.GetComponent<LineRenderer>().startWidth = LeafWidth;
                        leaves.GetComponent<LineRenderer>().endWidth = LeafWidth;
                        leaves.GetComponent<LineRenderer>().SetPosition(0, transform.position - new Vector3(UnityEngine.Random.Range(0, 2f), 2, UnityEngine.Random.Range(0, 2f)) * UnityEngine.Random.Range(minLeafLength, maxLeafLength));
                        leaves.GetComponent<LineRenderer>().SetPosition(1, transform.position + new Vector3(UnityEngine.Random.Range(0, 2f), 2, UnityEngine.Random.Range(0, 2f)) * UnityEngine.Random.Range(minLeafLength, maxLeafLength));
                        //leaves.transform.localPosition = transform.position;
                        //*/
                        #endregion

                        #region draw filled leaf - Old method
                        /*/
                        
                        Vector3 leafPosition = transform.position;
                        transform.Translate(Vector3.up * UnityEngine.Random.Range(minLeafLength, maxLeafLength));

                        GameObject leafSegment = Instantiate(leaf);
                        leafSegment.transform.SetParent(tree.transform);


                        LineRenderer leafRend = leafSegment.GetComponent<LineRenderer>();
                        leafRend.SetPosition(0, leafPosition);
                        leafRend.SetPosition(1, transform.position);
                        //leafRend.widthCurve = AnimationCurve.Linear(0, width, 1, LeafWidth);
                        leafRend.startWidth = width;
                        leafRend.endWidth = LeafWidth;
                        //*/
                        #endregion

                        #region draw leaf renderline
                        //*/

                        Vector3 leafPosition = transform.position;
                        transform.Translate(Vector3.up * leafRadius);
                        //transform.Translate(Vector3.up * UnityEngine.Random.Range(minLeafLength, maxLeafLength));

                        switch (leafType)
                        {
                            case LeafType.Polygons:
                                GameObject leafSegment = Instantiate(leaf);
                                leafSegment.transform.SetParent(tree.transform);
                                LineRenderer leafRend = leafSegment.GetComponent<LineRenderer>();
                                DrawPolygon(leafRend, leafShape, leafRadius, transform.position, width, width);
                                break;
                            case LeafType.Curve:                                
                                GameObject[] leafSegments = new GameObject[3];
                                LineRenderer[] leafRends = new LineRenderer[3];
                                for (int i = 0; i < leafSegments.Length; i++)
                                {
                                    leafSegments[i] = Instantiate(leaf);
                                    leafSegments[i].transform.SetParent(tree.transform);
                                    leafRends[i] = leafSegments[i].GetComponent<LineRenderer>();
                                }

                                Vector3 AB = transform.position - leafPosition;
                                Vector3 AC = Quaternion.AngleAxis(leafOpenDegree, Vector3.forward) * AB;
                                Vector3 triangle_L = leafPosition + AC;
                                Vector3 AD = Quaternion.AngleAxis(-leafOpenDegree, Vector3.forward) * AB;
                                Vector3 triangle_R = leafPosition + AD;

                                Vector3[] positionTri = new Vector3[3] 
                                { 
                                    leafPosition,
                                    triangle_L,
                                    triangle_R
                                };

                                Vector3[] curvePoint = new Vector3[3] 
                                {
                                    //Median value
                                    //positionTri[0] + (positionTri[1] - positionTri[0])/2,
                                    //positionTri[1] + (positionTri[2] - positionTri[1])/2,
                                    //positionTri[2] + (positionTri[0]- positionTri[2])/2
                                    transform.position - transform.position.normalized * leafCurveSide, //left edge

                                     transform.position + transform.position.normalized * leafCurveEdge, //outside edge

                                    transform.position + transform.position.normalized * leafCurveSide,//right edge
                                };
                                
                                DrawLeaf(leafRends,positionTri, curvePoint, width,width);
                                //DrawTriangle(leafRend, positionTri, width, width);
                                break;


                        }
                        //*/
                        #endregion
                    }
                    #endregion

                    SavedData backtransformInfo = savedDataStack.Pop();
                    transform.position = backtransformInfo.position;
                    transform.rotation = backtransformInfo.rotation;
                    //startColor = backtransformInfo.savedColor;

                    break;

                default:
                    throw new InvalidOperationException("Invalid L-Tree Operation");


            }
        }
        //Debug.Log(transformStack.Pop());
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        tree.transform.rotation = Quaternion.Euler(0, 0, 0);


    }
    //*/
    #endregion

    public void SplitScreen()
    {
        switch (preset_Rules)
        {
            case Preset_Rules.None:
                Debug.Log("No current rule to input");
                break;
            case Preset_Rules.Rule1:
                    tree.name = "TreeOne";
                    //tree.layer = LayerMask.NameToLayer("TreeOne");
                    foreach (Transform g in tree.GetComponentsInChildren<Transform>())
                    {
                        g.gameObject.layer = LayerMask.NameToLayer("TreeOne");
                    }
                    tree.transform.SetParent(treesHolder.transform);                
                break;
            case Preset_Rules.Rule2:
                    tree.name = "TreeTwo";
                    foreach (Transform g in tree.GetComponentsInChildren<Transform>())
                    {
                        g.gameObject.layer = LayerMask.NameToLayer("TreeTwo");
                    }
                    tree.transform.SetParent(treesHolder.transform);
                break;
            case Preset_Rules.Rule3:
                    tree.name = "TreeThree";
                foreach (Transform g in tree.GetComponentsInChildren<Transform>())
                {
                    g.gameObject.layer = LayerMask.NameToLayer("TreeThree");
                }
                tree.transform.SetParent(treesHolder.transform);
                break;
            case Preset_Rules.Rule4:
                    tree.name = "TreeFour";
                foreach (Transform g in tree.GetComponentsInChildren<Transform>())
                {
                    g.gameObject.layer = LayerMask.NameToLayer("TreeFour");
                }
                tree.transform.SetParent(treesHolder.transform);
                break;
            case Preset_Rules.Rule5:
                    tree.name = "TreeFive";
                foreach (Transform g in tree.GetComponentsInChildren<Transform>())
                {
                    g.gameObject.layer = LayerMask.NameToLayer("TreeFive");
                }
                tree.transform.SetParent(treesHolder.transform);
                break;
            case Preset_Rules.Rule6:
                    tree.name = "TreeSix";
                foreach (Transform g in tree.GetComponentsInChildren<Transform>())
                {
                    g.gameObject.layer = LayerMask.NameToLayer("TreeSix");
                }
                tree.transform.SetParent(treesHolder.transform);
                break;
        }

    }

    #region Draw Leaf function
    void DrawPolygon(LineRenderer lineRenderer, int vertexNumber, float radius, Vector3 centerPos, float startWidth, float endWidth)
    {
        lineRenderer.startWidth = startWidth;
        lineRenderer.endWidth = endWidth;
        lineRenderer.loop = true;
        float angle = 2 * Mathf.PI / vertexNumber;
        lineRenderer.positionCount = vertexNumber;

        for (int i = 0; i < vertexNumber; i++)
        {
            Matrix4x4 rotationMatrix = new Matrix4x4(new Vector4(Mathf.Cos(angle * i), Mathf.Sin(angle * i), 0, 0),
                                                     new Vector4(-1 * Mathf.Sin(angle * i), Mathf.Cos(angle * i), 0, 0),
                                       new Vector4(0, 0, 1, 0),
                                       new Vector4(0, 0, 0, 1));
            Vector3 initialRelativePosition = new Vector3(0, radius, 0);
            lineRenderer.SetPosition(i, centerPos + rotationMatrix.MultiplyPoint(initialRelativePosition));

        }
    }

    //void DrawTriangle(LineRenderer lineRenderer, Vector3[] vertexPositions, float startWidth, float endWidth)
    //{
    //    lineRenderer.startWidth = startWidth;
    //    lineRenderer.endWidth = endWidth;
    //    lineRenderer.loop = true;
    //    lineRenderer.positionCount = 3;
    //    lineRenderer.SetPositions(vertexPositions);
    //}

    void DrawQuadraticBezierCurve(LineRenderer lineRenderer, Vector3 point0, Vector3 point1, Vector3 point2,float startWidth, float endWidth)
    {
        lineRenderer.positionCount = 10;
        float t = 0f;
        Vector3 B = new Vector3(0, 0, 0);
        lineRenderer.startWidth = startWidth;
        lineRenderer.endWidth = endWidth;
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            B = (1 - t) * (1 - t) * point0 + 2 * (1 - t) * t * point1 + t * t * point2;
            lineRenderer.SetPosition(i, B);
            t += (1 / (float)lineRenderer.positionCount);
            if (i == lineRenderer.positionCount-1)
            {
                B = point2;
                lineRenderer.SetPosition(i, B);
            }
        }
    }

     
    void DrawLeaf(LineRenderer[] lineRenderer, Vector3[] leafPositions, Vector3[] curvePosition,float startWidth, float endWidth)
    {
        DrawQuadraticBezierCurve(lineRenderer[0], leafPositions[0], curvePosition[0], leafPositions[1], startWidth, endWidth);
        DrawQuadraticBezierCurve(lineRenderer[1], leafPositions[1], curvePosition[1], leafPositions[2], startWidth, endWidth);
        DrawQuadraticBezierCurve(lineRenderer[2], leafPositions[2], curvePosition[2], leafPositions[0], startWidth, endWidth);
    }

    #endregion

    void Update()
    {

        CameraView();
    }

    void CameraView()
    {
        //control users view
        float zoom = Input.GetAxis("Vertical");
        float round = Input.GetAxis("Horizontal");
        currentRound = currentRound + (round * Time.deltaTime * 30);
        cameraRotator.rotation = Quaternion.Euler(0, currentRound, 0);
        currentZoom = currentZoom + (zoom * Time.deltaTime * 30);
        cameraItself.transform.position = new Vector3(cameraItself.transform.position.x, currentZoom, cameraItself.transform.position.z);
        cameraItself.GetComponent<Camera>().orthographicSize = currentZoom;
    }


    //*/


    #region tree selection
    //*
    //public void TreeSelection(int treenumber)
    //{
    //    TreeTypeDetailsInput(treenumber);
    //    Generate(treeTypes[treenumber]);
    //    currentTreeType = treenumber;
    //    //Debug.Log("Tree Type" + treenumber);
    //}

    public void ChangeRandomness(float R)
    {
        randomness = R;
        r1 = randomness / 100 * UnityEngine.Random.Range(-1f, 1f);
        r2 = randomness / 100 * UnityEngine.Random.Range(-1f, 1f);
        //Generate(treeTypes[currentTreeType]);

        //Debug.Log("Ran  "+ randomness + "R1  "+r1+ "R2  "+r2);

    }
    //public void ChangeAngle(float A)
    //{
    //    angle = A;
    //    Generate(treeTypes[currentTreeType]);

    //}
    //public void ChangeeWidth(float W)
    //{
    //    width = W;
    //    Generate(treeTypes[currentTreeType]);

    //}
    //public void ChangeeLength(float L)
    //{
    //    length = L;
    //    Generate(treeTypes[currentTreeType]);

    //}
    //public void ChangeeIterations(float I)
    //{
    //    iterations = (int)I;
    //    Generate(treeTypes[currentTreeType]);
    //    iterationsNumberText.text = I.ToString();

    //}

    //*/
    #endregion

}
