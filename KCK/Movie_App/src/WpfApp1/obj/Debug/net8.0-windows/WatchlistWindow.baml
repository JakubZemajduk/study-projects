   M S B A M L   `   `   `  ����  WpfApp1    WpfApp1.WatchlistWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  	Watchlist��5   	   $	��400��6   $	��600��6(   ���#f0f0f0 � 
ResizeMode$ NoResize=�6I    � WindowStartupLocation$ CenterScreen=�6_   .��5      � $��20q�6   .+�5   
   �� $��	Watchlist��6   $��Center=�5      $	��Top=�5      $��24(�5	      $
��Bold"�5
      $��	0,10,0,20q�5      ���#3335      &   System.Windows.Controls.DataGrid  5   
   -   5        $��WatchMoviesDataGrid��5      $��Stretch=�5      $	��Top=�5      $��
0,50,0,100q�5         AutoGenerateColumns$ False��5         
IsReadOnly$
 True��5         	RowHeight$ 30Q�5      $��14(�5         Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Title��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6N   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 64   �� *Title+6N     5      $ Genre��6%   $ * 6N   	 64   �� *Genre+6N     5      $
 Year��6%   $	 100 6X   	 63   �� *YearOfProduction+6X     5      $ Rating��6%   $	 100 6P   	 65   �� *Rating+6P   5      �� 5   
   
 �� Orientation$
 
Horizontal=�6   $��Center=�6.   $��Bottom=�6K   .+�5      �� -   5        $��RemoveButton��5      $��Remove from Watchlist��5       $	��150��5!      $��40��5"      ���#dc3545
�������$��0q�5%       � Cursor$
 Hand��5&      $��5,0,5,0q�5'      5(      �� 5)      -   5        $��
BackButton��5)      $
��Back��5*      $	��100��5+      $��40��5,      ���#0078d7
�������$��0q�5/      $
 Hand��50      $��5,0,5,0q�51      52      53      54      55      