﻿using CefSharp;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Google_Play_Music
{
    partial class CoreMusicApp
    {
        public Boolean mini = false;

        public void restoreMaxiState()
        {
            int ratioX = MaterialSkin.Utilities.DPIMath.ratioX(this);
            int ratioY = MaterialSkin.Utilities.DPIMath.ratioY(this);
            mini = false;
            FormBorderStyle targetStyle;
            if (Environment.OSVersion.Version.Major == 6
                && Environment.OSVersion.Version.Minor == 1)
            {
                targetStyle = FormBorderStyle.None;
            }
            else
            {
                targetStyle = FormBorderStyle.Sizable;
            }
            // Maxi form settings
            Padding = new Padding(2 * ratioX, 24 * ratioY, 2 * ratioX, 2 * ratioY);
            if (GPMBrowser != null)
            {
                GPMBrowser.SetZoomLevel(Properties.Settings.Default.MaxiZoomLevel);
            }
            FormBorderStyle = targetStyle;
            MaximumSize = new Size();
            // Force it to be always bigger than the mini player
            MinimumSize = new Size(301 * ratioX, 301 * ratioY);
            MaximizeBox = true;
            handleZoom = false;
            // Restore Maxi size and pos
            Size savedSize = Properties.Settings.Default.MaxiSize;
            Point savedPoint = Properties.Settings.Default.MaxiPoint;
            if (savedSize.Height == -1 && savedSize.Width == -1)
            {
                savedSize = new Size(1080 * ratioX, 720 * ratioY);
            }
            savedPoint = (onScreen(savedPoint) ? savedPoint : new Point(-1, -1));
            if (savedPoint.X == -1 && savedPoint.Y == -1)
            {
                savedPoint = topLeft(savedSize);
            }
            Location = savedPoint;

            FormBorderStyle = FormBorderStyle.None;
            Size = savedSize;
            FormBorderStyle = targetStyle;

            TopMost = false;
        }

        public void restoreMiniState()
        {
            int ratioX = MaterialSkin.Utilities.DPIMath.ratioX(this);
            int ratioY = MaterialSkin.Utilities.DPIMath.ratioY(this);
            mini = true;
            // Mini form settings
            Padding = new Padding(2 * ratioX, 2 * ratioY, 2 * ratioX, 2 * ratioY);
            ClientSize = new Size(300 * ratioX, 300 * ratioY);
            MaximizeBox = false;
            MaximumSize = new Size(300 * ratioX, 300 * ratioY);
            MinimumSize = new Size(100 * ratioX, 100 * ratioY);
            handleZoom = true;
            FormBorderStyle = FormBorderStyle.None;

            // Restore Mini size and pos
            Size savedSize = Properties.Settings.Default.MiniSize;
            Point savedPoint = Properties.Settings.Default.MiniPoint;
            savedPoint = (onScreen(savedPoint) ? savedPoint : new Point(-1, -1));
            if (savedSize.Height == -1 && savedSize.Width == -1)
            {
                savedSize = new Size(300 * ratioX, 300 * ratioY);
            }
            if (savedPoint.X == -1 && savedPoint.Y == -1)
            {
                savedPoint = topLeft(savedSize);
            }
            Location = savedPoint;
            Size = savedSize;
            setZoomRatio();

            TopMost = Properties.Settings.Default.MiniAlwaysOnTop;
        }

        public void saveMaxiState()
        {
            if (!Maximized)
            {
                FormBorderStyle = FormBorderStyle.None;
                Properties.Settings.Default.MaxiSize = Size;
                FormBorderStyle = FormBorderStyle.Sizable;
                Properties.Settings.Default.MaxiPoint = Location;
            }
        }

        public void saveMiniState()
        {
            Properties.Settings.Default.MiniSize = Size;
            Properties.Settings.Default.MiniPoint = Location;
        }
    }
}
