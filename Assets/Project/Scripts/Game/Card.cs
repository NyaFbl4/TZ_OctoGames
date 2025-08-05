using System;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.Game
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private Image _image;
    
        private Sprite _frontSprite;
        private Sprite _backSprite;
        private bool _isFlipped;

        public Action<Card> OnCardClicked;
        public Action OnCardDestroyed;
        public Sprite FrontSprite => _frontSprite;
        public bool IsFlipped => _isFlipped;

        public void InitCard(Sprite frontSprite, Sprite backSprite)
        {
            _frontSprite = frontSprite;
            _backSprite = backSprite;
            _isFlipped = false;
            _image.sprite = _backSprite;
        }
    
        public void Clicked()
        {
            OnCardClicked?.Invoke(this);
        }

        public void DestroyCard()
        {
            OnCardDestroyed?.Invoke();
        }

        public void FlipCard()
        {
            _isFlipped = !_isFlipped;
            _image.sprite = _isFlipped ? _frontSprite : _backSprite;
        }
    }
}