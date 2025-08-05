using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.MiniGame
{
    [CreateAssetMenu(fileName = "CardsConfig")]
    public class CardsConfig : ScriptableObject
    {
        [SerializeField] private List<Sprite> _frontSprites;
        [SerializeField] private Sprite _backSprite;
        [SerializeField] private Card _cardPrefab;

        public List<Sprite> FrontSprites => _frontSprites;
        public Sprite BackSprite => _backSprite;
        public Card CardPrefab => _cardPrefab;
    }
}