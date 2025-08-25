using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("UI Components")]
    public TMP_InputField textInputField;
    public Button speakButton;
    public Button stopButton;
    public Slider rateSlider;
    public Slider volumeSlider;
    public TextMeshProUGUI statusText;
    
    [Header("References")]
    public TextToSpeechManager ttsManager;
    
    void Start()
    {
        InitializeUI();
        SetupEventListeners();
    }
    
    void InitializeUI()
    {
        // Set default values
        if (textInputField != null)
        {
            textInputField.text = "Xin chào! Tôi là Doraemon. Tôi có thể nói bất cứ điều gì bạn muốn!";
        }
        
        if (rateSlider != null)
        {
            rateSlider.minValue = -10;
            rateSlider.maxValue = 10;
            rateSlider.value = 0;
        }
        
        if (volumeSlider != null)
        {
            volumeSlider.minValue = 0;
            volumeSlider.maxValue = 100;
            volumeSlider.value = 100;
        }
        
        UpdateStatusText("Ready to speak!");
    }
    
    void SetupEventListeners()
    {
        // Speak button
        if (speakButton != null)
        {
            speakButton.onClick.AddListener(OnSpeakButtonClicked);
        }
        
        // Stop button
        if (stopButton != null)
        {
            stopButton.onClick.AddListener(OnStopButtonClicked);
        }
        
        // Rate slider
        if (rateSlider != null)
        {
            rateSlider.onValueChanged.AddListener(OnRateChanged);
        }
        
        // Volume slider
        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        }
        
        // Enter key shortcut
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
        
        if (ttsManager != null)
        {
            UpdateStatusText("Speaking...");
            ttsManager.SpeakText(text);
        }
        else
        {
            UpdateStatusText("TTS Manager not found!");
        }
    }
    
    void OnStopButtonClicked()
    {
        if (ttsManager != null)
        {
            ttsManager.StopSpeaking();
            UpdateStatusText("Speech stopped");
        }
    }
    
    void OnRateChanged(float value)
    {
        if (ttsManager != null)
        {
            ttsManager.SetRate((int)value);
            UpdateStatusText($"Speech rate: {value:F0}");
        }
    }
    
    void OnVolumeChanged(float value)
    {
        if (ttsManager != null)
        {
            ttsManager.SetVolume((int)value);
            UpdateStatusText($"Volume: {value:F0}%");
        }
    }
    
    void OnTextSubmitted(string text)
    {
        OnSpeakButtonClicked();
    }
    
    void UpdateStatusText(string message)
    {
        if (statusText != null)
        {
            statusText.text = message;
        }
        
        Debug.Log("Status: " + message);
    }
    
    // Public methods for external control
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