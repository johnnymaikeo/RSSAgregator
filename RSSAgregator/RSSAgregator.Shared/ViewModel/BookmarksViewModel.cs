using RSSAgregator.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.Storage;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Threading.Tasks;


namespace RSSAgregator.ViewModel
{
    public class BookmarksViewModel
    {
        private const string BOOKMARKS_LOCAL_DATA = "bookmarks";
        private const string LOCAL_DATA_FILENAME = "data.xml";
        public List<Bookmarks> BookmarksList = new List<Bookmarks>();

        public bool Add(string name, string url)
        {
            try
            {
                // TODO: try creating URL without adding http:// or https://
                Uri uri = new Uri(url);
                Bookmarks bookmark = new Bookmarks(name, url);
                BookmarksList.Add(bookmark);
                StoreLocalData();
                return true;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return false;
            }
        }

        public void List()
        {
            FetchLocalData();
        }

        #region LOCAL_DATA_STORAGE

        private async void StoreLocalData()
        {
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            var file = await folder.GetFileAsync(LOCAL_DATA_FILENAME);
            await Windows.Storage.FileIO.WriteTextAsync(file, SerializeListToXML(this.BookmarksList));
        }

        private async void FetchLocalData()
        {
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            if (await DoesFileExists(LOCAL_DATA_FILENAME))
            {
                StorageFile file = await folder.GetFileAsync(LOCAL_DATA_FILENAME);
                string listXml = await Windows.Storage.FileIO.ReadTextAsync(file);
                this.BookmarksList = DeserializeXmlToList(listXml);
            }
            else
            {
                StorageFile file = await folder.CreateFileAsync(LOCAL_DATA_FILENAME);
                this.BookmarksList = new List<Bookmarks>();
            }
        }

        // Currently, there isn't a way to validate if file exists
        // checking if able to open file
        private async Task<bool> DoesFileExists(string file)
        {
            try
            {
                StorageFolder folder = ApplicationData.Current.LocalFolder;
                await folder.GetFileAsync(file);
                return true;
            } 
            catch
            {
                return false;
            }
        }

        private string SerializeListToXML(List<Bookmarks> list)
        {
            try
            {
                XmlSerializer xml = new XmlSerializer(typeof(List<Bookmarks>));
                var writer = new StringWriter();
                xml.Serialize(writer, list);
                System.Diagnostics.Debug.WriteLine(writer.ToString());
                return writer.ToString();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return String.Empty;
            }
        }

        private List<Bookmarks> DeserializeXmlToList(string xmlString)
        {
            try
            {
                using (TextReader tr = new StringReader(xmlString))
                {
                    XmlSerializer xml = new XmlSerializer(typeof(List<Bookmarks>));
                    XmlReader xmlRead = XmlReader.Create(tr);
                    List<Bookmarks> list = new List<Bookmarks>();
                    list = (xml.Deserialize(xmlRead)) as List<Bookmarks>;
                    return list;
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return new List<Bookmarks>();
            }
        }

        #endregion
    }
}
