# Button Flyout Menu in UWP
```
<Button HorizontalAlignment="Center" VerticalAlignment="Center" Content="Button Flyout">
   <Button.Flyout>
      <MenuFlyout>
         <MenuFlyoutItem Text="Item 1" />
         <MenuFlyoutItem Text="Item 2" />
         <MenuFlyoutSeparator />
         <MenuFlyoutSubItem Text="Item 3">
            <MenuFlyoutItem Text="Item 4" />
            <MenuFlyoutSubItem Text="Item 5">
               <MenuFlyoutItem Text="Item 6" />
               <MenuFlyoutItem Text="Item 7" />
            </MenuFlyoutSubItem>
         </MenuFlyoutSubItem>
         <MenuFlyoutSeparator />
         <ToggleMenuFlyoutItem Text="Toggle Menu Item 1" />
      </MenuFlyout>
   </Button.Flyout>
</Button>
```
### YouTube Video

[![Button Flyout Menu in UWP](http://img.youtube.com/vi/rcrFPJa_5bk/0.jpg)](https://youtu.be/rcrFPJa_5bk "Button Flyout Menu in UWP")


