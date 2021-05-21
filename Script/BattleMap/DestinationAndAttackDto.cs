using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//210212 敵の攻撃用に移動先と攻撃対象の座標を保持するクラス
public class DestinationAndAttackDto
{
    public int x;
    public int y;
    public Coordinate AttackCoordinate;

    public DestinationAndAttackDto(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}
