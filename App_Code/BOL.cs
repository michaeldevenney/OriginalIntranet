using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using Microsoft.Reporting.WebForms;

namespace Veritas
{
    public static class BOL
    {
        public static void Print(int inBOLID, List<string> recipients, string project, string BOLNumber)
        {
            ReportViewer rview = new ReportViewer();
            ReportParameter bolID = new ReportParameter("ID", inBOLID.ToString());

            rview.ServerReport.ReportServerUrl = new Uri("http://Veritas15/reports");
            rview.ServerReport.ReportPath = "/Production Reports/BillOfLading";
            rview.ServerReport.SetParameters(bolID);

            string mimeType, encoding, extension;
            string[] streamids;
            Microsoft.Reporting.WebForms.Warning[] warnings;
            string format = "PDF";

            string deviceInfo =
                "<DeviceInfo>" +
                "  <OutputFormat>PDF</OutputFormat>" +
                "  <PageWidth>8.5in</PageWidth>" +
                "  <PageHeight>11in</PageHeight>" +
                "  <MarginTop>0.25in</MarginTop>" +
                "  <MarginLeft>0.25in</MarginLeft>" +
                "  <MarginRight>0.25in</MarginRight>" +
                "  <MarginBottom>0.25in</MarginBottom>" +
                "</DeviceInfo>";

            byte[] bytes = rview.ServerReport.Render(format, deviceInfo, out mimeType, out encoding, out extension, out streamids, out warnings);

            MailMessage msg = new MailMessage();
            foreach (string recip in recipients)
            {
                msg.To.Add(new MailAddress(recip));
            }

            //msg.CC.Add(new MailAddress("michael.devenney@veritas-medicalsolutions.com"));
            //msg.CC.Add(new MailAddress("john.brooks@veritas-medicalsolutions.com"));

            msg.Subject = "Bill of Lading - " + inBOLID.ToString();
            msg.From = new MailAddress("reporting@veritas-medicalsolutions.com");
            msg.Body = "For any questions about this bill of lading please contact a member of the project management team.";

            MemoryStream ms = new MemoryStream(bytes);
            Attachment pdf = new Attachment(ms, inBOLID.ToString() + ".pdf");

            msg.Attachments.Add(pdf);

            SmtpClient smtp = new SmtpClient("ver-sbs-01");
            smtp.Send(msg);
        }
    }
}