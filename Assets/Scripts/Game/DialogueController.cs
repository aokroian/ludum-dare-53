using System;
using Common;
using UI.Dialogue;
using UnityEngine;

namespace Game
{
    public class DialogueController : SingletonScene<DialogueController>
    {
        [SerializeField] private DialoguePanel dialoguePanel;

        [Header("Resources")]
        [SerializeField] private Sprite npc1Portrait;
        [SerializeField] private Sprite letterSprite;

        private bool _introShowed;
        public bool IntroShowed => _introShowed;
        
        public void Intro(Action action = null)
        {
            if (_introShowed)
            {
                action?.Invoke();
            }

            _introShowed = true;
            
            var config = new DialogueConfig
            {
                portrait = npc1Portrait,
                title = "NPC_NAME",
                message = "Greetings, I have a letter to deliver to you.",
                confirmBtnText = "Ok",
                // cancelBtnText = "Fine"
            };
            dialoguePanel.Show(config, () => Intro1(action));
        }

        private void Intro1(Action action)
        {
            var config = new DialogueConfig
            {
                portrait = letterSprite,
                // title = "NPC_NAME",
                message = "You received envelop.",
                confirmBtnText = "Ok",
                // cancelBtnText = "Fine"
            };
            dialoguePanel.Show(config, action);
        }
        
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