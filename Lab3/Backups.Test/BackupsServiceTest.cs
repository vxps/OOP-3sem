using System.Diagnostics.Tracing;
using Backups.Algorithms;
using Backups.Entities;
using Backups.Interfaces;
using Xunit;

namespace Backups.Test;

public class BackupsServiceTest
{
    [Fact]
    public void AddBackupTusk_CheckCreateRestorePoints()
    {
        string path = @"/home/runner/work/k1-rill/test";
        var repo = new Repository(path);
        IStorageAlgorithm algo = new SplitStorage();
        var backupObject1 = new BackupObject("hash", @"/bin/hash");
        var backupObject2 = new BackupObject("gzip", @"/bin/gzip");

        var task = new BackupTask("BackupTask", algo, repo);

        task.AddBackupObject(backupObject1);
        task.AddBackupObject(backupObject2);
        task.RunBackupTask("RP1");
        task.DeleteBackupObject(backupObject1);
        task.RunBackupTask("RP2");

        Assert.Equal(2, task.RestorePoints.Count);
        Assert.Equal(2, task.RestorePoints[0].BackupObjects.Count);
        Assert.Equal(1, task.RestorePoints[1].BackupObjects.Count);
    }
}