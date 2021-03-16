﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
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
        private static float PageWidth;
        private static Order _orderInfo;
        private static string _shopName = "";

        /// <summary>
        /// </summary>
        /// <param name="orderInfo"></param>
        /// <returns></returns>
        public static string PrintTicket(Order orderInfo)
        {
            try
            {
                _orderInfo = orderInfo;

                var apiUrl = new Uri(ConfigurationManager.GetInstance().Config.ApiUrl);
                _shopName = apiUrl.Segments.Length > 1 ? apiUrl.Segments[1].Trim('/') : "";
                _shopName = _shopName.Replace("-", " ");
                _shopName = _shopName.ToUpperInvariant();

                var ps = new PrinterSettings();
                var pdoc = new PrintDocument
                {
                    PrinterSettings = ps
                };
                pdoc.PrinterSettings.Copies = 1;
                pdoc.DefaultPageSettings.Margins = new Margins(10, 10, 10, 10);

                PageWidth = pdoc.DefaultPageSettings.PrintableArea.Width;

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

            var char8Qty = (int) (PageWidth / 8);
            var char10Qty = (int) (PageWidth / 10);

            float startX = 6;
            var startY = leading;
            float Offset = 10;

            var formatLeft = new StringFormat(StringFormatFlags.NoClip);
            var formatCenter = new StringFormat(formatLeft);
            var formatRight = new StringFormat(formatLeft);

            formatCenter.Alignment = StringAlignment.Center;
            formatRight.Alignment = StringAlignment.Far;
            formatLeft.Alignment = StringAlignment.Near;

            var layoutSize = new SizeF(PageWidth - Offset * 2, lineheight10);
            var layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);

            var brush = Brushes.Black;

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
            graphics.DrawString("RECIBO DEL PEDIDO", font8, brush, layout, formatCenter);
            Offset += lineheight8;

            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            graphics.DrawString("Número: " + _orderInfo.number, font8, brush, layout, formatLeft);
            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            graphics.DrawString("Fecha: " + _orderInfo.date_created.ToString("dd/MM/yyyy"), font8, brush, layout,
                formatRight);
            Offset += lineheight8;
            Offset += lineheight8;

            // ORDER PRODUCTS
            for (var i = 0; i < _orderInfo.line_items.Count; i++)
            {
                var item = _orderInfo.line_items[i];

                var itemName = item.name;
                if (itemName.Length > 24)
                    itemName = itemName.Substring(0, 24) + ".";

                layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
                graphics.DrawString(itemName + "  x" + item.quantity, font8, brush, layout, formatLeft);

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

            // ORDER DETAILS
            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            graphics.DrawString("DETALLES", font8, brush, layout, formatCenter);
            Offset += lineheight8;

            if (_orderInfo.meta_data != null && _orderInfo.meta_data.Count > 0)
            {
                var deliveryDateObject = _orderInfo.meta_data
                    .Where(i => i.key.Equals("pi_delivery_date", StringComparison.InvariantCultureIgnoreCase))
                    .FirstOrDefault();
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
                    .Where(i => i.key.Equals("pi_delivery_time", StringComparison.InvariantCultureIgnoreCase))
                    .FirstOrDefault();
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
            var noteLines = SplitLineToMultiline(_orderInfo.customer_note, 40);
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

            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            graphics.DrawString(_orderInfo.billing.address_1 + " " + _orderInfo.billing.address_2, font8, brush, layout,
                formatLeft);
            Offset += lineheight8;

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
            graphics.DrawString("THB", font8, brush, layout, formatCenter);
            Offset += lineheight8;

            font8.Dispose();
            font10.Dispose();
        }

        /// <summary>
        /// </summary>
        /// <param name="input"></param>
        /// <param name="rowLength"></param>
        /// <returns></returns>
        public static string[] SplitLineToMultiline(string input, int rowLength)
        {
            var result = new List<string>();
            var line = new StringBuilder();

            var stack = new Stack<string>(input.Split(' '));

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
                    result.Insert(0, line.ToString());
                    line.Clear();
                }

                line.Insert(0, word + " ");
            }

            result.Insert(0, line.ToString());
            return result.ToArray();
        }
    }
}