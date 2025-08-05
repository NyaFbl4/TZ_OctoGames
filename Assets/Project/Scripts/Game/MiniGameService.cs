using System;
using System.Collections.Generic;
using Naninovel;
using UnityEngine;

namespace Project.Scripts.Game
{
    [InitializeAtRuntime]
    public class MiniGameService : IEngineService
    {
        public event Action OnGameCompleted;
        public event Action<Card> OnCardCreated;
        
        private CardsConfig _config;
        
        private List<Card> _allCards = new();
        private List<Card> _selectedCards = new();
        private bool _isProcessing;
        
        public UniTask InitializeServiceAsync() => UniTask.CompletedTask;
        public void ResetService() => ClearGame();
        public void DestroyService() => ClearGame();
        
        
        public void InitMiniGame(CardsConfig config)
        {
            _config = config;
        }
        
        public void StartNewGame()
        {
            ClearGame();
            CreateCards();
            ShuffleCards();
        }
        
        private void ClearGame()
        {
            if (_allCards == null) return;
            
            foreach (var card in _allCards)
            {
                if (card != null)
                {
                    card.OnCardClicked -= HandleCardClicked;
                    card.OnCardDestroyed -= HandleCardDestroyed;
                    UnityEngine.Object.Destroy(card.gameObject);
                }
            }
            
            _allCards.Clear();
            _selectedCards.Clear();
            _isProcessing = false;
        }

        private void CreateCards()
        {
            foreach (var frontSprite in _config.FrontSprites)
            {
                for (int j = 0; j < 2; j++)
                {
                    var card = UnityEngine.Object.Instantiate(_config.CardPrefab);
                    card.InitCard(frontSprite, _config.BackSprite);
                    
                    card.OnCardClicked += HandleCardClicked;
                    card.OnCardDestroyed += HandleCardDestroyed;
                    
                    _allCards.Add(card);
                    OnCardCreated?.Invoke(card);
                }
            }
        }

        private void ShuffleCards()
        {
            for (int i = _allCards.Count - 1; i > 0; i--)
            {
                int randomIndex = UnityEngine.Random.Range(0, i + 1);
                var temp = _allCards[i].transform.GetSiblingIndex();
                _allCards[i].transform.SetSiblingIndex(_allCards[randomIndex].transform.GetSiblingIndex());
                _allCards[randomIndex].transform.SetSiblingIndex(temp);
                
                (_allCards[i], _allCards[randomIndex]) = (_allCards[randomIndex], _allCards[i]);
            }
        }

        private void HandleCardClicked(Card clickedCard)
        {
            if (_isProcessing || clickedCard.IsFlipped || _selectedCards.Contains(clickedCard))
                return;

            clickedCard.FlipCard();
            _selectedCards.Add(clickedCard);

            if (_selectedCards.Count == 2)
            {
                _isProcessing = true;
                CheckForMatchAsync().Forget();
            }
        }

        private async UniTask CheckForMatchAsync()
        {
            var card1 = _selectedCards[0];
            var card2 = _selectedCards[1];

            if (card1.FrontSprite == card2.FrontSprite)
            {
                await UniTask.Delay(300);
                card1.DestroyCard();
                card2.DestroyCard();
            }
            else
            {
                await UniTask.Delay(1000);
                card1.FlipCard();
                card2.FlipCard();
            }

            _selectedCards.Clear();
            _isProcessing = false;
        }

        private void HandleCardDestroyed()
        {
            if (_allCards.Count == 0)
            {
                OnGameCompleted?.Invoke();
            }
        }
        
        /*
        public event Action OnGameCompleted;
        public event Action<Card> OnCardCreated;
        public event Action OnCardsMatched;
        
        private readonly IInputManager _inputManager;
        private readonly CardsConfig _config;
        
        private List<Card> _allCards = new();
        private List<Card> _selectedCards = new();
        private bool _isProcessing;

        public MiniGameService(IInputManager inputManager, CardsConfig config)
        {
            _inputManager = inputManager;
            _config = config;
        }

        public void StartNewGame()
        {
            ClearGame();
            CreateCards();
            ShuffleCards();
        }

        public UniTask InitializeServiceAsync()
        {
            Debug.Log("MiniGameService инициализирован");
            throw new NotImplementedException();
        }

        public void ResetService() => ClearGame();
        public void DestroyService() => ClearGame();

        private void ClearGame()
        {
            foreach (var card in _allCards)
            {
                if (card != null)
                {
                    card.OnCardClicked -= HandleCardClicked;
                    card.OnCardDestroyed -= HandleCardDestroyed;
                    UnityEngine.Object.Destroy(card.gameObject);
                }
            }
            
            _allCards.Clear();
            _selectedCards.Clear();
            _isProcessing = false;
        }

        private void CreateCards()
        {
            foreach (var frontSprite in _config.FrontSprites)
            {
                for (int j = 0; j < 2; j++)
                {
                    var card = UnityEngine.Object.Instantiate(_config.CardPrefab);
                    card.InitCard(frontSprite, _config.BackSprite);
                    
                    card.OnCardClicked += HandleCardClicked;
                    card.OnCardDestroyed += HandleCardDestroyed;
                    
                    _allCards.Add(card);
                    OnCardCreated?.Invoke(card);
                }
            }
        }

        private void ShuffleCards()
        {
            for (int i = _allCards.Count - 1; i > 0; i--)
            {
                int randomIndex = UnityEngine.Random.Range(0, i + 1);
                var temp = _allCards[i].transform.GetSiblingIndex();
                _allCards[i].transform.SetSiblingIndex(_allCards[randomIndex].transform.GetSiblingIndex());
                _allCards[randomIndex].transform.SetSiblingIndex(temp);
                
                (_allCards[i], _allCards[randomIndex]) = (_allCards[randomIndex], _allCards[i]);
            }
        }

        private void HandleCardClicked(Card clickedCard)
        {
            if (_isProcessing || clickedCard.IsFlipped || _selectedCards.Contains(clickedCard))
                return;

            clickedCard.FlipCard();
            _selectedCards.Add(clickedCard);

            if (_selectedCards.Count == 2)
            {
                _isProcessing = true;
                CheckForMatchAsync().Forget();
            }
        }

        private async UniTask CheckForMatchAsync()
        {
            var card1 = _selectedCards[0];
            var card2 = _selectedCards[1];

            if (card1.FrontSprite == card2.FrontSprite)
            {
                await UniTask.Delay(300);
                card1.DestroyCard();
                card2.DestroyCard();
            }
            else
            {
                await UniTask.Delay(1000);
                card1.FlipCard();
                card2.FlipCard();
            }

            _selectedCards.Clear();
            _isProcessing = false;
        }

        private void HandleCardDestroyed()
        {
            if (_allCards.Count == 0)
            {
                OnGameCompleted?.Invoke();
            }
        }
        */
    }
}