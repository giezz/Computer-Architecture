using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace task4Library
{
    public class ClassForStaticLoad
    {
        /// <summary>
        /// Get device X and Y resolution
        /// </summary>
        /// <returns>Array of two elements where [0] is device width, [1] is device height</returns>
        public static int[] GetMonitorResolution()
        {
            using (Graphics g = Graphics.FromHwnd(IntPtr.Zero))
            {
                int[] dims = new int[2];
                IntPtr hdc = g.GetHdc();

                dims[0] = GetDeviceCaps(hdc, DeviceCap.HORZRES);
                dims[1] = GetDeviceCaps(hdc, DeviceCap.VERTRES);
                g.ReleaseHdc();
                return dims;
            }
        }
        
        /// <summary>
        /// Draw line
        /// </summary>
        /// <param name="hbitmap">Handle of window</param>
        /// <param name="start">Start point</param>
        /// <param name="end">End point</param>
        /// <param name="erase">Erase?</param>
        /// <param name="color">Color of line</param>
        public static void PaintLine(IntPtr hbitmap, Point start, Point end, bool erase, Color color)
        {
            Graphics maskg = Graphics.FromHwnd(IntPtr.Zero);
            IntPtr pTarget = maskg.GetHdc();
            IntPtr pDC = CreateCompatibleDC(pTarget);
            IntPtr pOrig = SelectObject(pDC, pTarget);

            Color penColor = color;
            int penWidth = 20;

            IntPtr pen = CreatePen(PenStyle.PS_SOLID | PenStyle.PS_GEOMETRIC | PenStyle.PS_ENDCAP_ROUND, penWidth,
                (uint) ColorTranslator.ToWin32(penColor));
            
            // select the pen into the device context
            IntPtr oldpen = SelectObject(pTarget, pen);

            MoveToEx(pTarget, start.X, start.Y, IntPtr.Zero);
            LineTo(pTarget, end.X, end.Y);
            
            // select the old pen back
            DeleteObject(SelectObject(pTarget, oldpen));
            SelectObject(pTarget, pOrig);
            maskg.ReleaseHdc();
        }
        
        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool DeleteObject([In] IntPtr hObject);
        
        [DllImport("gdi32.dll")]
        private static extern bool LineTo(IntPtr hdc, int nXEnd, int nYEnd);
        
        [DllImport("gdi32.dll")]
        private static extern bool MoveToEx(IntPtr hdc, int X, int Y, IntPtr lpPoint);

        [DllImport("gdi32.dll")]
        private static extern IntPtr CreatePen(PenStyle fnPenStyle, int nWidth, uint crColor);

        [DllImport("gdi32.dll", EntryPoint = "SelectObject")]
        private static extern IntPtr SelectObject([In] IntPtr hdc, [In] IntPtr hgdiobj);

        [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleDC", SetLastError = true)]
        private static extern IntPtr CreateCompatibleDC([In] IntPtr hdc);

        [DllImport("gdi32.dll")]
        private static extern int GetDeviceCaps(IntPtr hdc, DeviceCap nIndex);

        private enum PenStyle : int
        {
            PS_SOLID = 0, //The pen is solid.
            PS_DASH = 1, //The pen is dashed.
            PS_DOT = 2, //The pen is dotted.
            PS_DASHDOT = 3, //The pen has alternating dashes and dots.
            PS_DASHDOTDOT = 4, //The pen has alternating dashes and double dots.
            PS_NULL = 5, //The pen is invisible.

            PS_INSIDEFRAME =
                6, // Normally when the edge is drawn, it’s centred on the outer edge meaning that half the width of the pen is drawn

            // outside the shape’s edge, half is inside the shape’s edge. When PS_INSIDEFRAME is specified the edge is drawn
            //completely inside the outer edge of the shape.
            PS_USERSTYLE = 7,
            PS_ALTERNATE = 8,
            PS_STYLE_MASK = 0x0000000F,

            PS_ENDCAP_ROUND = 0x00000000,
            PS_ENDCAP_SQUARE = 0x00000100,
            PS_ENDCAP_FLAT = 0x00000200,
            PS_ENDCAP_MASK = 0x00000F00,

            PS_JOIN_ROUND = 0x00000000,
            PS_JOIN_BEVEL = 0x00001000,
            PS_JOIN_MITER = 0x00002000,
            PS_JOIN_MASK = 0x0000F000,

            PS_COSMETIC = 0x00000000,
            PS_GEOMETRIC = 0x00010000,
            PS_TYPE_MASK = 0x000F0000
        };

        private enum DeviceCap
        {
            /// <summary>
            /// Device driver version
            /// </summary>
            DRIVERVERSION = 0,

            /// <summary>
            /// Device classification
            /// </summary>
            TECHNOLOGY = 2,

            /// <summary>
            /// Horizontal size in millimeters
            /// </summary>
            HORZSIZE = 4,

            /// <summary>
            /// Vertical size in millimeters
            /// </summary>
            VERTSIZE = 6,

            /// <summary>
            /// Horizontal width in pixels
            /// </summary>
            HORZRES = 8,

            /// <summary>
            /// Vertical height in pixels
            /// </summary>
            VERTRES = 10,

            /// <summary>
            /// Number of bits per pixel
            /// </summary>
            BITSPIXEL = 12,

            /// <summary>
            /// Number of planes
            /// </summary>
            PLANES = 14,

            /// <summary>
            /// Number of brushes the device has
            /// </summary>
            NUMBRUSHES = 16,

            /// <summary>
            /// Number of pens the device has
            /// </summary>
            NUMPENS = 18,

            /// <summary>
            /// Number of markers the device has
            /// </summary>
            NUMMARKERS = 20,

            /// <summary>
            /// Number of fonts the device has
            /// </summary>
            NUMFONTS = 22,

            /// <summary>
            /// Number of colors the device supports
            /// </summary>
            NUMCOLORS = 24,

            /// <summary>
            /// Size required for device descriptor
            /// </summary>
            PDEVICESIZE = 26,

            /// <summary>
            /// Curve capabilities
            /// </summary>
            CURVECAPS = 28,

            /// <summary>
            /// Line capabilities
            /// </summary>
            LINECAPS = 30,

            /// <summary>
            /// Polygonal capabilities
            /// </summary>
            POLYGONALCAPS = 32,

            /// <summary>
            /// Text capabilities
            /// </summary>
            TEXTCAPS = 34,

            /// <summary>
            /// Clipping capabilities
            /// </summary>
            CLIPCAPS = 36,

            /// <summary>
            /// Bitblt capabilities
            /// </summary>
            RASTERCAPS = 38,

            /// <summary>
            /// Length of the X leg
            /// </summary>
            ASPECTX = 40,

            /// <summary>
            /// Length of the Y leg
            /// </summary>
            ASPECTY = 42,

            /// <summary>
            /// Length of the hypotenuse
            /// </summary>
            ASPECTXY = 44,

            /// <summary>
            /// Shading and Blending caps
            /// </summary>
            SHADEBLENDCAPS = 45,

            /// <summary>
            /// Logical pixels inch in X
            /// </summary>
            LOGPIXELSX = 88,

            /// <summary>
            /// Logical pixels inch in Y
            /// </summary>
            LOGPIXELSY = 90,

            /// <summary>
            /// Number of entries in physical palette
            /// </summary>
            SIZEPALETTE = 104,

            /// <summary>
            /// Number of reserved entries in palette
            /// </summary>
            NUMRESERVED = 106,

            /// <summary>
            /// Actual color resolution
            /// </summary>
            COLORRES = 108,

            // Printing related DeviceCaps. These replace the appropriate Escapes
            /// <summary>
            /// Physical Width in device units
            /// </summary>
            PHYSICALWIDTH = 110,

            /// <summary>
            /// Physical Height in device units
            /// </summary>
            PHYSICALHEIGHT = 111,

            /// <summary>
            /// Physical Printable Area x margin
            /// </summary>
            PHYSICALOFFSETX = 112,

            /// <summary>
            /// Physical Printable Area y margin
            /// </summary>
            PHYSICALOFFSETY = 113,

            /// <summary>
            /// Scaling factor x
            /// </summary>
            SCALINGFACTORX = 114,

            /// <summary>
            /// Scaling factor y
            /// </summary>
            SCALINGFACTORY = 115,

            /// <summary>
            /// Current vertical refresh rate of the display device (for displays only) in Hz
            /// </summary>
            VREFRESH = 116,

            /// <summary>
            /// Vertical height of entire desktop in pixels
            /// </summary>
            DESKTOPVERTRES = 117,

            /// <summary>
            /// Horizontal width of entire desktop in pixels
            /// </summary>
            DESKTOPHORZRES = 118,

            /// <summary>
            /// Preferred blt alignment
            /// </summary>
            BLTALIGNMENT = 119
        }
    }
}