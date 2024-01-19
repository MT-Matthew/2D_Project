using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemiesListScriptableObject", menuName = "ScriptableObjects/Event")]
public class EnemiesListScriptableObject : ScriptableObject
{
    [System.Serializable]
    public class EnemyEventProperties
    {
        public GameObject enemyPrefab;
        public int spawnMinute;
        public int spawnSecond;

        public float duration;
        public GameObject eventPrefab;
    }

    public List<EnemyEventProperties> events = new List<EnemyEventProperties>();

}
