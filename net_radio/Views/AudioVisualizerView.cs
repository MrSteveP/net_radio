using Microsoft.Maui.Controls;
using System.Timers;

namespace net_radio.Views
{
    public class AudioVisualizerView : ContentView
    {
        private readonly BoxView[] _bars;
        private readonly Random _random = new();
        private readonly System.Timers.Timer _timer;

        public AudioVisualizerView() : this(16)
        {
        }

        public AudioVisualizerView(int barCount)
        {
            var layout = new Grid { ColumnSpacing = 2, VerticalOptions = LayoutOptions.End, HeightRequest = 50 };
            _bars = new BoxView[barCount];

            for (int i = 0; i < barCount; i++)
            {
                layout.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                var bar = new BoxView
                {
                    Color = Colors.DeepSkyBlue,
                    VerticalOptions = LayoutOptions.End,
                    HeightRequest = 10
                };
                layout.Children.Add(bar);
                Grid.SetColumn(bar, i);
                Grid.SetRow(bar, 0);
                _bars[i] = bar;
            }

            Content = layout;

            _timer = new System.Timers.Timer(60);
            _timer.Elapsed += (s, e) => AnimateBars();
        }

        public void Start() => _timer.Start();
        public void Stop() => _timer.Stop();

        private void AnimateBars()
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                foreach (var bar in _bars)
                {
                    bar.HeightRequest = _random.Next(5, 50);
                }
            });
        }
    }
}