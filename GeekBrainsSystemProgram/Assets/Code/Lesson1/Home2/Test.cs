using System.Threading;
using UnityEngine;
using Task = System.Threading.Tasks.Task;

namespace Code.Lesson1.Task2
{
    public class Test : MonoBehaviour
    {
        private int _frame;
        private CancellationTokenSource _cancellationTokenSource;

        private void Start()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = _cancellationTokenSource.Token;
            Task task1 = Task.Run(() => SecondAsync(cancellationToken));
            Task _task2 = Task.Run(() => FrameAsync(cancellationToken));
            Task.WaitAll(task1, _task2);
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
        }

        async void SecondAsync(CancellationToken ct)
        {
            if (!ct.IsCancellationRequested)
            {
                await Task.Delay(1000);
                Debug.Log("Await 1 Second Complete");
            }
        }

        async void FrameAsync(CancellationToken ct)
        {
            while (_frame < 60)
            {
                _frame++;
                Debug.Log(_frame);
                if (_frame == 60)
                {
                    if (ct.IsCancellationRequested)
                    {
                        await Task.Yield();
                    }
                }
            }
        }
    }
}