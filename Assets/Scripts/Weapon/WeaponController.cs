using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour {

    #region 单例
    private static WeaponController instance;

    public static WeaponController Instance
    {
        get
        {
            if (instance == null)
                return GameObject.FindGameObjectWithTag("WeaponChange").GetComponent<WeaponController>();
            return instance;
        }
    }
    #endregion

    public Text bulletCost;
    public Transform bulletHolder;
    public Transform webHolder;

    public GameObject[] Webs;
    public GameObject[] Guns;
    public GameObject[] Bullets;

    private GameObject[][] bulletsPool = new GameObject[40][];
    private GameObject[][] webPool = new GameObject[4][];

    [System.NonSerialized]
    public int[] bulletPower = { 5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 60, 70, 80, 90, 100, 110, 120, 130, 140, 150, 170, 
        190, 210, 230, 250, 270, 290, 310, 330, 350, 400, 450, 500, 550, 600, 650, 700, 750, 800, 850 };
    private int gunIndex = 0;
    private int bulletIndex = 0;

    Transform firePosition;

    void Start()
    {
        InitializeWeapon();
        ChangeFirePosition();
    }

    void Update()
    {
        if (SettingUIController.Instance.IsGameOn)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                ChangeWeaponByBulletIndex(ref bulletIndex, true);
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                ChangeWeaponByBulletIndex(ref bulletIndex, false);
            }
            if (Input.GetMouseButtonDown(0) && UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() == false)
            {
                Fire();
            }
        }
    }

    public void PointDownLeftButton()
    {
        AudioContoller.Instance.PlayOtherAudio(AudioContoller.Instance.changeWeaponAudio);
        ChangeWeaponByGunIndex(ref gunIndex, true);
    }

    public void PointDownRightButton()
    {
        AudioContoller.Instance.PlayOtherAudio(AudioContoller.Instance.changeWeaponAudio);
        ChangeWeaponByGunIndex(ref gunIndex, false);
    }

    private void ChangeWeaponByBulletIndex(ref int BulletIndex, bool IsLeft)
    {
        if(IsLeft)
        {
            if(BulletIndex > 0)
            {
                BulletIndex--;
                bulletCost.text = "$" + bulletPower[BulletIndex].ToString();
                if(BulletIndex % 10 == 9)
                {
                    Guns[BulletIndex / 10 + 1].SetActive(false);
                    Guns[BulletIndex / 10].SetActive(true);
                    ChangeFirePosition();
                }
            }
        }
        else
        {
            if(BulletIndex < 39)
            {
                BulletIndex++;
                bulletCost.text = "$" + bulletPower[BulletIndex].ToString();
                if(BulletIndex % 10 == 0)
                {
                    Guns[BulletIndex / 10 - 1].SetActive(false);
                    Guns[BulletIndex / 10].SetActive(true);
                    ChangeFirePosition();
                }
            }
        }
    }

    private void ChangeWeaponByGunIndex(ref int GunIndex, bool IsLeft)
    {
        if (IsLeft)
        {
            if (GunIndex > 0)
            {
                Guns[GunIndex].SetActive(false);
                GunIndex--;
                Guns[GunIndex].SetActive(true);
                bulletIndex = GunIndex * 10;
                bulletCost.text = "$" + bulletPower[bulletIndex].ToString();
                ChangeFirePosition();
            }
        }
        else
        {
            if (GunIndex < 3)
            {
                Guns[GunIndex].SetActive(false);
                GunIndex++;
                Guns[GunIndex].SetActive(true);
                bulletIndex = GunIndex * 10;
                bulletCost.text = "$" + bulletPower[bulletIndex].ToString();
                ChangeFirePosition();
            }
        }
    }

    private void InitializeWeapon()
    {
        //初始化枪
        foreach (GameObject item in Guns)
        {
            item.SetActive(false);
        }
        //初始化子弹
        int BulletIndex = 0;
        foreach (GameObject item in Bullets)
        {
            bulletsPool[BulletIndex] = new GameObject[20];
            for (int i = 0; i <= 19; i++)
            {
                GameObject obj = Instantiate(item);
                obj.transform.SetParent(bulletHolder, false);
                bulletsPool[BulletIndex][i] = obj;
                obj.SetActive(false);
            }
            BulletIndex++;
        }
        //初始化渔网
        int webIndex = 0;
        foreach (GameObject item in Webs)
        {
            webPool[webIndex] = new GameObject[50];
            for (int i = 0; i < 50; i++)
            {
                GameObject obj = Instantiate(Webs[webIndex]);
                webPool[webIndex][i] = obj;
                obj.transform.SetParent(webHolder,false);
                obj.SetActive(false);
            }
            webIndex++;
        }
        Guns[gunIndex].SetActive(true);
        bulletCost.text = "$" + bulletPower[bulletIndex].ToString();
    }

    void ChangeFirePosition()
    {
        firePosition = Guns[gunIndex].transform.GetChild(Guns[gunIndex].transform.childCount - 1);
    }

    public void Fire()
    {
        AudioContoller.Instance.PlayOtherAudio(AudioContoller.Instance.bulletAudio);
        foreach (GameObject item in bulletsPool[bulletIndex])
        {
            if (item.activeSelf == false)
            {
                if (UIController.Instance.DecreaseMoney(bulletPower[bulletIndex]))
                {
                    item.SetActive(true);
                    item.transform.position = firePosition.position;
                    item.transform.rotation = firePosition.rotation;
                    item.GetComponent<BulletMove>().bulletIndex = this.bulletIndex;
                }
                break;
            }
        }
    }

    public void ProduceWeb( int BulletIndex, Transform WebTransform, int Damage)
    {
        foreach (GameObject item in webPool[BulletIndex / 10])
        {
            if(item.activeSelf == false)
            {
                item.SetActive(true);
                item.transform.position = WebTransform.position;
                item.GetComponent<Web>().damage = Damage;
                break;
            }
        }
    }
}
