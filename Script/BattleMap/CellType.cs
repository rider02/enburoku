using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//セルのタイプ
public enum CellType
{
    [StringValue("平地")]
    Field = 1,
    [StringValue("茂み")]
    Tree = 2,
    [StringValue("森")]
    Forest = 3,
    [StringValue("山")]
    Mountain = 4,
    [StringValue("壁")]
    Block = 5,
    [StringValue("社殿")]
    JINJA = 6,
    [StringValue("石灯篭")]
    LANTERN = 7,
    [StringValue("水面")]
    WATER = 8,
    [StringValue("深い森")]
    DEEP_FOREST = 9,

}
