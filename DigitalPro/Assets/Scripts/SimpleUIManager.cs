using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SimpleUIManager : MonoBehaviour
{
    [Header("UI Components")]
    public TMP_InputField textInputField;
    public Button speakButton;
    public TextMeshProUGUI statusText;
    
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip testClip; // Drag a test audio clip here
    
    void Start()
    {
        InitializeUI();
        SetupEventListeners();
    }
    
    void InitializeUI()
    {
        if (textInputField != null)
        {
            textInputField.text = "Xin chào! Tôi là Doraemon!";
        }
        
        UpdateStatusText("Ready! (TTS temporarily disabled)");
    }
    
    void SetupEventListeners()
    {
        if (speakButton != null)
        {
            speakButton.onClick.AddListener(OnSpeakButtonClicked);
        }
        
        if (textInputField != null)
        {
            textInputField.onSubmit.AddListener(OnTextSubmitted);
        }
    }
    
    void OnSpeakButtonClicked()
    {
        string text = textInputField?.text;
        
        if (string.IsNullOrEmpty(text))
        {
            UpdateStatusText("Please enter some text first!");
            return;
        }
        
        // For now, just play test audio instead of TTS
        PlayTestAudio();
        
        UpdateStatusText($"Would speak: '{text}'");
    }
    
    void OnTextSubmitted(string text)
    {
        OnSpeakButtonClicked();
    }
    
    void PlayTestAudio()
    {
        if (audioSource != null && testClip != null)
        {
            audioSource.clip = testClip;
            audioSource.Play();
            UpdateStatusText("Playing test audio...");
        }
        else
        {
            UpdateStatusText("No test audio clip assigned");
        }
    }
    
    void UpdateStatusText(string message)
    {
        if (statusText != null)
        {
            statusText.text = message;
        }
        
        Debug.Log("Status: " + message);
    }
    
    public void SetInputText(string text)
    {
        if (textInputField != null)
        {
            textInputField.text = text;
        }
    }
    
    public string GetInputText()
    {
        return textInputField?.text ?? "";
    }
}