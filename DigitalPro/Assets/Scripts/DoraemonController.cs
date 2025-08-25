using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DoraemonController : MonoBehaviour
{
    [Header("UI Components")]
    public TMP_InputField textInputField;
    public Button speakButton;
    public TextMeshProUGUI statusText;
    
    [Header("Audio")]
    public AudioSource audioSource;
    
    [Header("Doraemon Character")]
    public GameObject doraemonModel;
    private SimpleLipSync lipSync;
    
    void Start()
    {
        SetupUI();
        SetupDoraemon();
        SetupAudio();
    }
    
    void SetupUI()
    {
        if (textInputField != null)
        {
            textInputField.text = "Xin chào! Tôi là Doraemon!";
        }
        
        if (speakButton != null)
        {
            speakButton.onClick.AddListener(OnSpeakClicked);
        }
        
        UpdateStatus("Ready to test!");
    }
    
    void SetupDoraemon()
    {
        // Tìm Doraemon model trong scene
        if (doraemonModel == null)
        {
            doraemonModel = GameObject.Find("doreamon") ?? GameObject.Find("doreamon (Clone)");
        }
        
        if (doraemonModel != null)
        {
            // Thêm SimpleLipSync component
            lipSync = doraemonModel.GetComponent<SimpleLipSync>();
            if (lipSync == null)
            {
                lipSync = doraemonModel.AddComponent<SimpleLipSync>();
            }
        }
    }
    
    void SetupAudio()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
        
        // Cấu hình audio source
        audioSource.playOnAwake = false;
        audioSource.volume = 0.8f;
        audioSource.pitch = 1.2f;
    }
    
    public void OnSpeakClicked()
    {
        string text = textInputField?.text ?? "";
        
        if (string.IsNullOrEmpty(text))
        {
            UpdateStatus("Please enter some text!");
            return;
        }
        
        StartCoroutine(SpeakWithLipSync(text));
    }
    
    IEnumerator SpeakWithLipSync(string text)
    {
        UpdateStatus($"Speaking: {text}");
        
        // Bắt đầu lip sync
        if (lipSync != null)
        {
            lipSync.StartLipSync();
        }
        
        // Tạo audio và phát
        AudioClip speechClip = GenerateSpeechAudio(text);
        if (speechClip != null && audioSource != null)
        {
            audioSource.clip = speechClip;
            audioSource.Play();
            
            // Chờ audio kết thúc
            while (audioSource.isPlaying)
            {
                yield return null;
            }
        }
        else
        {
            // Fallback: chỉ lip sync
            float duration = text.Length * 0.1f;
            yield return new WaitForSeconds(duration);
        }
        
        // Dừng lip sync
        if (lipSync != null)
        {
            lipSync.StopLipSync();
        }
        
        UpdateStatus("Finished speaking!");
    }
    
    AudioClip GenerateSpeechAudio(string text)
    {
        // Tạo audio clip đơn giản
        int sampleRate = 44100;
        float duration = text.Length * 0.1f;
        int samples = (int)(sampleRate * duration);
        
        AudioClip clip = AudioClip.Create("Speech", samples, 1, sampleRate, false);
        float[] data = new float[samples];
        
        for (int i = 0; i < samples; i++)
        {
            // Tạo sóng sine với tần số thay đổi
            float frequency = 440f + Mathf.Sin(i * 0.01f) * 100f;
            data[i] = Mathf.Sin(2f * Mathf.PI * frequency * i / sampleRate) * 0.3f;
        }
        
        clip.SetData(data, 0);
        return clip;
    }
    
    void UpdateStatus(string message)
    {
        if (statusText != null)
        {
            statusText.text = message;
        }
        Debug.Log($"Status: {message}");
    }
}