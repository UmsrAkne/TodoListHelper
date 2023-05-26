using System;
using System.Configuration;
using LibGit2Sharp;

namespace TodoListHelper.Models
{
    public class GitManager
    {
        public GitManager(string repoPath)
        {
            RepositoryPath = repoPath;
            Repository = new Repository(RepositoryPath);
            CurrentFilePath = ConfigurationManager.AppSettings[App.TodoFilePathKeyName];
        }

        public Repository Repository { get; set; }

        public string CurrentFilePath { get; set; }

        private string RepositoryPath { get; set; }

        public void TodoAdditionCommit(Todo todo)
        {
            Commands.Stage(Repository, CurrentFilePath);
            Repository.Commit(
                todo.Title,
                Repository.Config.BuildSignature(DateTimeOffset.Now),
                Repository.Config.BuildSignature(DateTimeOffset.Now),
                new CommitOptions());
        }

        public void GetStatus()
        {
            foreach (var repositoryStatus in Repository.RetrieveStatus())
            {
                System.Diagnostics.Debug.WriteLine($"GitManager (21) : {repositoryStatus.FilePath}");
            }
        }
    }
}