using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;

using wooPrint.Core.ApiClient.Models;

namespace wooPrint.Core.Utils
{
    /// <summary>
    ///
    /// </summary>
    public static class PrinterUtil
    {
        private static float PageWidth = 0;
        private static Order _orderInfo = null;
        private static string _shopName = "";

        /// <summary>
        ///
        /// </summary>
        /// <param name="orderInfo"></param>
        /// <returns></returns>
        public static string PrintTcket(Order orderInfo)
        {
            try
            {
                PrintDocument pdoc = new PrintDocument
                {
                    PrinterSettings = new PrinterSettings
                    {
                        Copies = 1,
                    }
                };

                PageWidth = pdoc.DefaultPageSettings.PrintableArea.Width;
                _orderInfo = orderInfo;

                Uri apiUrl = new Uri(Configuration.WooPrintConfiguration.Config().ApiService.Url);
                _shopName = (apiUrl.Segments != null && apiUrl.Segments.Length > 1) ? apiUrl.Segments[1].Trim('/') : "";
                _shopName = _shopName.Replace("-", " ");
                _shopName = _shopName.ToUpperInvariant();

                pdoc.PrintPage += new PrintPageEventHandler(pdoc_PrintPage);

                //System.Windows.Forms.PrintPreviewDialog printDlg = new System.Windows.Forms.PrintPreviewDialog();
                //printDlg.Document = pdoc;
                //printDlg.ShowDialog();
                //return "";

                pdoc.Print();
                return string.Empty;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return ex.Message;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void pdoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            char euro = '€';

            Graphics graphics = e.Graphics;

            Font font8 = new Font("Arial", 8, FontStyle.Bold);
            Font font10 = new Font("Arial", 10, FontStyle.Bold);

            float leading = 4;
            float lineheight8 = font8.GetHeight() + leading;
            float lineheight10 = font10.GetHeight() + leading;

            int char8Qty = (int)(PageWidth / 8);
            int char10Qty = (int)(PageWidth / 10);

            float startX = 0;
            float startY = leading;
            float Offset = 0;

            StringFormat formatLeft = new StringFormat(StringFormatFlags.NoClip);
            StringFormat formatCenter = new StringFormat(formatLeft);
            StringFormat formatRight = new StringFormat(formatLeft);

            formatCenter.Alignment = StringAlignment.Center;
            formatRight.Alignment = StringAlignment.Far;
            formatLeft.Alignment = StringAlignment.Near;

            SizeF layoutSize = new SizeF(PageWidth - Offset * 2, lineheight10);
            RectangleF layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);

            Brush brush = Brushes.Black;

            // HEADER
            graphics.DrawString("The House Beer", font10, brush, layout, formatCenter);
            Offset += lineheight8;
            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            graphics.DrawString(_shopName, font10, brush, layout, formatCenter);
            Offset += lineheight10;
            Offset += lineheight10;

            // ORDER DETAILS
            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            graphics.DrawString("".PadRight(char8Qty, '-'), font8, brush, layout, formatCenter);
            Offset = Offset + lineheight8;
            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            graphics.DrawString("ORDER RECEIPT", font8, brush, layout, formatCenter);
            Offset += lineheight8;

            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            graphics.DrawString("Number: " + _orderInfo.number, font8, brush, layout, formatLeft);
            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            graphics.DrawString("Date: " + _orderInfo.date_created.ToString("dd/MM/yyyy"), font8, brush, layout, formatRight);
            Offset += lineheight8;
            Offset += lineheight8;

            // ORDER PRODUCTS
            for (int i = 0; i < _orderInfo.line_items.Count; i++)
            {
                var item = _orderInfo.line_items[i];

                var itemName = item.name;
                if (itemName.Length > 36)
                    itemName = itemName.Substring(0, 36) + "...";

                layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
                graphics.DrawString(itemName + "  x  " + item.quantity, font8, brush, layout, formatLeft);

                layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
                graphics.DrawString(item.total + " " + euro, font8, brush, layout, formatRight);
                Offset = Offset + lineheight8;
            }

            Offset += lineheight8;

            // ORDER TOTALS
            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            graphics.DrawString("Subtotal: ", font8, brush, layout, formatLeft);
            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            graphics.DrawString(_orderInfo.discount_total + " " + euro, font8, brush, layout, formatRight);
            Offset += lineheight8;

            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            graphics.DrawString("Shipping: ", font8, brush, layout, formatLeft);
            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            graphics.DrawString(_orderInfo.shipping_total + " " + euro, font8, brush, layout, formatRight);
            Offset += lineheight8;

            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            graphics.DrawString("Tax: ", font8, brush, layout, formatLeft);
            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            graphics.DrawString(_orderInfo.total_tax + " " + euro, font8, brush, layout, formatRight);
            Offset += lineheight8;

            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            graphics.DrawString("Payment Method: ", font8, brush, layout, formatLeft);
            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            graphics.DrawString(_orderInfo.payment_method_title, font8, brush, layout, formatRight);
            Offset += lineheight8;

            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            graphics.DrawString("Total: ", font8, brush, layout, formatLeft);
            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            graphics.DrawString(_orderInfo.total + " " + euro, font8, brush, layout, formatRight);
            Offset += lineheight8;

            Offset += lineheight8;

            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            graphics.DrawString("".PadRight(char8Qty, '-'), font8, brush, layout, formatCenter);
            Offset = Offset + lineheight8;

            //// ORDER DETAILS
            //layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            //graphics.DrawString("CUSTOMER DETAILS", font8, brush, layout, formatCenter);
            //Offset += lineheight8;
            //Offset += lineheight8;

            //layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            //graphics.DrawString("".PadRight(char8Qty, '-'), font8, brush, layout, formatCenter);
            //Offset = Offset + lineheight8;

            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            graphics.DrawString("BILLING ADDRESS", font8, brush, layout, formatCenter);
            Offset += lineheight8;

            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            graphics.DrawString(_orderInfo.billing.first_name + " " + _orderInfo.billing.last_name, font8, brush, layout, formatLeft);
            Offset += lineheight8;

            if (!string.IsNullOrWhiteSpace(_orderInfo.billing.company))
            {
                layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
                graphics.DrawString(_orderInfo.billing.company, font8, brush, layout, formatLeft);
                Offset += lineheight8;
            }

            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            graphics.DrawString(_orderInfo.billing.address_1 + " " + _orderInfo.billing.address_2, font8, brush, layout, formatLeft);
            Offset += lineheight8;

            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            graphics.DrawString(_orderInfo.billing.postcode + " " + _orderInfo.billing.city, font8, brush, layout, formatLeft);
            Offset += lineheight8;

            if (!string.IsNullOrWhiteSpace(_orderInfo.billing.phone))
            {
                layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
                graphics.DrawString(_orderInfo.billing.phone, font8, brush, layout, formatLeft);
                Offset += lineheight8;
            }

            if (!string.IsNullOrWhiteSpace(_orderInfo.billing.email))
            {
                layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
                graphics.DrawString(_orderInfo.billing.email, font8, brush, layout, formatLeft);
                Offset += lineheight8;
            }

            Offset += lineheight8;

            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            graphics.DrawString("".PadRight(char8Qty, '-'), font8, brush, layout, formatCenter);
            Offset = Offset + lineheight8;

            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            graphics.DrawString("SHIPPING METHOD", font8, brush, layout, formatCenter);
            Offset += lineheight8;

            if (_orderInfo.shipping_lines.Count > 0)
            {
                layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
                graphics.DrawString(_orderInfo.shipping_lines[0].method_title, font8, brush, layout, formatLeft);
                Offset += lineheight8;
            }
            Offset += lineheight8;

            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            graphics.DrawString("THB", font8, brush, layout, formatCenter);
            Offset += lineheight8;

            //layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            //graphics.DrawString("Powered by BizSwoop", font8, brush, layout, formatCenter);
            //Offset += lineheight8;
            //layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            //graphics.DrawString("www.bizwoop.com/print", font8, brush, layout, formatCenter);

            font8.Dispose();
            font10.Dispose();
        }
    }
}