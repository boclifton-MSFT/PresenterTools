using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using System.Timers;

namespace PresenterTools.Pages.Components
{
    public partial class BreakTimer
    {
        private string BreakText = "Take a Break";
        private bool IsEditingBreakText = false;
        private ElementReference breakTextInput { get; set; }

        private string TimerText = "Time Remaining";
        private bool IsEditingTimerText = false;
        private ElementReference timerTextInput { get; set; }

        private DateTime CountdownEndTime;
        private int BreakLength;
        private TimeSpan TimeRemaining;
        private string OutputTime;

        private Timer timer;

        private void EditBreakText()
        {
            IsEditingBreakText = true;
            StateHasChanged();
        }

        private void EditTimerText()
        {
            IsEditingTimerText = true;
            StateHasChanged();
        }

        private void StartCountdownTimer()
        {
            CountdownEndTime = DateTime.Now.AddMinutes(BreakLength);
            TimeRemaining = TimeSpan.FromMinutes(BreakLength);
            OutputTime = TimeRemaining.ToString(@"hh\:mm\:ss");

            timer = new Timer(250);
            timer.Elapsed += OnTick;
            timer.Enabled = true;
            timer.Start();
        }

        private void StopCountdownTimer()
        {
            timer.Enabled = false;
            timer?.Stop();
            TimeRemaining = TimeSpan.Zero;
        }

        private void OnTick(Object source, ElapsedEventArgs e)
        {
            TimeRemaining = CountdownEndTime.Subtract(e.SignalTime);
            if (TimeRemaining < TimeSpan.Zero)
            {
                timer.Stop();
            }
            OutputTime = TimeRemaining.ToString(@"hh\:mm\:ss");
            StateHasChanged();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (IsEditingBreakText == true)
                await breakTextInput.FocusAsync();

            if (IsEditingTimerText == true)
                await timerTextInput.FocusAsync();
        }

        public void Dispose()
        {
            timer?.Stop();
        }
    }
}
