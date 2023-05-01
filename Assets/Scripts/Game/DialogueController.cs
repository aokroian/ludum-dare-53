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
            dialoguePanel.Show(config, () => ReceivePackage(-1, action));
        }

        // private void Intro1(Action action)
        // {
        //     var config = new DialogueConfig
        //     {
        //         portrait = letterSprite,
        //         // title = "NPC_NAME",
        //         message = "You received envelope.",
        //         confirmBtnText = "Ok",
        //         // cancelBtnText = "Fine"
        //     };
        //     dialoguePanel.Show(config, action);
        //     PackageController.Instance.ReceivePackage(-1);
        // }

        public void DeliverPackage(PackageToDeliver package, Action anyAction)
        {
            
            var config = new DialogueConfig
            {
                portrait = npc1Portrait,
                title = package.receiverName,
                message = "Thank you!",
                confirmBtnText = "Ok",
                cancelBtnText = package.receiverDepth == 0 ? "Later" : null
            };
            dialoguePanel.Show(config, () => ReceivePackage(package.receiverDepth, anyAction), anyAction);
            PackageController.Instance.DeliverPackage();
        }

        public void ReceivePackage(int curDepth, Action confirm)
        {
            var config = new DialogueConfig
            {
                portrait = letterSprite,
                // title = "NPC_NAME",
                message = "You received envelope.",
                confirmBtnText = "Ok",
                // cancelBtnText = "Fine"
            };
            dialoguePanel.Show(config, confirm);
            PackageController.Instance.ReceivePackage(curDepth);
        }
        
        public void CantGoUpstairs(Action action = null)
        {
            var config = new DialogueConfig
            {
                portrait = null,
                title = "Can't go upstairs",
                message = "I have a letter to deliver here.",
                confirmBtnText = "Ok",
                cancelBtnText = "Fine"
            };
            dialoguePanel.Show(config, action, action);
        }

        public void PlayerDeath(Action confirm, Action cancel)
        {
            var deliveredCount = PackageController.Instance.deliveredCount;
            var config = new DialogueConfig
            {
                portrait = null,
                title = "You died",
                message = $"You delivered {deliveredCount} packages. Do you want to try again?",
                confirmBtnText = "Restart",
                cancelBtnText = "Main menu"
            };
            dialoguePanel.Show(config, confirm, cancel);
        }
    }
}