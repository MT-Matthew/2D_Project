using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillScriptableObject", menuName = "ScriptableObjects/Skill")]
public class SkillScriptableObject : ScriptableObject
{
    [System.Serializable]
    public class SkillProperties
    {
        public string characterName;
        public GameObject skillPrefab;

        public float duration;
        public float cooldown;
    }

    public List<SkillProperties> skills = new List<SkillProperties>();

}
