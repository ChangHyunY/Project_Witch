using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Anchor.Unity.UGui.Panel;

public class ShopItem
{
    public Image image;
    public TMPro.TextMeshProUGUI title;
}

public class ComUIShop : ComPanel<ComUIShop>
{
    [SerializeField] private ScrollRect m_ScrollRect;
    [SerializeField] private RectTransform m_Content;
    [SerializeField] private RectTransform m_Item;

    private const int m_DefaultCapacity = 8;

    private List<ShopItem> m_Items = new List<ShopItem>(m_DefaultCapacity);

    protected override void OnClose()
    {
    }

    protected override void OnOpen()
    {
        for (int i = 0; i < m_DefaultCapacity; ++i)
        {
            GameObject go = Instantiate(m_Item.gameObject, m_Content);
            ShopItem item = new ShopItem()
            {
                image = go.GetComponent<Image>(),
                title = go.GetComponentInChildren<TMPro.TextMeshProUGUI>()
            };
            item.image.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
            item.title.text = $"{i + 1}";
            m_Items.Add(item);
        }
    }

    protected override void OnInit()
    {
    }

    protected override void OnSetData(System.EventArgs args)
    {
    }

    protected override void OnSetBenText(string[] text)
    {
    }
}
