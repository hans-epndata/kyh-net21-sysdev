   M S B A M L   `   `   `  ????T  PDevice.WPF.TemperatureApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]3clr-namespace:Device.WPF.TemperatureApp.SetupWizard%Device.WPF.TemperatureApp.SetupWizard  5   	   7    1Device.WPF.TemperatureApp.SetupWizard.SetupWizard   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Elocal3clr-namespace:Device.WPF.TemperatureApp.SetupWizard     5   	     ? Title$  Setup Wizard??5   	   $	??500??6   $	??510??6+   .??5
      ? $??30 10 30 30q?6    ? RowDefinitions 5   
   ?? 5      $
??auto ?6   ?? 5      $
??auto ?6   ?? 5      $??* ?6   ?? 5      $
??auto ?6   .+?5   
   ?? $??0??6   $??Setup Wizard??6!   $??18(?65   $??0 0 0 20q?6C   $??Center=?6U   ? 5   
   $??1??6   $??0 0 0 30q?6    ? ColumnDefinitions 5      ?? 5      ?? 5      ?? 5      .+?5      ?? $??3??6   $	??280??6,   $??2??68   
???????$??	0 -20 0 0q?6T   ?? 5      $??0??6   .+?5       ? $??0 0 0 5q?6   .+?5!      A? -   5         A?Name$ ellipse_YourInformation??5!      
????ح?$??25??6P   $??25??6\   ?? 5"      $??1??6    $
??Bold"?6)   $??11(?6;   
???????$??Center=?6\   $??Center=?6y   5#      ?? 5$      -   5         ??Name$ tblock_YourInformation??5$      $??Your Information??6<   $??11(?6T   $
??Bold"?6b   $??Center=?6t   5%      ?? 5'      $??1??6   .+?5(      ? $??0 0 0 5q?6   .+?5)      A? -   5        $ ellipse_DeviceInformation??5)      
???????$??25??6R   $??25??6^   ?? 5*      $??2??6    $
??Bold"?6)   $??11(?6;   
???????$??Center=?6\   $??Center=?6y   5+      ?? 5,      -   5        $ tblock_DeviceInformation??5,      $??Device Information??6>   $??11(?6X   $??Center=?6f   5-      ?? 5/      $??2??6   .+?50      ? $??0 0 0 5q?6   .+?51      A? -   5        $ ellipse_Finish??51      
???????$??25??6G   $??25??6S   ?? 52      $??3??6    $
??Bold"?6)   $??11(?6;   
???????$??Center=?6\   $??Center=?6y   53      ?? 54      -   5        $ tblock_Finish??54      $??Connect/Finish??63   $??11(?6I   $??Center=?6W   55      56       ? 59   
   -   5          ?Name$ CurrentPage??59      $??2??6     ? NavigationUIVisibility$ Hidden=?62   ? 5<   
   $??3??6    5=      ?? 5>      ?? 5?      .+?5B      ?? $??0??6   $??10(?6(   
???????$
??Left=?6M   $??Center=?6h   ?? 5C      $?S/N: ??6    ?? 5D      -   5         ??Name$ tblock_DeviceId??5D      $*?$c47c22fd-e798-49d7-ba38-708b3c42888b??6/   5E      ?? 5G      $??1??6    ?? Orientation$ 
Horizontal=?6)   $??Right=?6B   $??Center=?6^   .+?5H      ?? -	   5        	 ??Name$	 btn_Back??5H      $
??15 5q?6B   $??0 0 10 0q?6Q   
?????? .??5I      ?? $ 
Horizontal=?6!   .+?5J      ?? $	????6$   $??9(?64   $??Segoe MDL2 Assets)?6A   $??Center=?6`   ?? 5K      $
??Back??6$   $??Center=?60   5L      5M      ?? 5O      -
   5        $	 btn_Next??5O      $
??15 5q?6B   
?????? .??5P      ?? $ 
Horizontal=?6!   .+?5Q      ?? -   5        $ tblock_Next??5Q   $   $
??Next??69   $??Center=?6E   ?? 5R      $	????6$   $??9(?64   $??Segoe MDL2 Assets)?6A   $??Center=?6`   5S      5T      5U      5W      5X      5Y      