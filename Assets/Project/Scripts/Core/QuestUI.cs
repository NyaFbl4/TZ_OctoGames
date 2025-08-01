using Naninovel.UI;
using TMPro;
using UnityEngine;

namespace Project.Scripts.Core
{
    public class QuestUI: CustomUI
    {
        [SerializeField] private TMP_Text questText;
    
        public void UpdateQuest(string text)
        {
            questText.text = text;
            Show();
        }
    }
}