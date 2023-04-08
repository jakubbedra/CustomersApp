using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using CustomersApp.Model;

namespace CustomersApp;

public class PdfService
{
    public void GeneratePdf(Customer customer, string filepath)
    {
        // Create a new PDF document
        Document document = new Document();

        // Create a PDF writer that writes the document to a MemoryStream
        MemoryStream memStream = new MemoryStream();
        PdfWriter writer = PdfWriter.GetInstance(document, memStream);

        // Open the document for writing
        document.Open();

        // Add content to the document
        Paragraph paragraph = new Paragraph($"Imie: {customer.Name} Nazwisko: {customer.Surname}");
        document.Add(paragraph);

        // Close the document
        document.Close();

        // Write the PDF document to a file
        File.WriteAllBytes(filepath, memStream.ToArray());
    }
}