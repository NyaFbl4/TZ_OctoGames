using System;
using System.Collections.Generic;
using Naninovel;
using UnityEngine;

namespace Project.Scripts.Map
{
    [InitializeAtRuntime]
    public class MapService : IEngineService
    {
        public event Action<string> OnLocationSelected;
        private readonly IScriptPlayer _scriptPlayer;
        private readonly HashSet<string> _blockedLocations = new();

        public UniTask InitializeServiceAsync() => UniTask.CompletedTask;
        public void ResetService() {}
        public void DestroyService() {}

        public MapService(IScriptPlayer scriptPlayer)
        {
            _scriptPlayer = scriptPlayer;
        }

        public void BlockLocation(string locationId)
        {
            _blockedLocations.Add(locationId);
        }

        public bool IsLocationAvailable(string locationId)
        {
            return !_blockedLocations.Contains(locationId);
        }

        public void NavigateToLocation(string locationId)
        {
            if (!IsLocationAvailable(locationId))
            {
                Debug.LogWarning($"Location {locationId} is currently blocked!");
                return;
            }

            _scriptPlayer.PreloadAndPlayAsync(locationId).Forget();
        }
    }
}