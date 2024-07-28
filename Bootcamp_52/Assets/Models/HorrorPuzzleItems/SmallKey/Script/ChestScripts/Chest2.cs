using UnityEngine;
using System.Collections;

public class Chest2 : MonoBehaviour
{
    public Vector3 closedRotation = new Vector3(0f, 0f, 0f); // Kapal� pozisyon rotasyonu
    public Vector3 openRotation = new Vector3(0f, -90f, 0f); // A��k pozisyon rotasyonu
    public float openCloseSpeed = 50f;  // A��l�� ve kapan�� h�z�
    public AudioSource audioSource;     // Ses kayna��
    public GameObject lockPiecePrefab;  // Kilit par�as� prefab�
    public Transform lockDropPosition;  // Kilit par�as�n�n d��ece�i konum
    public string requiredKey; // Gereken anahtar�n ad�
    private bool isOpen = false;        // Sand���n durumu, ba�lang��ta kapal�
    private GameObject player;          // Oyuncu kaps�l�ne referans
    private GameObject lockPiece;       // Olu�turulan kilit par�as� referans�

    void Start()
    {
        // Sand���n ba�lang�� pozisyonunu kapal� olarak ayarla
        transform.localRotation = Quaternion.Euler(closedRotation);
        player = GameObject.Find("FirstPersonController"); // Oyuncu kaps�l�n� bul
        lockPiece = null; // Ba�lang��ta kilit par�as� yok
    }

    void Update()
    {
        // Sand���n yak�n�nda olup olmad���n� ve E tu�una bas�l�p bas�lmad���n� kontrol et
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
        // Envanterde gereken anahtar olup olmad���n� kontrol et
        if (Inventory.Instance.HasItem(requiredKey))
        {
            // A��k pozisyon rotasyonunu hedef alarak sand��� a�
            Quaternion targetRotation = Quaternion.Euler(openRotation);
            StartCoroutine(RotateChest(targetRotation)); // Coroutine ba�lat
            isOpen = true;

            // Ses �al
            if (audioSource != null)
            {
                audioSource.Play();
            }

            // Anahtar� envanterden ��kar
            Inventory.Instance.RemoveItem(requiredKey);

            // Kilit par�as�n� yere d���r
            DropLockPiece();
        }
        else
        {
            Debug.Log("You need the " + requiredKey + " to open this chest.");
        }
    }

    void CloseChest()
    {
        // Kapal� pozisyon rotasyonunu hedef alarak sand��� kapat
        Quaternion targetRotation = Quaternion.Euler(closedRotation);
        StartCoroutine(RotateChest(targetRotation)); // Coroutine ba�lat
        isOpen = false;

        // Ses �al
        if (audioSource != null)
        {
            audioSource.Play();
        }

        // Kilit par�as�n� yok et
        DestroyLockPiece();
    }

    IEnumerator RotateChest(Quaternion targetRotation)
    {
        // Sand��� hedef rotasyona do�ru d�nd�r
        while (Quaternion.Angle(transform.localRotation, targetRotation) > 0.1f)
        {
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, openCloseSpeed * Time.deltaTime);
            yield return null;
        }
        transform.localRotation = targetRotation; // Rotasyonu tam olarak hedef rotasyona ayarla
    }

    void DropLockPiece()
    {
        // E�er kilit par�as� daha �nce olu�turulmad�ysa
        if (lockPiece == null && lockPiecePrefab != null && lockDropPosition != null)
        {
            // Kilit par�as� prefab�ndan yeni bir klon olu�tur
            lockPiece = Instantiate(lockPiecePrefab, lockDropPosition.position, Quaternion.identity);
            Rigidbody rb = lockPiece.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false; // Nesnenin kinematik olmayan (yani fiziksel olarak etkile�ime girebilen) olmas�n� sa�lar
                rb.useGravity = true; // Yer�ekimi etkisini ba�lat�r, b�ylece nesne d��er
            }
        }
    }

    void DestroyLockPiece()
    {
        // E�er kilit par�as� varsa yok et
        if (lockPiece != null)
        {
            Destroy(lockPiece);
            lockPiece = null; // Referans� null yaparak art�k yok oldu�unu belirt
        }
    }
}
