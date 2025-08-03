using System;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.Game
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Sprite _frontSprite;

        public Action<Card> OnCardClicked;

        public void Clicked()
        {
            OnCardClicked?.Invoke(this);
            Debug.Log(gameObject.name);
        }
    }
}