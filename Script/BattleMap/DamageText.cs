using TMPro;
using UnityEngine;
using DG.Tweening;

//ダメージ表示テキスト
public class DamageText : MonoBehaviour
{

    //　消えるまでの時間
    [SerializeField]
    private float deleteTime = 1.5f;

    [SerializeField]
    private TextMeshPro damageText;

    private float elapsedTime = 0f;


    //自動で勝手に消える
    void Update()
    {
        //経過時間を取得する
        elapsedTime += Time.deltaTime;

        if(elapsedTime >= deleteTime)
        {
            //自分自身を消す
            Destroy(gameObject);
        }
    }

    //ダメージをセット
    public void Init(string damage)
    {
        damageText.text = string.Format("{0}", damage);

        //文字をジャンプさせながら1秒で透明にする
        Sequence sequence = DOTween.Sequence();
        sequence.Append(this.transform.DOJump(endValue: new Vector3(transform.position.x, transform.position.y + 1.7f, transform.position.z), jumpPower: 0.5f, numJumps: 1, duration: 0.8f))
        .Append(damageText.DOFade(endValue: 0f, duration: 0.3f));

        sequence.Play();
    }


}
