WPF Aero Glass Lib  

**Only Windows 10 and Windows 11(before 22H2) are supported**

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
    <SolidColorBrush Color="White" Opacity=".5"/>  
 </Window.Background>
```

## Additional
**You can set the window to be partially transparent, like below**  
```
<Grid>
    <Grid.ColumnDefinitions>
        <ColumnDefinition Width="*"/>
        <ColumnDefinition Width="3*"/>
    </Grid.ColumnDefinitions>

    <Grid  Grid.Column="0">
        <Grid.Background>
            <SolidColorBrush Color="MediumPurple" Opacity=".5"/>
        </Grid.Background>
    </Grid>

    <Grid Grid.Column="1" Background="White"/>
</Grid>
```

**You can also set the picture background directly**  
```
<Grid>
    <Grid.Background>
        <ImageBrush ImageSource="yasuo.jpg" Stretch="UniformToFill" Opacity=".5"/>
    </Grid.Background>
</Grid>
```

**Title foreground**
```
        TitleForeground="Blue"
```

**ControlBox Visibility**
```
        ControlBoxVisibility="Visibility.Collapsed"
```

**Icon Visibility**
```
        IconVisibility="Visibility.Collapsed"
```

**Title Visibility**
```
        TitleVisibility="Visibility.Collapsed"
```

**IsEnable ContextMenu**
```
        IsEnableContextMenu="true"
```

**ControlBox Button Visibility**
```
        MinimizeVisibility = Visibility.Collapsed
        MaximizeVisibility = Visibility.Collapsed
        CloseVisibility = Visibility.Collapsed
```

**ContentSpan** 
```
        ContentSpan="true"
```

## New feature
**IsBlurBackground** 
```
        IsBlurBackground="true"
```

## License

[MIT License](LICENSE).

