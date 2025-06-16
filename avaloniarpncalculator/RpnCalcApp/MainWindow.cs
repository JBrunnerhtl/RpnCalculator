using System.Security.Cryptography;
using Avalonia.Controls;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Media.TextFormatting;
using Avalonia.Remote.Protocol.Input;
using Avalonia.Styling;
using Avalonia.Themes.Fluent;
using Logic;
using RpnCalc.Core;
using Key = Avalonia.Input.Key;

namespace avaloniarpncalculator;

public class MainWindow : Window
{
    
    private StackCalculator _stackCalculator = new StackCalculator();
    private TextBlock _inputValues;
    private Popup _errorPopup;
    private TextBlock _errorText;
    
    private Stack<TextBlock> _lineDisplay = new Stack<TextBlock>();
    public MainWindow()
    {
        Title = "RpnCalc.App";
        Width = 450;
        Height = 600;
        MinWidth = Width;
        MinHeight = Height;
        Background = new SolidColorBrush(Color.FromRgb(23, 22, 44));
        var stackPanel = new StackPanel();
        
        KeyDown += KeyPressed;
        stackPanel.Children.Add(FullBuild());
        Content = stackPanel;
    }


    private StackPanel FullBuild()
    {
        
        var stackPanel = new StackPanel();
        stackPanel.Children.Add(BuildStackPanelForDisplayLines());
        _errorPopup = new Popup()
        {
            Placement = PlacementMode.Bottom,
            IsOpen = false,
            Child = new Border()
            {
                CornerRadius = new CornerRadius(7),
                Child = _errorText =new TextBlock()
                {
                    Text = "",
                    Foreground = new SolidColorBrush(Colors.Red),
                    Background = new SolidColorBrush(Color.FromRgb(53, 51, 99)),
                    Padding = new Thickness(5),
                }
            }
        };
        stackPanel.Children.Add(_errorPopup);
        stackPanel.Children.Add(BuildGrid());
        
        stackPanel.Children.Add(BuildAdditionalFeature([false, false]));
        return stackPanel;
    }

    private Grid BuildGrid()
    {
        var buttons = new string[]
        {
            "7", "8", "9", "/",
            "4", "5", "6", "*",
            "1", "2", "3", "-",
            "0", ".", "Enter", "+",
            "Swap", "Clear","Remove",""
        };
        var grid = new Grid()
        {
            RowSpacing = 0.5,
            ColumnSpacing = 0.5,
            VerticalAlignment = VerticalAlignment.Stretch,
            HorizontalAlignment = HorizontalAlignment.Stretch,
        };
        for (int i = 0; i < 4; i++) // Columms
        {
            grid.ColumnDefinitions.Add(new ColumnDefinition(GridLength.Star));
        }

        for (int i = 0; i < 5; i++)
        {
            grid.RowDefinitions.Add(new RowDefinition(GridLength.Star));
        }
        int counter = 0;
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                
                var button = new Button()
                {
                    Content = buttons[counter],
                    Foreground = new SolidColorBrush(Colors.White),
                    Background = new SolidColorBrush(Color.FromRgb(4, 2, 48)),
                    Padding = new Thickness(7),
                    Margin = new Thickness(3),
                    CornerRadius = new CornerRadius(7),
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Stretch, 
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    Tag = buttons[counter],
                    Focusable = false,
                };
                
                
                if(buttons[counter] is "Swap" or "Clear" or "Enter" or "Remove")
                {
                    button.Background = new SolidColorBrush(Color.FromRgb(25, 23, 79));
                    button.Click += (sender, args) =>
                    {
                        OnOperationButtonClicked(sender, args);
                    };
                }
                else if (buttons[counter] is "/" or "*" or "-" or "+")
                {
                    button.Background = new SolidColorBrush(Color.FromRgb(38, 36, 68));
                    button.Click += OnActionButtonClicked;
                }
                else
                {
                    button.Click += ButtonClickedDigits; 
                }  
                
                
               
                if (button.Content.ToString() == String.Empty)
                {
                    continue;
                }
                
                Grid.SetRow(button, i);
                Grid.SetColumn(button, j);
                grid.Children.Add(button);
                counter++;
            }
        }
        
        return grid;
    }

    private StackPanel BuildStackPanelForDisplayLines()
    {
        var lines = new string[]
        {
            "",
            "Line 1",
            "Line 2",
            "Line 3",
            "Line 4",
            "Line 5"
        };
        var stackPanel = new StackPanel();
        stackPanel.Children.Add(new TextBlock
        {
            Text = "Display",
            FontSize = 16,
            Margin = new Thickness(5),
            Foreground = new SolidColorBrush(Colors.White),
            VerticalAlignment = VerticalAlignment.Center,
            TextAlignment = TextAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center,
            
        });
        for (int i = lines.Length -1; i >= 0; i--)
        {
            stackPanel.Children.Add(DefaultLineStyling(lines[i]));
        }
        return stackPanel;
    }


    private Border DefaultLineStyling(string text)
    {
        
        var border = new Border
        {
            
            Margin = new Thickness(3),
            Background = new SolidColorBrush(Color.FromRgb(53, 51, 99)),
            Padding = new Thickness(3),
            CornerRadius = new CornerRadius(7),
            BorderThickness = new Thickness(3),
            BorderBrush = new SolidColorBrush(Colors.Transparent),
            Child = new TextBlock()
            {
                Text = "",
                VerticalAlignment = VerticalAlignment.Center,
                TextAlignment = TextAlignment.Left,
                HorizontalAlignment = HorizontalAlignment.Left,

                Foreground = new SolidColorBrush(Colors.White),
            }
        };
        if(text == String.Empty)
        {
            border.BorderBrush = new SolidColorBrush(Color.FromRgb(82, 0, 198));
            _inputValues = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                TextAlignment = TextAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Text = String.Empty,
                Foreground = new SolidColorBrush(Colors.White),
            };
            border.Child = _inputValues;
        }
        
        _lineDisplay.Push((TextBlock)border.Child);
        
        return border;
    }

    private void ChangePopUpMessage(string message)
    {
        _errorText.Text = message;

    }

    private void UpdateLines()
    {
       
        var inputValues = _stackCalculator.GetStackSnapshot();
        bool fistline = true;
        int counter = 0;
        foreach (var element in _lineDisplay)
        {
            if (counter >= inputValues.Length || fistline)
            {
                
                if (fistline)
                {
                    element.Text = _inputValues.Text;
                    fistline = false;
                }
                else
                {
                    element.Text = "";
                }
            }
            else
            {
                element.Text = inputValues[counter].ToString();
                counter++;
            }
        }
    }

    private Grid BuildAdditionalFeature(bool[] buttonsChecked)
    {
        string[] inputValues = new string[]
        {
            "Graph",
        };
        double rows = inputValues.Length / 4;
        var grid = new Grid()
        {
            RowSpacing = 2,
            ColumnSpacing = 2,
           
        };
        if (inputValues.Length % 4 != 0)
        {
            rows = Math.Ceiling((double)inputValues.Length / 4) +1;
        }

        for (int i = 0; i < 4; i++)
        {
            grid.ColumnDefinitions.Add(new ColumnDefinition(GridLength.Star));
        }

        for (int i = 0; i < rows; i++)
        {
            grid.RowDefinitions.Add(new RowDefinition(GridLength.Star));
        }

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if(i*4+j >= inputValues.Length || i*4+j >= buttonsChecked.Length) continue;
                var button = new CheckBox()
                {
                    Content = inputValues[i * 4 + j],
                    Foreground = new SolidColorBrush(Colors.White),
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    Padding = new Thickness(5),
                    Height = 20,
                    CornerRadius = new CornerRadius(7),
                    Background = new SolidColorBrush(Color.FromRgb(4, 2, 48)),
                    BorderBrush = new SolidColorBrush(Colors.Aquamarine),
                    BorderThickness = new Thickness(1),
                    Tag = inputValues[i * 4 + j],
                    Margin = new Thickness(5),
                    IsChecked = buttonsChecked[i * 4 + j],
                    Focusable = false,
                };
                
                
                button.IsCheckedChanged += OnFeatureButtonClicked;
                
                Grid.SetRow(button, i);
                Grid.SetColumn(button, j);
                grid.Children.Add(button);
                
            }
        }
        return grid;
    }

    private void OnFeatureButtonClicked(object? sender, RoutedEventArgs e)
    {
        var checkBox = sender as CheckBox;
        try
        {
            
            if (_stackCalculator.GetStackSnapshot().Length == 0)
            {
                throw new RpnStackUnderflowException("Es sind nicht genügend Zahlen, um einen Graphen zu zeichen");
            }
       
            if (checkBox.IsChecked == true)
            {
                var tag = checkBox.Tag as string;
                if (tag == "Graph")
                {
                    Content = BuildGraphScreen();
                }
            }
            else if(checkBox.IsChecked == false)
            {
                _lineDisplay = new();
                Content = FullBuild();
                UpdateLines();
            }
            _errorPopup.IsOpen = false;
        }
        catch (Exception exception)
        {
            checkBox.IsChecked = false;
            Console.WriteLine(exception.Message);
            ChangePopUpMessage(exception.Message);
            _errorPopup.IsOpen = true;
        }
        
    }

    private StackPanel BuildGraphScreen()
    {
        var stackPanel = new StackPanel();
        stackPanel.Children.Add(new DrawingWindow().DrawGraph(_stackCalculator.GetStackSnapshot(), Width, Height));
        stackPanel.Children.Add(BuildAdditionalFeature([true, false]));
        return stackPanel;
    }

    private bool CheckInputField()
    {
        bool valid = !string.IsNullOrEmpty(_inputValues.Text);

        return valid;
    }
    private void KeyPressed(object sender, KeyEventArgs e)
    { 
        var key = e.Key;
        switch (key)
        {
            case Key.Enter:
                OnOperationButtonClicked(new Button(){Tag = "Enter"}, null);
                break;
            case Key.Escape:
                this.Close();
                break;
            case Key.Add:
                OnActionButtonClicked(new Button(){Tag = "+"}, null);
                break;
            case Key.Subtract:
                OnActionButtonClicked(new Button(){Tag = "-"}, null);
                break;
            case Key.Multiply:
                OnActionButtonClicked(new Button(){Tag = "*"}, null);
                break;
            case Key.Divide:
                OnActionButtonClicked(new Button(){Tag = "/"}, null);
                break;
            case Key.Back:
                OnOperationButtonClicked(new Button(){Tag = "Remove"}, null);
                break;
            case Key.OemPeriod:
                ButtonClickedDigits(new Button(){Tag = "."}, null);
                break;
            case Key.S:
                OnOperationButtonClicked(new Button(){Tag = "Swap"}, null);
                break;
            case Key.C:
                OnOperationButtonClicked(new Button(){Tag = "Clear"}, null);
                break;
        }

        if (key >= Key.D0 && key <= Key.D9)
        {
            ButtonClickedDigits(new Button(){Tag = e.KeySymbol}, null);
        }
        
    }

    private void OnActionButtonClicked(object sender, RoutedEventArgs eventArgs)
    {
        var button = sender as Button;
        
        try
        {
            switch (button.Tag.ToString())
            {
                case "/":
                    _stackCalculator.Divide();
                                    
                    break;
                case "*":
                    _stackCalculator.Multiply();
                    break;
                case "-":
                    _stackCalculator.Subtract();
                    break;
                case "+":
                    _stackCalculator.Add();
                    break;
            }

            _errorPopup.IsOpen = false;
            UpdateLines();
        }
        catch (Exception e)
        {
            _inputValues.Text = "";
            Console.WriteLine(e.Message);
            ChangePopUpMessage(e.Message);
            _errorPopup.IsOpen = true;
                            
        }
    }

    private void OnOperationButtonClicked(object sender, EventArgs eventArgs)
    {
        var button = sender as Button;
         try
         {


             if (button.Tag.ToString() == "Enter")
             {
                 if (!CheckInputField())
                 {
                     _inputValues.Text = String.Empty;
                     throw new AddingEmptyException("Es kann keine leere Zahl hinzugefügt werden.");
                 }

                 _stackCalculator.Push(double.Parse(_inputValues.Text.Replace(".", ",")));
                 _inputValues.Text = String.Empty;
                                

             }
             else if (button.Tag.ToString() == "Clear")
             {
                 _inputValues.Text = String.Empty;
                 _stackCalculator.Clear();
                 
             }
             else if (button.Tag.ToString() == "Swap")
             {
                 _stackCalculator.Swap();
             }
             else
             {

                 if (_inputValues.Text == String.Empty)
                 {
                     throw new Exception("Es kann nichts entfernt werden,\n da das Eingabefeld leer ist.");
                 }
                 string temp = _inputValues.Text;
                 _inputValues.Text = String.Empty;
                 for (int k = 0; k < temp.Length-1; k++)
                 {
                     _inputValues.Text += temp[k];
                 }

             }
             _errorPopup.IsOpen = false;
             UpdateLines();
         }
         catch (Exception e)
         {
             _inputValues.Text = "";
             Console.WriteLine(e.Message);
             ChangePopUpMessage(e.Message);
             _errorPopup.IsOpen = true;
                            
         }
    }

    private void ButtonClickedDigits(object? sender, RoutedEventArgs e)
    {
        _inputValues.Text += ((Button)sender).Tag.ToString();
    }
}
