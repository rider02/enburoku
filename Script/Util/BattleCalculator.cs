using UnityEngine;

/// <summary>
/// 戦闘に使用
/// 戦闘するユニットと敵を渡すと戦闘の数値を計算して返してくれるクラス
/// </summary>
public class BattleCalculator : MonoBehaviour
{
    //ユニットの攻撃力、命中率、必殺率
    public int unitAttack { get; set; } 
    public int unitHitRate { get; set; }
    public int unitCriticalRate { get; set; }

    
    //敵の攻撃力、命中率、必殺率
    public int enemyAttack { get; set; }
    public int enemyHitRate { get; set; }
    public int enemyCriticalRate { get; set; }

    //追撃フラグ trueなら2回攻撃
    public bool unitChaseFlag { get; set; }
    public bool enemyChaseFlag { get; set; }

    //運と技 スキル発動率計算の為使用
    public int unitLuk { get; set; }
    public int unitDex { get; set; }

    public int enemyLuk { get; set; }
    public int enemyDex { get; set; }

    //ユニットの回復量
    public int healAmount { get; set; } 

    //210220 攻撃可否フラグ
    public bool isUnitAttackable { get; set; }

    public bool isEnemyAttackable { get; set; }


    /// <summary>
    /// ユニット、敵、武器を受け取るとダメージ、命中率などを計算してDTOに詰めて返してくれる
    /// </summary>
    /// <param name="unit">ユニット名</param>
    /// <param name="unitWeapon">ユニットの武器</param>
    /// <param name="unitCell">ユニットの地形</param>
    /// <param name="enemy">敵</param>
    /// <param name="enemyCell">敵の地形</param>
    /// <param name="isPlayerAttack">プレイヤーからの攻撃か</param>
    /// <returns></returns>
    public BattleParameterDTO CalculateBattleParameter(Unit unit, Weapon unitWeapon, Main_Cell unitCell, Enemy enemy, Main_Cell enemyCell,
        bool isPlayerAttack)
    {
        //戻り値を詰めるクラス
        BattleParameterDTO battleParameterDTO = new BattleParameterDTO();

        //職業ステータス補正が入ったクラス
        JobStatusDto jobstatusDto = unit.job.statusDto;

        //スキル「狂化」判定 ステータスに反映するので最初に判定
        bool isUnitBerserk = false;
        bool isEnemyBerserk = false;

        bool isCloseAttack; //近接攻撃フラグ

        ///ユニットの計算

        //210218 ステータスと職業による基礎値の計算 他に装備品補正、バフ、デバフ、スキル判定が必要
        int unitMaxHp = unit.maxhp + jobstatusDto.jobHp;
        int unitLatk = unit.latk + jobstatusDto.jobLatk;
        int unitCatk = unit.catk + jobstatusDto.jobCatk;
        int unitDex = unit.dex + jobstatusDto.jobDex;
        int unitAgi = unit.agi + jobstatusDto.jobAgi;
        int unitLuk = unit.luk + jobstatusDto.jobLuk;
        int unitLdef = unit.ldef + jobstatusDto.jobLdef;
        int unitCdef = unit.cdef + jobstatusDto.jobCdef;

        //210218 バフを反映する処理を追加
        StatusDto buffedStatus = new StatusDto(unitMaxHp, unitLatk, unitCatk, unitAgi, unitDex, unitLdef, unitCdef, unitLuk);
        StatusCalculator statusCalculator = new StatusCalculator();

        //スキル「狂化」判定
        if (unit.hp != statusCalculator.CalcHpBuff(unitMaxHp, unit.job.skills) && unit.job.skills.Contains(Skill.狂化))
        {
            isUnitBerserk = true;
        }

        //武器、装飾品、スキルのバフ反映 最大値を上回らないようにして返される
        buffedStatus = statusCalculator.GetBuffedStatus(buffedStatus, unit.name, unit.equipWeapon,unit.equipAccessory,unit.job.skills, isUnitBerserk);

        //値を更新
        unitMaxHp = buffedStatus.hp;
        unitLatk = buffedStatus.latk;
        unitCatk = buffedStatus.catk;
        unitDex = buffedStatus.dex;
        this.unitDex = buffedStatus.dex;
        unitAgi = buffedStatus.agi;

        //スキル発動計算の為DTOにもセット
        unitLuk = buffedStatus.luk;
        this.unitLuk = buffedStatus.luk;

        unitLdef = buffedStatus.ldef;
        unitCdef = buffedStatus.cdef;

        ///敵の計算

        //210227 敵のステータスも上級職補正等は職業から取得する用に変更
        JobStatusDto enemyJobStatusDto = enemy.job.statusDto;

        //210218 ステータスと職業による基礎値の計算 他に装備品補正、バフ、デバフ、スキル判定が必要
        int enemyMaxHp = enemy.maxhp + enemyJobStatusDto.jobHp;
        int enemyLatk = enemy.latk + enemyJobStatusDto.jobLatk;
        int enemyCatk = enemy.catk + enemyJobStatusDto.jobCatk;
        int enemyDex = enemy.dex + enemyJobStatusDto.jobDex;
        int enemyAgi = enemy.agi + enemyJobStatusDto.jobAgi;
        int enemyLuk = enemy.luk + enemyJobStatusDto.jobLuk;
        int enemyLdef = enemy.ldef + enemyJobStatusDto.jobLdef;
        int enemyCdef = enemy.cdef + enemyJobStatusDto.jobCdef;

        //210218 敵のバフを反映する処理
        StatusDto enemyBuffedStatus = new StatusDto(enemyMaxHp, enemyLatk, enemyCatk, enemyAgi, enemyDex, enemyLdef, enemyCdef, enemyLuk);

        //スキル「狂化」判定
        if (enemy.hp != statusCalculator.CalcHpBuff(enemyMaxHp, enemy.job.skills) && enemy.job.skills.Contains(Skill.狂化))
        {
            isEnemyBerserk = true;
        }

        //武器、装飾品、スキルのバフ反映 最大値を上回らないようにして返される
        enemyBuffedStatus = statusCalculator.GetBuffedStatus(enemyBuffedStatus, enemy.name, enemy.equipWeapon, enemy.equipAccessory, enemy.job.skills,isEnemyBerserk);

        //値を更新
        enemyMaxHp = enemyBuffedStatus.hp;
        enemyLatk = enemyBuffedStatus.latk;
        enemyCatk = enemyBuffedStatus.catk;
        enemyDex = enemyBuffedStatus.dex;
        this.enemyDex = enemyBuffedStatus.dex;
        enemyAgi = enemyBuffedStatus.agi;

        //スキル発動計算の為DTOにもセット
        enemyLuk = enemyBuffedStatus.luk;
        this.enemyLuk = enemyBuffedStatus.luk;

        enemyLdef = enemyBuffedStatus.ldef;
        enemyCdef = enemyBuffedStatus.cdef;



        //210226 自分から攻撃した場合、ダメージが上がるスキル
        //これはステータス画面には表示しないのでstatusCalculator不使用
        //プレイヤーから攻撃した時
        if (isPlayerAttack){
            if (unit.job.skills.Contains(Skill.飛燕の一撃))
            {
                unitAgi += 5;
                Debug.Log($"{unit.name}スキル{Skill.飛燕の一撃} 速さ+5");
            }
            if (unit.job.skills.Contains(Skill.金剛の一撃))
            {
                unitCdef += 10;
                Debug.Log($"{unit.name}スキル{Skill.金剛の一撃} 近防-10");
            }
            if (unit.job.skills.Contains(Skill.鬼神の一撃))
            {
                unitCatk += 6;
                Debug.Log($"{unit.name}スキル{Skill.鬼神の一撃} 近攻+6");
            }
            if (unit.job.skills.Contains(Skill.魔女の一撃))
            {
                unitLatk += 6;
                Debug.Log($"{unit.name}スキル{Skill.魔女の一撃} 遠攻+6");
            }
        }
        else
        {
            //敵の〇〇の一撃判定
            if (enemy.job.skills.Contains(Skill.飛燕の一撃))
            {
                enemyAgi += 5;
                Debug.Log($"{unit.name}スキル{Skill.飛燕の一撃} 速さ+5");
            }
            if (enemy.job.skills.Contains(Skill.金剛の一撃))
            {
                enemyCdef += 10;
                Debug.Log($"{unit.name}スキル{Skill.金剛の一撃} 近防-10");
            }
            if (enemy.job.skills.Contains(Skill.鬼神の一撃))
            {
                enemyCatk += 6;
                Debug.Log($"{unit.name}スキル{Skill.鬼神の一撃} 近攻+6");
            }
            if (enemy.job.skills.Contains(Skill.魔女の一撃))
            {
                enemyLatk += 6;
                Debug.Log($"{unit.name}スキル{Skill.魔女の一撃} 遠攻+6");
            }
        }

        //慧眼の一撃(命中率上がるやつ)はまた別の場所で実装


        //210219 地形効果を味方、敵のセルから取得
        int unitTerrainHit;
        int unitTerrainDef;

        //通常は有り得ないが、セルが無ければ0
        if (unitCell == null)
        {
            Debug.Log("ERROR:ユニットの居るセルの情報が有りません");
            unitTerrainHit = 0;
            unitTerrainDef = 0;
        }
        else
        {
            unitTerrainHit = unitCell.AvoidRate;
            unitTerrainDef = unitCell.Defence;
        }

        int enemyTerrainHit;
        int enemyTerrainDef;

        if (enemyCell == null)
        {
            Debug.Log("ERROR:敵の居るセル情報が有りません");
            enemyTerrainHit = 0;
            enemyTerrainDef = 0;
        }
        else
        {
            enemyTerrainHit = enemyCell.AvoidRate;
            enemyTerrainDef = enemyCell.Defence;
        }

        //スキル判定 目隠し 攻撃を受けた時、回避+20%、守備+2
        bool isUnitBlind = false;
        bool isEnemyBlind = false;
        
        if (!isPlayerAttack && unit.job.skills.Contains(Skill.目隠し)) {
            Debug.Log($"{unit.name} スキル :{Skill.目隠し.ToString()}, {Skill.目隠し.GetStringValue()}");
            isEnemyBlind = true;
        }
        if (isPlayerAttack && enemy.job.skills.Contains(Skill.目隠し))
        {
            Debug.Log($"{enemy.name} スキル :{Skill.目隠し.ToString()}, {Skill.目隠し.GetStringValue()}");
            isUnitBlind = true;
        }



        //210220 攻撃可否を確認
        //座標の絶対値の合計が距離で、その値が武器の射程距離以下なら反撃可能となる
        //射程距離が1の場合、武器が近距離攻撃可能の場合は反撃可能となる
        //距離を取得
        int distance = Mathf.Abs(unitCell.X - enemyCell.X) + Mathf.Abs(unitCell.Y - enemyCell.Y);
        Debug.Log($"ユニットと敵との距離:{distance}");
        Debug.Log($"ユニット:X={unitCell.X},Y={unitCell.Y} 敵:X={enemyCell.X},Y={enemyCell.Y}");

        //近距離攻撃か遠距離か判定
        isCloseAttack = (distance == 1) ? true : false;
        Debug.Log($"近距離攻撃か:{isCloseAttack}");

        //武器の射程距離が距離よりも少ない場合は攻撃不可
        if (unitWeapon.range < distance)
        {
            isUnitAttackable = false;
        }
        else if (distance == 1 && !unitWeapon.isCloseAttack)
        {
            //武器の射程距離内だが、射程が1で近距離攻撃可能ではない場合
            //(射程2以上の武器は、近距離攻撃フラグが無い場合は近距離攻撃出来ない)
            isUnitAttackable = false;
        }
        else
        {
            isUnitAttackable = true;
        }

        //天狗のカメラは反撃不可
        if (enemy.equipWeapon.name == "天狗のカメラ" && !isPlayerAttack)
        {
            isUnitAttackable = false;
        }

        battleParameterDTO.isUnitAttackable = isUnitAttackable;
        Debug.Log($"ユニットの攻撃可否:{battleParameterDTO.isUnitAttackable}");

        //敵の攻撃可否判定 プレイヤーと同じ
        if (enemy.equipWeapon.range < distance)
        {
            isEnemyAttackable = false;
        }
        else if (distance == 1 && !enemy.equipWeapon.isCloseAttack)
        {
            isEnemyAttackable = false;
        }
        else
        {
            isEnemyAttackable = true;
        }

        if (unit.equipWeapon.name == "天狗のカメラ" && isPlayerAttack)
        {
            //ユニットの武器が天狗のカメラ、かつ文ちゃんから攻撃した場合
            isEnemyAttackable = false;
        }

        battleParameterDTO.isEnemyAttackable = isEnemyAttackable;
        Debug.Log($"敵の攻撃可否:{battleParameterDTO.isEnemyAttackable}");



        ///攻撃力計算
        //TODO 武器を持っていない状態で攻撃される場合を考慮する
        int unitWeaponAttack = unitWeapon.attack;

        //NONE以外の場合は何かしら特効対象が有るということ
        //かつ武器の特効対象と敵の種族が一致した場合は特効
        if (unitWeapon.slayer != RaceType.NONE &&
            unitWeapon.slayer == enemy.race)
        {
            //武器威力を3倍にして特効フラグを立てる 特効時は文字の色が変わる
            unitWeaponAttack *= 3;
            battleParameterDTO.isUnitAttackSlayer = true;
        }

        //210301 攻撃力計算 遠距離、近距離の概念を反映
        int unitRefAttack = isCloseAttack ? unitCatk : unitLatk;
        int enemydef = isCloseAttack ? enemyCdef : enemyLdef;

        //力＋武器威力*特効なら3倍 - 敵防御 - 地形効果
        unitAttack = (unitRefAttack + unitWeaponAttack) - enemydef - enemyTerrainDef;

        //スキル目隠し 敵の回避+20、守備+2
        if (isUnitBlind)
        {
            unitAttack -= 2;
        }

        //スキル「密室の守り」自分が地形効果0以外の場所で戦闘時、受けるダメージ-3
        if ((enemyTerrainHit != 0 || enemyTerrainDef != 0) && enemy.job.skills.Contains(Skill.密室の守り))
        {
            unitAttack -= 3;
            Debug.Log($"{enemy.name}: スキル{Skill.密室の守り} 効果{Skill.密室の守り.GetStringValue()}");
        }

        //スキル「死線」 敵に与えるダメージ+10、敵から受けるダメージ+10
        if (unit.job.skills.Contains(Skill.死線))
        {
            //ユニットが持っていれば攻撃力アップ
            unitAttack += 10;
            Debug.Log($"{unit.name}: スキル{Skill.死線} 効果{Skill.死線.GetStringValue()}");
        }
        if (enemy.job.skills.Contains(Skill.死線))
        {
            //敵が持っていれば受けるダメージアップ
            unitAttack += 10;
            Debug.Log($"{enemy.name}: スキル{Skill.死線} 効果{Skill.死線.GetStringValue()}");
        }

        //0～100の間に訂正
        unitAttack = CorrectParameter(unitAttack);
        battleParameterDTO.unitAttack = unitAttack;

        //敵の攻撃力計算 プレイヤーと同じ
        //TODO 敵は武器を持っていない可能性が有るけどまだ考慮してない
        if (enemy.equipWeapon != null)
        {
            //敵も特効の計算
            int enemyWeaponAttack = enemy.equipWeapon.attack;

            if (enemy.equipWeapon.slayer != RaceType.NONE &&
            enemy.equipWeapon.slayer == unit.race)
            {
                enemyWeaponAttack *= 3;
                battleParameterDTO.isEnemyAttackSlayer = true;
            }

            int enemyRefAttack = isCloseAttack ? enemyCatk : enemyLatk;
            int unitdef = isCloseAttack ? unitCdef : unitLdef;
            enemyAttack = (enemyRefAttack + enemyWeaponAttack) - unitdef - unitTerrainDef;

            //スキル目隠し 敵の回避+20、守備+2
            if (isEnemyBlind)
            {
                enemyAttack -= 2;
            }

            //スキル「密室の守り」自分が地形効果0以外の場所で戦闘時、受けるダメージ-3
            if ((unitTerrainHit != 0 || unitTerrainDef != 0) && unit.job.skills.Contains(Skill.密室の守り))
            {
                enemyAttack -= 3;
                Debug.Log($"{unit.name}: スキル{Skill.密室の守り} 効果{Skill.密室の守り.GetStringValue()}");
            }

            //スキル「死線」
            if (unit.job.skills.Contains(Skill.死線))
            {
                enemyAttack += 10;
                Debug.Log($"{unit.name}: スキル{Skill.死線} 効果{Skill.死線.GetStringValue()}");
            }
            if (enemy.job.skills.Contains(Skill.死線))
            {
                enemyAttack += 10;
                Debug.Log($"{enemy.name}: スキル{Skill.死線} 効果{Skill.死線.GetStringValue()}");
            }

            
            battleParameterDTO.enemyAttack = CorrectParameter(enemyAttack);
        }


        ///命中率計算
        //物理命中率＝物理命中－敵物理回避－地形効果
        //3すくみの乗っていない命中率計算 まだマイナス、100%以上になってもよい
        unitHitRate = (unitDex + unitWeapon.hitRate) - enemyAgi - enemyTerrainHit;

        //敵が武器を持っていれば、敵の命中率計算
        if (enemy.equipWeapon != null)
        {
            
            enemyHitRate = (enemyDex + enemy.equipWeapon.hitRate) - unitAgi - unitTerrainHit;
        }
        else
        {
            //無ければUI表示は「-」となり参照されることは無いが一旦0
            enemyHitRate = 0;
        }

        //210226 スキル「一撃離脱反映」自分から攻撃時、回避+30
        //プレイヤーの一撃離脱反映
        if (isPlayerAttack && unit.job.skills.Contains(Skill.一撃離脱))
        {
            enemyHitRate -= 30;
            Debug.Log($"{unit.name}: スキル{Skill.一撃離脱} 回避+30");
        }
        else if (!isPlayerAttack && enemy.job.skills.Contains(Skill.一撃離脱))
        {
            //敵の一撃離脱反映
            unitHitRate -= 30;
            Debug.Log($"{enemy.name}: スキル{Skill.一撃離脱} 回避+30");
        }

        //スキル「慧眼の一撃」判定 自分から攻撃時命中+40%
        if (isPlayerAttack && unit.job.skills.Contains(Skill.慧眼の一撃))
        {
            unitHitRate += 40;
            Debug.Log($"{unit.name}: スキル{Skill.慧眼の一撃} 命中+40");
        }
        else if (!isPlayerAttack && enemy.job.skills.Contains(Skill.慧眼の一撃))
        {
            enemyHitRate += 40;
            Debug.Log($"{enemy.name}: スキル{Skill.慧眼の一撃} 命中+40");
        }

        //スキル「自信満々」HPが100％の時命中・回避+15
        if (unitMaxHp == unit.hp)
        {
            if (unit.job.skills.Contains(Skill.自信満々))
            {
                enemyHitRate -= 15;
                unitHitRate += 15;
                Debug.Log($"{unit.name}: スキル{Skill.自信満々} 命中・回避+15");
            }
            if (unit.job.skills.Contains(Skill.完璧主義))
            {
                enemyHitRate -= 15;
                unitHitRate += 15;
                Debug.Log($"{unit.name}: スキル{Skill.完璧主義} 命中・回避+15");
            }

        }

        if (enemyMaxHp == enemy.hp)
        {
            if (enemy.job.skills.Contains(Skill.自信満々))
            {
                enemyHitRate += 15;
                unitHitRate -= 15;
                Debug.Log($"{unit.name}: スキル{Skill.自信満々} 命中・回避+15");
            }
            if (unit.job.skills.Contains(Skill.完璧主義))
            {
                enemyHitRate += 15;
                unitHitRate -= 15;
                Debug.Log($"{enemy.name}: スキル{Skill.完璧主義} 命中・回避+15");
            }

            
        }

        //スキル「狂気の瞳」 無条件で回避+20
        if (unit.job.skills.Contains(Skill.狂気の瞳))
        {
            enemyHitRate -= 20;
            Debug.Log($"{unit.name}: スキル{Skill.狂気の瞳} 効果{Skill.狂気の瞳.GetStringValue()}");
        }
        if (enemy.job.skills.Contains(Skill.狂気の瞳))
        {
            unitHitRate -= 20;
            Debug.Log($"{enemy.name}: スキル{Skill.狂気の瞳} 効果{Skill.狂気の瞳.GetStringValue()}");
        }

        //スキル「狂化」 命中率 - 10%
        if (isUnitBerserk)
        {
            unitHitRate -= 10;
            Debug.Log($"{unit.name}: スキル{Skill.狂化} 効果{Skill.狂化.GetStringValue()}");
        }
        if (isEnemyBerserk)
        {
            enemyHitRate -= 10;
            Debug.Log($"{enemy.name}: スキル{Skill.狂化} 効果{Skill.狂化.GetStringValue()}");
        }

        //スキル「目隠し」判定  敵の回避+20、守備+2
        if (isUnitBlind)
        {
            unitHitRate -= 20;
        }
        else if (isEnemyBlind)
        {
            enemyHitRate -= 20;
        }

        //戻り値に計算した値を設定
        battleParameterDTO.unitHitRate = unitHitRate;
        battleParameterDTO.enemyHitRate = enemyHitRate;

        battleParameterDTO.unitWeapon = unitWeapon;
        battleParameterDTO.enemyWeapon = enemy.equipWeapon;

        
        
        //3すくみ補正を含めた攻撃力、命中率の補正
        battleParameterDTO.affinity = BattleWeaponAffinity.EQUAL;
        if (enemy.equipWeapon != null)
        {
            battleParameterDTO = getWeaponAffinity(battleParameterDTO);
        }

        ///必殺率計算
        ///必殺率=(技+幸運)/2+武器の必殺 - 敵の幸運
        unitCriticalRate = (unitDex + unitLuk) / 2 + unitWeapon.criticalRate - enemyLuk;

        //210226 スキル「蒐集家」反映
        if (SkillUtil.isCollector(unit)) {
            unitCriticalRate += 10;

        }

        //210227 スキル「必殺」
        if (unit.job.skills.Contains(Skill.必殺))
        {
            unitCriticalRate += 15;
            Debug.Log($"{unit.name}: スキル{Skill.必殺} 効果{Skill.必殺.GetStringValue()}");
        }

        //スキル「狂化」
        if (isUnitBerserk)
        {
            unitCriticalRate += 20;
            Debug.Log($"{unit.name}: スキル{Skill.狂化} 効果{Skill.狂化.GetStringValue()}");
        }
        


        unitCriticalRate = CorrectParameter(unitCriticalRate);
        battleParameterDTO.unitCriticalRate = unitCriticalRate;

        if (enemy.equipWeapon != null)
        {
            enemyCriticalRate = (enemyDex + enemyLuk) / 2 + enemy.equipWeapon.criticalRate - unitLuk;

            //敵の「蒐集家」判定
            if (SkillUtil.isCollector(enemy))
            {
                enemyCriticalRate += 10;
            }

            //210227 スキル「必殺」
            if (enemy.job.skills.Contains(Skill.必殺))
            {
                enemyCriticalRate += 15;
                Debug.Log($"{enemy.name}: スキル{Skill.必殺} 効果{Skill.必殺.GetStringValue()}");
            }

            if (isEnemyBerserk)
            {
                enemyCriticalRate += 20;
                Debug.Log($"{enemy.name}: スキル{Skill.狂化} 効果{Skill.狂化.GetStringValue()}");
            }

            enemyCriticalRate = CorrectParameter(enemyCriticalRate);
            battleParameterDTO.enemyCiritcalRate = enemyCriticalRate;

        }

        ///追撃の計算 追撃は攻速差4以上 攻速 = 速さ-｛武器の重さ-(力/5)}
        ///勇者武器は今はガン無視で
        int unitAttackSpeed = unitAgi - unitWeapon.delay;
        int enemyAttackSpeed = enemyAgi;

        if(enemy.equipWeapon != null)
        {
            enemyAttackSpeed -= enemy.equipWeapon.delay;
        }

        unitChaseFlag = false;
        enemyChaseFlag = false;

        //ユニットも敵も追撃可能という状況は有り得ないのでelse
        //210220 追撃不可武器を作成
        if ((unitAttackSpeed - enemyAttackSpeed) >= 4 && unitWeapon.isChaseInvalid == false)
        {
            unitChaseFlag = true;
        }
        else if((enemyAttackSpeed - unitAttackSpeed) >= 4 && enemy.equipWeapon.isChaseInvalid == false)
        {
            enemyChaseFlag = true;
        }

        //自分か敵が宵闇の守りを持っていれば互いに追撃不可
        if(unit.job.skills.Contains(Skill.宵闇の守り) || enemy.job.skills.Contains(Skill.宵闇の守り))
        {
            Debug.Log($"スキル{Skill.宵闇の守り}発動 : 互いに追撃不可");
            unitChaseFlag = false;
            enemyChaseFlag = false;
        }

        //スキル「切り返し」実装 一旦、宵闇の守りより優先度高い
        if(!isPlayerAttack && unit.job.skills.Contains(Skill.切り返し))
        {
            //敵から攻撃された時
            unitChaseFlag = true;
            Debug.Log($"{unit.name}: スキル{Skill.切り返し} 効果{Skill.切り返し.GetStringValue()}");
        }
        else if(isPlayerAttack && enemy.job.skills.Contains(Skill.切り返し))
        {
            //敵を攻撃した時
            enemyChaseFlag = true;
            Debug.Log($"{enemy.name}: スキル{Skill.切り返し} 効果{Skill.切り返し.GetStringValue()}");
        }

        



        battleParameterDTO.unitName = unit.name;
        battleParameterDTO.enemyName = enemy.name;
        battleParameterDTO.unitWeaponName = unitWeapon.name;

        //210220 雑に勇者フラグをDTOに入れる
        //自分から攻撃した場合のみ2回攻撃なので、そうでなければフラグを倒してしまう
        if(unitWeapon.isYuusha && isPlayerAttack)
        {
            battleParameterDTO.isUnitYuusha = true;
        }
        else
        {
            battleParameterDTO.isUnitYuusha = false;
        }
       
        if (enemy.equipWeapon != null)
        {
            battleParameterDTO.enemyWeaponName = enemy.equipWeapon.name;

            if (enemy.equipWeapon.isYuusha && !isPlayerAttack)
            {
                battleParameterDTO.isEnemyYuusha = true;
            }
            else
            {
                battleParameterDTO.isEnemyYuusha = false;
            }
        }

        battleParameterDTO.unitHp = unit.hp;
        battleParameterDTO.unitMaxHp = unitMaxHp;
        battleParameterDTO.enemyHp = enemy.hp;
        battleParameterDTO.enemyMaxHp = enemyMaxHp;

        battleParameterDTO.unitChaseFlag = unitChaseFlag;
        battleParameterDTO.enemyChaseFlag = enemyChaseFlag;

        

        Debug.Log("unitAttackSpeed" + unitAttackSpeed.ToString());
        Debug.Log("enemyAttackSpeed" + enemyAttackSpeed.ToString());

        return battleParameterDTO;
    }

    /// <summary>
    /// 210216 仲間を回復する値を求める 効果量のみ求めるので内容が少ない
    /// </summary>
    public HealParameterDTO CalculateHealParameter(Unit unit, Weapon healRod, Unit targetUnit)
    {
        HealParameterDTO healParameterDTO = new HealParameterDTO();

        JobStatusDto jobstatusDto = unit.job.statusDto;
        int unitMaxHp = unit.maxhp + jobstatusDto.jobHp;
        int unitLatk = unit.latk + jobstatusDto.jobLatk;

        //必要な値はHPと遠攻 不要な値は使用しないでバフを受ける
        StatusDto buffedStatus = new StatusDto(unitMaxHp, unitLatk, 0, 0, 0, 0, 0, 0);
        StatusCalculator statusCalculator = new StatusCalculator();

        //武器、装飾品、スキルのバフ反映 最大値を上回らないようにして返される
        buffedStatus = statusCalculator.GetBuffedStatus(buffedStatus, unit.name, unit.equipWeapon, unit.equipAccessory, unit.job.skills, false);
        unitMaxHp = buffedStatus.hp;
        unitLatk = buffedStatus.latk;


        //戦闘にも使用するので本クラスに値を設定しておく
        healAmount = unitLatk + healRod.attack;

        //210226 スキル「癒し手」
        if (unit.job.skills.Contains(Skill.癒し手))
        {
            healAmount += 10;
        }

        //表示用DTO
        healParameterDTO.unitName = unit.name;
        healParameterDTO.unitHp = unit.hp;
        healParameterDTO.unitMaxHp = unitMaxHp;

        //Attackと表記してしまっているが回復量
        healParameterDTO.healAmount = healAmount;
        healParameterDTO.unitHealRodName = healRod.name;

        //回復対象のステータスも設定する
        JobStatusDto TargetjobstatusDto = targetUnit.job.statusDto;
        healParameterDTO.targetName = targetUnit.name;
        healParameterDTO.targetHp = targetUnit.hp;
        healParameterDTO.targetMaxHp = statusCalculator.CalcHpBuff( targetUnit.maxhp + TargetjobstatusDto.jobHp, targetUnit.job.skills);

        


        return healParameterDTO;
    }

    /// <summary>
    /// 値がマイナスになったら0にする
    /// ついでに100%以上も100になおす
    /// </summary>
    /// <param name="parameter"></param>
    /// <returns></returns>
    private int CorrectParameter(int parameter)
    {
        parameter = (parameter < 0) ? 0 : parameter;
        parameter = (parameter > 100) ? 100 : parameter;
        return parameter;
    }

    //基礎経験値を求めるメソッド
    public int getBaseExp(int unitLv, int EnemyLv)
    {
        //基礎経験値は11 + 敵Lv - ユニットLv
        int baseExp = 11 + EnemyLv - unitLv;
        return (baseExp < 0) ? 1 : baseExp;
    }

    //撃破経験値を求めるメソッド
    public int getKnockDownExp(int unitLv, int EnemyLv)
    {
        int knockDownExp = (EnemyLv * 3) - (unitLv * 3) + 20;
        return (knockDownExp < 0) ? 0 : knockDownExp;
    }

    //武器の相性による命中率補正
    //TODO 熟練度による命中率攻撃力補正をここに入れる
    public BattleParameterDTO getWeaponAffinity(BattleParameterDTO battleParameterDTO )
    {
        //プレイヤー側がショットの時
        if(battleParameterDTO.unitWeapon.type == WeaponType.SHOT)
        {
            //相性有利
            if(battleParameterDTO.enemyWeapon.type == WeaponType.STRIKE)
            {
                //命中率、攻撃力に補正
                unitHitRate = CorrectParameter(unitHitRate + 15);
                battleParameterDTO.unitHitRate = unitHitRate;

                unitAttack = CorrectParameter(unitAttack + 1);
                battleParameterDTO.unitAttack = unitAttack;

                enemyHitRate = CorrectParameter(enemyHitRate - 15);
                battleParameterDTO.enemyHitRate = enemyHitRate;
                enemyAttack = CorrectParameter(enemyAttack - 1);
                battleParameterDTO.enemyAttack = enemyAttack;

                battleParameterDTO.affinity = BattleWeaponAffinity.ADVANTAGE;
                return battleParameterDTO;
            }
            //相性不利
            else if (battleParameterDTO.enemyWeapon.type == WeaponType.LASER)
            {
                //命中率、攻撃力に補正
                unitHitRate = CorrectParameter(unitHitRate - 15);
                battleParameterDTO.unitHitRate = unitHitRate;

                unitAttack = CorrectParameter(unitAttack - 1);
                battleParameterDTO.unitAttack = unitAttack;

                enemyHitRate = CorrectParameter(enemyHitRate + 15);
                battleParameterDTO.enemyHitRate = enemyHitRate;
                enemyAttack = CorrectParameter(enemyAttack + 1);
                battleParameterDTO.enemyAttack = enemyAttack;

                battleParameterDTO.affinity = BattleWeaponAffinity.DISADVANTAGE;

                return battleParameterDTO;
            }
            else
            {
                //相性互角なら補正無しだが、0～100%の間への訂正を行う
                unitHitRate = CorrectParameter(unitHitRate);
                battleParameterDTO.unitHitRate = unitHitRate;

                unitAttack = CorrectParameter(unitAttack);
                battleParameterDTO.unitAttack = unitAttack;

                enemyHitRate = CorrectParameter(enemyHitRate);
                battleParameterDTO.enemyHitRate = enemyHitRate;
                enemyAttack = CorrectParameter(enemyAttack);
                battleParameterDTO.enemyAttack = enemyAttack;
                return battleParameterDTO;
            }
        
        }else if (battleParameterDTO.unitWeapon.type == WeaponType.LASER)
        {
            //ユニットの武器がレーザー
            //敵がショットで相性有利の時
            if (battleParameterDTO.enemyWeapon.type == WeaponType.SHOT)
            {
                //命中率、攻撃力に補正
                unitHitRate = CorrectParameter(unitHitRate + 15);
                battleParameterDTO.unitHitRate = unitHitRate;

                unitAttack = CorrectParameter(unitAttack + 1);
                battleParameterDTO.unitAttack = unitAttack;

                enemyHitRate = CorrectParameter(enemyHitRate - 15);
                battleParameterDTO.enemyHitRate = enemyHitRate;
                enemyAttack = CorrectParameter(enemyAttack - 1);
                battleParameterDTO.enemyAttack = enemyAttack;

                battleParameterDTO.affinity = BattleWeaponAffinity.ADVANTAGE;

                return battleParameterDTO;
            }
            //敵が物理で相性不利
            else if (battleParameterDTO.enemyWeapon.type == WeaponType.STRIKE)
            {
                //命中率、攻撃力に補正
                unitHitRate = CorrectParameter(unitHitRate - 15);
                battleParameterDTO.unitHitRate = unitHitRate;

                unitAttack = CorrectParameter(unitAttack - 1);
                battleParameterDTO.unitAttack = unitAttack;

                enemyHitRate = CorrectParameter(enemyHitRate + 15);
                battleParameterDTO.enemyHitRate = enemyHitRate;
                enemyAttack = CorrectParameter(enemyAttack + 1);
                battleParameterDTO.enemyAttack = enemyAttack;

                battleParameterDTO.affinity = BattleWeaponAffinity.DISADVANTAGE;

                return battleParameterDTO;
            }
            else
            {
                //等倍の時
                unitHitRate = CorrectParameter(unitHitRate);
                battleParameterDTO.unitHitRate = unitHitRate;

                unitAttack = CorrectParameter(unitAttack);
                battleParameterDTO.unitAttack = unitAttack;

                enemyHitRate = CorrectParameter(enemyHitRate);
                battleParameterDTO.enemyHitRate = enemyHitRate;
                enemyAttack = CorrectParameter(enemyAttack);
                battleParameterDTO.enemyAttack = enemyAttack;
                return battleParameterDTO;
            }

        }
        else if (battleParameterDTO.unitWeapon.type == WeaponType.STRIKE)
        {
            //ユニットの武器が物理
            if (battleParameterDTO.enemyWeapon.type == WeaponType.LASER)
            {
                //命中率、攻撃力に補正
                unitHitRate = CorrectParameter(unitHitRate + 15);
                battleParameterDTO.unitHitRate = unitHitRate;

                unitAttack = CorrectParameter(unitAttack + 1);
                battleParameterDTO.unitAttack = unitAttack;

                enemyHitRate = CorrectParameter(enemyHitRate - 15);
                battleParameterDTO.enemyHitRate = enemyHitRate;
                enemyAttack = CorrectParameter(enemyAttack - 1);
                battleParameterDTO.enemyAttack = enemyAttack;

                battleParameterDTO.affinity = BattleWeaponAffinity.ADVANTAGE;
                return battleParameterDTO;
            }
            else if (battleParameterDTO.enemyWeapon.type == WeaponType.SHOT)
            {
                //命中率、攻撃力に補正
                unitHitRate = CorrectParameter(unitHitRate - 15);
                battleParameterDTO.unitHitRate = unitHitRate;

                unitAttack = CorrectParameter(unitAttack - 1);
                battleParameterDTO.unitAttack = unitAttack;

                enemyHitRate = CorrectParameter(enemyHitRate + 15);
                battleParameterDTO.enemyHitRate = enemyHitRate;
                enemyAttack = CorrectParameter(enemyAttack + 1);
                battleParameterDTO.enemyAttack = enemyAttack;

                battleParameterDTO.affinity = BattleWeaponAffinity.DISADVANTAGE;
                return battleParameterDTO;
            }
            else
            {
                unitHitRate = CorrectParameter(unitHitRate);
                battleParameterDTO.unitHitRate = unitHitRate;

                unitAttack = CorrectParameter(unitAttack);
                battleParameterDTO.unitAttack = unitAttack;

                enemyHitRate = CorrectParameter(enemyHitRate);
                battleParameterDTO.enemyHitRate = enemyHitRate;
                enemyAttack = CorrectParameter(enemyAttack);
                battleParameterDTO.enemyAttack = enemyAttack;
                return battleParameterDTO;
            }

        }

        //通常は有り得ない
        return battleParameterDTO;
            
    }

}
