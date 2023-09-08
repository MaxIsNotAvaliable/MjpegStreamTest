using WinFormsApp3.Properties;

namespace WinFormsApp3.Source
{
    internal class MjpegStream
    {
        private bool inRun = false; // логическое значение, которое указывает, запущен ли поток воспроизведения видео
        private bool noSource = false; // логическое значение, которое указывает, есть ли источник видео

        Stream? stream; // потоковый объект, который используется для получения видео с сервера
        HttpClient httpClient = new HttpClient(); // объект HttpClient, который используется для получения видео с сервера

        private string _url = "https://google.com"; // строковое значение, которое содержит URL-адрес источника видео

        public string streamUrl { get { return _url; } set { _url = value; stream = null; } } // свойство, которое позволяет получить или установить URL-адрес источника видео

        private PictureBox? _pb; // объект PictureBox, который используется для отображения видео
        public PictureBox targetPb { get { return _pb; } set { _pb = value; } } // свойство, которое позволяет получить или установить объект PictureBox

        // конструктор класса, который принимает объект PictureBox и URL-адрес источника видео
        public MjpegStream(PictureBox pb, string url)
        {
            targetPb = pb;
            streamUrl = url;
        }

        // конструктор класса по умолчанию
        public MjpegStream() { }

        // метод, который останавливает воспроизведение видео
        public void Stop()
        {
            inRun = false;
            stream = null;

            if (_pb == null) return;

            Bitmap bmp = (Bitmap)_pb.Image;

            // Создаем графический объект из изображения
            Graphics g = Graphics.FromImage(bmp);

            // Рисуем белый квадрат с помощью пера
            Pen pen = new Pen(Color.White, 8);
            g.DrawRectangle(pen, 50, 50, 8, 60);
            g.DrawRectangle(pen, 50 + 30, 50, 8, 60);

            // Обновляем изображение в picturebox
            _pb.Image = bmp;
        }

        // метод, который запускает воспроизведение видео
        public async void Start()
        {
            if (inRun) return;
            if (_pb == null)
            {
                // MjpegStream mStream = new MjpegStream();
                // mStream.targetPb = YourPictureBox;
                throw new Exception("Picture box can't be null");
            }

            inRun = true;
            try
            {
                // Запускаем воспроизведение видео
                await StartAsync();
            }
            catch (OperationCanceledException)
            {

            }
        }

        // метод, который асинхронно запускает воспроизведение видео
        private async Task StartAsync(int chunkMaxSize = 1024, int frameBufferSize = 1024 * 1024)
        {
            if (_pb == null)
            {
                // MjpegStream mStream = new MjpegStream();
                // mStream.url = YourPictureBox;
                throw new Exception("Picture box can't be null");
            }

            if (string.IsNullOrEmpty(streamUrl))
            {
                // MjpegStream mStream = new MjpegStream();
                // Stream.streamUrl = "https://YourSource.com";
                throw new Exception("URL can't be empty");
            }

            var streamBuffer = new byte[chunkMaxSize];
            var frameBuffer = new byte[frameBufferSize];

            var frameIdx = 0;
            var lastFrameIdx = frameIdx;
            var inPicture = false;
            var previous = (byte)0;
            var current = (byte)0;

            while (inRun)
            {
                try
                {
                    // Получаем объект стрима по ссылке 
                    if (stream == null)
                    {
                        stream = await httpClient.GetStreamAsync(streamUrl).ConfigureAwait(false);
                    }
                    var streamLength = await stream.ReadAsync(streamBuffer, 0, chunkMaxSize, CancellationToken.None).ConfigureAwait(false);
                    ParseBuffer(image => { _pb.Image = image; }, frameBuffer, ref frameIdx, ref lastFrameIdx, ref inPicture, ref previous, ref current, streamBuffer, streamLength);
                }
                catch
                {

                }

                // После потери соединения единожды ставим изображение "no signal.png"
                bool noVid = frameIdx == lastFrameIdx;
                if (noSource != noVid && noVid == true)
                {
                    _pb.Image = Resources.no_signal;
                }
                noSource = noVid;
            };
        }

        // метод, который парсит буфер видео и возвращает изображение
        private static void ParseBuffer(Action<Image> action, byte[] frameBuffer, ref int frameIdx, ref int lastFrameIdx, ref bool inPicture, ref byte previous, ref byte current, byte[] streamBuffer, int streamLength)
        {
            // Сохраняем последний idx для проверки изменения видео
            lastFrameIdx = frameIdx;

            /* Структура MJPEG:
             * ... 
             * 0xFF, 0xD8   -> начало изображения 
             * ...          -> изображение
             * 0xFF, 0xD9   -> конец изображения
             */
            const byte imgFlag = 0xff;
            const byte imgStart = 0xd8;
            const byte imgEnd = 0xd9;

            for (int idx = 0; idx < streamLength; idx++)
            {
                previous = current;
                current = streamBuffer[idx];

                if (inPicture)
                {
                    frameBuffer[frameIdx++] = current;
                    if (previous != imgFlag || current != imgEnd) continue;

                    // получение изображения из потока
                    Image img = null;
                    using (var s = new MemoryStream(frameBuffer, 0, frameIdx))
                    {
                        try
                        {
                            img = Image.FromStream(s);
                            Task.Run(() => action(img));
                        }
                        catch
                        {

                        }
                    }
                    inPicture = false;
                }
                else
                {
                    if (previous != imgFlag || current != imgStart) continue;

                    frameIdx = 2;
                    frameBuffer[0] = imgFlag;
                    frameBuffer[1] = imgStart;
                    inPicture = true;
                }
            }
        }
    }
}
