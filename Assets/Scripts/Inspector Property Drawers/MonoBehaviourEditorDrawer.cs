#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(UnityEngine.MonoBehaviour), true)]
public class MonoBehaviourEditorDrawer : Editor
{
}
#endif