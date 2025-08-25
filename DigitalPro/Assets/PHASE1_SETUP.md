# SETUP PHASE 1 - CHỈ TEXT-TO-SPEECH (KHÔNG LIP SYNC)

## Bước 1: Setup Scene cơ bản

1. **Mở Unity project** (`D:\Working\DigitalPro\DigitalPro`)

2. **Tạo Scene mới:**
   - File → New Scene
   - Save as "DoraemonTTS"

3. **Tạo Hierarchy structure:**
   ```
   DoraemonTTS Scene
   ├── Main Camera
   ├── Directional Light  
   ├── TTS Manager (Empty GameObject)
   └── UI Canvas
   ```

## Bước 2: Setup TTS Manager

1. **Tạo Empty GameObject** tên "TTS Manager"
2. **Add Components:**
   - `TextToSpeechManager.cs` (đã tạo)
   - `AudioSource`
3. **Configure AudioSource:**
   - ✅ Play On Awake: false
   - ✅ Spatial Blend: 0 (2D)

## Bước 3: Setup UI Canvas 

### 3.1 Tạo UI Canvas:
1. **Right-click Hierarchy** → UI → Canvas
2. **Canvas Scaler settings:**
   - UI Scale Mode: Scale With Screen Size
   - Reference Resolution: 1920 x 1080

### 3.2 Tạo UI Elements:

#### Text Input Field:
1. **Right-click Canvas** → UI → Input Field - TextMeshPro
2. **Rename**: "TextInputField"
3. **RectTransform:**
   - Anchor: Top-Center
   - Width: 600, Height: 50
   - Pos Y: -100

#### Speak Button:
1. **Right-click Canvas** → UI → Button - TextMeshPro
2. **Rename**: "SpeakButton"
3. **Text**: "Speak"
4. **Position**: Below input field
5. **Size**: 120 x 40

#### Stop Button:
1. **Right-click Canvas** → UI → Button - TextMeshPro
2. **Rename**: "StopButton"  
3. **Text**: "Stop"
4. **Position**: Next to Speak button
5. **Size**: 120 x 40

#### Status Text:
1. **Right-click Canvas** → UI → Text - TextMeshPro
2. **Rename**: "StatusText"
3. **Text**: "Ready to speak!"
4. **Position**: Bottom center
5. **Font Size**: 18

## Bước 4: Connect Scripts

1. **Add UIManager.cs** to Canvas
2. **Drag references trong Inspector:**
   - TextInputField → textInputField
   - SpeakButton → speakButton
   - StopButton → stopButton
   - StatusText → statusText
   - TTS Manager → ttsManager

## Bước 5: Test TTS

1. **Play scene**
2. **Nhập text** vào input field
3. **Click Speak** → Should hear Windows TTS voice

## Troubleshooting

### Error: "SpeechSynthesizer not found"
**Solution:**
```csharp
// Add these at top of TextToSpeechManager.cs:
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
using System.Speech.Synthesis;
#endif
```

### Error: Build issues
**Solution:**
- Player Settings → Configuration: Master
- Scripting Backend: Mono

## Next Steps

1. **Get Phase 1 working** (TTS only)
2. **Add Doraemon model** (optional - có thể dùng basic cube)
3. **Find Oculus LipSync** package
4. **Move to Phase 2** (LipSync integration)

---
📝 Focus on TTS first, LipSync later!
