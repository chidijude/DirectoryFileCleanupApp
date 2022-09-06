using DirectoryFileCleanup.Worker.Interfaces;

namespace DirectoryFileCleanup.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _config;
        private readonly IFolderCleanupService _cleanupService;

        public Worker(ILogger<Worker> logger, IConfiguration config, IFolderCleanupService cleanupService)
        {
            _logger = logger;
            _config = config;
            _cleanupService = cleanupService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var directories = _cleanupService.GetDirectories();

                foreach (var directory in directories)
                {
                    await _cleanupService.Clean(directory);
                }
                
                await Task.Delay(1000 * 60 * _config.GetValue<int>("Timer"), stoppingToken);

            }
        }
    }
}