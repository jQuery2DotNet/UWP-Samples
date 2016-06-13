# Message Content Dialog in UWP app
```
Windows.UI.Popups.MessageDialog showDialog = new Windows.UI.Popups.MessageDialog("UWP message dialog without title - Windows 10");
showDialog.Commands.Add(new UICommand("Yes") {
 Id = 0
});
showDialog.Commands.Add(new UICommand("No") {
 Id = 1
});
showDialog.DefaultCommandIndex = 0;
showDialog.CancelCommandIndex = 1;
var result = await showDialog.ShowAsync();
if ((int) result.Id == 0) {
 //do your task
} else {
 //skip your task
}
```


### YouTube Video

[![Message Content Dialog in UWP app](http://img.youtube.com/vi/fpeAaOC0oLw/0.jpg)](https://youtu.be/fpeAaOC0oLw "Message Content Dialog  in UWP app")
