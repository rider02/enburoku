using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 210215 �G�̍��G�p�^�[��
/// </summary>
public enum EnemyAIPattern
{
    //�ˌ�AI
    [StringValue("�T��")]
    SEARCH,

    [StringValue("�w��^�[���o�ߌ�ɒT��")]
    WAIT,

    [StringValue("�U���\�͈͂Ƀ��j�b�g������΍U��")]
    REACT,

    [StringValue("�ړ����Ȃ�")]
    BOSS,
}
