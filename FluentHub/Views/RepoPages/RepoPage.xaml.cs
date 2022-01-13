using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FluentHub.Views.RepoPages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RepoPage : Page
    {
        public RepoPage()
        {
            this.InitializeComponent();

            muxc.TreeViewNode rootNode = new muxc.TreeViewNode() { Content = "src" };
            rootNode.IsExpanded = true;
            rootNode.Children.Add(new muxc.TreeViewNode() { Content = "List" });
            rootNode.Children.Add(new muxc.TreeViewNode() { Content = "List" });
            rootNode.Children.Add(new muxc.TreeViewNode() { Content = "List" });
            rootNode.Children.Add(new muxc.TreeViewNode() { Content = "List" });
            rootNode.Children.Add(new muxc.TreeViewNode() { Content = "List" });
            rootNode.Children.Add(new muxc.TreeViewNode() { Content = "List" });
            rootNode.Children.Add(new muxc.TreeViewNode() { Content = "List" });
            rootNode.Children.Add(new muxc.TreeViewNode() { Content = "List" });
            rootNode.Children.Add(new muxc.TreeViewNode() { Content = "List" });
            rootNode.Children.Add(new muxc.TreeViewNode() { Content = "List" });
            rootNode.Children.Add(new muxc.TreeViewNode() { Content = "List" });
            rootNode.Children.Add(new muxc.TreeViewNode() { Content = "List" });
            rootNode.Children.Add(new muxc.TreeViewNode() { Content = "List" });

            sampleTreeView.RootNodes.Add(rootNode);
        }
    }
}
