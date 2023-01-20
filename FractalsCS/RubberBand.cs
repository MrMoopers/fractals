using FractalsCS;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FractalsCS
{
    public class RubberBand
    {
        private enum RubberBandState
        {
            Inactive,
            Starting,
            Moving,
        }

        private Point StartPoint;
        private Point EndPoint;
        private RubberBandState CurrentState = RubberBandState.Inactive;
        private Control Surface;

        public RubberBand(Control ControlSurface)
        {
            Surface = ControlSurface;
        }

        public Rectangle SelectedRectangle
        {
            get
            {
                Rectangle selectedRect = new Rectangle();
                selectedRect.X = StartPoint.X < EndPoint.X ? StartPoint.X : EndPoint.X;
                selectedRect.Y = StartPoint.Y < EndPoint.Y ? StartPoint.Y : EndPoint.Y;
                selectedRect.Width = Math.Abs(EndPoint.X - StartPoint.X);
                selectedRect.Height = Math.Abs(EndPoint.Y - StartPoint.Y);
                return selectedRect;
            }
        }

        public void Start(int x, int y)
        {
            StartPoint.X = x;
            StartPoint.Y = y;
            EndPoint.X = x;
            EndPoint.Y = y;
            KeepInView(StartPoint);
            CurrentState = RubberBandState.Starting;
        }

        public void Stop()
        {
            DrawFrame();
            CurrentState = RubberBandState.Inactive;
        }

        private void KeepInView(Point origin)
        {
            if (origin.X < 0)
            {
                origin.X = 0;
            }

            if (origin.X > Surface.ClientSize.Width)
            {
                origin.X = Surface.ClientSize.Width - 1;
            }

            if (origin.Y < 0)
            {
                origin.Y = 0;
            }

            if (origin.Y > Surface.ClientSize.Height)
            {
                origin.Y = Surface.ClientSize.Height - 1;
            }
        }

        public void Move(int x, int y)
        {
            Point newPoint = new Point(x, y);
            KeepInView(newPoint);
            switch (CurrentState)
            {
                case RubberBandState.Inactive:
                    break;

                case RubberBandState.Starting:
                    EndPoint = newPoint;
                    DrawFrame();
                    CurrentState = RubberBandState.Moving;
                    break;

                case RubberBandState.Moving:
                    DrawFrame();
                    EndPoint = newPoint;
                    DrawFrame();
                    break;
            }
        }

        private void DrawFrame()
        {
            Point exactStart = Surface.PointToScreen(StartPoint);
            Point exactEnd = Surface.PointToScreen(EndPoint);
            Size rectSize = new Size(exactEnd.X - exactStart.X, exactEnd.Y - exactStart.Y);
            Rectangle drawRect = new Rectangle(Surface.PointToScreen(StartPoint), rectSize);
            ControlPaint.DrawReversibleFrame(drawRect, Color.Black, FrameStyle.Dashed);
        }
    }
}
