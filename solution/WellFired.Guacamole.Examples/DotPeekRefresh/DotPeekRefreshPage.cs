using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.DotPeekRefresh
{
    public static class DotPeekRefreshPage
    {
        public static Page Create()
        {
            var buildTime = new Label
            {
                Text = "Build Time : 10/06/2017 - 17:03", 
                Padding = new UIPadding(30, 10, 10, 10),
                HorizontalTextAlign = UITextAlign.Start,
                BackgroundColor = UIColor.FromRGB(40, 40, 40),
                OutlineColor = UIColor.FromRGB(40, 40, 40),
            };
            
            var gitCommitId = new Label
            {
                Text = "Commit ID : 67ea1f1",
                Padding = new UIPadding(30, 10, 10, 10),
                HorizontalTextAlign = UITextAlign.Start,
                BackgroundColor = UIColor.FromRGB(40, 40, 40),
                OutlineColor = UIColor.FromRGB(40, 40, 40),
            };
            
            var platform = new Label
            {
                Text = "Platform : Standalon MacOS",
                Padding = new UIPadding(30, 10, 10, 10),
                HorizontalTextAlign = UITextAlign.Start,
                BackgroundColor = UIColor.FromRGB(40, 40, 40),
                OutlineColor = UIColor.FromRGB(40, 40, 40),
            };
            
            var buildAssetSplit = new Label
            {
                Text = "Build Asset Split", 
                Padding = new UIPadding(30, 10, 10, 10),
                HorizontalTextAlign = UITextAlign.Middle,
                BackgroundColor = UIColor.FromRGB(40, 40, 40),
                OutlineColor = UIColor.FromRGB(40, 40, 40),
            };
            
            var textureBreakdown = new Label
            {
                Text = "Texture 100MB 0.3%", 
                Padding = new UIPadding(30, 10, 10, 10),
                HorizontalTextAlign = UITextAlign.Start,
                BackgroundColor = UIColor.FromRGB(40, 40, 40),
                OutlineColor = UIColor.FromRGB(40, 40, 40),
            };
            
            var animationBreakdown = new Label
            {
                Text = "Animation 100MB 0.3%", 
                Padding = new UIPadding(30, 10, 10, 10),
                HorizontalTextAlign = UITextAlign.Start,
                BackgroundColor = UIColor.FromRGB(40, 40, 40),
                OutlineColor = UIColor.FromRGB(40, 40, 40),
            };

            var unityVersion = new LayoutView
            {
                BackgroundColor = UIColor.FromRGB(40, 40, 40),
                OutlineColor = UIColor.FromRGB(40, 40, 40),
                Layout = new AdjacentLayout {Orientation = OrientationOptions.Horizontal},
                HorizontalLayout = LayoutOptions.Fill,
                VerticalLayout = LayoutOptions.Fill,
                Children =
                {
                    new Label
                    {
                        VerticalTextAlign = UITextAlign.Middle,
                        BackgroundColor = UIColor.FromRGB(40, 40, 40),
                        OutlineColor = UIColor.FromRGB(40, 40, 40),
                        Text = "Unity Version :",
                        Padding = UIPadding.Of(10)
                    },
                    new Label
                    {
                        VerticalTextAlign = UITextAlign.Middle,
                        Text = "Unity 5.5.1f1",
                        Padding = UIPadding.Of(10),
                        BackgroundColor = UIColor.DarkGreen,
                        OutlineColor = UIColor.DarkGreen
                    }
                }
            };

            var buildSize = new LayoutView
            {
                BackgroundColor = UIColor.FromRGB(40, 40, 40),
                OutlineColor = UIColor.FromRGB(40, 40, 40),
                Layout = new AdjacentLayout {Orientation = OrientationOptions.Horizontal},
                HorizontalLayout = LayoutOptions.Fill,
                VerticalLayout = LayoutOptions.Fill,
                Children =
                {
                    new Label
                    {
                        BackgroundColor = UIColor.FromRGB(40, 40, 40),
                        OutlineColor = UIColor.FromRGB(40, 40, 40),
                        Text = "Build size :",
                        Padding = UIPadding.Of(10)
                    },
                    new Label
                    {
                        VerticalTextAlign = UITextAlign.Middle,
                        Text = "199MB",
                        Padding = UIPadding.Of(15),
                        BackgroundColor = UIColor.DarkGreen,
                        OutlineColor = UIColor.DarkGreen
                    }
                }
            };

            return new Page
            {
                BackgroundColor = UIColor.FromRGB(40, 40, 40),
                OutlineColor = UIColor.FromRGB(40, 40, 40),
                Content = new LayoutView
                {
                    BackgroundColor = UIColor.FromRGB(40, 40, 40),
                    OutlineColor = UIColor.FromRGB(40, 40, 40),
                    Layout = new AdjacentLayout
                    {
                        Orientation = OrientationOptions.Vertical,
                        Spacing = 10
                    },
                    HorizontalLayout = LayoutOptions.Fill,
                    VerticalLayout = LayoutOptions.Expand,
                    Children =
                    {
                        new LayoutView
                        {
                            BackgroundColor = UIColor.FromRGB(40, 40, 40),
                            OutlineColor = UIColor.FromRGB(40, 40, 40),
                            Layout = new AdjacentLayout {Orientation = OrientationOptions.Horizontal},
                            HorizontalLayout = LayoutOptions.Fill,
                            VerticalLayout = LayoutOptions.Expand,
                            Children =
                            {
                                buildTime,
                                gitCommitId
                            }
                        },
                        platform,
                        new LayoutView
                        {
                            BackgroundColor = UIColor.FromRGB(40, 40, 40),
                            OutlineColor = UIColor.FromRGB(40, 40, 40),
                            Padding = new UIPadding(0, 0, 50, 0),
                            Layout = new AdjacentLayout
                            {
                                Orientation = OrientationOptions.Vertical,
                                Spacing = 10
                            },
                            HorizontalLayout = LayoutOptions.Fill,
                            VerticalLayout = LayoutOptions.Expand,
                            Children =
                            {
                                unityVersion,
                                buildSize
                            }
                        },
                        new LayoutView
                        {
                            BackgroundColor = UIColor.FromRGB(40, 40, 40),
                            OutlineColor = UIColor.FromRGB(40, 40, 40),
                            Padding = new UIPadding(0, 30, 0, 0),
                            Layout = new AdjacentLayout
                            {
                                Orientation = OrientationOptions.Vertical,
                                Spacing = 10
                            },
                            HorizontalLayout = LayoutOptions.Fill,
                            VerticalLayout = LayoutOptions.Expand,
                            Children =
                            {
                                buildAssetSplit,
                                textureBreakdown,
                                animationBreakdown
                            }
                        }
                    }
                }
            };
        }
    }
}