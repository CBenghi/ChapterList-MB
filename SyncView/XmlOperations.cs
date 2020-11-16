﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ChapterListMB
{
    internal static class XmlOperations
    {
        public static void SaveChapterListToXml(ChapterList chapList)
        {
            var xmlDoc = new XDocument(
                new XDeclaration("1.0", "UTF-8", "yes"),
                new XElement("Chapterlist", new XAttribute("version", "1.0")
                )
            );
            foreach (var chapter in chapList.Chapters)
            {
                var newElement = new XElement("Chapter",
                    new XAttribute("pos", chapter.Position),
                    new XAttribute("name", chapter.Title)
                );
                xmlDoc.Root.Add(newElement);
            }
            xmlDoc.Save(CLTrack.XmlPath.LocalPath);
        }
        public static void ReadChapterListFromXml(ChapterList chapList)
        {
            try
            {
                if (!System.IO.File.Exists(CLTrack.XmlPath.LocalPath))
                {
                    throw new FileNotFoundException();
                }
                var chaptersListDoc = XDocument.Load(CLTrack.XmlPath.LocalPath);
                if (chaptersListDoc.Root.Attribute("version").Value == "1.0") // 1.0 is original chapterlist XML format
                {
                    foreach (var xElement in chaptersListDoc.Descendants("Chapter"))
                    {
                        chapList.CreateNewChapter(xElement.Attribute("name").Value,
                            int.Parse(xElement.Attribute("pos").Value));
                    }
                }
            }
            catch (FileNotFoundException)
            {
                // ignored
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
