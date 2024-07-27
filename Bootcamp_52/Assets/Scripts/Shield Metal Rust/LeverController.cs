using UnityEngine;
using System;
using System.Collections;

public class LeverController : MonoBehaviour
{
    public Vector3 offRotation; // OFF rotasyonu
    public Vector3 onRotation;  // ON rotasyonu
    public GameObject sphere;   // K�re objesi
    public GameObject target2Object; // Materyali de�i�tirilecek 2. hedef obje
    public GameObject target4Object; // Materyali de�i�tirilecek 4. hedef obje
    public GameObject target1Object; // Materyali de�i�tirilecek 1. hedef obje
    public GameObject target3Object; // Materyali de�i�tirilecek 3. hedef obje
    public Material sphereOffMaterial; // K�re OFF durumu materyali
    public Material sphereOnMaterial;  // K�re ON durumu materyali

    public Material target2OffMaterial; // Hedef obje OFF durumu materyali
    public Material target2OnMaterial;  // Hedef obje ON durumu materyali

    public Material target4OffMaterial; // Hedef obje OFF durumu materyali
    public Material target4OnMaterial;  // Hedef obje ON durumu materyali

    public Material target1OffMaterial; // Hedef obje OFF durumu materyali
    public Material target1OnMaterial;  // Hedef obje ON durumu materyali

    public Material target3OffMaterial; // Hedef obje OFF durumu materyali
    public Material target3OnMaterial;  // Hedef obje ON durumu materyali

    private bool isOn = false;  // Tu�un durumu, ba�lang��ta OFF (false)
    public float transitionSpeed = 1.0f; // Ge�i� h�z�
    private Quaternion targetRotation; // Hedef rotasyon
    private bool isTransitioning = false; // Ge�i�in devam edip etmedi�i
    public float materialChangeDelay = 1.0f; // Materyal de�i�im gecikmesi

    // Materyal de�i�im olay�
    public static event Action<GameObject, Material> OnMaterialChanged;

    void Start()
    {
        transform.localRotation = Quaternion.Euler(offRotation); // Tu�un ba�lang�� rotasyonunu ayarla
        targetRotation = Quaternion.Euler(offRotation);
    }

    void OnMouseDown()
    {
        if (isOn)
        {
            // Tu� �u anda ON, OFF rotasyonuna ge�
            targetRotation = Quaternion.Euler(offRotation);
            ChangeSphereMaterial(sphereOffMaterial, false); // K�re materyalini sphereOffMaterial'e d�n��t�r ve Emission kapat
            StartCoroutine(ChangeTargetMaterialWithDelay(target2Object, target2OffMaterial, false, materialChangeDelay)); // Hedef objenin materyalini gecikmeyle de�i�tir
            StartCoroutine(ChangeTargetMaterialWithDelay(target4Object, target4OffMaterial, false, materialChangeDelay));
            StartCoroutine(ChangeTargetMaterialWithDelay(target3Object, target3OffMaterial, false, materialChangeDelay));
            StartCoroutine(ChangeTargetMaterialWithDelay(target1Object, target1OffMaterial, false, materialChangeDelay));
        }
        else
        {
            // Tu� �u anda OFF, ON rotasyonuna ge�
            targetRotation = Quaternion.Euler(onRotation);
            ChangeSphereMaterial(sphereOnMaterial, true); // K�re materyalini sphereOnMaterial'e d�n��t�r ve Emission a�
            StartCoroutine(ChangeTargetMaterialWithDelay(target2Object, target2OnMaterial, true, materialChangeDelay)); // Hedef objenin materyalini gecikmeyle de�i�tir
            StartCoroutine(ChangeTargetMaterialWithDelay(target4Object, target4OnMaterial, true, materialChangeDelay));
            StartCoroutine(ChangeTargetMaterialWithDelay(target3Object, target3OnMaterial, true, materialChangeDelay));
            StartCoroutine(ChangeTargetMaterialWithDelay(target1Object, target1OnMaterial, true, materialChangeDelay));
        }
        isOn = !isOn; // Durumu tersine �evir
        isTransitioning = true; // Ge�i�in ba�lad���n� belirt
    }

    void Update()
    {
        if (isTransitioning)
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * transitionSpeed);

            // Hedef rotasyona �ok yak�n olup olmad���n� kontrol et
            if (Quaternion.Angle(transform.localRotation, targetRotation) < 0.1f)
            {
                transform.localRotation = targetRotation; // Rotasyonu tam olarak hedef rotasyona ayarla
                isTransitioning = false; // Ge�i�i durdur
            }
        }
    }

    void ChangeSphereMaterial(Material material, bool enableEmission)
    {
        if (sphere != null)
        {
            Renderer renderer = sphere.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material = material; // K�re materyalini belirtilen materyale ayarla
                if (enableEmission)
                {
                    renderer.material.EnableKeyword("_EMISSION");
                    renderer.material.SetColor("_EmissionColor", material.color);
                }
                else
                {
                    renderer.material.DisableKeyword("_EMISSION");
                }
                Debug.Log("Sphere material changed to: " + material.name);
            }
            else
            {
                Debug.LogError("Sphere does not have a Renderer component!");
            }
        }
        else
        {
            Debug.LogError("Sphere is not assigned!");
        }
    }

    IEnumerator ChangeTargetMaterialWithDelay(GameObject target, Material material, bool enableEmission, float delay)
    {
        yield return new WaitForSeconds(delay);
        ChangeTargetMaterial(target, material, enableEmission);
    }

    void ChangeTargetMaterial(GameObject target, Material material, bool enableEmission)
    {
        if (target != null)
        {
            Renderer renderer = target.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material = material; // Hedef objenin materyalini belirtilen materyale ayarla
                if (enableEmission)
                {
                    renderer.material.EnableKeyword("_EMISSION");
                    renderer.material.SetColor("_EmissionColor", material.color);
                }
                else
                {
                    renderer.material.DisableKeyword("_EMISSION");
                }

                // Materyal de�i�im olay�n� tetikle
                OnMaterialChanged?.Invoke(target, material);

                Debug.Log("Target object material changed to: " + material.name);
            }
            else
            {
                Debug.LogError("Target object does not have a Renderer component!");
            }
        }
        else
        {
            Debug.LogError("Target object is not assigned!");
        }
    }
}
