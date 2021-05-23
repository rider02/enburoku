using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TalkManager : MonoBehaviour
{
    [SerializeField]
    GameObject talkListWindow;

    StatusManager statusManager;

    //支援会話パートで読み込むシーン名 支援会話レベルボタンクリック時にセット
    public static string sceneName;

    //会話の一覧 増えたらこれに追加していくこと
    List<string> talkList;

    //支援レベルが最大でBのリスト
    List<string> maxLevelBList;

    public void init(StatusManager statusManager)
    {
        this.statusManager = statusManager;

        //支援会話のパターン初期化
        initTalkList();

    }

    //支援会話のパターンを初期化
    private void initTalkList()
    {
        //支援会話の組み合わせ一覧を作って重複を省く
        talkList = new List<string> { 
            "reimu_marisa", "reimu_aya", "reimu_udon",//霊夢
            "marisa_cirno", "marisa_udon",//魔理沙
            "rumia_dai",//大妖精
            "rumia_cirno","dai_cirno", "cirno_aya",//チルノ
            "aya_udon",//文
        };

        //支援レベルが最大でBの人達
        maxLevelBList = new List<string>{
            "aya_udon"
        };

    }

    //支援会話ウィンドウを初期化 数が少ないのでScriptable Objectにせんでいいと思う
    public void initTalkListWindow(string unitName)
    {
        string buttonUnitName;

        //霊夢の時、霊夢と支援会話が有るキャラ一覧を表示する
        if ("霊夢" == unitName)
        {
            buttonUnitName = "reimu";
            //魔理沙
            addFriendPanel(buttonUnitName, "魔理沙", "marisa");

            //文
            addFriendPanel(buttonUnitName, "文", "aya");

            //うどん
            addFriendPanel(buttonUnitName, "鈴仙", "udon");

        }

        if("魔理沙" == unitName)
        {
            buttonUnitName = "marisa";
            //霊夢
            addFriendPanel(buttonUnitName, "霊夢", "reimu");

            //チルノ
            addFriendPanel(buttonUnitName, "チルノ", "cirno");

            //うどんげ氏
            addFriendPanel(buttonUnitName, "鈴仙", "udon");
        }
        if("ルーミア" == unitName)
        {
            buttonUnitName = "rumia";

            //大妖精
            addFriendPanel(buttonUnitName, "大妖精", "dai");
            //ツィルノ
            addFriendPanel(buttonUnitName, "チルノ", "cirno");

        }
        if("大妖精" == unitName)
        {
            buttonUnitName = "dai";

            //ルーミア
            addFriendPanel(buttonUnitName, "ルーミア", "rumia");

            //ツィルノ
            addFriendPanel(buttonUnitName, "チルノ", "cirno");
        }
        if ("チルノ" == unitName)
        {
            buttonUnitName = "cirno";

            //大妖精
            addFriendPanel(buttonUnitName, "大妖精", "dai");

            //魔理沙
            addFriendPanel(buttonUnitName, "魔理沙", "marisa");

            //文
            addFriendPanel(buttonUnitName, "文", "aya");

            //ルーミア
            addFriendPanel(buttonUnitName, "ルーミア", "rumia");

        }
        if ("文" == unitName)
        {
            buttonUnitName = "aya";

            //霊夢
            addFriendPanel(buttonUnitName, "霊夢", "reimu");

            //ツィルノ
            addFriendPanel(buttonUnitName, "チルノ", "cirno");

            //うどんげ氏
            addFriendPanel(buttonUnitName, "鈴仙", "udon");

        }
        if("鈴仙" == unitName)
        {
            buttonUnitName = "udon";
            //霊夢
            addFriendPanel(buttonUnitName, "霊夢", "reimu");

            //魔理沙
            addFriendPanel(buttonUnitName, "魔理沙", "marisa");

            //うどんげ氏
            addFriendPanel(buttonUnitName, "文", "aya");
        }


    }

    //支援会話パネルを作ってくれる
    private Transform addFriendPanel(string unitName, string friendDispName, string friendName)
    {
        var friendPanel = (Instantiate(Resources.Load("Prefabs/FriendPanel")) as GameObject).transform;
        //friendPanelに支援会話が有るキャラの名前を表示
        friendPanel.GetComponent<FriendPanel>().UpdateText(friendDispName);

        friendPanel.name = friendName + "Panel";

        //支援リストウィンドウ配下に表示
        friendPanel.transform.SetParent(talkListWindow.transform);

        addFriendLevelButton(friendPanel, unitName, friendName);

        return friendPanel;

    }

    //支援会話ボタンを作ってくれる
    private void addFriendLevelButton(Transform panel, string unitName, string friendName)
    {
        //TODO ここでキャラによってC～Bまでしか無かったりするようにする
        List<string> talkLevelList;

        if(isMaxLevelB(unitName,friendName))
        {
            talkLevelList = new List<string>() { "C", "B" };
        }
        else
        {
            talkLevelList = new List<string>() { "C", "B", "A" };
        }
        

        foreach(string talkLevel in talkLevelList) { 

            //支援会話レベルボタンを作る
            var friendLevelButton = (Instantiate(Resources.Load("Prefabs/FriendLevelButton")) as GameObject).transform;

            //ボタンの名前変更、テキスト変更
            friendLevelButton.name = unitName + "_" + friendName + "_" + talkLevel;
            friendLevelButton.GetComponent<FriendLevelButton>().UpdateText(talkLevel);
            friendLevelButton.GetComponent<FriendLevelButton>().Init(this);
            panel.GetComponent<FriendPanel>().AddFriendLevelList(friendLevelButton);
        }
    }

    //addPanelメソッドで、名前を引数にしたら簡単にパネルを追加出来るってことにする


    //支援会話ウィンドウを表示するメソッドを作る
    public void OpenTalkListWindow()
    {
        //支援会話ウィンドウを表示する
        talkListWindow.SetActive(true);
    }

    //支援レベルボタンが押された時、読み込むテキスト名をセットする
    public void setFriendTalk(string buttonName)
    {
        //霊夢×魔理沙と魔理沙×霊夢の結果を同じにする
        string selectedSceneName = correctFriendTalk(buttonName);

        sceneName = selectedSceneName;
        Debug.Log("sceneName : " + sceneName);
    }

    //支援会話シーンへ遷移 FrindLevelButtonから直接呼んでも良かったが、面倒だった
    public void ChangeSceneToTalk()
    {
        statusManager.ChangeScene("Talk");
    }

    //支援レベルが最大でBかどうかを返してくれる
    private bool isMaxLevelB(string unitName, string friendName)
    {
        foreach (string talk in maxLevelBList)
        {
            //霊夢×魔理沙、魔理沙×霊夢を同一とみなす
            if (talk == unitName + "_" + friendName || talk == friendName + "_" + unitName)
            {
                //これは支援レベルB
                return true;
            }
        }
        //ここまで来たら支援C
        return false;
    }

    //霊夢×魔理沙と魔理沙×霊夢の結果を同じにする
    private string correctFriendTalk(string buttonName)
    {
        //'_'で区切る
        string[] splitString = buttonName.Split('_');

        //サイズ数が3でなければ異常なのでデフォルトの会話となる
        if(splitString.Length != 3)
        {
            return "reimu_marisa_C";
        }
        string coupleName = splitString[0] + "_" + splitString[1];
        string friendLevel = splitString[2];

        foreach(string talk in talkList)
        {
            if(coupleName == talk)
            {
                return coupleName + "_" + friendLevel;
            }
        }

        //こっちに来たら魔理沙×霊夢のように逆のパターン
        //marisa_reimuをreimu_marisaにする
        string reverseCoupleName = splitString[1] + "_" + splitString[0];
        foreach (string talk in talkList)
        {
            if (reverseCoupleName == talk)
            {
                return reverseCoupleName + "_" + friendLevel;
            }
        }

        //ここまで来たらボタンの設定間違いなのでデフォルトの会話
        return "reimu_marisa_C";
    }

}
