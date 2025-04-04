﻿#pragma checksum "..\..\..\DisplayTopMoviesWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9E633813416BDF77655DD40CA21A489CB0D2072B"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ten kod został wygenerowany przez narzędzie.
//     Wersja wykonawcza:4.0.30319.42000
//
//     Zmiany w tym pliku mogą spowodować nieprawidłowe zachowanie i zostaną utracone, jeśli
//     kod zostanie ponownie wygenerowany.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace WpfApp1 {
    
    
    /// <summary>
    /// DisplayTopMoviesWindow
    /// </summary>
    public partial class DisplayTopMoviesWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 84 "..\..\..\DisplayTopMoviesWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox GenreListBox;
        
        #line default
        #line hidden
        
        
        #line 89 "..\..\..\DisplayTopMoviesWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox YearFromTextBox;
        
        #line default
        #line hidden
        
        
        #line 94 "..\..\..\DisplayTopMoviesWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox YearToTextBox;
        
        #line default
        #line hidden
        
        
        #line 99 "..\..\..\DisplayTopMoviesWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox RatingFromTextBox;
        
        #line default
        #line hidden
        
        
        #line 104 "..\..\..\DisplayTopMoviesWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox RatingToTextBox;
        
        #line default
        #line hidden
        
        
        #line 112 "..\..\..\DisplayTopMoviesWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.WrapPanel MoviesWrapPanel;
        
        #line default
        #line hidden
        
        
        #line 115 "..\..\..\DisplayTopMoviesWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button PreviousButton;
        
        #line default
        #line hidden
        
        
        #line 116 "..\..\..\DisplayTopMoviesWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button NextButton;
        
        #line default
        #line hidden
        
        
        #line 117 "..\..\..\DisplayTopMoviesWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BackButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/WpfApp1;V1.0.0.0;component/displaytopmovieswindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\DisplayTopMoviesWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.GenreListBox = ((System.Windows.Controls.ListBox)(target));
            return;
            case 2:
            this.YearFromTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.YearToTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.RatingFromTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.RatingToTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            
            #line 107 "..\..\..\DisplayTopMoviesWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ApplyFiltersButton_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 108 "..\..\..\DisplayTopMoviesWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ClearFiltersButton_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.MoviesWrapPanel = ((System.Windows.Controls.WrapPanel)(target));
            return;
            case 9:
            this.PreviousButton = ((System.Windows.Controls.Button)(target));
            
            #line 115 "..\..\..\DisplayTopMoviesWindow.xaml"
            this.PreviousButton.Click += new System.Windows.RoutedEventHandler(this.PreviousButton_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.NextButton = ((System.Windows.Controls.Button)(target));
            
            #line 116 "..\..\..\DisplayTopMoviesWindow.xaml"
            this.NextButton.Click += new System.Windows.RoutedEventHandler(this.NextButton_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.BackButton = ((System.Windows.Controls.Button)(target));
            
            #line 125 "..\..\..\DisplayTopMoviesWindow.xaml"
            this.BackButton.Click += new System.Windows.RoutedEventHandler(this.BackButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

