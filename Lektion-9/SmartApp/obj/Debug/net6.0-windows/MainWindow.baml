   M S B A M L   `   `   `  ????C  ?SmartApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:SmartAppSmartApp  5   	   C&clr-namespace:SmartApp.MVVM.ViewModelsSmartApp.MVVM.ViewModels  5   	   9!clr-namespace:SmartApp.ComponentsSmartApp.Components  6C       SmartApp.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:SmartApp     5   	   =
viewmodels&clr-namespace:SmartApp.MVVM.ViewModels     5   	   8
components!clr-namespace:SmartApp.Components     6C     ? Title$  SmartApp??5	   	    ? WindowStartupLocation$ CenterScreen=?5
   	    ? WindowStyle$
 None=?5   	    ? AllowsTransparency$
 True??5   	   
?????? $	??720??5   	   $
??1280??5   	    ? DataContext 5      ,  &SmartApp.MVVM.ViewModels.MainViewModel  5   
   .??5      ?? ???#333333 ?? CornerRadius$ 12??6"   ._?5   
   ?  ? RowDefinitions 5      ?? 5      $??* ?6    ?? 5      $
??auto ?6    .+?5      ?? $??0??6   ??6*   ?? *CurrentView+'   !SmartApp.Components.MenuComponent  5      $??1??6'   $??
0 -140 0 0q?64   5      5!      5"      