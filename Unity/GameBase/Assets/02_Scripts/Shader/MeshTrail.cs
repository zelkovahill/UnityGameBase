using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshTrail : MonoBehaviour
{
    public float activeTime = 2f;
    // public MovementInput moveScript;
    public float speedBoost = 6;
    // public Animator animator;
    public float animSpeedBoost = 1.5f;

    [Header("Mesh Releted")]
    public float meshRefreshRate = 0.1f;
    public float meshDestoryDelay = 3.0f;
    public Transform positionToSpawn;

    [Header("Shader Releted")]
    public Material mat;
    public string shaderVarRef;
    public float shaderVarRate = 0.1f;
    public float shaderVarRefreshRate = 0.05f;

    private SkinnedMeshRenderer[] skinnedRenderer;
    private bool isTrailActive;

    private float normalSpeed;
    private float normalAnimSpeed;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isTrailActive)
        {
            isTrailActive = true;
            StartCoroutine(ActivateTrail(activeTime));
        }
    }

    private IEnumerator AnimateMaterialFloat(Material m, float valueGoal, float rate, float refreshRate)
    {
        float valueToAnimate = m.GetFloat(shaderVarRef);

        while (valueToAnimate > valueGoal)
        {
            valueToAnimate -= rate;
            m.SetFloat(shaderVarRef, valueToAnimate);
            yield return new WaitForSeconds(refreshRate);
        }
    }

    private IEnumerator ActivateTrail(float timeActivated)
    {
        // normalSpeed = moveScript.movementSpeed;                 
        // moveScript.movementSpeed = speedBoost;

        // normalAnimSpeed = animator.GetFloat("animSpeed");       
        // animator.SetFloat("animSpeed", animSpeedBoost);

        while (timeActivated > 0)
        {
            timeActivated -= meshRefreshRate;

            if (skinnedRenderer == null)
            {
                skinnedRenderer = positionToSpawn.GetComponentsInChildren<SkinnedMeshRenderer>();
            }

            for (int i = 0; i < skinnedRenderer.Length; i++)
            {
                GameObject go = new GameObject();
                go.transform.SetPositionAndRotation(positionToSpawn.position, positionToSpawn.rotation);

                MeshRenderer mr = go.AddComponent<MeshRenderer>();
                MeshFilter mf = go.AddComponent<MeshFilter>();

                Mesh m = new Mesh();
                skinnedRenderer[i].BakeMesh(m);
                mf.mesh = m;
                mr.material = mat;

                StartCoroutine(AnimateMaterialFloat(mr.material, 0, shaderVarRate, shaderVarRefreshRate));

                Destroy(go, meshDestoryDelay);
            }

            yield return new WaitForSeconds(meshRefreshRate);
        }

        // moveScript.movementSpeed = normalSpeed;
        // animator.SetFloat("animSpeed", normalAnimSpeed);
        isTrailActive = false;
    }

}
