using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Dialogue
{
    public class DialoguePanel : MonoBehaviour
    {
        [SerializeField] Image portrait;
        [SerializeField] TMP_Text nameText;
        [SerializeField] TMP_Text messageText;
        [SerializeField] private Button confirmBtn;
        [SerializeField] private TMP_Text confirmBtnText;
        [SerializeField] private Button cancelBtn;
        [SerializeField] private TMP_Text cancelBtnText;
        
        public void Show(DialogueConfig config, Action confirmAction = null, Action cancelAction = null)
        {
            ApplyConfig(config);
            
            confirmBtn.onClick.RemoveAllListeners();
            cancelBtn.onClick.RemoveAllListeners();
            confirmBtn.onClick.AddListener(() =>
            {
                confirmAction?.Invoke();
                gameObject.SetActive(false);
            });
            cancelBtn.onClick.AddListener(() =>
            {
                cancelAction?.Invoke();
                gameObject.SetActive(false);
            });
        }

        private void ApplyConfig(DialogueConfig config)
        {
            portrait.enabled = config.portrait != null;
            nameText.enabled = config.title != null;
            messageText.enabled = config.message != null;
            confirmBtn.enabled = config.confirmBtnText != null;
            cancelBtn.enabled = config.cancelBtnText != null;
            
            portrait.sprite = config.portrait;
            nameText.text = config.title;
            messageText.text = config.message;
            confirmBtnText.text = config.confirmBtnText;
            cancelBtnText.text = config.cancelBtnText;
            gameObject.SetActive(true);
        }
    }
}