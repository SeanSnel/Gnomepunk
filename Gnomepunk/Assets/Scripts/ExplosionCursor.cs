using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionCursor : GameCursor
{
    public float explosionRadius = 5f;
    public float maxExplosionForce = 20f;
    public float minChargeTime = 1 / 3;
    public float maxChargeTime = 2f;
    public float overChargeTime = 3f;

    public GameObject particle;

    private Transform charger;
    private Material chargerMaterial;

    private AudioSource MpPlayer;

    public AudioClip explosion;
    public AudioClip scream1;
    public AudioClip scream2;
    public AudioClip scream3;


    // Start is called before the first frame update
    protected override void Start()
    {
        MpPlayer = GetComponent<AudioSource>();
        particle.GetComponent<ParticleSystem>().Stop();
        charger = GameObject.Find("Charger").transform;
        chargerMaterial = charger.GetComponent<Renderer>().material;
        base.Start();
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
            MpPlayer.PlayOneShot(explosion, percentage/100);
            particle.transform.position = hit.point;
            particle.transform.localScale = new Vector3((percentage / 100) + 0.1f, (percentage / 100) + 0.1f, (percentage / 100)+0.1f);
            particle.GetComponent<ParticleSystem>().Play();
            Collider[] explodables = Physics.OverlapSphere(hit.point, explosionRadius, interactsWithLayers);
            for (int i = 0; i < explodables.Length; i++)
            {
                
                switch (Random.Range(0, 3))
                {
                    case 0:
                        Debug.Log("1");
                        MpPlayer.PlayOneShot(scream1, 0.25f);
                        break;
                    case 1:
                        Debug.Log("2");
                        MpPlayer.PlayOneShot(scream2, 0.25f);
                        break;
                    case 2:
                        Debug.Log("3");
                        MpPlayer.PlayOneShot(scream3, 0.25f);
                        break;
                    default:
                        Debug.Log("error");
                        MpPlayer.PlayOneShot(scream1, 0.25f);
                        break;
                }
                IExplodable explodable = explodables[i].GetComponent<IExplodable>();
                explodable?.Explode(hit.point, maxExplosionForce * (percentage / 100), explosionRadius);
            }
        }
    }

    private void OverCharge()
    {
        ResetCharger();
    }

    private void FizzleOut()
    {
        ResetCharger();
    }

    private void ResetCharger()
    {
        charger.localScale = new Vector3(.1f, .1f, .1f);
        chargerMaterial.color = Color.black;
    }
}
