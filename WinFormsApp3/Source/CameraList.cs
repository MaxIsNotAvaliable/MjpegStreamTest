using System.Xml;

namespace WinFormsApp3.Source
{
    // представляет информацию о канале
    struct ChannelInfo
    {
        public string idValue;
        public string name;
    }
    // Класс CameraList представляет список камер и их информацию
    internal class CameraList
    {
        private string _url = "\0"; // Поле _url хранит URL-адрес для получения списка камер
        public string url { get { return _url; } set { _url = value; } } // свойство, которое позволяет получить или установить URL адрес источника

        private List<ChannelInfo> _list = new List<ChannelInfo>();
        public List<ChannelInfo> list { get { return _list; } set { _list = value; } } // свойство, которое позволяет получить или установить объект List

        // Конструктор класса 
        public CameraList(string listUrl)
        {
            _url = listUrl;
        }

        // Метод UpdateList асинхронно обновляет список камер из URL-адреса, хранящегося в _url
        async public void UpdateList()
        {
            // Создаем временный список для хранения нового списка каналов
            List<ChannelInfo> bufList = new List<ChannelInfo>(list.Count);

            // Проверяем URL
            if (string.IsNullOrEmpty(_url))
            {
                throw new Exception("URL can't be empty");
            }

            try
            {
                // Используем HttpClient для получения XML-документа с сервера
                using var client = new HttpClient();
                var response = await client.GetAsync(_url);
                string content = await response.Content.ReadAsStringAsync();

                // Извлекаем информацию о каналах из XML-документа
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(content);
                if (doc != null)
                {
                    XmlNodeList channelNodes = doc.SelectNodes("/Configuration/Channels/ChannelInfo");
                    foreach (XmlNode channelNode in channelNodes)
                    {
                        ChannelInfo channelInfo = new ChannelInfo();
                        var attributes = channelNode.Attributes;
                        channelInfo.idValue = attributes["Id"].Value;
                        channelInfo.name = attributes["Name"].Value;
                        bufList.Add(channelInfo);
                    }
                }
                // Обновляем список каналов
                list = bufList;
            }
            catch (Exception)
            {
                // Если получение XML-документа не удалось, то список каналов не обновляется
                list = bufList;
            }
        }
    }
}
