using UnityEngine;

public struct PlainBuilder 
{
    public int _PosX;
    public int _PosY;
    public int _PosZ;


    public int _scale;

    public PlainBuilder(int posx, int posY, int posz, int scale)
    {
        _PosX = posx;
        _PosY = posY;
        _PosZ = posz;
        _scale = scale;
    }
}
