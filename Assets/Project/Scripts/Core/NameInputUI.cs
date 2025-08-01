using Naninovel;
using Naninovel.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.Core
{
    public class NameInputUI : CustomUI
    {
        [SerializeField] private TMP_InputField _nameInput;
        [SerializeField] private Button _confirmButton;

        private UniTaskCompletionSource<string> _nameTaskSource;

        protected override void Awake()
        {
            base.Awake();
            _confirmButton.onClick.AddListener(OnConfirm);
        }
        
        public UniTask<string> RequestNameAsync()
        {
            _nameTaskSource = new UniTaskCompletionSource<string>();
            Show();
            return _nameTaskSource.Task;
        }

        private void OnConfirm()
        {
            if (string.IsNullOrWhiteSpace(_nameInput.text)) return;
        
            var variableManager = Engine.GetService<ICustomVariableManager>();
            variableManager.SetVariableValue("playerName", _nameInput.text);
            _nameTaskSource.TrySetResult(_nameInput.text);
            Hide();
        }
    }
}