using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Naninovel;
using Naninovel.UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Scripts.Game
{
    public class GameUI : CustomUI
    {
        [SerializeField] private Transform _cardsContainer;
        [SerializeField] private CardsConfig _config;
    
        private MiniGameService _gameService;
        private bool _isInitialized;

        protected override void Awake()
        {
            base.Awake();
        
            // Получаем сервис
            _gameService = Engine.GetService<MiniGameService>();
        
            // Инициализируем сервис
            if (_config != null)
            {
                _gameService.InitMiniGame(_config);
                _isInitialized = true;
            }
            else
            {
                Debug.LogError("CardsConfig not assigned in GameUI!");
            }
        }

        protected override void HandleVisibilityChanged(bool visible)
        {
            base.HandleVisibilityChanged(visible);
        
            if (!_isInitialized) return;
        
            if (visible)
            {
                SetupGame();
            }
            else
            {
                ClearGame();
            }
        }

        private void SetupGame()
        {
            if (!_isInitialized) return;
        
            // Добавляем проверку на null
            if (_gameService == null)
            {
                Debug.LogError("GameService is null!");
                return;
            }

            ClearGame();
        
            _gameService.OnCardCreated += HandleCardCreated;
            _gameService.OnGameCompleted += HandleGameCompleted;
        
            _gameService.StartNewGame();
        }

        private void ClearGame()
        {
            if (_gameService != null)
            {
                _gameService.ResetService();
                _gameService.OnCardCreated -= HandleCardCreated;
                _gameService.OnGameCompleted -= HandleGameCompleted;
            }
        }

        private void HandleCardCreated(Card card)
        {
            card.transform.SetParent(_cardsContainer, false);
            card.transform.localScale = Vector3.one;
        }

        private void HandleGameCompleted()
        {
            Hide();
            Engine.GetService<IScriptPlayer>().Play();
        }
        
        /*
        [SerializeField] private CardsConfig _cardsConfig;
        [SerializeField] private Transform _parentContainer;

        public event Action OnGameCompleted;
        
        private List<Card> _allCards = new();
        private List<Card> _selectedCards = new();
        
        private bool _isProcessing;

        protected override void Awake()
        {
            base.Awake();
            InitializeGame();
        }
        
        public void InitializeGame()
        {
            ClearCards();
            _isProcessing = false;
            CreateCards();
            ShuffleCards();
        }
        
        private void ClearCards()
        {
            foreach (var card in _allCards)
            {
                if (card != null) Destroy(card.gameObject);
            }
            _allCards.Clear();
            _selectedCards.Clear();
        }

        private void CreateCards()
        {
            foreach (var frontSprite in _cardsConfig.FrontSprites)
            {
                for (int j = 0; j < 2; j++)
                {
                    var newCardObj = Instantiate(_cardsConfig.CardPrefab, _parentContainer);
                    var newCard = newCardObj.GetComponent<Card>();
                    newCard.InitCard(frontSprite, _cardsConfig.BackSprite);

                    newCard.OnCardClicked += HandleCardClicked;
                    newCard.OnCardDestroyed += () => 
                    {
                        _allCards.Remove(newCard); // Удаляем карту из списка
                        CheckGameCompletion();
                    };
                    
                    _allCards.Add(newCard);
                }
            }
        }
        
        private void ShuffleCards()
        {
            for (int i = _allCards.Count - 1; i > 0; i--)
            {
                int randomIndex = Random.Range(0, i + 1);
                
                _allCards[i].transform.SetSiblingIndex(randomIndex);
                _allCards[randomIndex].transform.SetSiblingIndex(i);
                
                (_allCards[i], _allCards[randomIndex]) = (_allCards[randomIndex], _allCards[i]);
            }
        }

        private void CheckGameCompletion()
        {
            if(_allCards.Count == 0)
            {
                Debug.Log("complite");
                OnGameCompleted?.Invoke();
                Hide();
    
                // Сообщаем Naninovel, что игра завершена
                var naniGame = Engine.GetService<IScriptPlayer>();
                naniGame.Play(); // Продолжает выполнение скрипта Naninovel
            }
            
            Debug.Log("not complite");
            /*
            if(_allCards.Count != 0)
            {
                Debug.Log("not complite");
                return;
            }
            
            foreach (var card in _allCards)
            {
                if (card != null && card.gameObject != null)
                    return;
            }
    
            Debug.Log("complite");
            OnGameCompleted?.Invoke();
            Hide();
    
            // Сообщаем Naninovel, что игра завершена
            var naniGame = Engine.GetService<IScriptPlayer>();
            naniGame.Play(); // Продолжает выполнение скрипта Naninovel
            
        }

        private async void HandleCardClicked(Card clickedCard)
        {
            if (_isProcessing || clickedCard.IsFlipped || _selectedCards.Contains(clickedCard))
                return;

            clickedCard.FlipCard();
            _selectedCards.Add(clickedCard);

            if (_selectedCards.Count == 2)
            {
                _isProcessing = true;
                await CheckForMatchAsync(_selectedCards[0], _selectedCards[1]);
                _selectedCards.Clear();
                _isProcessing = false;
            }
        }
        
        private async Task CheckForMatchAsync(Card card1, Card card2)
        {
            if (card1.FrontSprite == card2.FrontSprite)
            {
                await Task.Delay(300);
                card1.DestroyCard();
                card2.DestroyCard();
            }
            else
            {
                await Task.Delay(1000);
                card1.FlipCard();
                card2.FlipCard();
            }
        }
        */
    }
}