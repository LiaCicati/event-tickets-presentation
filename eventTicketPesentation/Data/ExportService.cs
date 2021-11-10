using System;
using System.IO;
using eventTicketPesentation.Models;
using Syncfusion.Drawing;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using PdfDocument = Syncfusion.Pdf.PdfDocument;
using PdfPage = Syncfusion.Pdf.PdfPage;

namespace eventTicketPesentation.Service
{
    public class ExportService
    {
        public MemoryStream CreatePdf(Ticket ticket)
        {
            if (ticket == null)
            {
                throw new ArgumentNullException("Data cannot be null");
            }

            using (PdfDocument pdfDocument = new PdfDocument())
            {
                int paragraphAfterSpacing = 0;
                int cellMargin = 0;

                PdfPage page = pdfDocument.Pages.Add();

                PdfStandardFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);

                PdfTextElement title = new PdfTextElement("Electronic Ticket", font, PdfBrushes.Black);
                PdfLayoutResult result = title.Draw(page, new PointF(0, 0));

                PdfStandardFont contentFont = new PdfStandardFont(PdfFontFamily.Helvetica, 16);
                
                PdfLayoutFormat format = new PdfLayoutFormat();
                format.Layout = PdfLayoutType.Paginate;
                PdfTextElement TicketName = new PdfTextElement("Event: " + ticket.EventId, contentFont, PdfBrushes.Black);
                PdfTextElement TicketNumber = new PdfTextElement("Nr: " + ticket.ticketNr, contentFont, PdfBrushes.Black);
                result = TicketName.Draw(page, new RectangleF(0, result.Bounds.Bottom + paragraphAfterSpacing, page.GetClientSize().Width, page.GetClientSize().Height), format);
                result = TicketNumber.Draw(page, new RectangleF(0, result.Bounds.Bottom + paragraphAfterSpacing, page.GetClientSize().Width, page.GetClientSize().Height), format);
                
                
                // PdfGrid pdfGrid = new PdfGrid();
                // pdfGrid.Style.CellPadding.Left = cellMargin;
                // pdfGrid.Style.CellPadding.Right = cellMargin;
                //
                // pdfGrid.ApplyBuiltinStyle(PdfGridBuiltinStyle.ListTable4Accent1);

                
                using (MemoryStream stream = new MemoryStream())
                {
                    pdfDocument.Save(stream);
                    pdfDocument.Close(true);
                    return stream;
                }
            }
        }
    }
}