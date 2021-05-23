using System.Collections.Generic;

/// <summary>
/// 各ジョブのクラス
/// 転職先、成長率、スキル、ステータス補正値、装備可能武器を保持する
/// JobDatabaseでアセット化して使用する
/// </summary>
[System.Serializable]
public class Job
{
    //名前
    public JobName jobName;

    //職業のレベル
    public JobLevel jobLevel;

    //保持スキルリスト
    public List<Skill> skills;

    //クラスチェンジ先
    public List<JobName> classChangeDestination;

    //装備可能武器
    public List<WeaponType> weaponTypes;

    //移動力
    public int move;

    //職業のステータス補正
    public JobStatusDto statusDto;

    //職業の成長率補正
    public GrowthRateDto growthRateDto;

    //コンストラクタ
    public Job(JobName jobname, JobLevel jobLevel, List<Skill> skills , List<JobName> classChangeDestination,
        List<WeaponType> weaponTypes, JobStatusDto statusDto, GrowthRateDto growthRateDto , int move)
    {
        this.jobName = jobname;

        this.jobLevel = jobLevel;

        this.skills = skills;

        this.classChangeDestination = classChangeDestination;

        this.weaponTypes = weaponTypes;

        //ステータス
        this.statusDto = statusDto;

        //成長率補正
        this.growthRateDto = growthRateDto;

        this.move = move;
    }
    

}
