   M S B A M L   `   `   `  ����  WpfApp1     WpfApp1.SearchMoviesWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Search Movies��5   	   $	��500��6   $	��900��6,   ���#F5F5F5 � WindowStartupLocation$ CenterScreen=�6M   .��5      � $��20q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5	      $
��Auto �6   .+�5   
   �� $��0��6    �� Orientation$ 
Horizontal=�6"   $��Center=�6;   $��0,0,0,15q�6V   .+�5      �� -   5         ��Name$ SearchTextBox��5      $	��600��5      $��30��5      $��0,0,10,0q�5      $
��10,5q�5      $��Center=�5      $��14(�5      .��5   >   ��5      �� 5       �� 
TargetType" ��6    �� Triggers 5      P� 5       P� Property � �� P� Value$  ��66   . �5   "   �� 	 �� Property	 � ��
 �� Value
 5   &   !� 5   *    j� Stretch$
 None=�66    !� Visual 5   .   �� 5   2   $��Search movies...��6<   
�������$��Center=�5   <   $��14(�5    <   $��15,0,0,0q�5!   <   $��Italic$�5"   <   5$   +   5&   #   5'      5)      .��5*   !   5+      �� 5,      -   5         ��Name$ SearchButton��5,      $��Search��5-      $	��100��5.      $��30��5/      ���#4CAF50
�������$
��Bold"�52      53      54      &   System.Windows.Controls.DataGrid  56   
   -   5          Name$ MoviesDataGrid��56      $��1��57         AutoGenerateColumns$ False��58         SelectionMode$ Single=�59      
�������   AlternatingRowBackground �#F9F9F9   GridLinesVisibility$ 
Horizontal=�5<         VerticalGridLinesBrush �#DDDDDD   HorizontalGridLinesBrush �#DDDDDD���#CCCCCC$��1q�5@         Columns 5A      0  *System.Windows.Controls.DataGridTextColumn  5B      ,  &System.Windows.Controls.DataGridColumn   Header$ Title��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ 3* 6N   1  +System.Windows.Controls.DataGridBoundColumn   Binding 64   �� *Title+6N     5C      $ Genre��6%   $ 2* 6N    64   �� *Genre+6N     5D      $
 Year��6%   $ 1* 6X    63   �� *YearOfProduction+6X     5E      $ Rating��6%   $ 1* 6P    65   �� *Rating+6P   5G      �� 5I   
   $��2��6   $ 
Horizontal=�6"   $��Right=�6;   $��0,20,0,0q�6W   .+�5J      �� -   5        $ AddToLikedButton��5J      $��Add to Liked��5K      $	��150��5L      $��40��5M      ���#2196F3
�������$��14(�5P      $
��Bold"�5Q      $��0,0,10,0q�5R      5S      �� 5T      -   5        $ AddToWatchlistButton��5T      $��Add to Watchlist��5U      $	��150��5V      $��40��5W      ���#FF9800
�������$��14(�5Z      $
��Bold"�5[      5\      �� 5]      -   5        $��
BackButton��5]      $
��Back��6'   ���	#FFd9534f
�������$	��150��5`      $��40��5a      $
��Bold"�5b      $��	150,0,0,0q�5c      5d      5e      5f      5g      