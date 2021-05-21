using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 200726 ユニットのステータスを表示するウィンドウ
/// </summary>
public class StatusWindow : MonoBehaviour
{
    [SerializeField] private Text name;
    [SerializeField] private Text lvNum;

    [SerializeField] private Text race;

    [SerializeField] private Text job;

    [SerializeField] private Text exp;

    [SerializeField] private Slider expGauge;

    //移動
    [SerializeField] private Text move;

    //HP
    [SerializeField] private Text hp;
    [SerializeField] private Slider hpGauge;
    [SerializeField] private Slider hpBuffGauge;

    //遠距離攻撃
    [SerializeField] private Text latk;
    [SerializeField] private Slider latkGauge;
    [SerializeField] private Slider latkBuffGauge;

    //近距離攻撃
    [SerializeField] private Text catk;
    [SerializeField] private Slider catkGauge;
    [SerializeField] private Slider catkBuffGauge;

    //速さ
    [SerializeField] private Text agi;
    [SerializeField] private Slider agiGauge;
    [SerializeField] private Slider agiBuffGauge;

    //技
    [SerializeField] private Text dex;
    [SerializeField] private Slider dexGauge;
    [SerializeField] private Slider dexBuffGauge;

    //幸運
    [SerializeField] private Text luk;
    [SerializeField] private Slider lukGauge;
    [SerializeField] private Slider lukBuffGauge;

    //遠距離防御
    [SerializeField] private Text ldef;
    [SerializeField] private Slider ldefGauge;
    [SerializeField] private Slider ldefBuffGauge;

    //近距離防御
    [SerializeField] private Text cdef;
    [SerializeField] private Slider cdefGauge;
    [SerializeField] private Slider cdefBuffGauge;

    //200827 スキルレベル
    [SerializeField] private Text shotLevel;
    [SerializeField] private Text laserLevel;
    [SerializeField] private Text strikeLevel;
    [SerializeField] private Text healLevel;

    //210218 技能経験値の追加でゲージの表示非表示とそれぞれのゲージ追加
    [SerializeField] private GameObject shotGaugeFrame;
    [SerializeField] private GameObject laserGaugeFrame;
    [SerializeField] private GameObject strikeGaugeFrame;
    [SerializeField] private GameObject healGaugeFrame;

    [SerializeField] private Slider shotGauge;
    [SerializeField] private Slider laserGauge;
    [SerializeField] private Slider strikeGauge;
    [SerializeField] private Slider healGauge;

    //200822 ユニット画像
    [SerializeField] private Image statusImage;

    //200825 装備品リスト
    [SerializeField] private GameObject equipList;

    //道具リスト
    [SerializeField] private GameObject inventoryList;

    //スキルリスト
    [SerializeField] private GameObject skillList;

    //バフが掛かっていれば文字の色を変更
    Color highLightColor = new Color(147.0f / 255.0f, 241.0f / 255.0f, 129.0f / 255.0f, 255.0f / 255.0f);

    //テキスト更新
    //200719 職業のステータスを反映
    public void updateText(Unit unit)
    {
        //職業補正を取得
        JobStatusDto jobStatusDto = unit.job.statusDto;

        //名前、レベル、職業、移動力
        this.name.text = unit.name;
        this.race.text = unit.race.GetStringValue();
        this.lvNum.text = unit.lv.ToString();
        this.job.text = unit.job.jobName.ToString();
        this.exp.text = unit.exp.ToString();
        this.move.text = unit.job.move.ToString();

        //210226移動力計算と文字の色変更
        StatusCalculator statusCalculator = new StatusCalculator();

        int move = statusCalculator.calcMove(unit);
        this.move.text = move.ToString();

        //経験値ゲージ
        this.expGauge.maxValue = 100;
        this.expGauge.value = unit.exp;

        //ステータス ユニット基礎値と職業補正の合算を表示する
        int hp = unit.maxhp + jobStatusDto.jobHp;
        int latk = unit.latk + jobStatusDto.jobLatk;
        int catk = unit.catk + jobStatusDto.jobCatk;
        int agi = unit.agi + jobStatusDto.jobAgi;
        int dex = unit.dex + jobStatusDto.jobDex;
        int luk = unit.luk + jobStatusDto.jobLuk;
        int ldef = unit.ldef + jobStatusDto.jobLdef;
        int cdef = unit.cdef + jobStatusDto.jobCdef;

        //キャラの画像を取得
        statusImage.enabled = true;
        this.statusImage.sprite = Resources.Load<Sprite>("Image/Charactors/" + unit.pathName + "/status");

        //武器レベルを反映
        this.shotLevel.text = unit.shotLevel.GetStringValue();
        this.laserLevel.text = unit.laserLevel.GetStringValue();
        this.strikeLevel.text = unit.strikeLevel.GetStringValue();
        this.healLevel.text = unit.healLevel.GetStringValue();

        //210218 やっと技能経験値を表示するように設定した
        shotGauge.value = unit.shotExp;
        laserGauge.value = unit.laserExp;
        strikeGauge.value = unit.strikeExp;
        healGauge.value = unit.healExp;

        //ゲージの必要経験値(maxValue)をスキルレベルより設定する
        shotGauge.maxValue = unit.shotLevel.GetIntValue();
        laserGauge.maxValue = unit.laserLevel.GetIntValue();
        strikeGauge.maxValue = unit.strikeLevel.GetIntValue();
        healGauge.maxValue = unit.healLevel.GetIntValue();

        

        if (move != unit.job.move)
        {
            //バフが掛かってたら色変更
            this.move.color = highLightColor;
        }
        else
        {
            this.move.color = Color.white;
        }

        //スキルレベルがNONEかSの場合はゲージ自体を表示しない
        if (unit.shotLevel == SkillLevel.NONE || unit.shotLevel == SkillLevel.S)
        {
            shotGaugeFrame.SetActive(false);
        }
        else
        {
            shotGaugeFrame.SetActive(true);
        }
        if (unit.laserLevel == SkillLevel.NONE || unit.laserLevel == SkillLevel.S)
        {
            laserGaugeFrame.SetActive(false);
        }
        else
        {
            laserGaugeFrame.SetActive(true);
        }
        if (unit.strikeLevel == SkillLevel.NONE || unit.strikeLevel == SkillLevel.S)
        {
            strikeGaugeFrame.SetActive(false);
        }
        else
        {
            strikeGaugeFrame.SetActive(true);
        }
        if (unit.healLevel == SkillLevel.NONE || unit.healLevel == SkillLevel.S)
        {
            healGaugeFrame.SetActive(false);
        }
        else
        {
            healGaugeFrame.SetActive(true);
        }

        //210218 武器、装飾品による増加分を反映する スキルのバフもいずれ実装
        StatusDto normalStatus = new StatusDto(hp, latk, catk, agi, dex, ldef, cdef, luk);

        //武器、装飾品、スキルのバフ反映 最大値を上回らないようにして返される
        bool isberserk = false;
        if (unit.hp != statusCalculator.CalcHpBuff(hp, unit.job.skills) && unit.job.skills.Contains(Skill.狂化))
        {
            isberserk = true;
        }
        StatusDto buffedStatus = statusCalculator.GetBuffedStatus(normalStatus, unit.name, unit.equipWeapon, unit.equipAccessory, unit.job.skills, isberserk);

        GetStatus(normalStatus, buffedStatus);
    }

    //ステータス表示(敵)
    public void updateText(Enemy enemy)
    {
        //職業補正を取得
        JobStatusDto jobStatusDto = enemy.job.statusDto;

        //名前、レベル、職業、移動力
        this.name.text = enemy.name;
        this.race.text = enemy.race.GetStringValue();
        this.lvNum.text = enemy.lv.ToString();
        this.job.text = enemy.job.jobName.ToString();
        this.exp.text = "-";    //敵に経験値やレベルアップは存在しない
        this.move.text = enemy.job.move.ToString();

        //210226移動力計算と文字の色変更
        StatusCalculator statusCalculator = new StatusCalculator();

        int move = statusCalculator.calcMove(enemy);
        this.move.text = move.ToString();

        //経験値ゲージ
        this.expGauge.maxValue = 100;
        this.expGauge.value = 0;    //敵に経験値やレベルアップは存在しない

        //ステータス ユニット基礎値と職業補正の合算を表示する
        int hp = enemy.maxhp + jobStatusDto.jobHp;
        int latk = enemy.latk + jobStatusDto.jobLatk;
        int catk = enemy.catk + jobStatusDto.jobCatk;
        int agi = enemy.agi + jobStatusDto.jobAgi;
        int dex = enemy.dex + jobStatusDto.jobDex;
        int luk = enemy.luk + jobStatusDto.jobLuk;
        int ldef = enemy.ldef + jobStatusDto.jobLdef;
        int cdef = enemy.cdef + jobStatusDto.jobCdef;

        //キャラの画像を取得
        if (enemy.isBoss)
        {
            statusImage.enabled = true;
            this.statusImage.sprite = Resources.Load<Sprite>("Image/Charactors/" + enemy.pathName + "/status");
        }
        else
        {
            statusImage.enabled = false;
        }        

        //武器レベルを反映
        this.shotLevel.text = enemy.shotLevel.GetStringValue();
        this.laserLevel.text = enemy.laserLevel.GetStringValue();
        this.strikeLevel.text = enemy.strikeLevel.GetStringValue();
        this.healLevel.text = enemy.healLevel.GetStringValue();

        //敵に技能経験値は存在しないので、ゲージ自体非表示
        shotGaugeFrame.SetActive(false);
        laserGaugeFrame.SetActive(false);
        strikeGaugeFrame.SetActive(false);
        healGaugeFrame.SetActive(false);

        if (move != enemy.job.move)
        {
            //バフが掛かってたら色変更
            this.move.color = highLightColor;
        }
        else
        {
            this.move.color = Color.white;
        }

        
        //210218 武器、装飾品による増加分を反映する スキルのバフもいずれ実装
        StatusDto normalStatus = new StatusDto(hp, latk, catk, agi, dex, ldef, cdef, luk);

        //武器、装飾品、スキルのバフ反映 最大値を上回らないようにして返される
        bool isberserk = false;
        if (enemy.hp != statusCalculator.CalcHpBuff(hp, enemy.job.skills) && enemy.job.skills.Contains(Skill.狂化))
        {
            isberserk = true;
        }
        StatusDto buffedStatus = statusCalculator.GetBuffedStatus(normalStatus, enemy.name, enemy.equipWeapon, enemy.equipAccessory, enemy.job.skills, isberserk);

        GetStatus(normalStatus, buffedStatus);
    }

    //ステータスのバフを反映
    private void GetStatus(StatusDto normalStatus, StatusDto buffedStatus)
    {
        //ステータスゲージ　バフ前の値
        this.hpGauge.maxValue = StatusConst.HP_MAX;
        this.hpGauge.value = normalStatus.hp;

        this.latkGauge.maxValue = StatusConst.LATK_MAX;
        this.latkGauge.value = normalStatus.latk;

        this.catkGauge.maxValue = StatusConst.CATK_MAX;
        this.catkGauge.value = normalStatus.catk;

        this.agiGauge.maxValue = StatusConst.AGI_MAX;
        this.agiGauge.value = normalStatus.agi;

        this.dexGauge.maxValue = StatusConst.DEX_MAX;
        this.dexGauge.value = normalStatus.dex;

        this.ldefGauge.maxValue = StatusConst.LDEF_MAX;
        this.ldefGauge.value = normalStatus.ldef;

        this.cdefGauge.maxValue = StatusConst.CDEF_MAX;
        this.cdefGauge.value = normalStatus.cdef;

        this.lukGauge.maxValue = StatusConst.LUK_MAX;
        this.lukGauge.value = normalStatus.luk;

        //ステータスの数値 バフ込の値
        this.hp.text = buffedStatus.hp.ToString();
        this.latk.text = buffedStatus.latk.ToString();
        this.catk.text = buffedStatus.catk.ToString();
        this.agi.text = buffedStatus.agi.ToString();
        this.dex.text = buffedStatus.dex.ToString();
        this.luk.text = buffedStatus.luk.ToString();
        this.ldef.text = buffedStatus.ldef.ToString();
        this.cdef.text = buffedStatus.cdef.ToString();

        //補正前と補正後の数字が違ったら数字の色を変える
        if (normalStatus.hp < buffedStatus.hp)
        {
            this.hp.color = highLightColor;
        }
        else
        {
            this.hp.color = Color.white;
        }
        if (normalStatus.latk < buffedStatus.latk)
        {
            this.latk.color = highLightColor;
        }
        else
        {
            this.latk.color = Color.white;
        }
        if (normalStatus.catk < buffedStatus.catk)
        {
            this.catk.color = highLightColor;
        }
        else
        {
            this.catk.color = Color.white;
        }
        if (normalStatus.agi < buffedStatus.agi)
        {
            this.agi.color = highLightColor;
        }
        else
        {
            this.agi.color = Color.white;
        }
        if (normalStatus.dex < buffedStatus.dex)
        {
            this.dex.color = highLightColor;
        }
        else
        {
            this.dex.color = Color.white;
        }
        if (normalStatus.luk < buffedStatus.luk)
        {
            this.luk.color = highLightColor;
        }
        else
        {
            this.luk.color = Color.white;
        }
        if (normalStatus.ldef < buffedStatus.ldef)
        {
            this.ldef.color = highLightColor;
        }
        else
        {
            this.ldef.color = Color.white;
        }
        if (normalStatus.cdef < buffedStatus.cdef)
        {
            this.cdef.color = highLightColor;
        }
        else
        {
            this.cdef.color = Color.white;
        }

        //バフのグラフを表示
        hpBuffGauge.maxValue = StatusConst.HP_MAX;
        hpBuffGauge.value = buffedStatus.hp;

        latkBuffGauge.maxValue = StatusConst.LATK_MAX;
        latkBuffGauge.value = buffedStatus.latk;

        catkBuffGauge.maxValue = StatusConst.CATK_MAX;
        catkBuffGauge.value = buffedStatus.catk;

        agiBuffGauge.maxValue = StatusConst.AGI_MAX;
        agiBuffGauge.value = buffedStatus.agi;

        dexBuffGauge.maxValue = StatusConst.DEX_MAX;
        dexBuffGauge.value = buffedStatus.dex;

        ldefBuffGauge.maxValue = StatusConst.LDEF_MAX;
        ldefBuffGauge.value = buffedStatus.ldef;

        cdefBuffGauge.maxValue = StatusConst.CDEF_MAX;
        cdefBuffGauge.value = buffedStatus.cdef;

        lukBuffGauge.maxValue = StatusConst.LUK_MAX;
        lukBuffGauge.value = buffedStatus.luk;
    }

    //200726 手持ちの道具一覧を表示する
    public void addInventoryList(List<Item> itemList,StatusManager statusManager, BattleMapManager battleMapManager)
    {
        int index = 0;
        foreach (var item in itemList)
        {
            //210219 耐久力実装の為Itemクラスに保持したweaponを取得
            //Weapon weapon = weaponDatabase.FindByName(item.ItemName);
            //Resources配下からボタンをロード
            var itemButton = (Instantiate(Resources.Load("Prefabs/StatusItemButton")) as GameObject).transform;

            //ボタン初期化 ステータス一覧時のボタンは押せないので、第二、第三引数はnull
            itemButton.GetComponent<StatusItemButton>().Init(item, null, null, index);
            itemButton.GetComponent<StatusItemButton>().Init(statusManager, battleMapManager);
            itemButton.name = itemButton.name.Replace("(Clone)", "");

            itemButton.transform.SetParent(inventoryList.transform, false);
            //partyWindowオブジェクト配下にprefab作成
            index++;
        }

        //所持アイテムの最大は6個 足りなければ空ボタン作成
        if (itemList.Count < 6)
        {
            int emptyButtonNum = 6 - itemList.Count;

            for (int i = 0; i < emptyButtonNum; i++)
            {
                //Resources配下からボタンをロード
                var itemButton = (Instantiate(Resources.Load("Prefabs/StatusItemButton")) as GameObject).transform;
                //ボタン初期化
                itemButton.GetComponent<StatusItemButton>().InitEmptyButton(null, null, index);
                itemButton.GetComponent<StatusItemButton>().Init(statusManager, battleMapManager);
                itemButton.name = itemButton.name.Replace("(Clone)", "");

                itemButton.transform.SetParent(inventoryList.transform, false);

                index++;
            }

        }
        
    }

    //200825 装備している武器を表示させる
    public void addEquipWeapon(Weapon equipWeapon, StatusManager statusManager, BattleMapManager battleMapManager)
    {
        //装備している武器を設定する
        //210222 第二引数はStatusManager ステータス画面でのボタンの機能が増えるなら別途設ける必要有り
        var equipWeaponButton = (Instantiate(Resources.Load("Prefabs/StatusItemButton")) as GameObject).transform;
        equipWeaponButton.name = equipWeaponButton.name.Replace("(Clone)", "");
        if (equipWeapon != null && "" != equipWeapon.name)
        {
            equipWeaponButton.GetComponent<StatusItemButton>().InitWeaponButton(equipWeapon);
        }
        else
        {
            //武器を装備していなければ空の武器ボタン作成
            Debug.Log("武器を装備していません");
            equipWeaponButton.GetComponent<StatusItemButton>().InitEmptyButton(null, null, 0);
            
        }
        //呼び出し元クラスへの参照
        equipWeaponButton.GetComponent<StatusItemButton>().Init(statusManager, battleMapManager);

        equipWeaponButton.transform.SetParent(equipList.transform, false);

    }

    //210218 装備している装飾品を表示させる
    public void addEquipAccessory(Accessory accessory, StatusManager statusManager, BattleMapManager battleMapManager)
    {
        var equipAccessoryButton = (Instantiate(Resources.Load("Prefabs/StatusItemButton")) as GameObject).transform;
        equipAccessoryButton.name = equipAccessoryButton.name.Replace("(Clone)", "");
        if (accessory != null && "" != accessory.name)
        {
            equipAccessoryButton.GetComponent<StatusItemButton>().InitAccessoryButton(accessory);
        }
        else
        {
            //これも無ければ空のボタン作成
            Debug.Log("アクセサリを装備していません");
            equipAccessoryButton.GetComponent<StatusItemButton>().InitEmptyButton(null, null, 0);
        }

        //呼び出し元クラスへの参照
        equipAccessoryButton.GetComponent<StatusItemButton>().Init(statusManager, battleMapManager);

        equipAccessoryButton.transform.SetParent(equipList.transform, false);
    }

    //スキル一覧作成(ステータス画面)
    public void addSkillList(List<Skill> skills, StatusManager statusManager, BattleMapManager battleMapManager)
    {
        foreach (var skill in skills)
        {

            //Resources配下からボタンをロード
            var skillButton = (Instantiate(Resources.Load("Prefabs/SkillButton")) as GameObject).transform;
            //ボタン初期化 今はテキストのみ
            skillButton.GetComponent<SkillButton>().Init(skill, statusManager);
            //呼び出し元クラスへの参照
            skillButton.GetComponent<SkillButton>().Init(statusManager, battleMapManager);
            skillButton.name = skillButton.name.Replace("(Clone)", "");
            skillButton.transform.SetParent(skillList.transform, false);
        }

        //これも6個未満なら空のスキルを作成
        if (skills.Count < 6)
        {
            int emptyButtonNum = 6 - skills.Count;

            for (int i = 0; i < emptyButtonNum; i++)
            {
                //Resources配下からボタンをロード
                var skillButton = (Instantiate(Resources.Load("Prefabs/SkillButton")) as GameObject).transform;
                //ボタン初期化 今はテキストのみ
                skillButton.GetComponent<SkillButton>().InitEmptyButton(statusManager, battleMapManager);
                skillButton.name = skillButton.name.Replace("(Clone)", "");
                skillButton.transform.SetParent(skillList.transform, false);

            }

        }
        
    }

    //ウィンドウを閉じる時、ステータス画面に表示しているボタン類をすべて削除する
    public void DeleteSkillButtonAndItemButton()
    {
        GameObject[] g = GameObject.FindGameObjectsWithTag("SkillButton");
        foreach (var obj in g)
        {
            Destroy(obj);
        }

        g = GameObject.FindGameObjectsWithTag("StatusItemButton");
        foreach (var obj in g)
        {
            Destroy(obj);
        }
    }

}
