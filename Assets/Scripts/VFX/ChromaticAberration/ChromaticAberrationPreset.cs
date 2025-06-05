using UnityEngine;

[CreateAssetMenu(fileName = "ChromaticAberrationPreset", menuName = "Scriptable Objects/ChromaticAberrationPreset", order = 1)]

public class ChromaticAberrationPreset : ScriptableObject
{
    [field: SerializeField] public float Intensity { get; private set; }  = 1;
    [field: SerializeField] public float Duration { get; private set; }  = 0.5f;
    [field: SerializeField] public AnimationCurve Curve { get; private set; }
}
