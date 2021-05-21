using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public static class JobDatabaseCreator
{

    //MenuItemを付ける事で上部メニューに項目を追加
    [MenuItem("Create/JobDatabase")]
    private static void Create()
    {

        JobDatabase jobDatabase = ScriptableObject.CreateInstance<JobDatabase>();

        //霊夢
        JobName jobName = JobName.巫女;
        JobLevel jobLevel = JobLevel.NOVICE;
        List<Skill> skills = new List<Skill>() { Skill.天才肌, Skill.幸運 };
        List<JobName> classChangeDestination = new List<JobName>() { JobName.空飛ぶ不思議な巫女};
        List<WeaponType> weaponTypes = new List<WeaponType>() { WeaponType.SHOT, WeaponType.STRIKE ,WeaponType.HEAL};

        JobStatusDto statusDto = new JobStatusDto();
        statusDto.jobHp = 0;
        statusDto.jobLatk = 0;
        statusDto.jobCatk = 0;
        statusDto.jobDex = 0;
        statusDto.jobAgi = 0;
        statusDto.jobLuk = 0;
        statusDto.jobLdef = 0;
        statusDto.jobCdef = 0;

        GrowthRateDto growthRateDto = new GrowthRateDto();
        growthRateDto.jobHpRate = 10;
        growthRateDto.jobLatkRate = 15;
        growthRateDto.jobCatkRate = 10;
        growthRateDto.jobDexRate = 10;
        growthRateDto.jobAgiRate = 10;
        growthRateDto.jobLukRate = 5;
        growthRateDto.jobLdefRate = 5;
        growthRateDto.jobCdefRate = 5;

        int move = 4;

        Job job = new Job(jobName, jobLevel, skills, classChangeDestination, weaponTypes, statusDto, growthRateDto, move);
        jobDatabase.jobList.Add(job);

        jobName = JobName.空飛ぶ不思議な巫女;
        jobLevel = JobLevel.ADEPT;

        skills = new List<Skill>() { Skill.天才肌, Skill.幸運, Skill.亜空穴 };
        classChangeDestination = new List<JobName>() { JobName.永遠の巫女, JobName.結界の巫女 };
        weaponTypes = new List<WeaponType>() { WeaponType.SHOT, WeaponType.STRIKE, WeaponType.HEAL };

        statusDto = new JobStatusDto();
        statusDto.jobHp = 1;
        statusDto.jobLatk = 1;
        statusDto.jobCatk = 1;
        statusDto.jobDex = 0;
        statusDto.jobAgi = 1;
        statusDto.jobLuk = 2;
        statusDto.jobLdef = 1;
        statusDto.jobCdef = 0;

        growthRateDto = new GrowthRateDto();
        growthRateDto.jobHpRate = 10;
        growthRateDto.jobLatkRate = 15;
        growthRateDto.jobCatkRate = 10;
        growthRateDto.jobDexRate = 10;
        growthRateDto.jobAgiRate = 10;
        growthRateDto.jobLukRate = 5;
        growthRateDto.jobLdefRate = 5;
        growthRateDto.jobCdefRate = 5;

        move = 5;

        job = new Job(jobName, jobLevel, skills, classChangeDestination, weaponTypes, statusDto, growthRateDto, move);
        jobDatabase.jobList.Add(job);

        jobName = JobName.永遠の巫女;
        jobLevel = JobLevel.MASTER;

        skills = new List<Skill>() { Skill.天才肌, Skill.幸運, Skill.亜空穴, Skill.再行動, Skill.弾幕の達人, Skill.無想転生 };
        classChangeDestination = null;
        weaponTypes = new List<WeaponType>() { WeaponType.SHOT, WeaponType.STRIKE, WeaponType.HEAL };

        statusDto = new JobStatusDto();
        statusDto.jobHp = 3;
        statusDto.jobLatk = 4;
        statusDto.jobCatk = 2;
        statusDto.jobDex = 0;
        statusDto.jobAgi = 3;
        statusDto.jobLuk = 2;
        statusDto.jobLdef = 3;
        statusDto.jobCdef = 1;

        growthRateDto = new GrowthRateDto();
        growthRateDto.jobHpRate = 15;
        growthRateDto.jobLatkRate = 20;
        growthRateDto.jobCatkRate = 10;
        growthRateDto.jobDexRate = 10;
        growthRateDto.jobAgiRate = 15;
        growthRateDto.jobLukRate = 5;
        growthRateDto.jobLdefRate = 10;
        growthRateDto.jobCdefRate = 5;

        move = 7;

        job = new Job(jobName, jobLevel, skills, classChangeDestination, weaponTypes, statusDto, growthRateDto, move);
        jobDatabase.jobList.Add(job);

        jobName = JobName.結界の巫女;
        jobLevel = JobLevel.MASTER;

        skills = new List<Skill>() { Skill.天才肌, Skill.幸運, Skill.亜空穴, Skill.博麗の小結界, Skill.武器の達人, Skill.弾幕の達人 };
        classChangeDestination = null;
        weaponTypes = new List<WeaponType>() { WeaponType.SHOT, WeaponType.STRIKE, WeaponType.HEAL };

        statusDto = new JobStatusDto();
        statusDto.jobHp = 3;
        statusDto.jobLatk = 2;
        statusDto.jobCatk = 6;
        statusDto.jobDex = 2;
        statusDto.jobAgi = 2;
        statusDto.jobLuk = 2;
        statusDto.jobLdef = 3;
        statusDto.jobCdef = 1;

        growthRateDto = new GrowthRateDto();
        growthRateDto.jobHpRate = 15;
        growthRateDto.jobLatkRate = 10;
        growthRateDto.jobCatkRate = 20;
        growthRateDto.jobDexRate = 15;
        growthRateDto.jobAgiRate = 10;
        growthRateDto.jobLukRate = 5;
        growthRateDto.jobLdefRate = 10;
        growthRateDto.jobCdefRate = 5;

        move = 6;

        job = new Job(jobName, jobLevel, skills, classChangeDestination, weaponTypes, statusDto, growthRateDto, move);
        jobDatabase.jobList.Add(job);

        //魔理沙
        jobName = JobName.魔法使い;
        jobLevel = JobLevel.NOVICE;

        skills = new List<Skill>() { Skill.努力家, Skill.鍵開け };
        classChangeDestination = new List<JobName>() { JobName.普通の黒魔法少女 };
        weaponTypes = new List<WeaponType>() { WeaponType.SHOT, WeaponType.LASER,};

        statusDto = new JobStatusDto();
        statusDto.jobHp = 0;
        statusDto.jobLatk = 0;
        statusDto.jobCatk = 0;
        statusDto.jobDex = 0;
        statusDto.jobAgi = 0;
        statusDto.jobLuk = 0;
        statusDto.jobLdef = 0;
        statusDto.jobCdef = 0;

        growthRateDto = new GrowthRateDto();
        growthRateDto.jobHpRate = 10;
        growthRateDto.jobLatkRate = 20;
        growthRateDto.jobCatkRate = 5;
        growthRateDto.jobDexRate = 5;
        growthRateDto.jobAgiRate = 15;
        growthRateDto.jobLukRate = 5;
        growthRateDto.jobLdefRate = 5;
        growthRateDto.jobCdefRate = 5;

        move = 4;

        job = new Job(jobName, jobLevel, skills, classChangeDestination, weaponTypes, statusDto, growthRateDto, move);
        jobDatabase.jobList.Add(job);

        jobName = JobName.普通の黒魔法少女;
        jobLevel = JobLevel.ADEPT;

        skills = new List<Skill>() { Skill.努力家, Skill.鍵開け, Skill.メンテナンス };
        classChangeDestination = new List<JobName>() { JobName.恋の魔砲少女, JobName.森の魔砲探偵};
        weaponTypes = new List<WeaponType>() { WeaponType.SHOT, WeaponType.LASER, };

        statusDto = new JobStatusDto();
        statusDto.jobHp = 1;
        statusDto.jobLatk = 2;
        statusDto.jobCatk = 0;
        statusDto.jobDex = 0;
        statusDto.jobAgi = 1;
        statusDto.jobLuk = 1;
        statusDto.jobLdef = 1;
        statusDto.jobCdef = 0;

        growthRateDto = new GrowthRateDto();
        growthRateDto.jobHpRate = 10;
        growthRateDto.jobLatkRate = 20;
        growthRateDto.jobCatkRate = 5;
        growthRateDto.jobDexRate = 5;
        growthRateDto.jobAgiRate = 15;
        growthRateDto.jobLukRate = 5;
        growthRateDto.jobLdefRate = 5;
        growthRateDto.jobCdefRate = 5;

        move = 5;

        job = new Job(jobName, jobLevel, skills, classChangeDestination, weaponTypes, statusDto, growthRateDto, move);
        jobDatabase.jobList.Add(job);

        jobName = JobName.恋の魔砲少女;
        jobLevel = JobLevel.MASTER;
        skills = new List<Skill>() { Skill.努力家, Skill.鍵開け, Skill.メンテナンス, Skill.再行動, Skill.蒐集家, Skill.レーザーの達人 };
        classChangeDestination = null;
        weaponTypes = new List<WeaponType>() { WeaponType.SHOT, WeaponType.LASER, };

        statusDto = new JobStatusDto();
        statusDto.jobHp = 2;
        statusDto.jobLatk = 5;
        statusDto.jobCatk = 2;
        statusDto.jobDex = 1;
        statusDto.jobAgi = 3;
        statusDto.jobLuk = 1;
        statusDto.jobLdef = 3;
        statusDto.jobCdef = 1;

        growthRateDto = new GrowthRateDto();
        growthRateDto.jobHpRate = 10;
        growthRateDto.jobLatkRate = 25;
        growthRateDto.jobCatkRate = 5;
        growthRateDto.jobDexRate = 10;
        growthRateDto.jobAgiRate = 20;
        growthRateDto.jobLukRate = 5;
        growthRateDto.jobLdefRate = 10;
        growthRateDto.jobCdefRate = 0;

        move = 7;

        job = new Job(jobName, jobLevel, skills, classChangeDestination, weaponTypes, statusDto, growthRateDto, move);
        jobDatabase.jobList.Add(job);

        jobName = JobName.森の魔砲探偵;
        jobLevel = JobLevel.MASTER;

        skills = new List<Skill>() { Skill.努力家, Skill.鍵開け, Skill.メンテナンス, Skill.再行動, Skill.一撃離脱, Skill.薬売り };
        classChangeDestination = null;
        weaponTypes = new List<WeaponType>() { WeaponType.SHOT, WeaponType.LASER, };

        statusDto = new JobStatusDto();
        statusDto.jobHp = 2;
        statusDto.jobLatk = 4;
        statusDto.jobCatk = 3;
        statusDto.jobDex = 2;
        statusDto.jobAgi = 4;
        statusDto.jobLuk = 1;
        statusDto.jobLdef = 2;
        statusDto.jobCdef = 2;

        growthRateDto = new GrowthRateDto();
        growthRateDto.jobHpRate = 10;
        growthRateDto.jobLatkRate = 20;
        growthRateDto.jobCatkRate = 10;
        growthRateDto.jobDexRate = 20;
        growthRateDto.jobAgiRate = 15;
        growthRateDto.jobLukRate = 5;
        growthRateDto.jobLdefRate = 0;
        growthRateDto.jobCdefRate = 10;

        move = 7;

        job = new Job(jobName, jobLevel, skills, classChangeDestination, weaponTypes, statusDto, growthRateDto, move);
        jobDatabase.jobList.Add(job);

        //ルーミア
        jobName = JobName.暗闇の妖怪;
        jobLevel = JobLevel.NOVICE;

        skills = new List<Skill>() { Skill.目隠し, Skill.HP5 };
        classChangeDestination = new List<JobName>() { JobName.宵闇の妖怪 };
        weaponTypes = new List<WeaponType>() { WeaponType.SHOT, WeaponType.LASER, };

        statusDto = new JobStatusDto();
        statusDto.jobHp = 0;
        statusDto.jobLatk = 0;
        statusDto.jobCatk = 0;
        statusDto.jobDex = 0;
        statusDto.jobAgi = 0;
        statusDto.jobLuk = 0;
        statusDto.jobLdef = 0;
        statusDto.jobCdef = 0;

        growthRateDto = new GrowthRateDto();
        growthRateDto.jobHpRate = 10;
        growthRateDto.jobLatkRate = 5;
        growthRateDto.jobCatkRate = 10;
        growthRateDto.jobDexRate = 5;
        growthRateDto.jobAgiRate = 5;
        growthRateDto.jobLukRate = 0;
        growthRateDto.jobLdefRate = 10;
        growthRateDto.jobCdefRate = 10;

        move = 4;

        job = new Job(jobName, jobLevel, skills, classChangeDestination, weaponTypes, statusDto, growthRateDto, move);
        jobDatabase.jobList.Add(job);

        jobName = JobName.宵闇の妖怪;
        jobLevel = JobLevel.ADEPT;

        skills = new List<Skill>() { Skill.目隠し, Skill.HP5, Skill.魔力吸収 };
        classChangeDestination = new List<JobName>() { JobName.封印の大妖怪, JobName.深淵の少女 };
        weaponTypes = new List<WeaponType>() { WeaponType.SHOT, WeaponType.LASER, };

        statusDto = new JobStatusDto();
        statusDto.jobHp = 2;
        statusDto.jobLatk = 1;
        statusDto.jobCatk = 0;
        statusDto.jobDex = 0;
        statusDto.jobAgi = 1;
        statusDto.jobLuk = 0;
        statusDto.jobLdef = 2;
        statusDto.jobCdef = 1;

        growthRateDto = new GrowthRateDto();
        growthRateDto.jobHpRate = 10;
        growthRateDto.jobLatkRate = 5;
        growthRateDto.jobCatkRate = 10;
        growthRateDto.jobDexRate = 5;
        growthRateDto.jobAgiRate = 5;
        growthRateDto.jobLukRate = 0;
        growthRateDto.jobLdefRate = 10;
        growthRateDto.jobCdefRate = 10;

        move = 5;

        job = new Job(jobName, jobLevel, skills, classChangeDestination, weaponTypes, statusDto, growthRateDto, move);
        jobDatabase.jobList.Add(job);

        jobName = JobName.封印の大妖怪;
        jobLevel = JobLevel.MASTER;

        skills = new List<Skill>() { Skill.目隠し, Skill.HP5, Skill.魔力吸収, Skill.暗黒の一撃, Skill.弾幕の達人, Skill.EX化 };
        classChangeDestination = null;
        weaponTypes = new List<WeaponType>() { WeaponType.SHOT, WeaponType.LASER, };

        statusDto = new JobStatusDto();
        statusDto.jobHp = 3;
        statusDto.jobLatk = 4;
        statusDto.jobCatk = 2;
        statusDto.jobDex = 1;
        statusDto.jobAgi = 4;
        statusDto.jobLuk = 0;
        statusDto.jobLdef = 3;
        statusDto.jobCdef = 2;

        growthRateDto = new GrowthRateDto();
        growthRateDto.jobHpRate = 10;
        growthRateDto.jobLatkRate = 10;
        growthRateDto.jobCatkRate = 20;
        growthRateDto.jobDexRate = 5;
        growthRateDto.jobAgiRate = 15;
        growthRateDto.jobLukRate = 5;
        growthRateDto.jobLdefRate = 10;
        growthRateDto.jobCdefRate = 10;

        move = 6;

        job = new Job(jobName, jobLevel, skills, classChangeDestination, weaponTypes, statusDto, growthRateDto, move);
        jobDatabase.jobList.Add(job);

        jobName = JobName.深淵の少女;
        jobLevel = JobLevel.MASTER;

        skills = new List<Skill>() { Skill.目隠し, Skill.HP5, Skill.魔力吸収, Skill.宵闇の守り, Skill.レーザーの達人};
        classChangeDestination = null;
        weaponTypes = new List<WeaponType>() { WeaponType.SHOT, WeaponType.LASER, };

        statusDto = new JobStatusDto();
        statusDto.jobHp = 5;
        statusDto.jobLatk = 2;
        statusDto.jobCatk = 1;
        statusDto.jobDex = 2;
        statusDto.jobAgi = 1;
        statusDto.jobLuk = 0;
        statusDto.jobLdef = 6;
        statusDto.jobCdef = 4;

        growthRateDto = new GrowthRateDto();
        growthRateDto.jobHpRate = 15;
        growthRateDto.jobLatkRate = 10;
        growthRateDto.jobCatkRate = 10;
        growthRateDto.jobDexRate = 10;
        growthRateDto.jobAgiRate = 0;
        growthRateDto.jobLukRate = 5;
        growthRateDto.jobLdefRate = 15;
        growthRateDto.jobCdefRate = 20;

        move = 6;

        job = new Job(jobName, jobLevel, skills, classChangeDestination, weaponTypes, statusDto, growthRateDto, move);
        jobDatabase.jobList.Add(job);

        //大妖精
        jobName = JobName.大妖精;
        jobLevel = JobLevel.NOVICE;

        skills = new List<Skill>() { Skill.小さな声援, Skill.祈り };
        classChangeDestination = new List<JobName>() { JobName.ルーネイトエルフ };
        weaponTypes = new List<WeaponType>() { WeaponType.SHOT, WeaponType.HEAL, };

        statusDto = new JobStatusDto();
        statusDto.jobHp = 0;
        statusDto.jobLatk = 0;
        statusDto.jobCatk = 0;
        statusDto.jobDex = 0;
        statusDto.jobAgi = 0;
        statusDto.jobLuk = 0;
        statusDto.jobLdef = 0;
        statusDto.jobCdef = 0;

        growthRateDto = new GrowthRateDto();
        growthRateDto.jobHpRate = 0;
        growthRateDto.jobLatkRate = 10;
        growthRateDto.jobCatkRate = 10;
        growthRateDto.jobDexRate = 10;
        growthRateDto.jobAgiRate = 15;
        growthRateDto.jobLukRate = 5;
        growthRateDto.jobLdefRate = 10;
        growthRateDto.jobCdefRate = 0;

        move = 4;

        job = new Job(jobName, jobLevel, skills, classChangeDestination, weaponTypes, statusDto, growthRateDto, move);
        jobDatabase.jobList.Add(job);

        jobName = JobName.ルーネイトエルフ;
        jobLevel = JobLevel.ADEPT;

        skills = new List<Skill>() { Skill.小さな声援, Skill.祈り, Skill.幸運の叫び };
        classChangeDestination = new List<JobName>() { JobName.ティターニア, JobName.四大精霊 };
        weaponTypes = new List<WeaponType>() { WeaponType.SHOT, WeaponType.HEAL, };

        statusDto = new JobStatusDto();
        statusDto.jobHp = 1;
        statusDto.jobLatk = 1;
        statusDto.jobCatk = 0;
        statusDto.jobDex = 1;
        statusDto.jobAgi = 1;
        statusDto.jobLuk = 1;
        statusDto.jobLdef = 1;
        statusDto.jobCdef = 0;

        growthRateDto = new GrowthRateDto();
        growthRateDto.jobHpRate = 0;
        growthRateDto.jobLatkRate = 10;
        growthRateDto.jobCatkRate = 10;
        growthRateDto.jobDexRate = 10;
        growthRateDto.jobAgiRate = 15;
        growthRateDto.jobLukRate = 5;
        growthRateDto.jobLdefRate = 10;
        growthRateDto.jobCdefRate = 0;

        move = 5;

        job = new Job(jobName, jobLevel, skills, classChangeDestination, weaponTypes, statusDto, growthRateDto, move);
        jobDatabase.jobList.Add(job);

        jobName = JobName.ティターニア;
        jobLevel = JobLevel.MASTER;

        skills = new List<Skill>() { Skill.小さな声援, Skill.祈り, Skill.幸運の叫び ,Skill.遠距離の叫び , Skill.速さ封じ };
        classChangeDestination = null;
        weaponTypes = new List<WeaponType>() { WeaponType.SHOT, WeaponType.HEAL, };

        statusDto = new JobStatusDto();
        statusDto.jobHp = 2;
        statusDto.jobLatk = 3;
        statusDto.jobCatk = 1;
        statusDto.jobDex = 2;
        statusDto.jobAgi = 4;
        statusDto.jobLuk = 2;
        statusDto.jobLdef = 3;
        statusDto.jobCdef = 1;

        growthRateDto = new GrowthRateDto();
        growthRateDto.jobHpRate = 0;
        growthRateDto.jobLatkRate = 20;
        growthRateDto.jobCatkRate = 10;
        growthRateDto.jobDexRate = 10;
        growthRateDto.jobAgiRate = 15;
        growthRateDto.jobLukRate = 5;
        growthRateDto.jobLdefRate = 10;
        growthRateDto.jobCdefRate = 5;

        move = 6;

        job = new Job(jobName, jobLevel, skills, classChangeDestination, weaponTypes, statusDto, growthRateDto, move);
        jobDatabase.jobList.Add(job);

        jobName = JobName.四大精霊;
        jobLevel = JobLevel.MASTER;

        skills = new List<Skill>() { Skill.小さな声援, Skill.祈り, Skill.幸運の叫び, Skill.自然の恵み, Skill.癒し手 };
        classChangeDestination = null;
        weaponTypes = new List<WeaponType>() { WeaponType.SHOT, WeaponType.HEAL, };

        statusDto = new JobStatusDto();
        statusDto.jobHp = 4;
        statusDto.jobLatk = 2;
        statusDto.jobCatk = 1;
        statusDto.jobDex = 2;
        statusDto.jobAgi = 3;
        statusDto.jobLuk = 3;
        statusDto.jobLdef = 4;
        statusDto.jobCdef = 1;

        growthRateDto = new GrowthRateDto();
        growthRateDto.jobHpRate = 10;
        growthRateDto.jobLatkRate = 10;
        growthRateDto.jobCatkRate = 10;
        growthRateDto.jobDexRate = 10;
        growthRateDto.jobAgiRate = 15;
        growthRateDto.jobLukRate = 5;
        growthRateDto.jobLdefRate = 15;
        growthRateDto.jobCdefRate = 5;

        move = 6;

        job = new Job(jobName, jobLevel, skills, classChangeDestination, weaponTypes, statusDto, growthRateDto, move);
        jobDatabase.jobList.Add(job);

        //ツィルノ
        jobName = JobName.湖上の氷精;
        jobLevel = JobLevel.NOVICE;

        skills = new List<Skill>() { Skill.自信満々, Skill.しぶとい心 };
        classChangeDestination = new List<JobName>() { JobName.おてんば恋娘 };
        weaponTypes = new List<WeaponType>() { WeaponType.SHOT, WeaponType.STRIKE, };

        statusDto = new JobStatusDto();
        statusDto.jobHp = 0;
        statusDto.jobLatk = 0;
        statusDto.jobCatk = 0;
        statusDto.jobDex = 0;
        statusDto.jobAgi = 0;
        statusDto.jobLuk = 0;
        statusDto.jobLdef = 0;
        statusDto.jobCdef = 0;

        growthRateDto = new GrowthRateDto();
        growthRateDto.jobHpRate = 10;
        growthRateDto.jobLatkRate = 5;
        growthRateDto.jobCatkRate = 10;
        growthRateDto.jobDexRate = 5;
        growthRateDto.jobAgiRate = 10;
        growthRateDto.jobLukRate = 5;
        growthRateDto.jobLdefRate = 5;
        growthRateDto.jobCdefRate = 5;

        move = 4;

        job = new Job(jobName, jobLevel, skills, classChangeDestination, weaponTypes, statusDto, growthRateDto, move);
        jobDatabase.jobList.Add(job);

        jobName = JobName.おてんば恋娘;
        jobLevel = JobLevel.ADEPT;

        skills = new List<Skill>() { Skill.自信満々, Skill.しぶとい心, Skill.凍える冷気 };
        classChangeDestination = new List<JobName>() { JobName.最強の氷精, JobName.冬将軍 };
        weaponTypes = new List<WeaponType>() { WeaponType.SHOT, WeaponType.STRIKE, };

        statusDto = new JobStatusDto();
        statusDto.jobHp = 1;
        statusDto.jobLatk = 1;
        statusDto.jobCatk = 1;
        statusDto.jobDex = 1;
        statusDto.jobAgi = 1;
        statusDto.jobLuk = 0;
        statusDto.jobLdef = 0;
        statusDto.jobCdef = 1;

        growthRateDto = new GrowthRateDto();
        growthRateDto.jobHpRate = 10;
        growthRateDto.jobLatkRate = 5;
        growthRateDto.jobCatkRate = 10;
        growthRateDto.jobDexRate = 5;
        growthRateDto.jobAgiRate = 10;
        growthRateDto.jobLukRate = 5;
        growthRateDto.jobLdefRate = 5;
        growthRateDto.jobCdefRate = 5;

        move = 5;

        job = new Job(jobName, jobLevel, skills, classChangeDestination, weaponTypes, statusDto, growthRateDto, move);
        jobDatabase.jobList.Add(job);

        jobName = JobName.最強の氷精;
        jobLevel = JobLevel.MASTER;

        skills = new List<Skill>() { Skill.自信満々, Skill.しぶとい心, Skill.凍える冷気, Skill.近接の叫び, Skill.弾幕の達人 };
        classChangeDestination = null;
        weaponTypes = new List<WeaponType>() { WeaponType.SHOT, WeaponType.STRIKE, };

        statusDto = new JobStatusDto();
        statusDto.jobHp = 3;
        statusDto.jobLatk = 2;
        statusDto.jobCatk = 3;
        statusDto.jobDex = 2;
        statusDto.jobAgi = 3;
        statusDto.jobLuk = 1;
        statusDto.jobLdef = 1;
        statusDto.jobCdef = 3;

        growthRateDto = new GrowthRateDto();
        growthRateDto.jobHpRate = 15;
        growthRateDto.jobLatkRate = 10;
        growthRateDto.jobCatkRate = 15;
        growthRateDto.jobDexRate = 10;
        growthRateDto.jobAgiRate = 15;
        growthRateDto.jobLukRate = 5;
        growthRateDto.jobLdefRate = 5;
        growthRateDto.jobCdefRate = 5;

        move = 6;

        job = new Job(jobName, jobLevel, skills, classChangeDestination, weaponTypes, statusDto, growthRateDto, move);
        jobDatabase.jobList.Add(job);

        jobName = JobName.冬将軍;
        jobLevel = JobLevel.MASTER;

        skills = new List<Skill>() { Skill.自信満々, Skill.しぶとい心, Skill.凍える冷気, Skill.移動封じ, Skill.武器の達人 };
        classChangeDestination = null;
        weaponTypes = new List<WeaponType>() { WeaponType.SHOT, WeaponType.STRIKE, };

        statusDto = new JobStatusDto();
        statusDto.jobHp = 4;
        statusDto.jobLatk = 4;
        statusDto.jobCatk = 2;
        statusDto.jobDex = 3;
        statusDto.jobAgi = 2;
        statusDto.jobLuk = 0;
        statusDto.jobLdef = 1;
        statusDto.jobCdef = 4;

        growthRateDto = new GrowthRateDto();
        growthRateDto.jobHpRate = 15;
        growthRateDto.jobLatkRate = 15;
        growthRateDto.jobCatkRate = 10;
        growthRateDto.jobDexRate = 15;
        growthRateDto.jobAgiRate = 10;
        growthRateDto.jobLukRate = 0;
        growthRateDto.jobLdefRate = 5;
        growthRateDto.jobCdefRate = 10;

        move = 6;

        job = new Job(jobName, jobLevel, skills, classChangeDestination, weaponTypes, statusDto, growthRateDto, move);
        jobDatabase.jobList.Add(job);

        //文
        jobName = JobName.伝統の幻想ブン屋;
        jobLevel = JobLevel.ADEPT;

        skills = new List<Skill>() { Skill.飛燕の一撃, Skill.すり抜け, Skill.鍵開け };
        classChangeDestination = new List<JobName>() { JobName.風神少女, JobName.天魔の密偵 };
        weaponTypes = new List<WeaponType>() { WeaponType.SHOT, WeaponType.STRIKE, };

        statusDto = new JobStatusDto();
        statusDto.jobHp = 1;
        statusDto.jobLatk = 1;
        statusDto.jobCatk = 0;
        statusDto.jobDex = 1;
        statusDto.jobAgi = 2;
        statusDto.jobLuk = 1;
        statusDto.jobLdef = 0;
        statusDto.jobCdef = 0;

        growthRateDto = new GrowthRateDto();
        growthRateDto.jobHpRate = 5;
        growthRateDto.jobLatkRate = 10;
        growthRateDto.jobCatkRate = 5;
        growthRateDto.jobDexRate = 10;
        growthRateDto.jobAgiRate = 10;
        growthRateDto.jobLukRate = 5;
        growthRateDto.jobLdefRate = 5;
        growthRateDto.jobCdefRate = 0;

        move = 6;

        job = new Job(jobName, jobLevel, skills, classChangeDestination, weaponTypes, statusDto, growthRateDto, move);
        jobDatabase.jobList.Add(job);

        jobName = JobName.風神少女;
        jobLevel = JobLevel.MASTER;
        skills = new List<Skill>() { Skill.飛燕の一撃, Skill.すり抜け, Skill.鍵開け, Skill.流星, Skill.疾風迅雷 };
        classChangeDestination = null;
        weaponTypes = new List<WeaponType>() { WeaponType.SHOT, WeaponType.STRIKE, };

        statusDto = new JobStatusDto();
        statusDto.jobHp = 2;
        statusDto.jobLatk = 3;
        statusDto.jobCatk = 2;
        statusDto.jobDex = 2;
        statusDto.jobAgi = 5;
        statusDto.jobLuk = 2;
        statusDto.jobLdef = 2;
        statusDto.jobCdef = 1;

        growthRateDto = new GrowthRateDto();
        growthRateDto.jobHpRate = 10;
        growthRateDto.jobLatkRate = 20;
        growthRateDto.jobCatkRate = 10;
        growthRateDto.jobDexRate = 10;
        growthRateDto.jobAgiRate = 15;
        growthRateDto.jobLukRate = 5;
        growthRateDto.jobLdefRate = 10;
        growthRateDto.jobCdefRate = 5;

        move = 8;

        job = new Job(jobName, jobLevel, skills, classChangeDestination, weaponTypes, statusDto, growthRateDto, move);
        jobDatabase.jobList.Add(job);

        jobName = JobName.天魔の密偵;
        jobLevel = JobLevel.MASTER;
        skills = new List<Skill>() { Skill.飛燕の一撃, Skill.すり抜け, Skill.鍵開け, Skill.再行動, Skill.一撃離脱, Skill.瞬殺 };
        classChangeDestination = null;
        weaponTypes = new List<WeaponType>() { WeaponType.SHOT, WeaponType.STRIKE, };

        statusDto = new JobStatusDto();
        statusDto.jobHp = 1;
        statusDto.jobLatk = 2;
        statusDto.jobCatk = 1;
        statusDto.jobDex = 4;
        statusDto.jobAgi = 5;
        statusDto.jobLuk = 3;
        statusDto.jobLdef = 1;
        statusDto.jobCdef = 2;

        growthRateDto = new GrowthRateDto();
        growthRateDto.jobHpRate = 5;
        growthRateDto.jobLatkRate = 15;
        growthRateDto.jobCatkRate = 5;
        growthRateDto.jobDexRate = 20;
        growthRateDto.jobAgiRate = 15;
        growthRateDto.jobLukRate = 15;
        growthRateDto.jobLdefRate = 5;
        growthRateDto.jobCdefRate = 10;

        move = 8;

        job = new Job(jobName, jobLevel, skills, classChangeDestination, weaponTypes, statusDto, growthRateDto, move);
        jobDatabase.jobList.Add(job);

        //うどんげ氏
        jobName = JobName.狂気の月の兎;
        jobLevel = JobLevel.ADEPT;

        skills = new List<Skill>() { Skill.狂気の瞳, Skill.よくきく薬, Skill.慧眼の一撃 };
        classChangeDestination = new List<JobName>() { JobName.賢者の弟子, JobName.綿月の戦士 };
        weaponTypes = new List<WeaponType>() { WeaponType.SHOT, WeaponType.LASER, };

        statusDto = new JobStatusDto();
        statusDto.jobHp = 1;
        statusDto.jobLatk = 1;
        statusDto.jobCatk = 0;
        statusDto.jobDex = 2;
        statusDto.jobAgi = 1;
        statusDto.jobLuk = 0;
        statusDto.jobLdef = 1;
        statusDto.jobCdef = 0;

        growthRateDto = new GrowthRateDto();
        growthRateDto.jobHpRate = 10;
        growthRateDto.jobLatkRate = 15;
        growthRateDto.jobCatkRate = 0;
        growthRateDto.jobDexRate = 10;
        growthRateDto.jobAgiRate = 15;
        growthRateDto.jobLukRate = 5;
        growthRateDto.jobLdefRate = 5;
        growthRateDto.jobCdefRate = 0;

        move = 5;

        job = new Job(jobName, jobLevel, skills, classChangeDestination, weaponTypes, statusDto, growthRateDto, move);
        jobDatabase.jobList.Add(job);

        jobName = JobName.賢者の弟子;
        jobLevel = JobLevel.MASTER;

        skills = new List<Skill>() { Skill.狂気の瞳, Skill.よくきく薬, Skill.慧眼の一撃, Skill.薬売り, Skill.弾幕の達人, Skill.国士無双の薬 };
        classChangeDestination = null;
        weaponTypes = new List<WeaponType>() { WeaponType.SHOT, WeaponType.LASER, };

        statusDto = new JobStatusDto();
        statusDto.jobHp = 3;
        statusDto.jobLatk = 3;
        statusDto.jobCatk = 0;
        statusDto.jobDex = 5;
        statusDto.jobAgi = 4;
        statusDto.jobLuk = 1;
        statusDto.jobLdef = 3;
        statusDto.jobCdef = 1;

        growthRateDto = new GrowthRateDto();
        growthRateDto.jobHpRate = 15;
        growthRateDto.jobLatkRate = 15;
        growthRateDto.jobCatkRate = 0;
        growthRateDto.jobDexRate = 15;
        growthRateDto.jobAgiRate = 15;
        growthRateDto.jobLukRate = 10;
        growthRateDto.jobLdefRate = 10;
        growthRateDto.jobCdefRate = 5;

        move = 6;

        job = new Job(jobName, jobLevel, skills, classChangeDestination, weaponTypes, statusDto, growthRateDto, move);
        jobDatabase.jobList.Add(job);

        //美鈴
        jobName = JobName.門番;
        jobLevel = JobLevel.NOVICE;

        skills = new List<Skill>() { Skill.紅魔の盾, Skill.金剛の一撃 };
        classChangeDestination = new List<JobName>() { JobName.虹色の門番 };
        weaponTypes = new List<WeaponType>() { WeaponType.STRIKE };

        statusDto = new JobStatusDto();
        statusDto.jobHp = 0;
        statusDto.jobLatk = 0;
        statusDto.jobCatk = 0;
        statusDto.jobDex = 0;
        statusDto.jobAgi = 0;
        statusDto.jobLuk = 0;
        statusDto.jobLdef = 0;
        statusDto.jobCdef = 0;

        growthRateDto = new GrowthRateDto();
        growthRateDto.jobHpRate = 15;
        growthRateDto.jobLatkRate = 5;
        growthRateDto.jobCatkRate = 10;
        growthRateDto.jobDexRate = 5;
        growthRateDto.jobAgiRate = 5;
        growthRateDto.jobLukRate = 0;
        growthRateDto.jobLdefRate = 0;
        growthRateDto.jobCdefRate = 10;

        move = 4;

        job = new Job(jobName, jobLevel, skills, classChangeDestination, weaponTypes, statusDto, growthRateDto, move);
        jobDatabase.jobList.Add(job);

        jobName = JobName.虹色の門番;
        jobLevel = JobLevel.ADEPT;

        skills = new List<Skill>() { Skill.紅魔の盾, Skill.金剛の一撃, Skill.瞑想 };
        classChangeDestination = new List<JobName>() { JobName.悪魔の守護者, JobName.龍の末裔 };
        weaponTypes = new List<WeaponType>() { WeaponType.STRIKE, WeaponType.HEAL, };

        statusDto = new JobStatusDto();
        statusDto.jobHp = 2;
        statusDto.jobLatk = 0;
        statusDto.jobCatk = 1;
        statusDto.jobDex = 0;
        statusDto.jobAgi = 1;
        statusDto.jobLuk = 0;
        statusDto.jobLdef = 1;
        statusDto.jobCdef = 2;

        growthRateDto = new GrowthRateDto();
        growthRateDto.jobHpRate = 15;
        growthRateDto.jobLatkRate = 5;
        growthRateDto.jobCatkRate = 10;
        growthRateDto.jobDexRate = 5;
        growthRateDto.jobAgiRate = 5;
        growthRateDto.jobLukRate = 0;
        growthRateDto.jobLdefRate = 0;
        growthRateDto.jobCdefRate = 10;

        move = 5;

        job = new Job(jobName, jobLevel, skills, classChangeDestination, weaponTypes, statusDto, growthRateDto, move);
        jobDatabase.jobList.Add(job);

        jobName = JobName.悪魔の守護者;
        jobLevel = JobLevel.MASTER;

        skills = new List<Skill>() { Skill.紅魔の盾, Skill.金剛の一撃, Skill.瞑想,Skill.近防の応援, Skill.武器の達人 };
        classChangeDestination = null;
        weaponTypes = new List<WeaponType>() { WeaponType.STRIKE, WeaponType.HEAL, };

        statusDto = new JobStatusDto();
        statusDto.jobHp = 6;
        statusDto.jobLatk = 1;
        statusDto.jobCatk = 2;
        statusDto.jobDex = 3;
        statusDto.jobAgi = 2;
        statusDto.jobLuk = 1;
        statusDto.jobLdef = 1;
        statusDto.jobCdef = 4;

        growthRateDto = new GrowthRateDto();
        growthRateDto.jobHpRate = 20;
        growthRateDto.jobLatkRate = 5;
        growthRateDto.jobCatkRate = 10;
        growthRateDto.jobDexRate = 10;
        growthRateDto.jobAgiRate = 10;
        growthRateDto.jobLukRate = 5;
        growthRateDto.jobLdefRate = 10;
        growthRateDto.jobCdefRate = 15;

        move = 6;

        job = new Job(jobName, jobLevel, skills, classChangeDestination, weaponTypes, statusDto, growthRateDto, move);
        jobDatabase.jobList.Add(job);

        jobName = JobName.龍の末裔;
        jobLevel = JobLevel.MASTER;

        skills = new List<Skill>() { Skill.紅魔の盾, Skill.金剛の一撃, Skill.瞑想, Skill.切り返し, Skill.必殺 };
        classChangeDestination = null;
        weaponTypes = new List<WeaponType>() { WeaponType.STRIKE, WeaponType.HEAL, };

        statusDto = new JobStatusDto();
        statusDto.jobHp = 4;
        statusDto.jobLatk = 2;
        statusDto.jobCatk = 4;
        statusDto.jobDex = 3;
        statusDto.jobAgi = 3;
        statusDto.jobLuk = 1;
        statusDto.jobLdef = 2;
        statusDto.jobCdef = 2;

        growthRateDto = new GrowthRateDto();
        growthRateDto.jobHpRate = 20;
        growthRateDto.jobLatkRate = 20;
        growthRateDto.jobCatkRate = 10;
        growthRateDto.jobDexRate = 5;
        growthRateDto.jobAgiRate = 15;
        growthRateDto.jobLukRate = 0;
        growthRateDto.jobLdefRate = 5;
        growthRateDto.jobCdefRate = 5;

        move = 6;

        jobName = JobName.小悪魔;
        jobLevel = JobLevel.NOVICE;

        List<Skill> koaSkills1 = new List<Skill>() { Skill.鼓舞, Skill.ご奉仕の喜び };
        classChangeDestination = new List<JobName>() { JobName.インプ };
        weaponTypes = new List<WeaponType>() { WeaponType.SHOT, WeaponType.HEAL, };

        statusDto = new JobStatusDto();
        statusDto.jobHp = 0;
        statusDto.jobLatk = 0;
        statusDto.jobCatk = 0;
        statusDto.jobDex = 0;
        statusDto.jobAgi = 0;
        statusDto.jobLuk = 0;
        statusDto.jobLdef = 0;
        statusDto.jobCdef = 0;

        growthRateDto = new GrowthRateDto();
        growthRateDto.jobHpRate = 0;
        growthRateDto.jobLatkRate = 10;
        growthRateDto.jobCatkRate = 5;
        growthRateDto.jobDexRate = 10;
        growthRateDto.jobAgiRate = 15;
        growthRateDto.jobLukRate = 5;
        growthRateDto.jobLdefRate = 10;
        growthRateDto.jobCdefRate = 0;

        move = 4;

        job = new Job(jobName, jobLevel, koaSkills1, classChangeDestination, weaponTypes, statusDto, growthRateDto, move);
        jobDatabase.jobList.Add(job);

        jobName = JobName.インプ;
        jobLevel = JobLevel.ADEPT;

        List<Skill> koaSkills2 = koaSkills1;
        koaSkills2.Add(Skill.紅の花);
        classChangeDestination = new List<JobName>() { JobName.サキュバス, JobName.グレモリィ };
        weaponTypes = new List<WeaponType>() { WeaponType.SHOT, WeaponType.HEAL, };

        statusDto = new JobStatusDto();
        statusDto.jobHp = 1;
        statusDto.jobLatk = 1;
        statusDto.jobCatk = 0;
        statusDto.jobDex = 1;
        statusDto.jobAgi = 1;
        statusDto.jobLuk = 1;
        statusDto.jobLdef = 1;
        statusDto.jobCdef = 0;

        growthRateDto = new GrowthRateDto();
        growthRateDto.jobHpRate = 0;
        growthRateDto.jobLatkRate = 10;
        growthRateDto.jobCatkRate = 5;
        growthRateDto.jobDexRate = 10;
        growthRateDto.jobAgiRate = 15;
        growthRateDto.jobLukRate = 5;
        growthRateDto.jobLdefRate = 10;
        growthRateDto.jobCdefRate = 0;

        move = 5;

        job = new Job(jobName, jobLevel, koaSkills2, classChangeDestination, weaponTypes, statusDto, growthRateDto, move);
        jobDatabase.jobList.Add(job);

        jobName = JobName.サキュバス;
        jobLevel = JobLevel.MASTER;

        List<Skill> koaSkills3 = koaSkills2;
        koaSkills3.AddRange(new List<Skill>{Skill.技の叫び, Skill.遠攻封じ});
        classChangeDestination = null;
        weaponTypes = new List<WeaponType>() { WeaponType.SHOT, WeaponType.HEAL, };

        statusDto = new JobStatusDto();
        statusDto.jobHp = 2;
        statusDto.jobLatk = 4;
        statusDto.jobCatk = 1;
        statusDto.jobDex = 2;
        statusDto.jobAgi = 4;
        statusDto.jobLuk = 2;
        statusDto.jobLdef = 2;
        statusDto.jobCdef = 1;

        growthRateDto = new GrowthRateDto();
        growthRateDto.jobHpRate = 0;
        growthRateDto.jobLatkRate = 20;
        growthRateDto.jobCatkRate = 10;
        growthRateDto.jobDexRate = 10;
        growthRateDto.jobAgiRate = 20;
        growthRateDto.jobLukRate = 5;
        growthRateDto.jobLdefRate = 10;
        growthRateDto.jobCdefRate = 5;

        move = 6;

        job = new Job(jobName, jobLevel, koaSkills3, classChangeDestination, weaponTypes, statusDto, growthRateDto, move);
        jobDatabase.jobList.Add(job);


        jobName = JobName.グレモリィ;
        jobLevel = JobLevel.MASTER;

        koaSkills3 = koaSkills2;
        koaSkills3.AddRange(new List<Skill> { Skill.遠防の叫び, Skill.遠防封じ });
        classChangeDestination = null;
        weaponTypes = new List<WeaponType>() { WeaponType.SHOT, WeaponType.HEAL, };

        statusDto = new JobStatusDto();
        statusDto.jobHp = 3;
        statusDto.jobLatk = 3;
        statusDto.jobCatk = 1;
        statusDto.jobDex = 2;
        statusDto.jobAgi = 3;
        statusDto.jobLuk = 3;
        statusDto.jobLdef = 3;
        statusDto.jobCdef = 1;

        growthRateDto = new GrowthRateDto();
        growthRateDto.jobHpRate = 10;
        growthRateDto.jobLatkRate = 15;
        growthRateDto.jobCatkRate = 5;
        growthRateDto.jobDexRate = 15;
        growthRateDto.jobAgiRate = 15;
        growthRateDto.jobLukRate = 5;
        growthRateDto.jobLdefRate = 15;
        growthRateDto.jobCdefRate = 5;

        move = 6;

        job = new Job(jobName, jobLevel, koaSkills3, classChangeDestination, weaponTypes, statusDto, growthRateDto, move);
        jobDatabase.jobList.Add(job);


        //パチュリーの職業
        jobName = JobName.魔女;
        jobLevel = JobLevel.NOVICE;

        List<Skill> skills1 = new List<Skill>() { Skill.密室の守り, Skill.紫の花 };
        classChangeDestination = new List<JobName>() { JobName.知識と日陰の少女 };
        weaponTypes = new List<WeaponType>() { WeaponType.SHOT, WeaponType.LASER };

        statusDto = new JobStatusDto();
        statusDto.jobHp = 0;
        statusDto.jobLatk = 0;
        statusDto.jobCatk = 0;
        statusDto.jobDex = 0;
        statusDto.jobAgi = 0;
        statusDto.jobLuk = 0;
        statusDto.jobLdef = 0;
        statusDto.jobCdef = 0;

        growthRateDto = new GrowthRateDto();
        growthRateDto.jobHpRate = 0;
        growthRateDto.jobLatkRate = 15;
        growthRateDto.jobCatkRate = 0;
        growthRateDto.jobDexRate = 10;
        growthRateDto.jobAgiRate = 10;
        growthRateDto.jobLukRate = 10;
        growthRateDto.jobLdefRate = 10;
        growthRateDto.jobCdefRate = 0;

        move = 4;

        job = new Job(jobName, jobLevel, skills1, classChangeDestination, weaponTypes, statusDto, growthRateDto, move);
        jobDatabase.jobList.Add(job);

        jobName = JobName.知識と日陰の少女;
        jobLevel = JobLevel.ADEPT;

        List<Skill> skills2 = skills1;
        skills2.Add(Skill.魔女の一撃);
        classChangeDestination = new List<JobName>() { JobName.動かない大図書館, JobName.七曜の魔女 };
        weaponTypes = new List<WeaponType>() { WeaponType.SHOT, WeaponType.LASER };

        statusDto = new JobStatusDto();
        statusDto.jobHp = 0;
        statusDto.jobLatk = 2;
        statusDto.jobCatk = 0;
        statusDto.jobDex = 3;
        statusDto.jobAgi = 1;
        statusDto.jobLuk = 1;
        statusDto.jobLdef = 1;
        statusDto.jobCdef = 0;

        growthRateDto = new GrowthRateDto();
        growthRateDto.jobHpRate = 0;
        growthRateDto.jobLatkRate = 15;
        growthRateDto.jobCatkRate = 0;
        growthRateDto.jobDexRate = 10;
        growthRateDto.jobAgiRate = 10;
        growthRateDto.jobLukRate = 10;
        growthRateDto.jobLdefRate = 10;
        growthRateDto.jobCdefRate = 0;

        move = 5;

        job = new Job(jobName, jobLevel, skills2, classChangeDestination, weaponTypes, statusDto, growthRateDto, move);
        jobDatabase.jobList.Add(job);

        jobName = JobName.動かない大図書館;
        jobLevel = JobLevel.MASTER;

        List<Skill> skills3 = skills2;
        skills3.AddRange(new List<Skill> { Skill.魔力障壁, Skill.太陽 });
        classChangeDestination = null;
        weaponTypes = new List<WeaponType>() { WeaponType.SHOT, WeaponType.LASER, WeaponType.HEAL };

        statusDto = new JobStatusDto();
        statusDto.jobHp = 2;
        statusDto.jobLatk = 3;
        statusDto.jobCatk = 0;
        statusDto.jobDex = 7;
        statusDto.jobAgi = 1;
        statusDto.jobLuk = 1;
        statusDto.jobLdef = 4;
        statusDto.jobCdef = 1;

        growthRateDto = new GrowthRateDto();
        growthRateDto.jobHpRate = 0;
        growthRateDto.jobLatkRate = 15;
        growthRateDto.jobCatkRate = 0;
        growthRateDto.jobDexRate = 25;
        growthRateDto.jobAgiRate = 10;
        growthRateDto.jobLukRate = 10;
        growthRateDto.jobLdefRate = 20;
        growthRateDto.jobCdefRate = 5;

        move = 6;

        job = new Job(jobName, jobLevel, skills3, classChangeDestination, weaponTypes, statusDto, growthRateDto, move);
        jobDatabase.jobList.Add(job);


        jobName = JobName.七曜の魔女;
        jobLevel = JobLevel.MASTER;

        skills3 = skills2;
        skills3.AddRange(new List<Skill> { Skill.魔力吸収, Skill.月光 });
        classChangeDestination = null;
        weaponTypes = new List<WeaponType>() { WeaponType.SHOT, WeaponType.LASER,WeaponType.STRIKE };

        statusDto = new JobStatusDto();
        statusDto.jobHp = 1;
        statusDto.jobLatk = 5;
        statusDto.jobCatk = 1;
        statusDto.jobDex = 5;
        statusDto.jobAgi = 3;
        statusDto.jobLuk = 1;
        statusDto.jobLdef = 3;
        statusDto.jobCdef = 0;

        growthRateDto = new GrowthRateDto();
        growthRateDto.jobHpRate = 0;
        growthRateDto.jobLatkRate = 20;
        growthRateDto.jobCatkRate = 0;
        growthRateDto.jobDexRate = 15;
        growthRateDto.jobAgiRate = 20;
        growthRateDto.jobLukRate = 10;
        growthRateDto.jobLdefRate = 15;
        growthRateDto.jobCdefRate = 0;

        move = 6;

        job = new Job(jobName, jobLevel, skills3, classChangeDestination, weaponTypes, statusDto, growthRateDto, move);
        jobDatabase.jobList.Add(job);


        //咲夜の職業
        jobName = JobName.紅魔館のメイド;
        jobLevel = JobLevel.NOVICE;

        skills1 = new List<Skill>() { Skill.完璧主義, Skill.鍵開け };
        classChangeDestination = new List<JobName>() { JobName.電光石火のメイド };
        weaponTypes = new List<WeaponType>() { WeaponType.SHOT, WeaponType.STRIKE };

        statusDto = new JobStatusDto();
        statusDto.jobHp = 0;
        statusDto.jobLatk = 0;
        statusDto.jobCatk = 0;
        statusDto.jobDex = 0;
        statusDto.jobAgi = 0;
        statusDto.jobLuk = 0;
        statusDto.jobLdef = 0;
        statusDto.jobCdef = 0;

        growthRateDto = new GrowthRateDto();
        growthRateDto.jobHpRate = 5;
        growthRateDto.jobLatkRate = 15;
        growthRateDto.jobCatkRate = 10;
        growthRateDto.jobDexRate = 10;
        growthRateDto.jobAgiRate = 10;
        growthRateDto.jobLukRate = 5;
        growthRateDto.jobLdefRate = 10;
        growthRateDto.jobCdefRate = 5;

        move = 4;

        job = new Job(jobName, jobLevel, skills1, classChangeDestination, weaponTypes, statusDto, growthRateDto, move);
        jobDatabase.jobList.Add(job);

        jobName = JobName.電光石火のメイド;
        jobLevel = JobLevel.ADEPT;

        skills2 = skills1;
        skills2.Add(Skill.ミスディレクション);
        classChangeDestination = new List<JobName>() { JobName.完全で瀟洒な従者, JobName.銀の手品師 };
        weaponTypes = new List<WeaponType>() { WeaponType.SHOT, WeaponType.STRIKE };

        statusDto = new JobStatusDto();
        statusDto.jobHp = 1;
        statusDto.jobLatk = 2;
        statusDto.jobCatk = 0;
        statusDto.jobDex = 1;
        statusDto.jobAgi = 1;
        statusDto.jobLuk = 1;
        statusDto.jobLdef = 1;
        statusDto.jobCdef = 0;

        growthRateDto = new GrowthRateDto();
        growthRateDto.jobHpRate = 5;
        growthRateDto.jobLatkRate = 15;
        growthRateDto.jobCatkRate = 10;
        growthRateDto.jobDexRate = 10;
        growthRateDto.jobAgiRate = 10;
        growthRateDto.jobLukRate = 5;
        growthRateDto.jobLdefRate = 10;
        growthRateDto.jobCdefRate = 5;

        move = 6;

        job = new Job(jobName, jobLevel, skills2, classChangeDestination, weaponTypes, statusDto, growthRateDto, move);
        jobDatabase.jobList.Add(job);

        jobName = JobName.完全で瀟洒な従者;
        jobLevel = JobLevel.MASTER;

        skills3 = skills2;
        skills3.AddRange(new List<Skill> { Skill.パーフェクトメイド, Skill.武器の達人 });
        classChangeDestination = null;
        weaponTypes = new List<WeaponType>() { WeaponType.SHOT, WeaponType.STRIKE };

        statusDto = new JobStatusDto();
        statusDto.jobHp = 3;
        statusDto.jobLatk = 4;
        statusDto.jobCatk = 2;
        statusDto.jobDex = 2;
        statusDto.jobAgi = 3;
        statusDto.jobLuk = 2;
        statusDto.jobLdef = 3;
        statusDto.jobCdef = 2;

        growthRateDto = new GrowthRateDto();
        growthRateDto.jobHpRate = 5;
        growthRateDto.jobLatkRate = 15;
        growthRateDto.jobCatkRate = 10;
        growthRateDto.jobDexRate = 10;
        growthRateDto.jobAgiRate = 10;
        growthRateDto.jobLukRate = 5;
        growthRateDto.jobLdefRate = 10;
        growthRateDto.jobCdefRate = 5;

        move = 6;

        job = new Job(jobName, jobLevel, skills3, classChangeDestination, weaponTypes, statusDto, growthRateDto, move);
        jobDatabase.jobList.Add(job);


        jobName = JobName.銀の手品師;
        jobLevel = JobLevel.MASTER;

        skills3 = skills2;
        skills3.AddRange(new List<Skill> { Skill.再行動, Skill.チェンジリングマジック, Skill.速さの叫び });
        classChangeDestination = null;
        weaponTypes = new List<WeaponType>() { WeaponType.SHOT, WeaponType.STRIKE };

        statusDto = new JobStatusDto();
        statusDto.jobHp = 3;
        statusDto.jobLatk = 3;
        statusDto.jobCatk = 2;
        statusDto.jobDex = 4;
        statusDto.jobAgi = 2;
        statusDto.jobLuk = 2;
        statusDto.jobLdef = 3;
        statusDto.jobCdef = 3;

        growthRateDto = new GrowthRateDto();
        growthRateDto.jobHpRate = 10;
        growthRateDto.jobLatkRate = 15;
        growthRateDto.jobCatkRate = 15;
        growthRateDto.jobDexRate = 15;
        growthRateDto.jobAgiRate = 10;
        growthRateDto.jobLukRate = 10;
        growthRateDto.jobLdefRate = 10;
        growthRateDto.jobCdefRate = 10;

        move = 6;

        job = new Job(jobName, jobLevel, skills3, classChangeDestination, weaponTypes, statusDto, growthRateDto, move);
        jobDatabase.jobList.Add(job);


        //咲夜の職業
        jobName = JobName.紅魔館のメイド;
        jobLevel = JobLevel.NOVICE;

        skills1 = new List<Skill>() { Skill.完璧主義, Skill.鍵開け };
        classChangeDestination = new List<JobName>() { JobName.電光石火のメイド };
        weaponTypes = new List<WeaponType>() { WeaponType.SHOT, WeaponType.STRIKE };

        statusDto = new JobStatusDto();
        statusDto.jobHp = 0;
        statusDto.jobLatk = 0;
        statusDto.jobCatk = 0;
        statusDto.jobDex = 0;
        statusDto.jobAgi = 0;
        statusDto.jobLuk = 0;
        statusDto.jobLdef = 0;
        statusDto.jobCdef = 0;

        growthRateDto = new GrowthRateDto();
        growthRateDto.jobHpRate = 5;
        growthRateDto.jobLatkRate = 15;
        growthRateDto.jobCatkRate = 10;
        growthRateDto.jobDexRate = 10;
        growthRateDto.jobAgiRate = 10;
        growthRateDto.jobLukRate = 5;
        growthRateDto.jobLdefRate = 10;
        growthRateDto.jobCdefRate = 5;

        move = 4;

        job = new Job(jobName, jobLevel, skills1, classChangeDestination, weaponTypes, statusDto, growthRateDto, move);
        jobDatabase.jobList.Add(job);


        jobName = JobName.電光石火のメイド;
        jobLevel = JobLevel.ADEPT;

        skills2 = skills1;
        skills2.Add(Skill.ミスディレクション);
        classChangeDestination = new List<JobName>() { JobName.完全で瀟洒な従者, JobName.銀の手品師 };
        weaponTypes = new List<WeaponType>() { WeaponType.SHOT, WeaponType.STRIKE };

        statusDto = new JobStatusDto();
        statusDto.jobHp = 1;
        statusDto.jobLatk = 2;
        statusDto.jobCatk = 0;
        statusDto.jobDex = 1;
        statusDto.jobAgi = 1;
        statusDto.jobLuk = 1;
        statusDto.jobLdef = 1;
        statusDto.jobCdef = 0;

        growthRateDto = new GrowthRateDto();
        growthRateDto.jobHpRate = 5;
        growthRateDto.jobLatkRate = 15;
        growthRateDto.jobCatkRate = 10;
        growthRateDto.jobDexRate = 10;
        growthRateDto.jobAgiRate = 10;
        growthRateDto.jobLukRate = 5;
        growthRateDto.jobLdefRate = 10;
        growthRateDto.jobCdefRate = 5;

        move = 6;

        job = new Job(jobName, jobLevel, skills2, classChangeDestination, weaponTypes, statusDto, growthRateDto, move);
        jobDatabase.jobList.Add(job);

        jobName = JobName.完全で瀟洒な従者;
        jobLevel = JobLevel.MASTER;

        skills3 = skills2;
        skills3.AddRange(new List<Skill> { Skill.パーフェクトメイド, Skill.武器の達人 });
        classChangeDestination = null;
        weaponTypes = new List<WeaponType>() { WeaponType.SHOT, WeaponType.STRIKE };

        statusDto = new JobStatusDto();
        statusDto.jobHp = 3;
        statusDto.jobLatk = 4;
        statusDto.jobCatk = 2;
        statusDto.jobDex = 2;
        statusDto.jobAgi = 3;
        statusDto.jobLuk = 2;
        statusDto.jobLdef = 3;
        statusDto.jobCdef = 2;

        growthRateDto = new GrowthRateDto();
        growthRateDto.jobHpRate = 10;
        growthRateDto.jobLatkRate = 20;
        growthRateDto.jobCatkRate = 10;
        growthRateDto.jobDexRate = 10;
        growthRateDto.jobAgiRate = 15;
        growthRateDto.jobLukRate = 10;
        growthRateDto.jobLdefRate = 10;
        growthRateDto.jobCdefRate = 5;

        move = 6;

        job = new Job(jobName, jobLevel, skills3, classChangeDestination, weaponTypes, statusDto, growthRateDto, move);
        jobDatabase.jobList.Add(job);


        jobName = JobName.銀の手品師;
        jobLevel = JobLevel.MASTER;

        skills3 = skills2;
        skills3.AddRange(new List<Skill> { Skill.再行動, Skill.チェンジリングマジック, Skill.速さの叫び });
        classChangeDestination = null;
        weaponTypes = new List<WeaponType>() { WeaponType.SHOT, WeaponType.STRIKE };

        statusDto = new JobStatusDto();
        statusDto.jobHp = 3;
        statusDto.jobLatk = 3;
        statusDto.jobCatk = 2;
        statusDto.jobDex = 4;
        statusDto.jobAgi = 2;
        statusDto.jobLuk = 2;
        statusDto.jobLdef = 3;
        statusDto.jobCdef = 3;

        growthRateDto = new GrowthRateDto();
        growthRateDto.jobHpRate = 10;
        growthRateDto.jobLatkRate = 15;
        growthRateDto.jobCatkRate = 15;
        growthRateDto.jobDexRate = 15;
        growthRateDto.jobAgiRate = 10;
        growthRateDto.jobLukRate = 10;
        growthRateDto.jobLdefRate = 10;
        growthRateDto.jobCdefRate = 10;

        move = 7;

        job = new Job(jobName, jobLevel, skills3, classChangeDestination, weaponTypes, statusDto, growthRateDto, move);
        jobDatabase.jobList.Add(job);

        //レミリア
        jobName = JobName.紅い悪魔;
        jobLevel = JobLevel.ADEPT;

        skills1 = new List<Skill>() { Skill.鬼神の一撃, Skill.カリスマ, Skill.威風堂々 };
        classChangeDestination = new List<JobName>() { JobName.永遠に紅い幼き月, JobName.紅色のノクターナルデビル };
        weaponTypes = new List<WeaponType>() { WeaponType.STRIKE };

        statusDto = new JobStatusDto();
        statusDto.jobHp = 1;
        statusDto.jobLatk = 1;
        statusDto.jobCatk = 1;
        statusDto.jobDex = 0;
        statusDto.jobAgi = 1;
        statusDto.jobLuk = 2;
        statusDto.jobLdef = 1;
        statusDto.jobCdef = 0;

        growthRateDto = new GrowthRateDto();
        growthRateDto.jobHpRate = 10;
        growthRateDto.jobLatkRate = 15;
        growthRateDto.jobCatkRate = 10;
        growthRateDto.jobDexRate = 5;
        growthRateDto.jobAgiRate = 10;
        growthRateDto.jobLukRate = 10;
        growthRateDto.jobLdefRate = 5;
        growthRateDto.jobCdefRate = 5;

        move = 5;

        job = new Job(jobName, jobLevel, skills1, classChangeDestination, weaponTypes, statusDto, growthRateDto, move);
        jobDatabase.jobList.Add(job);

        jobName = JobName.永遠に紅い幼き月;
        jobLevel = JobLevel.MASTER;

        skills2 = skills1;
        skills2.AddRange(new List<Skill> { Skill.武器の達人, Skill.運命操作 });
        classChangeDestination = null;
        weaponTypes = new List<WeaponType>() { WeaponType.STRIKE };

        statusDto = new JobStatusDto();
        statusDto.jobHp = 3;
        statusDto.jobLatk = 2;
        statusDto.jobCatk = 5;
        statusDto.jobDex = 2;
        statusDto.jobAgi = 3;
        statusDto.jobLuk = 2;
        statusDto.jobLdef = 2;
        statusDto.jobCdef = 1;

        growthRateDto = new GrowthRateDto();
        growthRateDto.jobHpRate = 15;
        growthRateDto.jobLatkRate = 15;
        growthRateDto.jobCatkRate = 20;
        growthRateDto.jobDexRate = 5;
        growthRateDto.jobAgiRate = 15;
        growthRateDto.jobLukRate = 10;
        growthRateDto.jobLdefRate = 10;
        growthRateDto.jobCdefRate = 5;

        move = 6;

        job = new Job(jobName, jobLevel, skills2, classChangeDestination, weaponTypes, statusDto, growthRateDto, move);
        jobDatabase.jobList.Add(job);


        jobName = JobName.紅色のノクターナルデビル;
        jobLevel = JobLevel.MASTER;

        skills2 = skills1;
        skills2.AddRange(new List<Skill> { Skill.再行動, Skill.最強の体術, Skill.運命予知 });
        classChangeDestination = null;
        weaponTypes = new List<WeaponType>() {WeaponType.STRIKE };

        statusDto = new JobStatusDto();
        statusDto.jobHp = 4;
        statusDto.jobLatk = 2;
        statusDto.jobCatk = 4;
        statusDto.jobDex = 3;
        statusDto.jobAgi = 2;
        statusDto.jobLuk = 3;
        statusDto.jobLdef = 1;
        statusDto.jobCdef = 3;

        growthRateDto = new GrowthRateDto();
        growthRateDto.jobHpRate = 15;
        growthRateDto.jobLatkRate = 10;
        growthRateDto.jobCatkRate = 15;
        growthRateDto.jobDexRate = 10;
        growthRateDto.jobAgiRate = 10;
        growthRateDto.jobLukRate = 10;
        growthRateDto.jobLdefRate = 5;
        growthRateDto.jobCdefRate = 15;

        move = 7;

        job = new Job(jobName, jobLevel, skills2, classChangeDestination, weaponTypes, statusDto, growthRateDto, move);
        jobDatabase.jobList.Add(job);

        //フランの職業
        jobName = JobName.紅のカタストロフ;
        jobLevel = JobLevel.MASTER;

        skills1 = new List<Skill>() { Skill.鬼神の一撃, Skill.武器の達人, Skill.直死の一撃, Skill.死線, Skill.怒り };
        classChangeDestination = null;
        weaponTypes = new List<WeaponType>() { WeaponType.STRIKE };

        statusDto = new JobStatusDto();
        statusDto.jobHp = 1;
        statusDto.jobLatk = 5;
        statusDto.jobCatk = 7;
        statusDto.jobDex = 2;
        statusDto.jobAgi = 4;
        statusDto.jobLuk = 0;
        statusDto.jobLdef = 1;
        statusDto.jobCdef = 1;

        growthRateDto = new GrowthRateDto();
        growthRateDto.jobHpRate = 10;
        growthRateDto.jobLatkRate = 10;
        growthRateDto.jobCatkRate = 20;
        growthRateDto.jobDexRate = 10;
        growthRateDto.jobAgiRate = 15;
        growthRateDto.jobLukRate = 0;
        growthRateDto.jobLdefRate = 5;
        growthRateDto.jobCdefRate = 10;

        move = 6;

        job = new Job(jobName, jobLevel, skills1, classChangeDestination, weaponTypes, statusDto, growthRateDto, move);
        jobDatabase.jobList.Add(job);

        //以下、敵専用クラス ルールとしてステータス補正は行う
        //成長率の参照はしない
        //スキルはこれで管理する
        jobName = JobName.妖精;
        jobLevel = JobLevel.ADEPT;

        skills = new List<Skill>() { };
        classChangeDestination = null;
        weaponTypes = new List<WeaponType>() { WeaponType.STRIKE , WeaponType.SHOT, WeaponType.LASER};

        statusDto = new JobStatusDto();
        statusDto.jobHp = 0;
        statusDto.jobLatk = 0;
        statusDto.jobCatk = 0;
        statusDto.jobDex = 0;
        statusDto.jobAgi = 0;
        statusDto.jobLuk = 0;
        statusDto.jobLdef = 0;
        statusDto.jobCdef = 0;

        move = 4;

        job = new Job(jobName, jobLevel, skills, classChangeDestination, weaponTypes, statusDto, growthRateDto, move);
        jobDatabase.jobList.Add(job);

        jobName = JobName.メイド妖精;
        jobLevel = JobLevel.MASTER;

        skills = new List<Skill>() { };
        classChangeDestination = null;
        weaponTypes = new List<WeaponType>() { WeaponType.STRIKE, WeaponType.SHOT, WeaponType.LASER };

        statusDto = new JobStatusDto();
        statusDto.jobHp = 5;
        statusDto.jobLatk = 3;
        statusDto.jobCatk = 2;
        statusDto.jobDex = 2;
        statusDto.jobAgi = 2;
        statusDto.jobLuk = 2;
        statusDto.jobLdef = 2;
        statusDto.jobCdef = 2;

        move = 5;

        job = new Job(jobName, jobLevel, skills, classChangeDestination, weaponTypes, statusDto, growthRateDto, move);
        jobDatabase.jobList.Add(job);

        jobName = JobName.毛玉;
        jobLevel = JobLevel.ADEPT;

        skills = new List<Skill>() { };
        classChangeDestination = null;
        weaponTypes = new List<WeaponType>() { WeaponType.STRIKE};

        statusDto = new JobStatusDto();
        statusDto.jobHp = 0;
        statusDto.jobLatk = 0;
        statusDto.jobCatk = 0;
        statusDto.jobDex = 0;
        statusDto.jobAgi = 0;
        statusDto.jobLuk = 0;
        statusDto.jobLdef = 0;
        statusDto.jobCdef = 0;

        move = 5;

        job = new Job(jobName, jobLevel, skills, classChangeDestination, weaponTypes, statusDto, growthRateDto, move);
        jobDatabase.jobList.Add(job);

        jobName = JobName.妖獣;
        jobLevel = JobLevel.MASTER;

        skills = new List<Skill>() { };
        classChangeDestination = null;
        weaponTypes = new List<WeaponType>() { WeaponType.STRIKE};

        statusDto = new JobStatusDto();
        statusDto.jobHp = 4;
        statusDto.jobLatk = 2;
        statusDto.jobCatk = 3;
        statusDto.jobDex = 2;
        statusDto.jobAgi = 4;
        statusDto.jobLuk = 4;
        statusDto.jobLdef = 2;
        statusDto.jobCdef = 3;

        move = 6;

        job = new Job(jobName, jobLevel, skills, classChangeDestination, weaponTypes, statusDto, growthRateDto, move);
        jobDatabase.jobList.Add(job);

        jobName = JobName.魔導書;
        jobLevel = JobLevel.ADEPT;

        skills = new List<Skill>() { };
        classChangeDestination = null;
        weaponTypes = new List<WeaponType>() { WeaponType.LASER };

        statusDto = new JobStatusDto();
        statusDto.jobHp = 0;
        statusDto.jobLatk = 0;
        statusDto.jobCatk = 0;
        statusDto.jobDex = 0;
        statusDto.jobAgi = 0;
        statusDto.jobLuk = 0;
        statusDto.jobLdef = 0;
        statusDto.jobCdef = 0;

        move = 4;

        job = new Job(jobName, jobLevel, skills, classChangeDestination, weaponTypes, statusDto, growthRateDto, move);
        jobDatabase.jobList.Add(job);

        jobName = JobName.グリモワール;
        jobLevel = JobLevel.MASTER;

        skills = new List<Skill>() { };
        classChangeDestination = null;
        weaponTypes = new List<WeaponType>() { WeaponType.LASER };

        statusDto = new JobStatusDto();
        statusDto.jobHp = 4;
        statusDto.jobLatk = 4;
        statusDto.jobCatk = 1;
        statusDto.jobDex = 4;
        statusDto.jobAgi = 2;
        statusDto.jobLuk = 1;
        statusDto.jobLdef = 3;
        statusDto.jobCdef = 2;

        move = 5;

        job = new Job(jobName, jobLevel, skills, classChangeDestination, weaponTypes, statusDto, growthRateDto, move);
        jobDatabase.jobList.Add(job);


        jobName = JobName.使い魔;
        jobLevel = JobLevel.ADEPT;

        skills = new List<Skill>() { };
        classChangeDestination = null;
        weaponTypes = new List<WeaponType>() { WeaponType.STRIKE, WeaponType.SHOT, WeaponType.LASER };

        statusDto = new JobStatusDto();
        statusDto.jobHp = 0;
        statusDto.jobLatk = 0;
        statusDto.jobCatk = 0;
        statusDto.jobDex = 0;
        statusDto.jobAgi = 0;
        statusDto.jobLuk = 0;
        statusDto.jobLdef = 0;
        statusDto.jobCdef = 0;

        move = 4;

        job = new Job(jobName, jobLevel, skills, classChangeDestination, weaponTypes, statusDto, growthRateDto, move);
        jobDatabase.jobList.Add(job);


        jobName = JobName.ひまわり妖精;
        jobLevel = JobLevel.ADEPT;

        skills = new List<Skill>() { };
        classChangeDestination = null;
        weaponTypes = new List<WeaponType>() { WeaponType.STRIKE, WeaponType.SHOT, WeaponType.LASER };

        statusDto = new JobStatusDto();
        statusDto.jobHp = 0;
        statusDto.jobLatk = 0;
        statusDto.jobCatk = 0;
        statusDto.jobDex = 0;
        statusDto.jobAgi = 0;
        statusDto.jobLuk = 0;
        statusDto.jobLdef = 0;
        statusDto.jobCdef = 0;

        move = 4;

        job = new Job(jobName, jobLevel, skills, classChangeDestination, weaponTypes, statusDto, growthRateDto, move);
        jobDatabase.jobList.Add(job);

        jobName = JobName.ハイフェアリー;
        jobLevel = JobLevel.MASTER;

        skills = new List<Skill>() { };
        classChangeDestination = null;
        weaponTypes = new List<WeaponType>() { WeaponType.STRIKE, WeaponType.SHOT, WeaponType.LASER };

        statusDto = new JobStatusDto();
        statusDto.jobHp = 5;
        statusDto.jobLatk = 4;
        statusDto.jobCatk = 3;
        statusDto.jobDex = 2;
        statusDto.jobAgi = 1;
        statusDto.jobLuk = 1;
        statusDto.jobLdef = 3;
        statusDto.jobCdef = 1;

        move = 5;

        job = new Job(jobName, jobLevel, skills, classChangeDestination, weaponTypes, statusDto, growthRateDto, move);
        jobDatabase.jobList.Add(job);

        jobName = JobName.ホブゴブリン;
        jobLevel = JobLevel.ADEPT;

        skills = new List<Skill>() { };
        classChangeDestination = null;
        weaponTypes = new List<WeaponType>() { WeaponType.STRIKE };

        statusDto = new JobStatusDto();
        statusDto.jobHp = 0;
        statusDto.jobLatk = 0;
        statusDto.jobCatk = 0;
        statusDto.jobDex = 0;
        statusDto.jobAgi = 0;
        statusDto.jobLuk = 0;
        statusDto.jobLdef = 0;
        statusDto.jobCdef = 0;

        move = 5;

        job = new Job(jobName, jobLevel, skills, classChangeDestination, weaponTypes, statusDto, growthRateDto, move);
        jobDatabase.jobList.Add(job);


        jobName = JobName.吸血コウモリ;
        jobLevel = JobLevel.ADEPT;

        skills = new List<Skill>() { };
        classChangeDestination = null;
        weaponTypes = new List<WeaponType>() { WeaponType.STRIKE};

        statusDto = new JobStatusDto();
        statusDto.jobHp = 0;
        statusDto.jobLatk = 0;
        statusDto.jobCatk = 0;
        statusDto.jobDex = 0;
        statusDto.jobAgi = 0;
        statusDto.jobLuk = 0;
        statusDto.jobLdef = 0;
        statusDto.jobCdef = 0;

        move = 6;

        job = new Job(jobName, jobLevel, skills, classChangeDestination, weaponTypes, statusDto, growthRateDto, move);
        jobDatabase.jobList.Add(job);

        jobName = JobName.ツパイ;
        jobLevel = JobLevel.MASTER;

        skills = new List<Skill>() { };
        classChangeDestination = null;
        weaponTypes = new List<WeaponType>() { WeaponType.STRIKE};

        statusDto = new JobStatusDto();
        statusDto.jobHp = 2;
        statusDto.jobLatk = 2;
        statusDto.jobCatk = 2;
        statusDto.jobDex = 2;
        statusDto.jobAgi = 2;
        statusDto.jobLuk = 2;
        statusDto.jobLdef = 1;
        statusDto.jobCdef = 2;

        move = 7;

        job = new Job(jobName, jobLevel, skills, classChangeDestination, weaponTypes, statusDto, growthRateDto, move);
        jobDatabase.jobList.Add(job);



        jobName = JobName.幽霊;
        jobLevel = JobLevel.ADEPT;

        skills = new List<Skill>() { };
        classChangeDestination = null;
        weaponTypes = new List<WeaponType>() {WeaponType.SHOT, WeaponType.LASER };

        statusDto = new JobStatusDto();
        statusDto.jobHp = 0;
        statusDto.jobLatk = 0;
        statusDto.jobCatk = 0;
        statusDto.jobDex = 0;
        statusDto.jobAgi = 0;
        statusDto.jobLuk = 0;
        statusDto.jobLdef = 0;
        statusDto.jobCdef = 0;

        move = 5;

        job = new Job(jobName, jobLevel, skills, classChangeDestination, weaponTypes, statusDto, growthRateDto, move);
        jobDatabase.jobList.Add(job);

        jobName = JobName.怨霊;
        jobLevel = JobLevel.MASTER;

        skills = new List<Skill>() { };
        classChangeDestination = null;
        weaponTypes = new List<WeaponType>() {WeaponType.SHOT, WeaponType.LASER };

        statusDto = new JobStatusDto();
        statusDto.jobHp = 4;
        statusDto.jobLatk = 3;
        statusDto.jobCatk = 1;
        statusDto.jobDex = 2;
        statusDto.jobAgi = 2;
        statusDto.jobLuk = 1;
        statusDto.jobLdef = 3;
        statusDto.jobCdef = 1;

        move = 6;

        job = new Job(jobName, jobLevel, skills, classChangeDestination, weaponTypes, statusDto, growthRateDto, move);
        jobDatabase.jobList.Add(job);

        //ファイル書き出し Resources配下に作る
        AssetDatabase.CreateAsset(jobDatabase, "Assets/Resources/jobDatabase.asset");
    }
}
