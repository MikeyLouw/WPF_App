   M S B A M L   `   `   `  ����F  BTangentTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:TangentTestTangentTest  5   	   A%clr-namespace:TangentTest.DataSourcesTangentTest.DataSources  5   	       TangentTest.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:TangentTest     5   	   =DataSources%clr-namespace:TangentTest.DataSources     5   	   -   5          � Title$  
MainWindow��5	   	   $��668.813��6   $��1123.904��6-    � 	Resources 5
      +   %TangentTest.DataSources.ProjectsDBSet   projectsDBSet  projectViewSource �� Source �� Source  tasksViewSource��%�   &	        &	      7   &	 @     7     5   
   6$   �� 5   
    69   �� *	Project+8   �� 5   
    67   �� *Tasks+8   5      .��5      � $��Center=�6    � DataContext# [ 6(    � RowDefinitions 5   
   �� 5      $
��203* �6   �� 5      $	��97* �6   �� 5      $	��18* �6   �� 5      $	��50* �6   �� 5      $
��109* �6   �� 5      $	��49* �6   �� 5      $
��112* �6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ projectDataGrid��5         AutoGenerateColumns .  $��Center=�6H      EnableRowVirtualization . $��-542,50,-487,0q�6�   	   RowDetailsVisibilityMode$	 VisibleWhenSelected=�6�   $��2��6�   $��Center=�6�   
   HeadersVisibility$
 Column=�6  $
��1024��63  $	��250��6C  $	��250��6S  $
��1024��6c  4  .System.Windows.Automation.AutomationProperties   IsColumnHeader .    SelectionMode$ Single=�6�  $
��1029��6�  ��6�   ��    Columns 5      4  .System.Windows.Controls.DataGridTemplateColumn  5      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ 0.10* 6)      CellTemplate 5      �� 5      .R�5      �� $��Select��6'   -   5        $��Center=�5   i   6�   5      5!      0  *System.Windows.Controls.DataGridTextColumn  5"      -   5        titleColumn��   Header$ Title��5"   T   $ 0.15* 6c   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6:   �� *Title+6c     5#      -   5        descriptionColumn��$ Description��5#   `   $ 0.15* 6u    6@   �� *Description+6u     5$      -   5        start_DateColumn��$ 
Start Date��5$   C   $ 0.15* 6W    5%      �� 5&      .R�5'      (	  "System.Windows.Controls.DatePicker	   	  SelectedDate 6)   �� *
Start_Date+ �� Mode$ TwoWay=� �� NotifyOnValidationError .  �� ValidatesOnExceptions . 5(      5*        5+      -   5        end_DateColumn��$ End Date��5+   Z   $ 0.15* 6l    6=   �� *
End_Date+6l   4
  .System.Windows.Controls.DataGridCheckBoxColumn
  5,      -   5        is_BillableColumn��$ Is Billable��5,   d   $ 0.15* 6y    6D   �� *Is_Billable+6y   
  5-      -	   5        is_ActiveColumn��$ 	Is Active��5-   `   $ 0.15* 6s    6B   �� *	Is_Active+6s   5/        50   
   -
   5        $ tasksDataGrid��50       .   . $��-542,32,-487,15q�6}   $��3��6�   $��4��6�   $	 VisibleWhenSelected=�6�   $
��1024��6�   $	��250��6�   $
��1019��6  $	��250��6   . $ Single=�6N  ��6e   ��  51        52      $ 0.10* 6)    53      �� 54      .R�55      �� $��Select��6'   -   5        $��Center=�55   o   6�   56      58        59      -   5        titleColumn1��$ Title��59   U   $ 0.30* 6d    6;   �� *Title+6d     5:      -   5        due_DateColumn��$ Due Date��5:   A   $ 0.30* 6S    5;      �� 5<      .R�5=      	   6)   �� *
Due_Date+$ TwoWay=� .  . 5>      5@        5A      -   5        estimated_HoursColumn��$ Estimated Hours��5A   h   $ 0.30* 6�    6D   �� *Estimated_Hours+6�   5C      �� 5D   
   -   5        $��Refresh Data��5D      $
��Left=�6(   $��
-542,9,0,0q�6C   $	��Top=�6W   $	��126��6o   $��36��6{   6�   �� 5E   
   -   5        $��Add Project��5E      $
��Left=�6'   $��
-411,9,0,0q�6B   $	��Top=�6V   $	��126��6n   $��36��6z   6�   �� 5F   
   -   5        $��Delete��5F      $
��Left=�6"   $��
-280,9,0,0q�6=   $	��Top=�6Q   $	��126��6i   $��36��6u   6�   �� 5G   
   -   5        $��Update Changes��5G      $
��Left=�6*   $��
-149,9,0,0q�6E   $	��Top=�6Y   $	��126��6q   $��36��6}   6�   �� 5H   
   $��Select Project first!��6   $
��Left=�60   $��
-542,1,0,0q�6K   $��3��6_   $	��Top=�6l    J� RenderTransformOrigin$ 0.252,0.462&�6�   $	��126��6�   $��36��6�   �� 5I   
   -   5        $��Add Task��5I      $
��Left=�6$   $��
-411,1,0,0q�6?   $	��Top=�6S   $	��126��6k   $��36��6w   $��3��6�   6�   �� 5J   
   -   5        $��Delete Task��5J      $
��Left=�6'   $��
-280,1,0,0q�6B   $	��Top=�6V   $	��126��6n   $��36��6z   $��3��6�   6�   �� 5K   
   -   5        $��Update Task��5K      $
��Left=�6'   $��
-149,1,0,0q�6B   $	��Top=�6V   $	��126��6n   $��36��6z   $��3��6�   6�   5L      5M      