using UnityEngine;

public enum WorldType { Level_2 , None }

public  class GlobalTransformDetector  : MonoBehaviour
{
     private Vector2?[,] _globalPositionData;

    [SerializeField] private int WorldSizeX;
    [SerializeField] private int WorldSizeY;
    [SerializeField] private WorldType _world;



    [SerializeField] private GameObject[,] _buildingsInGlobalSpace;
    private void Awake()
    {
        _globalPositionData = new Vector2? [WorldSizeX, WorldSizeY];
        _buildingsInGlobalSpace = new GameObject[WorldSizeX, WorldSizeY];
        for(int x = 0; x < WorldSizeX; x++)
        {
            for(int y = 0; y < WorldSizeY; y++)
            {
                _globalPositionData[x, y] = null;
            }
        }

        for (int x = 0; x < WorldSizeX; x++)
        {
            for (int y = 0; y < WorldSizeY; y++)
            {
                _buildingsInGlobalSpace[x, y] = null;
            }
        }
    }
     private bool CanBuild(int x, int y, int size)
     {
        if (x < 0 || y < 0 || size < 0) return false;

        if (WorldSizeX < x + size / 2)  return false;
        if (WorldSizeX < y + size/2) return false;
         
        if (
            _globalPositionData[x - size/2 ,y - size/2] == Vector2.zero 
            &&
            _globalPositionData[x + size / 2, y + size / 2] == Vector2.zero
            
            ) return true;

        return false;
     }

    public bool TrySetPosition(int x, int y, int size, GameObject gm) 
    { 
        if(CanBuild(x, y, size))
        {
            _globalPositionData[x,y] =  new Vector2(x,y);
            _globalPositionData[x - size / 2, y - size / 2] = new  Vector2(x - size / 2, y - size / 2);
            _globalPositionData[x + size / 2, y + size / 2] = new Vector2(x + size / 2, y + size / 2);


            _buildingsInGlobalSpace[x,y] = gm;


            return true;
        }
        return false;
    }

    public void Initialize(PlainBuilder builder)
    {
        _globalPositionData[builder._PosX , builder._PosY ] = Vector2.zero;
        _globalPositionData[builder._PosX - builder._scale/2, builder._PosY - builder._scale / 2] = Vector2.zero;
        _globalPositionData[builder._PosX + builder._scale / 2, builder._PosY + builder._scale / 2] = Vector2.zero;
    }

}
