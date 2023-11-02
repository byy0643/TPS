using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;


public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private float normalSensitivity = 1f;
    [SerializeField] private float aimSensitivity = 0.3f;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private Transform debugTransform;
    [SerializeField] private Transform pfBulletProjectile;
    [SerializeField] private Transform spawnBulletPosition;
    [SerializeField] private GameObject playerGun;
    [SerializeField] private GameObject enemy;

    private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs starterAssetsInputs;
    private EnemyController enemyController;
    private Animator animator;
    private bool isNear = false;
    private float range;


    private void Awake()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        enemyController = enemy.GetComponent<EnemyController>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        playerGun.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseWorldPosition = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        

        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            debugTransform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
        }

        if (starterAssetsInputs.aim)//if press right click
        {
            aimVirtualCamera.gameObject.SetActive(true);//active aim camera
            thirdPersonController.SetSensitivity(aimSensitivity);//difference sensitivity with main camera
            thirdPersonController.SetRotateOnMove(false);

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        }
        else//if release right click
        {
            aimVirtualCamera.gameObject.SetActive(false);//inactive aim camera
            thirdPersonController.SetSensitivity(normalSensitivity);//change back to original sensitivity
            thirdPersonController.SetRotateOnMove(true);
        }

        int enemyLayerMask = 1 <<
        LayerMask.NameToLayer("Enemy");
        if (starterAssetsInputs.shoot)
        {
            
            if (Physics.Raycast(ray, out RaycastHit enemy, Mathf.Infinity, enemyLayerMask))
            {
                Debug.Log("Hitted");
                enemyController.Hit();

            }
            Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;
            Instantiate(pfBulletProjectile, spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
            
            starterAssetsInputs.shoot = false;
        }

        int itemLayerMask = 1 <<
        LayerMask.NameToLayer("Item");
        if(starterAssetsInputs.interaction) { //if interaction key pressed
            if(Physics.Raycast(ray, out RaycastHit selectedGun, Mathf.Infinity, itemLayerMask))//if aimed to item
            {
                playerGun.SetActive(true);//player gets weapon
                isNear = true;
                Destroy(selectedGun.transform.gameObject);//destroy weapon on map
                enemyController.Spawn();//spawn enemy
            }
            else
            {
                isNear = false;
            }

            if (isNear)
            {
                thirdPersonController.SetGunMotion();//change animation for gun shooting
            }
        }


    }
}
