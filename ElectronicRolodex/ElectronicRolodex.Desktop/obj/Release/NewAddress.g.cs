﻿#pragma checksum "..\..\NewAddress.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "14D6C4D836C50E5EBAEDEAA61FD3685A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18033
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
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


namespace ElectronicRolodex.Desktop {
    
    
    /// <summary>
    /// NewAddress
    /// </summary>
    public partial class NewAddress : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\NewAddress.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox HouseNumber;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\NewAddress.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox StreetName;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\NewAddress.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox AptOfficeRoom;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\NewAddress.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox City;
        
        #line default
        #line hidden
        
        
        #line 84 "..\..\NewAddress.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox State;
        
        #line default
        #line hidden
        
        
        #line 101 "..\..\NewAddress.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox Country;
        
        #line default
        #line hidden
        
        
        #line 118 "..\..\NewAddress.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ZipCode;
        
        #line default
        #line hidden
        
        
        #line 128 "..\..\NewAddress.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label ErrorLabel;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ElectronicRolodex.Desktop;component/newaddress.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\NewAddress.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.HouseNumber = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.StreetName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.AptOfficeRoom = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.City = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.State = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.Country = ((System.Windows.Controls.ComboBox)(target));
            
            #line 108 "..\..\NewAddress.xaml"
            this.Country.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.UpdateStates);
            
            #line default
            #line hidden
            return;
            case 7:
            this.ZipCode = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.ErrorLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 9:
            
            #line 142 "..\..\NewAddress.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddNewAddressClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
