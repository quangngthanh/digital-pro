using UnityEngine;
using System.Collections;

public class SimpleLipSync : MonoBehaviour
{
    [Header("Lip Sync Settings")]
    public float lipSyncSpeed = 0.1f;
    public float scaleAmount = 1.2f;
    
    private Vector3 originalScale;
    private bool isSpeaking = false;
    private Coroutine lipSyncCoroutine;
    
    void Start()
    {
        originalScale = transform.localScale;
    }
    
    public void StartLipSync()
    {
        if (isSpeaking) return;
        
        isSpeaking = true;
        if (lipSyncCoroutine != null)
        {
            StopCoroutine(lipSyncCoroutine);
        }
        lipSyncCoroutine = StartCoroutine(LipSyncAnimation());
    }
    
    public void StopLipSync()
    {
        isSpeaking = false;
        if (lipSyncCoroutine != null)
        {
            StopCoroutine(lipSyncCoroutine);
            lipSyncCoroutine = null;
        }
        transform.localScale = originalScale;
    }
    
    IEnumerator LipSyncAnimation()
    {
        while (isSpeaking)
        {
            // Scale up (mở miệng)
            transform.localScale = originalScale * scaleAmount;
            yield return new WaitForSeconds(lipSyncSpeed);
            
            // Scale down (đóng miệng)
            transform.localScale = originalScale;
            yield return new WaitForSeconds(lipSyncSpeed);
        }
    }
}
