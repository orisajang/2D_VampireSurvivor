using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

[RequireComponent(typeof(ScanTarget))]
public class Player : Unit
{
    //플레이어의 무가가 있는대로 무기 종류에 따라 공격을 한다.
    //아래는 처음부터 SO사용했을때 코드. 안써서 주석(추후 삭제예정)
    //[SerializeField] private List<WeaponSO> _weaponsData = new List<WeaponSO>();

    //CSV파일로 무기 정보를 받아온다
    WeaponCSVLoader loader = new WeaponCSVLoader();
    List<WeaponCSVData> _weaponCSVDataList = new List<WeaponCSVData>();

    //private List<Weapon> _weaponHaveList = new List<Weapon>();
    private Dictionary<eWeaponType, Weapon> _weaponHaveDic = new Dictionary<eWeaponType, Weapon>();

    public WeaponModel weaponModel { get; private set; } =  new WeaponModel();

    //무기 공격 시간체크용 코루틴
    Coroutine _weaponTimeCoroutine;
    WaitForSeconds _delay = new WaitForSeconds(1);

    //범위내의 가장 가까운적 탐색용 클래스
    ScanTarget _scanTarget;

    //상태패턴을 위한 플레이어 상태 인터페이스
    IPlayerState _playerState;

    //몬스터한테 데미지받았을떄 (삭제예정)
    public bool isTakeDamage = false;

    //플레이어 HP 
    [SerializeField] Image _hpBarImage;
    public float _CurrentHp { get; private set; }

    //피격 애니메이션 처리를 위해
    [SerializeField] Animator animator;

    private void Awake()
    {
        //csv
        _weaponCSVDataList = loader.LoadWeaponData("WeaponCSVData");


        _scanTarget = GetComponent<ScanTarget>();
        PlayerAttackWithWeapon();


        weaponModel.OnBulletLevelChanged += IncrementBulletWeaponLevel;
        weaponModel.OnRotateShiledLevelChanged += IncrementRotateShieldWeaponLevel;
    }
    public void SetData(List<List<string>> Data)
    {
        foreach (var item in Data)
        {
            WeaponCSVData weaponData = new WeaponCSVData();
            weaponData.weaponDamage = int.Parse(item[0]);
            weaponData.weaponSpeed = float.Parse(item[1]);
            weaponData.coolDown = int.Parse(item[2]);
            weaponData.weaponType = (eWeaponType)Enum.Parse(typeof(eWeaponType), item[3]);
            weaponData.prefabKey = item[4];
            _weaponCSVDataList.Add(weaponData);
        }
    }
    private void AssignPrefabs()
    {
        //PrefabManager prefabManager = FindObjectOfType<PrefabManager>();

        foreach (var w in _weaponCSVDataList)
        {
            w.weaponPrefab = PrefabManager.Instance.GetPrefab(w.prefabKey);
        }

        Debug.Log("Prefab 연결 완료");
    }

    public void IncrementBulletWeaponLevel(int plusLevel)
    {
        _weaponHaveDic[eWeaponType.ShootBullet].LevelUp(plusLevel);
    }
    public void IncrementRotateShieldWeaponLevel(int plusLevel)
    {
        _weaponHaveDic[eWeaponType.RotateShield].LevelUp(plusLevel);
    }



    private void Start()
    {
        _playerState = new PlayerIdleState(this);
        
    }
    private void OnEnable()
    {
        StartWeaponAttack();
        PlayerManager.Instance.SetPlayer(this);
    }
    private void OnDisable()
    {
        StopWeaponAttack();
    }

    private void Update()
    {
        _playerState?.Update();
    }
    public void SetState(IPlayerState state)
    {
        state?.Exit();
        _playerState = state;
        state.Enter();
    }

    //모든 플레이어 무기를 가지고 공격을 한다
    public void PlayerAttackWithWeapon()
    {
        /*
        foreach (var item in _weaponsData)
        {
            eWeaponType type = item.weaponType;
            Transform weaponSpawnPos = PlayerManager.Instance.playerWeaponSpawner;
            Weapon weapon = new Weapon(item.weaponPrefab,transform, type, item.weaponDamage, item.coolDown, item.weaponSpeed, weaponSpawnPos);
            AddWeapon(weapon);
        }
        */
        foreach(var item in _weaponCSVDataList)
        {
            eWeaponType type = item.weaponType;
            Transform weaponSpawnPos = PlayerManager.Instance.playerWeaponSpawner;
            Weapon weapon = new Weapon(item.weaponPrefab, transform, type, item.weaponDamage, item.coolDown, item.weaponSpeed, weaponSpawnPos);
            AddWeapon(weapon);
        }
    }

    private void AddWeapon(Weapon weapon)
    {
        //리스트에 추가
        eWeaponType weaponType = weapon.WeaponType;
        _weaponHaveDic[weaponType] = weapon;
    }
    private void StartWeaponAttack()
    {
        if(_weaponTimeCoroutine == null)
        {
            _weaponTimeCoroutine = StartCoroutine(AttackCoroutine());
        }
    }
    private void StopWeaponAttack()
    {
        if(_weaponTimeCoroutine != null)
        {
            StopCoroutine(_weaponTimeCoroutine);
        }
    }
    IEnumerator AttackCoroutine()
    {
        while(true)
        {
            foreach(var kvp in _weaponHaveDic)
            {
                if (kvp.Value == null) continue;
                kvp.Value.Tick(1, gameObject.transform, _scanTarget.NearestTarget);
            }
            yield return _delay;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            isTakeDamage = true;
            Monster mon = collision.gameObject.GetComponent<Monster>();

            //피격 애니메이션 재생
            animator.SetTrigger("hurt");

            _CurrentHp = BattleManager.Instance.CalculateDamage(_CurrentHp, mon.AttackValue, _defense);
            _hpBarImage.fillAmount = _CurrentHp / _hp;
        }
    }

    public void SetPlayerData(PlayerDataSO playerData)
    {
        _hp = playerData.hp;
        _defense = playerData.defense;

        _CurrentHp = _hp;
    }


    protected override void Attack()
    {
        throw new System.NotImplementedException();
    }

    protected override void TakeDamage()
    {
        throw new System.NotImplementedException();
    }
}
