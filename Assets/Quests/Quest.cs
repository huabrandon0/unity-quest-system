using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace Quests
{
    [CreateAssetMenu(fileName = "New Quest", menuName = "Quests/Quest")]
    public class Quest : SerializedScriptableObject
    {
        [SerializeField]
        public QuestID ID { get; set; }

        [SerializeField]
        public string Title { get; set; }

        [SerializeField]
        [MultiLineProperty]
        public string Description { get; set; }
    }
}
