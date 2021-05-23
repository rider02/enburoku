using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// 職業ScriptableObject精製用クラス
/// </summary>
public class JobDatabase : ScriptableObject
{

    //ListステータスのList
    public List<Job> jobList = new List<Job>();

    /// <summary>
    /// ユニットの名前からユニットを返却する
    /// </summary>
    /// <param name="unitName"></param>
    /// <returns></returns>
    public Job FindByJob(JobName jobName)
    {
        //名前の一致したユニットを返す 無ければnull
        return jobList.FirstOrDefault(job => job.jobName == jobName);

    }

}
