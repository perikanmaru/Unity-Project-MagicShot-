using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�A�j���[�V��������Ăяo���@Die�A�j���[�V�����Đ���w��b����ɃI�u�W�F�N�g���폜
public class DestroyObject : MonoBehaviour
{
    [Tooltip(" �I�u�W�F�N�g�����񂾂��ƃV�[�����ɃI�u�W�F�N�g�����܂鎞�Ԃł��B")]
    public float ObjectHoldingTime;

    //���ׂẴI�u�W�F�N�g��j�󂷂�Ƃ��p�ϐ� ����ŃV�[����ɑ��݂���G���Ǘ�����
    private GameObject[] targets;
    private bool AllDestroyActive = false;

    //�v���C���[���G��|�����Ƃ��p�̔j�󃁃\�b�h
    private void ObjectHoldingTimeAtDestroy()
    {

        Destroy(gameObject, ObjectHoldingTime);
    }

    //�V�[����̂��ׂĂ̓G�j��p���\�b�h
    public void AllDestroy()
    {

        if (AllDestroyActive == false)
        {
            targets = GameObject.FindGameObjectsWithTag("Enemy");

            //���ׂẴI�u�W�F�N�g��j���true�ɂ���
            AllDestroyActive = true;

            foreach (GameObject i in targets)
            {
                Destroy(gameObject, ObjectHoldingTime);
            }

        }
    }
}
