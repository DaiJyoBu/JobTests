
namespace KasperskyTests
{
    public class TestOne<T>
        where T : class
    {
        // Надо сделать очередь с операциями push(T) и T pop(). Операции должны поддерживать обращение с разных потоков. 
        // Операция push всегда вставляет и выходит. Операция pop ждет пока не появится новый элемент. 
        // В качестве контейнера внутри можно использовать только стандартную очередь (Queue)

        private readonly System.Collections.Queue _queue = new System.Collections.Queue();

        private readonly object _lockQueue = new object();
        private readonly object _lockPop = new object();
        private readonly System.Threading.AutoResetEvent _arEvent = new System.Threading.AutoResetEvent(false);

        public void Push(T value)
        {
            lock (_lockQueue)
            {
                _queue.Enqueue(value);
            }
            _arEvent.Set();
        }

        public T Pop()
        {
            lock (_lockPop)
            {
                lock (_lockQueue)
                {
                    if (_queue.Count > 0)
                    {
                        T result = (T)_queue.Dequeue();
                        if (_queue.Count == 0)
                            _arEvent.Reset();
                        return result;
                    }
                }

                _arEvent.WaitOne();
                lock (_lockQueue)
                {
                    return (T)_queue.Dequeue();
                }
            }
        }
    }
}
