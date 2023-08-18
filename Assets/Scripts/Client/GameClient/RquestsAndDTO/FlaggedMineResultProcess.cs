using Assets.Scripts.MVC.Game;
using Assets.Scripts.MVC.Game.Path;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlaggedMineResultProcess
{
    private FlaggedMineResult _currentFlaggedMineResult;
    private GameAndBattleCommandsSender _gameAndBattleCommandsSender;
    private GameModel _gameModel;
    private HeroPathMover _heroPathMover;
    private PathFinder _pathFinder;
    private PathDrawer _pathDrawer;
    public FlaggedMineResultProcess(GameAndBattleCommandsSender gameAndBattleCommandsSender, GameModel gameModel, HeroPathMover heroPathMover, PathFinder pathFinder, PathDrawer pathDrawer)
    {
        _gameAndBattleCommandsSender = gameAndBattleCommandsSender;
        _gameModel = gameModel;
        _heroPathMover = heroPathMover;
        _pathFinder = pathFinder;
        _pathDrawer = pathDrawer;
    }
    public void FlaggedMineResultHandler(MessageInput messageInput)
    {
        FlaggedMineResult flaggedMineResult = Newtonsoft.Json.JsonConvert.DeserializeObject<FlaggedMineResult>(messageInput.body);
        _currentFlaggedMineResult = flaggedMineResult;
        if (_gameModel.TryGetHeroModelObject(flaggedMineResult.heroId, out HeroModelObject heroModelObject))
        {
            heroModelObject.SetMovePointsLeft(flaggedMineResult.movePointsLeft);

            var path = _pathFinder.ConvertPositionToCellPath(flaggedMineResult.movementPath);
            _pathDrawer.DrawPath(path);
            _gameModel.HeroStartMove();
            _heroPathMover.MoveHeroOnPath(heroModelObject, path);
        }
    }
}
