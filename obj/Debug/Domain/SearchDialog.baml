   M S B A M L   `   `   `  ����C  ?TilleWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:TilleWPF.DomainTilleWPF.Domain  5      "    TilleWPF.Domain.SearchDialog   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      V RMaterialDesignThemes.Wpf, Version=2.5.0.1205, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5      :d2http://schemas.microsoft.com/expression/blend/2008  5      )localclr-namespace:TilleWPF.Domain  5      .��5      � $��6q�6     � RowDefinitions  5	   
   �� 5
      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $	��150 �6   �� 5      $	��150 �6   .+�5   
   �� $��2��6   $��18(�6(   $
��Bold"�66   $��5q�6H   	Búsqueda6]   �� 5   
   $��0��6   $��1��6&   $��6q�63   .+�5      ��  �� Orientation$ 
Horizontal=�6   .+�5      ��  �� Padding$ 5q�6   Mes6-   d� 5      -   5        5   n   5      �� 5      -   5        $��monthCombobox��5      {�.  5      5      �� 5   
   $��1��6   $��1��6&   $��6q�63   .+�5      �� $ 
Horizontal=�6   .+�5      �� $ 5q�6   Año6-   d� 5      -   5        5   l   5      �� 5       -   5        $��yearComboBox��5       {�.  5!      5"      �� 5#   
   -   5        $��AcceptButton��5%   2      MaterialDesignFlatButton#��[  5#       �� 	IsDefault . $��0��5$   &   $��2��66   $��0q�5%      {�.  $2��,materialDesign:DialogHost.CloseDialogCommand��5&      $��Buscar��5'      6&   �� 5(   
   -   5        $��CancelButton��5*       #��[  5(       �� IsCancel . $��1��5)   %   $��2��65   $��8q�5*      $2��,materialDesign:DialogHost.CloseDialogCommand��5+      $��Cancelar��5,      6(   5-      5.      