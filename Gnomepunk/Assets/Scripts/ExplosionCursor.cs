using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionCursor : MonoBehaviour
{
    private Camera cam;
    private const float RAYCAST_DISTANCE = 100f;
    public LayerMask backplaneMask;
    public LayerMask explodableMask;

    public float explosionRadius = 5f;
    public float maxExplosionForce = 20f;
    public float minChargeTime = 1 / 3;
    public float maxChargeTime = 2f;
    public float overChargeTime = 3f;

    private Transform charger;
    private Material chargerMaterial;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        charger = GameObject.Find("Charger").transform;
        chargerMaterial = charger.GetComponent<Renderer>().material;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(nameof(Charge));
        }
    }

    private IEnumerator Charge()
    {
        float chargeTimer = 0f;
        float percentage = 0f;
        while (Input.GetMouseButton(0))
        {
            chargeTimer += Time.deltaTime;
            if (chargeTimer > overChargeTime)
            {
                OverCharge();
                yield break;
            }
            percentage = Mathf.Min(chargeTimer.Remap(0, maxChargeTime, 0, 100), 100);

            charger.localScale = new Vector3(percentage.Remap(0, 100, .1f, 1f), .1f, .1f);
            if (chargeTimer < minChargeTime)
            {
                chargerMaterial.color = Color.black;
            }
            else if (chargeTimer > minChargeTime && chargeTimer < maxChargeTime)
            {
                chargerMaterial.color = Color.green;
            }
            else
            {
                chargerMaterial.color = Color.red;
            }

            yield return null;
        }

        if (chargeTimer > minChargeTime)
            Explode(percentage);
        else
            FizzleOut();
    }

    private void Explode(float percentage)
    {
        ResetCharger();
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, RAYCAST_DISTANCE, backplaneMask))
        {
            Collider[] explodables = Physics.OverlapSphere(hit.point, explosionRadius, explodableMask);
            for (int i = 0; i < explodables.Length; i++)
            {
                IExplodable explodable = explodables[i].GetComponent<IExplodable>();
                explodable?.Explode(hit.point, maxExplosionForce * (percentage / 100), explosionRadius);
            }
        }
    }

    private void OverCharge()
    {
        ResetCharger();
        Debug.Log("Overcharged");
    }

    private void FizzleOut()
    {
        ResetCharger();
        Debug.Log("Fizzled Out");
    }

    private void ResetCharger()
    {
        charger.localScale = new Vector3(.1f, .1f, .1f);
        chargerMaterial.color = Color.black;
    }
}