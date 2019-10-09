# DimWin Brightness #

This application gives you a handy brightness control in your Windows system tray which enables you to control your laptop screen brightness. It also adds a contrast overlay which can be used to control a "sudo" brightness on both Windows laptops and desktop computers.

This project exists due to a laptop I used to own which received a display driver update. The display driver update removed the brightness control which used to be displayed under the battery icon menu in the system tray and I had to keep going into display settings to adjust my brightness. I looked at other available solutions and they were either too complex or looked plain ugly. I wanted to keep DimWin Brightness simple while fitting in with a modern (updated) Windows design. 

## Sudo Brightness ##

I used to use a laptop which even though I had turned the screen brightness all the way down, still seemed to bright. Sudo brightness creates a screen overlay which can be used to darken what is being displayed onscreen and this gives the impression of additional brightness control. This feature may come in handy where you laptop screen is too bright on the lowest setting. Basically it's fake brightness control.

Sudo Brightness uses `System.Windows.Forms.NativeWindow` and some nifty low-level native Windows APIs to create a screen overlay which completely hides from Windows tasks and does not intercept mouse clicks. The `BasicWindow` (`NativeWindow`) class in this project is imported from my Gadg8 open-source project available on BitBucket.

Although Sudo Brightness will work with desktop computers, physical external monitor brightness can not be adjusted using DimWin. It only works on laptop displays. 

