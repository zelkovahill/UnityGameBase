using Cinemachine;
using StarterAssets;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerManager : MonoBehaviour
{
    [Header("Aim")]
    [SerializeField]
    private CinemachineVirtualCamera aimCam;

    [SerializeField]
    private GameObject aimImage;

    [SerializeField]
    private GameObject aimObject;

    [SerializeField]
    private LayerMask targetLayer;

    [SerializeField]
    private float aimObjectDistance = 10f;

    [Header("IK")]
    [SerializeField]
    private Rig handRig;

    [SerializeField]
    private Rig aimRig;


    private StarterAssetsInputs _starterAssetsInputs;
    private ThirdPersonController _thirdPersonController;
    private Animator _animator;


    private void Awake()
    {
        _starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        _thirdPersonController = GetComponent<ThirdPersonController>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        AimCheck();
    }

    private void AimCheck()
    {
        if (_starterAssetsInputs.reload)
        {
            _starterAssetsInputs.reload = false;

            if (_thirdPersonController.isReload)
            {
                return;
            }

            AimControll(false);
            SetRigWeight(0);
            _animator.SetLayerWeight(1, 1);
            _animator.SetTrigger("Reload");
            _thirdPersonController.isReload = true;
        }

        if (_thirdPersonController.isReload)
        {
            return;
        }


        if (_starterAssetsInputs.aim)
        {
            AimControll(true);

            _animator.SetLayerWeight(1, 1);

            Vector3 targetPosition = Vector3.zero;
            Transform camTransform = Camera.main.transform;
            RaycastHit hit;

            if (Physics.Raycast(camTransform.position, camTransform.forward, out hit, Mathf.Infinity, targetLayer))
            {
                targetPosition = hit.point;
                aimObject.transform.position = hit.point;
            }
            else
            {
                targetPosition = camTransform.position + camTransform.forward * aimObjectDistance;
                aimObject.transform.position = camTransform.position + camTransform.forward * aimObjectDistance;
            }

            Vector3 targetAim = targetPosition;
            targetAim.y = transform.position.y;
            Vector3 aimDirection = (targetAim - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 50f);

            SetRigWeight(1);

            if (_starterAssetsInputs.shoot)
            {
                _animator.SetBool("Shoot", true);
            }
            else
            {
                _animator.SetBool("Shoot", false);
            }
        }
        else
        {
            AimControll(false);
            SetRigWeight(0);
            _animator.SetLayerWeight(1, 0);
            _animator.SetBool("Shoot", false);
        }
    }

    private void AimControll(bool isCheck)
    {
        aimCam.gameObject.SetActive(isCheck);
        aimImage.SetActive(isCheck);
        _thirdPersonController.isAimMove = isCheck;
    }

    public void Reload()
    {
        // Debug.Log("Reload");
        _thirdPersonController.isReload = false;
        SetRigWeight(1);
        _animator.SetLayerWeight(1, 0);
    }

    private void SetRigWeight(float weight)
    {
        handRig.weight = weight;
        aimRig.weight = weight;
    }
}
