using System.IO;
using System.Linq;
using System.Xml.Linq;

using ProfanityEdit.Models;

public class Xspf
{
    XNamespace ns = @"http://xspf.org/ns/o";
    XNamespace vlcNs = @"http://www.videolan.org/vlc/playlist/ns/o";

    public Stream EditListToXspf(EditList editList, UserPreferenceSet preferenceSet)
    {
        // Make sure editList is sorted
        var editListItems = editList.EditListItems.OrderBy(e => e.StartTime).ToList();

        // Create XSPF document foundation
        XDocument doc = new XDocument(new XDeclaration("1.0", "UTF-8", null),
            new XElement(ns + "playlist",
                new XAttribute(XNamespace.Xmlns + "vlc", vlcNs.NamespaceName),
                new XElement(ns + "title", "Playlist"),
                new XElement(ns + "trackList")));

        // Process
        float startTime = 0.000F;
        float endTime = 0.000F;
        int trackId = 0;
        for (int i = 0; i < editListItems.Count(); i++)
        {
            // Ignore objectionables allowed by preference set






            var editListItem = editListItems[i];
            endTime = editListItem.StartTime;

            // add clear video
            AddVideoSegmentToXspf(doc, ns, vlcNs, trackId, startTime, endTime, false);
            trackId++;

            // add muted video
            AddVideoSegmentToXspf(doc, ns, vlcNs, trackId, editListItem.StartTime, editListItem.EndTime, true);
            trackId++;

            startTime = editListItem.EndTime;
        }

        // final segment
        AddVideoSegmentToXspf(doc, ns, vlcNs, trackId, startTime, 0, false);


        var fileStream = new MemoryStream();
        doc.Save(fileStream);
        fileStream.Position = 0;
        return fileStream;
    }


    private void AddVideoSegmentToXspf(XDocument doc, XNamespace ns, XNamespace vlcNs, int id, float startTime, float stopTime, bool muted = false)
    {
        XElement trackList = doc.Descendants(ns + "trackList").First();
        XElement extension = new XElement(ns + "extension",
                    new XAttribute("application", @"http://www.videolan.org/vlc/playlist/0"),
                    new XElement(vlcNs + "id", id.ToString()),
                    new XElement(vlcNs + "option", @"start-time=" + startTime.ToString()),
                    new XElement(vlcNs + "option", @"stop-time=" + stopTime.ToString()));
        trackList.Add(
            new XElement(ns + "track",
                new XElement(ns + "location", @"dvd:///f:/#1"),
                extension));

        if (muted)
        {
            extension.Add(new XElement(vlcNs + "option", "no-audio"));
        }
    }

}