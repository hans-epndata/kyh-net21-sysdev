   M S B A M L   `   `   `  ????C  ?SmartApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:SmartApp.MVVM.ViewsSmartApp.MVVM.Views  5      9!clr-namespace:SmartApp.ComponentsSmartApp.Components  6>   %    SmartApp.MVVM.Views.KitchenView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:SmartApp.MVVM.Views     5      8
components!clr-namespace:SmartApp.Components     6>   .??5
      ?? -   5          ??Name$  kitchen??5
         
pageBorder#??[  6   ._?5   
   ??  ?? CornerRadius$ 10??6   ??5      ?? 5       j? Stretch$
 Fill=?6    ?? Opacity$	 0.6Q?6,    ?? ImageSource$! /Assets/Images/kitchen.jpeg??6:   ._?5      ?  ? RowDefinitions 5      ?? 5      $
??auto ?6$   ?? 5      $??* ?6$   .+?5      ?? -   5         ??Name$ btnClose??5   &   $??0??6     closeButton#??[ 6O   ? 5      $??1??6   $??50 10q?6$    ? ColumnDefinitions 5      ?? 5      $
??auto ?6+   ?? 5      $??* ?6+   .+?5      ?? $??0??6!   $??Center=?61   .+?5      ?? $??Kitchen and Dining??6$   ???#ffffff$??36(?6S   $
??Bold"?6a   ?? 5!       ?? Orientation$ 
Horizontal=?6%   .+?5"      ?? ???#ffffff$??20(?6=   ?? 5#   "   $??32(?6C   ?6&   ?? *Temperature+6C    ?? 5$   "   ?6&   ?? *TemperatureScale+5%      ?? 5&      $??40 0 0 0q?6(   ???#ffffff$??20(?6O   ?? 5'   "   $??32(?6@   ?6&   ?? *
Humidity+6@    ?? 5(   "   ?6&   ?? *HumidityScale+5)      5*      5+      ? 5-      $??1??6   $??40 0 0 0q?6+   $??Right=?6=   $??Center=?6Y    5.      ?? 5/      $
??auto ?6/   ?? 50      $
??auto ?6/    52      ?? 53      $
??auto ?6,   ?? 54      $
??auto ?6,   .+?57         SmartApp.Components.Tile  $??0??6*   $??0??67     58      $??0??6*   $??1??67     59      $??1??6*   $??0??67     5:      $??1??6*   $??1??67   5=      5>      5?      5A      5B      5C      