   M S B A M L   `   `   `  ����R  NSharpGL.WPF, Version=2.3.0.1, Culture=neutral, PublicKeyToken=27fc851303210b27>.clr-namespace:SharpGL.WPF;assembly=SharpGL.WPFSharpGL.WPF  5   	    AssimpSample   AssimpSample.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   BsharpGL.clr-namespace:SharpGL.WPF;assembly=SharpGL.WPF     5   	   -   5        $��window��5   B     � Title$  SharpGL WPF Application��6	   $	��350��6)   $	��525��66   .��5      � $��
0,10,5,1.8q�6   .+�5
   
      SharpGL.WPF.OpenGLControl  -   5        $��openGLControl��5         DrawFPS .    RenderContextType$	 FBO=�5         	FrameRate$ 10Q�64   $��0.8,0,0.2,0q�6C   $��1��6X   �� 5   
   -   5        $��rotacijalabel��5   L   $��25��6   $
��Left=�6   $��Ubuntu)�67   $	��Top=�6a   $	��110��6y   
�������$��199,248,0,0q�6�   .��6�   SPEED:6�   �� 5   
   -   5        $��skaliranjelabel��5   L   $��25��6   $
��Left=�6   $��Ubuntu)�67   $	��Top=�6c   $��68��6{   
�������$��
12,248,0,0q�6�   .��6�   SCALE:6�   �� 5   
   -   5        $��tbVisina��5   :   $��25��6   $
��Left=�6   $	��Top=�6J   $��47��6b   
���   �$��417,216,0,0q�6�   �� 5   
   -   5        $��VisinaSkakanja_btn��5   �   $��Change height��6   $��25��6)   $��Ubuntu)�65   
����i��$
��Left=�6^   $��383,256,0,0q�6y   $	��Top=�6�   $	��110��6�   6�   �� 5   
   
����i��$��Ubuntu)�6%   $��183,0,178.4,273q�69   .��6R   CLICK V TO THROW THE BALL6m   �� 5   
   -   5        $��labelaTackasti��5      $��40��6   $��Ubuntu)�62   $
��Left=�6F   $��	10,84,0,0q�6a   
���   �$	��Top=�6�   $	��112��6�    J� RenderTransformOrigin$ 0.015,1.123&�6�   .��6�   	TACKASTI:6�   �� 5   
   -   5        $��tackasti��5       d� 	IsChecked$
 TrueY�6?   |�.   � Cursor$
 Hand��6b   $��20��6p   $��	93,94,0,0q�6|   $	��Top=�6�   $
��Left=�6�   $��17��6�   $ -4.824,1.28&�6�   �� 5   
   $��REFLEKTORSKI:��6   $��	1,119,0,0q�6(   $��Ubuntu)�6;   $	��Top=�6O   $
��Left=�6g   $��88��6�   
���   �$��24��6�   �� 5    
   -	   5        $��reflektorski��5       $��
94,129,0,0q�6'   $	��Top=�6;   $
 TrueY�6s   $
 Hand��6�   |�.  $
��Left=�6�   $��16��6�   $ -1.75,0.342&�6�   5!      5"      