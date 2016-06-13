# Full Screen Mode in UWP app
```csharp
ApplicationView view = ApplicationView.GetForCurrentView();

bool isInFullScreenMode = view.IsFullScreenMode;

if (isInFullScreenMode) {
 view.ExitFullScreenMode();
 btnMode.Content = "View Full Screen Mode";
} else {
 view.TryEnterFullScreenMode();
 btnMode.Content = "Exit Full Screen Mode";
}
```
### YouTube Video

[![Full Screen Mode in UWP app](http://img.youtube.com/vi/4RR3QncR4OI/0.jpg)](https://youtu.be/4RR3QncR4OI "Full Screen Mode in UWP app")

