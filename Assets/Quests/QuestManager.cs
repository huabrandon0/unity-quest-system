using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Linq;

namespace Quests
{
    public class QuestManager : SerializedMonoBehaviour
    {
        public static QuestManager Instance { get; private set; }

        const string SAVE_FOLDER = "/SaveData";
        const string QUEST_DATA = "/QuestData.json";
        
        [SerializeField]
        QuestData QuestData { get; set; }

        string QuestDataPath { get; set; }
        string SaveFolderPath { get; set; }
        DataFormat DataFormat { get; set; } = DataFormat.JSON;
        
        [SerializeField]
        QuestChapter[] QuestChapters { get; set; }

        int CurrentQuestChapterIndex { get; set; } = 0;

        void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(gameObject);
            else
                Instance = this;

            string dataPath = Application.isEditor ? Application.dataPath : Application.persistentDataPath;
            SaveFolderPath = string.Format("{0}{1}", dataPath, SAVE_FOLDER);
            QuestDataPath = string.Format("{0}{1}", SaveFolderPath, QUEST_DATA);

            LoadQuestData();
        }

        [Button]
        void LoadQuestData()
        {
            if (File.Exists(QuestDataPath))
            {
                var bytes = File.ReadAllBytes(QuestDataPath);
                QuestData = SerializationUtility.DeserializeValue<QuestData>(bytes, DataFormat);
                if (QuestData == null)
                    ResetQuestData();
                else
                {
                    CurrentQuestChapterIndex = 0;
                    while (IsValidQuestChapterIndex(CurrentQuestChapterIndex) && 
                        QuestChapters[CurrentQuestChapterIndex].Quests.All((quest) => QuestData.IsCompleted(quest.ID)))
                        CurrentQuestChapterIndex++;
                }
            }
            else
                ResetQuestData();
        }

        [Button]
        void SaveQuestData()
        {
            if (!Directory.Exists(SaveFolderPath))
                Directory.CreateDirectory(SaveFolderPath);

            var bytes = SerializationUtility.SerializeValue(QuestData, DataFormat);
            File.WriteAllBytes(QuestDataPath, bytes);
        }
        
        [Button]
        void ResetQuestData()
        {
            QuestData = new QuestData();
            CurrentQuestChapterIndex = 0;
        }

        [Button]
        public void CompleteQuest(QuestID questID)
        {
            if (!QuestData.IsCompleted(questID))
            {
                QuestChapter currentQuestChapter = IsValidQuestChapterIndex(CurrentQuestChapterIndex) ? QuestChapters[CurrentQuestChapterIndex] : null;
                if (currentQuestChapter != null && currentQuestChapter.Contains(questID))
                {
                    QuestData.CompleteQuest(questID);
                    if (QuestChapters[CurrentQuestChapterIndex].Quests.All((quest) => QuestData.IsCompleted(quest.ID)))
                        CurrentQuestChapterIndex++;
                }
            }
        }

        bool IsValidQuestChapterIndex(int chapter)
        {
            return chapter >= 0 && chapter < QuestChapters.Length;
        }
    }
}
