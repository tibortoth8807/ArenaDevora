namespace Arena
{
    using System;

    /// <summary>
    /// Hősök osztály. Minden hős rendelkezik egy azonosítóval és életerővel, valamint a lenti szabály szerint tudnak támadni és védekezni.
    /// </summary>
    internal sealed class Hosok
    {
        public uint Id { get; set; }
        public HosTipus HosTipus { get; set; }
        public int EletEro { get; set; }
        public int MaximalisEletEro { get; set; }
        public bool EletbenVan { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">egyedi azonosító</param>
        /// <param name="tipus">hős típus</param>
        /// <param name="eletEro"></param>
        public Hosok(uint id, HosTipus tipus, int eletEro)
        {
            Id = id;
            HosTipus = tipus;
            EletEro = eletEro;
            MaximalisEletEro = eletEro;
            EletbenVan = true;
        }

        /// <summary>
        /// Támadás indítása
        /// </summary>
        /// <param name="vedo">egy hős példány, akivel megküzd a támadó</param>
        public void Tamadas(Hosok vedo)
        {
            // nincs életben
            if (!vedo.EletbenVan)
                return;

            Random rnd = new Random();
            if (HosTipus == HosTipus.Ijasz)
            {
                if (vedo.HosTipus == HosTipus.Lovas)
                {
                    int esely = rnd.Next(1, 101);
                    if (esely <= 40) 
                    { 
                        vedo.EletbenVan = false;
                        vedo.EletEro = 0;
                    }
                }
                else if (vedo.HosTipus == HosTipus.Kardos)
                {
                    vedo.EletbenVan = false;
                    vedo.EletEro = 0;
                }
                else if (vedo.HosTipus == HosTipus.Ijasz)
                {
                    EletbenVan = false;
                    EletEro = 0;
                }
            }

            if (HosTipus == HosTipus.Lovas) 
            { 
                if (vedo.HosTipus == HosTipus.Kardos)
                {
                    vedo.EletbenVan = false;
                    vedo.EletEro = 0;
                }
                else if (vedo.HosTipus == HosTipus.Ijasz)
                {
                    EletbenVan = false;
                    EletEro = 0;
                } 
            }

            if (HosTipus == HosTipus.Kardos) 
            {
                if (vedo.HosTipus == HosTipus.Lovas)
                {
                    vedo.EletbenVan = false;
                    vedo.EletEro = 0;
                }
                else if (vedo.HosTipus == HosTipus.Ijasz)
                {
                    EletbenVan = false;
                    EletEro = 0;
                }
            }
        }
    }
}
