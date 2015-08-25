﻿function Note(data) {
    var self = this;

    self.title = ko.observable();
    self.text = ko.observable();
    self.type = ko.observable(0);
    self.userID = ko.observable();
    self.noteID = ko.observable();
    self.color = ko.observable();
    self.modificationDate = ko.observable();

    self.tasks = ko.observableArray([]);
    self.currentTask = ko.observable("");

    self.isEditTitle = ko.observable(false);
    self.isEditText = ko.observable(false);

    self.textAsHtml = ko.computed(function () {
        if (!self.text()) {
            return '';
        }
        return self.text().replace(/\n/g, "</br>");
    });

    if (data) {
        self.initialize(data);
    }
}

Note.prototype.initialize = function(data) {
    var self = this;
    self.title(data.Title);
    self.text(data.Text);
    self.type(data.Type);
    self.userID(data.UserID);
    self.noteID(data.NoteID);
    self.color(data.Color);
    self.modificationDate(self.getModificationDate(data));

    var mappedTasks = $.map(data.TaskItems, function(item) { return new Task(item); });
    self.tasks(mappedTasks);
};

Note.prototype.addTask = function () {
    var self = this;
    NoteeFy.changeNotificationStatus(1);
    $.ajax({
        url: "api/TaskItems/",
        type: "POST",
        data: {
            Text: self.currentTask,
            IsDone: false,
            NoteID: self.noteID
        },
        success: function (response) {
            var task = new Task(response.Data);
            self.tasks.unshift(task);
            self.currentTask("");
            self.modificationDate(self.getModificationDate());
            NoteeFy.refreshTextarea();
            NoteeFy.refreshLayout();
            NoteeFy.changeNotificationStatus(0);
        }
    });
};

Note.prototype.deleteTask = function (task) {
    var self = this;
    NoteeFy.changeNotificationStatus(1);
    $.ajax({
        url: "api/TaskItems/" + task.taskID(),
        type: "DELETE",
        success: function () {
            self.tasks.remove(task);
            self.modificationDate(self.getModificationDate());
            NoteeFy.refreshLayout();
            NoteeFy.changeNotificationStatus(0);
        }
    });
};

Note.prototype.updateNote = function() {
    var self = this;
    NoteeFy.changeNotificationStatus(1);
    $.ajax({
        url: "api/Notes/" + self.noteID(),
        type: "POST",
        data: {
            NoteID: self.noteID(),
            Title: self.title(),
            Text: self.text(),
            Type: self.type(),
            UserID: self.userID(),
            Color: self.color(),
            TaskItems: []
        },
        success: function (response) {
            self.modificationDate(self.getModificationDate(response.Data));
        },
        complete: function () {
            NoteeFy.changeNotificationStatus(0);
            NoteeFy.refreshLayout();
        }
    });
};

Note.prototype.goOnListBottom = function(task) {
    var temp = jQuery.extend(true, {}, task);
    var self = this;

    if (task.isDone()) {
        self.tasks.destroy(task);
        self.tasks.push(temp);
    } else {
        self.tasks.destroy(task);
        self.tasks.unshift(temp);
    }
    task.updateTask();
    NoteeFy.refreshTextarea();
};

Note.prototype.getModificationDate = function (data) {
    moment.locale('pl');
    if (data) {
        return moment(data.ModificationDate).startOf('minute').fromNow();
    } else {
        return moment(moment()).startOf('minute').fromNow();
    }
};

Note.prototype.addColorPicker = function (note, event) {
    $(event.target).colpick({
        layout: 'hex',
        submit: 0,
        color: 'FBEA6E',
        onChange: function (hsb, hex, rgb, el, bySetColor) {
            note.color('#' + hex);
        },
        onHide: function () {
            note.updateNote();
        }
    }
    );
}