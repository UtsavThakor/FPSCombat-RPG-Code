using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public float damage = 25f;
    public float range = 100f;
    public int maxAmmo = 15;
    public int minAmmo = 0;
    
    public float reloadTime = 1.5f;
    public float shotStaminaCost = 20f;
    public int currentAmmo;
    private bool isReloading = false;
    private float reloadTimer = 0f;
    private PlayerStats stats;


    // RECOIL
    public float recoilAmount = 2f;
    public float recoilSpeed = 10f;
    public float returnSpeed = 5f;
    private float lastRecoil = 0f;
    private Camera playerCamera;
    private MouseLook mouseLook;
    private float currentRecoil = 0f;
    private float targetRecoil = 0f;
    private AudioSource audioSource;

    void Start()
    {
        playerCamera = GetComponent<Camera>();
        mouseLook = GetComponent<MouseLook>();
        audioSource = GetComponent<AudioSource>();
        currentAmmo = maxAmmo;
        stats = GetComponentInParent<PlayerStats>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isReloading && currentAmmo > 0)
            Shoot();

        // RECOIL
        currentRecoil = Mathf.Lerp(currentRecoil, targetRecoil, recoilSpeed * Time.deltaTime);
        targetRecoil = Mathf.Lerp(targetRecoil, 0f, returnSpeed * Time.deltaTime);
        float recoilDelta = currentRecoil - lastRecoil;
        mouseLook.xRotation -= recoilDelta;
        mouseLook.xRotation = Mathf.Clamp(mouseLook.xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(mouseLook.xRotation, 0f, 0f);
        lastRecoil = currentRecoil;

        // RELOAD
        if (currentAmmo < maxAmmo && Input.GetKeyDown(KeyCode.R))
        {
            isReloading = true;
        }

        if (isReloading)
        {
            reloadTimer += Time.deltaTime;
            if (reloadTimer >= reloadTime)
            {
                currentAmmo = maxAmmo;
                isReloading = false;
                stats.isReloading = false;
                reloadTimer = 0f;
            }
        }

        if (currentAmmo == 0 && !isReloading)
        {
            isReloading = true;
            stats.isReloading = true;
        }
    }

    void Shoot()
    {
        audioSource.Play();
        targetRecoil += recoilAmount;
        currentAmmo -= 1;
        stats.DrainStamina(shotStaminaCost);

        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range))
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                EnemyHealth enemyHealth = hit.transform.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                    enemyHealth.TakeDamage(damage);
            }
        }
    }
}