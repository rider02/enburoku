using UnityEngine;

/// <summary>
/// 200719 クラスチェンジを行う
/// </summary>
public class ClassChangeManager : MonoBehaviour
{
    private GameObject classChangeWindow;

    StatusManager statusManager;

    private JobDatabase jobDatabase;

    private UnitController unitController;

    //初期化
    public void Init(StatusManager statusManager , JobDatabase jobDatabase, UnitController unitController)
    {

        this.statusManager = statusManager;
        this.jobDatabase = jobDatabase;
        this.unitController = unitController;
    }

    /// <summary>
    /// クラスチェンジ先のボタン一覧を作成する
    /// </summary>
    /// <param name="unit"></param>
    public void CreateClassChangeButton(Unit unit , ClassChangeDetailWindow classChangeDetailWindow)
    {

        //職業がマスターレベルなら転職先は存在しない
        if (unit.job.jobLevel == JobLevel.MASTER)
        {

            //Resources配下からボタンをロード
            var classChangeButton = (Instantiate(Resources.Load("Prefabs/ClassChangeButton")) as GameObject).transform;

            //転職先が存在しない場合のボタンを作成
            classChangeButton.GetComponent<ClassChangeButton>().DisableInit();
            classChangeButton.name = classChangeButton.name.Replace("(Clone)", "");

            //classChangeWindowオブジェクトをを探して取得
            GameObject canvas = GameObject.Find("Canvas");
            classChangeWindow = canvas.transform.Find("ClassChangeDestinationWindow").gameObject;

            //classChangeWindowオブジェクト配下にprefab作成
            classChangeButton.transform.SetParent(classChangeWindow.transform);
        }

        else
        {
            //転職先が存在する場合は転職先の数だけボタン表示
            foreach (var jobname in unit.job.classChangeDestination)
            {

                //201724 jobNameからjobを取得してボタンに持たせておく
                Job job = jobDatabase.FindByJob(jobname);

                //Resources配下からボタンをロード
                var classChangeButton = (Instantiate(Resources.Load("Prefabs/ClassChangeButton")) as GameObject).transform;
                classChangeButton.GetComponent<ClassChangeButton>().Init(job, unit, this , statusManager, classChangeDetailWindow);
                classChangeButton.name = classChangeButton.name.Replace("(Clone)", "");

                //レベルによるボタン無効化処理
                if(job.jobLevel == JobLevel.ADEPT)
                {

                    //中級職ならLv10でないとボタン無効化
                    if(unit.lv < 10)
                    {
                        classChangeButton.GetComponent<ClassChangeButton>().setDisable("中級職に転職するにはLv10が必要です。");
                    }
                }
                else if (job.jobLevel == JobLevel.MASTER)
                {

                    //上級職ならLv20でないとボタン無効化
                    if (unit.lv <　20)
                    {
                        classChangeButton.GetComponent<ClassChangeButton>().setDisable("上級職に転職するにはLv20が必要です。");
                    }
                }

                //classChangeWindowオブジェクトをを探して取得
                GameObject canvas = GameObject.Find("Canvas");
                classChangeWindow = canvas.transform.Find("ClassChangeDestinationWindow").gameObject;

                //classChangeWindowオブジェクト配下にprefab作成
                classChangeButton.transform.SetParent(classChangeWindow.transform);
            }
        }
    }

    /// <summary>
    /// クラスチェンジ実行
    /// </summary>
    /// <param name="unitName"></param>
    /// <param name="job"></param>
    public void ClassChange(string unitName, Job job)
    {

        //名前からユニットを取得
        Unit unit = unitController.findByName(unitName);

        if(unitName == null)
        {
            return;
        }

        //クラスチェンジ実行
        unit.job = job;

        //ユニットリスト更新
        unitController.UpdateUnit(unit);

        //キャンセルボタン押下と同じ事をしてウィンドウを閉じる
        //200724 ちゃんとした画面遷移を今後作りたい
        statusManager.CancelButton();

    }

    /// <summary>
    /// クラスチェンジ先のボタン一覧を削除する
    /// </summary>
    public void DeleteClassChangeButton()
    {
        GameObject[] g = GameObject.FindGameObjectsWithTag("ClassChangeDestinationButton");
        foreach (var obj in g)
        {
            Destroy(obj);
        }
    }
}
