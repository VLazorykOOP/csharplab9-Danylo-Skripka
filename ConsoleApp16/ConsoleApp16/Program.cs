using System;
using System.Collections;
using System.Collections.Generic;

public class Song
{
    public string Title { get; set; }
    public string Artist { get; set; }
    public double Duration { get; set; } // Тривалість пісні у хвилинах

    public Song(string title, string artist, double duration)
    {
        Title = title;
        Artist = artist;
        Duration = duration;
    }

    public override string ToString()
    {
        return $"{Title} by {Artist}, {Duration} min";
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        Song other = (Song)obj;
        return Title == other.Title && Artist == other.Artist && Duration == other.Duration;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Title, Artist, Duration);
    }
}

public class MusicDisc
{
    public string Title { get; set; }
    public string Artist { get; set; }
    public List<Song> Songs { get; set; }

    public MusicDisc(string title, string artist)
    {
        Title = title;
        Artist = artist;
        Songs = new List<Song>();
    }

    public void AddSong(Song song)
    {
        Songs.Add(song);
    }

    public void RemoveSong(Song song)
    {
        Songs.Remove(song);
    }

    public override string ToString()
    {
        return $"{Title} by {Artist}";
    }
}

public class MusicCatalog
{
    private Hashtable catalog;

    public MusicCatalog()
    {
        catalog = new Hashtable();
    }

    public void AddDisc(MusicDisc disc)
    {
        if (!catalog.Contains(disc.Title))
        {
            catalog.Add(disc.Title, disc);
        }
        else
        {
            Console.WriteLine("Disc already exists in the catalog.");
        }
    }

    public void RemoveDisc(string title)
    {
        if (catalog.Contains(title))
        {
            catalog.Remove(title);
        }
        else
        {
            Console.WriteLine("Disc not found in the catalog.");
        }
    }

    public void AddSongToDisc(string discTitle, Song song)
    {
        if (catalog.Contains(discTitle))
        {
            MusicDisc disc = (MusicDisc)catalog[discTitle];
            disc.AddSong(song);
        }
        else
        {
            Console.WriteLine("Disc not found in the catalog.");
        }
    }

    public void RemoveSongFromDisc(string discTitle, Song song)
    {
        if (catalog.Contains(discTitle))
        {
            MusicDisc disc = (MusicDisc)catalog[discTitle];
            disc.RemoveSong(song);
        }
        else
        {
            Console.WriteLine("Disc not found in the catalog.");
        }
    }

    public void ViewCatalog()
    {
        foreach (DictionaryEntry entry in catalog)
        {
            MusicDisc disc = (MusicDisc)entry.Value;
            Console.WriteLine(disc);
            foreach (var song in disc.Songs)
            {
                Console.WriteLine($"  - {song}");
            }
        }
    }

    public void ViewDisc(string title)
    {
        if (catalog.Contains(title))
        {
            MusicDisc disc = (MusicDisc)catalog[title];
            Console.WriteLine(disc);
            foreach (var song in disc.Songs)
            {
                Console.WriteLine($"  - {song}");
            }
        }
        else
        {
            Console.WriteLine("Disc not found in the catalog.");
        }
    }

    public void SearchByArtist(string artist)
    {
        foreach (DictionaryEntry entry in catalog)
        {
            MusicDisc disc = (MusicDisc)entry.Value;
            if (disc.Artist == artist)
            {
                Console.WriteLine(disc);
            }

            foreach (var song in disc.Songs)
            {
                if (song.Artist == artist)
                {
                    Console.WriteLine($"  - {song}");
                }
            }
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        MusicCatalog catalog = new MusicCatalog();

        MusicDisc disc1 = new MusicDisc("Thriller", "Michael Jackson");
        disc1.AddSong(new Song("Thriller", "Michael Jackson", 5.57));
        disc1.AddSong(new Song("Beat It", "Michael Jackson", 4.18));

        MusicDisc disc2 = new MusicDisc("Back in Black", "AC/DC");
        disc2.AddSong(new Song("Hells Bells", "AC/DC", 5.12));
        disc2.AddSong(new Song("Shoot to Thrill", "AC/DC", 5.17));

        catalog.AddDisc(disc1);
        catalog.AddDisc(disc2);

        Console.WriteLine("Viewing entire catalog:");
        catalog.ViewCatalog();

        Console.WriteLine("\nViewing specific disc (Thriller):");
        catalog.ViewDisc("Thriller");

        Console.WriteLine("\nSearching for all records by 'Michael Jackson':");
        catalog.SearchByArtist("Michael Jackson");

        catalog.RemoveSongFromDisc("Thriller", new Song("Beat It", "Michael Jackson", 4.18));
        Console.WriteLine("\nViewing 'Thriller' disc after removing a song:");
        catalog.ViewDisc("Thriller");

        catalog.RemoveDisc("Back in Black");
        Console.WriteLine("\nViewing entire catalog after removing 'Back in Black':");
        catalog.ViewCatalog();
    }
}
