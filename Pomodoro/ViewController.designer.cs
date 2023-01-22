// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Pomodoro
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		AppKit.NSButton StartStopButton { get; set; }

		[Outlet]
		AppKit.NSTextField TimerDurationFieldValue { get; set; }

		[Outlet]
		AppKit.NSTextField TimerLabel { get; set; }

		[Action ("ResetButtonClicked:")]
		partial void ResetButtonClicked (Foundation.NSObject sender);

		[Action ("SnoozeButtonClicked:")]
		partial void SnoozeButtonClicked (Foundation.NSObject sender);

		[Action ("StartStopButtonClicked:")]
		partial void StartStopButtonClicked (Foundation.NSObject sender);

		[Action ("TimerDurationChanged:")]
		partial void TimerDurationChanged (Foundation.NSObject sender);

		[Action ("TimerDurationChanged2:")]
		partial void TimerDurationChanged2 (Foundation.NSObject sender);

		[Action ("TimeValueChanged:")]
		partial void TimeValueChanged (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (StartStopButton != null) {
				StartStopButton.Dispose ();
				StartStopButton = null;
			}

			if (TimerLabel != null) {
				TimerLabel.Dispose ();
				TimerLabel = null;
			}

			if (TimerDurationFieldValue != null) {
				TimerDurationFieldValue.Dispose ();
				TimerDurationFieldValue = null;
			}
		}
	}
}
