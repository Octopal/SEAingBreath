App for SEAingBreath project

# Documentation

## FBX visibility and animation

### Control Visibility and Animation of the fbx
**FbxControl** controls the visibility all fbx elements (ex. shell, drain knob, tube) and runs fbx animation clip. 

Call `SetScene(string sceneName)` from anywhere. It hides others objects and show objects from `objects` array. `sceneName` defined in `scenes` array in `FbxControl` script. 

Call `Play (string clipName)` to run fbx clip. To see available animation `clipName` go to fbx prefab Animations tab. Make sure that the Motion is assigned in state in Animation tab.

### How to morph fbx animation
**PlayheadAnimator** morph any fbx animation from 0 to 1.

Call `setEnabled(bool value)` to enable morph.

Call `SetValue(float value)` to change the value, it should be from 0 to 1, otherwise it will be clamped from 0 to 1. `ClipName` is name defined in Animations tab inside fbx prefab import settings.

### Bubbles
GParticleSystem creates particles

Call `CreateParticles()` or press button in editor

## OSC
### How to create new osc
Assign `OSC.cs`,`UDPPacketIO.cs` and `OSCGeneralManager` to object

# Issues

If custom shader is black in editor but not in build change **Edit > Graphic Emulation > No emulation** by default it set to `OpenGL ES 3.0`

If there is an errors with compiling Amplify shaders just change **Build Settings > Player Settings > Other Settings > Auto Graphic API > turn off** put OpenGLES3 to top.

