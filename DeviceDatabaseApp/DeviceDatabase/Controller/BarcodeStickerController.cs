using BarcodeLib;
using DeviceDatabase.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DeviceDatabase.Controller
{
    public class BarcodeStickerController
    {
        public Image GenerateSticker(Device _Device)
        {
            // Create barcode with serialcode
            Barcode b = new Barcode(_Device.SerialCode, TYPE.CODE128);
            b.IncludeLabel = true;

            // Height added to barcode
            int addedHeight = 50;

            // Encode to Image
            System.Drawing.Image barcodeImage = b.Encode(TYPE.CODE128, b.RawData, 300, 100);

            int barcodeHeight = barcodeImage.Height;

            // Create new Image with added height
            System.Drawing.Image editedBarcodeImage = new Bitmap(barcodeImage.Width, barcodeHeight + addedHeight + 10);


            // Draw on the new Image
            using (Graphics g = Graphics.FromImage(editedBarcodeImage))
            {
                // Make new Image white
                g.FillRectangle(new SolidBrush(System.Drawing.Color.White), 0, 0, editedBarcodeImage.Width, editedBarcodeImage.Height);
                // Draw old Image on new Image
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, addedHeight, barcodeImage.Width, barcodeImage.Height);
                g.DrawImageUnscaledAndClipped(barcodeImage, rect);
                // Font to draw strings with
                Font font = new Font("Times New Roman", 12.0f);
                // Create strings to draw on new Image
                string deviceName = string.Format("Device name:  {0}", _Device.Name);
                string deviceType;
                // DeviceType can be null; Safety check
                if (_Device.DeviceType != null)
                    deviceType = string.Format("Device type:   {0}", _Device.DeviceType.Name);
                else
                    deviceType = string.Format("Device type:     Undefined");

                // Measure both sizes strings to draw
                SizeF deviceNameSize = g.MeasureString(deviceName, font);
                SizeF deviceTypeSize = g.MeasureString(deviceType, font);

                PointF deviceNamePointF;
                PointF deviceTypeSizePointF;
                float drawLocationY;

                // Set variables to center on barcode based on longest string to draw
                if (deviceNameSize.Width > deviceTypeSize.Width)
                {
                    drawLocationY = (editedBarcodeImage.Width / 2) - (deviceNameSize.Width / 2);
                    deviceNamePointF = new PointF(drawLocationY, 10);
                    deviceTypeSizePointF = new PointF(drawLocationY, 26);
                }
                else
                {
                    drawLocationY = (editedBarcodeImage.Width / 2) - (deviceTypeSize.Width / 2);
                    deviceNamePointF = new PointF(drawLocationY, 10);
                    deviceTypeSizePointF = new PointF(drawLocationY, 26);
                }
                // Draw strings on new Image
                g.DrawString(deviceName, font, System.Drawing.Brushes.Black, deviceNamePointF);
                g.DrawString(deviceType, font, System.Drawing.Brushes.Black, deviceTypeSizePointF);
            }

            return editedBarcodeImage;
        }

        public BitmapImage ConvertToBitmapImage(Image _Img)
        {
            MemoryStream ms = new MemoryStream();
            _Img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

            BitmapImage bm = new BitmapImage();

            bm.BeginInit();
            bm.StreamSource = new MemoryStream(ms.ToArray());
            bm.EndInit();

            return bm;
        }
    }
}
