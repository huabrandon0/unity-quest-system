using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Quests
{
    public class QuestData
    {
        [SerializeField]
        public Dictionary<QuestID, bool> QuestCompletion { get; set; }

        public QuestData()
        {
            QuestCompletion = new Dictionary<QuestID, bool>();
            foreach (QuestID questID in Enum.GetValues(typeof(QuestID)))
                QuestCompletion.Add(questID, false);
        }

        public bool IsCompleted(QuestID questID)
        {
            if (QuestCompletion.TryGetValue(questID, out bool completed))
                return completed;
            else
            {
                DebugLogErrorKeyDoesNotExist(questID);
                return false;
            }
        }

        public void CompleteQuest(QuestID questID)
        {
            if (QuestCompletion.ContainsKey(questID))
                QuestCompletion[questID] = true;
            else
                DebugLogErrorKeyDoesNotExist(questID);
        }

        void DebugLogErrorKeyDoesNotExist(QuestID qid)
        {
            Debug.LogError(string.Format("Unable to find QuestID key {0} in QuestCompletion.", qid));
        }
    }
}
