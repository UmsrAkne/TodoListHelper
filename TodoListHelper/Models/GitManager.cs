using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
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

        private Signature Sig => Repository.Config.BuildSignature(DateTimeOffset.Now);

        /// <summary>
        /// 追加した Todo のタイトルをメッセージとして git commit を実行します。
        /// </summary>
        /// <param name="todo">新しく追加した Todo を入力します</param>
        public void TodoAdditionCommit(Todo todo)
        {
            Commit($"add     / {todo.Title}");
        }

        /// <summary>
        /// 作業開始した Todo のタイトルをメッセージとして git commit を実行します。
        /// </summary>
        /// <param name="todo">作業開始した Todo を入力</param>
        public void TodoStartCommit(Todo todo)
        {
            Commit($"start   / {todo.Title}");
        }

        /// <summary>
        /// 完了した Todo のタイトルをメッセージとして git commit を実行します。
        /// </summary>
        /// <param name="todo">完了した Todo を入力</param>
        public void TodoFinishCommit(Todo todo)
        {
            Commit($"finish  / {todo.Title}");
        }

        public void AddComment(string comment)
        {
            Commit($"message / {comment}");
        }

        public List<Commit> GetCommits()
        {
            return Repository.Commits.OrderByDescending(c => c.Author.When).Take(100).ToList();
        }

        public void GetStatus()
        {
            foreach (var repositoryStatus in Repository.RetrieveStatus())
            {
                System.Diagnostics.Debug.WriteLine($"GitManager (21) : {repositoryStatus.FilePath}");
            }
        }

        /// <summary>
        /// CurrentFilePath のファイルをステージングした上で、入力したメッセージを使って git commit を実行します。
        /// </summary>
        /// <param name="msg">コミットメッセージです</param>
        private void Commit(string msg)
        {
            Commands.Stage(Repository, CurrentFilePath);
            Repository.Commit(msg, Sig, Sig, new CommitOptions());
        }
    }
}