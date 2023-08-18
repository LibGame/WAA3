using Assets.Scripts.MVC.CastleSlots;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.MVC.TradeMVC
{
    public class TradeController : MonoBehaviour
    {
        [SerializeField] private LayerMask _slotLayerMask;
        private TradeModel _tradeModel;
        private TradeCreatureSlot _lastPickSlot;


        public void Init(TradeModel tradeModel)
        {
            _tradeModel = tradeModel;
        }

        public void EnterInTradeAndSetPariticipants(HeroObjectFullInfo requesterHeroInfo , HeroObjectFullInfo receiverHeroInfo , Sprite requestIcon, Sprite receiverIcon)
        {
            _tradeModel.SetTradeParticipants(requesterHeroInfo, receiverHeroInfo, requestIcon, receiverIcon, FindAnyObjectByType<GameSceneLoaderTrigger>().RequesterHeroStats, FindAnyObjectByType<GameSceneLoaderTrigger>().RecieverHeroHeroStats);
        }

        public void ExitFromTradePanel()
        {
            if (_lastPickSlot != null)
            {
                _lastPickSlot.GetComponent<Image>().color = Color.white;
            }
            _tradeModel.SumbitTrade();
            FindAnyObjectByType<SlotsController>().tradePanelOpened = false;
            Debug.Log(FindAnyObjectByType<SlotsController>().tradePanelOpened + " FindAnyObjectByType<SlotsController>()");
            
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(TryPickSlot(out TradeCreatureSlot tradeCreatureSlot))
                {
                    if(_lastPickSlot != null)
                    {
                        _lastPickSlot.GetComponent<Image>().color = Color.white;
                    }
                    _tradeModel.PickTradeCreatureSlot(tradeCreatureSlot);
                    _lastPickSlot = tradeCreatureSlot;
                    tradeCreatureSlot.GetComponent<Image>().color = Color.red;
                }
            }
        }

        private bool TryPickSlot(out TradeCreatureSlot tradeCreatureSlot)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                PointerEventData eventData = new PointerEventData(EventSystem.current);
                eventData.position = Input.mousePosition;
                List<RaycastResult> raysastResults = new List<RaycastResult>();
                EventSystem.current.RaycastAll(eventData, raysastResults);
                foreach (var item in raysastResults)
                {
                    if (item.gameObject.TryGetComponent(out tradeCreatureSlot))
                    {
                        return true;
                    }
                }
            }
            tradeCreatureSlot = null;
            return false;
        }

    }
}