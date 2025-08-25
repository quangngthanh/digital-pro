using UnityEngine;
using System.Speech.Synthesis;
using System.IO;
using System.Collections;

public class TextToSpeechManager : MonoBehaviour
{
    [Header("TTS Settings")]
    public string voiceName = "Microsoft David Desktop"; // Default Windows voice
    public int rate = 0; // Speech rate (-10 to 10)
    public int volume = 100; // Volume (0 to 100)
    
    [Header("Audio Settings")]
    public AudioSource audioSource;
    public string audioFileName = "speech.wav";
    
    private SpeechSynthesizer synthesizer;
    private string audioPath;
    
    void Start()
    {
        // Initialize TTS synthesizer
        synthesizer = new SpeechSynthesizer();
        
        // Set audio file path
        audioPath = Path.Combine(Application.persistentDataPath, audioFileName);
        
        // Get audio source component
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
            
        // Configure synthesizer
        ConfigureSynthesizer();
    }
    
    void ConfigureSynthesizer()
    {
        try
        {
            // Set voice
            synthesizer.SelectVoice(voiceName);
            
            // Set speech rate and volume
            synthesizer.Rate = rate;
            synthesizer.Volume = volume;
            
            Debug.Log("TTS Synthesizer configured successfully");
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to configure TTS: " + e.Message);
        }
    }
    
    public void SpeakText(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            Debug.LogWarning("No text to speak");
            return;
        }
        
        StartCoroutine(GenerateAndPlaySpeech(text));
    }
    
    IEnumerator GenerateAndPlaySpeech(string text)
    {
        try
        {
            // Generate speech audio file
            synthesizer.SetOutputToWaveFile(audioPath);
            synthesizer.Speak(text);
            synthesizer.SetOutputToDefaultAudioDevice();
            
            Debug.Log("Audio file generated: " + audioPath);
            
            // Wait a frame to ensure file is written
            yield return new WaitForEndOfFrame();
            
            // Load and play audio
            yield return StartCoroutine(LoadAndPlayAudio());
        }
        catch (System.Exception e)
        {
            Debug.LogError("TTS Error: " + e.Message);
        }
    }
    
    IEnumerator LoadAndPlayAudio()
    {
        string url = "file://" + audioPath;
        
        using (WWW www = new WWW(url))
        {
            yield return www;
            
            if (string.IsNullOrEmpty(www.error))
            {
                // Create AudioClip from downloaded data
                AudioClip clip = www.GetAudioClip(false, false);
                
                if (clip != null)
                {
                    audioSource.clip = clip;
                    audioSource.Play();
                    
                    Debug.Log("Playing speech audio");
                    
                    // Trigger lip sync (we'll add this later)
                    OnSpeechStarted?.Invoke(clip);
                }
                else
                {
                    Debug.LogError("Failed to create AudioClip");
                }
            }
            else
            {
                Debug.LogError("Failed to load audio: " + www.error);
            }
        }
    }
    
    // Event for lip sync integration
    public System.Action<AudioClip> OnSpeechStarted;
    
    void OnDestroy()
    {
        synthesizer?.Dispose();
    }
    
    // Public methods for UI controls
    public void SetRate(int newRate)
    {
        rate = Mathf.Clamp(newRate, -10, 10);
        synthesizer.Rate = rate;
    }
    
    public void SetVolume(int newVolume)
    {
        volume = Mathf.Clamp(newVolume, 0, 100);
        synthesizer.Volume = volume;
    }
    
    public void StopSpeaking()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}