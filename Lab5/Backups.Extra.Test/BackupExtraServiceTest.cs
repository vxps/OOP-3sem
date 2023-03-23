using Backups.Algorithms;
using Backups.Entities;
using Backups.Extra.Algorithms.Clean;
using Backups.Extra.Algorithms.Limits;
using Backups.Extra.Entities;
using Backups.Extra.Interfaces;
using Backups.Extra.Loggers;
using Backups.Extra.Saver;
using Backups.Interfaces;
using Xunit;

namespace Backups.Extra.Test;

public class BackupExtraServiceTest
{
    [Fact]
    public void AddBackupTusk_CheckCreateRestorePoints()
    {
        string path = @"/home/runner/work/k1-rill/test";
        var repo = new Repository(path);
        IStorageAlgorithm algo = new SingleStorage();
        var backupObject1 = new BackupObject("hash", @"/bin/hash");
        var backupObject2 = new BackupObject("gzip", @"/bin/gzip");
        var logger = new ConsoleLogger();

        var task = new BackupTaskExtra("BackupTask", algo, repo, logger, true);

        task.AddExtraBackupObject(backupObject1);
        task.AddExtraBackupObject(backupObject2);
        task.RunBackupExtra("RP1", true);
        task.RunBackupExtra("RP2", true);
        var limit = new ChooseByCount(1);
        var clean = new CleanAlgorithm(task);
        task.CleanRestorePoints(clean, task.ChoosePointsByLimit(limit));

        Assert.Equal(1, task.RestorePoints.Count);
    }

    [Fact]
    public void CheckSaveBackup()
    {
        string path = @"/home/runner/work/k1-rill/test";
        var repo = new Repository(path);
        IStorageAlgorithm algo = new SingleStorage();
        var backupObject1 = new BackupObject("hash", @"/bin/hash");
        var backupObject2 = new BackupObject("gzip", @"/bin/gzip");
        var logger = new ConsoleLogger();

        var task = new BackupTaskExtra("BackupTask", algo, repo, logger, true);

        task.AddExtraBackupObject(backupObject1);
        task.AddExtraBackupObject(backupObject2);
        var restore = new RestorePoint("Rp1", DateTime.Now);
        var restore2 = new RestorePoint("Rp2", DateTime.Now);
        task.AddExtraRestorePoint(restore);
        task.AddExtraRestorePoint(restore2);

        var saver = new Save();
        saver.SaveState(task, path + ".json");
        Assert.Equal(2, task.RestorePoints.Count);
    }

    [Fact]
    public void MergePoints_CheckMerged()
    {
        string path = @"/home/runner/work/k1-rill/test";
        var repo = new Repository(path);
        IStorageAlgorithm algo = new SingleStorage();
        var backupObject1 = new BackupObject("hash", @"/bin/hash");
        var backupObject2 = new BackupObject("gzip", @"/bin/gzip");
        var logger = new ConsoleLogger();

        var task = new BackupTaskExtra("BackupTask", algo, repo, logger, true);

        task.AddExtraBackupObject(backupObject1);
        task.AddExtraBackupObject(backupObject2);
        var restore = new RestorePoint("Rp1", DateTime.Now);
        var restore2 = new RestorePoint("Rp2", DateTime.Now);
        task.AddExtraRestorePoint(restore);
        task.AddExtraRestorePoint(restore2);
        var merger = new Merge();
        task.MergePoints(task.RestorePoints[1], task.RestorePoints[0], merger);
        Assert.Equal(1, task.RestorePoints.Count);
    }
}