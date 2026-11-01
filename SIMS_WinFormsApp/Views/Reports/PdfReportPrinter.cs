using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.Views.Reports
{
    public sealed class PdfReportSection
    {
        public PdfReportSection(string title, IEnumerable<string> paragraphs)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Paragraphs = (paragraphs ?? throw new ArgumentNullException(nameof(paragraphs))).ToList();
        }

        public string Title { get; }
        public IReadOnlyList<string> Paragraphs { get; }
    }

    public static class PdfReportPrinter
    {
        public static string Save(
            IWin32Window owner,
            string defaultFileName,
            string title,
            string dateRange,
            string preparedBy,
            IReadOnlyList<PdfReportSection> sections)
        {
            return FinancialReportPdfExporter.SaveSections(
                owner, defaultFileName, title, dateRange, preparedBy, sections);
        }
    }
}
