namespace Arena
{
    using System;
    using System.Collections.Generic;

    namespace HeroBattle
    {
        class Program
        {
            static void Main(string[] args)
            {
                List<Hosok> hosok = HosokGeneralas(6);

                Csata csata = new Csata(hosok);
                csata.CsataIndit();

                Console.ReadKey();
            }

            static List<Hosok> HosokGeneralas(int hosokSzama)
            {
                List<Hosok> hosok = new List<Hosok>();
                Random rnd = new Random();

                for (int i = 1; i <= hosokSzama; i++)
                {
                    HosTipus hosTipus = (HosTipus)rnd.Next(3);
                    int eletEro = 0;
                    switch (hosTipus)
                    {
                        case HosTipus.Ijasz:
                            eletEro = 100;
                            break;
                        case HosTipus.Lovas:
                            eletEro = 150;
                            break;
                        case HosTipus.Kardos:
                            eletEro = 120;
                            break;
                    }


                    hosok.Add(new Hosok((uint)i,
                        hosTipus,
                        eletEro));
                }

                return hosok;
            }
        }
    }

}
