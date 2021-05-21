using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 210221 ���j�b�g�̃`���[�g���A���e�L�X�g��\������N���X
/// </summary>
public class UnitTutorialWindow : MonoBehaviour
{
    //���O
    [SerializeField] private Text name;

    //�푰
    [SerializeField] private Text race;

    //�e�L�X�g
    [SerializeField] private Text detailText;

    [SerializeField] private Image image;

    //HP
    [SerializeField] private Text hp;
    [SerializeField] private Slider hpGauge;

    //�������U��
    [SerializeField] private Text latk;
    [SerializeField] private Slider latkGauge;

    //�ߋ����U��
    [SerializeField] private Text catk;
    [SerializeField] private Slider catkGauge;

    //����
    [SerializeField] private Text agi;
    [SerializeField] private Slider agiGauge;

    //�Z
    [SerializeField] private Text dex;
    [SerializeField] private Slider dexGauge;

    //�K�^
    [SerializeField] private Text luk;
    [SerializeField] private Slider lukGauge;

    //�������h��
    [SerializeField] private Text ldef;
    [SerializeField] private Slider ldefGauge;

    //�ߋ����h��
    [SerializeField] private Text cdef;
    [SerializeField] private Slider cdefGauge;

    public void UpdateText(Unit unit)
    {
        this.name.text = unit.name;
        this.race.text = unit.race.GetStringValue();

        this.image.sprite = Resources.Load<Sprite>("Image/Charactors/" + unit.pathName + "/status");

        //�A�Z�b�g���琬�������擾
        GrowthDatabase growthDatabase = Resources.Load<GrowthDatabase>("growthDatabase");
        GrowthRate growthRate = growthDatabase.FindByName(unit.name);

        //HP
        hp.text = growthRate.hpRate.ToString();
        hpGauge.maxValue = StatusConst.GROWTH_MAX;
        hpGauge.value = growthRate.hpRate;

        //�������U��
        latk.text = growthRate.latkRate.ToString();
        latkGauge.maxValue = StatusConst.GROWTH_MAX;
        latkGauge.value = growthRate.latkRate;

        //�ߋ����U��
        catk.text = growthRate.catkRate.ToString();
        catkGauge.maxValue = StatusConst.GROWTH_MAX;
        catkGauge.value = growthRate.catkRate;

        //����
        agi.text = growthRate.agiRate.ToString();
        agiGauge.maxValue = StatusConst.GROWTH_MAX;
        agiGauge.value = growthRate.agiRate;

        //�Z
        dex.text = growthRate.dexRate.ToString();
        dexGauge.maxValue = StatusConst.GROWTH_MAX;
        dexGauge.value = growthRate.dexRate;

        //�K�^
        luk.text = growthRate.lukRate.ToString();
        lukGauge.maxValue = StatusConst.GROWTH_MAX;
        lukGauge.value = growthRate.lukRate;

        //�������h��
        ldef.text = growthRate.ldefRate.ToString();
        ldefGauge.maxValue = StatusConst.GROWTH_MAX;
        ldefGauge.value = growthRate.ldefRate;
        
        //�ߋ����h��
        cdef.text = growthRate.cdefRate.ToString();
        cdefGauge.maxValue = StatusConst.GROWTH_MAX;
        cdefGauge.value = growthRate.cdefRate;


        if (unit.name == "�얲")
        {
            this.detailText.text = "����_�Ђ̛ޏ�����B\n\n" +
                "2��ނ̕���Ɖ������ߋ����U���A���Ԃ̉񕜂��o���A\n" +
                "�����̏󋵂ŗL���ɐ키�����o���܂��B\n\n" +

                "�܂��A���������̏n���x���オ��₷���Ȃ�\n" +
                "�X�L���������Ă���̂ŁA\n" +
                "���Ղ��狭�͂ȕ��𑕔����鎖���o���܂��B\n\n" +

                "�����E�œG�̖h��X�L���𖳌�������X�L�����K�����܂��B\n\n" +

                "�㋉�E�ł͉������U�������^�ƁA�ߋ����U�������ӂňړ��͂�����\n" +
                "�o�����X�^��I�Ԏ����o���܂��B";
        }
        else if (unit.name == "������")
        {
            this.detailText.text = "�e���̓p���[�B\n\n" +

                "�������U���Ƒf�������オ��₷���A\n" +
                "�o���l�𑽂��l������X�L���Ő����������̂ŁA\n" +
                "�U���̗v�ƂȂ�܂��B\n" +
                "�܂��A�������ŕ󔠂��J���鎖���o���܂��B\n" +
                "�h��͍͂����Ȃ��̂ŁA�Ǘ������߂��Ȃ��悤�ɒ��ӂ��܂��傤�B\n\n" + 

                "�����E�ł͕���̎g�p�񐔏����}����X�L�����K�����܂��B\n\n" +

                "�㋉�E�ł͍U�������^�ƁA��𗦂������A�C�e���̎g�p���\n" +
                "�s�����o����h��^��I�Ԏ����o���܂��B";
        }
        else if (unit.name == "���[�~�A")
        {
            this.detailText.text = "�������A�ߋ����h��͋��ɍ����A�_���[�W���󂯂ɂ����ł��B\n" +
                "���ʁA�f�����͒Ⴂ�̂ŁA�U���͂̍����G����\n" +
                "�ǌ����󂯂Ȃ��悤�ɒ��ӂ��K�v�ł��B\n" +

                "�㋉�E�ɃN���X�`�F���W����ƁE�E�E";
        }
        else if (unit.name == "��d��")
        {
            this.detailText.text = "���Ԃ̉񕜂ƃX�e�[�^�X���グ�鎖���o���܂��B\n" +
                "�퓬�\�͂͒Ⴂ�̂ŁA�U�����󂯂Ȃ��悤�ɒ��ӂ��܂��傤�B\n\n" +

                "�����E�ɂȂ�ƁA�^�[���J�n����\n" +
                "���͂̒��Ԃ̗̑͂������񕜂��鎖���o���܂��B\n\n" +

                "�㋉�E�ɂȂ�ƒ��Ԃ��čs�������鎖���o����悤�ɂȂ�A\n" +
                "�X�ɒ��Ԃ̃X�e�[�^�X���グ�鎖���o����悤�ɂȂ�܂��B\n";

                
        }

        else if (unit.name == "�`���m")
        {
            this.detailText.text = "�퓬�\�͓͂ˏo�������������ł����A\n" +
                "�M�d�ȉ������Ƌߋ����̗�����\n" +
                "�U���o���镐����g�p���鎖���o���܂��B\n\n" +

                "�����E�ŗאڂ���G�̉�𗦂�������X�L�����K�����܂��B\n\n" +

                "�㋉�E�ł͒��Ԃ̃X�e�[�^�X���グ��X�L�����K������E���A\n" +
                "�G���ړ��o���Ȃ�����X�L�����K������E��I�Ԏ����o����̂ŁA\n" +
                "���Ɏg�����Ő�ǂ�L���ɐi�߂鎖���o���܂��B";
        }

        else if (unit.name == "��")
        {
            this.detailText.text = "���Ղ���������Đ��\�������ł����A���x���������ׁA\n" +
                "���΂����킹��Ƒ��̃L�������������܂���B\n" +
                "���Ղ͐퓬�����߂��Ȃ��悤�ɒ��ӂ��K�v�ł��B\n\n" +

                "�������ŕ󔠂��J���鎖���o����̂ŕ󔠂̉���W��A\n" +
                "������O���ĕǂƂ��Ċ��􂳂���̂������߂ł��B\n\n" +

                "�f���������ɍ����ł����A��ނ��ړI�ňٕς̉����ɂ�\n" +
                "�֗^����C�������ׁA�U���͂͒Ⴂ�ł��B\n\n" +

                "�����������L�����ł����A�������͍����̂ŁA\n" +
                "�Ō�܂Ŋ��􂷂鎖���o���܂��B\n" +

                "�㋉�E�ł͍U�������^�ƁA��𗦂̍���\n" +
                "�h��^��I�Ԏ����o���܂��B";
        }
        
        else if (unit.name == "����")
        {
            this.detailText.text = "�ߋ����U���Ɩh��ɓ����������\�ŁA\n" +
                "HP�������ǂƂ��Ċ��􂷂鎖���o���܂��B\n\n" +

                "�ߋ����U���̎�i�������Ȃ��G�ɑ΂��Ă�\n" +
                "����I�ɍ����_���[�W��^���鎖���o���܂��B\n\n" +

                "���ʁA�������U���ւ̖h��͂͒Ⴍ�A\n" +
                "�^���Ⴂ�וK�E���󂯂₷���̂ŁA\n" +
                "�ߐM���Ă���Ƒz���ȏ�Ƀ_���[�W���󂯂鎖���L��܂��B\n\n" +

                "�����E���璇�Ԃ̗̑͂��񕜂����鎖���o���܂��B\n\n" +

                "�㋉�E�ł͍����K�E�������E�ƂƁA\n" +
                "���Ԃ̖h��͂��グ�鎖���o����E�Ƃ�I�Ԏ����o���܂��B\n";
        }
        else if (unit.name == "������")
        {
            this.detailText.text = "���Ԃ̉񕜂ƃX�e�[�^�X���グ�鎖���o���܂��B\n" +
                "�U���͂����������̐��\�ŁA�G�Ƀ_���[�W��^���₷���ł��B\n" +
                "�܂��A���Ԃ��񕜂���Ǝ�����HP���񕜂��鎖���o���܂��B\n\n" +

                "�^���Ⴍ�A�h��͂������Ȃ��ׁA\n" +
                "�U�����󂯂Ȃ��悤�ɒ��ӂ��܂��傤�B\n\n" +

                "�����E�ɂȂ�ƁA���͂̒��Ԃ̍U���͂��グ�鎖���o���܂��B\n" +

                "�㋉�E�ɂȂ�ƒ��Ԃ��čs�������鎖���o����悤�ɂȂ�A\n" +
                "�X�ɒ��Ԃ̃X�e�[�^�X���グ�鎖���o����悤�ɂȂ�܂��B";
        }
        else if (unit.name == "���")
        {
            this.detailText.text = "�U���̖������������A�˒������̒��������\n" +
                "�G��_�����鎖���o���܂��B\n\n" +

                "�^�����ɒႭ�K�E���󂯂₷���̂ŁA\n" +
                "�U�����󂯂Ȃ��悤�ɒ��ӂ��K�v�ł��B\n\n" +

                "�㋉�E�ł͖�Œ��Ԃ̗̑͂��񕜂����鎖���o����o�����X�^�ƁA\n" +
                "�U�������^�̌X�����傫���قȂ�2��ނ̐E�Ƃ�I�Ԏ����o���܂��B\n\n" +

                "��œo�ꂳ�������ǁA�^�p�ɓ�L���ĉ����B";
        }
        else if (unit.name == "�p�`�����[")
        {
            this.detailText.text = "�������U���h��́A�Z�����ɍ����A\n" +
                "�ő�8�}�X��̓G�ɉ������U���o���镐����g�p���鎖���o����ׁA\n" +
                "�������U���ł͈��|�I�Ȑ��\�������Ă��܂��B\n\n" +

                "���ʁAHP�A�ߋ����h��͔͂��ɒႭ�A�f��������߂Ȃ̂ŁA\n" +
                "�ߋ����U�����󂯂�ƈꌂ�œ|����Ă��܂������L��܂��B\n\n" +

                "��p����͏C���o���܂������ɏC������z�ȈׁA\n" +
                "�������Ƃ������Ɏg�p���܂��傤�B\n\n" +

                "�����E�ł͂��U���͂��オ��A\n" +
                "�㋉�E�ł͍U�狤�ɋ��͂ȃX�L�����K�����܂��B\n\n" +

                "�����o���镄�̎�ނ������A�V���b�g�A���[�U�[�ɉ�����\n" +
                "�㋉�E�ł͕����A�����̂ǂ��炩�𑕔����鎖���o���܂��B\n";

        }
        else if (unit.name == "���")
        {
            this.detailText.text = "�Z�A�����A�K�^���オ��₷���A\n" +
                "�܂��A���ߗ����ɑ΂��čU���o�����p����������Ă���ׁA\n" +
                "���̖������􂪏o���܂��B\n\n" +

                "�o�����X�^�̐���������L�����̂��񑩂�\n" +
                "�^�������Ɗ�p�n�R�ɂȂ鎖���L��܂����A\n" +
                "���̏ꍇ�����Ԃ̃T�|�[�g�ƌ��J���ɂ��󔠂̉����\n" +
                "���Ԃ���̊��􂪏o����A�܂��Ƀp�[�t�F�N�g���C�h�B\n\n" +

                "�����E�ł͍s�������ɑҋ@�����\n" +
                "��𗦂��オ��X�L�����K�����܂��B\n\n" +

                "�㋉�E�ł͍U�������^�̐E�ƂƁA\n" +
                "���Ԃ̃T�|�[�g���o����E�Ƃ�I�Ԏ����o���܂��B\n";

        }

        else if (unit.name == "���~���A")
        {
            this.detailText.text = "���Ղ���������Đ��\�������ł����A���x���������ׁA\n" +
                "���~���A�΂����킹��Ƒ��̃L�������������܂���B\n" +
                "���Ղ͐퓬�����߂��Ȃ��悤�ɒ��ӂ��K�v�ł��B\n\n" +

                "��p����̃O���O�j���͉��ߗ��p�Ŕ��ɍ����\�ł����A\n" +
                "�C������z�ȈׁA���p����ƍg���ق̍������������Ă��܂��܂��B\n\n" +

                "�^���𑀂���x�̔\�ׁ͂̈A���ɍK�^�̐������������A\n" +
                "�S�̓I�ȃX�e�[�^�X�̐������������ׁA\n" +
                "�I�Ղ����􂷂鎖���o���܂��B\n\n" +

                "�㋉�E�ł͐퓬��ɓG�̔\�͂�������X�L�����K������E�ƂƁA\n" +
                "�G����󂯂�_���[�W���m���Ŕ���������X�L�����K������\n" +
                "�E�Ƃ�I�Ԏ����o���܂��B\n";

        }
        else if (unit.name == "�t�����h�[��")
        {
            this.detailText.text = "���ɍU�����̃X�e�[�^�X�ɃN���e�B�J������������p����A\n" +
                "�K�E�����グ���p�X�L���������Ă���A\n" +
                "�U������������Α����̓G���ꌂ�œ|�������o���܂��B\n\n" +

                "���ʁA�Z���Ⴂ�׍U���̖��������肵�Ȃ��̂ŁA\n" +
                "�^�p�ɉ^�̗v�f�������ł��B\n\n" +
                "�܂��h��͂͒Ⴂ�̂ŁA�V���[�Y�P���\n" +
                "�u����������v�ɒ��ӂ���K�v���L��܂��B\n\n" +

                "�Q�[���I�Ղɏ㋉�E�ŉ�������ׁA\n" +
                "�N���X�`�F���W�\�ȐE�Ƃ͗L��܂���B";

        }

    }
}
