using Naninovel;
using Naninovel.UI;
using UnityEngine;
using UnityEngine.UI;

public class MapUI : CustomUI
{
    [SerializeField] private Button _location1Btn;
    [SerializeField] private Button _location2Btn;
    [SerializeField] private Button _location3Btn;

    protected override void Awake()
    {
        base.Awake();

        _location1Btn.onClick.AddListener(() => LoadLocation("Location1"));
        _location2Btn.onClick.AddListener(() => LoadLocation("Location2"));
        _location3Btn.onClick.AddListener(() => LoadLocation("Location3"));
    }

    private void LoadLocation(string scriptName)
    {
        Engine.GetService<IScriptPlayer>().PreloadAndPlayAsync(scriptName).Forget();
        Hide(); 
    }
}