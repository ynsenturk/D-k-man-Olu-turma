using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace fatura
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string yol,kaynak;
            Console.WriteLine("Donusturulecek dosyanin yolu:");
            kaynak = Console.ReadLine();
            Console.WriteLine("Olusturulacak dosyanin yolu:");
            yol = Console.ReadLine();
            try
            {
                faturaOlustur(kaynak,yol);
            }
            catch (Exception)
            {

                Console.WriteLine("Yanlis bir dosya yolu girdiniz...\n");
                
            }
           
            Console.ReadKey();


        }

        private static void faturaOlustur(string kaynak,string yol)
        {
            string[] tumSatirlar;
            //csv dosyasındaki verileri diziye aldım.
            tumSatirlar = System.IO.File.ReadAllLines(kaynak,
                                                            Encoding.GetEncoding("windows-1254"));

            //Geçici olarak oluşturduğum alanlara verileri çektim.
            var faturalar = from satir in tumSatirlar
                            let bilgi = satir.Split(';') //her bir satırı aralarındaki noktalı virgüle göre kelime kelime ayırdım.
                            select new
                            {
                                FaturaNo = bilgi[0],
                                Ad = bilgi[1],
                                Soyad = bilgi[2],
                                Tutar = bilgi[3],

                            };
            
            //faturalar içerisinde dönülüyor.
            foreach (var fatura in faturalar)
            {

                Console.WriteLine("{0} - {1} - {2} - {3}", fatura.FaturaNo, fatura.Ad, fatura.Soyad, fatura.Tutar);

                DateTime tarih = DateTime.Now;
                //Burada her satırın sonuna \n yazarak not defterinde alt satırlara geçmeyi denedim. Ancak olmayınca paragrafı satırlar halinde yazdırdım.
                string satir1 = "Konu: " + tarih.ToString("d") + " tarihli faturanız.";
                string satir2 = "Sayın " + fatura.Ad + " " + fatura.Soyad;
                string satir3 = fatura.FaturaNo + " numaralı hizmet faturanız ekte gönderilmiştir.";
                string satir4 = "Bu dönem için fatura tutarınız: " + fatura.Tutar + " TL.";
                string satir5 = "Saygılarımızla,";
                string satir6 = "Moreum A.Ş.";
                dosyayaYaz(fatura.FaturaNo, yol, satir1, satir2, satir3, satir4, satir5, satir6);


            }
        }

        private static void dosyayaYaz(string faturaNo, string dosya_yolu, string satir1, string satir2, string satir3, string satir4, string satir5, string satir6)
        {
            string dosya_adi = faturaNo + ".txt";//dosyanın ismini faturaNo olacak şekilde belirttim.
            string hedef_yolu = System.IO.Path.Combine(dosya_yolu, dosya_adi);
            if (System.IO.File.Exists(hedef_yolu)) //belirtilen hedefte oluşturmak istediğimiz dosyalar var mı?
            {
                Console.Write("Dosya zaten mevcuttur.\n");
            }
            else
            {
                
                using (System.IO.StreamWriter dosya = new System.IO.StreamWriter(hedef_yolu))

                {
                    dosya.WriteLine(satir1);
                    dosya.WriteLine("");
                    dosya.WriteLine(satir2);
                    dosya.WriteLine(satir3);
                    dosya.WriteLine(satir4);
                    dosya.WriteLine("");
                    dosya.WriteLine(satir5);
                    dosya.WriteLine(satir6);
                    Console.WriteLine("Dosya olusturuldu..");
                }
                //System.IO.File.Create(hedef_yolu);
                //System.IO.File.WriteAllText(hedef_yolu, icerik);
            }
        }
    }
}
