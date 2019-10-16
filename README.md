# BlurWindow
WFP Aero Glass

> The window style is located in Theme.xaml and can be modified by yourself.  
> This project is part of [TianXiaTech](https://github.com/TianXiaTech)

## Nuget
`
PM>Install-Package BlurWindow -Version 1.0.0.1
`  

## Usage
### Step 1
import xaml prefix  
`
xmlns:blurwindow=&quot;clr-namespace:TianXiaTech;assembly=BlurWindow&quot;
`

### Step2
Replace &lt;Window&gt;&lt;/Window&gt; with &lt;blurwindow:BlurWindow&gt;&lt;/blurwindow:BlurWindow&gt;

### Step3  
Make MainWindow inherit from TianXiaTech.BlurWindow  
`
public partial class MainWindow : TianXiaTech.BlurWindow
`

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


