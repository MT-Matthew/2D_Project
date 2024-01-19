using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DropScriptableObject", menuName = "ScriptableObjects/Drop")]
public class DropScriptableObject : ScriptableObject
{
    [System.Serializable]
    public class DropProperties
    {
        public GameObject dropPrefab;
    }

    public List<DropProperties> dropList = new List<DropProperties>();


}
