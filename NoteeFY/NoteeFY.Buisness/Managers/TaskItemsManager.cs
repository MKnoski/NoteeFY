﻿using System.Collections.Generic;
using System.Linq;
using NoteeFY.Buisness.DTOs;
using NoteeFY.Data;

namespace NoteeFY.Buisness.Managers
{
    public class TaskItemsManager
    {

        public void AddOrUpdateTaskItems(int noteId, IEnumerable<TaskItemDTO> tasks)
        {
            using (var db = new NoteeContext())
            {

                foreach (var taskItem in tasks)
                {
                    TaskItem model;
                    if (taskItem.TaskItemID > 0)
                    {
                        model = db.TaskItems.Single(ti => ti.TaskItemID == taskItem.TaskItemID);
                    }
                    else
                    {
                        model = db.TaskItems.Create();
                        model.Note = db.Notes.Single(n => n.NoteID == noteId);
                        db.TaskItems.Add(model);
                    }

                    model.Text = taskItem.Text;
                    model.IsDone = taskItem.IsDone;

                    db.SaveChanges();
                    taskItem.TaskItemID = model.TaskItemID;
                    new NotesManager().UpdateModificationTime(model.NoteID);
                }
            }
        }

        public bool AddOrUpdateTaskItem(TaskItemDTO taskItem, string authorizedUserID)
        {
            using (var db = new NoteeContext())
            {
                var note = db.Notes.SingleOrDefault(n => n.NoteID == taskItem.NoteID);
                var user = db.SubUsers.SingleOrDefault(u => u.UserID == note.UserID);
                if (db.Notes.Any(n => n.NoteID == taskItem.NoteID) && user.UserID == authorizedUserID)
                {
                    TaskItem model;
                    if (taskItem.TaskItemID > 0)
                    {
                        model = db.TaskItems.Single(ti => ti.TaskItemID == taskItem.TaskItemID);
                    }
                    else
                    {
                        model = db.TaskItems.Create();
                        model.Note = db.Notes.Single(n => n.NoteID == taskItem.NoteID);
                        db.TaskItems.Add(model);
                    }

                    model.Text = taskItem.Text;
                    model.IsDone = taskItem.IsDone;

                    db.SaveChanges();
                    taskItem.TaskItemID = model.TaskItemID;
                    new NotesManager().UpdateModificationTime(model.NoteID);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        public bool DeleteTaskItem(int id, string authorizedUserID)
        {
            using (var db = new NoteeContext())
            {
                var taskItem = db.TaskItems.SingleOrDefault(ti => ti.TaskItemID == id);
                var note = db.Notes.SingleOrDefault(n => n.NoteID == taskItem.NoteID);
                var user = db.SubUsers.SingleOrDefault(u => u.UserID == note.UserID);

                if (authorizedUserID != user.UserID)
                {
                    return false;
                }

                if (taskItem == null)
                {
                    return false;
                }
                else
                {
                    db.TaskItems.Remove(taskItem);
                    db.SaveChanges();
                    new NotesManager().UpdateModificationTime(taskItem.NoteID);
                    return true;
                }
            }
        }
    }
}
