using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Quests
{
    [CreateAssetMenu(fileName = "New QuestBook", menuName = "Quests/QuestBook")]
    public class QuestBook : SerializedScriptableObject
    {
        [SerializeField]
        public QuestChapter[] QuestChapters { get; set; }
    }
}
