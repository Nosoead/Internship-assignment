using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class MonsterDictionaryUI : UIBase
{
    [SerializeField] private Button dictionaryOpenButtonUI;
    [SerializeField] private Image dictionaryButtonsBG;

    private Dictionary<string, ParsedMonsterData> monsterDictionary = new Dictionary<string, ParsedMonsterData>();

    public override void Open()
    {
        base.Open();
        dictionaryOpenButtonUI.onClick.AddListener(() => UIManager.Instance.ToggleUI<MonsterDictionaryUI>(true, false));
        dictionaryOpenButtonUI.onClick.AddListener(() => Pause());
        SetDictionary();
    }

    private void Pause()
    {
        Time.timeScale = 1f;
    }
    private void SetDictionary()
    {
        monsterDictionary = EntityManager.Instance.GetDictionary();
        foreach (var monsterData in monsterDictionary.Values)
        {

        }
    }
}
