using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DangerScriptableObject", menuName = "ScriptableObjects/Danger")]
public class DangerScriptableObject : ScriptableObject
{
    [System.Serializable]
    public class DangerProperties
    {
        public GameObject dangerPrefab;

        public float duration;
    }

    public List<DangerProperties> dangers = new List<DangerProperties>();
}
