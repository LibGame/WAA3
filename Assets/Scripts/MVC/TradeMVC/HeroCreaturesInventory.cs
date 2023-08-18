﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.MVC.TradeMVC
{
    public class HeroCreaturesInventory : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private List<TradeCreatureSlot> _tradeCreatureSlots;
        private int _lastFilledSlotIndex = 0;

        public void SetCreatureSlots(Sprite icon , ArmySlotInfo armySlotInfo)
        {
            if(_tradeCreatureSlots.Count > _lastFilledSlotIndex)
            {
                _tradeCreatureSlots[_lastFilledSlotIndex].SetCreatureInSlot(icon, armySlotInfo);
                _lastFilledSlotIndex++;
            }
            else
            {
                Debug.LogError("Index exit from array");
            }
        }

        public void SetHeroIcon(Sprite sprite)
        {
            _image.sprite = sprite;
        }

        public void ResetInventory()
        {
            _lastFilledSlotIndex = 0;
            foreach (var item in _tradeCreatureSlots)
                item.ResetSlot();
        }
    }
}