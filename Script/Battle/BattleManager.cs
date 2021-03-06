using DG.Tweening.Plugins.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

/// <summary>
/// 210522 戦闘処理をまとめたクラス
/// </summary>
public class BattleManager : MonoBehaviour
{

    [SerializeField] GameObject partyWindow;
    [SerializeField] GameObject weaponWindow;
    [SerializeField] GameObject enemyWindow;
    [SerializeField] GameObject battleConfirmWindow;

    [SerializeField]
    GameObject battleMessageWindow;

    [SerializeField]
    GameObject battleView;

    [SerializeField]
    UnitOutlineWindow unitOutlineWindow;

    [SerializeField]
    EnemyOutlineWindow enemyOutlineWindow;

    [SerializeField]
    GameObject enemyWeaponWindow;

    [SerializeField]
    UnitController unitController;

    [SerializeField]
    EnemyController enemyController;

    [SerializeField]
    FadeInOutManager fadeInOutManager;

    [SerializeField]
    GameObject detailWindow;

    [SerializeField]
    Main_Map mainMap;

    WeaponDatabase weaponDatabase;

    EnemyDatabase enemyDatabase;

    BattleMapManager battleMapManager;

    EnemyAIManager enemyAIManager;

    EffectManager effectManager;

    BattleTalkManager battleTalkManager;

    EXPWindow expWindow;

    private float deltaTime;
    private float wait = 1f;

    //戦闘中フラグ
    bool isBattleMode;

    //レベルアップ中か
    bool isLvupEnd;

    //経験値ゲージ上昇中か
    bool isExpGaugeUpdateEnd;

    //ボタン押下待ちか 
    bool buttonWait;

    //210213 プレイヤーからの攻撃か
    bool isPlayerAttack;

    //選択したユニット名
    public string selectedUnitName { get; set; }

    //選択した敵名
    public string selectedEnemyName { get; set; }

    BattleCalculator battleCalculator;

    //勇者武器で1初殴ったか
    bool isYuushaAttacked;

    Unit unit;
    Enemy enemy;

    //回復対象を追加 ボタンから押した時に対象を振り向かせる為に保持が必要
    PlayerModel healTargetModel;

    //アイテムを取得した、武器が壊れた、技能レベルアップ等の戦闘後メッセージ
    List<string> messageList;

    //戦闘の状態(プレイヤー攻撃、追撃等) 主にUpdateメソッドの処理を変える
    public BattleState battleState { get; set; }

    //回復終了、戦闘終了後では後処理が違うので、保持しておく
    BattleState endDestination;

    //レベルアップ用の変数
    [SerializeField]
    LvUpManager lvUpManager;
    List<StatusType> lvUpList;

    //210523 戦闘中デバッグ用メッセージを表示するか
    bool battleTextVisible;

    void Start()
    {
        //武器、敵のデータベース取得
        weaponDatabase = Resources.Load<WeaponDatabase>("weaponDatabase");
        enemyDatabase = Resources.Load<EnemyDatabase>("enemyDatabase");

        battleCalculator = new BattleCalculator();
        messageList = new List<string>();

        //成長率、レベルアップ時の感想を読み込み
        lvUpManager.init();

        //battleStateを初期化状態に
        battleState = BattleState.INIT;

        //レベルアップ中、経験値ゲージ上昇中ではないのでtrue
        isLvupEnd = true;
        isExpGaugeUpdateEnd = true;
    }

    //初期化 別クラス処理の為、インスタンス取得
    public void Init(BattleMapManager battleMapManager, EffectManager effectManager, EnemyAIManager enemyAIManager,
        BattleTalkManager battleTalkManager, EXPWindow expWindow, bool battleTextVisible)
    {
        this.battleMapManager = battleMapManager;
        this.battleTalkManager = battleTalkManager;
        this.effectManager = effectManager;
        this.enemyAIManager = enemyAIManager;
        this.expWindow = expWindow;
        this.battleTextVisible = battleTextVisible;
    }

    //BattleMapManagerのUpdateから呼ばれ、Updateの処理を行う
    public void battleUpdate()
    {
        deltaTime += Time.deltaTime;

        if (isBattleMode)
        {
            battle();
        }
    }

    //battleStateの変更とログ出力
    private void SetBattleState(BattleState battleState)
    {
        this.battleState = battleState;
        Debug.Log("BattleState = " + battleState.ToString());
    }

    /// <summary>
    /// 戦闘中の処理 battleStateを変える事で戦闘のフローが進行する
    /// </summary>
    private void battle()
    {
        //決定ボタンを押すとボタン押下待ちを進める
        if (KeyConfigManager.GetKeyDown(KeyConfigType.SUBMIT) || Input.GetButtonDown("Submit"))
        {
            //レベルアップ中はボタンを押した処理を受け付けない
            if(!isLvupEnd)
            {
                Debug.Log("レベルアップ中は操作を受け付けません");
                return;
            }
            if (!isExpGaugeUpdateEnd)
            {
                Debug.Log("経験値ゲージ上昇中は操作を受け付けません");
                return;
            }

            buttonWait = false;

        }

        //戦闘開始時
        if (battleState == BattleState.INIT)
        {
            //ログ
            if (battleTextVisible)
            {
                string messageText = string.Format("戦闘開始");
                battleMessageWindow.GetComponent<BattleMessageWindow>().UpdateText(messageText);
            }
            

            //210220 勇者武器で殴ったフラグを倒す
            isYuushaAttacked = false;

            //プレイヤーからの攻撃の場合
            if (isPlayerAttack)
            {
                SetBattleState(BattleState.PLAYERATTACK);
            }
            else{
                //敵からの攻撃の場合
                SetBattleState(BattleState.ENEMYATTACK);
            }
        }

        else if (battleState == BattleState.HEAL_INIT)
        {
            //210216 回復開始
            //ログ
            if (battleTextVisible)
            {
                battleMessageWindow.GetComponent<BattleMessageWindow>().UpdateText("回復開始");
            }

            SetBattleState(BattleState.HEAL);
        }

        else if (battleState == BattleState.HEAL)
        {
            //回復実行
            Heal();

            //回復の経験値増加処理へ
            SetBattleState(BattleState.HEAL_RESULT);
        }
        //プレイヤーの攻撃
        else if (battleState == BattleState.PLAYERATTACK)
        {
            PlayerAttackUpdate();
        }
        else if (battleState == BattleState.ENEMYATTACK)
        {
            //敵の攻撃
            EnemyAttackUpdate();
        }

        else if (battleState == BattleState.PLAYERCHASE)
        {
            //プレイヤーの追撃
            PlayerChaseUpdate();
        }
        else if (battleState == BattleState.ENEMYCHASE)
        {
            //敵の追撃
            EnemyChaseUpdate();
        }
        //経験値の計算
        else if (battleState == BattleState.RESULT)
        {
            //戦闘終了 経験値増加処理へ
            Result();

        }
        else if (battleState == BattleState.HEAL_RESULT)
        {
            //回復の経験値増加処理
            HealResult();

        }
        else if (battleState == BattleState.LOSE)
        {
            //210521 敗北時の処理
            Lose();
        }
        else if (battleState == BattleState.EXPGAUGE)
        {
            //経験値上昇中
            ExpGaugeUpdate();
        }
        
        else if(battleState == BattleState.LVUP)
        {
            //レベルアップ中
            CommonLvup();
            
        }
        else if (battleState == BattleState.MESSAGE)
        {
            //メッセージ確認
            ConfirmMessage();
        }

        //戦闘終了
        else if (battleState == BattleState.END)
        {
            End();
        }
        //210216 回復終了 戦闘と全然違う
        else if (battleState == BattleState.HEAL_END)
        {
            HealEnd();
        }
    }

    //210213 戦闘開始前に本クラスにユニットと敵を設定
    private void BattleInit(Unit unit, Enemy enemy)
    {

        this.unit = unit;
        this.enemy = enemy;
    }

    /// <summary>
    /// プレイヤーの攻撃、敵の攻撃関連のメソッド
    /// </summary>

    //プレイヤーの攻撃ターン
    private void PlayerAttackUpdate()
    {
        if (deltaTime <= wait)
        {
            return;
        }

        deltaTime = 0;
        buttonWait = true;

        //プレイヤーの攻撃
        UnitAttack();

        //敵が死んだら終了
        if (mainMap.ActiveEnemy.enemy.IsDead())
        {
            battleState = BattleState.RESULT;
            Debug.Log("BattleState = " + battleState.ToString());
            return;
        }

        //プレイヤーからの攻撃の場合
        if (isPlayerAttack)
        {
            //2102勇者処理 武器が勇者武器で1回しか殴っていなければフラグを立ててもう一回殴る
            //勇者武器の追撃は自分から攻撃時のみ
            if (unit.equipWeapon.isYuusha && !isYuushaAttacked)
            {
                isYuushaAttacked = true;
                SetBattleState(BattleState.PLAYERATTACK);
                return;
            }
            else
            {
                //2回殴ったのでフラグを初期化して以下の処理へ
                isYuushaAttacked = false;
            }

            //敵が攻撃可能な場合は敵の攻撃へ
            if (battleCalculator.isEnemyAttackable)
            {
                SetBattleState(BattleState.ENEMYATTACK);
            }
            else
            {
                //敵が攻撃出来ず、プレイヤー追撃フラグが有ればプレイヤーの追撃
                if (battleCalculator.unitChaseFlag)
                {
                    SetBattleState(BattleState.PLAYERCHASE);
                }
                //プレイヤーが追撃出来ないなら戦闘終了
                else
                {
                    SetBattleState(BattleState.RESULT);
                }
            }

        }
        else
        {
            //敵からの攻撃後にプレイヤーが攻撃した場合、互いに攻撃可能である事が保証されているので簡略化
            //勇者武器の追撃は自分から攻撃時のみなので不要

            //プレイヤーに追撃フラグが有ればプレイヤーの追撃
            if (battleCalculator.unitChaseFlag)
            {
                SetBattleState(BattleState.PLAYERCHASE);
            }
            //敵の追撃フラグが有れば敵の追撃
            else if (battleCalculator.enemyChaseFlag)
            {
                SetBattleState(BattleState.ENEMYCHASE);
            }
            //それ以外なら戦闘終了
            else
            {
                SetBattleState(BattleState.RESULT);
            }
        }
    }

    //敵の攻撃ターン Updateメソッドから呼ばれる
    private void EnemyAttackUpdate()
    {
        if (deltaTime <= wait)
        {
            return;
        }

        deltaTime = 0;
        buttonWait = true;

        //敵の攻撃
        EnemyAttack();
        

        //敵の攻撃で敗北した場合
        if (mainMap.ActiveUnit.unit.isDead())
        {
            SetBattleState(BattleState.LOSE);
            return;
        }

        //210213 プレイヤーから攻撃して敵が反撃した場合、互いに攻撃可能であることが保証されているので簡略化
        if (isPlayerAttack)
        {
            //追撃フラグが有ればプレイヤーの追撃
            if (battleCalculator.unitChaseFlag)
            {
                SetBattleState(BattleState.PLAYERCHASE);

            }
            //敵の追撃フラグが有れば敵の追撃
            else if (battleCalculator.enemyChaseFlag)
            {
                SetBattleState(BattleState.ENEMYCHASE);
            }
            //それ以外なら戦闘終了
            else
            {
                SetBattleState(BattleState.RESULT);
            }
        }
        else
        {
            //敵から攻撃している場合
            //2102勇者処理 武器が勇者武器で1回しか殴っていなければもう一回殴る
            if (enemy.equipWeapon.isYuusha && !isYuushaAttacked)
            {
                isYuushaAttacked = true;
                SetBattleState(BattleState.ENEMYATTACK);
            }
            else
            {
                //2回殴ったのでフラグを初期化して以下の処理へ
                isYuushaAttacked = false;
            }

            //敵からの攻撃の場合は、まだプレイヤーが反撃出来ない可能性が有る
            if (battleCalculator.isUnitAttackable)
            {
                SetBattleState(BattleState.PLAYERATTACK);
            }
            else
            {
                //敵からの攻撃かつ、プレイヤーが反撃出来ない状態で敵が追撃出来る場合
                if (battleCalculator.enemyChaseFlag)
                {
                    SetBattleState(BattleState.ENEMYCHASE);

                }
                //敵が追撃出来ないなら戦闘終了
                else
                {
                    SetBattleState(BattleState.RESULT);
                }
            }
        }
    }

    //プレイヤーの追撃ターン Updateメソッドから呼ばれる
    private void PlayerChaseUpdate()
    {
        //プレイヤーの追撃の場合 その後、敵の追撃になる事は有り得ないので敵の追撃には遷移しない
        if (deltaTime <= wait)
        {
            return;
        }

        deltaTime = 0;
        buttonWait = true;

        //攻撃
        UnitAttack();

        //2102勇者処理 武器が勇者武器で1回しか殴っていない、かつ自分から攻撃していた場合もう一回殴る
        if (unit.equipWeapon.isYuusha && !isYuushaAttacked && isPlayerAttack)
        {
            isYuushaAttacked = true;
            SetBattleState(BattleState.PLAYERCHASE);
            return;
        }
        else
        {
            //2回殴ったのでフラグを初期化して以下の処理へ
            isYuushaAttacked = false;
        }

        SetBattleState(BattleState.RESULT);
    }

    //敵の追撃ターン Updateメソッドから呼ばれる
    private void EnemyChaseUpdate()
    {
        //敵の追撃の場合、その後プレイヤーの追撃になる事は無い
        if (deltaTime <= wait)
        {
            return;
        }

        deltaTime = 0;
        buttonWait = true;

        //敵の攻撃
        EnemyAttack();

        //2102勇者処理 武器が勇者武器で1回しか殴っていない、かつ敵から攻撃していた場合もう一回殴る
        if (enemy.equipWeapon.isYuusha && !isYuushaAttacked && !isPlayerAttack)
        {
            isYuushaAttacked = true;
            SetBattleState(BattleState.ENEMYCHASE);
            return;
        }
        else
        {
            //2回殴ったのでフラグを初期化して以下の処理へ
            isYuushaAttacked = false;
        }

        SetBattleState(BattleState.RESULT);
    }



    //プレイヤーの攻撃 追撃も同じ処理　変更を加える場合はEnemyAttackにも同じ変更をすること
    private void UnitAttack()
    {

        //210211 攻撃エフェクト表示　第一引数：エフェクト 第二引数：位置 第三引数 : 向き
        //TODO ユニットによってエフェクトを変える場合はここに処理追加
        GameObject effect = Instantiate(effectManager.slashEffectList[1], new Vector3(mainMap.ActiveEnemy.transform.position.x, mainMap.ActiveEnemy.transform.position.y + 1,
            mainMap.ActiveEnemy.transform.position.z), Quaternion.identity) as GameObject;

        //プレイヤーの攻撃 ウィンドウ表示時に計算して出した攻撃力を取得
        int unitAttack = battleCalculator.unitAttack;

        //命中判定
        float ran = Random.Range(0f, 100.0f);
        string messageText;

        //命中した場合
        if (ran <= battleCalculator.unitHitRate)
        {
            //命中したのでくらいモーション
            mainMap.ActiveEnemy.SetDamageMotion(true);

            //命中したので更にクリティカル判定
            ran = Random.Range(0f, 100.0f);
            if(ran <= battleCalculator.unitCriticalRate)
            {
                //クリティカル攻撃 ダメージ3倍
                //TODO ダメージ4倍スキルを実装する時はここを変更すること
                unitAttack *= 3;
                messageText = string.Format(unit.name + "の攻撃！必殺の一撃！" + enemy.name + "に{0}のダメージを与えた。", unitAttack);
            }
            else
            {
                //通常攻撃
                messageText = string.Format(unit.name + "の攻撃！" + enemy.name + "に{0}のダメージを与えた。", unitAttack);
            }

           

            //祈り発動可能かどうか HP1以上なら発動
            bool prayable = enemy.hp <= 1 ? true : false;

            //敵のHPにダメージ反映

            int actualUnitAttack = unitAttack;

            //スキル「魔力障壁」を持っている場合
            if (enemy.job.skills.Contains(Skill.魔力障壁))
            {
                //技×2 % で発動 ダメージを半減させる
                if (SkillUtil.isGuard(battleCalculator.enemyDex, Random.Range(0f, 100.0f)))
                {
                    //発動したら小数点以下切り捨てでダメージ半減
                    actualUnitAttack = Mathf.FloorToInt(actualUnitAttack / 2);
                }
                //不発なら何も起こらない
            }

            //スキル「運命予知」を持っている場合
            if (enemy.job.skills.Contains(Skill.運命予知))
            {
                //幸運%で発動。ダメージを半減させる
                if (SkillUtil.isDestinyGuard(battleCalculator.enemyLuk, Random.Range(0f, 100.0f)))
                {
                    //小数点以下切り捨てでダメージ半減
                    actualUnitAttack = Mathf.FloorToInt(actualUnitAttack / 2);
                }
                //不発なら何も起こらない
            }

            //210211ダメージの数字を表示
            GameObject damageText = Instantiate(effectManager.damageText, mainMap.ActiveEnemy.transform.position, Quaternion.identity) as GameObject;
            damageText.GetComponent<DamageText>().Init(actualUnitAttack.ToString());

            //プレイヤーのHPにダメージ反映
            enemy.ReceiveDamage(actualUnitAttack);

            //210226 敵が死んだタイミングで発動可能なら祈り判定
            if (prayable && enemy.IsDead() && enemy.job.skills.Contains(Skill.祈り))
            {
                //祈りが発動したら
                if (SkillUtil.isPray(battleCalculator.enemyLuk, ran))
                {
                    enemy.hp = 1;
                }
                //発生しなければそのままisDead判定で死ぬ
            }

            //210226 スキル「メンテナンス」実装 幸運×2％の確率で武器耐久数を減らさずに攻撃する
            if (unit.job.skills.Contains(Skill.メンテナンス))
            {

                if(SkillUtil.isMaintenance(battleCalculator.unitLuk, ran))
                {
                    //武器の消費が発生しないので何も無い
                }
                else
                {
                    unit.equipWeapon.endurance -= 1;
                }
            }
            else
            {
                unit.equipWeapon.endurance -= 1;
                
            }

            //武器の使用回数が1でも追撃は可能だが、使用回数0となる
            if (unit.equipWeapon.endurance <= 0)
            {
                //TODO 壊れる処理をここに追加
                unit.equipWeapon.endurance = 0;
            }

        }
        else {
            //ミス
            messageText = "ミス！ダメージを与えられない";

            //missの文字を表示
            GameObject damageText = Instantiate(effectManager.damageText, mainMap.ActiveEnemy.transform.position, Quaternion.identity) as GameObject;
            damageText.GetComponent<DamageText>().Init("miss");
        }

        //メッセージ更新
        if (battleTextVisible) battleMessageWindow.GetComponent<BattleMessageWindow>().UpdateText(messageText);
        
        battleView.GetComponent<BattleView>().UpdateEnemyHp(mainMap.ActiveEnemy.enemy.hp);
    }

    //敵の攻撃 攻撃、追撃同じ処理なのでメソッド化　UnitAttackと同じ処理をすること
    private void EnemyAttack()
    {
        GameObject effect = Instantiate(effectManager.slashEffectList[1],  new Vector3(mainMap.ActiveUnit.transform.position.x, mainMap.ActiveUnit.transform.position.y +1 , 
            mainMap.ActiveUnit.transform.position.z), Quaternion.identity) as GameObject;

        //敵の攻撃 計算して出した敵の攻撃力を取得
        int enemyAttack = battleCalculator.enemyAttack;

        //命中判定
        float ran = Random.Range(0f, 100.0f);
        string messageText;

        if (ran <= battleCalculator.enemyHitRate)
        {
            mainMap.ActiveUnit.SetDamageMotion(true);

            //命中した場合は更にクリティカル判定
            ran = Random.Range(0f, 100.0f);
            if (ran <= battleCalculator.enemyCriticalRate)
            {
                enemyAttack *= 3;

                messageText = string.Format(enemy.name + "の攻撃！必殺の一撃！" + unit.name + "に{0}のダメージを与えた。", enemyAttack);
                
            }
            else
            {

                //必殺にならなかった場合は通常攻撃
                messageText = string.Format(enemy.name + "の攻撃！" + unit.name + "に{0}のダメージを与えた。", enemyAttack);
                
            }


            //祈りが出来るかどうか
            bool prayable = true;

            if (unit.hp <= 1)
            {
                prayable = false;
            }

            //表示と違い、スキル等で半減されたりする値
            int actualEnemyAttack = enemyAttack;

            //スキル「魔力障壁」
            if (unit.job.skills.Contains(Skill.魔力障壁))
            {
                if (SkillUtil.isGuard(battleCalculator.unitDex, Random.Range(0f, 100.0f)))
                {
                    actualEnemyAttack =Mathf.FloorToInt(actualEnemyAttack / 2);
                }
                //不発なら特に半減しない
            }

            //運命予知
            if (unit.job.skills.Contains(Skill.運命予知))
            {
                if (SkillUtil.isDestinyGuard(battleCalculator.unitLuk, Random.Range(0f, 100.0f)))
                {
                    actualEnemyAttack = Mathf.FloorToInt(actualEnemyAttack / 2);
                }
                //不発なら特に半減しない
            }

            //210211ダメージの数字を表示
            GameObject damageText = Instantiate(effectManager.damageText, mainMap.ActiveUnit.transform.position, Quaternion.identity) as GameObject;
            damageText.GetComponent<DamageText>().Init(actualEnemyAttack.ToString());

            //プレイヤーのHPにダメージ反映
            unit.receiveDamage(actualEnemyAttack);

            //210226 敵が死んだタイミングで祈り発動
            if (prayable && unit.isDead() && unit.job.skills.Contains(Skill.祈り))
            {
                //祈りが発動したら
                if (SkillUtil.isPray(battleCalculator.unitLuk, Random.Range(0f, 100.0f)))
                {
                    unit.hp = 1;
                }
                //発生しなければそのままisDead判定で死ぬ
            }

        }
        else
        {
            //missの文字を表示
            GameObject damageText = Instantiate(effectManager.damageText, mainMap.ActiveUnit.transform.position, Quaternion.identity) as GameObject;
            damageText.GetComponent<DamageText>().Init("miss");

            //ミス
            messageText = string.Format(unit.name + "は攻撃をかわした！");

        }

        if (battleTextVisible) battleMessageWindow.GetComponent<BattleMessageWindow>().UpdateText(messageText);

        //UIに反映
        battleView.GetComponent<BattleView>().UpdatePlayerHp(unit.hp);
    }

    //210216 回復処理
    private void Heal()
    {
        //仲間を回復する
        if (deltaTime <= wait)
        {
            return;
        }
        deltaTime = 0;
        buttonWait = true;

        Unit healTarget = healTargetModel.unit;

        //210211 回復エフェクト表示　第一引数：エフェクト 第二引数：位置 第三引数 : 向き
        GameObject effect = Instantiate(effectManager.healEffectList[0], new Vector3(healTargetModel.transform.position.x, healTargetModel.transform.position.y + 1,
            healTargetModel.transform.position.z), Quaternion.identity) as GameObject;

        //プレイヤーの回復 ウィンドウ表示時に計算して出した回復量を取得
        int healAmount = battleCalculator.healAmount;

        string messageText;

        //回復実行
        messageText = $"{unit.name}は回復の符を使った！{healTarget.name}を{healAmount}回復した";
        if (battleTextVisible) battleMessageWindow.GetComponent<BattleMessageWindow>().UpdateText(messageText);

        //210211回復の数字を表示
        GameObject healText = Instantiate(effectManager.healText, healTargetModel.transform.position, Quaternion.identity) as GameObject;
        healText.GetComponent<DamageText>().Init(healAmount.ToString());

        //仲間のHPにダメージ反映
        healTarget.receiveHeal(healAmount);

        //UIに反映 数字変更とゲージ増加
        battleView.GetComponent<BattleView>().UpdateEnemyHp(healTarget.hp);

        //210219 使用回数を減らす
        unit.equipHealRod.endurance -= 1;
        if (unit.equipHealRod.endurance <= 0)
        {
            unit.equipHealRod.endurance = 0;
        }
    }

    /// <summary>
    /// プレイヤーの攻撃、敵の攻撃関連のメソッド ここまで
    /// </summary>


    /// <summary>
    /// 210228 戦闘後の後処理
    /// </summary>
    private void Result()
    {
        if (deltaTime <= wait)
        {
            return;
        }

        deltaTime = 0;
        buttonWait = true;

        //敵の消滅処理
        if (mainMap.ActiveEnemy.enemy != null)
        {

            if (mainMap.ActiveEnemy.enemy.IsDead())
            {

                //210521 ボス撃破時の会話を表示
                if (enemy.isBoss)
                {
                    //会話が存在すれば会話処理へ
                    if (battleTalkManager.IsBossDestroyTalkExist(StageSelectManager.selectedChapter))
                    {
                        battleMapManager.SetMapMode(MapMode.BATTLE_AFTER_TALK);
                        return;
                    }
                }

                //210228 敵のドロップアイテム追加
                if (enemy.hasDromItem)
                {
                    //メッセージ追加
                    messageList.Add($"{enemy.dropItem.ItemName}を手に入れた！");
                    Debug.Log($"{unit.name}はドロップアイテム{enemy.dropItem.ItemName} を手に入れた！");

                    //6個以上の時はスキマに送る
                    //TODO 今後スキマに送るアイテムを選択出来るようにしたい
                    if (unit.carryItem.Count >= 6)
                    {
                        ItemInventory.itemList.Add(enemy.dropItem);
                        messageList.Add($"{enemy.dropItem.ItemName}は\n手持ちが一杯なのでスキマに送りました。");
                        Debug.Log($"{enemy.dropItem.ItemName} は手持ちが一杯なのでスキマに入れた。");
                    }
                    else
                    {
                        //手持ちに空きが有る場合は普通に入手する
                        unit.carryItem.Add(enemy.dropItem);
                    }
                }

                //210221 敵を倒した時の攻撃可能範囲処理削除
                mainMap.RemoveWarningCells(mainMap.ActiveEnemy.enemyId);

                //210303 消滅エフェクトを追加
                Vector3 effectPos = new Vector3(mainMap.ActiveEnemy.transform.position.x, mainMap.ActiveEnemy.transform.position.y + 0.2f, mainMap.ActiveEnemy.transform.position.z);
                GameObject portal = Instantiate(effectManager.portalEffectList[0], effectPos, Quaternion.identity) as GameObject;
                //敵の削除処理を実行
                Destroy(mainMap.ActiveEnemy.gameObject);

            }
        }

        //スキルレベルの計算
        string skillLevelResult = SkillLevelUtil.CalculateSkillLevel(unit);

        //技能レベルアップの文章が返って来ればメッセージウィンドウで最後に表示する
        if (skillLevelResult != null)
        {
            messageList.Add(skillLevelResult);
        }

        //TODO 武器の故障を反映する

        int exp;
        //基礎経験値の計算
        exp = battleCalculator.getBaseExp(unit.lv, enemy.lv);

        //敵が死んでいたら撃破経験値の計算 その内ボス補正入れる
        if (enemy.IsDead())
        {
            exp += battleCalculator.getKnockDownExp(unit.lv, enemy.lv);
        }

        //ユニットに経験値を与える
        //210226 スキル反映
        if (unit.job.skills.Contains(Skill.努力家))
        {
            exp = Mathf.RoundToInt(exp * 1.2f);
            Debug.Log($"スキル{Skill.努力家} 経験値1.2倍 : {exp}");
        }

        string messageText = string.Format("{0}の経験値を獲得した。", exp);
        if (battleTextVisible) battleMessageWindow.GetComponent<BattleMessageWindow>().UpdateText(messageText);

        //経験値ゲージ初期化 第一引数は現在 第二は獲得経験値
        isExpGaugeUpdateEnd = false;
        expWindow.InitExpGauge(unit.exp, exp);
        expWindow.gameObject.SetActive(true);

        //ユニットの経験値加算
        unit.exp += exp;

        //通常のレベルアップと回復のレベルアップでは後処理が違うのでここで設定する
        endDestination = BattleState.END;

        SetBattleState(BattleState.EXPGAUGE);
    }

    //回復時の後処理
    private void HealResult()
    {
        if (deltaTime <= wait)
        {
            return;
        }

        deltaTime = 0;

        //210216回復時の後処理 経験値計算の違いや撃破、アイテムドロップが無い為作成
        buttonWait = true;

        //スキルレベルの計算
        string skillLevelResult = SkillLevelUtil.CalculateHealSkillLevel(unit);

        //スキルレベルアップの文章が返って来ればメッセージウィンドウで最後に表示する
        if (skillLevelResult != null)
        {
            messageList.Add(skillLevelResult);
        }

        int exp;
        //経験値の計算 20固定
        exp = 20;

        if (unit.job.skills.Contains(Skill.努力家))
        {
            exp = Mathf.RoundToInt(exp * 1.2f);
            Debug.Log($"スキル{Skill.努力家} 経験値1.2倍 : {exp}");
        }

        string messageText = string.Format("{0}の経験値を獲得した。", exp);
        if (battleTextVisible) battleMessageWindow.GetComponent<BattleMessageWindow>().UpdateText(messageText);

        //経験値ゲージ初期化 第一引数は現在 第二は獲得経験値
        isExpGaugeUpdateEnd = false;
        expWindow.InitExpGauge(unit.exp, exp);
        expWindow.gameObject.SetActive(true);

        //ユニットに経験値を与える
        unit.exp += exp;

        //通常のレベルアップと回復のレベルアップでは後処理が違うのでここで設定する
        endDestination = BattleState.HEAL_END;

        SetBattleState(BattleState.EXPGAUGE);
    }

    //210521 敗北時の処理
    private void Lose()
    {
        if (deltaTime <= wait)
        {
            return;
        }

        //deltaTime = 0;

        //敗北時の会話を表示
        Unit unit = mainMap.ActiveUnit.unit;
        if (battleTalkManager.IsLoseTalkExist(unit.pathName))
        {
            battleMapManager.SetMapMode(MapMode.BATTLE_AFTER_TALK);
            
            //会話終了後、Endでボタン押下待ちになるのを防ぐ
            buttonWait = false;
            
            return;
        }

        //ユニット離脱

        //主人公はいつでも敗北すればゲームオーバー
        if (unit.name == "霊夢" || unit.name == "レミリア")
        {
            battleMapManager.GameOver();
        }
        else
        {
            //消滅エフェクトを追加
            Vector3 effectPos = new Vector3(mainMap.ActiveUnit.transform.position.x, mainMap.ActiveUnit.transform.position.y + 0.2f, mainMap.ActiveUnit.transform.position.z);
            GameObject portal = Instantiate(effectManager.portalEffectList[0], effectPos, Quaternion.identity) as GameObject;

            //味方の削除処理を実行
            //TODO カジュアルモードの場合はユニットが消滅しないようにする
            UnitController.unitList.Remove(unit);
            Destroy(mainMap.ActiveUnit.gameObject);

            //敗北時はその後メッセージ、経験値ゲージ等が表示される事は無いので終了へ
            SetBattleState(BattleState.END);

        }
    }

    //経験値ゲージ増加処理
    private void ExpGaugeUpdate()
    {
        //処理をexpWindowに委ねる 終わるとtrueが返ってきて処理が進む
        isExpGaugeUpdateEnd = expWindow.ExpGaugeUpdate();
        if (!isExpGaugeUpdateEnd)
        {
            return;
        }

        //経験値ゲージ上昇後、ボタン押下待ち
        if (buttonWait)
        {
            Debug.Log("決定ボタンを押してください");
            return;
        }

        //レベルアップウィンドウを非表示に
        expWindow.gameObject.SetActive(false);

        //レベルアップ判定
        if (unit.exp >= 100)
        {
            Debug.Log("Lvup!");

            //LVアップ後の残り経験値
            unit.exp = unit.exp % 100;

            //戦闘画面を閉じる
            battleView.SetActive(false);

            //レベルアップウィンドウの初期化
            lvUpManager.InitLvupWindow(unit);

            //LvUpの下準備
            lvUpList = lvUpManager.lvup(unit.name, unit.exp);

            //フラグを設定 レベルアップ中は操作不可能フラグとボタン押下待ち
            isLvupEnd = false;
            buttonWait = true;

            //LV_UP
            SetBattleState(BattleState.LVUP);
        }
        else if (messageList.Count != 0)
        {
            //レベルアップしておらず、メッセージ(技能レベルアップ、アイテム獲得等)が存在する場合
            isLvupEnd = true;
            SetBattleState(BattleState.MESSAGE);
        }
        else
        {
            //メッセージが存在しない場合はそのまま戦闘終了
            isLvupEnd = true;
            SetBattleState(endDestination);
        }
    }

    //レベルアップ処理
    private void CommonLvup()
    {
        //時間の制御とかはLvUpManagerがやってくれる
        //レベルアップが完了したら感想を表示
        isLvupEnd = lvUpManager.UpdateLvupWindow(lvUpList, unit.name);

        //レベルアップ処理が終わっていなければ後続処理へ入らない
        if (!isLvupEnd)
        {
            return;
        }


        //ボタンを押されるまで脱出しない
        if (buttonWait)
        {
            Debug.Log("決定ボタンを押してください");
            return;
        }

        //LvUp後はButtonWait設定しない

        //ボタンが押されたら戦闘終了へ
        //レベルアップウィンドウと感想のウィンドウを閉じる
        lvUpManager.closeLvupWindow();

        //デバッグ用のウィンドウも閉じる
        if (battleTextVisible) battleMessageWindow.SetActive(false);

        //再度レベルアップした時の為、レベルアップ済みへ
        isLvupEnd = true;

        //メッセージ表示後の行き先設定
        if (messageList.Count != 0)
        {
            //メッセージ(技能レベルアップ、アイテム獲得等)が存在する場合
            isLvupEnd = true;
            SetBattleState(BattleState.MESSAGE);
            Debug.Log("battleState : " + battleState);
        }
        else
        {
            //メッセージが存在しない場合終了
            isLvupEnd = true;
            SetBattleState(endDestination);
        }

    }

    /// <summary>
    /// 戦闘後のアイテム獲得、武器故障、技能レベルアップ等を順番に表示する機能
    /// </summary>
    private void ConfirmMessage()
    {
        if (buttonWait)
        {
            return;
        }

        buttonWait = true;

        if (messageList.Count != 0)
        {
            //メッセージが存在すれば1つずつメッセージ表示
            battleMapManager.OpenMessageWindow(messageList[0]);
            messageList.RemoveAt(0);
        }
        else
        {
            //全てのメッセージを出し終えたらメッセージ閉じる
            battleMapManager.BatleCloseMessageWindow();

            //ボタン押下待ちを切る
            buttonWait = false;

            //Resultメソッドで設定したので戦闘ならEND、回復ならHEAL_ENDへモード変更
            SetBattleState(endDestination);
        }

    }

    //戦闘終了し、経験値取得、レベルアップ、メッセージ表示後の後処理
    private void End()
    {
        if (buttonWait || deltaTime <= wait)
        {
            return;
        }

        isBattleMode = false;

        if (battleTextVisible) battleMessageWindow.SetActive(false);
        battleView.SetActive(false);

        //武器が無限に増えて行ってしまうので削除
        foreach (Transform obj in weaponWindow.transform)
        {
            Destroy(obj.gameObject);
        }

        //ユニットを行動済みにしてMapModeをNORMALに戻す
        if (isPlayerAttack)
        {
            battleMapManager.Wait();
        }
        else
        {
            //敵ターンの場合
            //敵を行動済みに 死んでいる場合は何もしない
            enemyAIManager.BattleEnd();
        }
    }

    //回復の終了処理 戦闘と結構違う
    private void HealEnd()
    {
        if (buttonWait || deltaTime <= wait)
        {
            return;
        }

        //戦闘終了
        isBattleMode = false;

        if (battleTextVisible) battleMessageWindow.SetActive(false);
        battleView.SetActive(false);

        //武器が無限に増えて行ってしまうので削除
        foreach (Transform obj in weaponWindow.transform)
        {
            Destroy(obj.gameObject);
        }

        //ユニットを行動済みにする
        //210213 SetBeforeActionはユニット周囲の行動前エフェクト
        battleMapManager.Wait();
    }

    /// <summary>
    /// 戦闘開始前の処理メソッド
    /// </summary>
    /// 
    //200814 自分ターンに敵を選択した時、敵AIの攻撃時に呼んで、戦闘ウィンドウを初期化する
    public void MapOpenBattleView(PlayerModel playerModel, EnemyModel enemyModel, bool isPlayerAttack)
    {
        //プレイヤーからの攻撃か否かを設定
        this.isPlayerAttack = isPlayerAttack;

        Unit unit = playerModel.unit;
        Enemy enemy = enemyModel.enemy;

        //210219 戦闘に地形効果を追加
        //ユニット、敵のセル情報を取得する
        Main_Cell unitCell = mainMap.Cells.FirstOrDefault(c => c.X == playerModel.x && c.Y == playerModel.z);
        Main_Cell enemyCell = mainMap.Cells.FirstOrDefault(c => c.X == enemyModel.x && c.Y == enemyModel.y);

        //通常は有り得ない
        if (unitCell == null)
        {
            Debug.Log($"ERROR:ユニットの座標のセル情報が有りません X:{playerModel.x},Y:{playerModel.z}");
        }
        if (enemyCell == null)
        {
            Debug.Log($"ERROR:敵の座標のセル情報が有りません X:{enemyModel.x},Y:{enemyModel.y}");
        }

        //戦闘に使用する数値の計算を実施してUIに表示
        var battleParameterDTO = battleCalculator.CalculateBattleParameter(unit, unit.equipWeapon, unitCell, enemy, enemyCell, isPlayerAttack);
        battleView.GetComponent<BattleView>().UpdateText(battleParameterDTO);

        //戦闘UI表示
        battleView.SetActive(true);

        //本クラスに戦闘するプレイヤーと敵を設定
        BattleInit(unit, enemy);

        //プレイヤー攻撃時のみ確認ウィンドウを表示してフォーカス
        if (isPlayerAttack)
        {
            //第二引数は攻撃ならtrue
            battleConfirmWindow.GetComponent<BattleConfirmWindow>().UpdateConfirmtext("戦闘開始して良いですか？", true);
            battleConfirmWindow.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(battleConfirmWindow.transform.Find("ButtonLayout/StartButton").gameObject);
        }
        else
        {
            //敵から攻撃された時、武器の回数が減らない場合があるバグ対応
            foreach(Item item in unit.carryItem)
            {
                if(item.ItemType == ItemType.WEAPON && item.isEquip)
                {
                    item.weapon = unit.equipWeapon;
                }
            }
            //敵の攻撃時は確認せずに戦闘開始
            battleConfirmWindow.SetActive(false);
            BattleStart();
        }

    }

    /// <summary>
    /// 戦闘後の後処理 ここまで
    /// </summary>

    /// <summary>
    /// 戦闘開始前のUI制御系メソッド
    /// </summary>
    //210216仲間を回復する前のUI表示
    public void MapOpenHealView(Unit unit, PlayerModel selectedUnit)
    {
        //回復に使用する数値の計算を実施してUIに表示 必殺率、命中率、相手の攻撃力等が存在しない版
        HealParameterDTO healParameterDTO = battleCalculator.CalculateHealParameter(unit, unit.equipHealRod, selectedUnit.unit);
        battleView.GetComponent<BattleView>().HealUpdateText(healParameterDTO);

        //戦闘UI表示
        battleView.SetActive(true);

        //本クラスに戦闘するプレイヤーと敵を設定
        this.unit = unit;
        healTargetModel = selectedUnit;

        battleConfirmWindow.GetComponent<BattleConfirmWindow>().UpdateConfirmtext("回復して良いですか？",false);
        battleConfirmWindow.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(battleConfirmWindow.transform.Find("ButtonLayout/StartButton").gameObject);

    }

    //戦闘開始確認ボタンから呼ばれ、戦闘の初期化を行う
    public void BattleStart()
    {
        //UIを非表示に
        battleConfirmWindow.SetActive(false);
        if (battleTextVisible) battleMessageWindow.SetActive(true);
        isBattleMode = true;

        //戦うタイミングで敵はプレイヤーの方を向く
        Vector3 vector = new Vector3(mainMap.ActiveUnit.transform.position.x, mainMap.ActiveEnemy.transform.position.y,
            mainMap.ActiveUnit.transform.position.z);
        mainMap.ActiveEnemy.transform.DOLookAt(vector, 0.1f);

        vector = new Vector3(mainMap.ActiveEnemy.transform.position.x, mainMap.ActiveUnit.transform.position.y,
            mainMap.ActiveEnemy.transform.position.z);
        mainMap.ActiveUnit.transform.DOLookAt(vector, 0.1f);


        //210211 モードをBATTLEにする事で本クラスのUpdateに処理が入る
        SetBattleState(BattleState.INIT);

        //210520 ここで戦闘前会話の判定
        if (mainMap.ActiveEnemy.enemy.isBoss)
        {
            //ボスの場合、かつ戦闘前会話が存在すればモード変更
            if(battleTalkManager.IsBattleStartTalkExist(StageSelectManager.selectedChapter, mainMap.ActiveUnit.unit.pathName))
            {
                battleMapManager.SetMapMode(MapMode.BATTLE_BEFORE_TALK);
                return;
            }
            
        }
        battleMapManager.SetMapMode(MapMode.BATTLE);
    }
    //回復開始の時、確認ボタンから呼ばれる

    public void HealStart()
    {
        //UIを非表示に
        battleConfirmWindow.SetActive(false);
        if (battleTextVisible) battleMessageWindow.SetActive(true);
        isBattleMode = true;

        //回復対象をプレイヤーの方向に向かせる
        Vector3 vector = new Vector3(mainMap.ActiveUnit.transform.position.x, healTargetModel.transform.position.y,
            mainMap.ActiveUnit.transform.position.z);
        healTargetModel.transform.DOLookAt(vector, 0.1f);

        //210211 モードをBATTLEにする事で本クラスのUpdateに処理が入る
        battleMapManager.SetMapMode(MapMode.BATTLE);

        //回復専用の処理へ
        SetBattleState(BattleState.HEAL_INIT);
    }


    /// <summary>
    /// その他UI制御メソッド
    /// </summary>

    //敵を攻撃するユニットの武器ウィンドウを開く BattleMapManagerからも呼ばれる
    //TODO 210211 武器が5個よりも少ない場合は空欄を作る？
    public void openWeaponWindow(string unitName, MapMode mapMode)
    {
        unit = unitController.findByName(unitName);
        if (unit == null)
        {
            Debug.Log("ERROR: ユニットが存在しません");
            return;
        }
        //ユニット名からユニットの持っている武器を取得
        var itemList = unit.carryItem;

        var weaponList = new List<Weapon>();

        //最初の要素にフォーカスする用
        bool isfocused = false;

        //武器リストのUIを表示する
        weaponWindow.SetActive(true);
        detailWindow.SetActive(true);

        //手持ちアイテムから武器リストを作成する
        foreach (var item in itemList)
        {
            //武器のみ選択
            if (item.ItemType == ItemType.WEAPON)
            {
                Weapon weapon = item.weapon;


                if (weapon == null)
                {
                    Debug.Log("ERROR: 武器が存在しません");
                    return;
                }

                //Resources配下からボタンをロード
                var weaponButton = (Instantiate(Resources.Load("Prefabs/MapWeaponButton")) as GameObject).transform;
                weaponButton.GetComponent<MapWeaponButton>().Init(weapon, this, battleMapManager, mainMap.ActiveUnit.unit);
                weaponButton.name = weaponButton.name.Replace("(Clone)", "");
                //partyWindowオブジェクト配下にprefab作成
                weaponButton.transform.SetParent(weaponWindow.transform);

                //210217 警告を取得してボタンに設定 何も無ければNONEが返ってくる
                WeaponEquipWarn warn = WeaponEquipWarnUtil.GetWeaponEquipWarn(weapon, unit);
                if (warn != WeaponEquipWarn.NONE)
                {
                    weaponButton.GetComponent<MapWeaponButton>().setWarn(warn);
                    Debug.Log($"WARN:装備出来ない武器が有ります: 武器:{weapon.name}, 警告:{warn.GetStringValue()}");
                }

                //武器選択の場合は回復系ボタンを不活性に
                if (mapMode == MapMode.WEAPON_SELECT)
                {
                    //回復系の場合はボタンを不活性に
                    if (weapon.type == WeaponType.HEAL)
                    {
                        weaponButton.GetComponent<MapWeaponButton>().setButtonDisinteractable();
                    }
                    else
                    {
                        if (!isfocused)
                        {
                            isfocused = true;
                            //最初の要素のみフォーカス設定
                            EventSystem.current.SetSelectedGameObject(weaponButton.gameObject);
                        }

                        if (item.isEquip)
                        {
                            //装備している武器ならフォーカス上書き
                            EventSystem.current.SetSelectedGameObject(weaponButton.gameObject);
                        }
                    }
                }
                else if (mapMode == MapMode.HEAL_SELECT)
                {
                    //回復系選択の場合はそれ以外のボタンを不活性に
                    if (weapon.type != WeaponType.HEAL)
                    {
                        weaponButton.GetComponent<MapWeaponButton>().setButtonDisinteractable();
                    }
                    else
                    {
                        if (!isfocused)
                        {
                            isfocused = true;
                            //フォーカス設定
                            EventSystem.current.SetSelectedGameObject(weaponButton.gameObject);
                        }

                    }
                }
            }
        }
    }

    //武器ウィンドウを閉じる
    public void CloseWeaponWindow()
    {
        weaponWindow.SetActive(false);
        detailWindow.SetActive(false);
    }

    //無限に増えてしまわないように武器ボタンを削除する 武器ボタン表示時にキャンセルボタンを押すと呼ばれる
    public void DeleteButtleWeaponButton()
    {
        GameObject[] g = GameObject.FindGameObjectsWithTag("BattleWeaponButton");
        foreach (var obj in g)
        {
            Destroy(obj);
        }
    }

    //敵を攻撃する武器を選択する
    public void setEquipWeapon(string weapon)
    {
        unit.equipWeapon = weaponDatabase.FindByName(weapon);
    }

    //武器ボタンを選択した時にテキストを変える
    public void changeWeaponDetailWindow(string weaponName)
    {
        Weapon weapon = weaponDatabase.FindByName(weaponName);

        detailWindow.GetComponent<DetailWindow>().UpdateBattleWeaponText(weapon);
    }



    //200814 マップ版のBattleViewが表示されている時キャンセルボタンを押すと戻る
    public void MapCloseBattleView()
    {
        battleMapManager.deltaTime = 0;

        Debug.Log("MapMode : " + battleMapManager.mapMode);
        
        battleView.SetActive(false);
        battleConfirmWindow.SetActive(false);
    }


    /// <summary>
    /// テスト用の古いメソッド
    /// </summary>
    /// <param name="unitName"></param>
    //BattleUnitButtonを選択したタイミングで文字を変更する
    public void changeUnitOutlineWindow(string unitName)
    {
        Unit forcusUnit = unitController.findByName(unitName);
        unitOutlineWindow.UpdateText(forcusUnit);

    }

    //EnemyUnitButtonを選択したタイミングで文字を変更する
    public void changeEnemyOutlineWindow(string unitName)
    {
        Enemy forcusEnemy = enemyDatabase.FindByName(unitName);
        enemyOutlineWindow.UpdateText(forcusEnemy);
        if (forcusEnemy.equipWeapon != null)
        {
            enemyWeaponWindow.GetComponent<DetailWindow>().UpdateBattleWeaponText(forcusEnemy.equipWeapon);
        }

    }
}
