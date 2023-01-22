using System;
using System.IO;
using System.Threading.Tasks;
using System.Timers;
using AppKit;
using AVFoundation;
using CoreGraphics;
using Foundation;
using ImageIO;
using Xamarin.Essentials;

namespace Pomodoro
{
    public partial class ViewController : NSViewController
    {

        System.Timers.Timer MainTimer;
        int TimeLeft = 1500;

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();



            // Do any additional setup after loading the view.

            // Fire the timer once a second
            MainTimer = new Timer(1000);
            MainTimer.Elapsed += (sender, e) =>
            {
                TimeLeft--;
                // Format the remaining time nicely for the label
                TimeSpan time = TimeSpan.FromSeconds(TimeLeft);
                string timeString = time.ToString(@"mm\:ss");
                InvokeOnMainThread(() =>
                {
                    //We want to interact with the UI from a different thread,
                    // so we must invoke this change on the main thread
                    TimerLabel.StringValue = timeString;
                    this.View.Window.Title = timeString + " (Pomodoro Timer)";
                });

                // If 25 minutes have passed
                if (TimeLeft == 0)
                {
                    // Stop the timer and reset
                    MainTimer.Stop();
                    TimeLeft = 1500;
                    InvokeOnMainThread(async () =>
                    {
                        // Reset the UI
                        ResetTimer();
                        NSAlert alert = new NSAlert();
                        // Set the style and message text
                        alert.AlertStyle = NSAlertStyle.Informational;
                        alert.MessageText = "25 Minutes elapsed! Take a 5 minute break.";
                        // Display the NSAlert from the current view
                        alert.BeginSheet(View.Window);
                        this.View.Window.Title = "Pomodoro Timer";
                        FlashScreen();

                        var soundUrl = NSUrl.FromFilename("/System/Library/Sounds/Ping.aiff");
                        var player = AVAudioPlayer.FromUrl(soundUrl);

                        for (int i = 0; i < 10; i++)
                        {
                            await Task.Delay(1000);
                            player.Play();
                        }




                    });
                }
            };
        }

        public override NSObject RepresentedObject
        {
            get
            {
                return base.RepresentedObject;
            }
            set
            {
                base.RepresentedObject = value;
                // Update the view, if already loaded.
            }
        }

        partial void StartStopButtonClicked(NSObject sender)
        {
            // If the timer is running, we want to stop it,
            // otherwise we want to start it
            if (MainTimer.Enabled)
            {
                MainTimer.Stop();
                StartStopButton.Title = "Start";
            }
            else
            {
                MainTimer.Start();
                StartStopButton.Title = "Stop";
            }
        }

        partial void ResetButtonClicked(NSObject sender)
        {
            ResetTimer();

        }

        partial void SnoozeButtonClicked(NSObject sender)
        {
            TimeLeft = TimeLeft + 60;
            StartStopButton.Title = "Stop";
            MainTimer.Start();
        }

        partial void TimerDurationChanged(NSObject sender)
        {
            ResetTimer();
            int timer = Int32.Parse(this.TimerDurationFieldValue.StringValue) * 60;
            TimeSpan time = TimeSpan.FromSeconds(timer);
            string timeString = time.ToString(@"mm\:ss");
            TimerLabel.StringValue = timeString;
            TimeLeft = timer;
        }



        void ResetTimer()
        {
            MainTimer.Stop();
            StartStopButton.Title = "Start";
            TimeLeft = 1500;
            TimerLabel.StringValue = "25:00";
        }

        async void FlashScreen()
        {
            var screen = NSScreen.MainScreen;
            var window = new NSWindow(new CGRect(0, 0, screen.Frame.Width, screen.Frame.Height), NSWindowStyle.Borderless, NSBackingStore.Buffered, false);
            window.IsOpaque = false;
            window.BackgroundColor = NSColor.White;
            window.Level = NSWindowLevel.ScreenSaver;
            window.MakeKeyAndOrderFront(null);

            for (var i = 0; i < 5; i++)
            {
                window.InvokeOnMainThread(() =>
                {
                    window.IsVisible = !window.IsVisible;
                });
                await Task.Delay(1000);
            }
            window.Close();
        }



    }
}
