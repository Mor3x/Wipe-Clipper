using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDotNet;
using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;

namespace DiscordAndTwitch {
    public class Stats {

        public static void SavePlot(IEnumerable<int> pulls, IEnumerable<double> breaks) {
            var model = new PlotModel {Title = $"Summary for {DateTime.Today.ToShortDateString()}"};
            var series = new LineSeries {
                MarkerType = MarkerType.Circle,
                MarkerFill = OxyColors.Blue,
                LineStyle = LineStyle.None,
                YAxisKey = "yAxis",
                XAxisKey = "xAxis"
            };

            var pullsList = pulls.ToList();
            for (int i = 0; i < pullsList.Count; i++) {
                series.Points.Add(new DataPoint(i + 1, pullsList[i]));
            }

            var xAxis = new LinearAxis {
                Key = "xAxis",
                Position = AxisPosition.Bottom,
                Title="Pull #",
                AxisTitleDistance = 10,
                Minimum = 0
            };
            
            var yAxis = new LinearAxis {
                Key = "yAxis",
                Position = AxisPosition.Left,
                Title="Pull time in seconds",
                AxisTitleDistance = 20,
                MaximumPadding = 0.05,
                Minimum = 0
            };

            model.Axes.Add(xAxis);
            model.Axes.Add(yAxis);

            foreach (var breakTime in breaks) {
                var line = new LineAnnotation {
                    StrokeThickness = 1,
                    Color = OxyColors.Red,
                    Type = LineAnnotationType.Vertical,
                    X = breakTime,
                };

                model.Annotations.Add(line);
            }

            model.Series.Add(series);
            model.Padding = new OxyThickness(10, 10, 30, 10); // padding around the plot

            var pngExporter = new PngExporter { Width = 600, Height = 400, Background = OxyColors.White };
            pngExporter.ExportToFile(model, "plot.png");
        }

        public static (int amount, int median, int longest) GetStats(IEnumerable<int> pulls) {
            var orderedPulls = pulls.OrderBy(x => x).ToList();
            var pullsCount = pulls.Count();
            int medianPull;

            if (orderedPulls.Count % 2 == 0) {
                medianPull = (orderedPulls[pullsCount / 2] + orderedPulls[(pullsCount / 2 - 1)]) / 2;
            } else {
                medianPull = orderedPulls.ElementAt(pullsCount / 2);
            }
            var longestPull = pulls.Max();

            Console.WriteLine("Median: " + medianPull);
            Console.WriteLine("Longest pull: " + longestPull);
            return (pullsCount, medianPull, longestPull);
        }
    }
}
