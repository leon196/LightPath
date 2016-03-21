# LightPath

![LightPath](http://i.giphy.com/xThuWehtO5eYlVKN7a.gif)

This is the Unity3D project, with all the sources including :
- Scripts/Renders/Webcam.cs : to get webcam in Unity3D
- Scripts/Renders/FrameBuffer.cs : triple frame buffer script, used to read & write a render texture and to store the previous frame
- Scripts/Elements/ChromaDetector.cs : naive CPU color detection helped by GPU little shader trick

# About

The project was inspired by light painting. The intention was to capture a visual marker, held by an artist like a painter or a dancer, and interpret it by visuals and sound generation.  
For the moment, the project can detect 4 colors, tracking the center and the density of a color mass.  
For the sound generation, since or knowledge is limited, we just synchronise the density of the color to the volume of a sound.  
It was made during the [Culture Experience Days](https://www.weezevent.com/ced-weekend-creatif-2016) : Creative Weekend by [ADAMI](https://www.adami.fr/)  

# Team

- Code : Leon
- Music : Raphaelle
- 3D print, welding and arduino scripts : Julien and Alice

# 3D printed electronical maracas

Photo by Julien Dorra :  
![LightPath](https://raw.githubusercontent.com/leon196/LightPath/master/Img/photo1-byJulienDorra.PNG)
![LightPath](https://raw.githubusercontent.com/leon196/LightPath/master/Img/photo2-byJulienDorra.PNG)