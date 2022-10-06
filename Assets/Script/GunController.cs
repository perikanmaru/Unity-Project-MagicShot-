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

    //残弾
    public int shotCount = 30;
    [SerializeField]
    GameObject bulletPrefab;
    public float shotSpeed;


    bool fireTimerIsActive = false;
    RaycastHit hit;
    WaitForSeconds fireIntervalWait;

    void Start()
    {
        fireIntervalWait = new WaitForSeconds(fireInterval);  // WaitForSecondsをキャッシュしておく（高速化）
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

    // 弾の発射処理
    void Fire()
    {
        if (fireTimerIsActive)
        {
            return;
        }

        GameObject bullet = (GameObject)Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.parent.eulerAngles.x, transform.parent.eulerAngles.y, 0));
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.AddForce(transform.forward * shotSpeed);

        //射撃されてから3秒後に銃弾のオブジェクトを破壊する.

        Destroy(bullet, 3.0f);
 
        if (Physics.Raycast(bulletSpawn.position, bulletSpawn.forward, out hit, maxRange, hitLayers, QueryTriggerInteraction.Ignore))
        {
            BulletHit();
        }

        StartCoroutine(nameof(FireTimer));
    }

    // 弾がヒットしたときの処理
    void BulletHit()
    {
        // テスト用
        Debug.Log("弾が「" + hit.collider.gameObject.name + "」にヒットしました。");

    }

    // 弾を発射する間隔を制御するタイマー
    IEnumerator FireTimer()
    {
        fireTimerIsActive = true;

        yield return fireIntervalWait;

        fireTimerIsActive = false;
    }

}