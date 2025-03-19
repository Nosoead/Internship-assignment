using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class DictionaryButtonUI : UIBase
{
    [SerializeField] private Button dictionaryOpenButtonUI;
            
    public override void Open()
    {
        base.Open();
        dictionaryOpenButtonUI.onClick.AddListener(() => UIManager.Instance.ToggleUI<MonsterDictionaryUI>(true, true));
        dictionaryOpenButtonUI.onClick.AddListener(() => Pause());
    }

    private void Pause()
    {
        Time.timeScale = 0f;
    }
}
