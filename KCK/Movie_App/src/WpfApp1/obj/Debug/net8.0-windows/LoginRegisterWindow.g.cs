﻿#pragma checksum "..\..\..\LoginRegisterWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "C675E3C49BD33805590F430B0A93FB7B3E0467B9"
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
    /// LoginRegisterWindow
    /// </summary>
    public partial class LoginRegisterWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\..\LoginRegisterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox UsernameTextBox;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\LoginRegisterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox PasswordBox;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\LoginRegisterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button LoginButton;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\LoginRegisterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button RegisterButton;
        
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
            System.Uri resourceLocater = new System.Uri("/WpfApp1;component/loginregisterwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\LoginRegisterWindow.xaml"
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
            this.UsernameTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 22 "..\..\..\LoginRegisterWindow.xaml"
            this.UsernameTextBox.GotFocus += new System.Windows.RoutedEventHandler(this.UsernameTextBox_GotFocus);
            
            #line default
            #line hidden
            
            #line 23 "..\..\..\LoginRegisterWindow.xaml"
            this.UsernameTextBox.LostFocus += new System.Windows.RoutedEventHandler(this.UsernameTextBox_LostFocus);
            
            #line default
            #line hidden
            return;
            case 2:
            this.PasswordBox = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 36 "..\..\..\LoginRegisterWindow.xaml"
            this.PasswordBox.GotFocus += new System.Windows.RoutedEventHandler(this.PasswordBox_GotFocus);
            
            #line default
            #line hidden
            
            #line 37 "..\..\..\LoginRegisterWindow.xaml"
            this.PasswordBox.LostFocus += new System.Windows.RoutedEventHandler(this.PasswordBox_LostFocus);
            
            #line default
            #line hidden
            return;
            case 3:
            this.LoginButton = ((System.Windows.Controls.Button)(target));
            
            #line 54 "..\..\..\LoginRegisterWindow.xaml"
            this.LoginButton.Click += new System.Windows.RoutedEventHandler(this.LoginButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.RegisterButton = ((System.Windows.Controls.Button)(target));
            
            #line 69 "..\..\..\LoginRegisterWindow.xaml"
            this.RegisterButton.Click += new System.Windows.RoutedEventHandler(this.RegisterButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

