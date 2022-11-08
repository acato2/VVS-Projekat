﻿using System;
using System.Collections.Generic;
using System.Linq;

public class Glasanje
{
	List<Kandidat> nezavisniKandidati;
	List<Stranka> stranke;
    List<Glasac> glasaci;

    public Glasanje()
    {
        Nezavisni = new List<Kandidat>();
        Stranke = new List<Stranka>();
        Glasaci = new List<Glasac>();
    }
    public List<Kandidat> Nezavisni
    {
        get
        {
            return nezavisniKandidati;
        }

        set
        {
            nezavisniKandidati = value;
        }
    }
    public List<Stranka> Stranke
    {
        get
        {
            return stranke;
        }

        set
        {
            stranke = value;
        }
    }
    public List<Glasac> Glasaci
    {
        get
        {
            return glasaci;
        }

        set
        {
            glasaci = value;
        }
    }
    public Glasac dodajGlasaca(Glasac glasac)
    {
        glasaci.Add(glasac);
        return glasac;
    }
    public void PrikaziGlasace() //Ispisuje listu glasaca, samo ime, prezime i Id, ostalo je zabranjeno u postavci
    {
        int brojac = 1;
        //glasaci.Sort(delegate (Glasac x, Glasac y) { return x.getPrezime().CompareTo(y.getPrezime(); });
        Console.WriteLine("Rb Ime             Prezime         ID Kod        Glasao/la");
        glasaci.Sort();
        foreach (Glasac x in glasaci)
        {
            String glasao = "";
            if (x.Glasao)
                glasao = "+";
            Console.WriteLine(brojac.ToString().PadLeft(2, ' ') + ". " +x.getIme().PadRight(15, ' ') +
                " "+ x.getPrezime().PadRight(15, ' ') + " "+ x.getId() + " " + glasao);
            brojac++;
            
        }
    }
    public Glasac DodajGlasaca(string ime, string prezime, string adresa, DateTime datum_rodenja, string br_licne, string jmbg)
    {
        Glasac glasac = new Glasac(ime, prezime, adresa, datum_rodenja, br_licne, jmbg);
        glasaci.Add(glasac);
        return glasac;
    }
    public void izvrsiGlasanjeZaNezavisnog(Glasac glasac, int odabirKandidata)
    {
        glasac.Glasaj(); //kako bi oznacili da je glasao=true
        GlasajZaNezavisnog(odabirKandidata);
    }
   public void GlasajZaNezavisnog(int odabirKandidata)
    {
        nezavisniKandidati.ElementAt(odabirKandidata - 1).dodajGlas();
    }
    public void izvrsiGlasanjeZaStranku(Glasac glasac, int odabirStranke)
    {
        glasac.Glasaj(); //kako bi oznacili da je glasao=true
        GlasajZaStranku(odabirStranke);
    }
    public Stranka GlasajZaStranku(int odabirStranke)
    {
        //prvo glasa za stranku te onda za pojedinacne kandidate stranke ukoliko zeli
        Stranka stranka = stranke.ElementAt(odabirStranke - 1);
        stranka.DodajGlas();
        return stranka;
    }
    public void GlasajZaKandidateStranke(Stranka stranka,List<int>odabiriKandidata)
    {
        List<Kandidat> kandidati = stranka.Kandidati;
        foreach(int i in odabiriKandidata)
        {
            kandidati.ElementAt((int)i-1).dodajGlas();
        }
    }
    public List<Kandidat> DajNezavisne()
    {
        return Nezavisni;
    }

    public List<Stranka> DajStranke()
    {
        return stranke;
    }
    public Glasac getGlasac(string id)
    {
        Glasac glasac = Glasaci.Find(g => g.getId().Equals(id));
        if (glasac!=null)return glasac;
        return null;
    }
    public void RezultatiGlasanja(Glasanje glasanje)
    {
        List<Stranka> st = new List<Stranka> (stranke);
        st.Sort();
        int i = 1;
        Console.WriteLine("\n Rezultati glasanja za stranke:");
        Console.WriteLine("Br Stranka  Broj Glasova");
        foreach(Stranka s in st)
        {
            Console.WriteLine(i.ToString() + ". " + s.Naziv.PadRight(9, ' ') + s.Broj_glasova.ToString());
            i++;
        }
        Console.WriteLine("Rezultati izbora po strankama: ");
        i = 1;
        foreach(Stranka s in st)
        {
            Console.WriteLine(i.ToString() + ". " + s.Naziv);
            List<Kandidat> ka = new List<Kandidat> (s.Kandidati);
            ka.Sort();
            Console.WriteLine("Br Kandidat       Broj Glasova");
            int j = 1;
            foreach (Kandidat k in ka)
            {
                Console.WriteLine(j.ToString() + ". " + (k.Ime + " " + k.Prezime).PadRight(20, ' ') + k.Broj_glasova.ToString());
                    j++;
            }
            i++;
            Console.WriteLine("------------------------");
        }
    }
}