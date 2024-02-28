using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using OxyPlot;
using OxyPlot.Series;

namespace ProceduralWorldGenerator.Helpers
{
    public class PlotModelJsonConverter : JsonConverter<PlotModel>
    {
        public override void WriteJson(JsonWriter writer, PlotModel? value, JsonSerializer serializer)
        {
            writer.WriteValue("ssss");
        }

        public override PlotModel? ReadJson(JsonReader reader, Type objectType, PlotModel? existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            var tmp = new PlotModel { Title = "Simple example", Subtitle = "using OxyPlot" };

            // Create two line series (markers are hidden by default)
            var series1 = new LineSeries { Title = "Series 1", MarkerType = MarkerType.Circle };
            series1.Points.Add(new DataPoint(0, 0));
            series1.Points.Add(new DataPoint(10, 18));
            series1.Points.Add(new DataPoint(20, 12));
            series1.Points.Add(new DataPoint(30, 8));
            series1.Points.Add(new DataPoint(40, 15));

            var series2 = new LineSeries { Title = "Series 2", MarkerType = MarkerType.Square };
            series2.Points.Add(new DataPoint(0, 4));
            series2.Points.Add(new DataPoint(10, 12));
            series2.Points.Add(new DataPoint(20, 16));
            series2.Points.Add(new DataPoint(30, 25));
            series2.Points.Add(new DataPoint(40, 5));


            // Add the series to the plot model
            tmp.Series.Add(series1);
            tmp.Series.Add(series2);

            // Axes are created automatically if they are not defined

            // Set the Model property, the INotifyPropertyChanged event will make the WPF Plot control update its content
            return tmp;
        }
    }
    public static class ObjectHelper
    {
        private static readonly JsonSerializerSettings DeepClone = new()
        {
            TypeNameHandling = TypeNameHandling.All,
            ReferenceLoopHandling = ReferenceLoopHandling.Error,
            Converters = new List<JsonConverter>()
            {
                new PlotModelJsonConverter()
            }
        };
        public static T DeepCopy<T>(T obj)
        {
            if (Equals(obj, default))
                return default;
            var copy = JsonConvert.SerializeObject((object)obj, DeepClone);
            return JsonConvert.DeserializeObject<T>(copy, DeepClone);
        }
    }
}