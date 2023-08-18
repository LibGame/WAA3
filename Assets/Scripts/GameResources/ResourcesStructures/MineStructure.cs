using System.Collections;
using UnityEngine;

public class MineStructure : GameMapObject
{
    //[field: SerializeField] public int DicMineId { get; private set; }
    [SerializeField] private MinesCube _minesCube;
    [SerializeField] private MeshRenderer _meshRenderer;
    public int DicMineID { get; private set; }
    public string ObjectID => MapObjectID;
    public Vector2Int GatePosition { get; private set; }
    //public CastleObjectFullInfo CastleInfo { get; private set; }
    public int Width { get; private set; }
    public int Height { get; private set; }

    //public void SetCastleObjectFullInfo(CastleObjectFullInfo castleObjectFullInfo)
    //{
    //    CastleInfo = castleObjectFullInfo;
    //}

    private void Start()
    {
        _meshRenderer.material.color = _minesCube.GetColorById(DicMineID);
    }

    public void SetSize(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public void SetGatePosition(Vector2Int vector2Int)
    {
        GatePosition = vector2Int;
    }

    public void SetCastleID()
    {

    }

    public void SetDicMineID(int MineID)
    {
        //if (castleID < 0)
        //    castleID = 0;
        DicMineID = MineID;
    }
}