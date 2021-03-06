﻿function Task(data) {
    var self = this;
    self.text = ko.observable(data.Text);
    self.isDone = ko.observable(data.IsDone);
    self.taskID = ko.observable(data.TaskItemID);
    self.noteID = ko.observable(data.NoteID);

    self.updateTask = function () {
        NoteeFy.changeNotificationStatus(1);
        $.ajax({
            url: "api/TaskItems/" + self.taskID(),
            type: "POST",
            data: {
                TaskItemID: self.taskID(),
                Text: self.text(),
                IsDone: self.isDone(),
                NoteID: self.noteID()
            },
            complete: function () {
                NoteeFy.refreshLayout();
                NoteeFy.changeNotificationStatus(0);
            }
        });
    };
}