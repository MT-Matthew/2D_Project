using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InformationScriptableObject", menuName = "ScriptableObjects/Information")]
public class InformationScriptableObject : ScriptableObject
{
    public Information[] information;

    public int informationCount
    {
        get
        {
            return information.Length;
        }
    }

    public Information GetInformation(int index)
    {
        return information[index];
    }
}
