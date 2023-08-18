using Assets.Scripts.MVC.Game;
using Assets.Scripts.MVC.Game.Path;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlaggedMineInfoProcess : MonoBehaviour
{
    private FlaggedMineInfo _currentFlaggedMineInfo;
    private GameAndBattleCommandsSender _gameAndBattleCommandsSender;
    private GameModel _gameModel;
    private HeroPathMover _heroPathMover;
    private PathFinder _pathFinder;
    public FlaggedMineInfoProcess(GameAndBattleCommandsSender gameAndBattleCommandsSender, GameModel gameModel, HeroPathMover heroPathMover, PathFinder pathFinder)
    {
        _gameAndBattleCommandsSender = gameAndBattleCommandsSender;
        _gameModel = gameModel;
        _heroPathMover = heroPathMover;
        _pathFinder = pathFinder;
    }
    public void FlaggedMineInfoHandler(MessageInput messageInput)
    {
        FlaggedMineInfo flaggedMineResult = Newtonsoft.Json.JsonConvert.DeserializeObject<FlaggedMineInfo>(messageInput.body);
        _currentFlaggedMineInfo = flaggedMineResult;
        if (_gameModel.TryGetHeroModelObject(flaggedMineResult.heroId, out HeroModelObject heroModelObject))
        {

            var path = _pathFinder.ConvertPositionToCellPath(flaggedMineResult.movementPath);
            _gameModel.HeroStartMove();
            _heroPathMover.MoveHeroOnPath(heroModelObject, path);
        }
    }
}
