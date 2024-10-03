using Assets.Clicker.Scripts.Items;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ClickableItem", menuName = "Clicker/Configs/ClickableItem", order = 0)]
public class ItemConfig : ScriptableObject
{
    [SerializeField] private Sprite _sprite;
    public Sprite Sprite => _sprite;

    [SerializeField] private double _baseProduction = 1;
    public double BaseProduction => _baseProduction;

    [SerializeField] private float _clickCost;
    public float ClickCost => _clickCost;

    public List<ItemClickModificator> Modificators;
}
