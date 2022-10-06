using System.Collections;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField]
    Transform bulletSpawn = null;
    [SerializeField, Min(1)]
    int damage = 1;
    [SerializeField, Min(1)]
    int maxAmmo = 30;
    [SerializeField, Min(1)]
    float maxRange = 30;
    [SerializeField]
    LayerMask hitLayers = 0;
    [SerializeField, Min(0.01f)]
    float fireInterval = 0.1f;

    //�c�e
    public int shotCount = 30;
    [SerializeField]
    GameObject bulletPrefab;
    public float shotSpeed;


    bool fireTimerIsActive = false;
    RaycastHit hit;
    WaitForSeconds fireIntervalWait;

    void Start()
    {
        fireIntervalWait = new WaitForSeconds(fireInterval);  // WaitForSeconds���L���b�V�����Ă����i�������j
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && (shotCount >= 0))
        {
            Fire();
            shotCount -= 1;
        }else if (Input.GetKeyDown(KeyCode.R))
        {
            shotCount = 30;
        }

    }

    // �e�̔��ˏ���
    void Fire()
    {
        if (fireTimerIsActive)
        {
            return;
        }

        GameObject bullet = (GameObject)Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.parent.eulerAngles.x, transform.parent.eulerAngles.y, 0));
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.AddForce(transform.forward * shotSpeed);

        //�ˌ�����Ă���3�b��ɏe�e�̃I�u�W�F�N�g��j�󂷂�.

        Destroy(bullet, 3.0f);
 
        if (Physics.Raycast(bulletSpawn.position, bulletSpawn.forward, out hit, maxRange, hitLayers, QueryTriggerInteraction.Ignore))
        {
            BulletHit();
        }

        StartCoroutine(nameof(FireTimer));
    }

    // �e���q�b�g�����Ƃ��̏���
    void BulletHit()
    {
        // �e�X�g�p
        Debug.Log("�e���u" + hit.collider.gameObject.name + "�v�Ƀq�b�g���܂����B");

    }

    // �e�𔭎˂���Ԋu�𐧌䂷��^�C�}�[
    IEnumerator FireTimer()
    {
        fireTimerIsActive = true;

        yield return fireIntervalWait;

        fireTimerIsActive = false;
    }

}