using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using wooPrint.DesktopApp.ApiClient.Models;
using wooPrint.DesktopApp.Configuration;

namespace wooPrint.DesktopApp.Utils
{
    /// <summary>
    /// </summary>
    public static class PrinterUtil
    {
        private static float _pageWidth;
        private static Order _orderInfo;

        private static readonly int _logoWith = 128;
        private static readonly int _logoHeight = 128;


        /// <summary>
        /// </summary>
        /// <param name="orderInfo"></param>
        /// <returns></returns>
        public static string PrintTicket(Order orderInfo)
        {
            try
            {
                _orderInfo = orderInfo;

                var ps = new PrinterSettings();
                var pdoc = new PrintDocument
                {
                    PrinterSettings = ps
                };
                pdoc.PrinterSettings.Copies = 1;
                pdoc.DefaultPageSettings.Margins = new Margins(10, 10, 10, 10);

                _pageWidth = pdoc.DefaultPageSettings.PrintableArea.Width;

                pdoc.PrintPage += pdoc_PrintPage;

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
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void pdoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            var euro = '€';

            var graphics = e.Graphics;

            var font8 = new Font("Lucida Console", 8, FontStyle.Regular);
            var font10 = new Font("Lucida Console", 10, FontStyle.Bold);

            float leading = 4;
            var lineheight8 = font8.GetHeight() + leading;
            var lineheight10 = font10.GetHeight() + leading;

            var char8Qty = (int) (_pageWidth / 8);
            var char10Qty = (int) (_pageWidth / 10);

            float startX = 6;
            var startY = leading;
            float Offset = 10;

            var formatLeft = new StringFormat(StringFormatFlags.NoClip);
            var formatCenter = new StringFormat(formatLeft);
            var formatRight = new StringFormat(formatLeft);

            formatCenter.Alignment = StringAlignment.Center;
            formatRight.Alignment = StringAlignment.Far;
            formatLeft.Alignment = StringAlignment.Near;

            var brush = Brushes.Black;
            var layoutSize = new SizeF(_pageWidth - Offset * 2, lineheight10);

            // HEADER
            if (!string.IsNullOrWhiteSpace(ConfigurationManager.GetInstance().Config.OrderLogoPath) &&
                File.Exists(ConfigurationManager.GetInstance().Config.OrderLogoPath))
            {
                var imageLogo = Image.FromFile(ConfigurationManager.GetInstance().Config.OrderLogoPath);
                imageLogo = ResizeImage(imageLogo, _logoWith, _logoHeight);

                graphics.DrawImage(imageLogo, new PointF((int) (_pageWidth / 2 - _logoWith / 2), startY + Offset));
                Offset += _logoHeight / 2 + lineheight8 + lineheight8;
            }

            var layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            graphics.DrawString(ConfigurationManager.GetInstance().Config.OrderHeader, font10, brush, layout,
                formatCenter);
            Offset += lineheight8;
            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            graphics.DrawString(ConfigurationManager.GetInstance().Config.OrderSubHeader, font10, brush, layout,
                formatCenter);
            Offset += lineheight10;
            Offset += lineheight10;

            // ORDER DETAILS
            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            graphics.DrawString("".PadRight(char8Qty, '-'), font8, brush, layout, formatCenter);
            Offset = Offset + lineheight8;
            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            graphics.DrawString("RECIBO DEL PEDIDO", font8, brush, layout, formatCenter);
            Offset += lineheight8;

            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            graphics.DrawString("Número: " + _orderInfo.number, font8, brush, layout, formatLeft);
            Offset += lineheight8;
            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            graphics.DrawString("Fecha: " + _orderInfo.date_created.ToString("dd/MM/yyyy"), font8, brush, layout,
                formatLeft);
            Offset += lineheight8;
            Offset += lineheight8;

            // ORDER PRODUCTS
            var productList = BuildProductList(_orderInfo.line_items);

            foreach (var item in productList)
            {
                var itemName = item.name;
                if (itemName.Length > 24)
                    itemName = itemName.Substring(0, 24) + ".";

                layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
                graphics.DrawString(itemName + "  x" + item.quantity, font8, brush, layout, formatLeft);

                layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
                graphics.DrawString(item.total + " " + euro, font8, brush, layout, formatRight);
                Offset = Offset + lineheight8;

                if (item.sub_products_items == null || item.sub_products_items.Count <= 0)
                    continue;

                foreach (var subItem in item.sub_products_items)
                {
                    var subItemName = subItem.name;
                    if (subItemName.Length > 16)
                        subItemName = subItemName.Substring(0, 16) + ".";

                    layout = new RectangleF(new PointF(startX + 8, startY + Offset), layoutSize);
                    graphics.DrawString("- " + subItemName + "  x" + subItem.quantity, font8, brush, layout,
                        formatLeft);

                    layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
                    graphics.DrawString(subItem.total + " " + euro, font8, brush, layout, formatRight);
                    Offset = Offset + lineheight8;
                }
            }

            Offset += lineheight8;

            // ORDER TOTALS
            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            graphics.DrawString("Envío: ", font8, brush, layout, formatLeft);
            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            graphics.DrawString(_orderInfo.shipping_total + " " + euro, font8, brush, layout, formatRight);
            Offset += lineheight8;

            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            graphics.DrawString("Impuesto: ", font8, brush, layout, formatLeft);
            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            graphics.DrawString(_orderInfo.total_tax + " " + euro, font8, brush, layout, formatRight);
            Offset += lineheight8;

            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            graphics.DrawString("Método de Pago: ", font8, brush, layout, formatLeft);
            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);

            var payMethod = _orderInfo.payment_method_title;
            if (payMethod.Length > 24)
                payMethod = payMethod.Substring(0, 24);

            graphics.DrawString(payMethod, font8, brush, layout, formatRight);
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

            // ORDER DETAILS
            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            graphics.DrawString("DETALLES", font8, brush, layout, formatCenter);
            Offset += lineheight8;

            if (_orderInfo.meta_data != null && _orderInfo.meta_data.Count > 0)
            {
                var deliveryDateObject = _orderInfo.meta_data
                    .FirstOrDefault(i => i.key.Equals("pi_delivery_date", StringComparison.InvariantCultureIgnoreCase));

                var deliveryDate = deliveryDateObject != null ? deliveryDateObject.value : "";
                if (!string.IsNullOrWhiteSpace(deliveryDate))
                {
                    layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
                    graphics.DrawString("Fecha de Recogida: ", font8, brush, layout, formatLeft);
                    layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
                    graphics.DrawString(deliveryDate, font8, brush, layout, formatRight);
                    Offset += lineheight8;
                }

                var deliveryTimeObject = _orderInfo.meta_data
                    .FirstOrDefault(i => i.key.Equals("pi_delivery_time", StringComparison.InvariantCultureIgnoreCase));

                var deliveryTime = deliveryTimeObject != null ? deliveryTimeObject.value : "";
                if (!string.IsNullOrWhiteSpace(deliveryTime))
                {
                    layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
                    graphics.DrawString("Hora de Recogida: ", font8, brush, layout, formatLeft);
                    layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
                    graphics.DrawString(deliveryTime, font8, brush, layout, formatRight);
                    Offset += lineheight8;
                }
            }

            Offset += lineheight8;
            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            graphics.DrawString("Notas: ", font8, brush, layout, formatLeft);
            Offset += lineheight8;
            var noteLines = SplitLineToMultiline(_orderInfo.customer_note, char8Qty);
            foreach (var line in noteLines)
            {
                layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
                graphics.DrawString(line, font8, brush, layout, formatLeft);
                Offset += lineheight8;
            }

            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            graphics.DrawString("".PadRight(char8Qty, '-'), font8, brush, layout, formatCenter);
            Offset = Offset + lineheight8;

            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            graphics.DrawString("DIRECCION DE FACTURACION", font8, brush, layout, formatCenter);
            Offset += lineheight8;
            Offset += lineheight8;

            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            graphics.DrawString(_orderInfo.billing.first_name + " " + _orderInfo.billing.last_name, font8, brush,
                layout, formatLeft);
            Offset += lineheight8;

            if (!string.IsNullOrWhiteSpace(_orderInfo.billing.company))
            {
                layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
                graphics.DrawString(_orderInfo.billing.company, font8, brush, layout, formatLeft);
                Offset += lineheight8;
            }

            var addressLine = SplitLineToMultiline(_orderInfo.billing.address_1 + " " + _orderInfo.billing.address_2, char8Qty);
            foreach (var line in addressLine)
            {
                layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
                graphics.DrawString(line, font8, brush, layout, formatLeft);
                Offset += lineheight8;
            }

            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            graphics.DrawString(_orderInfo.billing.postcode + " " + _orderInfo.billing.city, font8, brush, layout,
                formatLeft);
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
            graphics.DrawString("METODO DE ENVIO", font8, brush, layout, formatCenter);
            Offset += lineheight8;

            if (_orderInfo.shipping_lines.Count > 0)
            {
                layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
                graphics.DrawString(_orderInfo.shipping_lines[0].method_title, font8, brush, layout, formatLeft);
                Offset += lineheight8;
            }

            Offset += lineheight8;

            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            graphics.DrawString(ConfigurationManager.GetInstance().Config.OrderFooter, font8, brush, layout,
                formatCenter);
            Offset += lineheight8;

            font8.Dispose();
            font10.Dispose();
        }

        /// <summary>
        /// </summary>
        /// <param name="orderInfoLineItems"></param>
        /// <returns></returns>
        private static IEnumerable<LineItem> BuildProductList(IEnumerable<LineItem> orderInfoLineItems)
        {
            var products = (from fc in orderInfoLineItems select fc).ToList();

            var lookup = products.ToLookup(c => c.mnm_child_of);

            foreach (var p in products)
                if (lookup.Contains(p.id.ToString()))
                    p.sub_products_items = lookup[p.id.ToString()].ToList();

            products.RemoveAll(p => p.mnm_child_of != "");

            return products;
        }

        /// <summary>
        /// </summary>
        /// <param name="input"></param>
        /// <param name="rowLength"></param>
        /// <returns></returns>
        private static IEnumerable<string> SplitLineToMultiline(string input, int rowLength)
        {
            var result = new List<string>();
            var line = new StringBuilder();

            var stackR = new Stack<string>(input.Split(' '));
            var stack = new Stack<string>();
            while (stackR.Count != 0)
            {
                stack.Push(stackR.Pop());
            }

            while (stack.Count > 0)
            {
                var word = stack.Pop();
                if (word.Length > rowLength)
                {
                    var head = word.Substring(0, rowLength);
                    var tail = word.Substring(rowLength);

                    word = head;
                    stack.Push(tail);
                }

                if (line.Length + word.Length > rowLength)
                {
                    result.Add(line.ToString());
                    line.Clear();
                }

                line.Append(word + " ");
            }

            result.Add(line.ToString());
            return result.ToArray();
        }

        /// <summary>
        ///     Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
    }
}