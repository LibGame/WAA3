using Assets.Scripts.MVC.CastleMVC.Buildinngs;
using Assets.Scripts.MVC.CastleSlots;
using Assets.Scripts.MVC.Game;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.MVC.CastleMVC
{
    public class CastleModel
    {
        private List<Building> _buildings;
        private GameModel _gameModel;
        private CommonData _commonData;
        private SlotsController _slotsController;
        private Heroes _heroes;

        public CastleObjectFullInfo CurrentCastleFullOjbectInfo { get; private set; }
        public string CurrentCastleID { get; private set; }
        public string GarrisonID { get; private set; }
        public HeroObjectFullInfo HeroInGarrison { get; private set; }
        public HeroObjectFullInfo HeroInCastle { get; private set; }
        public DicCastleDTO CurrentDicCastleDTO { get; private set; }
        public IReadOnlyList<Building> Buildings => _buildings;

        public CastleModel(CommonData commonData, GameModel gameModel , SlotsController slotsController, Heroes heroes)
        {
            _gameModel = gameModel;
            _commonData = commonData;
            _slotsController = slotsController;
            _heroes = heroes;
            _slotsController = slotsController;
        }

        public void SetCasttleSettings(DicCastleDTO dicCastleDTO, HeroObjectFullInfo heroObjectFullInfo , HeroObjectFullInfo heroIncastle,  string garisonID,string castleID, CastleObjectFullInfo castleObjectFullInfo)
        {
            HeroInGarrison = heroObjectFullInfo;
            HeroInCastle = heroIncastle;
            CurrentDicCastleDTO = dicCastleDTO;
            GarrisonID = garisonID;
            CurrentCastleID = castleID;
            CurrentCastleFullOjbectInfo = castleObjectFullInfo;
            if (HeroInGarrison != null)
            {
                if (castleObjectFullInfo.heroInGarrison != null)
                {
                    if (_gameModel.TryGetHeroModelObject(castleObjectFullInfo.heroInGarrison.mapObjectId, out HeroModelObject heroModelObject))
                        _slotsController.DisplayGarnison(_heroes.GetHeroForCastleIconByID(HeroInGarrison.dicHeroId).Icon, heroModelObject);
                }
                else
                {
                    _slotsController.DisplayGarnison(_heroes.GetHeroForCastleIconByID(HeroInGarrison.dicHeroId).Icon, null);
                }
            }
            else
            if(HeroInCastle != null)
            {
                if (castleObjectFullInfo.heroInCastle != null)
                {
                    if (_gameModel.TryGetHeroModelObject(castleObjectFullInfo.heroInCastle.mapObjectId, out HeroModelObject heroModelObject))
                        _slotsController.DisplayCastle(_heroes.GetHeroForCastleIconByID(HeroInCastle.dicHeroId).Icon, heroModelObject);
                }
                else
                {
                    _slotsController.DisplayGarnison(_heroes.GetHeroForCastleIconByID(HeroInCastle.dicHeroId).Icon, null);
                }
            }
        }

        public void SetBuildings(List<Building> buildings)
        {
            _buildings = buildings;
            foreach (var item in _buildings)
                item.gameObject.SetActive(true);
            InitBuildingsIDS();
        }

        public void DisplayBuildgins(CastleObjectFullInfo fullInfo, DicCastleDTO castleInfo)
        {
            var activeBuildingsInCastle = _buildings.Select(b => b.Id).Intersect(fullInfo.buildings);
            
            activeBuildingsInCastle
                .ToList()
                .ForEach(buildingId =>
                {
                    _buildings[buildingId - 1].gameObject.SetActive(true);
                    
                });

            _buildings
                 .Select(b => b.Id)
                .Except(activeBuildingsInCastle)
                .Where(id => _buildings.Single(b => b.Id == id).gameObject.activeSelf)
                .ToList()
                .ForEach(buildingId =>
                {
                    _buildings.Single(b => b.Id == buildingId).gameObject.SetActive(false);
                });


            foreach (var buildingOnScene in _buildings.Where(b => castleInfo.buildingSet.Contains(b.Id)))
            {
                DicBuildingDTO buildingDTO = _commonData.BuildingDictianory[buildingOnScene.Id];


                if (buildingDTO.creatureId == 0)
                    continue;

                var sameCreatureLevelBuildings = fullInfo.buildings
                    .Select(bId => _commonData.BuildingDictianory[bId])
                    .Where(b => b.level == buildingDTO.level)
                    .Where(b => b.id != buildingDTO.id)
                    .Select(b => b.id);


                if (sameCreatureLevelBuildings.Count() == 1)
                {
                    if (fullInfo.buildings.Contains((int)buildingDTO.id))
                    {
                        _buildings
                            .Where(b => b.Id == Mathf.Min(sameCreatureLevelBuildings.Single(), buildingDTO.id))
                            .Single()
                            .gameObject.SetActive(false);
                    }
                }
            }

            //if (castleInfo.buildingSet[castleInfo.buildingSet.Count - 1] == 30)
            //{
            //    _buildings[31].gameObject.SetActive(true);
            //}
            //else
            //{
            //    _buildings[14].gameObject.SetActive(true);
            //}
        }

        public void InitBuildingsIDS()
        {
            int curBuidlingIndex = 0;
            foreach (DicCastleDTO castleDTO in _commonData.CastleDictianory.Values)
            {
                List<Building> castleBuildings = _buildings.GetRange(curBuidlingIndex, castleDTO.buildingSet.Count);
                var buildingSetEnumerator = castleDTO.buildingSet.GetEnumerator();

                for (int i = 0; i < castleBuildings.Count; i++)
                {
                    if (buildingSetEnumerator.MoveNext())
                    {
                        castleBuildings[i].SetID(buildingSetEnumerator.Current);
                    }
                }
                curBuidlingIndex += castleDTO.buildingSet.Count;
            }
        }

        public bool TryGetBuildingByID(int id, out Building building)
        {
            building = _buildings.FirstOrDefault(item => item.Id == id);

            if (building != null)
                return true;
            return false;
        }


    }
}