using UnityEngine;
using System;
using System.Collections;

public class LeverController : MonoBehaviour
{
    public Vector3 offRotation; // OFF rotasyonu
    public Vector3 onRotation;  // ON rotasyonu
    public GameObject sphere;   // Küre objesi
    public GameObject target2Object; // Materyali deðiþtirilecek 2. hedef obje
    public GameObject target4Object; // Materyali deðiþtirilecek 4. hedef obje
    public GameObject target1Object; // Materyali deðiþtirilecek 1. hedef obje
    public GameObject target3Object; // Materyali deðiþtirilecek 3. hedef obje
    public Material sphereOffMaterial; // Küre OFF durumu materyali
    public Material sphereOnMaterial;  // Küre ON durumu materyali

    public Material target2OffMaterial; // Hedef obje OFF durumu materyali
    public Material target2OnMaterial;  // Hedef obje ON durumu materyali

    public Material target4OffMaterial; // Hedef obje OFF durumu materyali
    public Material target4OnMaterial;  // Hedef obje ON durumu materyali

    public Material target1OffMaterial; // Hedef obje OFF durumu materyali
    public Material target1OnMaterial;  // Hedef obje ON durumu materyali

    public Material target3OffMaterial; // Hedef obje OFF durumu materyali
    public Material target3OnMaterial;  // Hedef obje ON durumu materyali

    private bool isOn = false;  // Tuþun durumu, baþlangýçta OFF (false)
    public float transitionSpeed = 1.0f; // Geçiþ hýzý
    private Quaternion targetRotation; // Hedef rotasyon
    private bool isTransitioning = false; // Geçiþin devam edip etmediði
    public float materialChangeDelay = 1.0f; // Materyal deðiþim gecikmesi

    // Materyal deðiþim olayý
    public static event Action<GameObject, Material> OnMaterialChanged;

    void Start()
    {
        transform.localRotation = Quaternion.Euler(offRotation); // Tuþun baþlangýç rotasyonunu ayarla
        targetRotation = Quaternion.Euler(offRotation);
    }

    void OnMouseDown()
    {
        if (isOn)
        {
            // Tuþ þu anda ON, OFF rotasyonuna geç
            targetRotation = Quaternion.Euler(offRotation);
            ChangeSphereMaterial(sphereOffMaterial, false); // Küre materyalini sphereOffMaterial'e dönüþtür ve Emission kapat
            StartCoroutine(ChangeTargetMaterialWithDelay(target2Object, target2OffMaterial, false, materialChangeDelay)); // Hedef objenin materyalini gecikmeyle deðiþtir
            StartCoroutine(ChangeTargetMaterialWithDelay(target4Object, target4OffMaterial, false, materialChangeDelay));
            StartCoroutine(ChangeTargetMaterialWithDelay(target3Object, target3OffMaterial, false, materialChangeDelay));
            StartCoroutine(ChangeTargetMaterialWithDelay(target1Object, target1OffMaterial, false, materialChangeDelay));
        }
        else
        {
            // Tuþ þu anda OFF, ON rotasyonuna geç
            targetRotation = Quaternion.Euler(onRotation);
            ChangeSphereMaterial(sphereOnMaterial, true); // Küre materyalini sphereOnMaterial'e dönüþtür ve Emission aç
            StartCoroutine(ChangeTargetMaterialWithDelay(target2Object, target2OnMaterial, true, materialChangeDelay)); // Hedef objenin materyalini gecikmeyle deðiþtir
            StartCoroutine(ChangeTargetMaterialWithDelay(target4Object, target4OnMaterial, true, materialChangeDelay));
            StartCoroutine(ChangeTargetMaterialWithDelay(target3Object, target3OnMaterial, true, materialChangeDelay));
            StartCoroutine(ChangeTargetMaterialWithDelay(target1Object, target1OnMaterial, true, materialChangeDelay));
        }
        isOn = !isOn; // Durumu tersine çevir
        isTransitioning = true; // Geçiþin baþladýðýný belirt
    }

    void Update()
    {
        if (isTransitioning)
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * transitionSpeed);

            // Hedef rotasyona çok yakýn olup olmadýðýný kontrol et
            if (Quaternion.Angle(transform.localRotation, targetRotation) < 0.1f)
            {
                transform.localRotation = targetRotation; // Rotasyonu tam olarak hedef rotasyona ayarla
                isTransitioning = false; // Geçiþi durdur
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
                renderer.material = material; // Küre materyalini belirtilen materyale ayarla
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

                // Materyal deðiþim olayýný tetikle
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
