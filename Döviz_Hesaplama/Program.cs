using System;
using System.Xml;

namespace Döviz_Hesaplama
{
    internal class Program
    {
        static void Main(string[] args)
        {

        first: XmlDocument datas = new XmlDocument();
            string rates = "https://www.tcmb.gov.tr/kurlar/today.xml";
            datas.Load(rates);

            string dolar = datas.SelectSingleNode("Tarih_Date/Currency[@Kod='USD']/BanknoteSelling").InnerXml;
            string euro = datas.SelectSingleNode("Tarih_Date/Currency[@Kod='EUR']/BanknoteSelling").InnerXml;
            dolar = dolar.Replace('.', ',');
            euro = euro.Replace('.', ',');

            Console.WriteLine($"1 Dolar: \t {dolar} TL\n1 Euro: \t {euro} TL\n\n\n");
            Console.WriteLine("Hangi döviz ile işlem yapmak istiyorsunuz? (Dolar için (D) - Euro için (E) tuşlayınız || Çıkmak için lütfen (Q) tuşlayınız");
            string islem = Console.ReadLine();

            if (islem.ToLower() == "q")
            {
                Console.WriteLine("İyi günler");
                goto exit;
            }

            else if (islem.ToLower() != "e" && islem.ToLower() != "d")
            {
                Console.WriteLine("Yanlış tuşlama yaptınız\n");
                goto first;
            }


        miktar: Console.WriteLine("Lütfen dönüştürmek istediğiniz TL miktarını girin (Küsurat için lütfen virgül(,) veya nokta(.) kullanın)");
            string miktar = Console.ReadLine().Replace(',', '.').Replace('-',' ').Trim();

            bool x = decimal.TryParse(miktar, out decimal val);


            if (x == false)
            {
                Console.WriteLine("Hatalı giriş yaptınız\n");
                goto miktar;
            }

            Console.Clear();
            decimal sonuc = val / (islem.ToLower() == "d" ? Convert.ToDecimal(dolar) : Convert.ToDecimal(euro));
            Console.WriteLine($"{miktar} TL  =  {Decimal.Round(sonuc, 4)} {(islem.ToLower() == "d" ? "Dolar" : "Euro")}");
            Console.WriteLine(new String('=', 100));
        devamMi: Console.WriteLine("Başka bir işlem yapmak istiyor musunuz?  (Evet ise (E) || Hayır ise (H)");
            string devamMi = Console.ReadLine();

            switch (devamMi.ToLower())
            {
                case "e":
                    Console.Clear();
                    goto first;
                case "h":
                    Console.WriteLine("İyi günler");
                    break;
                default:
                    goto devamMi;
            }

        exit: System.Threading.Thread.Sleep(1000);

            Environment.Exit(1);
        }
    }
}
