using AssettoServer.Server.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace LiveServerNamePlugin;

public class LiveServerName
{

    private const int MAX_ENTRIES = 20;

    private readonly ACServerConfiguration _acServerConfiguration;
    private readonly LiveServerNameConfiguration _configuration;

    private readonly string _originalName = "";
    private readonly List<string> _listOfNamesCopy = new();
    private int _currentNameIndex = 0;

    private readonly CancellationTokenSource _cancellationTokenSource = new();

    public LiveServerName(ACServerConfiguration acServerConfiguration, LiveServerNameConfiguration configuration, IHostApplicationLifetime applicationLifetime)
    {
        _acServerConfiguration = acServerConfiguration;
        _configuration = configuration;

        _originalName = acServerConfiguration.Server.Name;
        _listOfNamesCopy = configuration.ListOfNames;
        if (_listOfNamesCopy.Count < 1)
        {
            Log.Warning($"No entries found for LiveServerNameConfiguration");
        }
        else
        {
            if (_listOfNamesCopy.Count > MAX_ENTRIES)
            {
                _listOfNamesCopy = _listOfNamesCopy.Take(MAX_ENTRIES).ToList();
            }

            Log.Information($"Loaded LiveServerNamePlugin with ({_listOfNamesCopy.Count}) entries: {string.Join(", ", _listOfNamesCopy)}");
            StartAsyncTask();
        }

        applicationLifetime.ApplicationStopping.Register(OnApplicationStopping);
    }

    private async void StartAsyncTask()
    {
        await StartAsyncServerName();
    }

    private async Task StartAsyncServerName()
    {
        try
        {
            while (!_cancellationTokenSource.IsCancellationRequested)
            {
                int add = _currentNameIndex += 1;
                string newName = _originalName;
                if (!_configuration.Randomize)
                {
                    if (add + 1 > _listOfNamesCopy.Count)
                    {
                        newName = _listOfNamesCopy[0];
                        _currentNameIndex = 0;
                    }
                    else
                    {
                        newName = _listOfNamesCopy[add];
                    }
                }
                else
                {
                    Random random = new();
                    newName = _listOfNamesCopy[random.Next(0, _listOfNamesCopy.Count)];
                }

                _acServerConfiguration.Server.Name = newName;
                await Task.Delay(_configuration.UpdateInterval * 1000);
            }
        }
        catch (TaskCanceledException e)
        {
            Console.WriteLine($"Async operation was canceled: {e}");
        }
    }

    private void OnApplicationStopping()
    {
        _cancellationTokenSource.Cancel();
    }
}
