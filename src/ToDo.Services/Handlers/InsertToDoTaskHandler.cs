﻿using Microsoft.Extensions.Logging;
using System;
using ToDo.Core.Commands;
using ToDo.Core.Models;
using ToDo.Infrastructure;

namespace ToDo.Services.Handlers
{
    public class InsertToDoTaskHandler
    {
        IToDoTaskRepository _repository;
        ILogger<InsertToDoTaskHandler> _logger;

        public InsertToDoTaskHandler(IToDoTaskRepository repository, ILogger<InsertToDoTaskHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public CommandResult Execute(InsertToDoTask command)
        {
            try
            {
                var task = new ToDoTask
                (
                    id: 0,
                    title: command.Title,
                    deadline: command.Deadline,
                    category: command.Category,
                    completionDate: null,
                    status: ToDoTaskStatus.Created
                );

                _logger.LogDebug("Persistindo a tarefa...");
                _repository.InsertTasks(task);

                return new CommandResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new CommandResult(false);
            }
        }
    }
}
