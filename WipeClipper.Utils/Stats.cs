using System;
using System.Collections.Generic;
using System.Linq;
using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;

namespace WipeClipperUtils {
    public class Stats {
        private static readonly Dictionary<string, int> _teaMechTimes = new Dictionary<string, int> {
            {"Dolls End", 80},
            {"P2", 165},
            {"Gavel", 300},
            {"P3", 380},
            {"Inception", 410},
            {"Wormhole", 505}
        };

        private static Preset _preset;

        public static void CreatePlots(List<int> pulls, List<double> breaks, List<int> timeBetweenPulls, Preset preset, bool includeTimePlot) {
            _preset = preset;
            CreateSimplePlot(pulls, breaks);
            if (includeTimePlot) {
                CreateTimePlot(pulls, timeBetweenPulls);
            }
        }

        public static Statistics GetStats(List<int> pulls, List<int> timeBetweenPulls) {
            var orderedPulls = pulls.OrderBy(x => x).ToList();
            int medianPull;
            int totalTime = pulls.Sum() + timeBetweenPulls.Sum();

            if (orderedPulls.Count % 2 == 0) {
                medianPull = (orderedPulls[pulls.Count / 2] + orderedPulls[(pulls.Count / 2 - 1)]) / 2;
            } else {
                medianPull = orderedPulls.ElementAt(pulls.Count / 2);
            }

            var longestPull = pulls.Max();
            double percentageSpentOnPulls = (double)pulls.Sum() / totalTime;
            int timeSpentPulling = pulls.Sum();
            var timeOnPullsPastThreshold = pulls.Where(x => x > _preset.settings.GreenThreshold).Sum();

            return new Statistics(pulls.Count, medianPull, longestPull, percentageSpentOnPulls, timeSpentPulling, timeOnPullsPastThreshold);
        }

        public class Statistics {
            public int PullCount { get; }
            public int MedianPull { get; }
            public int LongestPull { get; }
            public double PercentageSpentOnPulls { get; }
            public int TimeSpentPulling { get; }
            public int TimeSpentPullingPastThreshold { get; }

            public Statistics(int count, int medianPull, int longestPull, double percentageSpentOnPulls, int timeSpentPulling, int timeSpentPastThreshold) {
                PullCount = count;
                MedianPull = medianPull;
                LongestPull = longestPull;
                PercentageSpentOnPulls = percentageSpentOnPulls * 100;
                TimeSpentPulling = timeSpentPulling;
                TimeSpentPullingPastThreshold = timeSpentPastThreshold;
            }
        }

        private static void CreateSimplePlot(List<int> pulls, List<double> breaks) {
            var model = new PlotModel { Title = $"Summary for {DateTime.Today.ToShortDateString()}" };

            var series = new LineSeries {
                MarkerType = MarkerType.Circle,
                MarkerFill = OxyColors.Blue,
                LineStyle = LineStyle.None,
                YAxisKey = "yAxis",
                XAxisKey = "xAxis"
            };

            var xAxis = new LinearAxis {
                Key = "xAxis",
                Position = AxisPosition.Bottom,
                Title = "Pull #",
                AxisTitleDistance = 10,
                Minimum = 0
            };

            var yAxis = new LinearAxis {
                Key = "yAxis",
                Position = AxisPosition.Left,
                Title = "Pull time in seconds",
                AxisTitleDistance = 20,
                MaximumPadding = 0.05,
                Minimum = 0
            };

            model.Axes.Add(xAxis);
            model.Axes.Add(yAxis);

            for (int i = 0; i < pulls.Count; i++) {
                series.Points.Add(new DataPoint(i + 1, pulls[i]));
            }

            foreach (var breakTime in breaks) {
                var line = new LineAnnotation {
                    StrokeThickness = 1,
                    Color = OxyColors.Red,
                    Type = LineAnnotationType.Vertical,
                    X = breakTime,
                };

                model.Annotations.Add(line);
            }

            if (_preset.settings.AddTeaMarkers) {
                foreach (var mech in _teaMechTimes) {
                    var line = new LineAnnotation {
                        StrokeThickness = 1,
                        Color = OxyColors.Red,
                        Type = LineAnnotationType.Horizontal,
                        Text = mech.Key,
                        TextColor = OxyColors.Black,
                        Y = mech.Value,
                        TextLinePosition = 0,
                        TextHorizontalAlignment = HorizontalAlignment.Left
                    };
                    model.Annotations.Add(line);
                }
            }

            var thresholdLine = new LineAnnotation {
                StrokeThickness = 1,
                Color = OxyColors.Green,
                LineStyle = LineStyle.Solid,
                Type = LineAnnotationType.Horizontal,
                Text = "Threshold",
                TextColor = OxyColors.Black,
                Y = _preset.settings.GreenThreshold,
                TextHorizontalAlignment = HorizontalAlignment.Right
            };
            model.Annotations.Add(thresholdLine);

            model.Series.Add(series);
            model.Padding = new OxyThickness(10, 10, 30, 10); // padding around the plot

            var pngExporter = new PngExporter { Width = 800, Height = 600, Background = OxyColors.White };
            pngExporter.ExportToFile(model, "plot.png");
        }

        private static void CreateTimePlot(List<int> pulls, List<int> timeBetweenPulls) {
            var model = new PlotModel { Title = $"Summary for {DateTime.Today.ToShortDateString()}" };

            var timeSeries = new LineSeries {
                MarkerType = MarkerType.Circle,
                MarkerFill = OxyColors.Blue,
                LineStyle = LineStyle.None,
                YAxisKey = "yAxis",
                XAxisKey = "xAxis"
            };

            var timeBreakSeries = new AreaSeries {
                YAxisKey = "yAxis",
                XAxisKey = "xAxis",
                Color = OxyColors.Red
            };

            var xAxis = new TimeSpanAxis {
                Key = "xAxis",
                Position = AxisPosition.Bottom,
                Title = "Time",
                AxisTitleDistance = 10,
                Minimum = TimeSpanAxis.ToDouble(new TimeSpan(0)),
                MinimumPadding = 0.05
            };

            var yAxis = new LinearAxis {
                Key = "yAxis",
                Position = AxisPosition.Left,
                Title = "Pull time in seconds",
                AxisTitleDistance = 20,
                MaximumPadding = 0.05,
                Minimum = 0
            };

            model.Axes.Add(xAxis);
            model.Axes.Add(yAxis);

            var time = 0;
            for (int i = 0; i < pulls.Count; i++) {
                time += pulls[i];
                timeSeries.Points.Add(new DataPoint(TimeSpanAxis.ToDouble(new TimeSpan(0, 0, time)), pulls[i]));
                var shadow = new LineAnnotation {
                    StrokeThickness = 1,
                    Color = OxyColors.Blue,
                    Type = LineAnnotationType.Vertical,
                    X = time,
                    MinimumY = 0,
                    MaximumY = pulls[i]
                };
                model.Annotations.Add(shadow);

                if (i != pulls.Count - 1) {
                    timeBreakSeries.Points.Add(new DataPoint(TimeSpanAxis.ToDouble(new TimeSpan(0, 0, time - 1)), 0));
                    timeBreakSeries.Points.Add(new DataPoint(TimeSpanAxis.ToDouble(new TimeSpan(0, 0, time)), 50));
                    timeBreakSeries.Points.Add(new DataPoint(TimeSpanAxis.ToDouble(new TimeSpan(0, 0, time + timeBetweenPulls[i] - 1)), 50));
                    timeBreakSeries.Points.Add(new DataPoint(TimeSpanAxis.ToDouble(new TimeSpan(0, 0, time + timeBetweenPulls[i])), 0));
                    time += timeBetweenPulls[i];
                }
            }

            if (_preset.settings.AddTeaMarkers) {
                foreach (var mech in _teaMechTimes) {
                    var line = new LineAnnotation {
                        StrokeThickness = 1,
                        Color = OxyColors.Red,
                        Type = LineAnnotationType.Horizontal,
                        Text = mech.Key,
                        TextColor = OxyColors.Black,
                        Y = mech.Value,
                        TextLinePosition = 0,
                        TextHorizontalAlignment = HorizontalAlignment.Left
                    };
                    model.Annotations.Add(line);
                }
            }

            var thresholdLine = new LineAnnotation {
                StrokeThickness = 1,
                Color = OxyColors.Green,
                LineStyle = LineStyle.Solid,
                Type = LineAnnotationType.Horizontal,
                Text = "Threshold",
                TextColor = OxyColors.Black,
                Y = _preset.settings.GreenThreshold,
                TextHorizontalAlignment = HorizontalAlignment.Right
            };
            model.Annotations.Add(thresholdLine);

            model.Series.Add(timeBreakSeries);
            model.Series.Add(timeSeries);
            model.Padding = new OxyThickness(10, 10, 30, 10); // padding around the plot

            var pngExporter = new PngExporter { Width = 1280, Height = 720, Background = OxyColors.White };
            pngExporter.ExportToFile(model, "timePlot.png");
        }
    }
}
