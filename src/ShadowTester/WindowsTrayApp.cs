using System;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using ShadowTester.CommandLine;
using ShadowTester.CommandLine.Commands;
using ShadowTesterLib;
using ShadowTesterLib.Recorder;

namespace ShadowTester
{
    public class WindowsTrayApp : Form
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool FreeConsole();

        public static void Run(RecordConfiguration recordConfiguration)
        {
            Console.In.Close();
            Console.Out.Flush();
            Console.Error.Flush();

            FreeConsole();

            Application.Run(new WindowsTrayApp(recordConfiguration));
        }

        internal WindowsTrayApp(RecordConfiguration recordConfiguration)
        {
            InitializeComponent();
            BuildComponents();

            mRecordConfiguration = recordConfiguration;

            ToggleRecordingStatus();
        }

        void ToggleRecordingStatusMenuItem_OnClick(object sender, EventArgs e)
        {
            ToggleRecordingStatus();
        }

        void ExitMenuItem_OnClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        void ToggleRecordingStatus()
        {
            if (mbIsRecording)
                StopRecording();
            else
                StartRecording();

            UpdateMenuItems();
        }

        void StartRecording()
        {
            try
            {
                Factory.ProcessRecorder.Start();
                mbIsRecording = true;
            }
            catch (InvalidOperationException ex)
            {
                mbIsRecording = false;
                return;
            }
        }

        void StopRecording()
        {
            ConsoleCommand command = ConsoleCommandFactory.CreateCommand(
                ConsoleHelper.STOP_RECORDING_ACTION, mRecordConfiguration);
            command.Execute();
            mbIsRecording = false;

            Process.Start(mRecordConfiguration.CapturesPath);
        }

        void UpdateMenuItems()
        {
            mToggleRecordingMenuItem.Text = mbIsRecording
                ? "Finish recording"
                : "Start recording";

            mExitMenuItem.Enabled = !mbIsRecording;
        }

        void InitializeComponent()
        {
            Name = "ShadowTester";
            ResumeLayout(false);
            PerformLayout();
        }

        void BuildComponents()
        {
            mTrayMenu = new ContextMenu();

            mToggleRecordingMenuItem = new MenuItem(
                string.Empty, ToggleRecordingStatusMenuItem_OnClick);
            mExitMenuItem = new MenuItem(
                "Exit ShadowTester", ExitMenuItem_OnClick);

            mTrayMenu.MenuItems.Add(mToggleRecordingMenuItem);
            mTrayMenu.MenuItems.Add(mExitMenuItem);

            mTrayIcon = new NotifyIcon();
            mTrayIcon.Icon = Icon.ExtractAssociatedIcon(
                Assembly.GetExecutingAssembly().Location);
            mTrayIcon.Text = "ShadowTester";
            mTrayIcon.ContextMenu = mTrayMenu;
            mTrayIcon.Visible = true;
        }

        protected override void OnLoad(EventArgs e)
        {
            Visible = false;
            ShowInTaskbar = false;

            base.OnLoad(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                mTrayIcon.Dispose();

            base.Dispose(disposing);
        }

        ContextMenu mTrayMenu;
        NotifyIcon mTrayIcon;

        MenuItem mToggleRecordingMenuItem;
        MenuItem mExitMenuItem;
        bool mbIsRecording = false;

        readonly RecordConfiguration mRecordConfiguration;
    }
}
