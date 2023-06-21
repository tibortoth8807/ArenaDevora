namespace Arena
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal sealed class Csata
    {
        List<Hosok> hosok;
        Random rnd;

        public Csata(List<Hosok> hosokLista)
        {
            hosok = hosokLista;
            rnd = new Random();
        }

        /// <summary>
        /// Csata elinditás
        /// </summary>
        public void CsataIndit()
        {
            int kor = 1;

            // addig megy a csata amig van élő hős
            while (EloHosok().Count > 1)
            {
                Console.WriteLine($"Kör: {kor}");

                List<Hosok> csatabanResztVettHosok = EloHosok();
                foreach (Hosok tamado in EloHosok())
                {
                    Hosok vedo = RandomTamado(EloHosok(), tamado);
                    if (vedo != null)
                    {
                        tamado.Tamadas(vedo);
                        Console.WriteLine($"Támadás - Hös: {tamado.Id} | {tamado.HosTipus} || Megtámadta: {vedo.Id} | {vedo.HosTipus}");

                        if (!vedo.EletbenVan) 
                        {
                            Console.WriteLine($"Csatában meghalt hös: {vedo.Id} | {vedo.HosTipus}");
                        }

                    }
                }

                if (csatabanResztVettHosok.Count > 1)
                {
                    Console.WriteLine("-----------Pihenés--------------");
                    Pihenes();
                    Logolas(csatabanResztVettHosok);

                    kor++;
                }
            }

            Console.WriteLine("Csata vége!");

            Hosok nyertes = EloHosok().FirstOrDefault();
            if (nyertes == null)
            {
                Console.WriteLine("Mindenki meghalt.");
            }
            else
            {
                Console.WriteLine($"Nyertes hös: {nyertes.Id} | {nyertes.HosTipus}");
            }
        }

        /// <summary>
        /// Még élő hősök lekérdezése
        /// </summary>
        /// <returns>Élő hősök listájával tér vissza.</returns>
        private List<Hosok> EloHosok()
        {
            List<Hosok> eloHosok = new List<Hosok>();
            foreach (Hosok hos in hosok)
            {
                if (hos.EletbenVan)
                    eloHosok.Add(hos);
            }
            return eloHosok;
        }

        /// <summary>
        /// Egy véletlenszerű támadó lekérése.
        /// </summary>
        /// <param name="eloHosok">Még élő hősök listája</param>
        /// <param name="tamado">Egy hős példány aki támad</param>
        /// <returns>Hős példánnyal tér vissza.</returns>
        private Hosok RandomTamado(List<Hosok> eloHosok, Hosok tamado)
        {
            // elo hosok listajanak lemasolasa
            List<Hosok> tamadokListaja = new List<Hosok>(eloHosok);
            tamadokListaja.Remove(tamado);

            if (tamadokListaja.Count > 0)
            {
                int randomTamado = rnd.Next(tamadokListaja.Count);
                return tamadokListaja[randomTamado];
            }

            return null;
        }

        /// <summary>
        /// Pihenés csata után.
        /// A kimaradt hősök pihennek és növekszik az élet erejük 10-el, viszont nem mehet a maximum fölé. 
        /// A harcban résztvevő hősök életereje a felére csökken, ha ez kisebb mint a kezdeti életerő negyede akkor meghalnak.Kezdeti életerők íjász:
        /// 100 lovas: 150 kardos: 120.
        /// </summary>
        private void Pihenes()
        {
            foreach (Hosok hos in EloHosok())
            {
                if (hos.EletbenVan)
                {
                    hos.EletEro -= hos.EletEro / 2;
                    if (hos.EletEro <= hos.MaximalisEletEro / 4)
                        hos.EletbenVan = false;
                    else if (hos.EletEro < hos.MaximalisEletEro)
                        hos.EletEro = Math.Min(hos.EletEro + 10, hos.MaximalisEletEro);
                }
            }
        }

        /// <summary>
        /// Minden kör végén logolni kell ki támadott meg kit és hogyan változott az életerejük. 
        /// </summary>
        private void Logolas(List<Hosok> csatabanResztVettHosok)
        {
            foreach (Hosok hos in csatabanResztVettHosok)
            {
                Console.WriteLine($"Hös {hos.Id} | {hos.HosTipus} | Életerö: {hos.EletEro}");
            }
            Console.WriteLine();
        }
    }
}
