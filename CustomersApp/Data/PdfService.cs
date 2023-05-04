using System.IO;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using CustomersApp.Model;
using CustomersApp.ViewModel;

namespace CustomersApp;

public class PdfService
{
    private BaseFont _baseFont;

    private const int SmallFontSize = 11;
    private const int VerySmallFontSize = 8;
    private const int Interline = 4;
    
    public void GeneratePdf(Customer customer, PdfType type, string filepath)
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        Document document = new Document();
        document.SetMargins(50f, 50f, 150f, 0f);
        PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filepath, FileMode.Create));
        document.Open();


        // title
        _baseFont = BaseFont.CreateFont("C:\\Users\\theKonfyrm\\Desktop\\DejaVuSans.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
        Font titleFont = new Font(_baseFont, 24, Font.BOLDITALIC);
        Paragraph title = new Paragraph("ŚWIADECTWO KREMACJI", titleFont);
        title.Alignment = Element.ALIGN_CENTER;
        document.Add(title);

        switch (type)
        {
            case PdfType.DeadPerson:
                // cremation date
                document.Add(CremationInfo(customer));

                // personal info
                document.Add(PersonalDataTitle(customer));
                document.Add(BirthRow(customer));
                document.Add(DeathRow(customer));
                document.Add(AddressRow(customer));
                document.Add(DeathCertificateRow(customer));
                document.Add(CremationPlaceRow1(customer));
                document.Add(CremationPlaceRow2(customer));
                document.Add(BookOfCremationRow(customer));

                document.Add(ExtraRow(customer));
                break;
            case PdfType.DeadPersonExhumated:
                // cremation date
                document.Add(CremationInfoExhumated(customer));

                // personal info
                document.Add(PersonalDataTitle(customer));
                document.Add(BirthRow(customer));
                document.Add(DeathRow(customer));
                document.Add(AddressRow(customer));
                document.Add(DeathCertificateRow(customer));
                document.Add(CremationPlaceRow1Exhumated(customer));
                document.Add(CremationPlaceRow2(customer));
                document.Add(BookOfCremationRow(customer));

                document.Add(ExtraRow(customer));
                break;
            case PdfType.Remains:
                // cremation date
                document.Add(CremationInfoRemains(customer));

                // personal info
                document.Add(PersonalDataTitleRemains(customer));
                document.Add(CremationPlaceRow1(customer));
                document.Add(CremationPlaceRow2(customer));
                document.Add(BookOfCremationRow(customer));

                document.Add(ExtraRow(customer));
                break;
            case PdfType.ChildBornDead:
                // cremation date
                document.Add(CremationInfo(customer));

                // personal info
                document.Add(PersonalDataTitle(customer));
                document.Add(BirthRowChildBornDead(customer));
                document.Add(DeathCertificateRowChildBornDead(customer));
                document.Add(CremationPlaceRow1(customer));
                document.Add(CremationPlaceRow2(customer));
                document.Add(BookOfCremationRow(customer));

                document.Add(ExtraRow(customer));
                break;
            case PdfType.DeadFetus:
                // cremation date
                document.Add(CremationInfoFetus(customer));

                // personal info
                document.Add(PersonalDataTitleFetus(customer));
                document.Add(CremationPlaceRow1Fetus(customer));
                document.Add(CremationPlaceRow2(customer));
                document.Add(BookOfCremationRow(customer));

                document.Add(ExtraRow(customer));
                break;
        }

        // footer
        Rectangle page = document.PageSize;
        PdfContentByte canvas = writer.DirectContent;
        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, FinalRow(customer), 50, page.GetBottom(50+40), 0);
        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, Footer1(), 50, page.GetBottom(50+11), 0);
        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, Footer2(), 50, page.GetBottom(50), 0);
        //document.Add(FinalRow(customer));
        //document.Add(Footer1());
        //document.Add(Footer2());
        
        document.Close();
    }

    private Paragraph CremationInfo(Customer customer)
    {
        Font titleFont = new Font(_baseFont, SmallFontSize);
        Paragraph paragraph = new Paragraph($"W dniu {customer.CremationDate} roku dokonano spopielenia ciała - kremacji {(customer.Sex=='K' ? "Zmarłej" : "Zmarłego")}", titleFont);
        paragraph.Alignment = Element.ALIGN_LEFT;
        paragraph.SpacingBefore = 20f;
        return paragraph;
    }
    
    private Paragraph CremationInfoExhumated(Customer customer)
    {
        Font titleFont = new Font(_baseFont, SmallFontSize);
        Paragraph paragraph = new Paragraph($"W dniu {customer.CremationDate} roku dokonano spopielenia - kremacji szczątków {(customer.Sex=='K' ? "Zmarłej" : "Zmarłego")}", titleFont);
        paragraph.Alignment = Element.ALIGN_LEFT;
        paragraph.SpacingBefore = 20f;
        return paragraph;
    }
    
    private Paragraph CremationInfoRemains(Customer customer)
    {
        Font titleFont = new Font(_baseFont, SmallFontSize);
        Paragraph paragraph = new Paragraph($"W dniu {customer.CremationDate} roku dokonano spopielenia ciała - kremacji ciał - szczątków ludzkich", titleFont);
        paragraph.Alignment = Element.ALIGN_LEFT;
        paragraph.SpacingBefore = 20f;
        return paragraph;
    }
    
    private Paragraph CremationInfoFetus(Customer customer)
    {
        Font titleFont = new Font(_baseFont, SmallFontSize);
        Paragraph paragraph = new Paragraph($"W dniu {customer.CremationDate} roku dokonano spopielenia płodów dzieci martwo urodzonych", titleFont);
        paragraph.Alignment = Element.ALIGN_LEFT;
        paragraph.SpacingBefore = 20f;
        return paragraph;
    }

    private Paragraph PersonalDataTitle(Customer customer)
    {
        // title
        Font titleFont = new Font(_baseFont, 16, Font.BOLD);
        Paragraph paragraph = new Paragraph($"{customer.Surname.ToUpper()} {customer.Name}", titleFont);
        paragraph.Alignment = Element.ALIGN_CENTER;
        paragraph.SpacingBefore = 20f;
        return paragraph;
    }
    
    private Paragraph PersonalDataTitleRemains(Customer customer)
    {
        // title
        Font titleFont = new Font(_baseFont, 16, Font.BOLD);
        Paragraph paragraph = new Paragraph($"{customer.Surname.ToUpper()} {customer.Name}", titleFont);
        paragraph.Alignment = Element.ALIGN_CENTER;
        paragraph.SpacingBefore = 20f;
        paragraph.SpacingAfter = 20f;
        return paragraph;
    }

    private Paragraph PersonalDataTitleFetus(Customer customer)
    {
        // title
        Font titleFont = new Font(_baseFont, 16, Font.BOLD);
        Paragraph paragraph = new Paragraph($"wg wykazu: {customer.Surname} {customer.Name}", titleFont);
        paragraph.Alignment = Element.ALIGN_CENTER;
        paragraph.SpacingBefore = 20f;
        paragraph.SpacingAfter = 20f;
        return paragraph;
    }
    
    private Paragraph BirthRow(Customer customer)
    {
        Font titleFont = new Font(_baseFont, SmallFontSize);
        Paragraph paragraph = new Paragraph($"{(customer.Sex=='K' ? "urodzonej" : "urodzonego")} dnia {customer.DateOfBirth.Value.ToString("dd-MM-yyyy")} roku - {customer.PlaceOfBirth}", titleFont);
        paragraph.Alignment = Element.ALIGN_LEFT;
        paragraph.SpacingBefore = 20f;
        return paragraph;
    }   
    
    private Paragraph BirthRowChildBornDead(Customer customer)
    {
        Font titleFont = new Font(_baseFont, SmallFontSize);
        Paragraph paragraph = new Paragraph($"{(customer.Sex=='K' ? "urodzonej" : "urodzonego")} dnia {customer.DateOfBirth.Value.ToString("dd-MM-yyyy")} roku - {customer.PlaceOfBirth} /dziecko urodziło się martwe/", titleFont);
        paragraph.Alignment = Element.ALIGN_LEFT;
        paragraph.SpacingBefore = 20f;
        return paragraph;
    }   
    
    private Paragraph DeathRow(Customer customer)
    {
        Font titleFont = new Font(_baseFont, SmallFontSize);
        Paragraph paragraph = new Paragraph($"{(customer.Sex=='K' ? "zmarłej" : "zmarłego")} dnia {customer.DateOfDeath.Value.ToString("dd-MM-yyyy")} roku - {customer.PlaceOfDeath}", titleFont);
        paragraph.Alignment = Element.ALIGN_LEFT;
        paragraph.SpacingBefore = Interline;
        return paragraph;
    }
    
    private Paragraph AddressRow(Customer customer)
    {
        Font titleFont = new Font(_baseFont, SmallFontSize);
        Paragraph paragraph = new Paragraph($"ostatnio {(customer.Sex=='K' ? "zamieszkałej" : "zamieszkałego")} - {customer.Address}", titleFont);
        paragraph.Alignment = Element.ALIGN_LEFT;
        paragraph.SpacingBefore = Interline;
        return paragraph;
    }
    
    private Paragraph DeathCertificateRow(Customer customer)
    {
        Font titleFont = new Font(_baseFont, SmallFontSize);
        Paragraph paragraph = new Paragraph($"akt zgonu nr {customer.DeathCertificateNumber} wydany przez {customer.IssuedBy}", titleFont);
        paragraph.Alignment = Element.ALIGN_LEFT;
        paragraph.SpacingBefore = Interline;
        return paragraph;
    }
    
    private Paragraph DeathCertificateRowChildBornDead(Customer customer)
    {
        Font titleFont = new Font(_baseFont, SmallFontSize);
        Paragraph paragraph = new Paragraph($"akt urodzenia nr {customer.DeathCertificateNumber} wydany przez {customer.IssuedBy}", titleFont);
        paragraph.Alignment = Element.ALIGN_LEFT;
        paragraph.SpacingBefore = Interline;
        return paragraph;
    }
    
    private Paragraph CremationPlaceRow1(Customer customer)
    {
        Font titleFont = new Font(_baseFont, SmallFontSize);
        Paragraph paragraph = new Paragraph($"Spopielenie ciała - kremację wykonano w Zakładzie Kremacji PPU \"Zieleń\" Sp. z o.o.", titleFont);
        paragraph.Alignment = Element.ALIGN_LEFT;
        paragraph.SpacingBefore = Interline;
        return paragraph;
    }
    
    private Paragraph CremationPlaceRow1Exhumated(Customer customer)
    {
        Font titleFont = new Font(_baseFont, SmallFontSize);
        Paragraph paragraph = new Paragraph($"Spopielenie - kremację szczątków wykonano w Zakładzie Kremacji PPU \"Zieleń\" Sp. z o.o.", titleFont);
        paragraph.Alignment = Element.ALIGN_LEFT;
        paragraph.SpacingBefore = Interline;
        return paragraph;
    }
    
    private Paragraph CremationPlaceRow1Fetus(Customer customer)
    {
        Font titleFont = new Font(_baseFont, SmallFontSize);
        Paragraph paragraph = new Paragraph($"Spopielenie płodów wykonano w Zakładzie Kremacji PPU \"Zieleń\" Sp. z o.o.", titleFont);
        paragraph.Alignment = Element.ALIGN_LEFT;
        paragraph.SpacingBefore = Interline;
        return paragraph;
    }
    
    private Paragraph CremationPlaceRow2(Customer customer)
    {
        Font titleFont = new Font(_baseFont, SmallFontSize);
        Paragraph paragraph = new Paragraph($"w Gdańsku przy ulicy Srebrniki 14 na podstawie zlecenia z dnia {customer.IssueDate} roku", titleFont);
        paragraph.Alignment = Element.ALIGN_LEFT;
        paragraph.SpacingBefore = Interline;
        return paragraph;
    }
    
    private Paragraph BookOfCremationRow(Customer customer)
    {
        Font titleFont = new Font(_baseFont, SmallFontSize);
        Paragraph paragraph = new Paragraph($"i zarejestrowano pod nr. {customer.CertificateNumber} w Księdze Kremacji.", titleFont);
        paragraph.Alignment = Element.ALIGN_LEFT;
        paragraph.SpacingBefore = Interline;
        return paragraph;
    }
    
    private Paragraph ExtraRow(Customer customer)
    {
        Font titleFont = new Font(_baseFont, SmallFontSize, Font.BOLD);
        Paragraph paragraph = new Paragraph(customer.Extra, titleFont);
        paragraph.Alignment = Element.ALIGN_LEFT;
        paragraph.SpacingBefore = Interline;
        return paragraph;
    }
    
    private Paragraph FinalRow(Customer customer)
    {
        Font titleFont = new Font(_baseFont, SmallFontSize);
        Paragraph paragraph = new Paragraph($"Gdańsk, dnia {customer.CremationDate} roku.", titleFont);
        paragraph.Alignment = Element.ALIGN_LEFT;
        paragraph.SpacingBefore = 260;
        return paragraph;
    }
    
    private Paragraph Footer1()
    {
        Font titleFont = new Font(_baseFont, VerySmallFontSize);
        Paragraph paragraph = new Paragraph($"Prochy otrzymane przez spopielenie winny być pochowane zgodnie z Ustawą z dnia 31.01.1959 i cmentarzach i", titleFont);
        paragraph.Alignment = Element.ALIGN_LEFT;
        paragraph.SpacingBefore = 20;
        return paragraph;
    }
    
    private Paragraph Footer2()
    {
        Font titleFont = new Font(_baseFont, VerySmallFontSize);
        Paragraph paragraph = new Paragraph($"chowaniu zmarłych/Dz. U. Nr 23/2000 poz.295 ze zm./", titleFont);
        paragraph.Alignment = Element.ALIGN_LEFT;
        paragraph.SpacingBefore = 1;
        return paragraph;
    }
    
}
