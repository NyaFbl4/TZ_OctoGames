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
            
            _gameService = Engine.GetService<MiniGameService>();
        
            _gameService.InitMiniGame(_config);
            _isInitialized = true;
        }

        protected override void HandleVisibilityChanged(bool visible)
        {
            base.HandleVisibilityChanged(visible);

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
            // Сообщаем Naninovel, что игра завершена
            var naniGame = Engine.GetService<IScriptPlayer>();
            naniGame.Play(); // Продолжает выполнение скрипта Naninovel
            
            Hide();
            Engine.GetService<IScriptPlayer>().Play();
        }
    }
}