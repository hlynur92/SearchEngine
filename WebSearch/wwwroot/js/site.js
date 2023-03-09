let me = this;

me.searchTerms = ko.observable();
me.hits = ko.observable();
me.results = ko.observableArray();
me.timeUsed = ko.observable();

me.search = function () {
    $.ajax({
        url: "http://localhost:5041/search?terms=" + me.searchTerms() + "&numberOfResults=10",
        success: function (data) {
            me.hits(data.documents.length);
            me.timeUsed(data.elapsedMilliseconds);
            me.results.removeAll();
            data.documents.foreach(function (hit) {
                me.results.push(hit);
            });
        }
    });
};
ko.applyBindings(new ViewModel());
