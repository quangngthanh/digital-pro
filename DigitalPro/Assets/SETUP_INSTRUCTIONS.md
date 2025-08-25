# HƯỚNG DẪN SETUP SCENE - DORAEMON TTS

## Bước 1: Import Assets

1. **Import Doraemon Model:**
   - Copy Doraemon.fbx vào Assets/Models/
   - Drag vào Scene
   - Đặt tên GameObject là "Doraemon"

2. **Import Oculus LipSync:**
   - Assets → Import Package → Custom Package
   - Chọn OVRLipSync.unitypackage
   - Import tất cả

## Bước 2: Setup Scene Structure

### Hierarchy Structure:
```
SampleScene
├── Main Camera
├── Directional Light
├── Doraemon (3D Model)
├── TTS Manager (Empty GameObject)
├── UI Canvas
│   ├── Text Input Field
│   ├── Speak Button  
│   ├── Stop Button
│   ├── Rate Slider
│   ├── Volume Slider
│   └── Status Text
└── Audio Source (hoặc attach vào TTS Manager)
```

## Bước 3: Setup TTS Manager

1. **Tạo Empty GameObject** tên "TTS Manager"
2. **Add Components:**
   - TextToSpeechManager.cs
   - AudioSource component
3. **Configure AudioSource:**
   - Spatial Blend: 2D (cho UI audio)
   - Play On Awake: false

## Bước 4: Setup UI Canvas

### 4.1 Tạo Canvas:
1. Right-click Hierarchy → UI → Canvas
2. Canvas Scaler → Scale With Screen Size
3. Reference Resolution: 1920 x 1080

### 4.2 Text Input Field:
1. Right-click Canvas → UI → Input Field - TextMeshPro
2. Position: Top center
3. Size: 600 x 40
4. Placeholder: "Enter text to speak..."

### 4.3 Control Buttons:
1. **Speak Button:**
   - UI → Button - TextMeshPro
   - Text: "Speak"
   - Size: 100 x 40
   - Position: Below input field

2. **Stop Button:**
   - UI → Button - TextMeshPro  
   - Text: "Stop"
   - Size: 100 x 40
   - Position: Next to Speak button

### 4.4 Control Sliders:
1. **Rate Slider:**
   - UI → Slider
   - Min Value: -10, Max Value: 10, Value: 0
   - Add Text label: "Speech Rate"

2. **Volume Slider:**
   - UI → Slider
   - Min Value: 0, Max Value: 100, Value: 100
   - Add Text label: "Volume"

### 4.5 Status Text:
1. UI → Text - TextMeshPro
2. Position: Bottom of UI
3. Text: "Ready to speak!"
4. Font Size: 16

## Bước 5: Connect References

### UIManager Setup:
1. **Add UIManager.cs** to Canvas
2. **Drag references:**
   - Text Input Field → textInputField
   - Speak Button → speakButton
   - Stop Button → stopButton
   - Rate Slider → rateSlider
   - Volume Slider → volumeSlider
   - Status Text → statusText
   - TTS Manager → ttsManager

## Bước 6: Test Basic TTS

1. **Play Scene**
2. **Enter text** in input field
3. **Click Speak** button
4. **Should hear** Windows TTS voice

## Bước 7: Prepare for Lip Sync (Phase 2)

1. **Select Doraemon model**
2. **Check SkinnedMeshRenderer** component
3. **Note BlendShapes** available (if any)
4. **If no blendshapes** → Need to create manually

## Troubleshooting

### Common Issues:

1. **"SpeechSynthesizer not found"**
   - Solution: Add `using System.Speech.Synthesis;`
   - Make sure running on Windows

2. **Audio not playing:**
   - Check AudioSource is attached
   - Check file path permissions
   - Check Windows audio settings

3. **UI not responding:**
   - Check EventSystem exists in scene
   - Check Button OnClick events connected

4. **Build errors:**
   - Player Settings → Configuration: Master
   - Scripting Backend: Mono (not IL2CPP)

## Next Phase Preview:

Phase 2 sẽ thêm:
- OVRLipSync components
- BlendShape mapping
- Real-time lip synchronization
- Character animation

---
📝 Save file này để reference sau!
