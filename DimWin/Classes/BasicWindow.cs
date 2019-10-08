/*
    The MIT License (MIT)

    Copyright (c) 2019 reblGreen Software Ltd. (https://reblgreen.com/)

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in
    all copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
    THE SOFTWARE.
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Security.Permissions;
using System.Runtime.InteropServices;

namespace DimWin
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    public class BasicWindow : NativeWindow
    {
        private IntPtr Owner = IntPtr.Zero;

        private Native.COLORREF pTransparentKey = new Native.COLORREF() { Color = Color.Magenta };
        public Color TransparentKey
        {
            get { return pTransparentKey.Color; }
            set
            {
                if (value != null)
                {
                    pTransparentKey.Color = value;
                }

                SetTransparency();
            }
        }

        private byte pOpacity = 255;
        public double Opacity
        {
            get { return (double)pOpacity / 255; }
            set
            {
                if (value > 1)
                {
                    value = 1;
                }
                else if (value < 0)
                {
                    value = 0;
                }

                pOpacity = (byte)(value * 255);
                SetTransparency();
            }
        }

        private Color pCol = Color.Magenta;
        public Color Color
        {
            get { return pCol; }
            set
            {
                if (value != null)
                {
                    pCol = value;
                }
            }
        }

        public bool Visible
        {
            get { return Native.IsWindowVisible(Handle); }
            set
            {
                if (value)
                {
                    Show();
                }
                else
                {
                    Hide();
                }
            }
        }

        private bool pTopMost = false;
        public bool TopMost
        {
            get
            {
                return pTopMost;
            }
            set
            {
                if (value == true)
                {
                    Native.SetWindowPos(Handle, (IntPtr)Native.HWND_TOPMOST, 0, 0, 0, 0, (int)(Native.SetWindowPosFlags.SWP_NOMOVE | Native.SetWindowPosFlags.SWP_NOSIZE));
                }
                else
                {
                    Native.SetWindowPos(Handle, (IntPtr)Native.HWND_NOTOPMOST, 0, 0, 0, 0, (int)(Native.SetWindowPosFlags.SWP_NOMOVE | Native.SetWindowPosFlags.SWP_NOSIZE));
                }
                pTopMost = value;
            }
        }

        public bool Active { get; private set; }
        public Rectangle Location { get; private set; }

        #region Events
        // Delegate declarations.
        public delegate void ActivatedEventHandler(object sender, EventArgs e);
        public delegate void MoveEventHandler(object sender, EventArgs e);
        public delegate void MouseOverEventHandler(object sender, EventArgs e);
        public delegate void MouseLeaveEventHandler(object sender, EventArgs e);
        public delegate void ResizeEventHandler(object sender, EventArgs e);
        public delegate void ZOrderChangedEventHandler(object sender, EventArgs e);
        public delegate void ClosingEventHandler(object sender, FormClosingEventArgs e);

        // Events
        public event ActivatedEventHandler Activated;
        public event MoveEventHandler Move;
        public event MouseOverEventHandler MouseOver;
        public event MouseLeaveEventHandler MouseLeave;
        public event ResizeEventHandler Resize;
        public event ZOrderChangedEventHandler ZOrderChanged;
        public event ClosingEventHandler Closing;

        #region Invoker Methods
        protected virtual void InvokeClosing(FormClosingEventArgs e)
        {
            ClosingEventHandler h = Closing;
            if (h != null)
            {
                h(this, e);
            }
        }

        protected virtual void InvokeZOrderChanged(EventArgs e)
        {
            ZOrderChangedEventHandler h = ZOrderChanged;
            if (h != null)
            {
                h(this, e);
            }
        }

        protected virtual void InvokeResize(EventArgs e)
        {
            GetLocation();
            ResizeEventHandler h = Resize;
            if (h != null)
            {
                h(this, e);
            }
        }

        protected virtual void InvokeMove(EventArgs e)
        {
            GetLocation();

            MoveEventHandler h = Move;
            if (h != null)
            {
                h(this, e);
            }
        }

        protected virtual void InvokeMouseOver(EventArgs e)
        {
            MouseOverEventHandler h = MouseOver;
            if (h != null)
            {
                h(this, e);
            }
        }

        protected virtual void InvokeMouseLeave(EventArgs e)
        {
            MouseLeaveEventHandler h = MouseLeave;
            if (h != null)
            {
                h(this, e);
            }
        }

        protected virtual void InvokeActivated(bool active)
        {
            Active = active;

            ActivatedEventHandler h = Activated;
            if (h != null)
            {
                h(this, new EventArgs());
            }
        }
        #endregion
        #endregion

        public BasicWindow(string caption = "", bool startHidden = true)
        {
            CreateParams cp = new CreateParams();
            cp.ClassStyle = (int)Native.ClassStyleFlags.CS_OWNDC;
            cp.Caption = caption;
#if DEBUG
            cp.Style = (int)(Native.StyleFlags.WS_POPUP | Native.StyleFlags.WS_BORDER);
#else
            cp.Style = (int)(Native.StyleFlags.WS_POPUP);
#endif
            cp.ExStyle = (int)(Native.ExStyleFlags.WS_EX_LAYERED | Native.ExStyleFlags.WS_EX_TOOLWINDOW);

            CreateHandle(cp);

            if (startHidden)
            {
                Hide();
            }
        }

        public void SetTransparency()
        {
            Native.SetLayeredWindowAttributes(Handle, (uint)pTransparentKey._color, pOpacity, (uint)(Native.LayeredWindowAttributeFlags.LWA_ALPHA | Native.LayeredWindowAttributeFlags.LWA_COLORKEY));
        }

        protected override void WndProc(ref Message m)
        {
            bool cancel = false;

            switch ((Native.WindowsMessageFlags)m.Msg)
            {
                case Native.WindowsMessageFlags.WM_PAINT:
                    Graphics g = default(Graphics);
                    Native.PAINTSTRUCT ps = new Native.PAINTSTRUCT();
                    g = Graphics.FromHdc(Native.BeginPaint(m.HWnd, ref ps));
                    OnPaint(g);
                    g.Dispose();
                    Native.EndPaint(m.HWnd, ref ps);
                    m.Result = new IntPtr(0);
                    break;

                case Native.WindowsMessageFlags.WM_SIZING:
                    InvokeResize(new EventArgs());
                    break;

                case Native.WindowsMessageFlags.WM_MOVE:
                    InvokeMove(new EventArgs());
                    break;

                case Native.WindowsMessageFlags.WM_MOUSEHOVER:
                    InvokeMouseOver(new EventArgs());
                    break;

                case Native.WindowsMessageFlags.WM_MOUSELEAVE:
                    InvokeMouseLeave(new EventArgs());
                    break;

                case Native.WindowsMessageFlags.WM_ACTIVATEAPP:
                    // Application is activated or deactivated, based on the WParam parameter.
                    InvokeActivated(((int)m.WParam != 0));
                    break;
                case Native.WindowsMessageFlags.WM_WINDOWPOSCHANGING:
                    Native.WINDOWPOS wndPos = (Native.WINDOWPOS)m.GetLParam(typeof(Native.WINDOWPOS));

                    if (((Native.SetWindowPosFlags)wndPos.flags & Native.SetWindowPosFlags.SWP_NOSIZE) == Native.SetWindowPosFlags.SWP_NOSIZE)
                    {
                        InvokeZOrderChanged(new EventArgs());
                    }

                    //Marshal.StructureToPtr(wndPos, m.LParam, true);
                    break;

                case Native.WindowsMessageFlags.WM_GETMINMAXINFO:
                    Native.MINMAXINFO minMaxInfo = (Native.MINMAXINFO)m.GetLParam(typeof(Native.MINMAXINFO));
                    minMaxInfo.ptMinTrackSize.x = 20;
                    minMaxInfo.ptMinTrackSize.y = 20;
                    Marshal.StructureToPtr(minMaxInfo, m.LParam, true);
                    break;

                case Native.WindowsMessageFlags.WM_QUERYENDSESSION:
                    // Windows is shutting down
                    FormClosingEventArgs qes = new FormClosingEventArgs(CloseReason.WindowsShutDown, false);
                    InvokeClosing(qes);
                    if (qes.Cancel)
                    {
                        m.Msg = 0;
                        m.Result = (IntPtr)1;
                        cancel = true;
                    }
                    break;

                case Native.WindowsMessageFlags.WM_CLOSE:
                    // User requested closing
                    FormClosingEventArgs c = new FormClosingEventArgs(CloseReason.UserClosing, false);
                    InvokeClosing(c);
                    if (c.Cancel)
                    {
                        m.Msg = 0;
                        m.Result = (IntPtr)0;
                        m.LParam = (IntPtr)0;
                        cancel = true;
                    }
                    //MessageBox.Show(c.Cancel.ToString());
                    break;

            }
            if (!cancel)
            {
                DefWndProc(ref m);
            }

            return;
        }

        private void GetLocation()
        {
            Native.RECT rect = new Native.RECT();
            Native.GetWindowRect(Handle, out rect);
            Location = rect.ToRectangle();
        }

        public void SetLocation(Rectangle rect)
        {
            SetLocation(rect.Left, rect.Top, rect.Width, rect.Height);
        }

        public void SetLocation(int left, int top)
        {
            SetLocation(left, top, Location.Width, Location.Height);
        }

        public void SetLocation(int left, int top, int width, int height)
        {
            bool invr = false;
            bool invm = false;

            if (width != Location.Width || height != Location.Height)
            {
                invr = true;
            }

            if (left != Location.Left || top != Location.Top)
            {
                invm = true;
            }

            Native.SetWindowPos(Handle, IntPtr.Zero, left, top, width, height, (int)(Native.SetWindowPosFlags.SWP_NOZORDER));
            GetLocation();

            if (invr || invm)
            {
                InvokeMove(new EventArgs());
            }
        }

        public void SendToBack()
        {
            Native.SetWindowPos(Handle, (IntPtr)1, 0, 0, 0, 0,
                (int)Native.SetWindowPosFlags.SWP_NOMOVE | (int)Native.SetWindowPosFlags.SWP_NOSIZE |
                (int)Native.SetWindowPosFlags.SWP_NOOWNERZORDER);
        }

        public void Show(bool activate = true, bool onTaskbar = false)
        {
            short flag;

            if (activate)
            {
                flag = (short)Native.ShowWindowFlags.SW_SHOW;
            }
            else
            {
                flag = (short)Native.ShowWindowFlags.SW_SHOWNOACTIVATE;
            }

            Native.ShowWindow(Handle, flag);

            try
            {
                var taskbarList = (ITaskbarList)new CoTaskbarList();
                taskbarList.HrInit();

                if (onTaskbar)
                {
                    taskbarList.AddTab(Handle);
                }
                else
                {
                    taskbarList.DeleteTab(Handle);
                }
            }
            catch { }
        }

        public void Hide()
        {
            Native.ShowWindow(Handle, (short)Native.ShowWindowFlags.SW_HIDE);
        }

        public void AddChild(IWin32Window child)
        {
            Native.SetWindowLong(child.Handle, (int)Native.GetWindowLongFlags.GWL_HWNDPARENT, Handle);
            Native.SetParent(child.Handle, Handle);
        }

        public void Close()
        {
            ReleaseHandle();
            DestroyHandle();
        }

        public void SetOwner()
        {
            if (!Helpers.IsWin64)
            {
                Native.SetWindowLong(Handle, (int)Native.GetWindowLongFlags.GWL_HWNDPARENT, Owner);
            }
            else
            {
                Native.SetWindowLongPtr(Handle, (int)Native.GetWindowLongFlags.GWL_HWNDPARENT, Owner);
            }
        }

        public void SetOwner(IWin32Window owner)
        {
            SetOwner(owner.Handle);
        }

        public void SetOwner(IntPtr ownerHandle)
        {
            if (!Helpers.IsWin64)
            {
                Owner = (IntPtr)Native.GetWindowLong(Handle, (int)Native.GetWindowLongFlags.GWL_HWNDPARENT);
                Native.SetWindowLong(Handle, (int)Native.GetWindowLongFlags.GWL_HWNDPARENT, ownerHandle);
            }
            else
            {
                Owner = Native.GetWindowLongPtr(Handle, (int)Native.GetWindowLongFlags.GWL_HWNDPARENT);
                Native.SetWindowLongPtr(Handle, (int)Native.GetWindowLongFlags.GWL_HWNDPARENT, ownerHandle);
            }
        }

        public void SetParent(IntPtr handle)
        {
            Native.SetParent(Handle, handle);
        }

        public IntPtr GetParent()
        {
            return (IntPtr)Native.GetWindowLong(Handle, (int)Native.GetWindowLongFlags.GWL_HWNDPARENT);
        }

        internal void OnHandleDestroyed(object sender, EventArgs e)
        {
            ReleaseHandle();
        }

        protected virtual void OnPaint(Graphics g)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            g.Clear(pCol);
        }

        public void Refresh(bool noChildren = false)
        {
            Native.RedrawWindowFlags flags = Native.RedrawWindowFlags.RDW_FRAME | Native.RedrawWindowFlags.RDW_UPDATENOW | Native.RedrawWindowFlags.RDW_INVALIDATE;

            if (noChildren)
            {
                flags = flags | Native.RedrawWindowFlags.RDW_NOCHILDREN;
            }
            else
            {
                flags = flags | Native.RedrawWindowFlags.RDW_ALLCHILDREN;
            }

            Native.RedrawWindow(Handle, IntPtr.Zero, IntPtr.Zero, flags);
        }
    }
}