using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Layout;
using Avalonia.Media;
using Logic;
using RpnCalc.Core;

namespace avaloniarpncalculator;

public class DrawingWindow : Control
{


    public Canvas DrawGraph(double[] stack, double width, double height)
    {
        width = width - 10;
        height = height - 100;
        var canvas = DrawCoordinatesystem(width, height);
        canvas.Margin = new Thickness(0, 20, 0, 20);
        double spaceBetweenMarksForX = width / 20;
        double spaceBetweenMarksForY = height / 20;
        double startValue = 0;
        double valueForFunction = -10;
        bool isAtZero = false;
        int countForGrapDrawing = 0;
        double extraAddingForHorizontalLine = 0;
        for (double i = 10; i <= 29.9; i+=0.01)
        {
            double startY = FunctionCalculator.Calculate(stack, valueForFunction);
            if (stack.Length == 1)
            {
                extraAddingForHorizontalLine = 0.01;
            }
            double endY = FunctionCalculator.Calculate(stack, valueForFunction + 0.01) + extraAddingForHorizontalLine;
            Line line = new Line()
            {
                StartPoint = new Point(startValue * spaceBetweenMarksForX, height /2 - startY * spaceBetweenMarksForY),
                EndPoint = new Point(startValue * spaceBetweenMarksForX,height /2 - endY * spaceBetweenMarksForY),
                Stroke = new SolidColorBrush(Colors.Yellow),
                StrokeThickness = 2,
            };
            valueForFunction += 0.01;

            startValue += 0.01;
            if (CheckPointValidity(line.StartPoint, width, height) && CheckPointValidity(line.EndPoint, width, height))
            {
                canvas.Children.Add(line);
                countForGrapDrawing++;
            }
        }

        if (countForGrapDrawing == 0) throw new GraphOutOfRangeException("Der Graph ist zu groß um ihn zu zeichen");
        return canvas;
    }

    private bool CheckPointValidity(Point p, double width, double height)
    {
        if (p.X > 0 && p.X < width && p.Y >
            0 && p.Y < height)
        {
            return true;
        }
        return false;
    }

    private Canvas DrawCoordinatesystem(double width, double height)
    {
        Canvas canvas = new Canvas()
        {
            Background = new SolidColorBrush(Colors.Transparent),
            HorizontalAlignment = HorizontalAlignment.Stretch,
            VerticalAlignment = VerticalAlignment.Stretch,
        };
        canvas.Width = width;
        canvas.Height = height;

        Line x = new Line()
        {
            StartPoint = new Point(0, height / 2),
            EndPoint = new Point(width, height / 2),
            Stroke = new SolidColorBrush(Colors.White)
        };
        Line y = new Line()
        {
            StartPoint = new Point(width / 2, 0),
            EndPoint = new Point(width / 2, height),
            Stroke = new SolidColorBrush(Colors.White)
        };
        double spaceBetweenMarksForX = width / 20;
        double spaceBetweenMarksForY = height / 20;
        double valueX = 0;
        double valueY = 0;
        for (int i = 0; i < 21; i++)
        {
            Line markX = new Line()
            {
                StartPoint = new Point(valueX, height / 2 - 2),
                EndPoint = new Point(valueX, height / 2 + 2),
                Stroke = new SolidColorBrush(Colors.White)
            };
            Line markY = new Line()
            {
                StartPoint = new Point(width / 2 -2, valueY),
                EndPoint = new Point(width / 2 + 2, valueY),
                Stroke = new SolidColorBrush(Colors.White)
            };
            valueY += spaceBetweenMarksForY;
            valueX += spaceBetweenMarksForX;
            canvas.Children.Add(markY);
            canvas.Children.Add(markX);
        }
        canvas.Children.Add(x);
        canvas.Children.Add(y);
        
        return canvas;
    }
    
}