   M S B A M L   `   `   `  ????L  HDevice.IntelliFan, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Device.IntelliFanDevice.IntelliFan  5   	   "    Device.IntelliFan.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:Device.IntelliFan     5   	     ? Title$  
IntelliFAN??5   	   $	??340??6   $	??300??6)    ? 	Resources 5
         iconRotateBladeStoryBoard g? RepeatBehavior ?? 
TargetName ?? TargetProperty W? From	 W? To g? 
SpeedRatio??%?   &	        ?? 5   
   .??5      ?? $ Forever??6   .??5      W? $ iconRotateBlade??6"   $ Angle?6J    06l    3606u   $	 0.5Q?6~   5      5      5      .??5      ? $??20q?6   .+?5   
   ?? $??Center=?6   $??Center=?62   .+?5      ? .+?5      ??   FontAwesome-Regular#??[ 6   $	????6N   
?????p?$	??110(?6u   $??Center=?6?   A? 5      $??70??6   $??70??6&   
???????$??	1 -10 0 0q?6>   ?? 5       J? RenderTransformOrigin$ 0.5 0.5&?6     FontAwesome-Solid#??[ 6<   $	????6l   
?????p?$??65(?6?   $??Center=?6?   $??Center=?6?   $??	0 -13 0 0q?6?   z?5      ?? 5      -   5        iconRotateBlade??	 ?? Angle$	 0Q?5   C   5      5      ?? 5      $??
IntelliFAN??6   $??22(?6*   $??SemiBold"?68   
?????p?$??Center=?6e   $??0 10 0 5q?6?   ?? 5      -   5        
 ??Name$
 tblockConnectionState??5      $??Not Connected??67   $??11(?6L   $??SemiBold"?6Z   
?????p?$??Center=?6?   $??0 0 0 10q?6?   ?? 5       -   5         ??Name$ 	btnAction??5         btn#??[ 6(   $??Start??6]   5!      5"      5#      