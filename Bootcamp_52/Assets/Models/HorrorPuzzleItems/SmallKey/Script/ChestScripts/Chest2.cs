using UnityEngine;
using System.Collections;

public class Chest2 : MonoBehaviour
{
    public Vector3 closedRotation = new Vector3(0f, 0f, 0f); // Kapalý pozisyon rotasyonu
    public Vector3 openRotation = new Vector3(0f, -90f, 0f); // Açýk pozisyon rotasyonu
    public float openCloseSpeed = 50f;  // Açýlýþ ve kapanýþ hýzý
    public AudioSource audioSource;     // Ses kaynaðý
    public GameObject lockPiecePrefab;  // Kilit parçasý prefabý
    public Transform lockDropPosition;  // Kilit parçasýnýn düþeceði konum
    public string requiredKey; // Gereken anahtarýn adý
    private bool isOpen = false;        // Sandýðýn durumu, baþlangýçta kapalý
    private GameObject player;          // Oyuncu kapsülüne referans
    private GameObject lockPiece;       // Oluþturulan kilit parçasý referansý

    void Start()
    {
        // Sandýðýn baþlangýç pozisyonunu kapalý olarak ayarla
        transform.localRotation = Quaternion.Euler(closedRotation);
        player = GameObject.Find("FirstPersonController"); // Oyuncu kapsülünü bul
        lockPiece = null; // Baþlangýçta kilit parçasý yok
    }

    void Update()
    {
        // Sandýðýn yakýnýnda olup olmadýðýný ve E tuþuna basýlýp basýlmadýðýný kontrol et
        if (Vector3.Distance(player.transform.position, transform.position) < 3f && Input.GetKeyDown(KeyCode.E))
        {
            if (isOpen)
            {
                CloseChest();
            }
            else
            {
                OpenChest();
            }
        }
    }

    void OpenChest()
    {
        // Envanterde gereken anahtar olup olmadýðýný kontrol et
        if (Inventory.Instance.HasItem(requiredKey))
        {
            // Açýk pozisyon rotasyonunu hedef alarak sandýðý aç
            Quaternion targetRotation = Quaternion.Euler(openRotation);
            StartCoroutine(RotateChest(targetRotation)); // Coroutine baþlat
            isOpen = true;

            // Ses çal
            if (audioSource != null)
            {
                audioSource.Play();
            }

            // Anahtarý envanterden çýkar
            Inventory.Instance.RemoveItem(requiredKey);

            // Kilit parçasýný yere düþür
            DropLockPiece();
        }
        else
        {
            Debug.Log("You need the " + requiredKey + " to open this chest.");
        }
    }

    void CloseChest()
    {
        // Kapalý pozisyon rotasyonunu hedef alarak sandýðý kapat
        Quaternion targetRotation = Quaternion.Euler(closedRotation);
        StartCoroutine(RotateChest(targetRotation)); // Coroutine baþlat
        isOpen = false;

        // Ses çal
        if (audioSource != null)
        {
            audioSource.Play();
        }

        // Kilit parçasýný yok et
        DestroyLockPiece();
    }

    IEnumerator RotateChest(Quaternion targetRotation)
    {
        // Sandýðý hedef rotasyona doðru döndür
        while (Quaternion.Angle(transform.localRotation, targetRotation) > 0.1f)
        {
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, openCloseSpeed * Time.deltaTime);
            yield return null;
        }
        transform.localRotation = targetRotation; // Rotasyonu tam olarak hedef rotasyona ayarla
    }

    void DropLockPiece()
    {
        // Eðer kilit parçasý daha önce oluþturulmadýysa
        if (lockPiece == null && lockPiecePrefab != null && lockDropPosition != null)
        {
            // Kilit parçasý prefabýndan yeni bir klon oluþtur
            lockPiece = Instantiate(lockPiecePrefab, lockDropPosition.position, Quaternion.identity);
            Rigidbody rb = lockPiece.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false; // Nesnenin kinematik olmayan (yani fiziksel olarak etkileþime girebilen) olmasýný saðlar
                rb.useGravity = true; // Yerçekimi etkisini baþlatýr, böylece nesne düþer
            }
        }
    }

    void DestroyLockPiece()
    {
        // Eðer kilit parçasý varsa yok et
        if (lockPiece != null)
        {
            Destroy(lockPiece);
            lockPiece = null; // Referansý null yaparak artýk yok olduðunu belirt
        }
    }
}
