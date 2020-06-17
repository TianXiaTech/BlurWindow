# BlurWindow
WPF Aero Glass Lib

</p>
<p align="center">
<a href="https://github.com/TianXiaTech/BlurWindow/stargazers" target="_blank">
 <img alt="GitHub stars" src="https://img.shields.io/github/stars/TianXiaTech/BlurWindow.svg" />
</a>
<a href="https://github.com/TianXiaTech/BlurWindow/releases" target="_blank">
 <img alt="All releases" src="https://img.shields.io/github/downloads/TianXiaTech/BlurWindow/total.svg" />
</a>
<a href="https://github.com/TianXiaTech/BlurWindow/network/members" target="_blank">
 <img alt="Github forks" src="https://img.shields.io/github/forks/TianXiaTech/BlurWindow.svg" />
</a>
<a href="https://github.com/TianXiaTech/BlurWindow/issues" target="_blank">
 <img alt="All issues" src="https://img.shields.io/github/issues/TianXiaTech/BlurWindow.svg" />
</a>
</p>

> The window style is located in Theme.xaml and can be modified by yourself.  
> This project is part of [TianXiaTech](https://github.com/TianXiaTech)

## Nuget
`
PM>Install-Package BlurWindow -Version 1.0.0.1
`  

## Usage
### Step 1
import xaml prefix  
```
xmlns:blurwindow="clr-namespace:TianXiaTech;assembly=BlurWindow";
```

### Step2
Replace **&lt;Window&gt;&lt;/Window&gt;** with **&lt;blurwindow:BlurWindow&gt;&lt;/blurwindow:BlurWindow&gt;**

### Step3  
Make MainWindow inherit from TianXiaTech.BlurWindow  
```
public partial class MainWindow : TianXiaTech.BlurWindow
```

### Step4  
Specify background transparency  

```
<Window.Background>
    <!--Specify transparency here-->
    <SolidColorBrush Color="White" Opacity=".5"/>  
 </Window.Background>
```

## Sample screenshot

### Opacity 0.8
<p align="center">
 <img align="center" alt="demo" src="ScreenShots/1.png" />
</p>

### Opacity 0.5
<p align="center">
 <img align="center" alt="demo" src="ScreenShots/2.png" />
</p>

### Actual use effect
<p align="center">
 <img align="center" alt="start up" src="ScreenShots/3.png" />
</p>

## Thanks
Project reference https://github.com/TranslucentTB/TranslucentTB

## License

[MIT License](LICENSE).


