using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Linq;

namespace Quests
{
    [CreateAssetMenu(fileName = "New QuestChapter", menuName = "Quests/QuestChapter")]
    public class QuestChapter : SerializedScriptableObject
    {
        [SerializeField]
        public Quest[] Quests { get; set; }

        public bool Contains(QuestID questID)
        {
            return Quests.Any((quest) => quest.ID == questID);
        }
    }
}
