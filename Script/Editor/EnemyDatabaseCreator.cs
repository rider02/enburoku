using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public static class EnemyDatabaseCreator
{

    //上部メニューに項目を追加
    [MenuItem("Create/EnemyDatabase")]
    private static void Create()
    {
        EnemyDatabase enemyDatabase = ScriptableObject.CreateInstance<EnemyDatabase>();

        //武器の初期化用にデータベース取得
        WeaponDatabase weaponDatabase = Resources.Load<WeaponDatabase>("weaponDatabase");

        JobDatabase jobDatabase = Resources.Load<JobDatabase>("jobDatabase");

        //妖精
        string name = "妖精";
        int[] status = new int[] { 1, 20, 5, 3, 3, 3, 1, 3, 2};
        Job job = jobDatabase.FindByJob(JobName.妖精);
        bool isBoss = false;
        RaceType race = RaceType.FAIRY;
        string pathName = null;
        var skillLevelMap = new Dictionary<WeaponType, SkillLevel>()
        {
            {WeaponType.SHOT,SkillLevel.D },
        };

        enemyDatabase.enemyList.Add(new Enemy(name, job, status, isBoss, race, pathName, skillLevelMap));

        //メイド妖精　上級職とのステータス補正は職業に持たせているので、基礎値は同じとなる
        name = "メイド妖精";
        status = new int[] { 1, 20, 5, 3, 3, 3, 1, 3, 2 };
        job = jobDatabase.FindByJob(JobName.メイド妖精);
        isBoss = false;
        race = RaceType.FAIRY;
        skillLevelMap = new Dictionary<WeaponType, SkillLevel>()
        {
            {WeaponType.SHOT,SkillLevel.B },
            {WeaponType.STRIKE,SkillLevel.B },
            {WeaponType.HEAL,SkillLevel.C },
        };
        enemyDatabase.enemyList.Add(new Enemy(name, job, status, isBoss, race, pathName, skillLevelMap));

        //毛玉
        name = "毛玉";
        status = new int[] { 1, 18, 2, 4, 3, 5, 2, 1, 4};
        job = jobDatabase.FindByJob(JobName.毛玉);
        isBoss = false;
        race = RaceType.YOUKAI;
        skillLevelMap = new Dictionary<WeaponType, SkillLevel>()
        {
            {WeaponType.STRIKE,SkillLevel.D },
        };
        enemyDatabase.enemyList.Add(new Enemy(name, job, status, isBoss, race, pathName, skillLevelMap));

        
        name = "妖獣";
        status = new int[] { 1, 18, 4, 5, 5, 5, 3, 1, 4 };
        job = jobDatabase.FindByJob(JobName.妖獣);
        isBoss = false;
        race = RaceType.YOUKAI;
        skillLevelMap = new Dictionary<WeaponType, SkillLevel>()
        {
            {WeaponType.STRIKE,SkillLevel.B },
        };
        enemyDatabase.enemyList.Add(new Enemy(name, job, status, isBoss, race, pathName, skillLevelMap));

        name = "魔導書";
        status = new int[] {1, 20, 6, 1, 8, 3, 2, 8, 1};
        job = jobDatabase.FindByJob(JobName.魔導書);
        isBoss = false;
        race = RaceType.TSUKUMOGAMI;
        skillLevelMap = new Dictionary<WeaponType, SkillLevel>()
        {
            {WeaponType.STRIKE,SkillLevel.D },
        };
        enemyDatabase.enemyList.Add(new Enemy(name, job, status, isBoss, race, pathName, skillLevelMap));

        name = "グリモワール";
        status = new int[] { 1, 20, 6, 1, 8, 3, 2, 8, 1 };
        job = jobDatabase.FindByJob(JobName.グリモワール);
        isBoss = false;
        race = RaceType.TSUKUMOGAMI;
        skillLevelMap = new Dictionary<WeaponType, SkillLevel>()
        {
            {WeaponType.STRIKE,SkillLevel.B },
        };
        enemyDatabase.enemyList.Add(new Enemy(name, job, status, isBoss, race, pathName, skillLevelMap));

        name = "使い魔";
        status = new int[] { 1, 17, 5, 3, 5, 2, 2, 3, 2};
        job = jobDatabase.FindByJob(JobName.使い魔);
        isBoss = false;
        race = RaceType.MAGIC;
        skillLevelMap = new Dictionary<WeaponType, SkillLevel>()
        {
            {WeaponType.SHOT,SkillLevel.E },
            {WeaponType.STRIKE,SkillLevel.E },
            {WeaponType.LASER,SkillLevel.E },
        };
        enemyDatabase.enemyList.Add(new Enemy(name, job, status, isBoss, race, pathName, skillLevelMap));

        name = "ひまわり妖精";
        status = new int[] { 1, 25, 6, 5, 6, 3, 2, 8, 3};
        job = jobDatabase.FindByJob(JobName.ひまわり妖精);
        isBoss = false;
        race = RaceType.FAIRY;
        skillLevelMap = new Dictionary<WeaponType, SkillLevel>()
        {
            {WeaponType.SHOT,SkillLevel.D },
        };
        enemyDatabase.enemyList.Add(new Enemy(name, job, status, isBoss, race, pathName, skillLevelMap));

        name = "ハイフェアリー";
        status = new int[] { 1, 25, 6, 5, 6, 3, 2, 8, 3 };
        job = jobDatabase.FindByJob(JobName.ハイフェアリー);
        isBoss = false;
        race = RaceType.FAIRY;
        skillLevelMap = new Dictionary<WeaponType, SkillLevel>()
        {
            {WeaponType.SHOT,SkillLevel.B },
            {WeaponType.STRIKE,SkillLevel.B },
        };
        enemyDatabase.enemyList.Add(new Enemy(name, job, status, isBoss, race, pathName, skillLevelMap));

        name = "ホブゴブリン";
        status = new int[] { 1, 28, 7, 12, 7, 5, 1, 5, 5};
        job = jobDatabase.FindByJob(JobName.ホブゴブリン);
        isBoss = false;
        race = RaceType.YOUKAI;
        skillLevelMap = new Dictionary<WeaponType, SkillLevel>()
        {
            {WeaponType.STRIKE,SkillLevel.B },
        };
        enemyDatabase.enemyList.Add(new Enemy(name, job, status, isBoss, race, pathName, skillLevelMap));

        name = "吸血コウモリ";
        status = new int[] { 1, 20, 4, 6, 6, 8, 6, 2, 5};
        job = jobDatabase.FindByJob(JobName.吸血コウモリ);
        isBoss = false;
        race = RaceType.YOUKAI;
        skillLevelMap = new Dictionary<WeaponType, SkillLevel>()
        {
            {WeaponType.STRIKE,SkillLevel.C },
        };
        enemyDatabase.enemyList.Add(new Enemy(name, job, status, isBoss, race, pathName, skillLevelMap));

        name = "ツパイ";
        status = new int[] { 1, 20, 4, 6, 6, 8, 6, 2, 5 };
        job = jobDatabase.FindByJob(JobName.ツパイ);
        isBoss = false;
        race = RaceType.YOUKAI;
        skillLevelMap = new Dictionary<WeaponType, SkillLevel>()
        {
            {WeaponType.STRIKE,SkillLevel.A },
        };
        enemyDatabase.enemyList.Add(new Enemy(name, job, status, isBoss, race, pathName, skillLevelMap));

        name = "幽霊";
        status = new int[] {1, 18, 5, 1, 5, 4, 1, 2, 5};
        job = jobDatabase.FindByJob(JobName.幽霊);
        isBoss = false;
        race = RaceType.GHOST;
        skillLevelMap = new Dictionary<WeaponType, SkillLevel>()
        {
            {WeaponType.LASER,SkillLevel.D },
        };
        enemyDatabase.enemyList.Add(new Enemy(name, job, status, isBoss, race, pathName, skillLevelMap));

        name = "怨霊";
        status = new int[] { 1, 18, 5, 1, 5, 4, 1, 2, 5 };
        job = jobDatabase.FindByJob(JobName.怨霊);
        isBoss = false;
        race = RaceType.GHOST;
        skillLevelMap = new Dictionary<WeaponType, SkillLevel>()
        {
            {WeaponType.LASER,SkillLevel.B },
        };
        enemyDatabase.enemyList.Add(new Enemy(name, job, status, isBoss, race, pathName, skillLevelMap));

        //魔理沙
        status = new int[] { 1, 19, 6, 4, 5, 8, 4, 4, 2};
        name = "魔理沙";
        job = jobDatabase.FindByJob(JobName.魔法使い);
        isBoss = true;
        race = RaceType.HUMAN;
        pathName = "Marisa";
        skillLevelMap = new Dictionary<WeaponType, SkillLevel>()
        {
            {WeaponType.SHOT,SkillLevel.E },
            {WeaponType.LASER,SkillLevel.D },
        };
        enemyDatabase.enemyList.Add(new Enemy(name, job, status, isBoss, race, pathName, skillLevelMap));

        //ルーミア
        status = new int[] { 1, 24, 10, 5, 4, 5, 4, 10, 9};
        name = "ルーミア";
        job = jobDatabase.FindByJob(JobName.暗闇の妖怪);
        isBoss = true;
        race = RaceType.YOUKAI;
        pathName = "Rumia";
        skillLevelMap = new Dictionary<WeaponType, SkillLevel>()
        {
            {WeaponType.SHOT,SkillLevel.E },
            {WeaponType.LASER,SkillLevel.D },
        };
        enemyDatabase.enemyList.Add(new Enemy(name, job, status, isBoss, race, pathName, skillLevelMap));

        //大妖精
        status = new int[] { 1, 24, 8, 5, 6, 7, 8, 9, 8 };
        name = "大妖精";
        job = jobDatabase.FindByJob(JobName.大妖精);
        isBoss = true;
        race = RaceType.FAIRY;
        pathName = "Daiyousei";
        skillLevelMap = new Dictionary<WeaponType, SkillLevel>()
        {
            {WeaponType.SHOT,SkillLevel.D },
            {WeaponType.HEAL,SkillLevel.C },
        };
        enemyDatabase.enemyList.Add(new Enemy(name, job, status, isBoss, race, pathName, skillLevelMap));

        //チルノ
        status = new int[] { 1, 26, 10, 10, 6, 8, 4, 9, 8};
        name = "チルノ";
        job = jobDatabase.FindByJob(JobName.湖上の氷精);
        isBoss = true;
        race = RaceType.FAIRY;
        pathName = "Cirno";
        skillLevelMap = new Dictionary<WeaponType, SkillLevel>()
        {
            {WeaponType.SHOT,SkillLevel.C },
            {WeaponType.STRIKE,SkillLevel.D },
        };
        enemyDatabase.enemyList.Add(new Enemy(name, job, status, isBoss, race, pathName, skillLevelMap));

        //美鈴
        status = new int[] { 1, 30, 5, 12, 8, 9, 2, 7, 11};
        name = "美鈴";
        job = jobDatabase.FindByJob(JobName.門番);
        isBoss = true;
        race = RaceType.YOUKAI;
        pathName = "Meirin";
        skillLevelMap = new Dictionary<WeaponType, SkillLevel>()
        {
            {WeaponType.STRIKE,SkillLevel.B },
            {WeaponType.HEAL,SkillLevel.D },
        };
        enemyDatabase.enemyList.Add(new Enemy(name, job, status, isBoss, race, pathName, skillLevelMap));

        //小悪魔
        status = new int[] { 1, 23, 6, 4, 6, 5, 5, 3, 3};
        name = "小悪魔";
        isBoss = true;
        job = jobDatabase.FindByJob(JobName.小悪魔);
        race = RaceType.YOUKAI;
        pathName = "Koakuma";
        skillLevelMap = new Dictionary<WeaponType, SkillLevel>()
        {
            {WeaponType.SHOT,SkillLevel.C },
            {WeaponType.HEAL,SkillLevel.C },
        };
        enemyDatabase.enemyList.Add(new Enemy(name, job, status, isBoss, race, pathName, skillLevelMap));

        //パチュリー
        status = new int[] {1, 20 ,10 ,3 ,6, 1, 4, 5, 1};
        name = "パチュリー";
        isBoss = true;
        job = jobDatabase.FindByJob(JobName.魔女);
        race = RaceType.YOUKAI;
        pathName = "Patu";
        skillLevelMap = new Dictionary<WeaponType, SkillLevel>()
        {
            {WeaponType.SHOT,SkillLevel.B },
            {WeaponType.LASER,SkillLevel.A },
            {WeaponType.STRIKE,SkillLevel.B },
            {WeaponType.HEAL,SkillLevel.C },
        };
        enemyDatabase.enemyList.Add(new Enemy(name, job, status, isBoss, race, pathName, skillLevelMap));

        //咲夜
        status = new int[] { 1, 25 ,8 ,8 ,10 ,8 ,10 ,6 ,6};

        name = "咲夜";
        job = jobDatabase.FindByJob(JobName.紅魔館のメイド);
        isBoss = true;
        race = RaceType.HUMAN;
        pathName = "Sakuya";
        skillLevelMap = new Dictionary<WeaponType, SkillLevel>()
        {
            {WeaponType.SHOT,SkillLevel.A },
            {WeaponType.STRIKE,SkillLevel.S },
        };
        enemyDatabase.enemyList.Add(new Enemy(name, job, status, isBoss , race, pathName, skillLevelMap));

        //レミリア
        status = new int[] { 1, 30, 10, 10, 8, 9, 12, 7, 7};

        name = "レミリア";
        job = jobDatabase.FindByJob(JobName.紅い悪魔);
        isBoss = true;

        race = RaceType.YOUKAI;
        pathName = "Remilia";
        skillLevelMap = new Dictionary<WeaponType, SkillLevel>()
        {
            {WeaponType.STRIKE,SkillLevel.S },
        };
        enemyDatabase.enemyList.Add(new Enemy(name, job, status, isBoss, race, pathName, skillLevelMap));

        //フラン
        status = new int[] { 1, 30, 10, 10, 8, 9, 12, 7, 7 };
        name = "フランドール";
        job = jobDatabase.FindByJob(JobName.紅のカタストロフ);
        isBoss = true;

        race = RaceType.YOUKAI;
        pathName = "Fran";
        skillLevelMap = new Dictionary<WeaponType, SkillLevel>()
        {
            {WeaponType.STRIKE,SkillLevel.S },
        };
        enemyDatabase.enemyList.Add(new Enemy(name, job, status, isBoss, race, pathName, skillLevelMap));

        //ファイル書き出し Resources配下に作る
        AssetDatabase.CreateAsset(enemyDatabase, "Assets/Resources/enemyDatabase.asset");
    }

}
