﻿using NoteeFY.Data.Models;

namespace NoteeFY.Buisness.DTOs
{
    public class TaskItemDTO
    {
        public int TaskItemID { get; set; }
        public string Text { get; set; }
        public bool IsDone { get; set; }

        public TaskItemDTO() { }

        public TaskItemDTO(TaskItem task)
        {
            if (task != null)
            {
                TaskItemID = task.TaskItemID;
                Text = task.Text;
                IsDone = task.IsDone;
            }
        }
    }
}
