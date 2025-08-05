using Naninovel;
using Naninovel.UI;
using Project.Scripts.Map;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.Map
{
    public class MapUI : CustomUI
    {
        [SerializeField] private Button _location1Btn;
        [SerializeField] private Button _location2Btn;
        [SerializeField] private Button _location3Btn;
        [SerializeField] private GameObject _location3LockIcon;

        private MapService _mapService;

        protected override void Awake()
        {
            base.Awake();
            _mapService = Engine.GetService<MapService>();
        
            _location1Btn.onClick.AddListener(() => NavigateToLocation("Location1"));
            _location2Btn.onClick.AddListener(() => NavigateToLocation("Location2"));
            _location3Btn.onClick.AddListener(() => NavigateToLocation("Location3"));
        }

        protected override void HandleVisibilityChanged(bool visible)
        {
            base.HandleVisibilityChanged(visible);
            if (visible) UpdateUI();
        }

        private void UpdateUI()
        {
            _location3Btn.interactable = _mapService.IsLocationAvailable("Location3");
            
            _location3LockIcon.SetActive(!_mapService.IsLocationAvailable("Location3"));
        }

        private void NavigateToLocation(string locationId)
        {
            _mapService.NavigateToLocation(locationId);
            Hide();
        }
    }
}