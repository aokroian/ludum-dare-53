using System;
using Common;
using UI.Dialogue;
using UnityEngine;

namespace Game
{
    public class DialogueController : SingletonScene<DialogueController>
    {
        [SerializeField] private DialoguePanel dialoguePanel;
        
        public void CantGoUpstairs(Action action = null)
        {
            var config = new DialogueConfig
            {
                portrait = null,
                title = "Can't go upstairs",
                message = "I have a letter to deliver deliver here.",
                confirmBtnText = "Ok",
                cancelBtnText = "Fine"
            };
            dialoguePanel.Show(config, action, action);
        }
    }
}